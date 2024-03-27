using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.Enrollment.Biometric
{
    public class IrisDto
    {
        public string contentType { get; set; }
        public string extension { get; set; }
        public byte[] left { get; set; }
        public string referenceNo { get; set; }
        public byte[] right { get; set; }
        public int? unit { get; set; }
    }
}
