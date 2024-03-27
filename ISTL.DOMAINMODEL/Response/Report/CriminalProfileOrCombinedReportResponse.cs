using ISTL.MODELS.DTO.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Response.Report
{
    public class CriminalProfileOrCombinedReportResponse
    {
        public int? code { get; set; }
        public string message { get; set; }
        public List<CriminalProfileOrCombinedReportDto> reportResultList { get; set; }
        public int? total { get; set; }
    }
}
