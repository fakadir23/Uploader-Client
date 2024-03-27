using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IriMagicBinoSDK;

namespace ISTL.IRIS
{
    class CaptureSetting
    {
        public int Count = 3;
        public IriCAMMSelectEyeMode SelectEyeMode = IriCAMMSelectEyeMode.NormalSelect;
        public IriCAMMQualityMode QualityMode = IriCAMMQualityMode.Normal;
        public IriCAMMCaptureOperationMode OperationMode = IriCAMMCaptureOperationMode.AutoCapture;
        public IriCAMMCaptureMode CaptureMode = IriCAMMCaptureMode.TimeBased;
        public byte ScaleRatio = 1;
        public bool IsStreamMode = true;
        public bool IsAutoLED = true;
    }
}
