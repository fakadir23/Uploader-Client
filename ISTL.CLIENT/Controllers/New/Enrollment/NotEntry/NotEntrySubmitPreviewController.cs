using ISTL.COMMON;
using ISTL.COMMON.Subscription;
using ISTL.COMMON.Threads;
using ISTL.MODELS.Common;
using ISTL.MODELS.DTO.New.NotEntry;
using ISTL.PERSOGlobals;
using ISTL.RAB.ApiManager;
using ISTL.RAB.Asynch;
using ISTL.RAB.DbManager;
using ISTL.RAB.Entity;
using ISTL.RAB.View;
using ISTL.RAB.View.New.Enrollment.NotEntry;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.Controllers.New.Enrollment.NotEntry
{
    public class NotEntrySubmitPreviewController : ViewController
    {
        private NotEntrySubmitPreviewUserControl notEntrySubmitPreview;
        private Logger logger = LogManager.GetCurrentClassLogger();
        private DbNotEntryManager dbNotEntryManager;
        private DbLookupManager dblookupManager;

        public NotEntrySubmitPreviewController()
        {
            notEntrySubmitPreview = new NotEntrySubmitPreviewUserControl();
            base.SetView((IView)notEntrySubmitPreview);
            notEntrySubmitPreview.SetController(this);

            dbNotEntryManager = new DbNotEntryManager();
            dblookupManager = new DbLookupManager();
        }

        public override string GetName()
        {
            return Globals.ChildControllers.NOT_ENTRY_SUBMIT_PREVIEW;
        }

        public void OnNoEntryBiometric()
        {
            ((MainController)parent).OnNotEntryBiometric();
        }

        public void SubmitPreview()
        {
            ((MainController)parent).OnNotEntrySubmitPreview();
        }

        public void OnBackToEntry()
        {
            ((MainController)parent).OnBackToNotEntryProfile();
        }

        public string GetValueForLookups(string columnName, string tableName, int id)
        {
            string value = dblookupManager.GetValueForLookup(columnName, tableName, id);
            return value;
        }

        public void OnSubmit()
        {
            bool value = false;

            try
            {
                ThreadHandler.GetInstance(new UploadThreadsAsync()).StopThread();

                ProcessingDialog.Run(delegate ()
                {
                    value = dbNotEntryManager.AddNotEntry(StaticData.NotEntry);
                });

                if (value == true)
                {
                    ProcessingDialog.Run(delegate ()
                    {
                        UploadNotEntryToServer();
                    });

                    if (uploadResult == 1)
                    {
                        InfoMessageBox.ShowMessage("SNSOP TOOLS", "Not Entry Profile saved and uploaded successfully.");
                    }
                    else
                    {
                        if (uploadResult == 3)
                        {
                            CustomMessageBox.ShowMessage("SNSOP TOOLS", "Not Entry Profile saved successfully but not uploaded as nothing has been updated");
                        }
                        else if (uploadResult == 4)
                        {
                            CustomMessageBox.ShowMessage("SNSOP TOOLS", "Not Entry Profile saved successfully but upload pending as offline");
                        }
                        else if (uploadResult == 5)
                        {
                            CustomMessageBox.ShowMessage("SNSOP TOOLS", "Not Entry Profile saved successfully but upload pending as connection timed out");
                        }
                        else if (uploadResult == 6)
                        {
                            CustomMessageBox.ShowMessage("SNSOP TOOLS", "Not Entry Profile saved successfully but upload pending as endpoint not found");
                        }
                        else if (uploadResult == 7)
                        {
                            CustomMessageBox.ShowMessage("SNSOP TOOLS", "Not Entry Profile saved successfully but not uploaded as critical exception occurred");
                        }
                    }

                    ((MainController)parent).OnAddNotEntryProfile();
                }
                else
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not save not entry");
                }
            }
            catch (Exception e)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an unexpected error when saving Not Entry Profile. Please contact with your Administrator.");
            }

            finally
            {
                ThreadHandler.GetInstance(new UploadThreadsAsync()).StartThread();
            }
        }

        private int uploadResult = 0;

        private void UploadNotEntryToServer()
        {
            bool uploadFailed = false;
            if (!string.IsNullOrEmpty(Users.AccessToken))
            {
                bool isEnrolled = false;
                OnlineSubject onlineStatus = (OnlineSubject)SubjectFactory.GetInstance().GetSubject(OnlineSubject.Name);

                string refNo = StaticData.NotEntry?.referenceNo;

                try
                {
                    NotEntryApiManager enrollNotEntryClient = new NotEntryApiManager();

                    NotEntryDto NotEntryEnrollmentDto = dbNotEntryManager.GetLocalNotEntry(StaticData.NotEntry.referenceNo);
                    ApiResponse NotEntrySaveResponse = enrollNotEntryClient.NotEntryProfileSubmit(NotEntryEnrollmentDto);
                    if (NotEntrySaveResponse?.code == 200)
                    {
                        isEnrolled = true;
                        logger.Info("Successfully Enrolled Not Entry Profile by Web API. Reference No: " + StaticData.NotEntry.referenceNo);

                        uploadResult = 1;
                    }
                    else
                    {
                        uploadFailed = true;
                        logger.Error("Unexpected Error Occured During uploading Not Entry Profile. " + StaticData.NotEntry.referenceNo + ", reason: " + NotEntrySaveResponse?.message);
                        string errorMsg = "There was an unexpected error during upload Not Entry Profile.\n" + NotEntrySaveResponse?.message;
                        dbNotEntryManager.UpdateErrorStatus(StaticData.NotEntry.referenceNo);
                        dbNotEntryManager.UpdateErrorMessage(StaticData.NotEntry.referenceNo, NotEntrySaveResponse?.message);
                        CustomMessageBox.ShowMessage("SNSOP TOOLS", errorMsg);

                        onlineStatus.IsOnline = true;
                        uploadResult = 2;
                    }

                    if (isEnrolled)
                    {
                        dbNotEntryManager.UpdateStatusToUploaded(StaticData.NotEntry.referenceNo);
                        logger.Info("Successfully Enrolled Not Entry Profile by Web API and Updated Local DB. " + StaticData.NotEntry.referenceNo);

                        onlineStatus.IsOnline = true;
                        onlineStatus.Notify();
                    }
                }
                catch (System.Net.WebException x)
                {
                    logger.Debug("Connection problems during upload Not Entry Profile . This is not serious." + x.Message);
                    uploadFailed = true;
                    onlineStatus.IsOnline = false;
                    uploadResult = 4;
                }
                catch (TimeoutException x)
                {
                    logger.Error("Connection timed out!\n" + x.ToString());
                    uploadFailed = true;
                    uploadResult = 5;
                }
                catch (System.ServiceModel.EndpointNotFoundException x)
                {
                    logger.Debug("EndPointNotFound during upload Not Entry Profile!" + x.Message);
                    uploadFailed = true;
                    onlineStatus.IsOnline = false;
                    uploadResult = 6;
                }
                catch (System.Exception x)
                {
                    logger.Error("There was an unexpected error during upload Not Entry Profile. " + refNo + "\n" + x.ToString());
                    uploadFailed = true;
                    dbNotEntryManager.UpdateErrorStatus(refNo);
                    dbNotEntryManager.UpdateErrorMessage(refNo, "Critical Exception");

                    logger.Error("Error occurred when uploading Not Entry Profile and updated local db" + refNo);
                    onlineStatus.IsOnline = true;
                    uploadResult = 7;
                }

                if (uploadFailed)
                {
                    onlineStatus.Notify();
                }
            }
            else
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Not Entry Profile saved successfully but upload pending as offline");
            }
        }
    }
}
