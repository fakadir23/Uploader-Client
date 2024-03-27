using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ISTL.COMMON;
using ISTL.RAB.Controllers;
using ISTL.COMMON.Threads;
using ISTL.COMMON.Subscription;
using System.Text.RegularExpressions;
using ISTL.RAB.Entity;
using System.Threading;
using NLog;
using ISTL.PERSOGlobals;
using System.Runtime.InteropServices;
using ISTL.COMMON.Common;
using GlobalsCommon;
using ISTL.RAB.Asynch;
using System.Threading.Tasks;
using ISTL.RAB.DbManager;

namespace ISTL.RAB.View
{
    public partial class LoginForm : ViewForm, IOnlineObservable, IUploadObservable, IDeviceObservable
    {
        // Drop shadow souce code, got from:
        // http://www.codeproject.com/Articles/19277/Let-Your-Form-Drop-a-Shadow

        // Define the CS_DROPSHADOW constant
        private const int CS_DROPSHADOW = 0x00020000;

        // Override the CreateParams property
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        #region Declaration(s)
        private OnlineSubject onlineStatus;
        private OnlineObserver onlineObserver;
        private UploadSubject uploadStatus;
        private UploadObserver uploadObserver;
        private DeviceObserver fingerprintDeviceObserver;
        private DeviceSubject fingerprintDeviceStatus;
        private DeviceObserver signatureDeviceObserver;
        private DeviceSubject irisDeviceStatus;
        private DeviceObserver webcamDeviceObserver;
        private DeviceSubject webcamDeviceStatus;
        private bool deviceFpScanner;
        private bool deviceWebcam;
        private bool deviceIrisScanner;
        private Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        public LoginForm()
        {
            InitializeComponent();
            this.tbUser.Focus();
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
        
        public string Username
        {
            get { return this.tbUser.Text; }
            set { this.tbUser.Text = value; }
        }

        public string Password
        {
            get { return this.tbPass.Text; }
            set { this.tbPass.Text = value; }
        }

        public string DeviceId
        {
            get { return this.tbDeviceId.Text; }
            set { this.tbDeviceId.Text = value; }
        }

        public string PendingCount
        {
            get { return this.lblRPU.Text; }
            set { this.lblRPU.Text = value; }
        }

        protected override void OnLoad()
        {
            Users.ClearAll();
            SetLocalizedValue();

            //Set version number
           this.lblVersion.Text = String.Format("V {0}", Globals.Assembly.EXE_RAB_CDMS);

            // Set token by admin user
            //((LoginController)controller).SetTokenByAdminUser();
            // Task.Run(() => AppUtils.AppUtils.SetTokenByAdminUser());

            // Start upload thread
            //ThreadHandler.GetInstance(new UploadEnrollmentAsynch()).StartThread();
            //ThreadHandler.GetInstance(new UploadSpecialEnrollAsync()).StartThread();
            //ThreadHandler.GetInstance(new UploadThreadsAsync()).StartThread();

            // Setup observers
            SubscribeToNotifications();

            // CheckDevices();
        }
       
        public void OnFocus()
        {
            this.tbUser.Focus();
        }

        public override void OnClosing()
        {
            UnsubscribeFromNotifications();
        }        

        private void btnLogin_Click(object sender, EventArgs e)
        {            
            ((LoginController)controller).OnLogin();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ((LoginController)controller).OnExit();
        }

        public void SubscribeToNotifications()
        {
            onlineStatus = (OnlineSubject)SubjectFactory.GetInstance().GetSubject(OnlineSubject.Name);
            uploadStatus = (UploadSubject)SubjectFactory.GetInstance().GetSubject(UploadSubject.Name);

            fingerprintDeviceStatus = (DeviceSubject)SubjectFactory.GetInstance().GetSubject(Globals.DeviceSubject.FP);
            webcamDeviceStatus = (DeviceSubject)SubjectFactory.GetInstance().GetSubject(Globals.DeviceSubject.CAM);
            irisDeviceStatus = (DeviceSubject)SubjectFactory.GetInstance().GetSubject(Globals.DeviceSubject.IRIS);

            onlineObserver = new OnlineObserver(onlineStatus, this);
            onlineStatus.Attach(onlineObserver);

            uploadObserver = new UploadObserver(uploadStatus, this);
            uploadStatus.Attach(uploadObserver);

            fingerprintDeviceObserver = new DeviceObserver(fingerprintDeviceStatus, this);
            fingerprintDeviceStatus.Attach(fingerprintDeviceObserver);

            webcamDeviceObserver = new DeviceObserver(webcamDeviceStatus, this);
            webcamDeviceStatus.Attach(webcamDeviceObserver);

            signatureDeviceObserver = new DeviceObserver(irisDeviceStatus, this);
            irisDeviceStatus.Attach(signatureDeviceObserver);
        }

        public void UnsubscribeFromNotifications()
        {
            onlineStatus.Detach(onlineObserver);
            uploadStatus.Detach(uploadObserver);
            fingerprintDeviceStatus.Detach(fingerprintDeviceObserver);
            webcamDeviceStatus.Detach(webcamDeviceObserver);
            irisDeviceStatus.Detach(signatureDeviceObserver);
        }

        #region IDeviceObservable Members

        public void ShowOnlineStatus()
        {
            // Do nothing for now
        }

        public void ShowOfflineStatus()
        {
            // Do nothing for now
        }

        public void ShowUploadIdleStatus(int pending)
        {
            this.PendingCount = pending.ToString();
            this.picUploading.Visible = false;
        }

        public void ShowUploadingStatus(int pending, string timeLeft)
        {
            this.PendingCount = pending.ToString();
            this.picUploading.Visible = true;
        }

        public void ShowUploadFailedStatus(int pending)
        {
            this.PendingCount = pending.ToString();
            this.picUploading.Visible = false;
        }

        public void ShowDeviceStatus()
        {
            if (deviceFpScanner && deviceWebcam && deviceIrisScanner)
            {
                this.lblDeviceCheck.Text = "Devices ok";
                //this.btnDevices.Visible = true;

                // Test code
                this.btnDevices.Visible = false;

                this.picDeviceCheck.Visible = false;
                this.picDeviceNotOk.Visible = false;
                this.picDeviceOk.Visible = true;
            }
            else
            {
                this.lblDeviceCheck.Text = "Device failed";
                //this.btnDevices.Visible = true;

                // Test code
                this.btnDevices.Visible = false;

                this.picDeviceCheck.Visible = false;
                this.picDeviceNotOk.Visible = true;
                this.picDeviceOk.Visible = false;
            }
        }

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
        //    ShowDeviceStatus();
        //}

        //public void ShowConnectedStatus(string name, bool status, string actualName)
        //{
        //    logger.Debug("CONNECTED*****************" + name + " -- " + status);

        //    if (name.Equals(Configuration.Cam?.Name))
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

        //    ShowDeviceStatus();
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

            ShowDeviceStatus();
        }

        #endregion

        public void SetLocalizedValue()
        {
            //this.lblTitle.Text = _translator.Translate(LanguageConstant.ISTL);
            //this.lblVersion.Text = String.Format(_translator.Translate(LanguageConstant.Version) + " {0}", Globals.Assembly.EXE_ISTLPERSO);
            //this.lblUser.Text = String.Format("{0}:", _translator.Translate(LanguageConstant.Username));
            //this.lblPassword.Text = String.Format("{0}:", _translator.Translate(LanguageConstant.Password));
            //this.btnLoginX.Text = _translator.Translate(LanguageConstant.Login);
           // this.lblPendingTitle.Text = _translator.Translate(LanguageConstant.PendingUpload);
         //   this.lblSelectLanguage.Text = _translator.Translate(LanguageConstant.SelectLanguage) + ":";
        }

        private void General_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (((TextBox)sender).Name.Equals("tbPass"))
                {
                    ((LoginController)controller).OnLogin();
                }
                else
                {
                    this.SelectNextControl(
                        (Control)sender, true, true, true, true);

                    e.Handled = e.SuppressKeyPress = true;
                }
            }
        }

        private void tbUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(tbUser.Text))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please type your user name");
                    return;
                }
                else
                {
                    tbPass.Focus();
                }
            }
        }

        private void tbPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(tbPass.Text))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please type your password");
                    return;
                }
                else
                {
                    btnLogin_Click(null, null);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tbDeviceId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(tbDeviceId.Text))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please type Device Id");
                    return;
                }
                else
                {
                    btnLogin_Click(null, null);
                }
            }
        }
    }
}
