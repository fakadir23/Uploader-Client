using ISTL.COMMON;
using ISTL.COMMON.Subscription;
using ISTL.COMMON.Threads;
using ISTL.MODELS.Common;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.PERSOGlobals;
using ISTL.RAB.ApiManager;
using ISTL.RAB.Asynch;
using ISTL.RAB.DbManager;
using ISTL.RAB.Entity;
using ISTL.RAB.View;
using ISTL.RAB.View.New;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.Controllers.New.Enrollment
{
    public class PreviewSubmitController : ViewController
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private PreviewAndSubmitUserControl previewAndSubmitUserControl;
        private DbEnrollClientManager dbEnrollClientManager;
        private DbLookupManager dblookupManager;

        public PreviewSubmitController()
        {
            previewAndSubmitUserControl = new PreviewAndSubmitUserControl();
            base.SetView((IView)previewAndSubmitUserControl);
            previewAndSubmitUserControl.SetController(this);

            dbEnrollClientManager = new DbEnrollClientManager();
            dblookupManager = new DbLookupManager();
        }

        public override string GetName()
        {
            return Globals.ChildControllers.PREVIEW_SUBMIT;
        }

        public string GetValueForLookups(string columnName, string tableName, int id)
        {
            string value = dblookupManager.GetValueForLookup(columnName, tableName, id);
            return value;
        }

        public string GetCrimeTypeValueForLookup(int id)
        {
            string value;
            string nameEn = dblookupManager.GetValueForLookup("name_en", "crime_type", id);
            string nameBn = dblookupManager.GetValueForLookup("name_bn", "crime_type", id);
            value = nameEn;
            if (!string.IsNullOrEmpty(nameBn)) value += " (" + nameBn + ")";
            return value;
        }

        public void CriminalProfile()
        {
            parent.AddChild(Globals.ChildControllers.ENROLL);
        }

        public void Family()
        {
            parent.AddChild(Globals.ChildControllers.OTHER_INFO);
        }

        public void Biometric()
        {
            ((MainController)parent).SearchCriteriaBeforeEnrollment = Globals.SearchCriteriaBeforeEnrollment.CDMS_Search;
            parent.AddChild(Globals.ChildControllers.BIOMETRIC);
        }

        public void Submit()
        {
            bool ret = false;

            int bioMissing = 0;
            if (StaticData.Enrollment?.profile?.biometric?.fingerprint?.lt == null
                && StaticData.Enrollment?.profile?.biometric?.fingerprint?.li == null
                && StaticData.Enrollment?.profile?.biometric?.fingerprint?.lm == null
                && StaticData.Enrollment?.profile?.biometric?.fingerprint?.lr == null
                && StaticData.Enrollment?.profile?.biometric?.fingerprint?.ls == null
                && StaticData.Enrollment?.profile?.biometric?.fingerprint?.rt == null
                && StaticData.Enrollment?.profile?.biometric?.fingerprint?.ri == null
                && StaticData.Enrollment?.profile?.biometric?.fingerprint?.rm == null
                && StaticData.Enrollment?.profile?.biometric?.fingerprint?.rr == null
                && StaticData.Enrollment?.profile?.biometric?.fingerprint?.rs == null)
            {
                bioMissing += 1;
            }

            if (StaticData.Enrollment?.profile?.biometric?.photo?.photo == null) bioMissing += 1;

            if (StaticData.Enrollment?.profile?.biometric?.iris?.left == null
                    && StaticData.Enrollment?.profile?.biometric?.iris?.right == null)
            {
                bioMissing += 1;
            }

            if (bioMissing >= 2)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Provide at least two types of biometric data");
                return;
            }

            if (StaticData.Enrollment?.profile?.attachment?.firList?.Count > 0)
            {
                if (string.IsNullOrEmpty(StaticData.Enrollment?.profile?.attachment?.firList[0].firNo))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please provide Case Number for FIR(s)");
                    return;
                }
            }

            try
            {
                if (string.IsNullOrEmpty(StaticData.Enrollment?.profile?.fullName) || string.IsNullOrEmpty(StaticData.Enrollment?.profile?.referenceNo) ||
                string.IsNullOrEmpty(StaticData.Enrollment?.profile?.unit.ToString()) || StaticData.Enrollment?.profile?.unit == -1 ||
                string.IsNullOrEmpty(StaticData.Enrollment?.profile?.crimeInformation?.crimeType.ToString()) ||
                StaticData.Enrollment?.profile?.crimeInformation?.crimeType <= 0 ||
                string.IsNullOrEmpty(StaticData.Enrollment?.profile?.gender) ||
                (StaticData.Enrollment?.profile?.unit > 0 && (StaticData.Enrollment?.profile?.subUnit == null ||
                StaticData.Enrollment?.profile?.subUnit < 1)) ||
                (StaticData.Enrollment?.profile?.crimeInformation?.crimeZone?.district == null ||
                StaticData.Enrollment?.profile?.crimeInformation?.crimeZone?.district == 0 ||
                StaticData.Enrollment?.profile?.crimeInformation?.crimeZone?.upozilaOrThana == null ||
                StaticData.Enrollment?.profile?.crimeInformation?.crimeZone?.upozilaOrThana == 0))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please make sure that Full Name, Gender," +
                        " Crime Type, Battalion, Sub Station, Crime District and Crime Thana are not empty");
                    return;
                }

                ThreadHandler.GetInstance(new UploadThreadsAsync()).StopThread();

                ProcessingDialog.Run(delegate ()
                {
                    ret = dbEnrollClientManager.AddCriminalProfile(StaticData.Enrollment, Globals.RecordState.NEW);                    
                });

                if (ret)
                {
                    string msg = null;
                    
                    ProcessingDialog.Run(delegate ()
                    {
                        UploadToServer(ref msg);
                    });

                    if (uploadResult == 1)
                    {
                        InfoMessageBox.ShowMessage("SNSOP TOOLS", "Criminal Profile saved and uploaded successfully.");
                    }
                    else
                    {
                        if (uploadResult == 3)
                        {
                            CustomMessageBox.ShowMessage("SNSOP TOOLS", "Criminal Profile saved successfully but not uploaded as nothing has been updated");
                        }
                        else if (uploadResult == 4)
                        {
                            CustomMessageBox.ShowMessage("SNSOP TOOLS", "Criminal Profile saved successfully but upload pending as offline");
                        }
                        else if (uploadResult == 5)
                        {
                            CustomMessageBox.ShowMessage("SNSOP TOOLS", "Criminal Profile saved successfully but upload pending as connection timed out");
                        }
                        else if (uploadResult == 6)
                        {
                            CustomMessageBox.ShowMessage("SNSOP TOOLS", "Criminal Profile saved successfully but upload pending as endpoint not found");
                        }
                        else if (uploadResult == 7)
                        {
                            CustomMessageBox.ShowMessage("SNSOP TOOLS", "Criminal Profile saved successfully but not uploaded as critical exception occurred");
                        }
                    }

                    StaticData.Enrollment.profile = new MODELS.DTO.New.Enrollment.ProfileDto();
                    ((MainController)parent).SearchCriteriaBeforeEnrollment = Globals.SearchCriteriaBeforeEnrollment.CDMS_Search;
                    StaticData.MoveFromBiometricEntryPageWithoutCDMSsearch = false;
                    parent.AddChild(Globals.ChildControllers.BIOMETRIC);
                }
                else
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Failed to save Criminal Profile. Please contact with your Administrator.");
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an unexpected error when saving Criminal Profile. Please contact with your Administrator.");
            }

            finally
            {
                ThreadHandler.GetInstance(new UploadThreadsAsync()).StartThread();
            }

            //EnrollmentApiManager enrollmentApiManager = new EnrollmentApiManager();
            //enrollmentApiManager.ProfileSubmit(StaticData.Enrollment);
        }

        private int uploadResult = 0;

        public void UploadToServer(ref string msg)
        {
            bool uploadFailed = false;
            OnlineSubject onlineStatus = (OnlineSubject)SubjectFactory.GetInstance().GetSubject(OnlineSubject.Name);
            CounterPendingSubject counterPendingStatus = (CounterPendingSubject)SubjectFactory.GetInstance().GetSubject(Globals.Common.COUNTER_PENDING_NAME);
            CounterErrorSubject counterErrorStatus = (CounterErrorSubject)SubjectFactory.GetInstance().GetSubject(Globals.Common.COUNTER_ERROR_NAME);
            DbEnrollClientManager enrollClient = new DbEnrollClientManager();

            List<string> hashList = enrollClient.GetEnrolledHash();
            int pendingCount = hashList.Count;

            bool isEnrolled = false;

            string hash = StaticData.Enrollment.profile.hash;

            if (!string.IsNullOrEmpty(Users.AccessToken))
            {
                try
                {
                    //Stopwatch sw = new Stopwatch();
                    //sw.Start();

                    EnrollmentApiManager enrollmentApiManager = new EnrollmentApiManager();

                    // Check if record is already uploaded (maybe from an exported DB)
                    // Send only hash value to web service

                    int code = enrollmentApiManager.CheckEnrolledHash(hash);
                    if (code == 0)
                    {
                        // Record not in server. So upload it
                        EnrollmentDto enrollmentDto = enrollClient.GetEnrolledData(hash);
                        ApiResponse normalSaveResponse = enrollmentApiManager.ProfileSubmit(enrollmentDto);
                        if (normalSaveResponse?.code == 200)
                        {
                            isEnrolled = true;
                            logger.Info("Successfully Enrolled Criminal Profile by Web API. Hash: " + hash);

                            //Returning pending time of pending uploads
                            //sw.Stop();
                            //uploadStatus.TimeLeft = Utils.GetPendingTime(1, sw.ElapsedMilliseconds, (pendingCount - 1));

                            uploadResult = 1;
                        }
                        else
                        {
                            uploadFailed = true;
                            logger.Error("Error Occured During uploading Criminal Profile by Web API. Hash: " + hash + ", reason: " + normalSaveResponse?.message);
                            string errorMsg = "There was an error during upload Criminal Profile through Web API.\n" + normalSaveResponse?.message;
                            enrollClient.UpdateErrorStatus(hash);
                            enrollClient.UpdateErrorMessage(hash, normalSaveResponse?.message);
                            CustomMessageBox.ShowMessage("SNSOP TOOLS", errorMsg);

                            onlineStatus.IsOnline = true;
                            uploadResult = 2;
                        }

                        counterPendingStatus.Count = enrollClient.GetNormalUploadPendingCount();
                        counterPendingStatus.Notify();
                        counterErrorStatus.Count = enrollClient.GetEnrolledErrorCount();
                        counterErrorStatus.Notify();
                    }
                    else
                    {
                        // Record already in server. So skip upload
                        isEnrolled = true;
                        uploadResult = 3;
                    }

                    if (isEnrolled)
                    {
                        --pendingCount;
                        enrollClient.UpdateStatusToUploaded(hash);
                        logger.Info("Successfully enrolled Criminal Profile by Web API and Updated Local DB. " + hash);

                        counterPendingStatus.Count = enrollClient.GetNormalUploadPendingCount();
                        counterPendingStatus.Notify();
                        onlineStatus.IsOnline = true; // Since upload is successful, status should be online (incase it was showing offline)
                        onlineStatus.Notify();
                    }
                }
                catch (System.Net.WebException x)
                {
                    // Connection problems. This is not serious
                    logger.Debug("Connection problems during upload Criminal Profile. This is not serious." + x.Message);
                    uploadFailed = true;
                    onlineStatus.IsOnline = false;
                    uploadResult = 4;
                }
                catch (TimeoutException x)
                {
                    logger.Error("Connection timed out!\n" + x.ToString());
                    //ErrorMessageBox.ShowError("The connection timed out during upload Criminal Profile.", x);
                    uploadFailed = true;
                    uploadResult = 5;
                    // Not sure if this is due to network issue or server is slow
                    // So not setting network status
                }
                catch (System.ServiceModel.EndpointNotFoundException x)
                {
                    logger.Debug("EndPointNotFound during upload!" + x.Message);
                    uploadFailed = true;
                    onlineStatus.IsOnline = false;
                    uploadResult = 6;
                }
                //catch (ThreadAbortException x)
                //{
                //    logger.Debug("Inside UploadEnrollment: Force shutdown initiated! ThreadAbortException: " + x.Message);
                //    return false; // Return from here since application is shutting down. No need to notify all observers.
                //} // Not needed as this is not being uploaded from thread
                catch (System.Exception x)
                {
                    // Is Online but got unexpected exception
                    logger.Error("There was an unexpected error during upload Criminal Profile or fetch Criminal Profile from DB. " + hash + "\n" + x.ToString());
                    //ErrorMessageBox.ShowError("There was an unexpected error during upload Criminal Profile.", x);
                    uploadFailed = true;
                    // This is possibly not due to any network issue, so not setting network status.
                    enrollClient.UpdateErrorStatus(hash);
                    enrollClient.UpdateErrorMessage(hash, "Critical Exception");
                    counterPendingStatus.Count = enrollClient.GetNormalUploadPendingCount();
                    counterPendingStatus.Notify();
                    counterErrorStatus.Count = enrollClient.GetEnrolledErrorCount();
                    counterErrorStatus.Notify();
                    logger.Error("Error occurred when uploading Criminal Profile and updated local db" + hash);
                    onlineStatus.IsOnline = true;
                    uploadResult = 7;
                }
            }
            else
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Criminal Profile saved successfully but upload pending as offline");
            }

            if (uploadFailed)
            {
                //enrollClient.UpdateErrorStatus(hash);
                onlineStatus.Notify(); // online status should already have been set if this was due to a connection problem
            }
        }
    }
}
