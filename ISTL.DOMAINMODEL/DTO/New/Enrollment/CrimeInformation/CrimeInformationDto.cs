using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.Enrollment.CrimeInformation
{
    public class CrimeInformationDto
    {
        public ActivityDto activities { get; set; }
        public List<CaseDetailDto> caseDetails { get; set; }
        public string createdAt;
        public int? createdBy { get; set; }
        public List<CrimeHistoryDto> crimeHistorys { get; set; }
        public int? crimeType { get; set; }
        public CrimeZoneDto crimeZone { get; set; }
        public int? criminalStatus { get; set; }
        public string criminal_id { get; set; }
        public string details { get; set; }
        public string groupOrGangName { get; set; }
        public IllegalArmsPossessionDto illegalArmsPossession { get; set; }
        public int? latestState { get; set; }
        public bool? legalArmsPossession { get; set; }
        public int? priorityListGovt { get; set; }
        public string referenceNo { get; set; }
        public List<RemandInformationDto> remandInformations { get; set; }
        public string updatedAt { get; set; }
        public int? updatedBy { get; set; }
        public WarrantDto warrant { get; set; }
        public List<RecoveryEntryDto> recoveryList { get; set; }
        public CrimeInformationDto()
        {
            activities = new ActivityDto();
            caseDetails = new List<CaseDetailDto>();
            crimeHistorys = new List<CrimeHistoryDto>();
            crimeZone = new CrimeZoneDto();
            illegalArmsPossession = new IllegalArmsPossessionDto();
            remandInformations = new List<RemandInformationDto>();
            warrant = new WarrantDto();
            recoveryList = new List<RecoveryEntryDto>();
        }
    }
}
