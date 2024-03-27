using System;
using System.Windows.Forms;
using NLog;
using ISTL.COMMON;
using ISTL.COMMON.CommandManager;
using ISTL.RAB.View;
using ISTL.RAB.Entity;
using ISTL.PERSOGlobals;
using ISTL.MODELS.Request.Adjudication;
using ISTL.MODELS.DTO.Enrollment;
using ISTL.RAB.Controllers.New.Home.Setup;
using ISTL.RAB.Controllers.New.Home;
using ISTL.RAB.Controllers.New.Enrollment;
using ISTL.RAB.Controllers.New.Home.Report;
using ISTL.RAB.ControllersNew.Home.Report;
using ISTL.COMMON.Subscription;
using ISTL.RAB.DbManager;
using ISTL.COMMON.Threads;
using ISTL.RAB.Asynch;
using ISTL.RAB.Controllers.New.Enrollment.Special;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.RAB.View.New.Enrollment.BiometricInformation;
using ISTL.RAB.Controllers.New.ProfileManagement;
using ISTL.RAB.Controllers.New.Enrollment.NotEntry;

namespace ISTL.RAB.Controllers
{
    public class MainController : ViewController
    {
        #region Declaration(s)
        private Logger logger = LogManager.GetCurrentClassLogger();
        private MainForm mainForm;
        public CommandManager cmdMgr;
        public long PersonId { get; set; }
        public PersonDataDto PersonData { get; set; }
        private CounterSubject counterStatus;
        private CounterPendingSubject counterPendingStatus;
        private CounterErrorSubject counterErrorStatus;
        private CounterDraftSubject counterDraftStatus;
        private DbEnrollClientManager enrollClient;
        private DbSpecialEnrollManager specialEnrollManager;
        private DbBecManager dbBecManager;

        public int SearchCriteriaBeforeEnrollment;
        #endregion

        #region Constructor(s)
        public MainController()
        {
            mainForm = new MainForm();
            base.SetView((IView)mainForm);
            mainForm.SetController(this);

            enrollClient = new DbEnrollClientManager();
            specialEnrollManager = new DbSpecialEnrollManager();
            dbBecManager = new DbBecManager();
            counterStatus = (CounterSubject)SubjectFactory.GetInstance().GetSubject(Globals.Common.COUNTER_NAME);
            counterPendingStatus = (CounterPendingSubject)SubjectFactory.GetInstance().GetSubject(Globals.Common.COUNTER_PENDING_NAME);
            counterErrorStatus = (CounterErrorSubject)SubjectFactory.GetInstance().GetSubject(Globals.Common.COUNTER_ERROR_NAME);
            counterDraftStatus = (CounterDraftSubject)SubjectFactory.GetInstance().GetSubject(Globals.Common.COUNTER_DRAFT_NAME);
        }

        ~MainController() { }
        #endregion

        #region Method(s)
        public override void OnLoad()
        {
            InitializeController();
            mainForm.InitializeCommandManager();
            AddChild(Globals.ChildControllers.DEFAULT);

            ShowEnrollTodayCount();

            // Start heartbeat sender thread
            // if (!string.IsNullOrWhiteSpace(Users.WorkStationCode) && Users.WorkStationCode != "0")
            // {
            //     ThreadHandler.GetInstance(new SendHeartbeatAsynch()).StartThread();  //In login form
            // }

            // Start update check
            // ThreadHandler.GetInstance(new UpdateCheckAsynch()).StartThread();

            // Get nid search pending count
            // var pendingList = dbBecManager.GetRequestList("PENDING", 0, 0);

            //if (pendingList != null && pendingList.Count > 0)
            //{
            // Start bec server nid search result check
            //    ThreadHandler.GetInstance(new BecNidIdentificationAsynch()).StartThread();
            // }
        }

        private void ShowEnrollTodayCount()
        {
            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    //counterStatus.Count = enrollClient.GetTodayEnrollCount();
                    counterStatus.Count = enrollClient.GetBeneficiaryEnrolledCount();
                    counterStatus.Notify();

                    //counterPendingStatus.Count = enrollClient.GetNormalUploadPendingCount();
                    counterPendingStatus.Count = enrollClient.GetBeneficiaryUploadPendingCount();
                    counterPendingStatus.Notify();

                    //counterErrorStatus.Count = enrollClient.GetEnrolledErrorCount();
                    counterErrorStatus.Count = enrollClient.GetBeneficiaryUploadErrorCount();
                    counterErrorStatus.Notify();

                    //counterDraftStatus.Count = enrollClient.GetEnrolledDraftCount();
                    counterDraftStatus.Count = enrollClient.GetUploadedCount();
                    counterDraftStatus.Notify();
                }
                catch (Exception x)
                {
                    logger.Error("There was an error when getting total enroll count.\n" + x.ToString());
                    ErrorMessageBox.ShowError("There was an error when getting total enroll count.", x);
                }
            });
        }

        private void InitializeController()
        {
            cmdMgr = new CommandManager();
            //mainForm.UserNameHeader = Users.FullName;            
        }

        public override void AddChild(string name)
        {
            switch (name)
            {
                case Globals.ChildControllers.DEFAULT:
                    base.RemoveAllChild();
                    DeselectAllButton();
                    base.AddChild(new DefaultController());
                    break;
                case Globals.ChildControllers.ENROLL:
                    if (!Users.UserPermission.NewEntry)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    CriminalProfileController enrollController = new CriminalProfileController();
                    base.AddChild(enrollController);
                    break;
                case Globals.ChildControllers.OTHER_INFO:
                    if (!Users.UserPermission.NewEntry)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    base.AddChild(new OtherInformationController());
                    break;
                case Globals.ChildControllers.BIOMETRIC:
                    if (!Users.UserPermission.NewEntry)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    base.AddChild(new BiometricController());
                    break;
                case Globals.ChildControllers.PREVIEW_SUBMIT:
                    if (!Users.UserPermission.NewEntry)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    base.AddChild(new PreviewSubmitController());
                    break;
                case Globals.ChildControllers.ENROLL_MATCH:
                    if (!Users.UserPermission.BiometricSearch)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    MatchController enrollMatchController = new MatchController();
                    base.AddChild(enrollMatchController);
                    enrollMatchController.OnFocus();
                    break;
                case Globals.ChildControllers.MANAGE:
                    base.RemoveAllChild();
                    DeselectAllButton();
                    ManageController manageController = new ManageController();
                    base.AddChild(manageController);
                    break;
                case Globals.ChildControllers.MATCH:
                    if (!Users.UserPermission.BiometricSearch)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    PersonMatchResultController personMatchResultController = new PersonMatchResultController();
                    base.AddChild(personMatchResultController);
                    break;
                case Globals.ChildControllers.DIAGNOSE:
                    base.RemoveAllChild();
                    DeselectAllButton();
                    base.AddChild(new DiagnoseController());
                    break;
                case Globals.ChildControllers.SEARCH_CRIMINAL:
                    if (!Users.UserPermission.SearchProfile)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    base.AddChild(new SearchCriminalController());
                    break;
                case Globals.ChildControllers.BIOMETRIC_SEARCH:
                    if (!Users.UserPermission.BiometricSearch)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    base.AddChild(new BiometricSearchController());
                    break;
                case Globals.ChildControllers.BATTALION_SETTINGS:
                    if (!Users.UserPermission.Settings)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    base.AddChild(new BattalionSettingsController());
                    break;
                case Globals.ChildControllers.SERVICE_SETTINGS:
                    if (!Users.UserPermission.Settings)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    base.AddChild(new ServiceSettingsController());
                    break;
                case Globals.ChildControllers.USER_MANAGEMENT:
                    if (!Users.UserPermission.UserManagement)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    base.AddChild(new UserManagementController());
                    break;
                case Globals.ChildControllers.REPORT:
                    if (!Users.UserPermission.Report)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    base.AddChild(new ReportController());
                    break;
                case Globals.ChildControllers.DAILY_ENROLLMENT_REPORT:
                    if (!Users.UserPermission.Report)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    base.AddChild(new DailyEnrollmentReportController());
                    break;
                case Globals.ChildControllers.SUMMARY_REPORT:
                    if (!Users.UserPermission.Report)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    base.AddChild(new SummaryReportController());
                    break;
                case Globals.ChildControllers.DRAFT_LIST:
                    if (!Users.UserPermission.DraftRecords)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    base.AddChild(new DraftDataController());
                    break;
                case Globals.ChildControllers.UPLOAD_PENDING:
                    if (!Users.UserPermission.DraftRecords)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    base.AddChild(new UploadPendingListController());
                    break;
                case Globals.ChildControllers.FAILED_UPLOAD:
                    if (!Users.UserPermission.FailedUploads)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    base.AddChild(new FailedUploadController());
                    break;
                case Globals.ChildControllers.ENROLLED_TODAY:
                    if (!Users.UserPermission.NewEntry)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    base.AddChild(new EnrolledTodayController());
                    break;
                case Globals.ChildControllers.MATCH_RESULTS:
                    if (!Users.UserPermission.BiometricSearch)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    base.AddChild(new MatchResultController());
                    break;
                case Globals.ChildControllers.SPECIAL_ENTRY:
                    if (!Users.UserPermission.SpecialEntry)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    base.AddChild(new SpecialEnrollmentController());
                    break;
                case Globals.ChildControllers.SPECIAL_COUNT:
                    if (!Users.UserPermission.SpecialEntry)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    base.AddChild(new SpecialEnrollCountController());
                    break;
                case Globals.ChildControllers.SPECIAL_SEARCH_PROFILE:
                    if (!Users.UserPermission.SpecialEntry)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    base.AddChild(new SpecialSearchProfileController());
                    break;
                case Globals.ChildControllers.SPECIAL_DRAFT:
                    if (!Users.UserPermission.SpecialEntry)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    base.AddChild(new SpecialDraftProfileController());
                    break;
                case Globals.ChildControllers.UPLOAD_PENDING_SPECIAL:
                    if (!Users.UserPermission.SpecialEntry)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    base.AddChild(new SpecialUploadPendingController());
                    break;
                case Globals.ChildControllers.FAILED_UPLOAD_SPECIAL:
                    if (!Users.UserPermission.SpecialEntry)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    base.AddChild(new SpecialFailedUploadController());
                    break;
                case Globals.ChildControllers.NOT_ENTRY:
                    if (!Users.UserPermission.NotEntry)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
					base.AddChild(new NotEntryController());
					break;
                case Globals.ChildControllers.NOT_ENTRY_BIOMETRIC:
                    if (!Users.UserPermission.NotEntry)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    base.AddChild(new NotEntryBiometricController());
                    break;
                case Globals.ChildControllers.NOT_ENTRY_SUBMIT_PREVIEW:
                    if (!Users.UserPermission.NotEntry)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    base.AddChild(new NotEntrySubmitPreviewController());
                    break;
                case Globals.ChildControllers.SEARCH_NOT_ENTRY_PROFILE:
                    if (!Users.UserPermission.NotEntry)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    base.AddChild(new SearchNotEntryProfileController());
                    break;
                case Globals.ChildControllers.NOT_ENTRY_UPLOAD_PENDING:
                    if (!Users.UserPermission.NotEntry)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    base.AddChild(new NotEntryUploadPendingController());
                    break;
                case Globals.ChildControllers.NOT_ENTRY_FAILED_UPLOAD:
                    if (!Users.UserPermission.NotEntry)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    base.AddChild(new NotEntryFailedUploadController());
                    break;
                case Globals.ChildControllers.PROFILE_MANAGEMENT:
                    if (!Users.UserPermission.ProfileManagement)
                    {
                        CustomMessageBox.ShowMessage(Globals.CustomMessage.ErrorTitle, Globals.CustomMessage.ErrorMessage);
                        break;
                    }
                    base.RemoveAllChild();
                    DeselectAllButton();
                    base.AddChild(new ProfileManagementController());
                    break;
            }
        }

        public void OnExportDb()
        {
            ExportDbController exportDbController = new ExportDbController();
            exportDbController.ShowView();
        }

        public void OnImportDb()
        {
            ImportDbController importDbController = new ImportDbController();
            importDbController.ShowView();
        }

        public void OnExit()
        {
            //if (MessageBox.Show("Are you sure you want to exit?", "RAB CDMS", MessageBoxButtons.YesNo)
            //        == DialogResult.Yes)
            DialogResult dr = YesNoMessageBox.YesNoMessageBoxResult("RAB CDMS", "Are you sure you want to exit?");
            if (dr == DialogResult.Yes)
            {
                if (!IsCancelClosing())
                {
                    mainForm.DialogResult = DialogResult.Cancel;
                }
            }
        }

        public void OnLogout()
        {
            //if (MessageBox.Show("Are you sure you want to logout?", "RAB CDMS", MessageBoxButtons.YesNo)
            //    == DialogResult.Yes)
            DialogResult dr = YesNoMessageBox.YesNoMessageBoxResult("RAB CDMS", "Are you sure you want to logout?");
            if (dr == DialogResult.Yes)
            {
                mainForm.DialogResult = DialogResult.OK;
                StaticData.StaticEnrolledWithBiometicCount = -1;
                StaticData.StaticFIRUploadPendingCount = -1;
            }
        }

        public void OnEnroll()
        {
            AddChild(Globals.ChildControllers.ENROLL);
        }

        public void OnFamily()
        {
            AddChild(Globals.ChildControllers.OTHER_INFO);
        }

        public void OnBiometric()
        {
            //StaticData.MoveFromBiometricEntryPageWithoutCDMSsearch = false;
            //StaticData.Enrollment.profile = new ProfileDto();
            //StaticData.ModifiableNormalEnrollment = true;

            var form = new ChooseDBToMatchForm();
            DialogResult dr = form.ShowDialog();
            if (dr == DialogResult.OK)
            {
                StaticData.MoveFromBiometricEntryPageWithoutCDMSsearch = false;
                StaticData.Enrollment.profile = new ProfileDto();
                StaticData.ModifiableNormalEnrollment = true;

                SearchCriteriaBeforeEnrollment = form.SelectedSearchCriteria;
                AddChild(Globals.ChildControllers.BIOMETRIC);
            }

            //AddChild(Globals.ChildControllers.BIOMETRIC);
        }
        public void OnEnrollMatch()
        {
            AddChild(Globals.ChildControllers.ENROLL_MATCH);
        }
        public void OnHome()
        {
            AddChild(Globals.ChildControllers.DEFAULT);
        }
        public void OnManage()
        {
            AddChild(Globals.ChildControllers.MANAGE);
        }
        public void OnMatch()
        {
            AddChild(Globals.ChildControllers.MATCH);
        }
        public void OnDiagnose()
        {
            AddChild(Globals.ChildControllers.DIAGNOSE);
        }
        public void OnSearchCriminal()
        {
            AddChild(Globals.ChildControllers.SEARCH_CRIMINAL);
        }
        public void OnBiometricSearch()
        {
            var form = new ChooseDBToMatchForm();
            DialogResult dr = form.ShowDialog();
            if (dr == DialogResult.OK)
            {
                SearchCriteriaBeforeEnrollment = form.SelectedSearchCriteria;
                AddChild(Globals.ChildControllers.BIOMETRIC_SEARCH);
            }
        }
        public void OnUserManagement()
        {
            AddChild(Globals.ChildControllers.USER_MANAGEMENT);
        }
        public void OnReport()
        {
            try
            {
                AddChild(Globals.ChildControllers.REPORT);
            }
            catch(Exception ex)
            {
                logger.Error("There was an error when init report form. Error Message: " + ex.ToString());
            }
        }
        public void OnBattalionSettings()
        {
            AddChild(Globals.ChildControllers.BATTALION_SETTINGS);
        }
        public void OnServiceSettings()
        {
            AddChild(Globals.ChildControllers.SERVICE_SETTINGS);
        }
        public void OnDraftRecords()
        {
            AddChild(Globals.ChildControllers.DRAFT_LIST);
        }                
        public void OnUploadPending()
        {
            AddChild(Globals.ChildControllers.UPLOAD_PENDING);
        }
        public void OnFailedUploads()
        {
            AddChild(Globals.ChildControllers.FAILED_UPLOAD);
        }
        public void OnEnrolledToday()
        {
            AddChild(Globals.ChildControllers.ENROLLED_TODAY);
        }
        public void OnSpecialEntry()
        {
            StaticData.ModifiableSpecialEnrollment = true;
            StaticData.specialEnrollment = new MODELS.DTO.New.Enrollment.SpecialEnrollmentDto();
            AddChild(Globals.ChildControllers.SPECIAL_ENTRY);
        }
        public void OnSpecialCount()
        {
            AddChild(Globals.ChildControllers.SPECIAL_COUNT);
        }
        public void OnSpecialProfile()
        {
            AddChild(Globals.ChildControllers.SPECIAL_SEARCH_PROFILE);
        }
        public void OnSpecialDraft()
        {
            AddChild(Globals.ChildControllers.SPECIAL_DRAFT);
        }
        public void OnSpecialUploadPending()
        {
            AddChild(Globals.ChildControllers.UPLOAD_PENDING_SPECIAL);
        }
        public void OnFailedUploadSpecial()
        {
            AddChild(Globals.ChildControllers.FAILED_UPLOAD_SPECIAL);
        }
        public void OnAddNotEntryProfile()
        {
            StaticData.ModifiableNotEntry = true;
            StaticData.NotEntry = new MODELS.DTO.New.NotEntry.NotEntryDto();
            AddChild(Globals.ChildControllers.NOT_ENTRY);
        }
        public void OnBackToNotEntryProfile()
        {
            AddChild(Globals.ChildControllers.NOT_ENTRY);
        }
        public void OnNotEntryBiometric()
        {
            AddChild(Globals.ChildControllers.NOT_ENTRY_BIOMETRIC);
        }
        public void OnNotEntrySubmitPreview()
        {
            AddChild(Globals.ChildControllers.NOT_ENTRY_SUBMIT_PREVIEW);
        }
        public void OnSearchNotEntryProfile()
        {
            AddChild(Globals.ChildControllers.SEARCH_NOT_ENTRY_PROFILE);
        }
        public void OnNotEntryUploadPending()
        {
            AddChild(Globals.ChildControllers.NOT_ENTRY_UPLOAD_PENDING);
        }
        public void OnNotEntryFailedUpload()
        {
            AddChild(Globals.ChildControllers.NOT_ENTRY_FAILED_UPLOAD);
        }
        public override void OnClosing()
        {
            base.OnClosing();

            // Stop update check
            ThreadHandler.GetInstance(new UpdateCheckAsynch()).StopThread();
        }

        private void DeselectAllButton()
        {
            //mainForm.HomeSelected(false);
            //mainForm.EnrollSelected(false);
            //mainForm.ManageSelected(false);
            //mainForm.EnrollMatchSelected(false);
        }
        #endregion
    }
}
