using ISTL.COMMON.Network;
using ISTL.MODELS.Request;
using ISTL.MODELS.Response;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.ApiManager
{
    public class LoginApiManager
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private readonly string AuthEndpoint = ConfigurationManager.AppSettings["AuthEndpoint"].ToString();

        public LoginResponse Login(string username, string password, string deviceId="")
        {
            LoginResponse response = new LoginResponse();
            LoginRequest request = new LoginRequest() { userName = username, password = password };
            try
            {
                response = NetworkService.SubmitSNSOPRequest<LoginResponse>(request, AuthEndpoint, null, deviceId);
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
