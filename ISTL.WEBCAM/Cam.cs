using AForge.Video.DirectShow;
using ISTL.MODELS.DTO.Webcam;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ISTL.WEBCAM
{
    public partial class Cam : Form
    {

        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;
        bool stop = false;
        bool stopped = true;
        Bitmap snapshot;

        public WebcamData CamData { get; set; }

        public Cam()
        {
            InitializeComponent();
            CamData = new WebcamData();
            this.Width = 640;
            this.Height = 540;
        }

        private void Cam_Load(object sender, EventArgs e)
        {
            try
            {
                filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                foreach (FilterInfo filterInfo in filterInfoCollection)
                {
                    cmbCamera.Items.Add(filterInfo.Name);
                }
                cmbCamera.SelectedIndex = 0;
                videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[cmbCamera.SelectedIndex].MonikerString);
                videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
                videoCaptureDevice.VideoResolution = selectResolution(videoCaptureDevice);
                videoCaptureDevice.Start();
                stopped = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show("There was an error when opening Web Cam.");
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void VideoCaptureDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            picBox.Image = (Bitmap)eventArgs.Frame.Clone();
            if (stop)
            {
                videoCaptureDevice.SignalToStop();
                this.snapshot = (Bitmap)eventArgs.Frame.Clone();
                stopped = true;
            }
        }

        private static VideoCapabilities selectResolution(VideoCaptureDevice device)
        {
            foreach (var cap in device.VideoCapabilities)
            {
                if (cap.FrameSize.Height == 480)
                    return cap;
                if (cap.FrameSize.Width == 640)
                    return cap;
            }
            return device.VideoCapabilities.Last();
        }

        private void closeCamera()
        {
            if (videoCaptureDevice.IsRunning)
            {
                videoCaptureDevice.Stop();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            closeCamera();
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            stop = true;
            while (true)
            {
                if (stopped)
                {
                    this.CamData.CamImage = ImageToByte(this.snapshot);
                    this.DialogResult = DialogResult.OK;
                    break;
                }

            }
        }

        private void Cam_FormClosing(object sender, FormClosingEventArgs e)
        {
            closeCamera();
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
    }
}

