using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.Enrollment.CrimeInformation
{
    public class CaseDetailDto
    {
        public string caseNumber { get; set; }
        public int? caseStatus { get; set; }
        public string dateOfCase { get; set; }
        public int? district { get; set; }
        public string penalCodeSection { get; set; }
        public int? upozilaOrThana { get; set; }
    }
}
