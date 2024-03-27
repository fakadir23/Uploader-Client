using ISTL.MODELS.DTO.New.Enrollment.Biometric;
using ISTL.MODELS.DTO.New.Enrollment.CrimeInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.Enrollment
{
    public class ProfileResponseDto
    {
        public int? age { get; set; }
        public string arrestDate { get; set; }
        public List<ArrestInfoDto> arrestInfos { get; set; }
        public string arrestedBy { get; set; }
        public List<BankAccountDto> bankAccounts { get; set; }
        public string bloodGroup { get; set; }
        public List<ComplainDto> complains { get; set; }
        public string createdAt { get; set; }
        public Int32? createdBy { get; set; }
        public CrimeInformationDto crimeInformation { get; set; }
        public int? crimeZoneDistrict { get; set; }
        public int? crimeZoneUpazila { get; set; }
        public string criminalName { get; set; }
        public string dateOfBirth { get; set; }
        public List<EducationInfoDto> educationalInformations { get; set; }
        public string eyeColor { get; set; }
        public List<FamilyDto> familys { get; set; }
        public FingerprintResponseDto fingerprint { get; set; }
        public List<FIRDto> firs { get; set; }
        public ForeignAddressDto foreignAddress { get; set; }
        public string fullName { get; set; }
        public string gender { get; set; }
        public string hash { get; set; }
        public HeightDto height { get; set; }
        public string id { get; set; }
        public string identificationMark { get; set; }
        public string investigatingOfficerBPNumber { get; set; }
        public string investigatingOfficerMobile { get; set; }
        public string investigatingOfficerName { get; set; }
        public string iorank { get; set; }
        public IrisResponseDto iris { get; set; }
        public string maritalStatus { get; set; }
        public List<string> mobile { get; set; }
        public string morningDate { get; set; }
        public NationalityProfileDto nationality { get; set; }
        public List<string> nickName { get; set; }
        public string nid { get; set; }
        public string occupation { get; set; }
        public List<OtherInfoDto> otherInformationList { get; set; }
        public AddressDto permanentAddress { get; set; }
        public string photo { get; set; }
        public string politicalGroup { get; set; }
        public PoliticalIdentityDto politicalIdentity { get; set; }
        public AddressDto presentAddress { get; set; }
        public List<PropertiesInfoDto> propertiesInfos { get; set; }
        public string referenceNo { get; set; }
        public Int32? regionOfBirth { get; set; }
        public string religion { get; set; }
        public List<SeizureDto> seizures { get; set; }
        public int? status { get; set; }
        public int? subUnit { get; set; }
        public Int32? unit { get; set; }
        public string updatedAt { get; set; }
        public int? updatedBy { get; set; }
        public Int32? uploadedBy { get; set; }
        public string uploadedDate { get; set; }
        public WeightDto weight { get; set; }
    }
}
