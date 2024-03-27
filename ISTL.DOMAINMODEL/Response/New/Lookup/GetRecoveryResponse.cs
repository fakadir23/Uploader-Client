using ISTL.MODELS.DTO.New.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Response.New.Lookup
{
    public class GetRecoveryResponse
    {
        public int? code { get; set; }
        public List<RecoveryDto> lookupList { get; set; }
        public string message { get; set; }
    }
}
