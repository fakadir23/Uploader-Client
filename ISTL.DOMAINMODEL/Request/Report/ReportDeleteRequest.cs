using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Request.Report
{
    public class ReportDeleteRequest
    {
        public string dateFrom { get; set; }
        public string dateTo { get; set; }
        public List<string> tokenList { get; set; }
    }
}
