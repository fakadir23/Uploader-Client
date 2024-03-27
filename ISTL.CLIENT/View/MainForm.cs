using System;
using System.Deployment.Application;
using System.Windows.Forms;
using ISTL.COMMON;
using ISTL.COMMON.CommandManager;
using ISTL.COMMON.Subscription;
using ISTL.RAB.Controllers;
using ISTL.RAB.Entity;
using System.Threading;
using NLog;
using ISTL.PERSOGlobals;
using ISTL.COMMON.Common;
using System.Drawing;
using ISTL.RAB.Controllers.New.Home;
using ISTL.RAB.View.New.Enrollment.BiometricInformation;
using ISTL.RAB.View.Beneficiary;

namespace ISTL.RAB.View
{
    public partial class MainForm : ViewForm, IOnlineObservable, 
        IUploadObservable, ICounterObservable, IUpdateObservable, 
        IDeviceObservable, ICounterErrorObservable, ICounterDraftObservable, ICounterPendingObservable
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private OnlineSubject onlineStatus;
        private OnlineObserver onlineObserver;
        private UploadSubject uploadStatus;
        private UploadObserver uploadObserver;
        private CounterSubject counterStatus;
        private CounterObserver counterObserver;
        private CounterPendingSubject counterPendingStatus;
        private CounterPendingObserver counterPendingObserver;
        private CounterErrorSubject counterErrorStatus;
        private CounterErrorObserver counterErrorObserver;
        private CounterDraftSubject counterDraftStatus;
        private CounterDraftObserver counterDraftObserver;
        private UpdateSubject updateStatus;
        private UpdateObserver updateObserver;
        private DeviceObserver fingerprintDeviceObserver;
        private DeviceSubject fingerprintDeviceStatus;
        private DeviceObserver irisDeviceObserver;
        private DeviceSubject irisDeviceStatus;
        private DeviceObserver webcamDeviceObserver;
        private DeviceSubject webcamDeviceStatus;
        private NidSearchObserver nidSearchObserver;
        private NidSearchSubject nidSearchStatus;

        private bool deviceFpScanner;
        private bool deviceWebcam;
        private bool deviceIrisScanner;

        public MainForm()
        {
            Font = new Font(Font.Name, 8.25f * 96f / CreateGraphics().DpiX, Font.Style, Font.Unit, Font.GdiCharSet, Font.GdiVerticalFont);
            InitializeComponent();

            base.RegisterPanel(Globals.ChildControllers.DEFAULT, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.ENROLL, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.MANAGE, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.MATCH, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.ENROLL_MATCH, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.OTHER_INFO, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.BIOMETRIC, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.DIAGNOSE, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.SEARCH_CRIMINAL, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.BIOMETRIC_SEARCH, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.REPORT, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.DAILY_ENROLLMENT_REPORT, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.SUMMARY_REPORT, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.BATTALION_SETTINGS, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.SERVICE_SETTINGS, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.USER_MANAGEMENT, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.PREVIEW_SUBMIT, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.DRAFT_LIST, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.FAILED_UPLOAD, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.UPLOAD_PENDING, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.ENROLLED_TODAY, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.MATCH_RESULTS, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.SPECIAL_ENTRY, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.SPECIAL_COUNT, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.SPECIAL_SEARCH_PROFILE, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.SPECIAL_DRAFT, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.FAILED_UPLOAD_SPECIAL, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.UPLOAD_PENDING_SPECIAL, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.NOT_ENTRY, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.NOT_ENTRY_BIOMETRIC, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.NOT_ENTRY_SUBMIT_PREVIEW, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.SEARCH_NOT_ENTRY_PROFILE, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.NOT_ENTRY_UPLOAD_PENDING, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.NOT_ENTRY_FAILED_UPLOAD, this.panelMain);
            base.RegisterPanel(Globals.ChildControllers.PROFILE_MANAGEMENT, this.panelMain);

            this.Mainmenu.Cursor = Cursors.Hand;
        }

        // Ref: http://msdn.microsoft.com/en-us/library/windows/desktop/aa363480(v=vs.85).aspx
        private const int WM_DEVICECHANGE = 0x0219; // device change event       

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_DEVICECHANGE)
            {
                //logger.Debug(">>>>>>DEVICE: " + m.WParam);
                CheckDevices();
            }
        }

        private void CheckDevices()
        {
            new Thread(delegate ()
            {
                fingerprintDeviceStatus.IsConnected = Utils.IsDeviceConnected(Configuration.FP?.Name,
                    Configuration.FP?.Type, Configuration.FP?.SupportedModel, out fingerprintDeviceStatus.actualName);
                webcamDeviceStatus.IsConnected = Utils.IsDeviceConnected(Configuration.Cam?.Name,
                    Configuration.Cam?.Type, Configuration.Cam?.SupportedModel, out webcamDeviceStatus.actualName);
                irisDeviceStatus.IsConnected = Utils.IsDeviceConnected(Configuration.Iris?.Name,
                    Configuration.Iris?.Type, Configuration.Iris?.SupportedModel, out irisDeviceStatus.actualName);

                if (this.deviceFpScanner != fingerprintDeviceStatus.IsConnected
                    || !fingerprintDeviceStatus.IsChecked)
                {
                    fingerprintDeviceStatus.IsChecked = true;
                    fingerprintDeviceStatus.Notify();
                }
                if (this.deviceWebcam != webcamDeviceStatus.IsConnected
                    || !webcamDeviceStatus.IsChecked)
                {
                    webcamDeviceStatus.IsChecked = true;
                    webcamDeviceStatus.Notify();
                }
                if (this.deviceIrisScanner != irisDeviceStatus.IsConnected
                    || !irisDeviceStatus.IsChecked)
                {
                    irisDeviceStatus.IsChecked = true;
                    irisDeviceStatus.Notify();
                }
            }).Start();
        }

        public void InitializeCommandManager()
        {
            // Get Command Manager
            CommandManager cmdMgr = ((MainController)controller).cmdMgr;

            cmdMgr.Commands.Add(new Command(Globals.Commands.SAVE, null, null));
            cmdMgr.Commands.Add(new Command(Globals.Commands.CANCEL, null, null));
            cmdMgr.Commands.Add(new Command(Globals.Commands.TAKE, null, null));

            //cmdMgr.Commands[Globals.Commands.SAVE].CommandInstances.Add(btnSave);
            //cmdMgr.Commands[Globals.Commands.CANCEL].CommandInstances.Add(btnCancel);
            //cmdMgr.Commands[Globals.Commands.TAKE].CommandInstances.Add(btnTake);

            cmdMgr.Commands[Globals.Commands.SAVE].Checked = false;
            cmdMgr.Commands[Globals.Commands.CANCEL].Checked = false;
            cmdMgr.Commands[Globals.Commands.TAKE].Checked = false;
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            this.lblVersion.Text = String.Format("V {0}", Globals.Assembly.EXE_RAB_CDMS);
            loggerInUser.Text = Users.Username;
            //InitializeCommandManager();
            SubscribeToNotifications();

            var buildProfile = System.Configuration.ConfigurationManager.AppSettings["build.profile.active"];
            if (buildProfile == "prod")
            {
                exportDatabaseToolStripMenuItem.Visible = false;
                importDatabaseToolStripMenuItem.Visible = false;
            }
            else
            {
                exportDatabaseToolStripMenuItem.Visible = false;
                importDatabaseToolStripMenuItem.Visible = false;
            }
        }

        public override void OnClosing()
        {
            base.OnClosing();
            UnsubscribeFromNotifications();
        }

        //public string UserNameHeader
        //{
        //    set
        //    {
        //        string name = value;
        //        if (name == null) return;
        //        if (name.Length > 12)
        //        {
        //            this.lblUsername.Text = name.Substring(0, 12) + "...";
        //        }
        //        else
        //        {
        //            this.lblUsername.Text = name;
        //        }
        //    }
        //}

        //public string Title
        //{
        //    set { this.lblTitle2.Text = value; }
        //}

        private void btnExit_Click(object sender, EventArgs e)
        {
            ((MainController)controller).OnExit();
        }
                        
        private void btnLogout_Click(object sender, EventArgs e)
        {
            ((MainController)controller).OnLogout();
        }

        private void MenuColorChange(ToolStripMenuItem item)
        {
            HomeMenu.BackColor = Color.Transparent;
            NewEntryMenu.BackColor = Color.Transparent;
            SearchMenu.BackColor = Color.Transparent;
            userManagementMenu.BackColor = Color.Transparent;
            localRecords.BackColor = Color.Transparent;

            HomeMenu.ForeColor = Color.MediumSeaGreen;
            NewEntryMenu.ForeColor = Color.MediumSeaGreen;
            SearchMenu.ForeColor = Color.MediumSeaGreen;
            userManagementMenu.ForeColor = Color.MediumSeaGreen;

            if (item != null)
            {
                item.BackColor = Color.MediumSeaGreen;
                item.ForeColor = Color.Black;
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            MenuColorChange(HomeMenu);
            ((MainController)controller).OnHome();
        }
        private void btnEnroll_Click(object sender, EventArgs e)
        {
            ((MainController)controller).OnEnroll();
        }
        private void btnManage_Click(object sender, EventArgs e)
        {
            ((MainController)controller).OnManage();
        }
        
        public void SubscribeToNotifications()
        {
            onlineStatus = (OnlineSubject)SubjectFactory.GetInstance().GetSubject(OnlineSubject.Name);
            uploadStatus = (UploadSubject)SubjectFactory.GetInstance().GetSubject(UploadSubject.Name);
            counterStatus = (CounterSubject)SubjectFactory.GetInstance().GetSubject(Globals.Common.COUNTER_NAME);
            counterPendingStatus = (CounterPendingSubject)SubjectFactory.GetInstance().GetSubject(Globals.Common.COUNTER_PENDING_NAME);
            counterErrorStatus = (CounterErrorSubject)SubjectFactory.GetInstance().GetSubject(Globals.Common.COUNTER_ERROR_NAME);
            counterDraftStatus = (CounterDraftSubject)SubjectFactory.GetInstance().GetSubject(Globals.Common.COUNTER_DRAFT_NAME);
            updateStatus = (UpdateSubject)SubjectFactory.GetInstance().GetSubject(UpdateSubject.Name);
            fingerprintDeviceStatus = (DeviceSubject)SubjectFactory.GetInstance().GetSubject(Globals.DeviceSubject.FP);
            webcamDeviceStatus = (DeviceSubject)SubjectFactory.GetInstance().GetSubject(Globals.DeviceSubject.CAM);
            irisDeviceStatus = (DeviceSubject)SubjectFactory.GetInstance().GetSubject(Globals.DeviceSubject.IRIS);
            nidSearchStatus = (NidSearchSubject)SubjectFactory.GetInstance().GetSubject(NidSearchSubject.Name);

            onlineObserver = new OnlineObserver(onlineStatus, this);
            onlineStatus.Attach(onlineObserver);

            uploadObserver = new UploadObserver(uploadStatus, this);
            uploadStatus.Attach(uploadObserver);

            counterObserver = new CounterObserver(counterStatus, this);
            counterStatus.Attach(counterObserver);

            counterPendingObserver = new CounterPendingObserver(counterPendingStatus, this);
            counterPendingStatus.Attach(counterPendingObserver);

            counterErrorObserver = new CounterErrorObserver(counterErrorStatus, this);
            counterErrorStatus.Attach(counterErrorObserver);

            counterDraftObserver = new CounterDraftObserver(counterDraftStatus, this);
            counterDraftStatus.Attach(counterDraftObserver);

            updateObserver = new UpdateObserver(updateStatus, this);
            updateStatus.Attach(updateObserver);

            fingerprintDeviceObserver = new DeviceObserver(fingerprintDeviceStatus, this);
            fingerprintDeviceStatus.Attach(fingerprintDeviceObserver);

            webcamDeviceObserver = new DeviceObserver(webcamDeviceStatus, this);
            webcamDeviceStatus.Attach(webcamDeviceObserver);

            irisDeviceObserver = new DeviceObserver(irisDeviceStatus, this);
            irisDeviceStatus.Attach(irisDeviceObserver);

            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.picOnlineStatus, "Network Online");
            tt.SetToolTip(this.picOfflineStatus, "Network Offline");
        }

        public void UnsubscribeFromNotifications()
        {
            onlineStatus.Detach(onlineObserver);
            uploadStatus.Detach(uploadObserver);
            counterStatus.Detach(counterObserver);
            counterPendingStatus.Detach(counterPendingObserver);
            counterErrorStatus.Detach(counterErrorObserver);
            counterDraftStatus.Detach(counterDraftObserver);
            updateStatus.Detach(updateObserver);
            fingerprintDeviceStatus.Detach(fingerprintDeviceObserver);
            webcamDeviceStatus.Detach(webcamDeviceObserver);
            irisDeviceStatus.Detach(irisDeviceObserver);
            nidSearchStatus.Detach(nidSearchObserver);
        }

        /// <summary>
        /// Sakib vai's code
        /// </summary>
        //public void RefreshToolbarErrorNotif()
        //{
        //    string networkError = "Network Offline";
        //    string uploadError = "Upload Failed";
        //    string deviceError = "Device Failed";
        //    string newLine = ""; // keep newline blank in case there is only one type of error
        //    string errorMessage = "";

        //    if (onlineStatus.IsOnline
        //        && uploadStatus.State != UploadSubject.Status.FAILED
        //        && deviceFpScanner && deviceWebcam && deviceIrisScanner)
        //    {
        //        this.picErrorStatus.Visible = false;
        //    }
        //    else
        //    {
        //        // Any type of error should show error icon
        //        this.picErrorStatus.Visible = true;
        //    }

        //    if (onlineStatus.IsOnline == false)
        //    {
        //        errorMessage = networkError;
        //        newLine = ", "; // Add a newline in case there can be other errors
        //    }

        //    if (uploadStatus.State == UploadSubject.Status.FAILED)
        //    {
        //        // If this is the only error, then previous value of label is blank
        //        // and value of newline is also blank
        //        errorMessage = errorMessage + newLine + uploadError;
        //        newLine = ", "; // Add a newline in case there can be other errors
        //    }

        //    //if (!deviceFpScanner || !deviceWebcam || !deviceIrisScanner)
        //    //{
        //    //    // If this is the only error, then previous value of label is blank
        //    //    // and value of newline is also blank
        //    //    errorMessage = errorMessage + newLine + deviceError;
        //    //}

        //    Label lblFP = new Label();
        //    Label lblCam = new Label();
        //    Label lblIris = new Label();

        //    if (!deviceFpScanner)
        //    {
        //        lblFP.Text = "FP Scanner (X) ";
        //        lblFP.BackColor = Color.Red;
        //    }
        //    else
        //    {
        //        lblFP.Text = "FP Scanner (✓)";
        //        lblFP.BackColor = Color.Green;
        //    }
        //    if (!deviceWebcam)
        //    {
        //        lblCam.Text = "Camera (X) ";
        //        lblCam.BackColor = Color.Red;
        //    }
        //    else
        //    {
        //        lblCam.Text = "Camera (✓) ";
        //        lblCam.BackColor = Color.Green;
        //    }
        //    if (!deviceIrisScanner)
        //    {
        //        lblIris.Text = "Iris Scanner (X)";
        //        lblIris.BackColor = Color.Red;
        //    }
        //    else
        //    {
        //        lblIris.Text = "Iris Scanner (✓)";
        //        lblIris.BackColor = Color.Green;
        //    }

        //    errorMessage += newLine;

        //    this.lblErrorStatus.Text = errorMessage + lblFP.Text + lblCam.Text + lblIris.Text;            
        //}

        public void RefreshToolbarErrorNotif()
        {
            string networkError = "Network Offline";
            string uploadError = "Upload Failed";
            string deviceError = "Device Failed";
            string seperator = ""; // keep blank in case there is only one type of error
            string errorMessage = "";

            if (onlineStatus.IsOnline
                && uploadStatus.State != UploadSubject.Status.FAILED
                && deviceFpScanner && deviceWebcam && deviceIrisScanner)
            {
                this.picErrorStatus.Visible = false;
            }
            else
            {
                // Any type of error should show error icon
                // this.picErrorStatus.Visible = true;
            }

            if (onlineStatus.IsOnline == false)
            {
                errorMessage = networkError;
                seperator = ", "; // Add a newline in case there can be other errors
            }

            if (uploadStatus.State == UploadSubject.Status.FAILED)
            {
                // If this is the only error, then previous value of label is blank
                // and value of newline is also blank
                errorMessage = errorMessage + seperator + uploadError;
                seperator = ", "; // Add a newline in case there can be other errors
            }

            if (!deviceFpScanner || !deviceWebcam || !deviceIrisScanner)
            {
                // If this is the only error, then previous value of label is blank
                // and value of newline is also blank
                errorMessage = errorMessage + seperator + deviceError;
            }

            if (!deviceWebcam)
            {
                this.picPhotoSuccessFailed.IconColor = Color.IndianRed;
            }
            else
            {
                this.picPhotoSuccessFailed.IconColor = Color.MediumSeaGreen;
            }

            if (!deviceFpScanner)
            {
                this.picFPSuccessFailed.ForeColor = Color.IndianRed;
                this.picFPSuccessFailed.IconColor = Color.IndianRed;
                //this.picFPSuccessFailed.IconChar = FontAwesome.Sharp.IconChar.Fingerprint;
            }
            else
            {
                this.picFPSuccessFailed.ForeColor = Color.MediumSeaGreen;
                this.picFPSuccessFailed.IconColor = Color.MediumSeaGreen;
                //this.picFPSuccessFailed.IconChar = FontAwesome.Sharp.IconChar.Fingerprint;
            }

            if (!deviceIrisScanner)
            {
                this.picIrisSuccessFailed.IconColor = Color.IndianRed;
            }
            else
            {
                this.picIrisSuccessFailed.IconColor = Color.MediumSeaGreen;
            }

            // this.lblErrorStatus.Text = errorMessage;
        }

        public void ShowOnlineStatus()
        {
            this.picOnlineStatus.Visible = true;
            this.picOfflineStatus.Visible = false;
            this.lblOnlineOffline.Text = "Online";
            this.lblOnlineOffline.ForeColor = Color.Green;
            RefreshToolbarErrorNotif();
        }

        public void ShowOfflineStatus()
        {
            this.picOnlineStatus.Visible = false;
            this.picOfflineStatus.Visible = true;
            this.lblOnlineOffline.Text = "Offline";
            this.lblOnlineOffline.ForeColor = Color.Red;
            RefreshToolbarErrorNotif();
        }

        public void ShowUploadIdleStatus(int pending)
        {
            this.picUploadStatus.Visible = false;
            this.picUploadFailedStatus.Visible = false;
            this.lblUploadStatus.Text = "";
            RefreshToolbarErrorNotif();
        }

        public void ShowUploadingStatus(int pending, string timeLeft)
        {
            string statusString = "Uploading. " + pending + " records left.";
            if (timeLeft != null && timeLeft != "")
            {
                statusString = statusString + " " + timeLeft + " left.";
            }

            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.picUploadStatus, statusString);

            this.picUploadStatus.Visible = true;
            this.picUploadFailedStatus.Visible = false;
            this.lblUploadStatus.Text = statusString;
            RefreshToolbarErrorNotif();
        }

        public void ShowUploadFailedStatus(int pending)
        {
            string statusString = "Upload failed. " + pending + " records pending upload.";
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.picUploadFailedStatus, statusString);

            this.picUploadStatus.Visible = false;
            this.picUploadFailedStatus.Visible = true;
            this.lblUploadStatus.Text = statusString;
            RefreshToolbarErrorNotif();
        }

        public void ShowCount(int cnt, string name)
        {
            this.lblEnrolledToday.Text = cnt.ToString();
        }

        public void ShowPendingCount(int cnt, string name)
        {
            this.lblPendingCount.Text = cnt.ToString();
        }

        public void ShowErrorCount(int cnt, string name)
        {
            this.lblEnrolledErrorCount.Text = cnt.ToString();
        }

        public void ShowDraftCount(int cnt, string name)
        {
            this.lblUploadedCount.Text = cnt.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Please restart the application" +
            //    " to start using the new version. You will need to be online " +
            //    "for the update to work.", "Update Available");
            InfoMessageBox.ShowMessage("SNSOP TOOLS", "Update Available\n\nPlease restart the application to start using the new version." +
                " You will need to be online for the update to work");
        }

        #region IUpdateObservable Members

        public void ShowUpdateAvailable()
        {
            this.btnUpdate.Visible = true;
        }

        public void ShowNoUpdates()
        {
            this.btnUpdate.Visible = false;
        }

        #endregion

        #region IDeviceObservable Members

        public void ShowInstalledStatus(string name, bool status, string actualName)
        {
            //logger.Debug("INSTALLED*****************" + name + " -- " + status);
        }

        //public void ShowConnectedStatus(string name, bool status, string actualName)
        //{
        //    logger.Debug("CONNECTED*****************" + name + " -- " + status);
        //    switch (name)
        //    {
        //        case Globals.Device.Lumidigm.NAME:
        //            deviceFpScanner = status;
        //            break;
        //        case Globals.Device.Logitech.NAME:
        //            deviceWebcam = status;
        //            break;
        //        case Globals.Device.Wacam.NAME:
        //            deviceIrisScanner = status;
        //            break;
        //    }
        //    RefreshToolbarErrorNotif();
        //}

        //public void ShowConnectedStatus(string name, bool status, string actualName)
        //{
        //    logger.Debug("CONNECTED*****************" + name + " -- " + status);

        //    if(name.Equals(Configuration.Cam?.Name))
        //    {
        //        deviceWebcam = status;
        //    }
        //    else if (name.Equals(Configuration.FP?.Name))
        //    {
        //        deviceFpScanner = status;
        //    }
        //    else if (name.Equals(Configuration.Iris?.Name))
        //    {
        //        deviceIrisScanner = status;
        //    }

        //    RefreshToolbarErrorNotif();
        //}

        public void ShowConnectedStatus(string name, bool status, string actualName)
        {
            logger.Debug("CONNECTED*****************" + name + " -- " + status);

            if (name.Equals(Globals.DeviceSubject.CAM))
            {
                deviceWebcam = status;
            }
            else if (name.Equals(Globals.DeviceSubject.FP))
            {
                deviceFpScanner = status;
            }
            else if (name.Equals(Globals.DeviceSubject.IRIS))
            {
                deviceIrisScanner = status;
            }

            RefreshToolbarErrorNotif();
        }

        #endregion

        private void btnMatch_Click(object sender, EventArgs e)
        {
            ((MainController)controller).OnEnrollMatch();
        }

        private void logout_Click(object sender, EventArgs e)
        {
            ((MainController)controller).OnLogout();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MenuColorChange(NewEntryMenu);
            ((MainController)controller).OnBiometric();
        }

        private void SearchMenu_Click(object sender, EventArgs e)
        {
            //MenuColorChange(SearchMenu);
            //StaticData.firPending = false;
            //StaticData.dataWithBio = false;
            //((MainController)controller).OnSearchCriminal();
        }
        private void btnBiometricSearch_Click(object sender, EventArgs e)
        {
            ((MainController)controller).OnBiometricSearch();
        }
        private void DiagnoseMenu_Click(object sender, EventArgs e)
        {
            ((MainController)controller).OnDiagnose();
        }

        private void userManagementMenu_Click(object sender, EventArgs e)
        {
            MenuColorChange(userManagementMenu);
            ((MainController)controller).OnUserManagement();
        }

        private void reportMenu_Click(object sender, EventArgs e)
        {
            ((MainController)controller).OnReport();
        }

        private void toolStripMenuItemBattalion_Click(object sender, EventArgs e)
        {
            ((MainController)controller).OnBattalionSettings();
        }

        private void toolStripMenuItemService_Click(object sender, EventArgs e)
        {
            ((MainController)controller).OnServiceSettings();
        }

        private void btnMinize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnDraftRecords_Click(object sender, EventArgs e)
        {
            MenuColorChange(localRecords);
            ((MainController)controller).OnDraftRecords();
        }

        private void btnUploadPending_Click(object sender, EventArgs e)
        {
            MenuColorChange(localRecords);
            ((MainController)controller).OnUploadPending();
        }

        private void btnFailedUploads_Click(object sender, EventArgs e)
        {
            MenuColorChange(localRecords);
            ((MainController)controller).OnFailedUploads();
        }

        private void lblPending_Click(object sender, EventArgs e)
        {
            //MenuColorChange(localRecords);
            //((MainController)controller).OnUploadPending();
            BeneficiaryListForm form = new BeneficiaryListForm();
            form.pageBeneficiaryCriteria = "pending";
            form.ShowDialog();
        }

        private void lblPendingCount_Click(object sender, EventArgs e)
        {
            //MenuColorChange(localRecords);
            //((MainController)controller).OnUploadPending();
        }

        private void lblFailed_Click(object sender, EventArgs e)
        {
            //MenuColorChange(localRecords);
            //((MainController)controller).OnFailedUploads();
            BeneficiaryListForm form = new BeneficiaryListForm();
            form.pageBeneficiaryCriteria = "failed";
            form.ShowDialog();
        }

        private void lblEnrolledErrorCount_Click(object sender, EventArgs e)
        {
            //MenuColorChange(localRecords);
            //((MainController)controller).OnFailedUploads();
        }

        private void lblDrafted_Click(object sender, EventArgs e)
        {
            //MenuColorChange(localRecords);
            //((MainController)controller).OnDraftRecords();
            BeneficiaryListForm form = new BeneficiaryListForm();
            form.pageBeneficiaryCriteria = "uploaded";
            form.ShowDialog();
        }

        private void lblEnrolledDraftCount_Click(object sender, EventArgs e)
        {
            //MenuColorChange(localRecords);
            //((MainController)controller).OnDraftRecords();
        }

        private void lblTodayName_Click(object sender, EventArgs e)
        {
            //((MainController)controller).OnEnrolledToday();
            BeneficiaryListForm form = new BeneficiaryListForm();
            form.pageBeneficiaryCriteria = "total";
            form.ShowDialog();
        }

        private void lblEnrolledToday_Click(object sender, EventArgs e)
        {
            //((MainController)controller).OnEnrolledTkoday();
        }

        private void ShowNidSearchList()
        {
            var form = new NidSearchForm();
            var dr = form.ShowDialog();
        }
        
        private void searchByFingerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new NidSearchForm();
            var dr = form.ShowDialog();
        }

        private void lblNidViewList_Click(object sender, EventArgs e)
        {
            ShowNidSearchList();
        }

        private void exportDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuColorChange(localRecords);
            ((MainController)controller).OnExportDb();
        }

        private void importDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuColorChange(localRecords);
            ((MainController)controller).OnImportDb();
        }

        private void DemographicSearchMenuItem_Click(object sender, EventArgs e)
        {
            MenuColorChange(SearchMenu);
            StaticData.firPending = false;
            StaticData.dataWithBio = false;
            ((MainController)controller).OnSearchCriminal();
        }

        private void BiometricSearchMenuItem_Click(object sender, EventArgs e)
        {
            MenuColorChange(SearchMenu);
            ((MainController)controller).OnBiometricSearch();
        }
    }
}
