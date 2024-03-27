using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.Enrollment.CrimeInformation
{
    public class CrimeHistoryDto
    {
        public string crimeCode { get; set; }
        public string crimeNote { get; set; }
        public int? crimeType { get; set; }
        public string dateOfCrime { get; set; }
        public string description { get; set; }
        public int? durationDay { get; set; }
        public int? durationMonth { get; set; }
        public int? durationYear { get; set; }
        public string titleId { get; set; }
        public string victims { get; set; }
        public string warrantAccused { get; set; }
        public string warrantNotAccused { get; set; }
    }
}
