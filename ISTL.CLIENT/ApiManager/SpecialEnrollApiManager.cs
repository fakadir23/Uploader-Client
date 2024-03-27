using ISTL.COMMON.Network;
using ISTL.MODELS.Common;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.MODELS.DTO.New.Enrollment.Special;
using ISTL.MODELS.Request.New;
using ISTL.MODELS.Request.New.Enrollment;
using ISTL.MODELS.Response.New.Enrollment;
using ISTL.MODELS.Response.New.Special;
using ISTL.RAB.Entity;
using ISTL.RAB.View;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ISTL.RAB.ApiManager
{
    public class SpecialEnrollApiManager
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private readonly string SpecialProfileSubmitEndpoint = ConfigurationManager.AppSettings["SpecialProfileSubmitEndpoint"];
        private readonly string GetSpecialNotVerifiedHashListEndpoint = ConfigurationManager.AppSettings["GetSpecialNotVerifiedHashListEndpoint"];
        private readonly string CheckSpecialEnrolledHashEndpoint = ConfigurationManager.AppSettings["CheckSpecialEnrolledHashEndpoint"];
        private readonly string GetSpecialProfileListEndpoint = ConfigurationManager.AppSettings["GetSpecialProfileListEndpoint"];
        private readonly string GetSpecialCountEndpoint = ConfigurationManager.AppSettings["GetSpecialCountEndpoint"];
        private readonly string SubmitSpecialCountEndpoint = ConfigurationManager.AppSettings["SubmitSpecialCountEndpoint"];

        public ApiResponse SpecialProfileSubmit(SpecialEnrollmentDto enrollmentDto)
        {
            ApiResponse response = new ApiResponse();
            SpecialEnrollmentDto request = enrollmentDto;

            //var jsonSave = new JavaScriptSerializer().Serialize(request);
            //logger.Debug(jsonSave);

            try
            {
                response = NetworkService.SubmitRequest<ApiResponse>(request, SpecialProfileSubmitEndpoint, Users.AccessToken);

                if (response?.code == (int)HttpResponseStatus.OK)
                {
                    logger.Debug("Special Criminal Profile Upload By API is Success. Reference No: " + enrollmentDto?.referenceNo);
                }
                else
                {
                    logger.Debug("Special Criminal Profile Upload By API is Failed. Reference No: " + enrollmentDto?.referenceNo);
                    logger.Debug("Upload :: Special Criminal Profile :: API Error Message :: " + response?.message + " :: Reference No :: " + enrollmentDto?.referenceNo);
                }

                return response;
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
                throw x;
            }
        }

        public GetSpecialProfileResponse GetSpecialProfile(GetSpecialProfileRequest dto)
        {
            GetSpecialProfileResponse response = new GetSpecialProfileResponse();
            GetSpecialProfileRequest request = dto;
            try
            {
                response = NetworkService.SubmitRequest<GetSpecialProfileResponse>(request, GetSpecialProfileListEndpoint, Users.AccessToken);
                return response;
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
                throw x;
            }
        }

        public NotVerifiedHashResponse GetSpecialNotVerifiedHashList(List<string> list)
        {
            NotVerifiedHashRequest request = new NotVerifiedHashRequest() { hashList = list };
            NotVerifiedHashResponse response = new NotVerifiedHashResponse();
            try
            {
                response = NetworkService.SubmitRequest<NotVerifiedHashResponse>(request, GetSpecialNotVerifiedHashListEndpoint,
                        Users.AccessToken);
                return response;
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
                throw x;
            }
        }

        public int CheckSpecialEnrolledHash(string hash)
        {
            try
            {
                int code = 0;
                HashVerifyRequest request = new HashVerifyRequest() { hash = hash };
                HashVerifiedResponse response = new HashVerifiedResponse();
                try
                {
                    response = NetworkService.SubmitRequest<HashVerifiedResponse>(request, CheckSpecialEnrolledHashEndpoint, Users.AccessToken);

                    if (response.code == 200) code = Convert.ToInt32(response.result);
                    else code = 0;

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

        public ApiResponse SubmitSpecialArrestTypeCount(SpecialArrestTypeCountDto dto)
        {
            ApiResponse response = new ApiResponse();
            SpecialArrestTypeCountDto request = dto;            

            try
            {
                response = NetworkService.SubmitRequest<ApiResponse>(request, SubmitSpecialCountEndpoint, Users.AccessToken);
                return response;
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
                throw x;
            }
        }

        public GetSpecialArrestTypeCountResponse GetSpecialArrestTypeCount(GetSpecialArrestTypeCountRequest dto)
        {
            GetSpecialArrestTypeCountResponse response = new GetSpecialArrestTypeCountResponse();
            GetSpecialArrestTypeCountRequest request = dto;
            try
            {
                ProcessingDialog.Run(delegate ()
                {
                    response = NetworkService.SubmitRequest<GetSpecialArrestTypeCountResponse>
                    (request, GetSpecialCountEndpoint, Users.AccessToken);
                });
                return response;
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
                throw x;
            }
        }
    }
}
