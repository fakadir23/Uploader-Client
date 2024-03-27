using ISTL.MODELS.Request.Beneficiary.Alternate;
using ISTL.MODELS.Request.Beneficiary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Request.Beneficiary
{
    public class NomineeDto
    {
        public string applicationId { get; set; }
        public bool? isReadWrite { get; set; }
        public int? nomineeAge { get; set; }
        public string nomineeFirstName { get; set; }
        public string nomineeGender { get; set; }
        public string nomineeLastName { get; set; }
        public string nomineeMiddleName { get; set; }
        public string nomineeNickName { get; set; }
        public string nomineeOccupation { get; set; }
        public string otherOccupation { get; set; }
        public string relationshipOther { get; set; }
        public string relationshipWithHouseholdHead { get; set; }
    }
}
