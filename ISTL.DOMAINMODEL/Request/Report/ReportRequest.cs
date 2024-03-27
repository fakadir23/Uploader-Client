using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Request.Report
{
    public class ReportRequest
    {
        public int? reportType { get; set; }
        public string ageRange { get; set; }
        public int? arrestType { get; set; }
        public string creationDateFrom { get; set; }
        public string creationDateTo { get; set; }
        public int crimeType { get; set; }
        public string currentDate { get; set; }
        public int? gender { get; set; }
        public int? limit { get; set; }
        public string reportExtension { get; set; }
        public int? startIndex { get; set; }
        public int? unit { get; set; }
        public int? subUnit { get; set; }        
    }
}
