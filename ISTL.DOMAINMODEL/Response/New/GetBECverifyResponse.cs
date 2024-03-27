using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Response.New
{
    public class GetBECverifyResponse
    {
        public int? code { get; set; }
        public GetBECdataResponse data { get; set; }
        public string message { get; set; }
    }
}
