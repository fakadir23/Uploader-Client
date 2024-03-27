using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Request.Report
{
    public class CriminalReportRequest
    {
        public string ageRange { get; set; }
        public int? arrestType { get; set; }
        public string creationDateFrom { get; set; }
        public string creationDateTo { get; set; }
        public int? crimeType { get; set; }
        public List<string> criminalHistoryIdList { get; set; }
        public string currentDate { get; set; }
        public int? gender { get; set; }
        public string id { get; set; }
        public int? nationality { get; set; }
        public string referenceNo { get; set; }
        public string reportExtension { get; set; }
        public int? subUnit { get; set; }
        public int? unit { get; set; }
    }
}
