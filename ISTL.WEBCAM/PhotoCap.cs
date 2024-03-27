using AForge.Video.DirectShow;
using Emgu.CV;
using Emgu.CV.Structure;
using ISTL.MODELS.DTO.Webcam;
using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ISTL.WEBCAM
{
    class PhotoCap
    {
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;
        volatile bool isSnapshot = false;
        volatile bool isDetect = false;
        volatile bool snapshotTaken = false;
        Bitmap snapshot;
        int detectWidth = 200;
        int detectHeight = 300;
        int marginWidth = 50;
        int marginHeight = 100;
        private Logger logger = LogManager.GetCurrentClassLogger();


        PictureBox picBox;

        int selectedIndex = -1;

        static readonly CascadeClassifier classifier = new CascadeClassifier("haarcascade_frontalface_alt_tree.xml");

        public PhotoCap(PictureBox picBox, String deviceName, List<string> supportedCameraModels)
        {
            logger.Info("Camera Constructor Entry.");

            this.picBox = picBox;

            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            int i = 0;
            String selectedDeviceName = deviceName;

            if (filterInfoCollection.Count <= 0)
            {
                logger.Info("No camera filter info collection found.");
            }


            foreach (FilterInfo filterInfo in filterInfoCollection)
            {
                //if (filterInfo.Name == "EOS Webcam Utility" && (selectedDeviceName=="Canon EOS 700D" || selectedDeviceName == "Canon EOS 1100D"))
                //{
                //    selectedIndex = i;
                //    break;
                //}

                logger.Info("Camera Filter Name: " + filterInfo.Name + " :: Selected Camera Name: " + selectedDeviceName);

                if (filterInfo.Name == "EOS Webcam Utility" && supportedCameraModels.Contains(selectedDeviceName))
                {
                    selectedIndex = i;
                    break;
                }

                i++;

                /*if(filterInfo.Name.Contains(selectedDeviceName))
                {
                    selectedIndex = i;
                    break;
                }  */


                ///cmbCamera.Items.Add(filterInfo.Name);
            }
            ///cmbCamera.SelectedIndex = 0;            
            logger.Info("Camera Constructor Exit.");
        }

        public void startCapture(int index)
        {
            if (selectedIndex < 0)
            {
                MessageBox.Show("Photo capture device not found.", "RAB CDMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (videoCaptureDevice == null || !videoCaptureDevice.IsRunning)
                {

                    videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[selectedIndex].MonikerString);
                    videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
                    videoCaptureDevice.VideoResolution = selectResolution(videoCaptureDevice);
                    videoCaptureDevice.Start();
                }
            }
            catch (Exception ex)
            {
                logger.Error("There was an unexpected error when starting camera. Error Message:\n" + ex.ToString());
                MessageBox.Show("Please setup camera driver first and try again.", "RAB CDMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VideoCaptureDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            try
            {
                isDetect = false;

                Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();

                Image<Bgr, byte> grayScale = new Image<Bgr, byte>(bitmap);
                //Rectangle[] boxes = classifier.DetectMultiScale(grayScale, 1.2, 1, new Size(detectWidth, detectHeight));
                Rectangle[] boxes = classifier.DetectMultiScale(grayScale, 1.2, 1);

                for (int i = 0; i < boxes.Length; i++)
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        using (Pen pen = new Pen(Color.Green, 2))
                        {
                            boxes[i].X = boxes[i].X - (marginWidth / 2);
                            boxes[i].Y = boxes[i].Y - (marginHeight / 2);
                            boxes[i].Height = boxes[i].Height + marginHeight;
                            boxes[i].Width = boxes[i].Width + marginWidth;
                            g.DrawRectangle(pen, boxes[i]);
                        }
                    }
                    isDetect = true;
                    if (isSnapshot)
                    {
                        this.snapshot = cropImage(bitmap, boxes[i]);
                    }
                    else
                    {
                        //this.snapshot = bitmap;
                    }

                    break;
                }

                picBox.Image = bitmap;
                if (isSnapshot)
                {
                    if (!isDetect)
                    {
                        this.snapshot = null;
                    }
                    snapshotTaken = true;
                }
            }
            catch { }
        }

        private VideoCapabilities selectResolution(VideoCaptureDevice device)
        {
            foreach (var cap in device.VideoCapabilities)
            {
                if (cap.FrameSize.Height == picBox.Height)
                    return cap;
                if (cap.FrameSize.Width == picBox.Width)
                    return cap;
            }
            return device.VideoCapabilities.First();
        }

        public void closeCamera()
        {
            if (videoCaptureDevice != null && videoCaptureDevice.IsRunning)
            {
                videoCaptureDevice.Stop();
            }
        }
        public Bitmap takeSnapshot()
        {
            isSnapshot = true;
            Bitmap takenImage = null;
            while (true)
            {
                if (snapshotTaken)
                {
                    takenImage = this.snapshot;
                    snapshotTaken = false;
                    break;
                }

            }
            return takenImage;
        }

        private Bitmap cropImage(Bitmap src, Rectangle cropRect)
        {

            Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                                 cropRect,
                                 GraphicsUnit.Pixel);
            }
            return target;
        }

        public bool getDetection()
        {
            return isDetect;
        }
    }
}
