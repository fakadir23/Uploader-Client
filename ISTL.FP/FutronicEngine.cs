using Futronic.SDKHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISTL.FP
{
    public class FutronicEngine : IFpEngine
    {
        const string kCompanyName = "Futronic";
        const string kProductName = "SDK 4.0";
        const string kDbName = "DataBaseNet";
        int count = 0;
        /// <summary>
        /// This delegate enables asynchronous calls for setting
        /// the text property on a status control.
        /// </summary>
        /// <param name="text"></param>
        delegate void SetTextCallback(string text);

        /// <summary>
        /// This delegate enables asynchronous calls for setting
        /// the text property on a identification limit control.
        /// </summary>
        /// <param name="text"></param>
        delegate void SetIdentificationLimitCallback(int limit);

        /// <summary>
        /// This delegate enables asynchronous calls for setting
        /// the Image property on a PictureBox control.
        /// </summary>
        /// <param name="hBitmap">the instance of Bitmap class</param>
        delegate void SetImageCallback(Bitmap hBitmap);

        /// <summary>
        /// This delegate enables asynchronous calls for setting
        /// the Enable property on a buttons.
        /// </summary>
        /// <param name="bEnable">true to enable buttons, otherwise to disable</param>
        delegate void EnableControlsCallback(bool bEnable);

        /// <summary>
        /// Contain reference for current operation object
        /// </summary>
        public FutronicSdkBase m_Operation;

        private bool m_bExit;

        /// <summary>
        /// The type of this parameter is depending from current operation. For
        /// enrollment operation this is DbRecord.
        /// </summary>
        public Object m_OperationObj;
        IFpControl fpControl;

        /// <summary>
        /// A directory name to write user's information.
        /// </summary>
        private String m_DatabaseDir;

        private bool m_bInitializationSuccess;       
        public FutronicEngine(IFpControl fpControl)
        {

            try
            {
                this.fpControl = fpControl;
                FutronicSdkBase dummy = new FutronicEnrollment();
                if (m_Operation != null)
                {
                    m_Operation.Dispose();
                    m_Operation = null;
                }
                m_Operation = dummy;

                this.registerOnPutOn(OnPutOn);
                this.registerOnTakeOff(OnTakeOff);
                this.registerUpdateScreen(UpdateScreenImage);
                this.registerOnComplete(OnEnrollmentComplete);
            }
            catch
            {
                MessageBox.Show("Please setup fingerprint driver first and try again.", "RAB CDMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void update()
        {
            FutronicSdkBase dummy = new FutronicEnrollment();
            m_Operation = dummy;

            this.registerOnPutOn(OnPutOn);
            this.registerOnTakeOff(OnTakeOff);
            this.registerUpdateScreen(UpdateScreenImage);
            this.registerOnComplete(OnEnrollmentComplete);
        }
        public void startCapture()
        {
            if (count < 10)
            {
                // Set control properties
                m_Operation.FakeDetection = false;
                m_Operation.FFDControl = true;
                m_Operation.FARN = 166;
                m_Operation.Version = VersionCompatible.ftr_version_current;
                m_Operation.FastMode = false;
                ((FutronicEnrollment)m_Operation).MIOTControlOff = false;
                ((FutronicEnrollment)m_Operation).MaxModels = 5;

                // start enrollment process
                ((FutronicEnrollment)m_Operation).Enrollment();
                count++;
            }

            else 
            {
                count = 0;
                update();
                OpenDevice();
            }
            
        }

        public void registerOnPutOn(Action<FTR_PROGRESS> onPut)
        {
            if (m_Operation != null)
            {
                m_Operation.OnPutOn += new OnPutOnHandler(onPut);
            }
        }

        public void registerOnTakeOff(Action<FTR_PROGRESS> onTakeoff)
        {
            if (m_Operation != null)
            {
                m_Operation.OnTakeOff += new OnTakeOffHandler(onTakeoff);
            }
        }

        public void registerUpdateScreen(Action<Bitmap> UpdateScreenImage)
        {
            if (m_Operation != null)
            {
                m_Operation.UpdateScreenImage += new UpdateScreenImageHandler(UpdateScreenImage);
            }
        }

        public void registerOnComplete(Action<bool, int> OnEnrollmentComplete)
        {
            if (m_Operation != null)
            {
                ((FutronicEnrollment)m_Operation).OnEnrollmentComplete += new OnEnrollmentCompleteHandler(OnEnrollmentComplete);
            }
        }

        public void unregisterOnComplete(Action<bool, int> OnEnrollmentComplete)
        {
            if (m_Operation != null)
            {
                ((FutronicEnrollment)m_Operation).OnEnrollmentComplete -= new OnEnrollmentCompleteHandler(OnEnrollmentComplete);
            }
        }

        public void unregisterOnPutOn(Action<FTR_PROGRESS> onPut)
        {
            if (m_Operation != null)
            {
                m_Operation.OnPutOn -= new OnPutOnHandler(onPut);
            }            
        }

        public void unregisterOnTakeOff(Action<FTR_PROGRESS> onTakeoff)
        {
            if (m_Operation != null)
            {
                m_Operation.OnTakeOff -= new OnTakeOffHandler(onTakeoff);
            }
        }

        public void unregisterUpdateScreen(Action<Bitmap> UpdateScreenImage)
        {
            if (m_Operation != null)
            {
                m_Operation.UpdateScreenImage -= new UpdateScreenImageHandler(UpdateScreenImage);
            }
        }

        private void OnPutOn(FTR_PROGRESS Progress)
        {
            this.fpControl.SetMessage("Put finger into device, please ...");
        }

        private void OnTakeOff(FTR_PROGRESS Progress)
        {
            this.fpControl.SetMessage("Take off finger from device, please ...");
        }

        private void OnEnrollmentComplete(bool bSuccess, int nRetCode)
        {
            StringBuilder szMessage = new StringBuilder();
            if (bSuccess)
            {
                // set status string
                szMessage.Append("Enrollment process finished successfully.");
                //szMessage.Append("Quality: ");
                //szMessage.Append(((FutronicEnrollment)futronic.m_Operation).Quality.ToString());
                this.fpControl.SetMessage(szMessage.ToString());
                this.fpControl.OnComplete(bSuccess);

            }
            else
            {
                szMessage.Append("Enrollment process failed.");
                szMessage.Append("Error description: ");
                szMessage.Append(FutronicSdkBase.SdkRetCode2Message(nRetCode));
                this.fpControl.SetMessage(szMessage.ToString());
            }
            
            //futronic.m_OperationObj = null;
        }

        private void UpdateScreenImage(Bitmap hBitmap)
        {
            // Do not change the state control during application closing.
            this.fpControl.OnGetImage(hBitmap);

        }


        override public bool OpenDevice()
        {
            try
            {
                startCapture();
            }
            catch(Exception ex)
            {

            }
            return false;
        }
        override public void CloseDevice()
        {
            this.unregisterOnPutOn(OnPutOn);
            this.unregisterOnTakeOff(OnTakeOff);
            this.unregisterUpdateScreen(UpdateScreenImage);
            this.unregisterOnComplete(OnEnrollmentComplete);
            if(m_Operation != null)
            {
                m_Operation.Dispose();
                m_Operation = null;
            }            
        }

    }
}
