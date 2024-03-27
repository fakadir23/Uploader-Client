using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.Enrollment
{
    public class ComplainDto
    {
        public int? attachmentNumber { get; set; }
        public byte[] complaint { get; set; }
        public string contentType { get; set; }
        public string extension { get; set; }
        public string detail { get; set; }
        public List<string> files { get; set; }
        public string title { get; set; }
    }
}
