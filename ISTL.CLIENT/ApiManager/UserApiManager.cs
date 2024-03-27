using ISTL.COMMON.Network;
using ISTL.MODELS.Common;
using ISTL.MODELS.Request.User;
using ISTL.MODELS.Response.User;
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
    public class UserApiManager
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private readonly string AddUserEndpoint = ConfigurationManager.AppSettings["AddUserEndpoint"];
        private readonly string UpdateUserEndpoint = ConfigurationManager.AppSettings["UpdateUserEndpoint"];
        private readonly string SearchUserEndpoint = ConfigurationManager.AppSettings["SearchUserEndpoint"];
        private readonly string UserActivationEndpoint = ConfigurationManager.AppSettings["UserActivationEndpoint"].ToString();
        private readonly string UserDeactivationEndpoint = ConfigurationManager.AppSettings["UserDeactivationEndpoint"].ToString();

        public ApiResponse ActivateUser(UserActivationRequest request)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                response = NetworkService.SubmitRequest<ApiResponse>(request, UserActivationEndpoint,
                    Users.AccessToken);
                return response;
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
                throw x;
            }
        }

        public ApiResponse DeactivateUser(UserActivationRequest request)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                response = NetworkService.SubmitRequest<ApiResponse>(request, UserDeactivationEndpoint,
                    Users.AccessToken);
                return response;
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
                throw x;
            }
        }

        public UserSearchResponse GetUserList(UserSearchCriteriaRequest request)
        {
            UserSearchResponse response = new UserSearchResponse();

            try
            {
                response = NetworkService.SubmitRequest<UserSearchResponse>(request, SearchUserEndpoint,
                    Users.AccessToken);
                return response;
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
                throw x;
            }
        }

        public ApiResponse SaveUser(AddUserRequest request)
        {
            ApiResponse response = new ApiResponse();
            if (request?.id > 0)
            {
                response = UpdateUser(request);
            }
            else
            {
                response = AddUser(request);
            }
            return response;
        }

        private ApiResponse AddUser(AddUserRequest request)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                response = NetworkService.SubmitRequest<ApiResponse>(request, AddUserEndpoint,
                    Users.AccessToken);
                return response;
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
                throw x;
            }
        }

        private ApiResponse UpdateUser(AddUserRequest request)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                response = NetworkService.SubmitRequest<ApiResponse>(request, UpdateUserEndpoint,
                    Users.AccessToken);
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
