using ISTL.MODELS.DTO.New.Enrollment.BECverify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Response.New
{
    public class GetBECidentifyResponse
    {
        public int? code { get; set; }
        public string message { get; set; }
        public int? total { get; set; }
        public List<BECvoterInfoDto> payloads { get; set; }
    }
}
