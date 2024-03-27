using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.Enrollment.Special
{
    public class SpecialAddressDto
    {
        public int? district { get; set; }
        public int? upazila { get; set; }
        public int? union { get; set; }
        public string villageHouseRoadNo { get; set; }
    }
}
