using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Request.Beneficiary.Biometric
{
    public enum NoFingerprintReason
    {
        NoFingerprintImpression = 1,
        NoFinger = 2,
        NoLeftHand = 3,
        NoRightHand = 4,
        NoBothHand = 5,
        Other = 6
    }
}
