using ISTL.MODELS.Request.Beneficiary.Alternate;
using ISTL.MODELS.Request.Beneficiary.Biometric;
using ISTL.MODELS.Request.Beneficiary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Request.Beneficiary
{
    public class RegisterBeneficiaryRequest
    {
        public Address address { get; set; }
        public AlternateDto alternatePayee1 { get; set; }
        public AlternateDto alternatePayee2 { get; set; }
        public string applicationId { get; set; }
        public List<Biometrics> biometrics { get; set; }
        public int? createdBy { get; set; }
        public string currency { get; set; }
        public string documentType { get; set; }
        public string documentTypeOther { get; set; }
        public string householdIncomeSource { get; set; }
        public HouseholdInfo householdMember17 { get; set; }
        public HouseholdInfo householdMember2 { get; set; }
        public HouseholdInfo householdMember35 { get; set; }
        public HouseholdInfo householdMember5 { get; set; }
        public HouseholdInfo householdMember64 { get; set; }
        public HouseholdInfo householdMember65 { get; set; }
        public int? householdMonthlyAvgIncome { get; set; }
        public int? householdSize { get; set; }
        public string incomeSourceOther { get; set; }
        public bool? isOtherMemberPerticipating { get; set; }
        public bool? isReadWrite { get; set; }
        public LocationDto location { get; set; }
        public int? memberReadWrite { get; set; }
        public List<NomineeDto> nominees { get; set; }
        public string notPerticipationOtherReason { get; set; }
        public string notPerticipationReason { get; set; }
        public string relationshipOther { get; set; }
        public string relationshipWithHouseholdHead { get; set; }
        public int? respondentAge { get; set; }
        public string respondentFirstName { get; set; }
        public string respondentGender { get; set; }
        public string respondentId { get; set; }
        public string respondentLastName { get; set; }
        public string respondentLegalStatus { get; set; }
        public string respondentMaritalStatus { get; set; }
        public string respondentMiddleName { get; set; }
        public string respondentNickName { get; set; }
        public string respondentPhoneNo { get; set; }
        public string selectionCriteria { get; set; }
        public List<string> selectionReason { get; set; }
        public string spouseFirstName { get; set; }
        public string spouseLastName { get; set; }
        public string spouseMiddleName { get; set; }
        public string spouseNickName { get; set; }

        public RegisterBeneficiaryRequest()
        {
            biometrics = new List<Biometrics>();
            nominees = new List<NomineeDto>();
            selectionReason = new List<string>();
        }
    }
}
