using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.Enrollment.Special
{
    public class SpecialArrestTypeCountDto
    {
        public int? contagiousPatients { get; set; }
        public string countDate { get; set; }
        public string createdAt { get; set; }
        public int? createdBy { get; set; }
        public int? directSubmitInPS { get; set; }
        public int? mobileCourts { get; set; }
        public int? subUnit { get; set; }
        public int? unit { get; set; }
        public int? updateBy { get; set; }
        public string updatedAt { get; set; }
    }
}
