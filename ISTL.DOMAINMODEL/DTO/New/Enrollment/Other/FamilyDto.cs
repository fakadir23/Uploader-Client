using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.Enrollment
{
    public class FamilyDto
    {
        public int? district { get; set; }
        public bool? illegalArmsPossession { get; set; }
        public string illegalArmsPossessionRemarks { get; set; }
        public string incomeSource { get; set; }
        public string maritalStatus { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public bool? politicalInvolvement { get; set; }
        public string politicalInvolvementRemarks { get; set; }
        public string profession { get; set; }
        public string relation { get; set; }
        public int? union { get; set; }
        public int? upazila { get; set; }
        public string villageHouseRoadNo { get; set; }
    }
}
