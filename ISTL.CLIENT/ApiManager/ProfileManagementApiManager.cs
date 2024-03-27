using ISTL.COMMON.Network;
using ISTL.MODELS.Request;
using ISTL.MODELS.Request.New;
using ISTL.MODELS.Request.ProfileManagement;
using ISTL.MODELS.Response;
using ISTL.MODELS.Response.New;
using ISTL.MODELS.Response.ProfileManagement;
using ISTL.RAB.Entity;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.ApiManager
{
    public class ProfileManagementApiManager
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        //private readonly string ProfileManagementAuthEndpoint = ConfigurationManager.AppSettings["ProfileManagementAuthEndpoint"];
        //private readonly string ProfileManagementEnrollmentEndpoint = ConfigurationManager.AppSettings["ProfileManagementEnrollmentEndpoint"];
        //private readonly string ProfileManagementProfileSearchEndpoint = ConfigurationManager.AppSettings["ProfileManagementProfileSearchEndpoint"];
        //private readonly string ProfileManagementGetByteDataEndpoint = ConfigurationManager.AppSettings["ProfileManagementGetByteDataEndpoint"];

        public LoginResponse Login(string username, string password)
        {
            string ProfileManagementAuthEndpoint = string.Empty;
            try
            {
                ProfileManagementAuthEndpoint = ConfigurationManager.AppSettings["ProfileManagementAuthEndpoint"];
            }
            catch(Exception x)
            {
                logger.Error("Error resolving App config for Profile Management login endpoint. " + x.ToString());
            }

            if (string.IsNullOrEmpty(ProfileManagementAuthEndpoint))
            {
                ProfileManagementAuthEndpoint = "auth/login";
            }

            LoginResponse response = new LoginResponse();
            LoginRequest request = new LoginRequest() { userName = username, password = password };
            try
            {
                response = NetworkService.SubmitProfileManagementRequest<LoginResponse>(request, ProfileManagementAuthEndpoint);
                return response;
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
                throw x;
            }
        }

        public ProfileManagementEnrollmentResponse ProfileManagementSubmit(ProfileManagementEnrollmentRequest enrollmentDto, string token)
        {
            string ProfileManagementEnrollmentEndpoint = string.Empty;
            try
            {
                ProfileManagementEnrollmentEndpoint = ConfigurationManager.AppSettings["ProfileManagementEnrollmentEndpoint"];
            }
            catch (Exception x)
            {
                logger.Error("Error resolving App config for Profile Management enrollment endpoint. " + x.ToString());
            }

            if (string.IsNullOrEmpty(ProfileManagementEnrollmentEndpoint))
            {
                ProfileManagementEnrollmentEndpoint = "profile/enrollment";
            }

            ProfileManagementEnrollmentResponse response = new ProfileManagementEnrollmentResponse();
            ProfileManagementEnrollmentRequest request = enrollmentDto;

            request.cdmsUser = Users.Id;

            //var jsonSave = new JavaScriptSerializer().Serialize(request);
            //logger.Debug(jsonSave);

            try
            {
                response = NetworkService.SubmitProfileManagementRequest<ProfileManagementEnrollmentResponse>(request, ProfileManagementEnrollmentEndpoint, "POST", token);

                if (response?.code == (int)HttpResponseStatus.OK)
                {
                    logger.Debug("Profile Management Upload By API is Success. Name: " + enrollmentDto?.fullName);
                }
                else
                {
                    logger.Debug("Upload in Profile Management. API Error Message :: " + response?.message + " :: Name :: " + enrollmentDto?.fullName);
                }

                return response;
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
                throw x;
            }
        }

        public ProfileManagementSearchResponse GetProfileManagementProfile(string profileId, string token)
        {
            string ProfileManagementProfileSearchEndpoint = string.Empty;
            try
            {
                ProfileManagementProfileSearchEndpoint = ConfigurationManager.AppSettings["ProfileManagementProfileSearchEndpoint"];
            }
            catch (Exception x)
            {
                logger.Error("Error resolving App config for Profile Management profile search endpoint. " + x.ToString());
            }

            if (string.IsNullOrEmpty(ProfileManagementProfileSearchEndpoint))
            {
                ProfileManagementProfileSearchEndpoint = "profile/biometricInformationSearch";
            }

            ProfileManagementSearchResponse response = new ProfileManagementSearchResponse();
            try
            {
                response = NetworkService.SubmitProfileManagementRequest<ProfileManagementSearchResponse>
                    (null, ProfileManagementProfileSearchEndpoint +"?profileID="+profileId, "GET", token);

                return response;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
        }

        public GetProfileDataByteResponse GetByteDataByFilePath(string path, string token)
        {
            string ProfileManagementGetByteDataEndpoint = string.Empty;
            try
            {
                ProfileManagementGetByteDataEndpoint = ConfigurationManager.AppSettings["ProfileManagementGetByteDataEndpoint"];
            }
            catch (Exception x)
            {
                logger.Error("Error resolving App config for Profile Management profile byte data search endpoint. " + x.ToString());
            }

            if (string.IsNullOrEmpty(ProfileManagementGetByteDataEndpoint))
            {
                ProfileManagementGetByteDataEndpoint = "profile/getByte";
            }

            try
            {
                GetProfileDataByteResponse response = new GetProfileDataByteResponse();
                GetProfileDataByteRequest request = new GetProfileDataByteRequest();
                request.key = path;

                response = NetworkService.SubmitProfileManagementRequest<GetProfileDataByteResponse>(request, 
                    ProfileManagementGetByteDataEndpoint, "POST", token);

                return response;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
        }
    }
}
