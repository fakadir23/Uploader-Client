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
using ISTL.RAB.View.New.Enrollment.Special;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.Controllers.New.Enrollment.Special
{
    public class SpecialEnrollmentController : ViewController
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private SpecialEnrollmentUserControl specialEnrollmentUserControl;
        private DbSpecialEnrollManager dbSpecialEnrollManager;
        public SpecialEnrollmentController()
        {
            specialEnrollmentUserControl = new SpecialEnrollmentUserControl();
            base.SetView((IView)specialEnrollmentUserControl);
            specialEnrollmentUserControl.SetController(this);

            dbSpecialEnrollManager = new DbSpecialEnrollManager();
        }
        public override string GetName()
        {
            return Globals.ChildControllers.SPECIAL_ENTRY;
        }

        public void AddSpecialProfile(SpecialEnrollmentDto dto)
        {
            bool val = false;

            try
            {
                ThreadHandler.GetInstance(new UploadThreadsAsync()).StopThread();

                ProcessingDialog.Run(delegate ()
                {
                    val = dbSpecialEnrollManager.AddSpecialCriminalProfile(dto, Globals.RecordState.NEW);
                });

                if (val == true)
                {
                    //InfoMessageBox.ShowMessage("SNSOP TOOLS", "Saved special enrollment successfully");
                    ProcessingDialog.Run(delegate ()
                    {
                        UploadSpecialToServer();
                    });

                    if (uploadResult == 1)
                    {
                        InfoMessageBox.ShowMessage("SNSOP TOOLS", "Special Criminal Profile saved and uploaded successfully.");
                    }
                    else
                    {
                        if (uploadResult == 3)
                        {
                            CustomMessageBox.ShowMessage("SNSOP TOOLS", "Special Criminal Profile saved successfully but not uploaded as nothing has been updated");
                        }
                        else if (uploadResult == 4)
                        {
                            CustomMessageBox.ShowMessage("SNSOP TOOLS", "Special Criminal Profile saved successfully but upload pending as offline");
                        }
                        else if (uploadResult == 5)
                        {
                            CustomMessageBox.ShowMessage("SNSOP TOOLS", "Special Criminal Profile saved successfully but upload pending as connection timed out");
                        }
                        else if (uploadResult == 6)
                        {
                            CustomMessageBox.ShowMessage("SNSOP TOOLS", "Special Criminal Profile saved successfully but upload pending as endpoint not found");
                        }
                        else if (uploadResult == 7)
                        {
                            CustomMessageBox.ShowMessage("SNSOP TOOLS", "Special Criminal Profile saved successfully but not uploaded as critical exception occurred");
                        }
                    }

                    ((MainController)parent).OnSpecialEntry();
                }
                else
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not save special enrollment successfully");
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
        }

        private int uploadResult = 0;

        private void UploadSpecialToServer()
        {
            bool uploadFailed = false;
            if (!string.IsNullOrEmpty(Users.AccessToken))
            {
                bool isEnrolled = false;
                OnlineSubject onlineStatus = (OnlineSubject)SubjectFactory.GetInstance().GetSubject(OnlineSubject.Name);
                DbSpecialEnrollManager enrollSpecialClient = new DbSpecialEnrollManager();

                string hash = StaticData.specialEnrollment?.hash;

                try
                {                    
                    SpecialEnrollApiManager enrollmentApiManager = new SpecialEnrollApiManager();

                    int code = enrollmentApiManager.CheckSpecialEnrolledHash(hash);
                    if (code == 0)
                    {
                        SpecialEnrollmentDto specialEnrollmentDto = enrollSpecialClient.GetLocalSpecialEnrolled(hash);
                        ApiResponse specialSaveResponse = enrollmentApiManager.SpecialProfileSubmit(specialEnrollmentDto);
                        if (specialSaveResponse?.code == 200)
                        {
                            isEnrolled = true;
                            logger.Info("Successfully Enrolled Special Criminal Profile by Web API. Hash: " + hash);

                            uploadResult = 1;
                        }
                        else
                        {
                            uploadFailed = true;
                            logger.Error("Unexpected Error Occured During uploading Special Criminal Profile. " + hash + ", reason: " + specialSaveResponse?.message);
                            string errorMsg = "There was an unexpected error during upload Special Criminal Profile.\n" + specialSaveResponse?.message;
                            enrollSpecialClient.UpdateErrorStatus(hash);
                            enrollSpecialClient.UpdateErrorMessage(hash, specialSaveResponse?.message);
                            CustomMessageBox.ShowMessage("SNSOP TOOLS", errorMsg);

                            onlineStatus.IsOnline = true;
                            uploadResult = 2;
                        }
                    }
                    else
                    {
                        isEnrolled = true;
                        uploadResult = 3;
                    }

                    if (isEnrolled)
                    {
                        enrollSpecialClient.UpdateStatusToUploaded(hash);
                        logger.Info("Successfully Enrolled Special Criminal Profile by Web API and Updated Local DB. " + hash);

                        onlineStatus.IsOnline = true; // Since upload is successful, status should be online (incase it was showing offline)
                        onlineStatus.Notify();
                    }
                }
                catch (System.Net.WebException x)
                {
                    logger.Debug("Connection problems during upload Special Criminal Profile . This is not serious." + x.Message);
                    uploadFailed = true;
                    onlineStatus.IsOnline = false;
                    uploadResult = 4;
                }
                catch (TimeoutException x)
                {
                    logger.Error("Connection timed out!\n" + x.ToString());
                    //ErrorMessageBox.ShowError("The connection timed out during upload Special Criminal Profile.", x);
                    uploadFailed = true;
                    uploadResult = 5;
                }
                catch (System.ServiceModel.EndpointNotFoundException x)
                {
                    logger.Debug("EndPointNotFound during upload Special Criminal Profile!" + x.Message);
                    uploadFailed = true;
                    onlineStatus.IsOnline = false;
                    uploadResult = 6;
                }
                catch (System.Exception x)
                {
                    logger.Error("There was an unexpected error during upload Special Criminal Profile. " + hash + "\n" + x.ToString());
                    //ErrorMessageBox.ShowError("There was an unexpected error during upload Special Criminal Profile.", x);
                    uploadFailed = true;
                    enrollSpecialClient.UpdateErrorStatus(hash);
                    enrollSpecialClient.UpdateErrorMessage(hash, "Critical Exception");

                    logger.Error("Error occurred when uploading Special Criminal Profile and updated local db" + hash);
                    onlineStatus.IsOnline = true;
                    uploadResult = 7;
                }

                if (uploadFailed)
                {
                    //enrollSpecialClient.UpdateErrorStatus(hash);
                    onlineStatus.Notify();
                }
            }
            else
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Special Criminal Profile saved successfully but upload pending as offline");
            }
        }

        public void GoBacktoDashboard()
        {
            ((MainController)parent).OnHome();
        }
    }
}
