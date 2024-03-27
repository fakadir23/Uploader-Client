using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ISTL.COMMON;
using ISTL.MODELS.DTO.Device;
using NLog;
using ISTL.RAB.Entity;
using ISTL.RAB.Controllers;
using ISTL.RAB.DbManager;
using ISTL.COMMON.Subscription;
using ISTL.PERSOGlobals;
using ISTL.WEBCAM;
using ISTL.RAB.View.New.Enrollment.BiometricInformation;
using ISTL.RAB.Controllers.New.Home;

namespace ISTL.RAB.View.New
{
    public partial class DiagnoseUserControl : ViewUserControl
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private string selCam;
        private string selFP;
        private string selIris;
        private DbDeviceManager dbDeviceManager;
        public DiagnoseUserControl()
        {
            InitializeComponent();
            dbDeviceManager = new DbDeviceManager();
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            SetBiometricDevices();
            LoadBiometricDevices();
        }

        private void SetBiometricDevices()
        {
            selCam = dbDeviceManager.GetDevice(Globals.DeviceCategory.CAM)?.Name;
            selFP = dbDeviceManager.GetDevice(Globals.DeviceCategory.FP)?.Name;
            selIris = dbDeviceManager.GetDevice(Globals.DeviceCategory.IRIS)?.Name;

            if (!string.IsNullOrWhiteSpace(selCam)) lblSelCam.Text = selCam;
            if (!string.IsNullOrWhiteSpace(selFP)) lblSelFP.Text = selFP;
            if (!string.IsNullOrWhiteSpace(selIris)) lblSelIris.Text = selIris;
        }

        private void LoadBiometricDevices()
        {
            Camera = Devices.GetPhotoDeviceList();
            FP = Devices.GetFpDeviceList();
            Iris = Devices.GetIrisDeviceList();
        }

        public object Camera
        {
            get { return this.cbCam.SelectedItem; }
            set
            {
                try
                {
                    if (value != null)
                    {
                        this.cbCam.DataSource = value;
                        this.cbCam.DisplayMember = "DisplayName";
                        this.cbCam.SelectedIndex = -1;

                        var obj = (value as IList<DeviceDto>).Where(p => p.Name.Equals(selCam)).SingleOrDefault();
                        if (obj != null)
                        {
                            this.cbCam.SelectedItem = obj;
                        }
                    }
                }
                catch (Exception x)
                {
                    logger.Error("An error has occurred during setting device value of Camera in combobox.\n", x.ToString());
                }
            }
        }

        public object FP
        {
            get { return this.cbFP.SelectedItem; }
            set
            {
                try
                {
                    if (value != null)
                    {
                        this.cbFP.DataSource = value;
                        this.cbFP.DisplayMember = "DisplayName";
                        this.cbFP.SelectedIndex = -1;

                        var obj = (value as IList<DeviceDto>).Where(p => p.Name.Equals(selFP)).SingleOrDefault();
                        if (obj != null)
                        {
                            this.cbFP.SelectedItem = obj;
                        }
                    }
                }
                catch (Exception x)
                {
                    logger.Error("An error has occurred during setting device value of Fingerprint in combobox.\n", x.ToString());
                }
            }
        }

        public object Iris
        {
            get { return this.cbIris.SelectedItem; }
            set
            {
                try
                {
                    if (value != null)
                    {
                        this.cbIris.DataSource = value;
                        this.cbIris.DisplayMember = "Name";
                        this.cbIris.SelectedIndex = -1;

                        var obj = (value as IList<DeviceDto>).Where(p => p.Name.Equals(selIris)).SingleOrDefault();
                        if (obj != null)
                        {
                            this.cbIris.SelectedItem = obj;
                        }
                    }
                }
                catch (Exception x)
                {
                    logger.Error("An error has occurred during setting device value of Iris in combobox.\n", x.ToString());
                }
            }
        }

        private void SetCamDeviceSubject(DeviceDto selectedDevice)
        {
            Configuration.Cam = selectedDevice;
            DeviceSubject webcamDeviceStatus = (DeviceSubject)SubjectFactory.GetInstance().GetSubject(Globals.DeviceSubject.CAM);
            webcamDeviceStatus.IsConnected = Utils.IsDeviceConnected(selectedDevice?.Name,
                    selectedDevice?.Type, selectedDevice?.SupportedModel, out webcamDeviceStatus.actualName);
            webcamDeviceStatus.IsChecked = true;
            webcamDeviceStatus.Notify();
        }

        private void SetFpDeviceSubject(DeviceDto selectedDevice)
        {
            Configuration.FP = selectedDevice;
            DeviceSubject fpDeviceStatus = (DeviceSubject)SubjectFactory.GetInstance().GetSubject(Globals.DeviceSubject.FP);
            fpDeviceStatus.IsConnected = Utils.IsDeviceConnected(selectedDevice?.Name,
                    selectedDevice?.Type, selectedDevice?.SupportedModel, out fpDeviceStatus.actualName);
            fpDeviceStatus.IsChecked = true;
            fpDeviceStatus.Notify();
        }

        private void SetIrisDeviceSubject(DeviceDto selectedDevice)
        {
            Configuration.Iris = selectedDevice;
            DeviceSubject irisDeviceStatus = (DeviceSubject)SubjectFactory.GetInstance().GetSubject(Globals.DeviceSubject.IRIS);
            irisDeviceStatus.IsConnected = Utils.IsDeviceConnected(selectedDevice?.Name,
                    selectedDevice?.Type, selectedDevice?.SupportedModel, out irisDeviceStatus.actualName);
            irisDeviceStatus.IsChecked = true;
            irisDeviceStatus.Notify();
        }

        private void btnSetCam_Click(object sender, EventArgs e)
        {
            if (Camera == null)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please select Device.");
                return;
            }

            var ret = dbDeviceManager.AddCamDevices((DeviceDto)Camera, "CAM");

            if (ret)
            {
                SetBiometricDevices();
                SetCamDeviceSubject((DeviceDto)Camera);
                InfoMessageBox.ShowMessage("SNSOP TOOLS", "Camera device is set successfully.");
            }
            else
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Camera device is failed to set. Please contact with your Administrator.");
            }
        }

        private void btnSetFP_Click(object sender, EventArgs e)
        {
            if (FP == null)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please select Device.");
                return;
            }

            var ret = dbDeviceManager.AddCamDevices((DeviceDto)FP, "FP");

            if (ret)
            {
                SetBiometricDevices();

                SetFpDeviceSubject((DeviceDto)FP);
                InfoMessageBox.ShowMessage("SNSOP TOOLS", "Fingerprint device is set successfully.");
            }
            else
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Fingerprint device is failed to set. Please contact with your Administrator.");
            }
        }

        private void btnSetIris_Click(object sender, EventArgs e)
        {
            if (Iris == null)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please select Device.");
                return;
            }

            var ret = dbDeviceManager.AddCamDevices((DeviceDto)Iris, "IRIS");

            if (ret)
            {
                SetBiometricDevices();

                SetIrisDeviceSubject((DeviceDto)Iris);
                InfoMessageBox.ShowMessage("SNSOP TOOLS", "Iris device is set successfully.");
            }
            else
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Iris device is failed to set. Please contact with your Administrator.");
            }
        }

        private void btnCheckFP_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = DialogResult.OK;
                var form = new FingerprintCaptureDialogForm(((DeviceDto)FP).Name);
                dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    this.pbRT.Image = Utils.ByteToImage(form.FingerData.FpRt);
                    this.pbRI.Image = Utils.ByteToImage(form.FingerData.FpRi);
                    this.pbRM.Image = Utils.ByteToImage(form.FingerData.FpRm);
                    this.pbRR.Image = Utils.ByteToImage(form.FingerData.FpRr);
                    this.pbRS.Image = Utils.ByteToImage(form.FingerData.FpRl);

                    this.pbLT.Image = Utils.ByteToImage(form.FingerData.FpLt);
                    this.pbLI.Image = Utils.ByteToImage(form.FingerData.FpLi);
                    this.pbLM.Image = Utils.ByteToImage(form.FingerData.FpLm);
                    this.pbLR.Image = Utils.ByteToImage(form.FingerData.FpLr);
                    this.pbLS.Image = Utils.ByteToImage(form.FingerData.FpLl);
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please select a device or check the device connection");
            }
        }

        private void btnCheckCam_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = DialogResult.OK;
                var form = new ImageCaptureDialogForm(((DeviceDto)Camera).Name);
                dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    this.pbPhoto.Image = Utils.ByteToImage(form.CamData.CamImage);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Photo capture error." + ex.ToString());
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please select a device or check the device connection");
            }

        }

        private void btnCheckIris_Click(object sender, EventArgs e)
        {
            DialogResult dr = DialogResult.OK;
            try
            {
                var form = new IrisCaptureDialogForm(((DeviceDto)Iris).Name);
                dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    this.pbLeftIris.Image = Utils.ByteToImage(form.irisData.LeftIris);
                    this.pbRightIris.Image = Utils.ByteToImage(form.irisData.RightIris);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Please select a device or check the device connection");
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please select a device or check the device connection");
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            ((DiagnoseController)controller).GoBacktoDashboard();
        }
    }
}
