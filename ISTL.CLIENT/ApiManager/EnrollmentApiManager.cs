using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Script.Serialization;
using ISTL.COMMON.Network;
using ISTL.MODELS.Common;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.MODELS.DTO.Search;
using ISTL.MODELS.Request.New;
using ISTL.MODELS.Request.New.Enrollment;
using ISTL.MODELS.Response.New;
using ISTL.MODELS.Response.New.Enrollment;
using ISTL.RAB.Controllers;
using ISTL.RAB.Entity;
using ISTL.RAB.View;
using NLog;

namespace ISTL.RAB.ApiManager
{
    public class EnrollmentApiManager
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private readonly string ProfileSubmitEndpoint = ConfigurationManager.AppSettings["ProfileSubmitEndpoint"];
        private readonly string SearchCriminalUserEndpoint = ConfigurationManager.AppSettings["SearchCriminalUserEndpoint"];
        private readonly string GetNotVerifiedHashListEndpoint = ConfigurationManager.AppSettings["GetNotVerifiedHashListEndpoint"];
        private readonly string CheckEnrolledHashEndpoint = ConfigurationManager.AppSettings["CheckEnrolledHashEndpoint"];

        private readonly string SearchProfileEndpoint = ConfigurationManager.AppSettings["SearchProfileEndpoint"];
        private readonly string GetByteDataEndpoint = ConfigurationManager.AppSettings["GetByteDataEndpoint"].ToString();


        public ApiResponse ProfileSubmit(EnrollmentDto enrollmentDto)
        {
            ApiResponse response = new ApiResponse();
            EnrollmentDto request = enrollmentDto;
            
            try
            {
                response = NetworkService.SubmitRequest<ApiResponse>(request, ProfileSubmitEndpoint, Users.AccessToken);

                if(response?.code == (int)HttpResponseStatus.OK)
                {
                    logger.Debug("Criminal Profile Upload By API is Success. Reference No: " + request?.profile?.referenceNo);
                }
                else
                {
                    logger.Debug("Criminal Profile Upload By API is Failed. Reference No: " + request?.profile?.referenceNo);
                    logger.Debug("Upload :: Criminal Profile :: API Error Message :: " + response?.message + " :: Reference No :: " + request?.profile?.referenceNo);
                }

                logger.Debug("Upload :: Criminal Profile :: API Response Code :: " + response?.code + " :: Reference No :: " + request?.profile?.referenceNo);
                return response;
            }
            catch (Exception x)
            {
                logger.Error("There was an api error when upload criminal profile by api. Error Message:\n" + x.ToString());
                throw x;
            }
        }

        public NotVerifiedHashResponse GetNotVerifiedHashList(List<string> list)
        {
            NotVerifiedHashRequest request = new NotVerifiedHashRequest() { hashList = list };
            NotVerifiedHashResponse response = new NotVerifiedHashResponse();
            try
            {
                response = NetworkService.SubmitRequest<NotVerifiedHashResponse>(request, GetNotVerifiedHashListEndpoint,
                        Users.AccessToken);
                return response;
                //response.operationResult = true;
                //response.hashList.Add("yXvKNo7dh5HitDmQ1BsmnIb8vSA=");
                //return response;
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
                throw x;
            }
        }

        public int CheckEnrolledHash(string hash)
        {
            try
            {
                int code = 0;
                HashVerifyRequest request = new HashVerifyRequest() { hash = hash };
                HashVerifiedResponse response = new HashVerifiedResponse();

                var jsonSave = new JavaScriptSerializer().Serialize(request);
                logger.Debug(jsonSave);

                try
                {
                    response = NetworkService.SubmitRequest<HashVerifiedResponse>(request, CheckEnrolledHashEndpoint,
                        Users.AccessToken);

                    if (response.code == 200)
                    {
                        if (response.result == 1) code = 1;
                        else code = 0;
                    }
                    else
                    {
                        code = 0;
                    }

                    return code;
                }
                catch (Exception x)
                {
                    logger.Error(x.ToString());
                    throw x;
                }
            }
            catch (Exception x)
            {
                logger.Error(x.Message);
                throw x;
            }
        }

        public bool Enroll(EnrollmentDto client)
        {
            return false;
        }

        public ProfileSearchSummaryResponse SearchCriminalUser(SearchCriminalDto searchCriminalDto)
        {
            try
            {
                ProfileSearchSummaryRequest profileSearchSummaryRequest =
                    new ProfileSearchSummaryRequest(searchCriminalDto);
                ProfileSearchSummaryResponse profileSearchSummaryResponse =
                    NetworkService.SubmitRequest<ProfileSearchSummaryResponse>(
                        profileSearchSummaryRequest,
                        SearchCriminalUserEndpoint,
                        Users.AccessToken);
                return profileSearchSummaryResponse;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
        }

        public ProfileSearchSummaryResponse DemographicSearch(DemographicSearchDto dto)
        {
            try
            {                
                ProfileSearchSummaryResponse profileSearchSummaryResponse = new ProfileSearchSummaryResponse();

                profileSearchSummaryResponse = NetworkService.SubmitRequest<ProfileSearchSummaryResponse>(
                        dto,
                        SearchCriminalUserEndpoint,
                        Users.AccessToken);
                
                return profileSearchSummaryResponse;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
        }

        public ProfileSearchListResponse SearchProfileList(string referenceNo, string criminalId)
        {
            try
            {                
                ProfileSearchSummaryRequest profileSearchSummaryRequest = new ProfileSearchSummaryRequest();

                profileSearchSummaryRequest.referenceNo = referenceNo;
                profileSearchSummaryRequest.id = criminalId;

                profileSearchSummaryRequest.limit = 1;

                var json = new JavaScriptSerializer().Serialize(profileSearchSummaryRequest);
                logger.Debug(json);

                ProfileSearchListResponse profileSearchListResponse = new ProfileSearchListResponse();

                profileSearchListResponse = NetworkService.SubmitRequest<ProfileSearchListResponse>(
                        profileSearchSummaryRequest,
                        SearchProfileEndpoint,
                        Users.AccessToken);
                
                return profileSearchListResponse;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
        }

        public GetProfileDataByteResponse GetByteDataByFilePath(string path)
        {
            try
            {
                GetProfileDataByteResponse response = new GetProfileDataByteResponse();
                GetProfileDataByteRequest request = new GetProfileDataByteRequest();
                request.key = path;

                response = NetworkService.SubmitRequest<GetProfileDataByteResponse>(request, GetByteDataEndpoint, Users.AccessToken);

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