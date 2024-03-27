using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.Enrollment.CrimeInformation
{
    public class RemandInformationDto
    {
        public string dateOfRemand { get; set; }
        public int? durationDay { get; set; }
        public int? durationMonth { get; set; }
        public int? durationYear { get; set; }
        public string reason { get; set; }
        public string remandNumber { get; set; }
    }
}
