using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Request.Adjudication
{
    public class AdjUpdateMatchStatusRequest
    {
        public string referenceNo { get; set; }
        public int status { get; set; }
    }
}
