using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.MODELS.DTO.New.Enrollment.Biometric;
using ISTL.MODELS.DTO.New.Enrollment.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.NotEntry
{
    public class NotEntryDto
    {
        public string accusedName { get; set; }
        public AddressDto address { get; set; }
        public AttachmentDto attachment { get; set; }
        public BiometricDto biometric { get; set; }
        public ComplainDto complain { get; set; }
        public string createdAt { get; set; }
        public int? createdBy { get; set; }
        public string fatherName { get; set; }
        public FingerprintResponseDto fingerprint { get; set; }
        public FIRDto fir { get; set; }
        public string id { get; set; }
        public IrisResponseDto iris { get; set; }
        public string mobileNo { get; set; }
        public string noEntryDate { get; set; }
        public string notEntryCaseType { get; set; }
        public string notEntryReason { get; set; }
        public string photo { get; set; }
        public string referenceNo { get; set; }
        public SeizureDto seizure { get; set; }
        public int? subUnit { get; set; }
        public int? unit { get; set; }
        public string updatedAt { get; set; }
        public int? updatedBy { get; set; }
        public string uploadedAt { get; set; }
        public int? uploadedBy { get; set; }
        public string warrantNo { get; set; }
        public string warrantIssueDate { get; set; }
        public int? status { get; set; }
        public int? errorStatus { get; set; }
        public string errorMessage { get; set; }
        public NotEntryDto()
        {
            attachment = new AttachmentDto();
            biometric = new BiometricDto();
        }
    }
}
