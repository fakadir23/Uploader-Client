using ISTL.MODELS.Common;
using ISTL.MODELS.DTO.New.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Response.New.Enrollment
{
    public class HashVerifiedResponse : ApiResponse
    {
        //public string errorMsg { get; set; }
        //public List<HashStatus> hashStatuses { get; set; }
        public int? result { get; set; }
    }
}
