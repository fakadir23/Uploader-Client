using ISTL.MODELS.DTO.New.Enrollment.Special;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.Enrollment
{
    public class SpecialEnrollmentDto
    {
        public int? id { get; set; }
        public string referenceNo { get; set; }
        public string fullName { get; set; }
        public string fatherName { get; set; }
        public string crimeType { get; set; }
        public SpecialCrimeZoneDto crimeZone { get; set; }
        public string fineAmount { get; set; }
        public string rabOfficerName { get; set; }
        public string magistrateName { get; set; }
        public string placeOfFine { get; set; }
        public SpecialAddressDto address { get; set; }
        public int? arrestTypeIntValue { get; set; }
        public string arrestType { get; set; }
        public string warrantNo { get; set; }
        public string hash { get; set; }
        public string createdAt { get; set; }
        public int? createdBy { get; set; }
        public string updatedAt { get; set; }
        public int? updatedBy { get; set; }
        public int? unit { get; set; }
        public int? subUnit { get; set; }
        public string gender { get; set; }
        public string errorMsgFromServer { get; set; }
        public SpecialEnrollPhotoDto photo { get; set; }
        public string photoUrl { get; set; }
        public string law { get; set; }
        public string nid { get; set; }
    }
}
