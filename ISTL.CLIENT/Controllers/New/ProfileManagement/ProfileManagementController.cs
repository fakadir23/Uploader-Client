using ISTL.COMMON;
using ISTL.MODELS.DTO.ProfileManagement.Enrollment;
using ISTL.MODELS.Request.ProfileManagement;
using ISTL.MODELS.Response;
using ISTL.MODELS.Response.New;
using ISTL.MODELS.Response.ProfileManagement;
using ISTL.PERSOGlobals;
using ISTL.RAB.ApiManager;
using ISTL.RAB.View;
using ISTL.RAB.View.New.ProfileManagement;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.Controllers.New.ProfileManagement
{
    public class ProfileManagementController : ViewController
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private ProfileManagementUserControl profileManagementUserControl;
        private ProfileManagementApiManager profileManagementApiManager = new ProfileManagementApiManager();
        private string token = null;

        public ProfileManagementController()
        {
            profileManagementUserControl = new ProfileManagementUserControl();
            base.SetView((IView)profileManagementUserControl);
            profileManagementUserControl.SetController(this);
        }

        public override string GetName()
        {
            return Globals.ChildControllers.PROFILE_MANAGEMENT;
        }

        public ProfileManagementEnrollmentRequest FetchProfileById(string ProfileId)
        {
            if (string.IsNullOrEmpty(token))
            {
                CustomMessageBox.ShowMessage("RAB Profile Management", "You are not logged in Profile Management");
                return null;
            }

            ProfileManagementSearchResponse response = new ProfileManagementSearchResponse();
            ProfileManagementEnrollmentRequest enrollmentDto = new ProfileManagementEnrollmentRequest();

            string errorMsg = null;

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = profileManagementApiManager.GetProfileManagementProfile(ProfileId, token);
                    if (response?.code == 200)
                    {
                        enrollmentDto = CopyToEnrollmentRequest(response);
                    }
                }
                catch (WebException x)
                {
                    logger.Debug("Known error, when server not found.\n" + x.Message);
                    errorMsg = "Failed to fetch profile, web exception";
                }
                catch (TimeoutException x)
                {
                    logger.Debug("Connection timedout! Should attempt to login offline.\n" + x.Message);
                    errorMsg = "Failed to fetch profile, timed out";
                }
                catch (Exception x)
                {
                    logger.Error("There was an unexpected error during login.\n" + x.ToString());
                    errorMsg = "Failed to fetch profile, critical error";
                }
            });

            if (errorMsg?.Length > 0)
            {
                CustomMessageBox.ShowMessage("RAB Profile Management", errorMsg);
            }

            return enrollmentDto;
        }

        private ProfileManagementEnrollmentRequest CopyToEnrollmentRequest(ProfileManagementSearchResponse response)
        {
            if (string.IsNullOrEmpty(response?.id))
            {
                return null;
            }

            ProfileManagementEnrollmentRequest enrollmentDto = new ProfileManagementEnrollmentRequest();

            enrollmentDto.fullName = response.fullName;
            enrollmentDto.gender = response.gender;
            enrollmentDto.profileType = response.profileType;

            enrollmentDto.id = response.id;
            profileManagementUserControl.ProfileLongId = response.id;

            if (!string.IsNullOrEmpty(response.photo))
            {
                if (enrollmentDto.photo == null) enrollmentDto.photo = new ProfilePhotoDto();

                GetByteDataForProfile(response.photo, "photo", enrollmentDto);
            }

            if (response.fingerprint != null)
            {
                if (enrollmentDto.fingerprint == null) enrollmentDto.fingerprint = new ProfileFingerprintDto();

                if (!string.IsNullOrEmpty(response.fingerprint.rtWsq)) GetByteDataForProfile(response.fingerprint.rtWsq, "rt", enrollmentDto);
                if (!string.IsNullOrEmpty(response.fingerprint.riWsq)) GetByteDataForProfile(response.fingerprint.riWsq, "ri", enrollmentDto);
                if (!string.IsNullOrEmpty(response.fingerprint.rmWsq)) GetByteDataForProfile(response.fingerprint.rmWsq, "rm", enrollmentDto);
                if (!string.IsNullOrEmpty(response.fingerprint.rrWsq)) GetByteDataForProfile(response.fingerprint.rrWsq, "rr", enrollmentDto);
                if (!string.IsNullOrEmpty(response.fingerprint.rsWsq)) GetByteDataForProfile(response.fingerprint.rsWsq, "rs", enrollmentDto);

                if (!string.IsNullOrEmpty(response.fingerprint.ltWsq)) GetByteDataForProfile(response.fingerprint.ltWsq, "lt", enrollmentDto);
                if (!string.IsNullOrEmpty(response.fingerprint.liWsq)) GetByteDataForProfile(response.fingerprint.liWsq, "li", enrollmentDto);
                if (!string.IsNullOrEmpty(response.fingerprint.lmWsq)) GetByteDataForProfile(response.fingerprint.lmWsq, "lm", enrollmentDto);
                if (!string.IsNullOrEmpty(response.fingerprint.lrWsq)) GetByteDataForProfile(response.fingerprint.lrWsq, "lr", enrollmentDto);
                if (!string.IsNullOrEmpty(response.fingerprint.lsWsq)) GetByteDataForProfile(response.fingerprint.lsWsq, "ls", enrollmentDto);
            }

            if (response.iris != null)
            {
                if (enrollmentDto.iris == null) enrollmentDto.iris = new ProfileIrisDto();

                if (!string.IsNullOrEmpty(response.iris.left)) GetByteDataForProfile(response.iris.left, "leftIris", enrollmentDto);
                if (!string.IsNullOrEmpty(response.iris.right)) GetByteDataForProfile(response.iris.right, "rightIris", enrollmentDto);
            }

            return enrollmentDto;
        }

        private void GetByteDataForProfile(string str, string attribute, ProfileManagementEnrollmentRequest enrollmentDto)
        {
            GetProfileDataByteResponse response = profileManagementApiManager.GetByteDataByFilePath(str, token);
            if (response != null)
            {
                if (response.code == 200 && response.file?.Length > 0)
                {
                    if (attribute == "photo") enrollmentDto.photo.photo = response.file;

                    else if (attribute == "rt") enrollmentDto.fingerprint.rt = response.file;
                    else if (attribute == "ri") enrollmentDto.fingerprint.ri = response.file;
                    else if (attribute == "rm") enrollmentDto.fingerprint.rm = response.file;
                    else if (attribute == "rr") enrollmentDto.fingerprint.rr = response.file;
                    else if (attribute == "rs") enrollmentDto.fingerprint.rs = response.file;

                    else if (attribute == "lt") enrollmentDto.fingerprint.lt = response.file;
                    else if (attribute == "li") enrollmentDto.fingerprint.li = response.file;
                    else if (attribute == "lm") enrollmentDto.fingerprint.lm = response.file;
                    else if (attribute == "lr") enrollmentDto.fingerprint.lr = response.file;
                    else if (attribute == "ls") enrollmentDto.fingerprint.ls = response.file;

                    else if (attribute == "leftIris") enrollmentDto.iris.left = response.file;
                    else if (attribute == "rightIris") enrollmentDto.iris.right = response.file;
                }
            }
        }

        public bool SubmitProfileManagement(ProfileManagementEnrollmentRequest enrollmentDto)
        {
            if (string.IsNullOrEmpty(token))
            {
                CustomMessageBox.ShowMessage("RAB Profile Management", "You are not logged in Profile Management");
                return false;
            }

            ProfileManagementEnrollmentResponse response = new ProfileManagementEnrollmentResponse();

            int UploadResult = 0;

            if (enrollmentDto.photo?.photo?.Length > 0)
            {
                enrollmentDto.photo.contentType = "image/jpg";
                enrollmentDto.photo.extension = ".jpg";
            }
            else
            {
                enrollmentDto.photo = null;
            }

            if (enrollmentDto.fingerprint?.rt?.Length > 0 || enrollmentDto.fingerprint?.ri?.Length > 0 || enrollmentDto.fingerprint?.rm?.Length > 0
                || enrollmentDto.fingerprint?.rr?.Length > 0 || enrollmentDto.fingerprint?.rs?.Length > 0 || enrollmentDto.fingerprint?.lt?.Length > 0
                || enrollmentDto.fingerprint?.li?.Length > 0 || enrollmentDto.fingerprint?.lm?.Length > 0 || enrollmentDto?.fingerprint?.lr?.Length > 0
                || enrollmentDto.fingerprint?.ls?.Length > 0)
            {
                enrollmentDto.fingerprint.contentType = "image/wsq";
                enrollmentDto.fingerprint.extension = ".wsq";
            }
            else
            {
                enrollmentDto.fingerprint = null;
            }

            if (enrollmentDto.iris?.left?.Length > 0 || enrollmentDto.iris?.right?.Length > 0)
            {
                enrollmentDto.iris.contentType = "image/jpg";
                enrollmentDto.iris.extension = ".jpg";
            }
            else
            {
                enrollmentDto.iris = null;
            }

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = profileManagementApiManager.ProfileManagementSubmit(enrollmentDto, token);
                }
                catch (WebException x)
                {
                    logger.Debug("Connection problems during upload Criminal Profile. This is not serious." + x.Message);
                    UploadResult = 1;
                }
                catch (TimeoutException x)
                {
                    logger.Error("Connection timed out!\n" + x.ToString());
                    UploadResult = 2;
                }
                catch (System.ServiceModel.EndpointNotFoundException x)
                {
                    logger.Debug("EndPointNotFound during upload!" + x.Message);
                    UploadResult = 3;
                }
                catch (Exception x)
                {
                    logger.Error("There was an unexpected error during upload Profile" + x.ToString());
                    UploadResult = 4;
                }
            });

            if (response?.code == 200)
            {
                string successMessage = string.Empty;
                if (!string.IsNullOrEmpty(response.message))
                {
                    successMessage = response.message;
                }
                else
                {
                    successMessage = "Successfully uploaded.";
                }
                if (!string.IsNullOrEmpty(response.profileId))
                {
                    successMessage += "\nProfile Id: " + response.profileId;
                }

                InfoMessageBox.ShowMessage("RAB Profile Management", successMessage);

                //profileManagementUserControl.ProfileId = response.profileId;

                return true;
            }
            else
            {
                if (UploadResult == 0)
                {
                    if (!string.IsNullOrEmpty(response.message))
                    {
                        CustomMessageBox.ShowMessage("RAB Profile Management", response.message);
                    }
                    else
                    {
                        CustomMessageBox.ShowMessage("RAB Profile Management", "Failed to upload");
                    }
                }
                else
                {
                    if (UploadResult == 1)
                    {
                        CustomMessageBox.ShowMessage("SNSOP TOOLS", "Profile upload failed as offline");
                    }
                    else if (UploadResult == 2)
                    {
                        CustomMessageBox.ShowMessage("SNSOP TOOLS", "Profile upload failed as connection timed out");
                    }
                    else if (UploadResult == 3)
                    {
                        CustomMessageBox.ShowMessage("SNSOP TOOLS", "Profile upload failed as endpoint not found");
                    }
                    else if (UploadResult == 4)
                    {
                        CustomMessageBox.ShowMessage("SNSOP TOOLS", "Profile upload failed as critical exception occurred");
                    }
                }

                return false;
            }
        }
    }
}
