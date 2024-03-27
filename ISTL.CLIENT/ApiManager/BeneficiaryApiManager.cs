using ISTL.COMMON.Network;
using ISTL.MODELS.Request.Beneficiary;
using ISTL.MODELS.Response.Beneficiary;
using ISTL.RAB.Entity;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.ApiManager
{
    public class BeneficiaryApiManager
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private readonly string BeneficiaryRegistrationEndpoint = ConfigurationManager.AppSettings["BeneficiaryRegistrationEndpoint"];
        private readonly string BatchBeneficiaryRegistrationEndpoint = ConfigurationManager.AppSettings["BatchBeneficiaryRegistrationEndpoint"];

        public bool ProfileSubmit(RegisterBeneficiaryRequest enrollmentDto)
        {
            try
            {
                string apiBaseURL = "";
                if (ConfigurationManager.AppSettings["build.profile.active"].ToString() == "dev")
                {
                    apiBaseURL = System.Configuration.ConfigurationManager.AppSettings["ApiBaseUrlDev"];
                }
                else
                {
                    apiBaseURL = System.Configuration.ConfigurationManager.AppSettings["ApiBaseUrlProd"];
                }

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiBaseURL);
                    client.DefaultRequestHeaders.Add("DeviceId", Users.DeviceId);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(60);

                    if (!string.IsNullOrWhiteSpace(Users.AccessToken))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Users.AccessToken);
                    }

                    HttpResponseMessage response = new HttpResponseMessage();
                    response = client.PostAsJsonAsync(BeneficiaryRegistrationEndpoint, enrollmentDto).Result;

                    return response.IsSuccessStatusCode;
                }

            }
            catch (Exception x)
            {
                logger.Error("There was an api error when upload criminal profile by api. Error Message:\n" + x.ToString());
                throw x;
            }
        }

        public BatchRegisterBeneficiaryResponse BatchProfileSubmit(BatchRegisterBeneficiaryRequest request)
        {
            try
            {                
                BatchRegisterBeneficiaryResponse response = NetworkService.SubmitSNSOPRequest<BatchRegisterBeneficiaryResponse>
                    (request, BatchBeneficiaryRegistrationEndpoint, Users.AccessToken, Users.DeviceId);

                return response;

            }
            catch (Exception x)
            {
                logger.Error("There was an api error when upload criminal profile by api. Error Message:\n" + x.ToString());
                throw x;
            }
        }
    }
}
