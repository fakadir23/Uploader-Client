using ISTL.COMMON.Common;
using ISTL.MODELS.DTO.Webcam;
using ISTL.RAB.Controllers;
using ISTL.RAB.DbManager;
using ISTL.RAB.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISTL.RAB.View.New.Enrollment.BiometricInformation
{
    public partial class ImageCaptureDialogForm : Form
    {
        Bitmap[] sampleImages = new Bitmap[4];
        int currentIndex = 0;
        ISTL.WEBCAM.PhotoCapture photoCapture = null;

        public WebcamData CamData { get; set; }

        String deviceName = "";

        public ImageCaptureDialogForm()
        {
            Font = new Font(Font.Name, 8.25f * 96f / CreateGraphics().DpiX, Font.Style, Font.Unit, Font.GdiCharSet, Font.GdiVerticalFont);
            InitializeComponent();
            CamData = new WebcamData();
        }

        public ImageCaptureDialogForm(String deviceName) : this()
        {
            this.deviceName = deviceName;
        }

        private void ImageCaptureDialogForm_Load(object sender, EventArgs e)
        {

            if (this.deviceName == null || this.deviceName.Length == 0)
            {
                DbDeviceManager dManager = new DbDeviceManager();
                try
                {
                    this.deviceName = dManager.GetDevice("CAM").Name;
                }
                catch (Exception ex)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "No Camera Set");
                    this.Close();
                    return;
                }
            }

            List<string> supportedCameraModels = new List<string>();
            supportedCameraModels.AddRange(Devices.GetPhotoDeviceList().Select(x => x.Name));

            photoCapture = new WEBCAM.PhotoCapture(camPicBox, this.deviceName, supportedCameraModels);
            photoCapture.startCapture(camListCB.SelectedIndex);

            this.captureBtn.Enabled = false;
            this.timer1.Start();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (photoCapture != null)
            {
                photoCapture.closeCamera();
            }

            this.timer1.Stop();
        }

        private void captureBtn_Click(object sender, EventArgs e)
        {
            snapshot spn = new snapshot(spanshotDelegate);
            this.Invoke(spn);
            //photoCapture.startCapture();
        }

        delegate void snapshot();

        private void spanshotDelegate()
        {
            try
            {
                Bitmap photo = photoCapture.takeSnapshot();
                if (photo != null)
                {
                    sampleImages[currentIndex] = photo;
                    currentIndex++;
                    if (currentIndex == 4)
                    {
                        currentIndex = 0;
                    }
                    updateSampleImage();
                }
            }
            catch (Exception ex)
            {
                /*String exp = ex.StackTrace;
                MessageBox.Show(exp);*/
            }
        }

        private void updateSampleImage()
        {
            for (int i = 0; i < sampleImages.Length; i++)
            {
                if (i == 0)
                {
                    samplePB1.Image = sampleImages[i];
                }
                if (i == 1)
                {
                    samplePB2.Image = sampleImages[i];
                }
                if (i == 2)
                {
                    samplePB3.Image = sampleImages[i];
                }
                if (i == 3)
                {
                    samplePB4.Image = sampleImages[i];
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (photoCapture.getDetection())
            {
                if (chkAutoCapture.Checked)
                {
                    this.captureBtn.Enabled = false;
                    timer1.Stop();
                    captureBtn_Click(null, null);
                    timer1.Start();
                }
                else
                {
                    this.captureBtn.Enabled = true;
                }

                //this.captureBtn.Enabled = true;
            }
            else
            {
                this.captureBtn.Enabled = false;
            }
        }

        private void materialRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (materialRadioButton1.Checked)
            {
                if (this.samplePB1.Image != null)
                {
                    this.finalPB.Image = this.samplePB1.Image;
                }
            }
        }

        private void materialRadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (materialRadioButton3.Checked)
            {
                if (this.samplePB2.Image != null)
                {
                    this.finalPB.Image = this.samplePB2.Image;
                }
            }
        }

        private void materialRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (materialRadioButton2.Checked)
            {
                if (this.samplePB3.Image != null)
                {
                    this.finalPB.Image = this.samplePB3.Image;
                }
            }
        }

        private void materialRadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (materialRadioButton4.Checked)
            {
                if (this.samplePB4.Image != null)
                {
                    this.finalPB.Image = this.samplePB4.Image;
                }
            }
        }

        private void btnArrestInfo_Click(object sender, EventArgs e)
        {
            if (this.finalPB.Image != null)
            {
                this.CamData.CamImage = ImageToByte(this.finalPB.Image);

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                if (this.samplePB1.Image != null ||
                   this.samplePB2.Image != null ||
                   this.samplePB3.Image != null ||
                   this.samplePB4.Image != null)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please Select an Image.");
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                }
            }

        }

        private static byte[] ImageToByte(Image img)
        {
            try
            {
                ImageConverter converter = new ImageConverter();
                return (byte[])converter.ConvertTo(img, typeof(byte[]));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (photoCapture != null)
            {
                photoCapture.closeCamera();
            }

            photoCapture.startCapture(camListCB.SelectedIndex);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            this.samplePB1.Image = null;
            this.samplePB2.Image = null;
            this.samplePB3.Image = null;
            this.samplePB4.Image = null;
            this.finalPB.Image = null;
            this.currentIndex = 0;
            for (int i = 0; i < 4; i++)
            {
                this.sampleImages[i] = null;
            }

            if (materialRadioButton1.Checked) materialRadioButton1.Checked = false;
            if (materialRadioButton2.Checked) materialRadioButton2.Checked = false;
            if (materialRadioButton3.Checked) materialRadioButton3.Checked = false;
            if (materialRadioButton4.Checked) materialRadioButton4.Checked = false;
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void exitButton_Click_1(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.OK;
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
