using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.Enrollment
{
    public class EducationInfoDto
    {
        public EducationInfoDto(string educationStatus, string educationInstitute, bool politicalInvolvementInInstitute, string educationRemarks)
        {
            this.educationalStatus = educationStatus;
            this.nameOfInstitution = educationInstitute;
            this.politicalInvolvement = politicalInvolvementInInstitute;
            this.remarks = educationRemarks;
        }

        public EducationInfoDto()
        {

        }

        public string educationalStatus { get; set; }
        public string nameOfInstitution { get; set; }
        public bool? politicalInvolvement { get; set; }
        public string remarks { get; set; }
    }
}
