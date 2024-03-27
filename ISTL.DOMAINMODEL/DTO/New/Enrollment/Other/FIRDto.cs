using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.Enrollment
{
    public class FIRDto
    {
        public int? attachmentNumber { get; set; }
        public string contentType { get; set; }
        public int? district { get; set; }
        public string extension { get; set; }
        public List<string> files { get; set; }
        public byte[] fir { get; set; }
        public string firDate { get; set; }
        public string firNo { get; set; }
        public string remarks { get; set; }
        public int? upozilaOrThana { get; set; }
    }
}
