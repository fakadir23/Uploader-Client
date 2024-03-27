using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.Enrollment
{
    public class PersonBiometricDto
    {
        public int? id { get; set; }
        public byte[] leftIris { get; set; }
        public int? personId { get; set; }
        public byte[] photo { get; set; }
        public string referenceNumber { get; set; }
        public byte[] rightIris { get; set; }
        public byte[] wsqLi { get; set; }
        public byte[] wsqLl { get; set; }
        public byte[] wsqLm { get; set; }
        public byte[] wsqLr { get; set; }
        public byte[] wsqLt { get; set; }
        public byte[] wsqRi { get; set; }
        public byte[] wsqRl { get; set; }
        public byte[] wsqRm { get; set; }
        public byte[] wsqRr { get; set; }
        public byte[] wsqRt { get; set; }
    }
}
