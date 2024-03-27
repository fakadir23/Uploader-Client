using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Response.New
{
    public class GetProfileDataByteResponse
    {
        public int? code { get; set; }
        public byte[] file { get; set; }
        public string message { get; set; }
    }
}
