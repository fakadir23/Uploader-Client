using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.Enrollment.Biometric
{
    public class FingerprintResponseDto
    {
        public string li { get; set; }
        public string lm { get; set; }
        public string lr { get; set; }
        public string ls { get; set; }
        public string lt { get; set; }
        public string ri { get; set; }
        public string rm { get; set; }
        public string rr { get; set; }
        public string rs { get; set; }
        public string rt { get; set; }
        public int? unit { get; set; }
    }
}
