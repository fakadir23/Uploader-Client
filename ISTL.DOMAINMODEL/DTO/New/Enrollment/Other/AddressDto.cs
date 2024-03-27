using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.Enrollment
{
    public class AddressDto
    {
        public string createdAt { get; set; }
        public int? createdBy { get; set; }
        public int? district { get; set; }
        public string id { get; set; }
        public int? union { get; set; }
        public int? upazila { get; set; }
        public string updatedAt { get; set; }
        public int? updatedBy { get; set; }
        public string villageHouseRoadNo { get; set; }
    }
}
