using ISTL.MODELS.DTO.ProfileManagement.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Request.ProfileManagement
{
    public class ProfileManagementEnrollmentRequest
    {
        public int? cdmsUser { get; set; }
        public ProfileFingerprintDto fingerprint { get; set; }
        public string fullName { get; set; }
        public int? gender { get; set; }
        public string id { get; set; }
        public ProfileIrisDto iris { get; set; }
        public ProfilePhotoDto photo { get; set; }
        public int? profileType { get; set; }
    }
}
