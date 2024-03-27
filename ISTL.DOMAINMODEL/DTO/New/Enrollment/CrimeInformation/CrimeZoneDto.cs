using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.Enrollment.CrimeInformation
{
    public class CrimeZoneDto
    {
        public string addressLine { get; set; }
        public int? district { get; set; }
        public string remarks { get; set; }
        public int? union { get; set; }
        public int? unit { get; set; }
        public int? upozilaOrThana { get; set; }
    }
}
