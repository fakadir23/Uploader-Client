using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IriMagicBinoSDK;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using NLog;
using Iddk2000DotNet;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ISTL.IRIS
{
    public class IriShield : IIrisEngine
    {
        IIrisControl irisControl;
        ImageCtrl leftCtrl;
        ImageCtrl rightCtrl;
        private System.Windows.Forms.Timer irisShieldTimer;
        private Logger logger = LogManager.GetCurrentClassLogger();
        private Iddk2000APIs apis = null;
        private bool isBino = false;

        public IriShield(IIrisControl irisControl, ImageCtrl leftCtrl, ImageCtrl rightCtrl)
        {
            this.irisControl = irisControl;
            this.leftCtrl = leftCtrl;
            this.rightCtrl = rightCtrl;
            this.leftCtrl.Tag = "LeftEyeWnd";
            this.rightCtrl.Tag = "RightEyeWnd";

            irisShieldTimer = new Timer();
            irisShieldTimer.Interval = 66; // As per sdk
            irisShieldTimer.Tick += new EventHandler(OnStreamingIrisV1);
        }

        private void UpdateCaptureResult()
        {

        }

        public override void CloseDevice()
        {
            if (apis != null)
            {
                apis.CloseDevice();
                apis = null;
            }
        }

        public override bool OpenDevice(string pos)
        {
            try
            {
                IddkConfig config = new IddkConfig();
                IddkResult ret = IddkResult.OK;
                List<string> deviceDescs = new List<string>();
                apis = new Iddk2000APIs();

                /* We should get the current configuration before setting new one */
                ret = Iddk2000APIs.GetSdkConfig(config);
                if (ret != IddkResult.OK)
                {
                    MessageBox.Show("Failed to open the device!");
                    logger.Error("Failed to set the configuration.");
                    return false;
                }

                config.CommStd = IddkCommStd.Usb;

                /* Clear current capture section and exit any available administrator login */
                config.ResetOnOpenDevice = true;

                /* Set new configuration*/
                ret = Iddk2000APIs.SetSdkConfig(config);
                if (ret != IddkResult.OK)
                {
                    MessageBox.Show("Failed to open the device!");
                    logger.Error("Failed to set configuration.");
                    return false;
                }

                /* Now, we can open device*/
                /* If USB, we should scan devices first */
                string devDesc = "";
                ret = Iddk2000APIs.ScanDevices(deviceDescs);
                if (ret != IddkResult.OK)
                {
                    if (ret == IddkResult.DeviceNotFound)
                    {
                        MessageBox.Show("No IriShield devices found !");
                    }
                    else
                    {
                        MessageBox.Show("Failed to open the device!");
                    }
                    return false;
                }

                //Choose first device as default
                devDesc = deviceDescs[0];

                /* Open device */
                ret = apis.OpenDevice(devDesc);
                if (ret != IddkResult.OK)
                {
                    MessageBox.Show("Failed to open the device!");
                    return false;
                }

                StartCapture(null);

                return true;
            }
            catch (Exception ex)
            {
                logger.Error("There was an unexpected error when opening iris scanner (IriShield): " + ex.ToString());
            }
            return false;
        }

        public override void StartCapture(string pos)
        {
            OnStartCapture();
        }

        private void OnStartCapture()
        {
            try
            {
                IddkInteger imageWidth = new IddkInteger();
                IddkInteger imageHeight = new IddkInteger();
                IddkResult ret = IddkResult.OK;

                /* We have to init camera first */
                ret = apis.InitCamera(imageWidth, imageHeight);
                if (ret != IddkResult.OK)
                {
                    MessageBox.Show("Upable to open device.");
                    logger.Error("Upable to init device.");
                    return;
                }

                //apis.StopCapture();
                if (ret != IddkResult.OK)
                {
                    apis.DeinitCamera();
                    return;
                }
                irisShieldTimer.Start();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Upable to open device.");
                logger.Error("There was an unexpected error when starting iris capture (IriShield). Error Message: \n" + ex.ToString());
                if (irisShieldTimer != null) irisShieldTimer.Stop();
            }
        }

        private Image CreateBitmap8bits(byte[] pRawBuffer, int nWidth, int nHeight)
        {
            Bitmap bmpBitmap = new Bitmap(nWidth, nHeight, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            ColorPalette pal = bmpBitmap.Palette;
            for (int i = 0; i <= 255; i++)
            {
                pal.Entries[i] = Color.FromArgb(i, i, i);
            }
            bmpBitmap.Palette = pal;
            BitmapData bmpData;
            bmpData = bmpBitmap.LockBits(new Rectangle(0, 0, bmpBitmap.Width, bmpBitmap.Height), ImageLockMode.WriteOnly, bmpBitmap.PixelFormat);
            Marshal.Copy(pRawBuffer, 0, bmpData.Scan0, pRawBuffer.Length);
            bmpBitmap.UnlockBits(bmpData);
            return bmpBitmap;
        }

        private List<IddkImage> GetResultImageList()
        {
            /* For result image */
            List<IddkImage> resultImages = new List<IddkImage>();
            int compressionRatio = 0;
            IddkImageFormat imageFormat = IddkImageFormat.MonoJpeg2000;
            IddkImageKind imageKind = IddkImageKind.K1;

            /* Other params */
            IddkResult ret = IddkResult.OK;

            ret = apis.GetResultImage(imageKind, imageFormat, (byte)compressionRatio, resultImages);
            return resultImages;
        }

        private void OnStreamingIris(Object obj, EventArgs args)
        {
            try
            {
                Image imgLeft = null;
                Image imgRight = null;

                Bitmap bmpright = null;
                Bitmap bmpleft = null;

                /* For streaming images */
                List<IddkImage> images = new List<IddkImage>();
                IddkInteger imageWidth = new IddkInteger();
                IddkInteger imageHeight = new IddkInteger();

                IddkResult res = new IddkResult();
                res = apis.InitCamera(imageWidth, imageHeight);
                if (res != IddkResult.OK)
                {
                    MessageBox.Show("Upable to open device.");
                    logger.Error("Upable to init device.");
                    return;
                }

                /* Parameters for capturing */
                IddkCaptureMode captureMode = IddkCaptureMode.TimeBased;
                IddkQualityMode qualityMode = IddkQualityMode.Normal;
                IddkEyeSubtype eyeSubtype = IddkEyeSubtype.Both;

                /* Other params */
                IddkResult ret = IddkResult.OK;
                IddkCaptureStatus captureStatus = IddkCaptureStatus.Idle;
                IddkDeviceConfig deviceConfig = new IddkDeviceConfig();
                List<IddkIrisQuality> qualities = new List<IddkIrisQuality>();

                ret = apis.StartCapture(captureMode, 3, qualityMode, IddkCaptureOperationMode.AutoCapture, eyeSubtype, true, null, null);


                if (ret != IddkResult.OK)
                {
                    /* Remember to deinit camera */
                    /*MessageBox.Show("Failed to start capture. ");*/
                    apis.DeinitCamera();
                    irisShieldTimer.Stop();
                    return;
                }

                /*ret = apis.GetStreamImage(images, out captureStatus);
                if (ret == IddkResult.OK)
                {
                    //TODO/////////////////////////////////////////////////////////////////
                    //
                    // Your code to process stream image.
                    //
                    ///////////////////////////////////////////////////////////////////////
                    ///

                    imgLeft = null;
                    imgRight = null;

                    if (images != null && images.Count > 0)
                    {
                        imgRight = CreateBitmap8bits(images[0].ImageData, images[0].ImageWidth, images[0].ImageWidth);
                        //imgRight.Save("D:/iris_right.jpg", ImageFormat.Jpeg);
                    }

                    if (images != null && images.Count > 1)
                    {
                        imgLeft = CreateBitmap8bits(images[1].ImageData, images[1].ImageWidth, images[1].ImageWidth);
                        //imgLeft.Save("D:/iris_left.jpg", ImageFormat.Jpeg);
                    }

                    bmpright = new Bitmap(imgRight);
                    bmpleft = new Bitmap(imgLeft);

                    this.irisControl.OnGetLeftIris(bmpleft);
                    this.irisControl.OnGetRightIris(bmpright);
                }
                //If the stream image is not allowed by device configuration
                else if (ret == IddkResult.DEV_FunctionDisabled)
                {
                    ret = apis.GetCaptureStatus(out captureStatus);
                }
                else if (ret == IddkResult.SE_NoFrameAvailable)
                {
                    // when GetStreamImage returns SE_NoFrameAvailable, there are 2 possibilities:
                    // 1. The capturing process ended.
                    // 2. The capturing process has not started yet
                    //So try to check capturing status to know which above possiblity is:
                    ret = apis.GetCaptureStatus(out captureStatus);
                }

                *//* If GetStreamImage and GetCaptureStatus cause no error, process the capture status.*//*
                if (ret == IddkResult.OK)
                {
                    if (captureStatus == IddkCaptureStatus.Capturing)
                    {
                        if (!eyeDetected)
                        {
                            logger.Info("Eyes are detected.");
                            eyeDetected = true;
                        }
                    }
                    else if (captureStatus == IddkCaptureStatus.Complete)
                    {
                        *//* capture has finished *//*
                    }
                    else if (captureStatus == IddkCaptureStatus.Abort)
                    {
                        *//* capture has been aborted *//*
                    }
                }

                *//* Try to stop capturing for sure even though it might be stopped *//*
                ret = apis.StopCapture();
                if (ret != IddkResult.OK)
                {
                    apis.DeinitCamera();
                    return;
                }*/

                bool bRun = true;
                bool enableStream = true;
                Console.WriteLine("\tScanning for eyes");
                while (bRun)
                {
                    if (enableStream)
                    {
                        res = apis.GetStreamImage(images, out captureStatus);
                        if (res == IddkResult.OK)
                        {
                            imgLeft = null;
                            imgRight = null;
                            /* Monocular Device: only one image returned */
                            if (images.Count == 1)
                            {
                                /*Process for stream image with unknown eye side.*/
                            }

                            else if (images.Count == 2)
                            {
                                /* Check if the image data of right eye is available */
                                if (images[0] != null)
                                {
                                    imgRight = CreateBitmap8bits(images[0].ImageData, images[0].ImageWidth, images[0].ImageWidth);
                                    bmpright = new Bitmap(imgRight);
                                    this.irisControl.OnGetRightIris(bmpright);


                                }
                                /* Check if the image data of left eye is available */
                                if (images[1] != null)
                                {
                                    imgLeft = CreateBitmap8bits(images[1].ImageData, images[1].ImageWidth, images[1].ImageWidth);
                                    bmpleft = new Bitmap(imgLeft);
                                    this.irisControl.OnGetLeftIris(bmpleft);

                                }
                            }
                        }
                        else if (res == IddkResult.DEV_FunctionDisabled)
                        {
                            enableStream = false;
                            res = apis.GetCaptureStatus(out captureStatus);
                        }
                        else if (res == IddkResult.SE_NoFrameAvailable)
                        {
                            res = apis.GetCaptureStatus(out captureStatus);
                        }
                    }
                    else
                    {
                        res = apis.GetCaptureStatus(out captureStatus);
                    }
                    /* If GetStreamImage and GetCaptureStatus cause no error, process the 
                   capture status.*/
                    if (res == IddkResult.OK)
                    {
                        if (captureStatus == IddkCaptureStatus.Capturing)
                        {
                            /* Do something when camera detects your eye */
                            Console.WriteLine("\n\tEye detected\n");
                        }
                        else if (captureStatus == IddkCaptureStatus.Complete)
                        {
                            /* Capture process has finished successful with the best 
                           iris images are now available in the camera device */
                            bRun = false;
                        }
                        else if (captureStatus == IddkCaptureStatus.Abort)
                        {
                            /* Capture process has been aborted */
                            Console.WriteLine("\n\tCapture aborted\n");
                            bRun = false;
                        }
                        else
                        {
                            Console.Write(".");
                        }
                    }
                    else
                    {
                        /* handle error and terminate this capture */
                        bRun = false;
                    }
                }
                apis.DeinitCamera();
                ret = apis.StopCapture();
                if (ret != IddkResult.OK)
                {
                    apis.DeinitCamera();
                }
                IddkImageKind imageKind = IddkImageKind.K1;
                IddkImageFormat imageFormat = IddkImageFormat.MonoRaw;
                res = apis.GetResultImage(imageKind, imageFormat, 0, images);
                ret = apis.GetResultQuality(qualities);
                if (ret == IddkResult.OK)
                {
                    if (qualities.Count == 1)
                    {
                        // monocular device model
                        logger.Info("Quality of the current captured image:\n\t1. Total score: {0:D}\n\t2. Usable area: {1:D}\n", qualities[0].TotalScore, qualities[0].UsableArea);
                    }
                    else
                    {
                        // binocular device model
                        logger.Info("Quality of the current captured images:\n\t");
                        logger.Info("1. Total score of right eye: {0:D}\n\t2. Usable area of right eye: {1:D}\n\t", qualities[0].TotalScore, qualities[0].UsableArea);
                        logger.Info("3. Total score of left eye: {0:D}\n\t4. Usable area of left eye: {1:D}\n", qualities[1].TotalScore, qualities[1].UsableArea);
                    }
                }
                else if (ret == IddkResult.SE_LeftFrameUnqualified)
                {
                    // binocular device model
                    logger.Info("Left eye image is not qualified. Quality of the current captured image:\n\t");
                    logger.Info("1. Total score of right eye: {0:D}\n\t2. Usable area of right eye: {1:D}\n", qualities[0].TotalScore, qualities[0].UsableArea);
                }
                else if (ret == IddkResult.SE_RightFrameUnqualified)
                {
                    // binocular device model
                    logger.Info("Right eye image is not qualified. Quality of the current captured image:\n\t");
                    logger.Info("1. Total score of left eye: {0:D}\n\t2. Usable area of left eye: {1:D}\n", qualities[1].TotalScore, qualities[1].UsableArea);
                }
                else
                {
                    apis.DeinitCamera();
                    return;
                }

                if (captureStatus == IddkCaptureStatus.Complete)
                {
                    /* Now we have time to get the result image */
                    irisShieldTimer.Stop();

                    /*imgLeft = null;
                    imgRight = null;

                    var imageList = GetResultImageList();

                    if (imageList[0] != null)
                    {
                        imgRight = CreateBitmap8bits(images[0].ImageData, images[0].ImageWidth, images[0].ImageWidth);
                        bmpright = new Bitmap(imgRight);
                        this.irisControl.OnGetRightIris(bmpright);


                    }
                    *//* Check if the image data of left eye is available *//*
                    if (imageList[1] != null)
                    {
                        imgLeft = CreateBitmap8bits(images[1].ImageData, images[1].ImageWidth, images[1].ImageWidth);
                        bmpleft = new Bitmap(imgLeft);
                        this.irisControl.OnGetLeftIris(bmpleft);

                    }*/

                    /*if (imageList != null && imageList.Count > 0)
                    {
                        imgRight = CreateBitmap8bits(imageList[0].ImageData, imageList[0].ImageWidth, imageList[0].ImageWidth);
                    }

                    if (imageList != null && imageList.Count > 1)
                    {
                        imgLeft = CreateBitmap8bits(imageList[1].ImageData, imageList[1].ImageWidth, imageList[1].ImageWidth);
                    }*/

                    /*bmpright = new Bitmap(imgRight);
                    bmpleft = new Bitmap(imgLeft);

                    this.irisControl.OnGetLeftIris(bmpleft);
                    this.irisControl.OnGetRightIris(bmpright);*/
                }
            }
            catch (Exception ex)
            {

                logger.Error("There was an unexpected error when streaming iris by camera (IriShield). Error Message: \n" + ex.ToString());
                MessageBox.Show("Unable to open device.");
            }
        }

        private void OnStreamingIrisV1(Object obj, EventArgs args)
        {
            try
            {
                Image imgLeft = null;
                Image imgRight = null;

                Bitmap bmpright = null;
                Bitmap bmpleft = null;

                /* For streaming images */
                List<IddkImage> images = new List<IddkImage>();

                IddkResult res = new IddkResult();

                /* Parameters for capturing */
                IddkCaptureMode captureMode = IddkCaptureMode.TimeBased;
                IddkQualityMode qualityMode = IddkQualityMode.Normal;
                IddkEyeSubtype eyeSubtype = IddkEyeSubtype.Both;

                /* Other params */
                IddkResult ret = IddkResult.OK;
                IddkCaptureStatus captureStatus = IddkCaptureStatus.Idle;
                IddkDeviceConfig deviceConfig = new IddkDeviceConfig();
                List<IddkIrisQuality> qualities = new List<IddkIrisQuality>();

                ret = apis.StartCapture(captureMode, 3, qualityMode, IddkCaptureOperationMode.AutoCapture, eyeSubtype, true, null, null);
                if (ret != IddkResult.OK)
                {
                    /* Remember to deinit camera */
                    irisShieldTimer.Stop();
                    MessageBox.Show("Failed to start capture. ");
                    apis.DeinitCamera();
                    return;
                }

                res = apis.GetStreamImage(images, out captureStatus);
                if (res == IddkResult.OK)
                {
                    imgLeft = null;
                    imgRight = null;
                    /* Monocular Device: only one image returned */
                    if (images.Count == 1)
                    {
                        /*Process for stream image with unknown eye side.*/
                    }
                    else if (images.Count == 2)
                    {
                        /* Check if the image data of right eye is available */
                        if (images[0] != null)
                        {
                            imgRight = CreateBitmap8bits(images[0].ImageData, images[0].ImageWidth, images[0].ImageWidth);
                            bmpright = new Bitmap(imgRight);
                            this.irisControl.OnGetRightIris(bmpright);
                        }
                        /* Check if the image data of left eye is available */
                        if (images[1] != null)
                        {
                            imgLeft = CreateBitmap8bits(images[1].ImageData, images[1].ImageWidth, images[1].ImageWidth);
                            bmpleft = new Bitmap(imgLeft);
                            this.irisControl.OnGetLeftIris(bmpleft);
                        }
                    }
                }
                else if (res == IddkResult.DEV_FunctionDisabled)
                {
                    res = apis.GetCaptureStatus(out captureStatus);
                }
                else if (res == IddkResult.SE_NoFrameAvailable)
                {
                    res = apis.GetCaptureStatus(out captureStatus);
                }

                /* If GetStreamImage and GetCaptureStatus cause no error, process the 
                   capture status.*/
                if (res == IddkResult.OK)
                {
                    if (captureStatus == IddkCaptureStatus.Capturing)
                    {
                        /* Do something when camera detects your eye */
                        Console.WriteLine("\n\tEye detected\n");

                        imgLeft = null;
                        imgRight = null;
                        /* Monocular Device: only one image returned */
                        if (images.Count == 1)
                        {
                            /*Process for stream image with unknown eye side.*/
                        }

                        else if (images.Count == 2)
                        {
                            irisShieldTimer.Stop();

                            /* Check if the image data of right eye is available */
                            if (images[0] != null)
                            {
                                imgRight = CreateBitmap8bits(images[0].ImageData, images[0].ImageWidth, images[0].ImageWidth);
                                bmpright = new Bitmap(imgRight);
                                this.irisControl.OnGetRightIris(bmpright);
                            }
                            /* Check if the image data of left eye is available */
                            if (images[1] != null)
                            {
                                imgLeft = CreateBitmap8bits(images[1].ImageData, images[1].ImageWidth, images[1].ImageWidth);
                                bmpleft = new Bitmap(imgLeft);
                                this.irisControl.OnGetLeftIris(bmpleft);
                            }
                            this.irisControl.OnComplete(true);
                        }
                    }
                    else if (captureStatus == IddkCaptureStatus.Complete)
                    {
                        /* Capture process has finished successful with the best 
                       iris images are now available in the camera device */
                        irisShieldTimer.Stop();
                    }
                    else if (captureStatus == IddkCaptureStatus.Abort)
                    {
                        /* Capture process has been aborted */
                        Console.WriteLine("\n\tCapture aborted\n");
                    }
                }
                else
                {
                    /* handle error and terminate this capture */
                    logger.Error("Failed to stream.");
                }

                if (ret != IddkResult.OK)
                {
                    apis.DeinitCamera();
                }

                IddkImageKind imageKind = IddkImageKind.K1;
                IddkImageFormat imageFormat = IddkImageFormat.MonoRaw;
                res = apis.GetResultImage(imageKind, imageFormat, 0, images);
                ret = apis.GetResultQuality(qualities);
                if (ret == IddkResult.OK)
                {
                    if (qualities.Count == 1)
                    {
                        // monocular device model
                        logger.Info("Quality of the current captured image:\n\t1. Total score: {0:D}\n\t2. Usable area: {1:D}\n", qualities[0].TotalScore, qualities[0].UsableArea);
                    }
                    else
                    {
                        // binocular device model
                        logger.Info("Quality of the current captured images:\n\t");
                        logger.Info("1. Total score of right eye: {0:D}\n\t2. Usable area of right eye: {1:D}\n\t", qualities[0].TotalScore, qualities[0].UsableArea);
                        logger.Info("3. Total score of left eye: {0:D}\n\t4. Usable area of left eye: {1:D}\n", qualities[1].TotalScore, qualities[1].UsableArea);
                    }
                }
                else if (ret == IddkResult.SE_LeftFrameUnqualified)
                {
                    // binocular device model
                    logger.Info("Left eye image is not qualified. Quality of the current captured image:\n\t");
                    logger.Info("1. Total score of right eye: {0:D}\n\t2. Usable area of right eye: {1:D}\n", qualities[0].TotalScore, qualities[0].UsableArea);
                }
                else if (ret == IddkResult.SE_RightFrameUnqualified)
                {
                    // binocular device model
                    logger.Info("Right eye image is not qualified. Quality of the current captured image:\n\t");
                    logger.Info("1. Total score of left eye: {0:D}\n\t2. Usable area of left eye: {1:D}\n", qualities[1].TotalScore, qualities[1].UsableArea);
                }
            }
            catch (Exception ex)
            {
                //this.irisControl.OnComplete(true);
                irisShieldTimer.Stop();
                logger.Error("There was an unexpected error when streaming iris by camera (IriShield). Error Message: \n" + ex.ToString());
                //MessageBox.Show("Unable to open device.");
            }
        }
    }
}
