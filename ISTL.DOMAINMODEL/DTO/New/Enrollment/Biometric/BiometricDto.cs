using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.Enrollment.Biometric
{
    public class BiometricDto
    {
        public FingerprintDto fingerprint { get; set; }
        public IrisDto iris { get; set; }
        public PhotoDto photo { get; set; }
        public string referenceNo { get; set; }

        public BiometricDto()
        {
            photo = new PhotoDto();
            fingerprint = new FingerprintDto();
            iris = new IrisDto();
        }
    }
}
