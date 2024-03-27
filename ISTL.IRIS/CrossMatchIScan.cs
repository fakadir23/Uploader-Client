using CrossMatch.IScan3BioBaseApi;
using CrossMatch.IScan3Transcoder;
using CrossMatch.IScan3Transcoder.Iris;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISTL.IRIS
{
    public class CrossMatchIScan : IIrisEngine
    {
        private IseBioBase iseBioBase;
        BioBaseDeviceInfo[] bbdInfo;
        BioBaseDeviceInfo bbdInfoCurrent = null;
        IIseBioBaseDevice ibbd = null;
        IIrisControl irisControl;
        ImageCtrl leftCtrl;
        ImageCtrl rightCtrl;
        System.Drawing.Bitmap leftBmp;
        System.Drawing.Bitmap rightBmp;
        string biometricPos = "IrisBoth";
        private Logger logger = LogManager.GetCurrentClassLogger();

        public CrossMatchIScan(IIrisControl irisControl, ImageCtrl leftCtrl, ImageCtrl rightCtrl)
        {
            this.irisControl = irisControl;
            this.leftCtrl = leftCtrl;
            this.rightCtrl = rightCtrl;
            this.leftCtrl.Tag = "LeftEyeWnd";
            this.rightCtrl.Tag = "RightEyeWnd";
        }

        BioBaseDeviceInfo SelectedDevice
        {
            get
            {
                bbdInfoCurrent = null;
                try
                {
                    iseBioBase = new IseBioBase();
                    iseBioBase.Open();
                    Thread.Sleep(2000);
                    BioBaseDeviceInfo[] bbdInfo = null;
                    bbdInfo = iseBioBase.ConnectedDevices;
                    if (bbdInfo != null && bbdInfo.Length > 0)
                    {
                        bbdInfoCurrent = bbdInfo[0];
                    }
                }
                catch (Exception ex)
                {
                    logger.Info("CrossMatch IScan 3 Iris scanner set device error: " + ex.ToString());
                }

                return bbdInfoCurrent;
            }
        }

        public override void CloseDevice()
        {
            IScanCloseDevice();

            //=====================================================
            //      We need to now wait for all the Events that were 
            //      generated because of BioBase callback functions to
            //      finish. All of the Event handling functions that are 
            //      processing BioBase data must return to the SDK
            //      so that it can be disposed. This must be done before
            //      the device is closed.
            //====================================================
            //Application.DoEvents();

            if (iseBioBase != null)
            {
                Thread.Sleep(2000);
                iseBioBase.Dispose();
            }
            iseBioBase = null;
        }

        private void IScanCloseDevice()
        {
            if (ibbd != null)
            {
                try
                {
                    IScanCancelAcquire();
                    if (ibbd.IsDeviceOpen())
                    {
                        ibbd.Dispose();
                    }
                    callbackSetup(false);
                }
                catch (Exception e)
                {
                    //writeLogStatus("CloseDevice Dispose error " + e.Message + Environment.NewLine);
                }
            }
            ibbd = null;
        }

        private void IScanCancelAcquire()
        {
            if (ibbd != null)
                if (ibbd.IsDeviceOpen())
                    if (ibbd.IsDeviceAcquiring())
                        ibbd.CancelAcquisition();
        }

        public override bool OpenDevice(string pos)
        {
            this.biometricPos = pos;
            if (SelectedDevice == null)
            {
                MessageBox.Show("Unable to open device.");
                return false;
            }

            ibbd = (IIseBioBaseDevice)iseBioBase.OpenDevice(bbdInfoCurrent);
            callbackSetup(true);
            this.IScanAcquire();
            return true;
        }

        private void IScanAcquire()
        {
            string strPosType = this.biometricPos;
            if (this.biometricPos == "IrisBoth")
            {
                strPosType = BioBase_Const_DeviceProp.PositionType_BothIris;
            }
            else if (this.biometricPos == "IrisRight")
            {
                strPosType = BioBase_Const_DeviceProp.PositionType_RightIris;
            }
            else if (this.biometricPos == "IrisLeft")
            {
                strPosType = BioBase_Const_DeviceProp.PositionType_LeftIris;
            }
            ibbd.BeginAcquisitionProcess(strPosType, BioBase_Const_DeviceProp.ImpressionType_IrisRegular);
        }

        private void callbackSetup(bool enable)
        {
            if (enable)
            {
                ibbd.AcquisitionStart += new EventHandler<BioBaseAcquisitionStartEventArgs>(IrisEventHandler);
                ibbd.AcquisitionComplete += new EventHandler<BioBaseAcquisitionCompleteEventArgs>(IrisEventHandler);
                ibbd.DataAvailable += new EventHandler<BioBaseDataAvailableEventArgs>(IrisEventHandler);
                ibbd.DeviceError += new EventHandler<BioBaseDeviceErrorEventArgs>(IrisEventHandler);
                ibbd.Preview += new EventHandler<BioBasePreviewEventArgs>(IrisEventHandler);
            }
            else
            {
                ibbd.AcquisitionStart -= new EventHandler<BioBaseAcquisitionStartEventArgs>(IrisEventHandler);
                ibbd.AcquisitionComplete -= new EventHandler<BioBaseAcquisitionCompleteEventArgs>(IrisEventHandler);
                ibbd.DataAvailable -= new EventHandler<BioBaseDataAvailableEventArgs>(IrisEventHandler);
                ibbd.DeviceError -= new EventHandler<BioBaseDeviceErrorEventArgs>(IrisEventHandler);
                ibbd.Preview += new EventHandler<BioBasePreviewEventArgs>(IrisEventHandler);
            }
        }

        private void IrisEventHandler(object sender, BioBaseInitProgressEventArgs arg)
        {
        }

        private void IrisEventHandler(object sender, BioBaseAcquisitionStartEventArgs arg)
        {
        }

        private void IrisEventHandler(object sender, BioBaseAcquisitionCompleteEventArgs arg)
        {
        }

        private void IrisEventHandler(object sender, BioBaseImageDataAvailableEventArgs arg)
        {
            ReadOnlyCollection<BioBaseImageData> imageData = arg.ImageData;            
        }
        private void IrisEventHandler(object sender, BioBaseDataAvailableEventArgs arg)
        {
            if (arg is BioBaseImageDataAvailableEventArgs)
            {
                BioBaseImageDataAvailableEventArgs a = (BioBaseImageDataAvailableEventArgs)arg;
                if (true)
                {
                    IseBioBasePreviewEventArgs data = new IseBioBasePreviewEventArgs(arg.Device, arg.GetIsoData(), arg.DataFormat, arg.IsFinal);

                    switch (arg.DataFormat)
                    {
                        case BioBaseDataFormat.IIR:
                            PreviewIIR(data);
                            this.irisControl.OnComplete(true);
                            break;
                        case BioBaseDataFormat.BMP:
                            PreviewBMP(data);
                            this.irisControl.OnComplete(true);
                            break;
                    }
                }

                //stateAcquireCompleteWithData();
            }
            else if (arg is BioBaseDataAvailableEventArgs)
            {
                //stateAcquireComplete();
            }
        }

        private void PreviewIIR(IseBioBasePreviewEventArgs data)
        {
            IImageTranscoder xc = IrisTranscoder.Transcoder;
            IImageTranscodeable xci = xc.Deserialize(data.GetIsoData());
            List<IBiometricCharacteristic> bcs = xci.GetContent();
            foreach (IBiometricCharacteristic bc in bcs)
            {
                if (bc.Position == 0x02)
                {
                    leftBmp = xci.GenerateBitmap(bc);
                }
                else if (bc.Position == 0x01)
                {
                    rightBmp = xci.GenerateBitmap(bc);
                }
            }

            try
            {
                if(this.biometricPos == "IrisBoth" || this.biometricPos == "IrisLeft")
                {
                    this.irisControl.OnGetLeftIris(leftBmp);
                }

                if (this.biometricPos == "IrisBoth" || this.biometricPos == "IrisRight")
                {
                    this.irisControl.OnGetRightIris(rightBmp);
                }
            }
            catch (Exception e)
            {
            }
        }

        private void PreviewBMP(IseBioBasePreviewEventArgs data)
        {
            // we got one bitmap containing both eyes
            MemoryStream bmpData = new MemoryStream(data.GetIsoData());
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(bmpData);
            System.Drawing.Rectangle leftRegion = new System.Drawing.Rectangle(image.Width / 2, 0, image.Width / 2, image.Height);
            System.Drawing.Rectangle rightRegion = new System.Drawing.Rectangle(0, 0, image.Width / 2, image.Height);
            leftBmp = image.Clone(leftRegion, PixelFormat.Format8bppIndexed);
            rightBmp = image.Clone(rightRegion, PixelFormat.Format8bppIndexed);
            try
            {
                if (this.biometricPos == "IrisBoth" || this.biometricPos == "IrisLeft")
                {
                    this.irisControl.OnGetLeftIris(leftBmp);
                }

                if (this.biometricPos == "IrisBoth" || this.biometricPos == "IrisRight")
                {
                    this.irisControl.OnGetRightIris(rightBmp);
                }
            }
            catch (Exception e)
            {
                //writeLogStatus("Preview error " + e.Message + Environment.NewLine);
            }
        }

        private void IrisEventHandler(object sender, BioBasePreviewEventArgs arg)
        {
            if (sender is IseBioBaseDevice)
            {
                IseBioBaseDevice dev = (IseBioBaseDevice)sender;
            }
            if (arg is IseBioBasePreviewEventArgs)
            {
                IseBioBasePreviewEventArgs data = (IseBioBasePreviewEventArgs)arg;
                switch (data.DataFormat)
                {
                    case BioBaseDataFormat.IIR:
                        PreviewIIR(data);
                        break;
                    case BioBaseDataFormat.BMP:
                        PreviewBMP(data);
                        break;
                }
            }
        }

        private void IrisEventHandler(object sender, BioBaseDeviceErrorEventArgs arg)
        {
            //writeLogStatus("Error Event " + Environment.NewLine);
        }

        public override void StartCapture(string pos)
        {
            throw new NotImplementedException();
        }
    }
}
