using ISTL.MODELS.DTO.New.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.JailDBBioMatch
{
    public class JailProfileDto
    {
        public int? age { get; set; }
        public string bloodGroup { get; set; }
        public string countryId { get; set; }
        public string dob { get; set; }
        public string email { get; set; }
        public string genderLookup { get; set; }
        public string ipsddmsNo { get; set; }
        public string maritalStatus { get; set; }
        public string mobileNo { get; set; }
        public string nameEn { get; set; }
        public string nid { get; set; }
        public AddressDto permanentAddress { get; set; }
        public string phone { get; set; }
        public byte[] photo { get; set; }
        public AddressDto presentAddress { get; set; }
        public string prisonName { get; set; }
        public string religionLookup { get; set; }
        public double? score { get; set; }
        public string status { get; set; }
        public string utpConvictNo { get; set; }
        public string utpConvictType { get; set; }
    }
}
