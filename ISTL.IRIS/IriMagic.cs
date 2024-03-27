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

namespace ISTL.IRIS
{
    public class IriMagic : IIrisEngine
    {
        private IriCAMMResult _captureResult = IriCAMMResult.UnknownError;
        private CaptureSetting _captureSetting = new CaptureSetting();
        IIrisControl irisControl;
        ImageCtrl leftCtrl;
        ImageCtrl rightCtrl;
        private System.Windows.Forms.Timer irisStreamingCallbackTimer;
        private Logger logger = LogManager.GetCurrentClassLogger();

        public IriMagic(IIrisControl irisControl, ImageCtrl leftCtrl, ImageCtrl rightCtrl)
        {
            this.irisControl = irisControl;
            this.leftCtrl = leftCtrl;
            this.rightCtrl = rightCtrl;
            this.leftCtrl.Tag = "LeftEyeWnd";
            this.rightCtrl.Tag = "RightEyeWnd";

            irisStreamingCallbackTimer = new Timer();
            irisStreamingCallbackTimer.Interval = 66; // As per sdk
            irisStreamingCallbackTimer.Tick += new EventHandler(OnStreamingIris);

        }
        
        public void IrisMagicStartCapture()
        {
            try
            {
                /*ShowHourglassCursor();*/
                _captureResult = IrisMagicHelper.CaptureImage();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Iris Magic Start Capture Exception: " + ex);
            }
            finally
            {
                /*ReleaseHourglassCursor();*/

            }
        }
        private void UpdateCaptureResult()
        {
            uint deviceVersion = 0;
            _captureResult = IriMagicBino.GetDeviceVersion(out deviceVersion);
        }

        private void OnStreamingTimer()
        {
            try
            {
                // Get streaming pictures from the camera
                Image imgLeft, imgRight;
                IriCAMMStatus curStatus;
                IriCAMMImageQualityMeasurement leftQuality, rightQuality;
                _captureResult = IrisMagicHelper.GetAndProcessImage(out imgLeft, out imgRight, out curStatus, out leftQuality, out rightQuality);
                Bitmap bmpright = new Bitmap(imgRight);
                Bitmap bmpleft = new Bitmap(imgRight);
                this.irisControl.OnGetLeftIris(bmpleft);
                this.irisControl.OnGetRightIris(bmpright);

                if (IrisMagicHelper.IsStopCapture)
                {
                    //MessageBox.Show("Image Captured");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception on on stream timer: " + ex);
            }
        }

        public override void CloseDevice()
        {
            IrisMagicHelper.DeinitAndCloseDevice();

            if(irisStreamingCallbackTimer != null)
            {
                irisStreamingCallbackTimer.Stop();
                irisStreamingCallbackTimer = null;
            }
        }

        public override bool OpenDevice(string pos)
        {
            try
            {
                IrisMagicHelper.InitCameraSetting(_captureSetting);
                _captureResult = IrisMagicHelper.TurnOnAndOpenDevice();
                if (_captureResult == IriCAMMResult.Ok)

                {
                    //MessageBox.Show("Turned On Suceessfully");
                    StartCapture(pos);

                    return true;
                }
                else
                {
                    //MessageBox.Show("Unable to open device.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Exception: " + " " + ex);
                logger.Error("There was an unexpected error when opening iris scanner (IriMagic): " + ex.ToString());
            }
            return false;
        }


        // Previous code blocked by Al-Amin on 26 Sep 2021

        //public override void StartCapture(string pos)
        //{
        //    IrisMagicStartCapture();

        //    while (true)
        //    {
        //        OnStreamingTimer();
        //        if (_captureResult != IriCAMMResult.Ok || IrisMagicHelper.IsStopCapture)
        //        {
        //            UpdateCaptureResult();
        //            IrisMagicHelper.DeinitAndCloseDevice();
        //            break;
        //        }

        //    }
        //}


        public override void StartCapture(string pos)
        {
            OnStartCapture();
        }

        // Added by Al-Amin on 26 Sep 2021
        private void OnStartCapture()
        {
            try
            {
                _captureResult = IrisMagicHelper.CaptureImage();
                if (_captureResult == IriCAMMResult.Ok)
                {
                    irisStreamingCallbackTimer.Start();
                }
                else
                {
                    MessageBox.Show("Upable to open device.");
                }
            }
            catch(Exception ex)
            {

                MessageBox.Show("Upable to open device.");
                logger.Error("There was an unexpected error when starting iris capture (IriMagic). Error Message: \n" + ex.ToString());
                if (irisStreamingCallbackTimer != null) irisStreamingCallbackTimer.Stop();
            }
        }

        private void OnStreamingIris(Object obj, EventArgs args)
        {
            try
            {               
                Image imgLeft, imgRight;
                IriCAMMStatus curStatus;
                IriCAMMImageQualityMeasurement leftQuality, rightQuality;
                _captureResult = IrisMagicHelper.GetAndProcessImage(out imgLeft, out imgRight, out curStatus, out leftQuality, out rightQuality);

                Bitmap bmpright = new Bitmap(imgRight);
                Bitmap bmpleft = new Bitmap(imgLeft);

                this.irisControl.OnGetLeftIris(bmpleft);
                this.irisControl.OnGetRightIris(bmpright);

                if (IrisMagicHelper.IsStopCapture)
                {
                    irisStreamingCallbackTimer.Stop();

                    bmpright = new Bitmap(imgRight);
                    bmpleft = new Bitmap(imgLeft);

                    this.irisControl.OnGetLeftIris(bmpleft);
                    this.irisControl.OnGetRightIris(bmpright);
                    this.irisControl.OnComplete(true);
                }
            }
            catch (Exception ex)
            {

                irisStreamingCallbackTimer.Stop();
                //this.irisControl.OnComplete(true);
                logger.Error("There was an unexpected error when streaming iris by camera (IriMagic). Error Message: \n" + ex.ToString());
                //MessageBox.Show("Upable to open device.");
            }
        }
    }
}
