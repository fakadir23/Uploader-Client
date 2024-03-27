using ISTL.COMMON;
using ISTL.COMMON.Subscription;
using ISTL.COMMON.Threads;
using ISTL.MODELS.Common;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.MODELS.DTO.New.NotEntry;
using ISTL.MODELS.Request.Beneficiary;
using ISTL.MODELS.Request.WorkStation;
using ISTL.MODELS.Response.New;
using ISTL.PERSOGlobals;
using ISTL.RAB.ApiManager;
using ISTL.RAB.DbManager;
using ISTL.RAB.Entity;
using ISTL.RAB.View;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ISTL.RAB.Asynch
{
    public class UploadThreadsAsync : IThreadable
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        //private const int SLEEP_TIME = 5 * 60 * 1000;
        private const int SLEEP_TIME = 10 * 1000;
        //private const int SLEEP_TIME = 500;
        private const string NAME = "profile_uploader";
        private AutoResetEvent waitHandle = new AutoResetEvent(false);
        private AutoResetEvent abortHandle = new AutoResetEvent(false);
        private DateTime verificationTimeStamp = DateTime.MinValue;
        private WorkStationApiManager workStationApiManager;

        public UploadThreadsAsync()
        {
            workStationApiManager = new WorkStationApiManager();
        }

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
                logger.Debug("PROFILE UPLOADER: task started!");
                while (true)
                {
                    if (!Upload())
                    {
                        // Only when thread stop request received
                        break;
                    }

                    logger.Debug("PROFILE UPLOADER: Going into sleep mode!");
                    GetWaitHandle().WaitOne(GetSleepTime());
                    logger.Debug("PROFILE UPLOADER: Woke up from sleep!");
                    if (GetAbortHandle().WaitOne(1))
                    {
                        logger.Debug("PROFILE UPLOADER: Abort request received. Getting out!");
                        break;
                    }
                }
            }
            catch (ThreadAbortException e)
            {
                logger.Debug("PROFILE UPLOADER: Force shutdown initiated! ThreadAbortException: " + e.Message);
            }
        }

        private bool Upload()
        {
            bool uploadFailed = false;
            UploadSubject uploadStatus = (UploadSubject)SubjectFactory.GetInstance().GetSubject(UploadSubject.Name);
            OnlineSubject onlineStatus = (OnlineSubject)SubjectFactory.GetInstance().GetSubject(OnlineSubject.Name);
            CounterPendingSubject counterPendingStatus = (CounterPendingSubject)SubjectFactory.GetInstance().GetSubject(Globals.Common.COUNTER_PENDING_NAME);
            CounterErrorSubject counterErrorStatus = (CounterErrorSubject)SubjectFactory.GetInstance().GetSubject(Globals.Common.COUNTER_ERROR_NAME);
            CounterDraftSubject counterDraftStatus = (CounterDraftSubject)SubjectFactory.GetInstance().GetSubject(Globals.Common.COUNTER_DRAFT_NAME);
            DbEnrollClientManager enrollClient = new DbEnrollClientManager();
            BeneficiaryApiManager beneficiaryApiManager = new BeneficiaryApiManager();

            // The following loop is necessary when more enrollments were done while 
            // looping through the hashlist. The pending count will determine the exit condition

            TimeZone zone = TimeZone.CurrentTimeZone;
            while (true)
            {
                // Set access token
                AppUtils.AppUtils.SetTokenByAdminUser();               

                //Verify enroll:START
                //enrollClient.GetVerifyDayCount(); //look up verifyDayCount from local db
                TimeSpan diff = zone.ToLocalTime(DateTime.Now) - verificationTimeStamp;

                //if (verificationTimeStamp == DateTime.MinValue
                //    || (diff.Days * 24 + diff.Hours) > 24)
                //{
                //    VerifyEnroll verifyEnroll = new VerifyEnroll();
                //    if (verifyEnroll.EnrollVerify())
                //    {
                //        verificationTimeStamp = zone.ToLocalTime(DateTime.Now);
                //    }
                //}


                //Verify enroll:END

                // Get only the list of hash of all pending uploads (i.e. all exported and new records)
                // Shouldn't be an issue storing all in memory
                List<string> applicationIdList = enrollClient.GetUnsyncedApplicationIds();
                int pendingCount = applicationIdList.Count;
                uploadStatus.Pending = pendingCount;

                // Exit loop if noothing left to upload, or if the last upload failed
                if ((pendingCount == 0) || uploadFailed)
                {
                    // Notify IDLE status only if upload did not fail
                    if (!uploadFailed)
                    {
                        uploadStatus.State = UploadSubject.Status.IDLE;
                        uploadStatus.Notify();
                    }
                    break;
                }

                uploadStatus.State = UploadSubject.Status.IDLE;
                uploadStatus.Pending = pendingCount;
                uploadStatus.Notify();

                // Exit loop on thread stop request
                if (GetAbortHandle().WaitOne(1))
                {
                    logger.Debug("PROFILE UPLOADER: Abort request received. Getting out!");
                    return false;
                }
                // If logged in offline, access token will not be present
                if (!string.IsNullOrEmpty(Users.AccessToken))
                {
                    // Upload each record of corresponding hash (from hashList)
                    // but will ignore new enrollments during this time
                    foreach (string applicationId in applicationIdList)
                    {
                        Exception ex = null;
                        bool isEnrolled = false;
                        uploadStatus.State = UploadSubject.Status.UPLOADING;
                        uploadStatus.Pending = pendingCount; // decrease this count after uploading record
                        uploadStatus.Notify();
                        try
                        {
                            Stopwatch sw = new Stopwatch();
                            sw.Start();

                            EnrollmentApiManager enrollmentApiManager = new EnrollmentApiManager();

                            // Check if record is already uploaded (maybe from an exported DB)
                            // Send only hash value to web service
                            //int code = enrollmentApiManager.CheckEnrolledHash(hash);
                            int code = 0;
                            if (code == 0)
                            {
                                // Record not in server. So upload it
                                RegisterBeneficiaryRequest dto = enrollClient.getUnsyncedData(applicationId);

                                //JavaScriptSerializer jSerial = new JavaScriptSerializer();
                                //jSerial.MaxJsonLength = 999999999;

                                //var json = jSerial.Serialize(dto);
                                //Console.WriteLine("json: \n" + json);

                                bool statusCode = beneficiaryApiManager.ProfileSubmit(dto);

                                //EnrollmentDto enrollmentDto = enrollClient.GetEnrolledData(hash);
                                //ApiResponse normalSaveResponse = enrollmentApiManager.ProfileSubmit(enrollmentDto);
                                if (statusCode)
                                {
                                    isEnrolled = true;
                                    logger.Info("Successfully Enrolled Beneficiary Profile by Web API. Application Id: " + dto.applicationId);

                                    //Returning pending time of pending uploads
                                    sw.Stop();
                                    uploadStatus.TimeLeft = Utils.GetPendingTime(1, sw.ElapsedMilliseconds, (pendingCount - 1));
                                }
                                else
                                {
                                    uploadFailed = true;
                                    logger.Error("Error Occured During uploading Beneficiary Profile by Web API. Application Id: " + dto.applicationId);
                                    string errorMsg = "There was an error during uploading Beneficiary Profile through Web API.\nApplication Id: " + dto.applicationId;
                                    enrollClient.UpdateBeneficiaryUploadErrorStatus(applicationId);
                                    CustomMessageBox.ShowMessage("SNSOP TOOLS", errorMsg);

                                    onlineStatus.IsOnline = true;
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
                                //enrollClient.UpdateStatusToUploaded(applicationId);
                                enrollClient.UpdateStatusToSynced(applicationId);
                                logger.Info("Successfully enrolled beneficiary by Web API and Updated Local DB. " + applicationId);

                                onlineStatus.IsOnline = true; // Since upload is successful, status should be online (incase it was showing offline)
                                onlineStatus.Notify();
                            }

                            counterPendingStatus.Count = enrollClient.GetBeneficiaryUploadPendingCount();
                            counterPendingStatus.Notify();
                            counterErrorStatus.Count = enrollClient.GetBeneficiaryUploadErrorCount();
                            counterErrorStatus.Notify();
                            counterDraftStatus.Count = enrollClient.GetUploadedCount();
                            counterDraftStatus.Notify();
                        }
                        catch (System.Net.WebException x)
                        {
                            // Connection problems. This is not serious
                            logger.Debug("Connection problems during upload Criminal Profile. This is not serious." + x.Message);
                            uploadFailed = true;
                            onlineStatus.IsOnline = false;
                        }
                        catch (TimeoutException x)
                        {
                            logger.Error("Connection timed out!\n" + x.ToString());
                            ErrorMessageBox.ShowError("The connection timed out during upload Criminal Profile.", x);
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
                            logger.Error("There was an unexpected error during upload Criminal Profile or fetch Criminal Profile from DB. " + applicationId + "\n" + x.ToString());
                            ex = x;
                            //ErrorMessageBox.ShowError("There was an unexpected error during upload Criminal Profile.", x);
                            uploadFailed = true;
                            // This is possibly not due to any network issue, so not setting network status.
                            enrollClient.UpdateBeneficiaryUploadErrorStatus(applicationId);
                            counterPendingStatus.Count = enrollClient.GetBeneficiaryUploadPendingCount();
                            counterPendingStatus.Notify();
                            counterErrorStatus.Count = enrollClient.GetBeneficiaryUploadErrorCount();
                            counterErrorStatus.Notify();
                            counterDraftStatus.Count = enrollClient.GetUploadedCount();
                            counterDraftStatus.Notify();
                            logger.Error("Error occurred when uploading Criminal Profile and updated local db" + applicationId);
                            onlineStatus.IsOnline = true;
                        }

                        if (ex != null)
                        {
                            //ErrorMessageBox.ShowError("There was an unexpected error during upload Criminal Profile.", ex);
                        }

                        if (uploadFailed)
                        {
                            //enrollClient.UpdateErrorStatus(hash);
                            enrollClient.UpdateBeneficiaryUploadErrorStatus(applicationId);
                            uploadStatus.State = UploadSubject.Status.FAILED;
                            uploadStatus.Notify();
                            onlineStatus.Notify(); // online status should already have been set if this was due to a connection problem
                            break; // Stop looping through hashList, but need to check if there are any new pending uploads
                        }
                    } // Looping through hash list.
                } // Access Token exists loop
            } // While loop
            return true;
        }
    }
}
