using ISTL.MODELS.Common;
using ISTL.MODELS.DTO.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Response.Report
{
    public class ReportResultResponse : ApiResponse
    {
        public List<ReportResult> reportResultList { get; set; }
        public int? total { get; set; }
    }
}
