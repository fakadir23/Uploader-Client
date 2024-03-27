using ISTL.MODELS.DTO.New.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Request.NotEntry
{
    public class NotEntrySearchRequest
    {
        public string accusedName { get; set; }
        public AddressDto address { get; set; }
        public int? createdBy { get; set; }
        public string creationDateFrom { get; set; }
        public string creationDateTo { get; set; }
        public string fatherName { get; set; }
        public string id { get; set; }
        public int? limit { get; set; }
        public string mobileNo { get; set; }
        public string noEntryDate { get; set; }
        public int? notEntryCaseType { get; set; }
        public int? notEntryReason { get; set; }
        public string referenceNo { get; set; }
        public int? startIndex { get; set; }
        public int? subUnit { get; set; }
        public int? unit { get; set; }
        public string uploadedDateFrom { get; set; }
        public string uploadedDateTo { get; set; }
    }
}
