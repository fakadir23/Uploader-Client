using ISTL.MODELS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Response.New.Enrollment
{
    public class NotVerifiedHashResponse : ApiResponse
    {
        public List<string> hashList { get; set; }
        public NotVerifiedHashResponse()
        {
            hashList = new List<string>();
        }
    }
}
