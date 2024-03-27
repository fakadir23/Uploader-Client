using CrossMatch.BioBaseDotNet;
using CrossMatch.Biometrics.Utils;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISTL.IRIS
{
    public class CmIris : IIrisEngine
    {

        Dictionary<DeviceCategory, BioDeviceCollector> m_Collectors = new Dictionary<DeviceCategory, BioDeviceCollector>();
        //String[] devices = { "I Scan 2 0000118910", "I Scan 2 0000118861" };
        String biometricPos = "IrisBoth";
        String impressionTypeStr = "IrisRegular";
        IIrisControl irisControl;
        ImageCtrl leftCtrl;
        ImageCtrl rightCtrl;
        private Logger logger = LogManager.GetCurrentClassLogger();

        BioDeviceCollector SelectedDeviceCollector
        {

            get
            {
                BioDeviceCollector collector = null;
                try
                {
                    DeviceCategory category = DeviceCategory.IScanEssentials;
                    if (!m_Collectors.TryGetValue(category, out collector))
                    {
                        collector = new BioDeviceCollector(category);
                        m_Collectors.Add(category, collector);
                    }
                }
                catch (BioBaseException ex)
                {

                }

                return collector;
            }

        }

        // Old code blocked by Al-Amin on 21-Sep-21
        //BioDevice SelectedDevice
        //{
        //    get
        //    {
        //        if (devices[0].Length == 0 || SelectedDeviceCollector == null || !SelectedDeviceCollector.Devices.ContainsKey(devices[0]))
        //            return null;

        //        return SelectedDeviceCollector.Devices[devices[0]];
        //    }
        //}

        // Added new code by Al-Amin on 21-Sep-21
        BioDevice SelectedDevice
        {
            get
            {
                BioDevice sDevice = null;
                try
                {
                    foreach(KeyValuePair<string, BioDevice> pair in SelectedDeviceCollector.Devices)
                    {
                        logger.Info("Iris scanner set device :: serial no :: " + pair.Key);
                    }

                    sDevice = SelectedDeviceCollector.Devices[SelectedDeviceCollector.Devices.First().Key];
                    logger.Info("Iris scanner set device :: selected serial no :: " + SelectedDeviceCollector.Devices.First().Key);
                }
                catch(Exception ex)
                {
                    foreach (KeyValuePair<string, BioDevice> pair in SelectedDeviceCollector.Devices)
                    {
                        sDevice = SelectedDeviceCollector.Devices[pair.Key];
                    }

                    logger.Info("Iris scanner set device error: " + ex.ToString());
                }
                return sDevice;
            }
        }

        public CmIris(IIrisControl irisControl, ImageCtrl leftCtrl, ImageCtrl rightCtrl)
        {
            BioDeviceCollector deviceList = SelectedDeviceCollector;
            this.irisControl = irisControl;
            this.leftCtrl = leftCtrl;
            this.rightCtrl = rightCtrl;
            this.leftCtrl.Tag = "LeftEyeWnd";
            this.rightCtrl.Tag = "RightEyeWnd";
        }

        override public void CloseDevice()
        {
            if(SelectedDevice != null)
            {
                SelectedDevice.InitProgress -= new InitProgressEventHandler(SelectedDevice_InitProgress);
                SelectedDevice.AcquistionStart -= new EventHandler(device_AcquistionStart);
                SelectedDevice.AcquisitionComplete -= new EventHandler(device_AcquisitionComplete);
                SelectedDevice.Preview -= new DataAvailableEventHandler(device_Preview);
                SelectedDevice.DataAvailable -= new DataAvailableEventHandler(device_DataAvailable);
                SelectedDevice.Close();
            }
            
        }

        override public bool OpenDevice(string pos)
        {

            try
            {
                if (SelectedDevice == null)
                {
                    //MessageBox.Show("Unable to open device.");
                    return false;
                }

                SelectedDevice.InitProgress += new InitProgressEventHandler(SelectedDevice_InitProgress);
                SelectedDevice.AcquistionStart += new EventHandler(device_AcquistionStart);
                SelectedDevice.AcquisitionComplete += new EventHandler(device_AcquisitionComplete);
                SelectedDevice.Preview += new DataAvailableEventHandler(device_Preview);
                SelectedDevice.DataAvailable += new DataAvailableEventHandler(device_DataAvailable);

                SelectedDevice.Open();

                LayoutVisualizers();
                SelectedDevice.Visualizers["LeftEyeWnd"].DeviceControl = leftCtrl;
                SelectedDevice.Visualizers["RightEyeWnd"].DeviceControl = rightCtrl;

                this.biometricPos = pos;
                this.AquireImage();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "RAB CDMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Please setup iris driver first and try again.", "RAB CDMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        void device_DataAvailable(object sender, DataAvailableEventArgs e)
        {
            BioDevice device = (BioDevice)sender;
            LoadImages(device, e);
            this.irisControl.OnComplete(true);
        }

        public void SetMessage()
        {

        }

        void LoadImages(BioDevice device, DataAvailableEventArgs e)
        {
            SortedList<string, ImageCtrl> visualizerCtrlList = new SortedList<string, ImageCtrl>();
            foreach (Visualizer visualizer in device.Visualizers.Values)
            {
                if (visualizer.DeviceControl.GetType() == typeof(ImageCtrl))
                {
                    ImageCtrl imageCtrl = (ImageCtrl)visualizer.DeviceControl;
                    imageCtrl.ClearImage();
                    // imageCtrl.CaptionUL = string.Empty;
                    visualizerCtrlList.Add(imageCtrl.Tag.ToString(), imageCtrl);
                }
            }

            byte[] bioData = e.GetBiometricData();

            if (bioData.Length == 0)
                return;

            ShowIris(visualizerCtrlList, bioData);


        }

        private void ShowIris(SortedList<string, ImageCtrl> visualizerCtrlList, byte[] bioData)
        {
            CmtIris cmtIris;
            try
            {
                // use the CmtIris object to pull pieces of data from the IIR
                cmtIris = new CmtIris(bioData);
            }
            catch (BioBaseException ex)
            {
                //ShowBioBaseError(ex);
                return;
            }

            try
            {
                BiometricPosition position = (BiometricPosition)TypeDescriptor.GetConverter(typeof(BiometricPosition)).ConvertFromString(biometricPos);
                if ((position == BiometricPosition.IrisBoth) || (position == BiometricPosition.IrisLeft))
                {
                    Bitmap bm = LoadIrisImage(IrisPosition.Left, cmtIris, visualizerCtrlList["LeftEyeWnd"]);
                    this.irisControl.OnGetLeftIris(bm);
                }
                if ((position == BiometricPosition.IrisBoth) || (position == BiometricPosition.IrisRight))
                {
                    Bitmap bm = LoadIrisImage(IrisPosition.Right, cmtIris, visualizerCtrlList["RightEyeWnd"]);
                    this.irisControl.OnGetRightIris(bm);
                }

            }
            finally
            {
                cmtIris.Dispose();
            }
        }

        Bitmap LoadIrisImage(IrisPosition irisPosition, CmtIris cmtIris, ImageCtrl imageCtrl)
        {
            try
            {
                byte[] imageBytes = null;
                int width;
                int height;

                imageBytes = cmtIris.GetRasterImage(irisPosition, out width, out height);
                Bitmap bm = imageCtrl.LoadImage(width, height, 500, ref imageBytes);
                imageCtrl.CenterAndFit(true);
                int quality = cmtIris.GetProperty(irisPosition, IrisProperty.Quality);
                imageCtrl.CaptionUL = string.Format("{0} ({1})", irisPosition.ToString(), quality.ToString());
                return bm;
            }

            catch (BioBaseException)
            {
                string msg = string.Format("No data available for the {0} iris.", irisPosition.ToString());
                //MessageBox.Show(this, msg, "Iris", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (CmtIrisException)
            {
                string msg = string.Format("No data available for the {0} iris.", irisPosition.ToString());
                //MessageBox.Show(this, msg, "Iris", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return null;
        }

        private void LayoutVisualizers()
        {

        }

        void SelectedDevice_InitProgress(object sender, InitProgressEventArgs e)
        {
            float i = e.ProgressValue * 100.0f;
        }

        void device_AcquistionStart(object sender, EventArgs e)
        {
            int x = 0;
        }

        void device_AcquisitionComplete(object sender, EventArgs e)
        {
            int x = 0;
        }

        void device_Preview(object sender, DataAvailableEventArgs e)
        {
            Byte[] bioData = e.GetBiometricData();
            if (e.DataFormat == DataFormat.IIR)
            {
                CmtIris cmtIris;
                cmtIris = new CmtIris(bioData);
                if (bioData.Length != 0)
                {
                    IrisPosition which_eye = cmtIris.GetEyePosition();
                    // LogEvent("Preview event: " + which_eye.ToString());   // generates too many previews in the log

                    // We have valid eye in IIR record.
                    // TODO: Set AnnotatedEye to IrisPosition.Left or IrisPosition.Right
                    //       to annotated/skip the eye during normal capture.
                    IrisPosition AnnotatedEye = IrisPosition.Unknown;
                    if (which_eye == AnnotatedEye)
                    {
                        SelectedDevice.ForceCapture();     // skip to next eye
                    }

                    UpdateLatentSettings();

                }
                cmtIris.Dispose();
            }
            //LogEvent( "Preview event" ); // generates too many previews in the log

            //uncommenting the line below will pop up a viewer with the preview image data
            //ShowImage( e.ImageData, e.Width, e.Height );
        }

        private void UpdateLatentSettings()
        {

        }

        public void AquireImage()
        {
            try
            {
                BiometricPosition position = (BiometricPosition)TypeDescriptor.GetConverter(typeof(BiometricPosition)).ConvertFromString(biometricPos);
                ImpressionType impressionType = (ImpressionType)TypeDescriptor.GetConverter(typeof(ImpressionType)).ConvertFromString(impressionTypeStr);

                // TODO: Check if position and impressionType are valid for this scanner.

                // refresh the visualizers in case their size or position has changed on the screen
                // to assure the "live" images are drawn in the correct location
                foreach (Visualizer visualizer in SelectedDevice.Visualizers.Values)
                {
                    visualizer.RefreshVisualizer();
                }

                SelectedDevice.Acquire(position, impressionType);
            }
            catch (Exception ex)
            {

            }


        }

        public override void StartCapture(string pos)
        {
            this.biometricPos = pos;
            LayoutVisualizers();
            SelectedDevice.Visualizers["LeftEyeWnd"].DeviceControl = leftCtrl;
            SelectedDevice.Visualizers["RightEyeWnd"].DeviceControl = rightCtrl;
            AquireImage();
        }
    }
}
