using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Response.New
{
    public class DashboardSummaryResponse
    {
        public int? biometricCount { get; set; }
        public int? code { get; set; }
        public int? firPendingCount { get; set; }
        public string message { get; set; }
    }
}
