using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Request.Adjudication
{
    public class GetMatchListRequest
    {
        public string matchedWith { get; set; }
        public string referenceNo { get; set; }
    }
}
