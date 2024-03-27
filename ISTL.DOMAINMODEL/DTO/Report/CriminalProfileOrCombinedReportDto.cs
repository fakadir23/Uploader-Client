using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.Report
{
    public class CriminalProfileOrCombinedReportDto
    {
        public int? code { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        public string token { get; set; }
        public string url { get; set; }
    }
}
