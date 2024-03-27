using ISTL.COMMON.Network;
using ISTL.MODELS.DTO.Enrollment;
using ISTL.MODELS.Response.New.JailDBBioMatch;
using ISTL.MODELS.Response.Old.Adjudication;
using ISTL.RAB.View;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.ApiManager
{
    public class BiometricMatchApiManager
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private readonly string EnrollmentIdentifyEndpoint = ConfigurationManager.AppSettings["EnrollmentIdentifyEndpoint"];
        //private readonly string JailDbBiometricMatchEndpoint = ConfigurationManager.AppSettings["JailDbBiometricMatchEndpoint"];

        public GetMatchListIdsResponse GetMatchByBiometric(PersonDataDto dto)
        {
            GetMatchListIdsResponse response = new GetMatchListIdsResponse();
            PersonDataDto request = dto;
            try
            {
                response = NetworkService.SubmitBioRequest<GetMatchListIdsResponse>(request, EnrollmentIdentifyEndpoint + "?token=abc", "POST", null);
                return response;
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
                throw x;
            }
        }

        public JailBiometricMatchProfileResponse GetJailDBMatch(PersonBiometricDto dto)
        {
            string JailDbBiometricMatchEndpoint = string.Empty;
            try
            {
                JailDbBiometricMatchEndpoint = ConfigurationManager.AppSettings["JailDbBiometricMatchEndpoint"];
            }
            catch(Exception x)
            {
                logger.Error("Error resolving App config for Jail db match endpoint" + x.ToString());
                JailDbBiometricMatchEndpoint = "identify";
            }

            if (string.IsNullOrEmpty(JailDbBiometricMatchEndpoint) || string.IsNullOrWhiteSpace(JailDbBiometricMatchEndpoint))
            {
                JailDbBiometricMatchEndpoint = "identify";
            }

            JailBiometricMatchProfileResponse response = new JailBiometricMatchProfileResponse();
            try
            {
                response = NetworkService.SubmitJailDbMatchBioRequest<JailBiometricMatchProfileResponse>(dto, JailDbBiometricMatchEndpoint, "POST", null);
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
