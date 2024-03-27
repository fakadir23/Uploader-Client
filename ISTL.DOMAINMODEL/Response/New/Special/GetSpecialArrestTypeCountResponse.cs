using ISTL.MODELS.DTO.New.Enrollment.Special;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Response.New.Special
{
    public class GetSpecialArrestTypeCountResponse
    {
        public int? code { get; set; }
        public string message { get; set; }
        public List<SpecialArrestTypeCountDto> specialCriminalCountList { get; set; }
        public int? total { get; set; }
    }
}
