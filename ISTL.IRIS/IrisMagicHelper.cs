using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using IriMagicBinoSDK;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;


namespace ISTL.IRIS
{
    class IrisMagicHelper
    {
        #region Private
        private static CaptureSetting _captureSetting = null;
        private static int _imageWidth = 0;
        private static int _imageHeight = 0;

        #endregion

        #region Public
        public static volatile bool IsStopCapture = false;

        #endregion

        public static Image CreateBitmap8bits(byte[] pRawBuffer, int nWidth, int nHeight)
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

        /// <summary>
        /// Set setting for camera to get iris images
        /// </summary>
        /// <param name="setting"></param>
        /// 
        public static void InitCameraSetting(CaptureSetting setting)
        {
            _captureSetting = setting;
        }

        //public static void HandleError(IriCAMMResult resultCode)
        //{
        //    string errorMessage = ErrorHandler.GetErrorMessage(resultCode);
        //}

        /// <summary>
        /// Stop capture and deinit device
        /// </summary>
        /// <returns></returns>
        public static IriCAMMResult AutoStopCapture()
        {
            IsStopCapture = true;

            // 1. StopCapture
            IriCAMMResult errorCode = IriMagicBino.StopCapture();
            if (errorCode != IriCAMMResult.Ok)
            {
                return errorCode;
            }

            // 2. DeinitDevice
            errorCode = IriMagicBino.DeinitDevice();

            return errorCode;
        }

        /// <summary>
        /// Get result image after finishing capture
        /// </summary>
        /// <param name="leftImage"></param>
        /// <param name="rightImage"></param>
        /// <returns></returns>
        public static IriCAMMResult ProcessStopJobs(out Image leftImage, out Image rightImage,
                    out IriCAMMImageQualityMeasurement leftQuality, out IriCAMMImageQualityMeasurement rightQuality)
        {
            leftImage = rightImage = null;
            leftQuality = new IriCAMMImageQualityMeasurement();
            rightQuality = new IriCAMMImageQualityMeasurement();

            IriCAMMImage pLeftIriCAMMImage;
            IriCAMMImage pRightIriCAMMImage;
            IriCAMMRawImage pLeftImage;
            pLeftImage.ImageData = null;
            IriCAMMRawImage pRightImage;
            pRightImage.ImageData = null;

            bool isLeftFlashExistInIrisArea = false;
            bool isRightFlashExistInIrisArea = false;
            IriCAMMResult resultCode;
            resultCode = IriMagicBino.GetResultImageWithQualityMeasurement(out pLeftIriCAMMImage, out pRightIriCAMMImage,
                     out isLeftFlashExistInIrisArea, out isRightFlashExistInIrisArea, out leftQuality, out rightQuality);

            if (resultCode == IriCAMMResult.Ok || resultCode == IriCAMMResult.SE_FrameLeftNoQualifiedFrame
                || resultCode == IriCAMMResult.SE_FrameRightNoQualifiedFrame)
            {
                DateTime CurrTime = DateTime.Now;
                // string timestring = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string timestring = "IRIS";
                string strFileName;
                if (resultCode == IriCAMMResult.Ok)
                {
                    resultCode = IriMagicBino.GetRawImage(pLeftIriCAMMImage, out pLeftImage);
                    if (resultCode != IriCAMMResult.Ok)
                    {
                        goto Exit;
                    }
                    resultCode = IriMagicBino.GetRawImage(pRightIriCAMMImage, out pRightImage);
                    if (resultCode != IriCAMMResult.Ok)
                    {
                        goto Exit;
                    }
                    strFileName = timestring + "_Left.bmp";
                    Image image =CreateBitmap8bits(pLeftImage.ImageData, pLeftImage.ImageWidth, pLeftImage.ImageHeight);
                    leftImage = image;
                    image.Save(strFileName, System.Drawing.Imaging.ImageFormat.Bmp);
                    strFileName = timestring + "_Right.bmp";
                    image =CreateBitmap8bits(pRightImage.ImageData, pRightImage.ImageWidth, pRightImage.ImageHeight);
                    rightImage = image;
                    image.Save(strFileName, System.Drawing.Imaging.ImageFormat.Bmp);
                }
                else if (resultCode == IriCAMMResult.SE_FrameLeftNoQualifiedFrame)
                {
                    resultCode = IriMagicBino.GetRawImage(pRightIriCAMMImage, out pRightImage);
                    if (resultCode == IriCAMMResult.Ok)
                    {
                        strFileName = timestring + "_Right.bmp";
                        Image image =CreateBitmap8bits(pRightImage.ImageData, pRightImage.ImageWidth, pRightImage.ImageHeight);
                        image.Save(strFileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        rightImage = image;
                        Array.Resize(ref pLeftImage.ImageData, 640 * 480);
                        Array.Clear(pLeftImage.ImageData, 0, 640 * 480);
                        leftImage =CreateBitmap8bits(pLeftImage.ImageData, 640, 480);
                    }
                    else
                    {
                        goto Exit;
                    }
                }
                else if (resultCode == IriCAMMResult.SE_FrameRightNoQualifiedFrame)
                {
                    resultCode = IriMagicBino.GetRawImage(pLeftIriCAMMImage, out pLeftImage);
                    if (resultCode == IriCAMMResult.Ok)
                    {
                        strFileName = timestring + "_Left.bmp";
                        Image image = CreateBitmap8bits(pLeftImage.ImageData, pLeftImage.ImageWidth, pLeftImage.ImageHeight);
                        image.Save(strFileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        leftImage = image;
                        Array.Resize(ref pRightImage.ImageData, 640 * 480);
                        Array.Clear(pRightImage.ImageData, 0, 640 * 480);
                        rightImage = CreateBitmap8bits(pRightImage.ImageData, 640, 480);
                    }
                    else
                    {
                        goto Exit;
                    }
                }
            }
            else
            {
                Array.Resize(ref pLeftImage.ImageData, 640 * 480);
                Array.Clear(pLeftImage.ImageData, 0, 640 * 480);
                leftImage =CreateBitmap8bits(pLeftImage.ImageData, 640, 480);
                Array.Resize(ref pRightImage.ImageData, 640 * 480);
                Array.Clear(pRightImage.ImageData, 0, 640 * 480);
                rightImage =CreateBitmap8bits(pRightImage.ImageData, 640, 480);
                goto Exit;
            }
        Exit:
            return resultCode;
        }

        public static IriCAMMResult CaptureImage()
        {
            IriCAMMResult resultCode = IriMagicBino.InitDevice(out _imageWidth, out _imageHeight);
            if (resultCode != IriCAMMResult.Ok)
            {
                return resultCode;
            }
            resultCode = IriMagicBino.StartCapture(_captureSetting.CaptureMode, _captureSetting.QualityMode,
                                                   _captureSetting.OperationMode, _captureSetting.SelectEyeMode,
                                                   _captureSetting.Count, _captureSetting.IsStreamMode,
                                                   _captureSetting.ScaleRatio, _captureSetting.IsAutoLED);

            if (resultCode == IriCAMMResult.Ok)
            {
                IsStopCapture = false;
            }
            else
            {
                IsStopCapture = true;
            }

            return resultCode;
        }

        public static IriCAMMResult GetAndProcessImage(out Image leftImage, out Image rightImage, out IriCAMMStatus curCameraStatus,
                                                       out IriCAMMImageQualityMeasurement leftQuality,
                                                       out IriCAMMImageQualityMeasurement rightQuality)
        {
            leftImage = rightImage = null;
            leftQuality = new IriCAMMImageQualityMeasurement();
            rightQuality = new IriCAMMImageQualityMeasurement();
            curCameraStatus = IriCAMMStatus.Idle;
            IriCAMMResult resultCode = IriCAMMResult.Ok;
            IriCAMMRawImage leftRawImage;
            leftRawImage.ImageData = null;
            IriCAMMRawImage rightRawImage;
            rightRawImage.ImageData = null;
            bool isLeftFlashExistInIrisArea = false;
            bool isRightFlashExistInIrisArea = false;

            resultCode = IriMagicBino.GetStreamImage(out leftRawImage, out rightRawImage, out isLeftFlashExistInIrisArea,
                                                     out isRightFlashExistInIrisArea, out curCameraStatus);
            if (resultCode == IriCAMMResult.Ok)
            {
                // Update the images
                leftImage = CreateBitmap8bits(leftRawImage.ImageData, leftRawImage.ImageWidth, leftRawImage.ImageHeight);
                rightImage =CreateBitmap8bits(rightRawImage.ImageData, rightRawImage.ImageWidth, rightRawImage.ImageHeight);

                // Process status of camera.
                if ((curCameraStatus == IriCAMMStatus.Capturing))
                {
                    // Do nothing because Camera in capturing
                }
                else if (curCameraStatus == IriCAMMStatus.Finish || curCameraStatus == IriCAMMStatus.Timeout)
                {
                    resultCode = ProcessStopJobs(out leftImage, out rightImage, out leftQuality, out rightQuality);
                    resultCode = AutoStopCapture();
                }
                else if (curCameraStatus == IriCAMMStatus.Abort)
                {
                    resultCode = AutoStopCapture();
                }
            }
            else if (resultCode == IriCAMMResult.SE_FrameNotAvailable
                || (resultCode == IriCAMMResult.SE_FrameRightNoQualifiedFrame) || (resultCode == IriCAMMResult.SE_FrameLeftNoQualifiedFrame))
            {
                resultCode = IriMagicBino.GetCaptureStatus(out curCameraStatus);
                if (curCameraStatus == IriCAMMStatus.Finish || curCameraStatus == IriCAMMStatus.Timeout)
                {
                    resultCode = ProcessStopJobs(out leftImage, out rightImage, out leftQuality, out rightQuality);
                    resultCode = AutoStopCapture();
                }
                else if (curCameraStatus == IriCAMMStatus.Abort)
                {
                    resultCode = AutoStopCapture();
                }
            }
            else
            {
                resultCode = AutoStopCapture();
            }

            return resultCode;
        }

        public static IriCAMMResult TurnOnAndOpenDevice()
        {
            IriCAMMResult ret = IriCAMMResult.UnknownError;

            // OpenDevice the get handle 
            ret = IriMagicBino.OpenDevice();

            // Cannot open device
            if (ret != IriCAMMResult.Ok)
            {
                return IriCAMMResult.DeviceNotOpen;
            }

            return ret;
        }

        /// <summary>
        /// Check model of the camera
        /// </summary>
        /// <returns></returns>
        public static IriCAMMResult GetModelCamera(out IriCAMMDeviceInformation productInfo, out string strProductLine,
                                        out string strFocus, out string strModality, out string strSeries)
        {
            productInfo = new IriCAMMDeviceInformation();
            IriCAMMResult ret;
            UInt32 propertyFlag;
            UInt32 productLine;
            UInt32 productFamily;
            UInt32 productModality;
            UInt32 productSerial;

            strProductLine = strFocus = strModality = strSeries = string.Empty;

            productInfo = new IriCAMMDeviceInformation();
            ret = IriMagicBino.GetDeviceInformation(out productInfo);
            if (ret != IriCAMMResult.Ok)
            {
                return ret;
            }

            propertyFlag = productInfo.PropertyFlag;

            /************************************************************************/
            /* Bit flag structure of device properties                              */
            /************************************************************************/
            /*
            8: Product line (IriCAMMES, IriTerminal,...)
            4: Family/Class (F/A)
            4: Modality (Mono/Bino)
            8: Series (S/C/N)
            8: Reserved
            */

            productLine = propertyFlag >> (4 + 4 + 8 + 8);
            if (productLine == 0)
            {
                strProductLine = "IriCAMMES";

            }
            else if (productLine == 1)
            {
                strProductLine = "IriTerminal";
            }
            else if (productLine == 2)
            {
                strProductLine = "IriMagic";
            }

            productFamily = ((propertyFlag & 0x00FFFFFF) >> (4 + 8 + 8));
            if (productFamily == 0)
            {
                strFocus = "Fixed Focus";
            }
            else
            {
                strFocus = "Auto Focus";
            }
            productModality = (propertyFlag & 0x000FFFFF) >> (8 + 8);
            if (productModality == 0)
            {
                strModality = "Mono";
            }
            else
            {
                strModality = "Bino";
            }
            productSerial = (propertyFlag & 0x0000FFFF) >> 8;
            if (productSerial == 0)
            {
                strSeries = "S";
            }
            else if (productSerial == 1)
            {
                strSeries = "C";
            }
            else if (productSerial == 2)
            {
                strSeries = "N";
            }

            return ret;
        }

        /// <summary>
        /// Start getting the iris image from camera in operater initiated mode
        /// </summary>
        /// <returns></returns>
        public static IriCAMMResult OperateCapture()
        {
            return IriMagicBino.OperateCapture();
        }

        /// <summary>
        /// Force to deinit and close the camera
        /// </summary>
        public static void DeinitAndCloseDevice()
        {
            IriMagicBino.StopCapture();
            IriMagicBino.DeinitDevice();
            IriMagicBino.CloseDevice();
        }

        /// <summary>
        /// Force to stop capturing
        /// </summary>
        public static IriCAMMResult StopCapture()
        {
            return IriMagicBino.StopCapture();
        }

        public static IriCAMMResult GetDeviceVersion(out uint version)
        {
            return IriMagicBino.GetDeviceVersion(out version);
        }

        public static IriCAMMResult GetDeviceInfomation(out IriCAMMDeviceInformation deviceInfo)
        {
            deviceInfo = new IriCAMMDeviceInformation();
            return IriMagicBino.GetDeviceInformation(out deviceInfo);
        }
    }
}
