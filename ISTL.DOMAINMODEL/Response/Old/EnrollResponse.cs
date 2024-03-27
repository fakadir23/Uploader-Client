using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Response
{
    public class EnrollResponse : ServiceResponse
    {
        public string afisId { get; set; }
        public string responseMessage { get; set; }
    }
}
