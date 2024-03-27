using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Request
{
    public class EnrollmentListSearchRequest
    {
        public string fullName { get; set; }
        public string gender { get; set; }
        public string id { get; set; }
        public int limit { get; set; }
        public string nationalId { get; set; }
        public string nickName { get; set; }
        public string phone { get; set; }
        public string referenceNumber { get; set; }
        public int startIndex { get; set; }
    }
}
