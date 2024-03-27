using ISTL.COMMON;
using ISTL.COMMON.Subscription;
using ISTL.COMMON.Threads;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.PERSOGlobals;
using ISTL.RAB.ApiManager;
using ISTL.RAB.DbManager;
using ISTL.RAB.View;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ISTL.RAB.Asynch
{
    public class UploadSpecialEnrollAsync : IThreadable
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        //private const int SLEEP_TIME = 5 * 60 * 1000;
        private const int SLEEP_TIME = 20 * 1000;
        private const string NAME = "special_profile_uploader";
        private AutoResetEvent waitHandle = new AutoResetEvent(false);
        private AutoResetEvent abortHandle = new AutoResetEvent(false);
        private DateTime verificationTimeStamp = DateTime.MinValue;

        public string GetName()
        {
            return NAME;
        }

        public int GetSleepTime()
        {
            return SLEEP_TIME;
        }

        public AutoResetEvent GetWaitHandle()
        {
            return waitHandle;
        }

        public AutoResetEvent GetAbortHandle()
        {
            return abortHandle;
        }

        public void Task()
        {
            try
            {
                logger.Debug("SPECIAL PROFILE UPLOADER: task started!");
                while (true)
                {
                    if (!Upload())
                    {
                        // Only when thread stop request received
                        break;
                    }

                    logger.Debug("SPECIAL PROFILE UPLOADER: Going into sleep mode!");
                    GetWaitHandle().WaitOne(GetSleepTime());
                    logger.Debug("SPECIAL PROFILE UPLOADER: Woke up from sleep!");
                    if (GetAbortHandle().WaitOne(1))
                    {
                        logger.Debug("SPECIAL PROFILE UPLOADER: Abort request received. Getting out!");
                        break;
                    }
                }
            }
            catch (ThreadAbortException e)
            {
                logger.Debug("SPECIAL PROFILE UPLOADER: Force shutdown initiated! ThreadAbortException: " + e.Message);
            }
        }

        private bool Upload()
        {
            bool uploadFailed = false;
            UploadSubject uploadStatus = (UploadSubject)SubjectFactory.GetInstance().GetSubject(UploadSubject.Name);
            OnlineSubject onlineStatus = (OnlineSubject)SubjectFactory.GetInstance().GetSubject(OnlineSubject.Name);
            CounterErrorSubject counterErrorStatus = (CounterErrorSubject)SubjectFactory.GetInstance().GetSubject(Globals.Common.COUNTER_ERROR_NAME);
            DbSpecialEnrollManager enrollClient = new DbSpecialEnrollManager();

            TimeZone zone = TimeZone.CurrentTimeZone;
            while (true)
            {
                AppUtils.AppUtils.SetTokenByAdminUser();

                enrollClient.GetVerifyDayCount(); //look up verifyDayCount from local db
                TimeSpan diff = zone.ToLocalTime(DateTime.Now) - verificationTimeStamp;

                if (verificationTimeStamp == DateTime.MinValue
                    || (diff.Days * 24 + diff.Hours) > 24)
                {
                    VerifyEnroll verifyEnroll = new VerifyEnroll();
                    if (verifyEnroll.SpecialEnrollVerify())
                    {
                        verificationTimeStamp = zone.ToLocalTime(DateTime.Now);
                    }
                }
                //Verify enroll:END

                // Get only the list of hash of all pending uploads (i.e. all exported and new records)
                // Shouldn't be an issue storing all in memory
                List<string> hashList = enrollClient.GetEnrolledHash();
                int pendingCount = hashList.Count;
                uploadStatus.Pending = pendingCount;

                // Exit loop if noothing left to upload, or if the last upload failed
                if (pendingCount == 0 || uploadFailed)
                {
                    // Notify IDLE status only if upload did not fail
                    if (!uploadFailed)
                    {
                        uploadStatus.State = UploadSubject.Status.IDLE;
                        uploadStatus.Notify();
                    }
                    break;
                }

                // Exit loop on thread stop request
                if (GetAbortHandle().WaitOne(1))
                {
                    logger.Debug("PROFILE UPLOADER: Abort request received. Getting out!");
                    return false;
                }

                // Upload each record of corresponding hash (from hashList)
                // but will ignore new enrollments during this time
                foreach (string hash in hashList)
                {
                    bool isEnrolled = false;
                    uploadStatus.State = UploadSubject.Status.UPLOADING;
                    uploadStatus.Pending = pendingCount; // decrease this count after uploading record
                    uploadStatus.Notify();
                    try
                    {
                        Stopwatch sw = new Stopwatch();
                        sw.Start();

                        SpecialEnrollApiManager enrollmentApiManager = new SpecialEnrollApiManager();

                        // Check if record is already uploaded (maybe from an exported DB)
                        // Send only hash value to web service
                        int code = enrollmentApiManager.CheckSpecialEnrolledHash(hash);
                        if (code == 0)
                        {
                            // Record not in server. So upload it
                            SpecialEnrollmentDto specialEnrollmentDto = enrollClient.GetLocalSpecialEnrolled(hash);
                            if (enrollmentApiManager.SpecialProfileSubmit(specialEnrollmentDto)?.code == 200)
                            {
                                isEnrolled = true;
                                logger.Info("Successfully Enrolled Criminal Profile by Web API. Hash: " + hash);

                                //Returning pending time of pending uploads
                                sw.Stop();
                            }
                            else
                            {
                                uploadFailed = true;
                                logger.Error("Unexpected Error Occured During uploading Criminal Profile. " + hash);
                                System.Windows.Forms.MessageBox.Show("There was an unexpected error during upload Criminal Profile.", "RAB CDMS");
                                enrollClient.UpdateErrorStatus(hash);
                            }
                        }
                        else
                        {
                            // Record already in server. So skip upload
                            isEnrolled = true;
                        }

                        if (isEnrolled)
                        {
                            --pendingCount;
                            enrollClient.UpdateStatusToUploaded(hash);
                            logger.Info("Successfully Enrolled To Web API and Update Local DB. " + hash);

                            onlineStatus.IsOnline = true; // Since upload is successful, status should be online (incase it was showing offline)
                            onlineStatus.Notify();
                        }
                    }
                    catch (System.Net.WebException x)
                    {
                        // Connection problems. This is not serious
                        logger.Debug("Connection problems. This is not serious." + x.Message);
                        uploadFailed = true;
                        onlineStatus.IsOnline = false;
                    }
                    catch (TimeoutException x)
                    {
                        logger.Error("Connection timed out!\n" + x.ToString());
                        ErrorMessageBox.ShowError("The connection timed out during upload.", x);
                        uploadFailed = true;
                        // Not sure if this is due to network issue or server is slow
                        // So not setting network status
                    }
                    catch (System.ServiceModel.EndpointNotFoundException x)
                    {
                        logger.Debug("EndPointNotFound during upload!" + x.Message);
                        uploadFailed = true;
                        onlineStatus.IsOnline = false;
                    }
                    catch (ThreadAbortException x)
                    {
                        logger.Debug("Inside UploadEnrollment: Force shutdown initiated! ThreadAbortException: " + x.Message);
                        return false; // Return from here since application is shutting down. No need to notify all observers.
                    }
                    catch (System.Exception x)
                    {
                        // Is Online but got unexpected exception
                        logger.Error("There was an unexpected error during upload. " + hash + "\n" + x.ToString());
                        ErrorMessageBox.ShowError("There was an unexpected error during upload.", x);
                        uploadFailed = true;
                        // This is possibly not due to any network issue, so not setting network status.

                        enrollClient.UpdateErrorStatus(hash);
                        logger.Error("Error occurred when uploading and updating local db" + hash);
                        onlineStatus.IsOnline = true;
                    }

                    if (uploadFailed)
                    {
                        uploadStatus.State = UploadSubject.Status.FAILED;
                        uploadStatus.Notify();
                        onlineStatus.Notify(); // online status should already have been set if this was due to a connection problem
                        break; // Stop looping through hashList, but need to check if there are any new pending uploads
                    }
                } // Looping through hash list.
            } // While loop

            return true;
        }
    }
}
