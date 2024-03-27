using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.Enrollment.Other
{
    public class AttachmentDto
    {
        public int? unit { get; set; }
        public List<ComplainDto> complaintList { get; set; }
        public List<FIRDto> firList { get; set; }
        public string referenceNo { get; set; }
        public List<SeizureDto> seizureList { get; set; }
    }
}
