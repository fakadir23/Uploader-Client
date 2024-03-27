using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.Enrollment.Biometric
{
    public class FingerprintDto
    {
        public string contentType { get; set; }
        public string extension { get; set; }
        public byte[] li { get; set; }
        public byte[] lm { get; set; }
        public byte[] lr { get; set; }
        public byte[] ls { get; set; }
        public byte[] lt { get; set; }
        public string referenceNo { get; set; }
        public byte[] ri { get; set; }
        public byte[] rm { get; set; }
        public byte[] rr { get; set; }
        public byte[] rs { get; set; }
        public byte[] rt { get; set; }
        public int? unit { get; set; }
    }
}
