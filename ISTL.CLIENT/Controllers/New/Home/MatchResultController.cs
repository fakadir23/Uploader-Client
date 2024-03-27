using ISTL.COMMON;
using ISTL.COMMON.Database;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.MODELS.DTO.New.Enrollment.BECverify;
using ISTL.MODELS.DTO.New.Enrollment.CrimeInformation;
using ISTL.MODELS.DTO.Report;
using ISTL.MODELS.Request.Report;
using ISTL.MODELS.Response.New;
using ISTL.MODELS.Response.New.Enrollment;
using ISTL.MODELS.Response.Report;
using ISTL.PERSOGlobals;
using ISTL.RAB.ApiManager;
using ISTL.RAB.DbManager;
using ISTL.RAB.Entity;
using ISTL.RAB.View;
using ISTL.RAB.View.New.Home;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISTL.RAB.Controllers.New.Home
{
    public class MatchResultController : ViewController
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        public MatchResultForm matchResultForm;
        public List<ProfileResponseDto> profileList;
        public GetBECdataResponse becDataResponse;
        public DbLookupManager dblookupManager;
        public List<BECvoterInfoDto> voterInfoList;
        public MatchResultController()
        {
            matchResultForm = new MatchResultForm();
            base.SetView((IView)matchResultForm);
            matchResultForm.SetController(this);

            profileList = new List<ProfileResponseDto>();
            becDataResponse = new GetBECdataResponse();
            dblookupManager = new DbLookupManager();
            voterInfoList = new List<BECvoterInfoDto>();
        }

        public string GetValueForLookup(string columnName, string tableName, int id)
        {
            string value = dblookupManager.GetValueForLookup(columnName, tableName, id);
            return value;
        }

        public string GetCrimeTypeValueForLookup(int id)
        {
            string value;
            string nameEn = dblookupManager.GetValueForLookup("name_en", "crime_type", id);
            string nameBn = dblookupManager.GetValueForLookup("name_bn", "crime_type", id);
            value = nameEn;
            if (!string.IsNullOrEmpty(nameBn)) value += " (" + nameBn + ")";
            return value;
        }

        public override string GetName()
        {
            return Globals.ChildControllers.MATCH_RESULTS;
        }

        public void NIDSelect()
        {
            //CopyToProfileData(becDataResponse);
            if (becDataResponse != null)
            {
                CopyToProfileData(becDataResponse);
            }
            else
            {
                CopyToProfileFPnidMatchData(voterInfoList, matchResultForm.NidFPmatchIndex);
            }
            if (matchResultForm.openedFromUI == 1)
            {
                StaticData.Enrollment.profile.referenceNo = Guid.NewGuid().ToString().Replace("-", "").ToUpper().Substring(0, 10);
                StaticData.ModifiableNormalEnrollment = false;
            }
            else
            {
                StaticData.ModifiableNormalEnrollment = true;
            }
            //matchResultForm.DialogResult = System.Windows.Forms.DialogResult.OK;
            var previewForm = new GeneralProfilePreviewForm();
            DialogResult dr = previewForm.ShowDialog();
            if (dr == DialogResult.OK) matchResultForm.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void CopyToProfileData(GetBECdataResponse dto)
        {
            if (!string.IsNullOrEmpty(dto.nationalId))
            {
                StaticData.Enrollment.profile.nid = dto.nationalId;
            }
            if (!string.IsNullOrEmpty(dto.mobile))
            {
                List<string> mobileList = new List<string>();
                mobileList.Add(dto.mobile);
                StaticData.Enrollment.profile.mobile = mobileList;
            }
            StaticData.Enrollment.profile.fullName = dto.nameEn;
            if (!string.IsNullOrEmpty(dto.gender))
            {
                if (dto.gender == "male") StaticData.Enrollment.profile.gender = "Male";
                else if (dto.gender == "female") StaticData.Enrollment.profile.gender = "Female";
                else if (dto.gender == "other") StaticData.Enrollment.profile.gender = "Other";
            }
            if (!string.IsNullOrEmpty(dto.dateOfBirth))
            {
                try
                {
                    DateTime DOB = DateTime.ParseExact(dto.dateOfBirth, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    StaticData.Enrollment.profile.dateOfBirth = DOB.ToString("yyyy-MM-ddTHH:mm:ss.fffK");
                }
                catch (Exception) { }
            }
            if (!string.IsNullOrEmpty(dto.religion))
            {
                if (dto.religion == "muslim" || dto.religion == "hindu" || dto.religion == "christian" || dto.religion == "buddhist")
                {
                    StaticData.Enrollment.profile.religion = dto.religion;
                }
            }
            if (dto.photo != null)
            {
                StaticData.Enrollment.profile.biometric.photo.photo = dto.photo;
            }
        }

        private void CopyToProfileFPnidMatchData(List<BECvoterInfoDto> list, int index)
        {
            BECvoterInfoDto dto = list[index];
            if (!string.IsNullOrEmpty(dto.nid))
            {
                StaticData.Enrollment.profile.nid = dto.nid;
            }
            if (!string.IsNullOrEmpty(dto.mobile))
            {
                List<string> mobileList = new List<string>();
                mobileList.Add(dto.mobile);
                StaticData.Enrollment.profile.mobile = mobileList;
            }
            StaticData.Enrollment.profile.fullName = dto.nameEn;
            if (!string.IsNullOrEmpty(dto.gender))
            {
                if (dto.gender == "male") StaticData.Enrollment.profile.gender = "Male";
                else if (dto.gender == "female") StaticData.Enrollment.profile.gender = "Female";
                else if (dto.gender == "other") StaticData.Enrollment.profile.gender = "Other";
            }
            if (!string.IsNullOrEmpty(dto.dob))
            {
                try
                {
                    DateTime DOB = DateTime.ParseExact(dto.dob, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    StaticData.Enrollment.profile.dateOfBirth = DOB.ToString("yyyy-MM-ddTHH:mm:ss.fffK");
                }
                catch (Exception) { }
            }
            if (!string.IsNullOrEmpty(dto.religion))
            {
                if (dto.religion == "muslim" || dto.religion == "hindu" || dto.religion == "christian" || dto.religion == "buddhist")
                {
                    StaticData.Enrollment.profile.religion = dto.religion;
                }
            }
            if (!string.IsNullOrEmpty(dto.identificationMark)) StaticData.Enrollment.profile.identificationMark = dto.identificationMark;
            if (dto.photo != null)
            {
                StaticData.Enrollment.profile.biometric.photo.photo = dto.photo;
            }
        }

        public void ConfirmGetProfile()
        {
            ProfileSearchListResponse profileSearchListResponse = new ProfileSearchListResponse();

            string errorMsg = null;

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    profileSearchListResponse = new EnrollmentApiManager().SearchProfileList(null, matchResultForm.criminalId);
                }
                catch (WebException x)
                {
                    logger.Debug("Known error, when server not found when searching criminal. Error Message: " + x.Message);
                    errorMsg = "Seems you are Offline while searching criminal. Please contact with your Administrator.";
                }
                catch (TimeoutException x)
                {
                    logger.Debug("Connection timedout when searching criminal. Error Message: " + x.Message);
                    errorMsg = "Seems you are Offline while searching criminal. Please contact with your Administrator.";
                }
                catch (Exception x)
                {
                    logger.Error("There was an unexpected error when searching criminal.\n" + x.ToString());
                    errorMsg = "There was an unexpected error when searching criminal. Please contact with your Administrator.";
                }
            });

            if (errorMsg?.Length > 0)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", errorMsg);
                return;
            }

            if (profileSearchListResponse != null)
            {
                if (profileSearchListResponse.total > 0)
                {
                    if (matchResultForm.openedFromUI == 1 && profileSearchListResponse.profileList[0]?.createdBy != Users.Id)
                    {
                        CustomMessageBox.ShowMessage("SNSOP TOOLS", "You are not authorized to edit this profile");
                        return;
                    }
                    ProcessingDialog.Run(delegate ()
                    {
                        CopyToStaticData(profileSearchListResponse.profileList[0]);
                    });
                }
                else
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "No Profile Found.");
                    matchResultForm.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }
            }
            else
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "No Profile Found.");
                matchResultForm.DialogResult = DialogResult.Cancel;
            }
            if (matchResultForm.openedFromUI == 1)
            {
                StaticData.ModifiableNormalEnrollment = false;
                if (StaticData.Enrollment?.profile?.createdBy != Users.Id)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "You are not authorized to edit this profile");
                    return;
                }
            }
            else
            {
                StaticData.ModifiableNormalEnrollment = true;
            }

            matchResultForm.DialogResult = DialogResult.OK;
        }

        public void Select()
        {
            ProfileSearchListResponse profileSearchListResponse = new ProfileSearchListResponse();

            string errorMsg = null;

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    //profileSearchListResponse = new EnrollmentApiManager().SearchProfileList(matchResultForm.referenceNo, null);
                    profileSearchListResponse = new EnrollmentApiManager().SearchProfileList(null, matchResultForm.criminalId);
                }
                catch (WebException x)
                {
                    logger.Debug("Known error, when server not found when searching criminal. Error Message: " + x.Message);
                    errorMsg = "Seems you are Offline while searching criminal. Please contact with your Administrator.";
                }
                catch (TimeoutException x)
                {
                    logger.Debug("Connection timedout when searching criminal. Error Message: " + x.Message);
                    errorMsg = "Seems you are Offline while searching criminal. Please contact with your Administrator.";
                }
                catch (Exception x)
                {
                    logger.Error("There was an unexpected error when searching criminal.\n" + x.ToString());
                    errorMsg = "There was an unexpected error when searching criminal. Please contact with your Administrator.";
                }
            });

            if (!string.IsNullOrEmpty(errorMsg))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", errorMsg);
                return;
            }

            string OldRefereceNo = string.Empty;

            if (profileSearchListResponse != null)
            {
                if (profileSearchListResponse.total > 0)
                {
                    ProcessingDialog.Run(delegate ()
                    {
                        //CopyToStaticData(profileSearchListResponse.profileList[0]);
                        OldRefereceNo = profileSearchListResponse.profileList[0].referenceNo;
                        CopyToInitialStaticData(profileSearchListResponse.profileList[0]);
                    });
                }
                else
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "No Profile Found.");
                    matchResultForm.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }
            }
            else
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "No Profile Found.");
                matchResultForm.DialogResult = DialogResult.Cancel;
            }
            if (matchResultForm.openedFromUI == 1)
            {
                StaticData.ModifiableNormalEnrollment = false;                
            }
            else
            {
                StaticData.ModifiableNormalEnrollment = true;
            }
            //matchResultForm.DialogResult = System.Windows.Forms.DialogResult.OK;
            var previewForm = new GeneralProfilePreviewForm();
            previewForm.OldReferenceNo = OldRefereceNo;
            previewForm.ThisParentUI = matchResultForm.openedFromUI;
            DialogResult dr = previewForm.ShowDialog();

            //if (dr == DialogResult.OK) matchResultForm.DialogResult = System.Windows.Forms.DialogResult.OK;
            if (dr == DialogResult.OK)
            {
                CopyToStaticData(profileSearchListResponse.profileList[0]);
                matchResultForm.DialogResult = DialogResult.OK;
            }
        }

        private void CopyToStaticData(ProfileResponseDto dto)
        {
            if (matchResultForm.openedFromUI == 1)  // Only if not in new entry, these are needed
            {
                StaticData.Enrollment.profile.arrestedBy = dto.arrestedBy;
                StaticData.Enrollment.profile.arrestDate = dto.arrestDate;
                StaticData.Enrollment.profile.arrestInfos = dto.arrestInfos;
                StaticData.Enrollment.profile.crimeInformation = dto.crimeInformation;
                StaticData.Enrollment.profile.investigatingOfficerBPNumber = dto.investigatingOfficerBPNumber;
                StaticData.Enrollment.profile.investigatingOfficerMobile = dto.investigatingOfficerMobile;
                StaticData.Enrollment.profile.investigatingOfficerName = dto.investigatingOfficerName;
                StaticData.Enrollment.profile.iorank = dto.iorank;
                StaticData.Enrollment.profile.politicalGroup = dto.politicalGroup;
                StaticData.Enrollment.profile.unit = dto.unit;
                StaticData.Enrollment.profile.subUnit = dto.subUnit;
                if (dto.complains != null)
                {
                    if (dto.complains.Count > 0)
                    {
                        List<ComplainDto> complaintList = new List<ComplainDto>();
                        for (int j = 0; j < dto.complains[0]?.files?.Count; j++)
                        {
                            ComplainDto complainDto = new ComplainDto();
                            complainDto.attachmentNumber = j;
                            string[] arr = dto.complains[0].files[j].Split('.');
                            complainDto.extension = "." + arr[1];
                            if (arr[1] == "pdf" || arr[1] == "PDF") complainDto.contentType = "application/" + arr[1];
                            else
                            {
                                complainDto.contentType = "image/" + arr[1];
                            }
                            complainDto.complaint = GetByteDataForStatic(dto.complains[0].files[j]);
                            if (complainDto.complaint?.Length > 0)
                            {
                                complaintList.Add(complainDto);
                            }
                        }
                        StaticData.Enrollment.profile.complains = complaintList;
                        StaticData.Enrollment.profile.attachment.complaintList = complaintList;
                    }
                }

                if (dto.seizures != null)
                {
                    if (dto.seizures.Count > 0)
                    {
                        List<SeizureDto> seizureList = new List<SeizureDto>();
                        for (int j = 0; j < dto.seizures[0]?.files?.Count; j++)
                        {
                            SeizureDto seizureDto = new SeizureDto();
                            seizureDto.attachmentNumber = j;
                            string[] arr = dto.seizures[0].files[j].Split('.');
                            seizureDto.extension = "." + arr[1];
                            if (arr[1] == "pdf" || arr[1] == "PDF") seizureDto.contentType = "application/" + arr[1];
                            else
                            {
                                seizureDto.contentType = "image/" + arr[1];
                            }
                            seizureDto.seizure = GetByteDataForStatic(dto.seizures[0].files[j]);
                            if (seizureDto.seizure?.Length > 0)
                            {
                                seizureList.Add(seizureDto);
                            }
                        }
                        StaticData.Enrollment.profile.seizures = seizureList;
                        StaticData.Enrollment.profile.attachment.seizureList = seizureList;
                    }
                }

                if (dto.firs != null)
                {
                    if (dto.firs.Count > 0)
                    {
                        List<FIRDto> firList = new List<FIRDto>();
                        for (int j = 0; j < dto.firs[0]?.files?.Count; j++)
                        {
                            FIRDto firDto = new FIRDto();
                            firDto.district = dto.firs[0].district;
                            firDto.upozilaOrThana = dto.firs[0].upozilaOrThana;
                            firDto.firDate = dto.firs[0].firDate;
                            firDto.firNo = dto.firs[0].firNo;
                            firDto.attachmentNumber = j;
                            string[] arr = dto.firs[0].files[j].Split('.');
                            firDto.extension = "." + arr[1];
                            if (arr[1] == "pdf" || arr[1] == "PDF") firDto.contentType = "application/" + arr[1];
                            else
                            {
                                firDto.contentType = "image/" + arr[1];
                            }
                            firDto.fir = GetByteDataForStatic(dto.firs[0].files[j]);
                            if (firDto.fir?.Length > 0)
                            {
                                firList.Add(firDto);
                            }
                        }
                        StaticData.Enrollment.profile.firs = firList;
                        StaticData.Enrollment.profile.attachment.firList = firList;
                    }
                }
            }
            else
            {
                StaticData.Enrollment.profile.arrestedBy = null;
                StaticData.Enrollment.profile.arrestDate = null;
                StaticData.Enrollment.profile.arrestInfos = new List<ArrestInfoDto>(); ;
                StaticData.Enrollment.profile.crimeInformation = new CrimeInformationDto();
                StaticData.Enrollment.profile.investigatingOfficerBPNumber = null;
                StaticData.Enrollment.profile.investigatingOfficerMobile = null;
                StaticData.Enrollment.profile.investigatingOfficerName = null;
                StaticData.Enrollment.profile.iorank = null;
                StaticData.Enrollment.profile.politicalGroup = null;
                StaticData.Enrollment.profile.unit = Users.Unit;
                StaticData.Enrollment.profile.subUnit = Users.SubUnit;
                StaticData.Enrollment.profile.complains = new List<ComplainDto>();
                StaticData.Enrollment.profile.attachment.complaintList = new List<ComplainDto>();
                StaticData.Enrollment.profile.seizures = new List<SeizureDto>();
                StaticData.Enrollment.profile.attachment.seizureList = new List<SeizureDto>();
                StaticData.Enrollment.profile.firs = new List<FIRDto>();
                StaticData.Enrollment.profile.attachment.firList = new List<FIRDto>();
            }
            //StaticData.Enrollment.profile.complains = dto.complains;
            StaticData.Enrollment.profile.createdAt = dto.createdAt;
            StaticData.Enrollment.profile.createdBy = dto.createdBy;
            StaticData.Enrollment.profile.dateOfBirth = dto.dateOfBirth;
            StaticData.Enrollment.profile.age = dto.age;
            StaticData.Enrollment.profile.nid = dto.nid;
            StaticData.Enrollment.profile.educationalInformations = dto.educationalInformations;
            StaticData.Enrollment.profile.familys = dto.familys;
            //StaticData.Enrollment.profile.firs = dto.firs;
            StaticData.Enrollment.profile.foreignAddress = dto.foreignAddress;
            StaticData.Enrollment.profile.fullName = dto.fullName;
            StaticData.Enrollment.profile.gender = dto.gender;
            StaticData.Enrollment.profile.hash = dto.hash;
            StaticData.Enrollment.profile.height = dto.height;
            StaticData.Enrollment.profile.identificationMark = dto.identificationMark;
            StaticData.Enrollment.profile.maritalStatus = dto.maritalStatus;
            StaticData.Enrollment.profile.mobile = dto.mobile;
            StaticData.Enrollment.profile.nationality = dto.nationality;
            StaticData.Enrollment.profile.nickName = dto.nickName;
            StaticData.Enrollment.profile.occupation = dto.occupation;
            StaticData.Enrollment.profile.otherInformationList = dto.otherInformationList;
            StaticData.Enrollment.profile.permanentAddress = dto.permanentAddress;
            StaticData.Enrollment.profile.photo = dto.photo;
            StaticData.Enrollment.profile.presentAddress = dto.presentAddress;
            if (matchResultForm.openedFromUI == 1)
            {
                StaticData.Enrollment.profile.id = dto.id;
                StaticData.Enrollment.profile.referenceNo = dto.referenceNo;
            }
            StaticData.Enrollment.profile.religion = dto.religion;
            //StaticData.Enrollment.profile.seizures = dto.seizures;
            StaticData.Enrollment.profile.status = dto.status;

            if (dto.fingerprint != null)
            {
                if (!string.IsNullOrEmpty(dto.fingerprint.lt) && StaticData.Enrollment.profile.biometric?.fingerprint?.lt == null)
                    StaticData.Enrollment.profile.biometric.fingerprint.lt = GetByteDataForStatic(dto.fingerprint.lt);
                if (!string.IsNullOrEmpty(dto.fingerprint.li) && StaticData.Enrollment.profile.biometric?.fingerprint?.li == null)
                    StaticData.Enrollment.profile.biometric.fingerprint.li = GetByteDataForStatic(dto.fingerprint.li);
                if (!string.IsNullOrEmpty(dto.fingerprint.lm) && StaticData.Enrollment.profile.biometric?.fingerprint?.lm == null)
                    StaticData.Enrollment.profile.biometric.fingerprint.lm = GetByteDataForStatic(dto.fingerprint.lm);
                if (!string.IsNullOrEmpty(dto.fingerprint.lr) && StaticData.Enrollment.profile.biometric?.fingerprint?.lr == null)
                    StaticData.Enrollment.profile.biometric.fingerprint.lr = GetByteDataForStatic(dto.fingerprint.lr);
                if (!string.IsNullOrEmpty(dto.fingerprint.ls) && StaticData.Enrollment.profile.biometric?.fingerprint?.ls == null)
                    StaticData.Enrollment.profile.biometric.fingerprint.ls = GetByteDataForStatic(dto.fingerprint.ls);
                if (!string.IsNullOrEmpty(dto.fingerprint.rt) && StaticData.Enrollment.profile.biometric?.fingerprint?.rt == null)
                    StaticData.Enrollment.profile.biometric.fingerprint.rt = GetByteDataForStatic(dto.fingerprint.rt);
                if (!string.IsNullOrEmpty(dto.fingerprint.ri) && StaticData.Enrollment.profile.biometric?.fingerprint?.ri == null)
                    StaticData.Enrollment.profile.biometric.fingerprint.ri = GetByteDataForStatic(dto.fingerprint.ri);
                if (!string.IsNullOrEmpty(dto.fingerprint.rm) && StaticData.Enrollment.profile.biometric?.fingerprint?.rm == null)
                    StaticData.Enrollment.profile.biometric.fingerprint.rm = GetByteDataForStatic(dto.fingerprint.rm);
                if (!string.IsNullOrEmpty(dto.fingerprint.rr) && StaticData.Enrollment.profile.biometric?.fingerprint?.rr == null)
                    StaticData.Enrollment.profile.biometric.fingerprint.rr = GetByteDataForStatic(dto.fingerprint.rr);
                if (!string.IsNullOrEmpty(dto.fingerprint.rs) && StaticData.Enrollment.profile.biometric?.fingerprint?.rs == null)
                    StaticData.Enrollment.profile.biometric.fingerprint.rs = GetByteDataForStatic(dto.fingerprint.rs);
            }

            if (dto.iris != null)
            {
                if (!string.IsNullOrEmpty(dto.iris.left) && StaticData.Enrollment.profile.biometric?.iris?.left == null)
                    StaticData.Enrollment.profile.biometric.iris.left = GetByteDataForStatic(dto.iris.left);
                if (!string.IsNullOrEmpty(dto.iris.right) && StaticData.Enrollment.profile.biometric?.iris?.right == null)
                    StaticData.Enrollment.profile.biometric.iris.right = GetByteDataForStatic(dto.iris.right);
            }

            if (!string.IsNullOrEmpty(dto.photo) && StaticData.Enrollment.profile.biometric?.photo?.photo == null)
                StaticData.Enrollment.profile.biometric.photo.photo = GetByteDataForStatic(dto.photo);

            if (StaticData.Enrollment.profile.crimeInformation == null) StaticData.Enrollment.profile.crimeInformation = new CrimeInformationDto();
            StaticData.Enrollment.profile.crimeInformation.criminal_id = dto.id;   // This id is needed to download exact profile report
        }

        private void CopyToInitialStaticData(ProfileResponseDto dto)
        {
            StaticData.PreviewEnrollment.profile.id = dto.id;
            StaticData.PreviewEnrollment.profile.arrestedBy = dto.arrestedBy;
            StaticData.PreviewEnrollment.profile.arrestDate = dto.arrestDate;
            StaticData.PreviewEnrollment.profile.arrestInfos = dto.arrestInfos;
            StaticData.PreviewEnrollment.profile.crimeInformation = dto.crimeInformation;
            if (StaticData.PreviewEnrollment.profile.crimeInformation == null) StaticData.PreviewEnrollment.profile.crimeInformation = new CrimeInformationDto();
            StaticData.PreviewEnrollment.profile.crimeInformation.criminal_id = dto.id;   // This id is needed to download exact profile report
            StaticData.PreviewEnrollment.profile.investigatingOfficerBPNumber = dto.investigatingOfficerBPNumber;
            StaticData.PreviewEnrollment.profile.investigatingOfficerMobile = dto.investigatingOfficerMobile;
            StaticData.PreviewEnrollment.profile.investigatingOfficerName = dto.investigatingOfficerName;
            StaticData.PreviewEnrollment.profile.iorank = dto.iorank;
            StaticData.PreviewEnrollment.profile.politicalGroup = dto.politicalGroup;
            StaticData.PreviewEnrollment.profile.unit = dto.unit;
            StaticData.PreviewEnrollment.profile.subUnit = dto.subUnit;
            if (dto.complains != null)
            {
                if (dto.complains.Count > 0)
                {
                    List<ComplainDto> complaintList = new List<ComplainDto>();
                    for (int j = 0; j < dto.complains[0]?.files?.Count; j++)
                    {
                        ComplainDto complainDto = new ComplainDto();
                        complainDto.attachmentNumber = j;
                        string[] arr = dto.complains[0].files[j].Split('.');
                        complainDto.extension = "." + arr[1];
                        complainDto.contentType = "image/" + arr[1];
                        complainDto.complaint = GetByteDataForPreviewStatic(dto.complains[0].files[j]);
                        if (complainDto.complaint?.Length > 0)
                        {                            
                            complaintList.Add(complainDto);
                        }
                    }
                    StaticData.PreviewEnrollment.profile.complains = complaintList;
                    StaticData.PreviewEnrollment.profile.attachment.complaintList = complaintList;
                }
            }

            if (dto.seizures != null)
            {
                if (dto.seizures.Count > 0)
                {
                    List<SeizureDto> seizureList = new List<SeizureDto>();
                    for (int j = 0; j < dto.seizures[0]?.files?.Count; j++)
                    {
                        SeizureDto seizureDto = new SeizureDto();
                        seizureDto.attachmentNumber = j;
                        string[] arr = dto.seizures[0].files[j].Split('.');
                        seizureDto.extension = "." + arr[1];
                        seizureDto.contentType = "image/" + arr[1];
                        seizureDto.seizure = GetByteDataForPreviewStatic(dto.seizures[0].files[j]);
                        if (seizureDto.seizure?.Length > 0)
                        {
                            seizureList.Add(seizureDto);
                        }
                    }
                    StaticData.PreviewEnrollment.profile.seizures = seizureList;
                    StaticData.PreviewEnrollment.profile.attachment.seizureList = seizureList;
                }
            }

            if (dto.firs != null)
            {
                if (dto.firs.Count > 0)
                {
                    List<FIRDto> firList = new List<FIRDto>();
                    for (int j = 0; j < dto.firs[0]?.files?.Count; j++)
                    {
                        FIRDto firDto = new FIRDto();
                        firDto.district = dto.firs[0].district;
                        firDto.upozilaOrThana = dto.firs[0].upozilaOrThana;
                        firDto.firDate = dto.firs[0].firDate;
                        firDto.firNo = dto.firs[0].firNo;
                        firDto.attachmentNumber = j;
                        string[] arr = dto.firs[0].files[j].Split('.');
                        firDto.extension = "." + arr[1];
                        firDto.contentType = "image/" + arr[1];
                        firDto.fir = GetByteDataForPreviewStatic(dto.firs[0].files[j]);
                        if (firDto.fir?.Length > 0)
                        {                            
                            firList.Add(firDto);
                        }
                    }
                    StaticData.PreviewEnrollment.profile.firs = firList;
                    StaticData.PreviewEnrollment.profile.attachment.firList = firList;
                }
            }

            StaticData.PreviewEnrollment.profile.createdAt = dto.createdAt;
            StaticData.PreviewEnrollment.profile.createdBy = dto.createdBy;
            StaticData.PreviewEnrollment.profile.dateOfBirth = dto.dateOfBirth;
            StaticData.PreviewEnrollment.profile.age = dto.age;
            StaticData.PreviewEnrollment.profile.nid = dto.nid;
            StaticData.PreviewEnrollment.profile.educationalInformations = dto.educationalInformations;
            StaticData.PreviewEnrollment.profile.familys = dto.familys;
            StaticData.PreviewEnrollment.profile.foreignAddress = dto.foreignAddress;
            StaticData.PreviewEnrollment.profile.fullName = dto.fullName;
            StaticData.PreviewEnrollment.profile.gender = dto.gender;
            StaticData.PreviewEnrollment.profile.hash = dto.hash;
            StaticData.PreviewEnrollment.profile.height = dto.height;
            StaticData.PreviewEnrollment.profile.identificationMark = dto.identificationMark;
            StaticData.PreviewEnrollment.profile.maritalStatus = dto.maritalStatus;
            StaticData.PreviewEnrollment.profile.mobile = dto.mobile;
            StaticData.PreviewEnrollment.profile.nationality = dto.nationality;
            StaticData.PreviewEnrollment.profile.nickName = dto.nickName;
            StaticData.PreviewEnrollment.profile.occupation = dto.occupation;
            StaticData.PreviewEnrollment.profile.otherInformationList = dto.otherInformationList;
            StaticData.PreviewEnrollment.profile.permanentAddress = dto.permanentAddress;
            StaticData.PreviewEnrollment.profile.photo = dto.photo;
            StaticData.PreviewEnrollment.profile.presentAddress = dto.presentAddress;

            StaticData.PreviewEnrollment.profile.religion = dto.religion;
            StaticData.PreviewEnrollment.profile.status = dto.status;

            if (dto.fingerprint != null)
            {
                if (!string.IsNullOrEmpty(dto.fingerprint.lt)) StaticData.PreviewEnrollment.profile.biometric.fingerprint.lt = GetByteDataForPreviewStatic(dto.fingerprint.lt);
                if (!string.IsNullOrEmpty(dto.fingerprint.li)) StaticData.PreviewEnrollment.profile.biometric.fingerprint.li = GetByteDataForPreviewStatic(dto.fingerprint.li);
                if (!string.IsNullOrEmpty(dto.fingerprint.lm)) StaticData.PreviewEnrollment.profile.biometric.fingerprint.lm = GetByteDataForPreviewStatic(dto.fingerprint.lm);
                if (!string.IsNullOrEmpty(dto.fingerprint.lr)) StaticData.PreviewEnrollment.profile.biometric.fingerprint.lr = GetByteDataForPreviewStatic(dto.fingerprint.lr);
                if (!string.IsNullOrEmpty(dto.fingerprint.ls)) StaticData.PreviewEnrollment.profile.biometric.fingerprint.ls = GetByteDataForPreviewStatic(dto.fingerprint.ls);
                if (!string.IsNullOrEmpty(dto.fingerprint.rt)) StaticData.PreviewEnrollment.profile.biometric.fingerprint.rt = GetByteDataForPreviewStatic(dto.fingerprint.rt);
                if (!string.IsNullOrEmpty(dto.fingerprint.ri)) StaticData.PreviewEnrollment.profile.biometric.fingerprint.ri = GetByteDataForPreviewStatic(dto.fingerprint.ri);
                if (!string.IsNullOrEmpty(dto.fingerprint.rm)) StaticData.PreviewEnrollment.profile.biometric.fingerprint.rm = GetByteDataForPreviewStatic(dto.fingerprint.rm);
                if (!string.IsNullOrEmpty(dto.fingerprint.rr)) StaticData.PreviewEnrollment.profile.biometric.fingerprint.rr = GetByteDataForPreviewStatic(dto.fingerprint.rr);
                if (!string.IsNullOrEmpty(dto.fingerprint.rs)) StaticData.PreviewEnrollment.profile.biometric.fingerprint.rs = GetByteDataForPreviewStatic(dto.fingerprint.rs);
            }

            if (dto.iris != null)
            {
                if (!string.IsNullOrEmpty(dto.iris.left)) StaticData.PreviewEnrollment.profile.biometric.iris.left = GetByteDataForPreviewStatic(dto.iris.left);
                if (!string.IsNullOrEmpty(dto.iris.right)) StaticData.PreviewEnrollment.profile.biometric.iris.right = GetByteDataForPreviewStatic(dto.iris.right);
            }

            if (!string.IsNullOrEmpty(dto.photo)) StaticData.PreviewEnrollment.profile.biometric.photo.photo = GetByteDataForPreviewStatic(dto.photo);
        }

        private byte[] GetByteDataForPreviewStatic(string str)
        {
            GetProfileDataByteResponse response = new EnrollmentApiManager().GetByteDataByFilePath(str);
            if (response != null)
            {
                if (response.code == 200 && response.file?.Length > 0)
                {                    
                    return response.file;
                }
            }
            return null;
        }

        private byte[] GetByteDataForStatic(string str)
        {
            GetProfileDataByteResponse response = new EnrollmentApiManager().GetByteDataByFilePath(str);
            if (response != null)
            {
                if (response.code == 200 && response.file?.Length > 0)
                {                    
                    return response.file;
                }
            }
            return null;
        }

        public void SetResultData()
        {
            matchResultForm.criminalIdList = new List<string>();
            for (int i = 0; i < profileList?.Count; i++)
            {
                matchResultForm.criminalIdList.Add(profileList[i].id);
            }
            matchResultForm.ShowMatchResults(profileList, 0);
        }

        public void SetNIDResultData()
        {
            //matchResultForm.ShowNIDMatchResults(becDataResponse);
            if (becDataResponse != null)
            {
                matchResultForm.ShowNIDMatchResults(becDataResponse);
            }
            else
            {
                matchResultForm.ShowFPnidMatchResults(voterInfoList, 0);
            }
        }

        public void GoToNextNidFpMatch()
        {
            if (voterInfoList?.Count > (matchResultForm.NidFPmatchIndex + 1))
                matchResultForm.NidFPmatchIndex += 1;
            matchResultForm.ShowFPnidMatchResults(voterInfoList, matchResultForm.NidFPmatchIndex);
        }

        public void GoToPrevNidFpMatch()
        {
            if (matchResultForm.NidFPmatchIndex > 0)
                matchResultForm.NidFPmatchIndex -= 1;
            matchResultForm.ShowFPnidMatchResults(voterInfoList, matchResultForm.NidFPmatchIndex);
        }

        public void GenerateCombinedReport()
        {
            CriminalReportRequest request = new CriminalReportRequest();
            CriminalProfileOrCombinedReportResponse response = new CriminalProfileOrCombinedReportResponse();

            request.criminalHistoryIdList = matchResultForm.criminalIdList;

            string ProcessingErrorMsg = string.Empty;

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = new ReportApiManager().GetCriminalHistoryReport(request);
                }
                catch (WebException x)
                {
                    logger.Debug("Known error, when server not found when searching criminal. Error Message: " + x.Message);
                    ProcessingErrorMsg = "Seems you are Offline while trying to download report. Please contact with your Administrator.";
                }
                catch (TimeoutException x)
                {
                    logger.Debug("Connection timedout when searching criminal. Error Message: " + x.Message);
                    ProcessingErrorMsg = "Seems you are Offline while trying to download report. Please contact with your Administrator.";
                }
                catch (Exception x)
                {
                    logger.Error("There was an unexpected error when searching criminal.\n" + x.ToString());
                    ProcessingErrorMsg = "There was an unexpected error when trying to download report. Please contact with your Administrator.";
                }
            });

            if (!string.IsNullOrEmpty(ProcessingErrorMsg))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", ProcessingErrorMsg);
                return;
            }

            string errorMsg = string.Empty;

            if (response != null)
            {
                if (response.code == 200)
                {
                    if (response.reportResultList?.Count > 0)
                    {
                        string url = response.reportResultList[0].url;
                        if (!string.IsNullOrEmpty(url) && response.reportResultList[0].status == "TRUE")
                        {
                            try
                            {
                                Process.Start(url);
                            }
                            catch (Exception x)
                            {
                                logger.Error("Could not download combined report. " + x.ToString());
                                errorMsg = "Could not download report";
                            }
                        }
                        else
                        {
                            errorMsg = "Could not download report";
                        }
                    }
                    else
                    {
                        errorMsg = "Could not download report";
                    }
                }
                else
                {
                    errorMsg = "Could not download report";
                }
            }
            else
            {
                errorMsg = "Could not download report";
            }

            if (!string.IsNullOrEmpty(errorMsg))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", errorMsg);
            }
        }
    }
}
