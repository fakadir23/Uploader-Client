using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.Enrollment.Biometric
{
    public class PhotoDto
    {
        public string contentType { get; set; }
        public string extension { get; set; }
        public byte[] photo { get; set; }
        public string referenceNo { get; set; }
        public int? unit { get; set; }
    }
}
