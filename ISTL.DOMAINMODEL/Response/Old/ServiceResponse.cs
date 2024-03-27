using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Response
{
    public class ServiceResponse
    {
        public string errorCode { get; set; }
        public string errorMsg { get; set; }
        public bool operationResult { get; set; }
    }
}
