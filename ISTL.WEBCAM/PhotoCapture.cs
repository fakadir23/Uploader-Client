
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
using EOSDigital.API;
using EOSDigital.SDK;
using Rectangle = System.Drawing.Rectangle;
using System.IO;



namespace ISTL.WEBCAM
{
    public class PhotoCapture
    {
        volatile bool isSnapshot = false;
        volatile bool isDetect = false;
        volatile bool snapshotTaken = false;
        Bitmap snapshot;
        int marginWidth = 50;
        int marginHeight = 100;
        private Logger logger = LogManager.GetCurrentClassLogger();


        
        #region Canon Variables
        CanonAPI APIHandler;
        Camera MainCamera;
        List<Camera> CamList;
        bool IsInit = false;
        Bitmap Evf_Bmp;
        int LVBw, LVBh, w, h;
        float LVBratio, LVration;

        object ErrLock = new object();
        object LvLock = new object();

        #endregion

        PictureBox picBox;

        int selectedIndex = -1;

        static readonly CascadeClassifier classifier = new CascadeClassifier("haarcascade_frontalface_alt_tree.xml");

        public PhotoCapture(PictureBox picBox, String deviceName, List<string> supportedCameraModels)
        {
            #region new Canon
            logger.Info("Camera Constructor Entry.");

            this.picBox = picBox;
            int i = 0;
            String selectedDeviceName = deviceName;

            try
            {
                APIHandler = new CanonAPI();
            }
            catch (DllNotFoundException) { MessageBox.Show("Canon DLLs not found!"); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            CamList = APIHandler.GetCameraList();

            foreach (Camera camera in CamList)
            {
                if (supportedCameraModels.Contains(selectedDeviceName) && camera.DeviceName == selectedDeviceName)
                {
                    selectedIndex = i;
                    break;
                }
                i++;
            }

            logger.Info("Camera Constructor Exit.");

            #endregion


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
                /*APIHandler = new CanonAPI();*/
                ErrorHandler.SevereErrorHappened += ErrorHandler_SevereErrorHappened;
                ErrorHandler.NonSevereErrorHappened += ErrorHandler_NonSevereErrorHappened;
                picBox.Paint += LiveViewPicBox_Paint;
                LVBw = picBox.Width;
                LVBh = picBox.Height;
                IsInit = true;

                try
                {
                    if (MainCamera?.SessionOpen == true) CloseSession();
                    else OpenSession(selectedIndex);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No device Found");
                }
                try
                {

                    if (!MainCamera.IsLiveViewOn) { MainCamera.StartLiveView(); }
                    else { MainCamera.StopLiveView();}
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            catch (Exception ex)
            {
                logger.Error("There was an unexpected error when starting camera. Error Message:\n" + ex.ToString());
                MessageBox.Show("Please setup camera driver first and try again.", "RAB CDMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Canon Without DLL Section

        #region API Events

       
        private void MainCamera_StateChanged(Camera sender, StateEventID eventID, int parameter)
        {
            try 
            {
                if (eventID == StateEventID.Shutdown && IsInit)
                {
                    CloseSession();
                }
                
            }
            catch (Exception ex) { MessageBox.Show(ex.Message + "Main_camera_satechanged"); }
        }


        private void MainCamera_LiveViewUpdated(Camera sender, Stream img)
        {
            try
            {
                MainCamera.SendCommand(CameraCommand.DriveLensEvf, (int)DriveLens.Near2);
                lock (LvLock)
                {
                    Evf_Bmp?.Dispose();
                    Evf_Bmp = new Bitmap(img);
                    FaceDetect(Evf_Bmp);
                }
                picBox.Invalidate();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message + "MainCamera_LiveViewUpdated"); }
        }
        private void LiveViewPicBox_Paint(object sender, PaintEventArgs e)
        {
            if (MainCamera == null || !MainCamera.SessionOpen) return;

            if (!MainCamera.IsLiveViewOn) e.Graphics.Clear(Control.DefaultBackColor);

            else
            {
                lock (LvLock)
                {
                    if (Evf_Bmp != null)
                    {
                        LVBratio = LVBw / (float)LVBh;
                        LVration = Evf_Bmp.Width / (float)Evf_Bmp.Height;
                        if (LVBratio < LVration)
                        {
                            w = LVBw;
                            h = (int)(LVBw / LVration);
                        }
                        else
                        {
                            w = (int)(LVBh * LVration);
                            h = LVBh;
                        }
                        e.Graphics.DrawImage(Evf_Bmp, 0, 0, w, h);
                    }
                }
            }
        }

        private void ErrorHandler_NonSevereErrorHappened(object sender, ErrorCode ex)
        {
            MessageBox.Show($"SDK Error code: {ex} ({((int)ex).ToString("X")})"+ "ErrorHandler_NonSevereErrorHappened");
        }

        private void ErrorHandler_SevereErrorHappened(object sender, Exception ex)
        {
            MessageBox.Show(ex.Message + "ErrorHandler_SevereErrorHappened");
        }

        #endregion

        private void CloseSession()
        {
            if(MainCamera!=null)
            {
                try
                {
                    MainCamera.CloseSession();
                }
                catch
                {
                    MessageBox.Show("Please Check Your Camera");
                }
            }
            
            
        }
        private void OpenSession(int i)
        {
            if (i >= 0)
            {
                MainCamera = CamList[i];
                MainCamera.OpenSession();
                MainCamera.LiveViewUpdated += MainCamera_LiveViewUpdated;
                MainCamera.StateChanged += MainCamera_StateChanged;
            }
        }

        #endregion

        public void closeCamera()
        {
            CloseSession();
            try
            {

                IsInit = false;
                MainCamera?.Dispose();
                APIHandler?.Dispose();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public Bitmap takeSnapshot()
        {
            isSnapshot = true;
            Bitmap takenImage = null;
            while (true)
            {
                if (snapshotTaken)
                {
                    float quality;
                    takenImage = this.snapshot;
                    Image<Bgr, byte> grayScale = new Image<Bgr, byte>(takenImage);
                    Mat mat = grayScale.Mat;
                    quality = calcBlurriness(mat);
                    logger.Debug("Image Quality: " + quality);
                    snapshotTaken = false;
                    break;
                }

            }
            return takenImage;
        }
        public bool getDetection()
        {
            return isDetect;
        }

       /* private void UpdatePicture(Bitmap bitmap)
        {
            if (isSnapshot)
            {
                this.snapshot = bitmap;
            }
            else
            {
                isSnapshot = false;
            }

        }
        private void UpdateSnap()
        {
            if (isSnapshot)
            {
                if (!isDetect)
                {
                    this.snapshot = null;
                }
                snapshotTaken = true;
            }
        }*/
        private void FaceDetect(Bitmap bitmapImage)
        {
            
            try
            {
                isDetect = false;

                Bitmap bitmap = bitmapImage;

                Image<Bgr, byte> grayScale = new Image<Bgr, byte>(bitmap);
                /*Mat mat = grayScale.Mat;
                quality=calcBlurriness(mat);*/
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
                    /*Bitmap bitmap1 = cropImage(bitmap, boxes[i]);
                    Image<Bgr, byte> gray = new Image<Bgr, byte>(bitmap1);
                    Mat mat = gray.Mat;
                    quality = calcBlurriness(mat);
                    Console.WriteLine(quality * 1000000);*/
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
        static float calcBlurriness(Mat src)
        {
            Mat Gx = new Mat();
            Mat Gy = new Mat();
            CvInvoke.Sobel(src, Gx,Emgu.CV.CvEnum.DepthType.Cv32F, 1, 0);
            CvInvoke.Sobel(src, Gy, Emgu.CV.CvEnum.DepthType.Cv32F, 0, 1);
            double normGx = CvInvoke.Norm(Gx);
            double normGy = CvInvoke.Norm(Gy);
            double sumSq = normGx * normGx + normGy * normGy;
            return (float)(1.0 / (sumSq / (src.Height * src.Width) + 1e-6));
        }
    }
}
  