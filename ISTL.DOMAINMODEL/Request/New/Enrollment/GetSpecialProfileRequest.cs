using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Request.New.Enrollment
{
    public class GetSpecialProfileRequest
    {
        public int arrestType { get; set; }
        public string creationDateFrom { get; set; }
        public string creationDateTo { get; set; }
        public int crimeType { get; set; }
        public int district { get; set; }
        public string fatherName { get; set; }
        public string fullName { get; set; }
        public int gender { get; set; }
        public int id { get; set; }
        public int limit { get; set; }
        public string magistrateName { get; set; }
        public string rabOfficerName { get; set; }
        public string referenceNo { get; set; }
        public int startIndex { get; set; }
        public int subUnit { get; set; }
        public int union { get; set; }
        public int? unit { get; set; }
        public int upazila { get; set; }
    }
}
