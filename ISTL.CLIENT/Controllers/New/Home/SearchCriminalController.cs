using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Net;
using ISTL.COMMON;
using ISTL.COMMON.Network;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.MODELS.DTO.New.Enrollment.CrimeInformation;
using ISTL.MODELS.DTO.Search;
using ISTL.MODELS.Request.New;
using ISTL.MODELS.Response.New;
using ISTL.MODELS.Response.New.Enrollment;
using ISTL.PERSOGlobals;
using ISTL.RAB.ApiManager;
using ISTL.RAB.Entity;
using ISTL.RAB.View;
using ISTL.RAB.View.New;
using ISTL.RAB.View.New.Home;
//using Minio;
using NLog;

namespace ISTL.RAB.Controllers.New.Home
{
    public class SearchCriminalController : ViewController, SearchCriminalUserControl.ISearchCriminalUser
    {
        private SearchCriminalUserControl searchCriminalUserControl;
        private Logger logger = LogManager.GetCurrentClassLogger();

        public SearchCriminalController()
        {
            searchCriminalUserControl = new SearchCriminalUserControl();
            base.SetView((IView)searchCriminalUserControl);
            searchCriminalUserControl.SetController(this);
        }

        public override string GetName()
        {
            return Globals.ChildControllers.SEARCH_CRIMINAL;
        }


        public void OnSearch(SearchCriminalDto searchCriminalDto)
        {
            ClearTotalRecordsAndDataGrid();

            if (string.IsNullOrEmpty(searchCriminalDto.FullName))
            {
                searchCriminalDto.FullName = null;
            }

            if (string.IsNullOrEmpty(searchCriminalDto.ReferenceNumber))
            {
                searchCriminalDto.ReferenceNumber = null;
            }

            ProfileSearchSummaryResponse profileSearchSummaryResponse = new ProfileSearchSummaryResponse();
            ProcessingDialog.Run(delegate ()
            {
                profileSearchSummaryResponse = new EnrollmentApiManager().SearchCriminalUser(searchCriminalDto);
            });

            PopulateSearchProfileSummariesDataGrid(profileSearchSummaryResponse);
        }

        public void OnSearchNew(DemographicSearchDto dto)
        {
            ClearTotalRecordsAndDataGrid();
            position = dto.startIndex * 10;
            ProfileSearchSummaryResponse response = new ProfileSearchSummaryResponse();

            if (string.IsNullOrEmpty(Users.AccessToken))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "You are logged in offilne");
                return;
            }

            string errorMsg = null;

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = new EnrollmentApiManager().DemographicSearch(dto);
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

            PopulateSearchProfileSummariesDataGrid(response);
        }
        int position = 0;
        //public void OnSearchProfile(string referenceNo)
        public void OnSearchProfile(string criminalId)
        {
            if (string.IsNullOrEmpty(Users.AccessToken))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "You are logged in offilne");
                return;
            }

            ProfileSearchListResponse profileSearchListResponse = new ProfileSearchListResponse();

            string errorMsg = null;

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    //profileSearchListResponse = new EnrollmentApiManager().SearchProfileList(referenceNo, null);
                    profileSearchListResponse = new EnrollmentApiManager().SearchProfileList(null, criminalId);
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

            if(profileSearchListResponse.code != (int)HttpResponseStatus.OK)
            {
                //logger.Error("There was an error when profile search by API Call for ref. no: " + referenceNo + "\nAPI Error Message:\n" + profileSearchListResponse.message);
                logger.Error("There was an error when profile search by API Call for criminal_id " + criminalId + "\nAPI Error Message:\n" + profileSearchListResponse.message);
            }

            if (profileSearchListResponse != null)
            {
                if (profileSearchListResponse.total > 0)
                {
                    //StaticData.Enrollment.profile = profileSearchListResponse.profileList[0];
                    ProcessingDialog.Run(delegate ()
                    {
                        StaticData.Enrollment = CopyToStaticData(profileSearchListResponse.profileList[0]);
                    });
                }
                else
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not fetch Profile.");
                    return;
                }
            }
            else
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not fetch Profile.");
                return;
            }
            StaticData.ModifiableNormalEnrollment = false;
            ((MainController)parent).SearchCriteriaBeforeEnrollment = Globals.SearchCriteriaBeforeEnrollment.CDMS_Search;
            StaticData.MoveFromBiometricEntryPageWithoutCDMSsearch = true;
            parent.AddChild(Globals.ChildControllers.ENROLL);
        }

        public void OnPreviewProfile(string criminalId)
        {
            if (string.IsNullOrEmpty(Users.AccessToken))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "You are logged in offilne");
                return;
            }

            ProfileSearchListResponse profileSearchListResponse = new ProfileSearchListResponse();

            string errorMsg = string.Empty;

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    profileSearchListResponse = new EnrollmentApiManager().SearchProfileList(null, criminalId);
                }
                catch (WebException x)
                {
                    logger.Debug("Known error, when server not found when searching criminal. Error Message: " + x.Message);
                    errorMsg = "Web Exception while searching criminal. Please contact with your Administrator.";
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

            if (profileSearchListResponse.code != (int)HttpResponseStatus.OK)
            {
                logger.Error("There was an error when profile search by API Call for criminal_id " + criminalId + "\nAPI Error Message:\n" + profileSearchListResponse.message);
            }

            if (!string.IsNullOrEmpty(errorMsg))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", errorMsg);
                return;
            }

            if (profileSearchListResponse != null)
            {
                if (profileSearchListResponse.total > 0)
                {
                    ProcessingDialog.Run(delegate ()
                    {
                        StaticData.PreviewEnrollment = CopyToStaticData(profileSearchListResponse.profileList[0]);
                    });
                }
                else
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not fetch Profile.");
                    return;
                }
            }
            else
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not fetch Profile.");
                return;
            }
            var previewForm = new GeneralProfilePreviewForm();
            previewForm.OpenedFromUI = 1;
            previewForm.ShowDialog();
        }        

        private EnrollmentDto CopyToStaticData(ProfileResponseDto dto)
        {
            EnrollmentDto ResultEnrollmentDto = new EnrollmentDto();

            ResultEnrollmentDto.profile.id = dto.id;
            ResultEnrollmentDto.profile.arrestedBy = dto.arrestedBy;
            ResultEnrollmentDto.profile.arrestDate = dto.arrestDate;
            ResultEnrollmentDto.profile.arrestInfos = dto.arrestInfos;
            ResultEnrollmentDto.profile.createdAt = dto.createdAt;
            ResultEnrollmentDto.profile.createdBy = dto.createdBy;
            ResultEnrollmentDto.profile.crimeInformation = dto.crimeInformation;
            ResultEnrollmentDto.profile.dateOfBirth = dto.dateOfBirth;
            ResultEnrollmentDto.profile.age = dto.age;
            ResultEnrollmentDto.profile.nid = dto.nid;
            ResultEnrollmentDto.profile.educationalInformations = dto.educationalInformations;
            ResultEnrollmentDto.profile.familys = dto.familys;
            ResultEnrollmentDto.profile.foreignAddress = dto.foreignAddress;
            ResultEnrollmentDto.profile.fullName = dto.fullName;
            ResultEnrollmentDto.profile.gender = dto.gender;
            ResultEnrollmentDto.profile.hash = dto.hash;
            ResultEnrollmentDto.profile.height = dto.height;
            ResultEnrollmentDto.profile.identificationMark = dto.identificationMark;
            ResultEnrollmentDto.profile.investigatingOfficerBPNumber = dto.investigatingOfficerBPNumber;
            ResultEnrollmentDto.profile.investigatingOfficerMobile = dto.investigatingOfficerMobile;
            ResultEnrollmentDto.profile.investigatingOfficerName = dto.investigatingOfficerName;
            ResultEnrollmentDto.profile.iorank = dto.iorank;
            ResultEnrollmentDto.profile.maritalStatus = dto.maritalStatus;
            ResultEnrollmentDto.profile.mobile = dto.mobile;
            ResultEnrollmentDto.profile.nationality = dto.nationality;
            ResultEnrollmentDto.profile.nickName = dto.nickName;
            ResultEnrollmentDto.profile.occupation = dto.occupation;
            ResultEnrollmentDto.profile.otherInformationList = dto.otherInformationList;
            ResultEnrollmentDto.profile.permanentAddress = dto.permanentAddress;
            ResultEnrollmentDto.profile.photo = dto.photo;
            ResultEnrollmentDto.profile.politicalGroup = dto.politicalGroup;
            ResultEnrollmentDto.profile.presentAddress = dto.presentAddress;
            ResultEnrollmentDto.profile.referenceNo = dto.referenceNo;
            ResultEnrollmentDto.profile.religion = dto.religion;
            ResultEnrollmentDto.profile.status = dto.status;
            ResultEnrollmentDto.profile.unit = dto.unit;
            ResultEnrollmentDto.profile.subUnit = dto.subUnit;

            if (dto.fingerprint != null)
            {
                if (!string.IsNullOrEmpty(dto.fingerprint.lt))
                    ResultEnrollmentDto.profile.biometric.fingerprint.lt = GetByteDataForStatic(dto.fingerprint.lt);
                if (!string.IsNullOrEmpty(dto.fingerprint.li))
                    ResultEnrollmentDto.profile.biometric.fingerprint.li = GetByteDataForStatic(dto.fingerprint.li);
                if (!string.IsNullOrEmpty(dto.fingerprint.lm))
                    ResultEnrollmentDto.profile.biometric.fingerprint.lm = GetByteDataForStatic(dto.fingerprint.lm);
                if (!string.IsNullOrEmpty(dto.fingerprint.lr))
                    ResultEnrollmentDto.profile.biometric.fingerprint.lr = GetByteDataForStatic(dto.fingerprint.lr);
                if (!string.IsNullOrEmpty(dto.fingerprint.ls))
                    ResultEnrollmentDto.profile.biometric.fingerprint.ls = GetByteDataForStatic(dto.fingerprint.ls);
                if (!string.IsNullOrEmpty(dto.fingerprint.rt))
                    ResultEnrollmentDto.profile.biometric.fingerprint.rt = GetByteDataForStatic(dto.fingerprint.rt);
                if (!string.IsNullOrEmpty(dto.fingerprint.ri))
                    ResultEnrollmentDto.profile.biometric.fingerprint.ri = GetByteDataForStatic(dto.fingerprint.ri);
                if (!string.IsNullOrEmpty(dto.fingerprint.rm))
                    ResultEnrollmentDto.profile.biometric.fingerprint.rm = GetByteDataForStatic(dto.fingerprint.rm);
                if (!string.IsNullOrEmpty(dto.fingerprint.rr))
                    ResultEnrollmentDto.profile.biometric.fingerprint.rr = GetByteDataForStatic(dto.fingerprint.rr);
                if (!string.IsNullOrEmpty(dto.fingerprint.rs))
                    ResultEnrollmentDto.profile.biometric.fingerprint.rs = GetByteDataForStatic(dto.fingerprint.rs);
            }

            if (dto.iris != null)
            {
                if (!string.IsNullOrEmpty(dto.iris.left)) ResultEnrollmentDto.profile.biometric.iris.left = GetByteDataForStatic(dto.iris.left);
                if (!string.IsNullOrEmpty(dto.iris.right)) ResultEnrollmentDto.profile.biometric.iris.right = GetByteDataForStatic(dto.iris.right);
            }

            if (!string.IsNullOrEmpty(dto.photo)) ResultEnrollmentDto.profile.biometric.photo.photo = GetByteDataForStatic(dto.photo);

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
                        else complainDto.contentType = "image/" + arr[1];
                        complainDto.complaint = GetByteDataForStatic(dto.complains[0].files[j]);
                        if (complainDto.complaint?.Length > 0)
                        {
                            complaintList.Add(complainDto);
                        }
                    }
                    ResultEnrollmentDto.profile.complains = complaintList;
                    ResultEnrollmentDto.profile.attachment.complaintList = complaintList;
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
                        else seizureDto.contentType = "image/" + arr[1];
                        seizureDto.seizure = GetByteDataForStatic(dto.seizures[0].files[j]);
                        if (seizureDto.seizure?.Length > 0)
                        {
                            seizureList.Add(seizureDto);
                        }
                    }
                    ResultEnrollmentDto.profile.seizures = seizureList;
                    ResultEnrollmentDto.profile.attachment.seizureList = seizureList;
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
                        else firDto.contentType = "image/" + arr[1];
                        firDto.fir = GetByteDataForStatic(dto.firs[0].files[j]);
                        if (firDto.fir?.Length > 0)
                        {
                            firList.Add(firDto);
                        }
                    }
                    ResultEnrollmentDto.profile.firs = firList;
                    ResultEnrollmentDto.profile.attachment.firList = firList;
                }
            }

            if (ResultEnrollmentDto.profile.crimeInformation == null) ResultEnrollmentDto.profile.crimeInformation = new CrimeInformationDto();
            ResultEnrollmentDto.profile.crimeInformation.criminal_id = dto.id;   // This id is needed to download exact profile report

            return ResultEnrollmentDto;
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

        private void ClearTotalRecordsAndDataGrid()
        {
            searchCriminalUserControl.labelTotalRecords1.Text = "0";
            searchCriminalUserControl.dgvSearchProfileSummaries1.Rows.Clear();
        }

        private void PopulateSearchProfileSummariesDataGrid(ProfileSearchSummaryResponse profileSearchSummaryResponse)
        {
            if (profileSearchSummaryResponse?.code != 200 || profileSearchSummaryResponse?.criminalProfileSummaryList == null)
            {
                return;
            }
            searchCriminalUserControl.totalCount = Convert.ToInt32(profileSearchSummaryResponse.total);
            searchCriminalUserControl.labelTotalRecords1.Text = "" + searchCriminalUserControl.totalCount;
            UpdateTotalRecordsFoundAndPopulateSummariesDataGrid(profileSearchSummaryResponse.criminalProfileSummaryList);
        }

        private void UpdateTotalRecordsFoundAndPopulateSummariesDataGrid(List<CriminalProfileSummaryList> criminalProfileSummaryLists)
        {
            int criminalProfileSummariesCount = criminalProfileSummaryLists.Count;

            for (int serial = 0; serial < criminalProfileSummaryLists.Count; serial++)
            {
                CriminalProfileSummaryList criminalProfileSummaryList = criminalProfileSummaryLists[serial];

                string dob = criminalProfileSummaryList.dateOfBirth;
                if (!string.IsNullOrEmpty(criminalProfileSummaryList.dateOfBirth))
                {
                    try
                    {
                        DateTime DOB = DateTime.ParseExact(criminalProfileSummaryList.dateOfBirth, "yyyy-MM-ddTHH:mm:ss.fffK", System.Globalization.CultureInfo.InvariantCulture);
                        dob = DOB.ToString("dd/MM/yyyy");
                    }
                    catch (Exception)
                    {

                    }
                }

                //string ActionName = string.Empty;
                //if (criminalProfileSummaryList.createdBy == Users.Id) ActionName = "Edit";
                //else ActionName = "Preview";

                searchCriminalUserControl.dgvSearchProfileSummaries1.Rows.Add(
                    position + serial + 1,
                    criminalProfileSummaryList.referenceNo,
                    criminalProfileSummaryList.fullName,
                    dob,
                    criminalProfileSummaryList.gender,
                    criminalProfileSummaryList.religion,
                    "Preview",
                    "Edit",
                    criminalProfileSummaryList.id,
                    criminalProfileSummaryList.createdBy);

                if (criminalProfileSummaryList.createdBy != Users.Id)
                {
                    //searchCriminalUserControl.dgvSearchProfileSummaries1.Rows[serial].DefaultCellStyle.BackColor = Color.IndianRed;
                }
                else
                {
                    searchCriminalUserControl.dgvSearchProfileSummaries1.Rows[serial].DefaultCellStyle.BackColor = Color.FromArgb(202, 214, 192);
                }
            }
        }

        public void GoBacktoDashboard()
        {
            ((MainController)parent).OnHome();
        }
    }
}