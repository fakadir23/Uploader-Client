using ISTL.MODELS.DTO.New.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.Search
{
    public class DemographicSearchDto
    {
        public string creationDateFrom { get; set; }
        public string creationDateTo { get; set; }
        public int crimeType { get; set; }
        public string fullName { get; set; }
        public string id { get; set; }
        public int limit { get; set; }
        public int nationality { get; set; }
        public string nid { get; set; }
        //public AddressDto permanentAddress { get; set; }
        //public AddressDto presentAddress { get; set; }
        public AddressDto address { get; set; }
        public string referenceNo { get; set; }
        public int startIndex { get; set; }
        public int unit { get; set; }
        public bool? pendingFir { get; set; }
        public bool? dataWithBiometric { get; set; }
        public bool? dataWithOutBiometric { get; set; }
        public int subUnit { get; set; }
        public int gender { get; set; }
        public int religion { get; set; }
        public string uploadedDateFrom { get; set; }
        public string uploadedDateTo { get; set; }
    }
}
