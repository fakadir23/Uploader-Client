using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.Enrollment
{
    public class SeizureDto
    {
        public int? attachmentNumber { get; set; }
        public string contentType { get; set; }
        public string detail { get; set; }
        public string extension { get; set; }
        public List<string> files { get; set; }
        public byte[] seizure { get; set; }
        public string title { get; set; }
    }
}
