using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Request.Adjudication
{
    public class AdjudicateRequest
    {
        public bool approved { get; set; }
        public string referenceNo { get; set; }
    }
}
