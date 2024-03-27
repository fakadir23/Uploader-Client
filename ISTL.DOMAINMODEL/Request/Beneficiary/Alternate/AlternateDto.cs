using ISTL.MODELS.Request.Beneficiary.Biometric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Request.Beneficiary.Alternate
{
    public class AlternateDto
    {
        public List<Biometrics> biometrics { get; set; }
        public string documentType { get; set; }
        public string documentTypeOther { get; set; }
        public string nationalId { get; set; }
        public int payeeAge { get; set; }
        public string payeeFirstName { get; set; }
        public string payeeGender { get; set; }
        public string payeeLastName { get; set; }
        public string payeeMiddleName { get; set; }
        public string payeeNickName { get; set; }
        public string payeePhoneNo { get; set; }
        public string relationshipOther { get; set; }
        //public RelationshipWithHouseholdHead relationshipWithHouseholdHead { get; set; }
        public string relationshipWithHouseholdHead { get; set; }

        public AlternateDto()
        {
            biometrics = new List<Biometrics>();
        }
    }
}
