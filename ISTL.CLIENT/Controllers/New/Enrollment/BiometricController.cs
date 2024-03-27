using ISTL.COMMON;
using ISTL.COMMON.Network;
using ISTL.COMMON.Subscription;
using ISTL.COMMON.Threads;
using ISTL.MODELS.DTO.Enrollment;
using ISTL.MODELS.DTO.New.Enrollment.BECverify;
using ISTL.MODELS.DTO.New.Enrollment.Biometric;
using ISTL.MODELS.DTO.Search;
using ISTL.MODELS.Request.Adjudication;
using ISTL.MODELS.Request.New;
using ISTL.MODELS.Response;
using ISTL.MODELS.Response.New;
using ISTL.MODELS.Response.New.Enrollment;
using ISTL.MODELS.Response.New.JailDBBioMatch;
using ISTL.MODELS.Response.Old.Adjudication;
using ISTL.PERSOGlobals;
using ISTL.RAB.ApiManager;
using ISTL.RAB.Asynch;
using ISTL.RAB.Controllers.New.Home;
using ISTL.RAB.DbManager;
using ISTL.RAB.Entity;
using ISTL.RAB.View;
using ISTL.RAB.View.New;
using ISTL.RAB.View.New.Home;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace ISTL.RAB.Controllers.New.Enrollment
{
    public class BiometricController : ViewController
    {
        private BiometricUserControl biometricUserControl;
        private Logger logger = LogManager.GetCurrentClassLogger();
        public GetMatchListRequest request = new GetMatchListRequest();
        public GetMatchListIdsResponse response = new GetMatchListIdsResponse();
        public string ReferenceNumber;
        private readonly string GetMatchListIdsEndpoint = ConfigurationManager.AppSettings["GetMatchListIDsEndpoint"].ToString();
        private readonly string EnrollmentAddEndpoint = ConfigurationManager.AppSettings["EnrollmentAddEndpoint"].ToString();
        private readonly string NidIdentifyFPEndpoint = ConfigurationManager.AppSettings["GetBECidentifyEndpoint"].ToString();
        private MatchResultController matchResultController = new MatchResultController();
        private NidSearchSubject nidSearchSubject = (NidSearchSubject)SubjectFactory.GetInstance().GetSubject(NidSearchSubject.Name);
        private DbBecManager dbBecManager = new DbBecManager();

        public BiometricController()
        {
            biometricUserControl = new BiometricUserControl();
            base.SetView((IView)biometricUserControl);
            biometricUserControl.SetController(this);
        }

        public override string GetName()
        {
            return Globals.ChildControllers.BIOMETRIC;
        }

        public int CheckSearchCriteria()
        {
            if (((MainController)parent).SearchCriteriaBeforeEnrollment > 0)
            {
                return ((MainController)parent).SearchCriteriaBeforeEnrollment;
            }
            return 0;
        }
        public void GoToCriminalProfile()
        {
            parent.AddChild(Globals.ChildControllers.ENROLL);
        }

        public void Family()
        {
            parent.AddChild(Globals.ChildControllers.OTHER_INFO);
        }

        public void Biometric()
        {
            ((MainController)parent).SearchCriteriaBeforeEnrollment = Globals.SearchCriteriaBeforeEnrollment.CDMS_Search;
            parent.AddChild(Globals.ChildControllers.BIOMETRIC);
        }

        public void PreviewSubmit()
        {
            parent.AddChild(Globals.ChildControllers.PREVIEW_SUBMIT);
        }

        public void OnIdentifyNIDByBiometric()
        {
            try
            {
                StaticData.VoterInfos = null;

                if (StaticData.Enrollment.profile?.biometric?.fingerprint?.rt == null &&
                    StaticData.Enrollment.profile?.biometric?.fingerprint?.ri == null &&
                    StaticData.Enrollment.profile?.biometric?.fingerprint?.lt == null &&
                    StaticData.Enrollment.profile?.biometric?.fingerprint?.li == null)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please provide at least any of the four: " +
                        "left thumb, left index, right thumb, right index for NID search");
                    return;
                }

                nidSearchSubject.State = NidSearchSubject.Status.PENDING;
                nidSearchSubject.Notify();

                Task.Run(() => IdentifyNIDByBiometricAsynch());

                InfoMessageBox.ShowMessage("SNSOP TOOLS", "Search by Fingerprint in NID Server is processing in background.");
            }
            catch (Exception ex)
            {
                logger.Error("There was an unexpected error during NID Identification. Error:\n" + ex.ToString());
            }
        }

        private void IdentifyNIDByBiometricAsynch()
        {            
            GetBECidentifyResponse response = new GetBECidentifyResponse();
            GetBECidentifyRequest request = new GetBECidentifyRequest();

            if (biometricUserControl.getBECidentifyrequest != null)
            {
                request = biometricUserControl.getBECidentifyrequest;
                string deviceName = null;
                DbDeviceManager dManager = new DbDeviceManager();
                try
                {
                    deviceName = dManager.GetDevice("FP").Name;
                }
                catch (Exception ex)
                {

                }
                if (!string.IsNullOrEmpty(deviceName))
                {
                    if (deviceName.Contains("Futronic")) request.isInverted = true;
                }
                else
                {
                    request.isInverted = false;
                }
            }

            string errorMsg = string.Empty;

            try
            {
                response = new BECApiManager().DoProfileRequestByNidBiometric(request);
            }
            catch (WebException x)
            {
                logger.Debug("Known error, when server not found when getting token for NID Identification Request by API call. Error Message: " + x.Message);
                errorMsg = "Seems you are Offline while getting token for NID Identification Request by API call. Please contact with your Administrator.";
            }
            catch (TimeoutException x)
            {
                logger.Debug("Connection timedout when getting token for NID Identification Request by API call. Error Message: " + x.Message);
                errorMsg = "Seems you are Offline while getting token for NID Identification Request by API call. Please contact with your Administrator.";
            }
            catch (Exception x)
            {
                logger.Error("There was an unexpected error when getting token for NID Identification Request by API call.\n" + x.ToString());
                errorMsg = "There was an unexpected error when getting token for NID Identification Request by API call. Please contact with your Administrator.";
            }

            if (!string.IsNullOrEmpty(errorMsg))
            {
                logger.Error(errorMsg);
                nidSearchSubject.State = NidSearchSubject.Status.FAILED;
                nidSearchSubject.Notify();

                CustomMessageBox.ShowMessage("SNSOP TOOLS", errorMsg);
                return;
            }

            if (response != null && response?.code == (int)HttpResponseStatus.OK)
            {
                List<BECvoterInfoDto> list = new List<BECvoterInfoDto>();
                BECvoterInfoDto obj = new BECvoterInfoDto()
                {
                    token = response.message,
                    dob = null,
                    status = (int)NidSearchSubject.Status.PENDING,
                    createdAt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffK")
                };

                list.Add(obj);
                var ret = dbBecManager.AddRequest(list);
                if(ret)
                {
                    logger.Info("Successfully saved in the db for NID Identification Request for Token: " + response.message);
                }
                else
                {
                    logger.Error("Failed to save in the db for NID Identification Request for Token: " + response.message);
                }

                // Get nid search pending count
                var pendingList = dbBecManager.GetRequestList("PENDING", 0, 0);

                if (pendingList != null && pendingList.Count > 0)
                {
                    // Start bec server nid search result check
                    ThreadHandler.GetInstance(new BecNidIdentificationAsynch()).StartThread();
                }
            }
            else
            {
                nidSearchSubject.State = NidSearchSubject.Status.FAILED;
                nidSearchSubject.Notify();

                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Search by Fingerprint failed. Server's Error Message: " + response?.message);
                logger.Error("Response of NID Identification Request is null or having error when getting token for NID Identification Request by API call. Please check your network. Server's Error Message: " + response?.message);
            }
        }

        public void IdentifyNIDByBiometric()
        {
            if (StaticData.Enrollment.profile?.biometric?.fingerprint?.rt == null &&
                StaticData.Enrollment.profile?.biometric?.fingerprint?.ri == null &&
                StaticData.Enrollment.profile?.biometric?.fingerprint?.lt == null &&
                StaticData.Enrollment.profile?.biometric?.fingerprint?.li == null)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please provide at least any of the four:-" +
                    "left thumb, left index, right thumb, right index for NID search");
                return;
            }

            GetBECidentifyResponse response = new GetBECidentifyResponse();
            GetBECidentifyRequest request = new GetBECidentifyRequest();

            if (biometricUserControl.getBECidentifyrequest != null)
            {
                request = biometricUserControl.getBECidentifyrequest;
                string deviceName = null;
                DbDeviceManager dManager = new DbDeviceManager();
                try
                {
                    deviceName = dManager.GetDevice("FP").Name;
                }
                catch (Exception ex)
                {

                }
                if (deviceName.Contains("Futronic")) request.isInverted = true;
                else
                {
                    request.isInverted = false;
                }
            }

            //if (StaticData.Enrollment?.profile?.biometric?.fingerprint?.rt != null) 
            //    request.wsqRt = StaticData.Enrollment?.profile?.biometric?.fingerprint?.rt;
            //if (StaticData.Enrollment?.profile?.biometric?.fingerprint?.ri != null)
            //    request.wsqRi = StaticData.Enrollment?.profile?.biometric?.fingerprint?.ri;
            //if (StaticData.Enrollment?.profile?.biometric?.fingerprint?.rm != null)
            //    request.wsqRm = StaticData.Enrollment?.profile?.biometric?.fingerprint?.rm;
            //if (StaticData.Enrollment?.profile?.biometric?.fingerprint?.rr != null)
            //    request.wsqRr = StaticData.Enrollment?.profile?.biometric?.fingerprint?.rr;
            //if (StaticData.Enrollment?.profile?.biometric?.fingerprint?.rs != null)
            //    request.wsqRl = StaticData.Enrollment?.profile?.biometric?.fingerprint?.rs;
            //if (StaticData.Enrollment?.profile?.biometric?.fingerprint?.lt != null)
            //    request.wsqLt = StaticData.Enrollment?.profile?.biometric?.fingerprint?.lt;
            //if (StaticData.Enrollment?.profile?.biometric?.fingerprint?.li != null)
            //    request.wsqLi = StaticData.Enrollment?.profile?.biometric?.fingerprint?.li;
            //if (StaticData.Enrollment?.profile?.biometric?.fingerprint?.lm != null)
            //    request.wsqLm = StaticData.Enrollment?.profile?.biometric?.fingerprint?.lm;
            //if (StaticData.Enrollment?.profile?.biometric?.fingerprint?.lr != null)
            //    request.wsqLr = StaticData.Enrollment?.profile?.biometric?.fingerprint?.lr;
            //if (StaticData.Enrollment?.profile?.biometric?.fingerprint?.rs != null)
            //    request.wsqLl = StaticData.Enrollment?.profile?.biometric?.fingerprint?.ls;


            string errorMsg = string.Empty;

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = new BECApiManager().DoProfileRequestByNidBiometric(request);
                }
                catch (WebException x)
                {
                    logger.Debug("Known error, when server not found when searching criminal. Error Message: " + x.Message);
                    errorMsg = "Seems you are Offline while searching by nid fp. Please contact with your Administrator.";
                }
                catch (TimeoutException x)
                {
                    logger.Debug("Connection timedout when searching criminal. Error Message: " + x.Message);
                    errorMsg = "Seems you are Offline while searching by nid fp. Please contact with your Administrator.";
                }
                catch (Exception x)
                {
                    logger.Error("There was an unexpected error when searching criminal.\n" + x.ToString());
                    errorMsg = "There was an unexpected error when searching by nid fp. Please contact with your Administrator.";
                }
            });

            if (!string.IsNullOrEmpty(errorMsg))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", errorMsg);
                return;
            }

            if (response != null)
            {
                if (response.total != null)
                {
                    if (response.total > 0)
                    {
                        matchResultController.matchResultForm.MatchedByNID = true;
                        matchResultController.matchResultForm.openedFromUI = 0;
                        matchResultController.becDataResponse = null;
                        matchResultController.voterInfoList = response.payloads;
                        DialogResult dr = matchResultController.matchResultForm.ShowDialog();
                        if (dr == DialogResult.OK)
                        {
                            biometricUserControl.SetPhoto();
                            biometricUserControl.UpdateBiometricDataLocal();
                        }
                        else
                        {
                            string refNo = StaticData.Enrollment?.profile?.referenceNo;
                            StaticData.Enrollment.profile = new MODELS.DTO.New.Enrollment.ProfileDto();
                            StaticData.Enrollment.profile.referenceNo = refNo;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(response.message))
                        {
                            CustomMessageBox.ShowMessage("SNSOP TOOLS", "No Match found");
                        }
                        else
                        {
                            CustomMessageBox.ShowMessage("SNSOP TOOLS", response.message);
                        }
                        return;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(response.message))
                    {
                        CustomMessageBox.ShowMessage("SNSOP TOOLS", "No Match found");
                    }
                    else
                    {
                        CustomMessageBox.ShowMessage("SNSOP TOOLS", response.message);
                    }
                    return;
                }
            }
        }

        public void SearchByNID(string nid, string dob)
        {
            if (string.IsNullOrEmpty(Users.AccessToken))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "You are logged in offline");
                return;
            }

            GetBECverifyResponse response = new GetBECverifyResponse();
            GetBECverifyRequest request = new GetBECverifyRequest();

            request.nid = nid;
            request.dob = dob;

            string errorMsg = string.Empty;

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = new BECApiManager().GetProfileByNid(request);
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

            if (response != null)
            {
                if (response.code == 200)
                {
                    if (response.data != null)
                    {
                        matchResultController.matchResultForm.MatchedByNID = true;
                        matchResultController.matchResultForm.openedFromUI = 0;
                        matchResultController.becDataResponse = response.data;
                        DialogResult dr = matchResultController.matchResultForm.ShowDialog();
                        if (dr == DialogResult.OK)
                        {
                            biometricUserControl.SetPhoto();
                            biometricUserControl.UpdateBiometricDataLocal();
                        }
                        else
                        {
                            string refNo = StaticData.Enrollment?.profile?.referenceNo;
                            StaticData.Enrollment.profile = new MODELS.DTO.New.Enrollment.ProfileDto();
                            StaticData.Enrollment.profile.referenceNo = refNo;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(response.message))
                        {
                            CustomMessageBox.ShowMessage("SNSOP TOOLS", "No match found");
                        }
                        else
                        {
                            CustomMessageBox.ShowMessage("SNSOP TOOLS", response.message);
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(response.message))
                    {
                        CustomMessageBox.ShowMessage("SNSOP TOOLS", "No match found");
                    }
                    else
                    {
                        CustomMessageBox.ShowMessage("SNSOP TOOLS", response.message);
                    }
                }
            }
        }

        public bool ValidBiometricSearch = false;

        //public void SearchByBiometric()
        public void SearchByBiometric(int flag)
        {
            if (StaticData.Enrollment.profile?.biometric?.photo?.photo == null &&
                StaticData.Enrollment.profile?.biometric?.fingerprint?.rt == null && StaticData.Enrollment.profile?.biometric?.fingerprint?.ri == null &&
                StaticData.Enrollment.profile?.biometric?.fingerprint?.rm == null && StaticData.Enrollment.profile?.biometric?.fingerprint?.rr == null &&
                StaticData.Enrollment.profile?.biometric?.fingerprint?.rs == null && StaticData.Enrollment.profile?.biometric?.fingerprint?.lt == null &&
                StaticData.Enrollment.profile?.biometric?.fingerprint?.li == null && StaticData.Enrollment.profile?.biometric?.fingerprint?.lm == null &&
                StaticData.Enrollment.profile?.biometric?.fingerprint?.lr == null && StaticData.Enrollment.profile?.biometric?.fingerprint?.ls == null &&
                StaticData.Enrollment.profile?.biometric?.iris?.left == null && StaticData.Enrollment.profile?.biometric?.iris?.right == null)
            {
                ValidBiometricSearch = false;
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Biometric data is needed for search");
                return;
            }

            ValidBiometricSearch = true;

            if (string.IsNullOrEmpty(Users.AccessToken))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "You are logged in offilne");
                return;
            }

            string erroMsg = string.Empty;

            EnrollmentAddResponse response = new EnrollmentAddResponse();
            PersonDataDto request = new PersonDataDto();

            request.createdBy = Users.FullName;
            request.createdOn = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

            PersonBiometricDto bioDto = new PersonBiometricDto();

            if (flag == 3)
            {
                bioDto.leftIris = StaticData.Enrollment.profile?.biometric?.iris?.left;
                bioDto.rightIris = StaticData.Enrollment.profile?.biometric?.iris?.right;
            }
            else if (flag == 2)
            {
                bioDto.photo = StaticData.Enrollment.profile?.biometric?.photo?.photo;
            }
            else if (flag == 1)
            {
                bioDto.wsqRt = StaticData.Enrollment.profile?.biometric?.fingerprint?.rt;
                bioDto.wsqRi = StaticData.Enrollment.profile?.biometric?.fingerprint?.ri;
                bioDto.wsqRm = StaticData.Enrollment.profile?.biometric?.fingerprint?.rm;
                bioDto.wsqRr = StaticData.Enrollment.profile?.biometric?.fingerprint?.rr;
                bioDto.wsqRl = StaticData.Enrollment.profile?.biometric?.fingerprint?.rs;

                bioDto.wsqLt = StaticData.Enrollment.profile?.biometric?.fingerprint?.lt;
                bioDto.wsqLi = StaticData.Enrollment.profile?.biometric?.fingerprint?.li;
                bioDto.wsqLm = StaticData.Enrollment.profile?.biometric?.fingerprint?.lm;
                bioDto.wsqLr = StaticData.Enrollment.profile?.biometric?.fingerprint?.lr;
                bioDto.wsqLl = StaticData.Enrollment.profile?.biometric?.fingerprint?.ls;
            }

            string searchedByCriteria = string.Empty;

            if (bioDto.photo != null) searchedByCriteria += "Photo";
            if (bioDto.wsqLt != null || bioDto.wsqLi != null || bioDto.wsqLm != null || bioDto.wsqLr != null || bioDto.wsqLl != null
                || bioDto.wsqRt != null || bioDto.wsqRi != null || bioDto.wsqRm != null || bioDto.wsqRr != null || bioDto.wsqRl != null)
            {
                if (!string.IsNullOrEmpty(searchedByCriteria)) searchedByCriteria += ", Fingerprint";
                else searchedByCriteria += "Fingerprint";
            }
            if (bioDto.leftIris != null || bioDto.rightIris != null)
            {
                if (!string.IsNullOrEmpty(searchedByCriteria)) searchedByCriteria += ", Iris";
                else searchedByCriteria += "Iris";
            }

            request.personBiometric = bioDto;

            //JavaScriptSerializer jSerial = new JavaScriptSerializer();
            //jSerial.MaxJsonLength = 999999999;

            //var json = jSerial.Serialize(request);

            string errorMsg = string.Empty;

            GetMatchListIdsResponse getMatchListIdsResponse = new GetMatchListIdsResponse();

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    getMatchListIdsResponse = new BiometricMatchApiManager().GetMatchByBiometric(request);
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

            if (getMatchListIdsResponse != null)
            {
                if (getMatchListIdsResponse.operationResult)
                {
                    if (getMatchListIdsResponse.total > 0)
                    {
                        if (getMatchListIdsResponse.matchList != null)
                        {
                            try
                            {
                                getMatchListIdsResponse.matchList = getMatchListIdsResponse.matchList.OrderByDescending(x => x.matchScore).ToList();
                            }
                            catch { }
                            matchResultController.matchResultForm.profileList.Clear();
                            matchResultController.matchResultForm.matchScoreList.Clear();
                            matchResultController.matchResultForm.profileList = new List<MODELS.DTO.New.Enrollment.ProfileResponseDto>();
                            matchResultController.matchResultForm.matchScoreList = new List<int>();
                            ProcessingDialog.Run(delegate ()
                            {
                                for (int i = 0; i < getMatchListIdsResponse.matchList?.Count; i++)
                                {
                                    bool profileInCDMS = GetProfileSummary(getMatchListIdsResponse.matchList[i]?.matchedWith);
                                    if (profileInCDMS)
                                    {
                                        matchResultController.matchResultForm.matchScoreList.Add(Convert.ToInt32
                                            (getMatchListIdsResponse.matchList[i]?.matchScore));
                                    }
                                }
                            });
                            if (matchResultController.profileList != null)
                            {
                                if (matchResultController.profileList.Count < 1)
                                {
                                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "No match found");
                                    return;
                                }
                            }
                            matchResultController.matchResultForm.profileList = matchResultController.profileList;
                            matchResultController.matchResultForm.matchCount = Convert.ToInt32(matchResultController.profileList?.Count);
                            matchResultController.matchResultForm.MatchedByNID = false;
                            matchResultController.matchResultForm.openedFromUI = 0;
                            matchResultController.matchResultForm.MatchedByCriteria = "Matched By: " + searchedByCriteria;
                            DialogResult dr = matchResultController.matchResultForm.ShowDialog();
                            if (dr == DialogResult.OK)
                            {
                                biometricUserControl.SetPhoto();
                                biometricUserControl.SetFingerPrint();
                                biometricUserControl.SetIris();
                                biometricUserControl.UpdateBiometricDataLocal();

                                biometricUserControl.CriminalProfileClickOperation(); // Moving to criminal profile
                            }
                            else
                            {
                                BiometricDto oldBiometricDto = new BiometricDto();
                                oldBiometricDto = StaticData.Enrollment?.profile?.biometric;
                                string refNo = StaticData.Enrollment?.profile?.referenceNo;
                                StaticData.Enrollment.profile = new MODELS.DTO.New.Enrollment.ProfileDto();
                                StaticData.Enrollment.profile.biometric = oldBiometricDto;
                                StaticData.Enrollment.profile.referenceNo = refNo;
                            }
                        }
                        else
                        {
                            CustomMessageBox.ShowMessage("SNSOP TOOLS", "No match found");
                            return;
                        }
                    }
                    else
                    {
                        CustomMessageBox.ShowMessage("SNSOP TOOLS", "No match found");
                        return;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(getMatchListIdsResponse.errorMsg))
                    {
                        CustomMessageBox.ShowMessage("SNSOP TOOLS", getMatchListIdsResponse.errorMsg);
                        return;
                    }
                    else
                    {
                        erroMsg = "No match found";
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(errorMsg))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "No match found");
                }
                else
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", errorMsg);
                }
            }
        }

        private bool GetProfileSummary(string criminalId)
        {
            DemographicSearchDto searchDto = new DemographicSearchDto();
            searchDto.id = criminalId;
            searchDto.limit = 1;

            ProfileSearchListResponse profileSearchListResponse = new EnrollmentApiManager().SearchProfileList(null, criminalId);

            if (profileSearchListResponse?.code == 200)
            {
                if (profileSearchListResponse?.profileList?.Count > 0)
                {
                    if (profileSearchListResponse?.profileList[0] != null)
                    {
                        matchResultController.profileList.Add(profileSearchListResponse.profileList[0]);
                        return true;
                    }
                }
            }
            return false;
        }

        public void JailIdentifyByBiometric(int flag)
        {
            if (StaticData.Enrollment.profile?.biometric?.fingerprint?.rt == null && StaticData.Enrollment.profile?.biometric?.fingerprint?.ri == null &&
                StaticData.Enrollment.profile?.biometric?.fingerprint?.rm == null && StaticData.Enrollment.profile?.biometric?.fingerprint?.rr == null &&
                StaticData.Enrollment.profile?.biometric?.fingerprint?.rs == null && StaticData.Enrollment.profile?.biometric?.fingerprint?.lt == null &&
                StaticData.Enrollment.profile?.biometric?.fingerprint?.li == null && StaticData.Enrollment.profile?.biometric?.fingerprint?.lm == null &&
                StaticData.Enrollment.profile?.biometric?.fingerprint?.lr == null && StaticData.Enrollment.profile?.biometric?.fingerprint?.ls == null &&
                StaticData.Enrollment.profile?.biometric?.iris?.left == null && StaticData.Enrollment.profile?.biometric?.iris?.right == null)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Biometric data is needed for search");
                return;
            }

            if (string.IsNullOrEmpty(Users.AccessToken))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "You are logged in offilne");
                return;
            }

            JailBiometricMatchProfileResponse response = new JailBiometricMatchProfileResponse();

            PersonBiometricDto bioDto = new PersonBiometricDto();

            if (flag == 3)
            {
                bioDto.leftIris = StaticData.Enrollment.profile?.biometric?.iris?.left;
                bioDto.rightIris = StaticData.Enrollment.profile?.biometric?.iris?.right;
            }

            else if (flag == 1)
            {
                bioDto.wsqRt = StaticData.Enrollment.profile?.biometric?.fingerprint?.rt;
                bioDto.wsqRi = StaticData.Enrollment.profile?.biometric?.fingerprint?.ri;
                bioDto.wsqRm = StaticData.Enrollment.profile?.biometric?.fingerprint?.rm;
                bioDto.wsqRr = StaticData.Enrollment.profile?.biometric?.fingerprint?.rr;
                bioDto.wsqRl = StaticData.Enrollment.profile?.biometric?.fingerprint?.rs;

                bioDto.wsqLt = StaticData.Enrollment.profile?.biometric?.fingerprint?.lt;
                bioDto.wsqLi = StaticData.Enrollment.profile?.biometric?.fingerprint?.li;
                bioDto.wsqLm = StaticData.Enrollment.profile?.biometric?.fingerprint?.lm;
                bioDto.wsqLr = StaticData.Enrollment.profile?.biometric?.fingerprint?.lr;
                bioDto.wsqLl = StaticData.Enrollment.profile?.biometric?.fingerprint?.ls;
            }

            string searchedByCriteria = string.Empty;

            if (bioDto.wsqLt != null || bioDto.wsqLi != null || bioDto.wsqLm != null || bioDto.wsqLr != null || bioDto.wsqLl != null
                || bioDto.wsqRt != null || bioDto.wsqRi != null || bioDto.wsqRm != null || bioDto.wsqRr != null || bioDto.wsqRl != null)
            {
                if (!string.IsNullOrEmpty(searchedByCriteria)) searchedByCriteria += ", Fingerprint";
                else searchedByCriteria += "Fingerprint";
            }
            if (bioDto.leftIris != null || bioDto.rightIris != null)
            {
                if (!string.IsNullOrEmpty(searchedByCriteria)) searchedByCriteria += ", Iris";
                else searchedByCriteria += "Iris";
            }

            string errorMsg = string.Empty;

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = new BiometricMatchApiManager().GetJailDBMatch(bioDto);
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
                    errorMsg = "There was an unexpected error when searching criminal. Please check your vpn connection or contact with your Administrator.";
                }
            });

            if (!string.IsNullOrEmpty(errorMsg))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", errorMsg);
                return;
            }

            if (response != null)
            {
                if (response.operationResult == true)
                {
                    if (response.profiles?.Count > 0)
                    {
                        JailDbMatchForm jailDbMatchForm = new JailDbMatchForm();
                        jailDbMatchForm.profileList = response.profiles;
                        jailDbMatchForm.ShowDialog();
                    }
                    else
                    {
                        errorMsg = "No Match found";
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(response.errorMsg))
                    {
                        errorMsg = "No Match found";
                    }
                    else
                    {
                        errorMsg = response.errorMsg;
                    }
                }
                if (!string.IsNullOrEmpty(errorMsg))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", errorMsg);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(errorMsg))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "No match found");
                }
                else
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", errorMsg);
                }
            }
        }
    }
}
