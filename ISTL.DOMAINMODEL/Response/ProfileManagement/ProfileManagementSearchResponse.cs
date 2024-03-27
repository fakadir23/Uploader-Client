using ISTL.MODELS.DTO.New.Enrollment.Biometric;
using ISTL.MODELS.DTO.ProfileManagement;
using ISTL.MODELS.DTO.ProfileManagement.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Response.ProfileManagement
{
    public class ProfileManagementSearchResponse
    {
        public int? code { get; set; }
        //public FingerprintResponseDto fingerprint { get; set; }
        public ProfileManagementFingerprintResponseDto fingerprint { get; set; }
        public string fullName { get; set; }
        public int? gender { get; set; }
        public string id { get; set; }
        public IrisResponseDto iris { get; set; }
        public string message { get; set; }
        public string photo { get; set; }
        public int? profileType { get; set; }
    }
}
