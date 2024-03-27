using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Response
{
    public class EnrollmentAddResponse : ServiceResponse
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
    }
}
