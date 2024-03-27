using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.Report
{
    public class ReportResult
    {
        public int id { get; set; }
        public string status { get; set; }
        public string token { get; set; }
        public string url { get; set; }


        public int? reportType { get; set; }
        public string ageRange { get; set; }
        public int? arrestType { get; set; }
        public string creationDateFrom { get; set; }
        public DateTime creationDateFromDt { get; set; }
        public string creationDateTo { get; set; }
        public DateTime creationDateToDt { get; set; }
        public string createdAt { get; set; }
        public DateTime createdAtDt { get; set; }
        public int crimeType { get; set; }
        public int nationality { get; set; }
        public string currentDate { get; set; }
        public DateTime currentDateDt { get; set; }
        public int? gender { get; set; }
        public int? limit { get; set; }
        public string reportExtension { get; set; }
        public int? startIndex { get; set; }
        public int? unit { get; set; }
        public int? subUnit { get; set; }
    }
}
