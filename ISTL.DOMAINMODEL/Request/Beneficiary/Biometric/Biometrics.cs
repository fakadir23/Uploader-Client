using ISTL.MODELS.Request.Beneficiary.Biometric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Request.Beneficiary.Biometric
{
    public class Biometrics
    {
        public string applicationId { get; set; }
        public byte[] biometricData { get; set; }
        public string biometricType { get; set; }
        public string biometricUrl { get; set; }
        public string biometricUserType { get; set; }
        public bool noFingerPrint { get; set; }
        public string noFingerprintReason { get; set; }
        public string noFingerprintReasonText { get; set; }
    }
}
