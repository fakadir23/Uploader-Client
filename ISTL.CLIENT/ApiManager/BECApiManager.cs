using ISTL.COMMON.Network;
using ISTL.MODELS.Request.New;
using ISTL.MODELS.Response.New;
using ISTL.RAB.Controllers;
using ISTL.RAB.Entity;
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
    public class BECApiManager
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private readonly string GetBECverifyEndpoint = ConfigurationManager.AppSettings["GetBECverifyEndpoint"];
        private readonly string GetBECidentifyEndpoint = ConfigurationManager.AppSettings["GetBECidentifyEndpoint"];
        private readonly string BecNidIdentificationRequestEndpoint = ConfigurationManager.AppSettings["BecNidIdentificationRequestEndpoint"].ToString();
        private readonly string BecNidIdentificationResultEndpoint = ConfigurationManager.AppSettings["BecNidIdentificationResultEndpoint"].ToString();


        public GetBECverifyResponse GetProfileByNid(GetBECverifyRequest request)
        {            
            GetBECverifyResponse response = new GetBECverifyResponse();
            try
            {
                //ProcessingDialog.Run(delegate ()
                //{
                    response = NetworkService.SubmitNidRequest<GetBECverifyResponse>(request, GetBECverifyEndpoint, "POST", Users.AccessToken);
                    //response = NetworkService.SubmitNidRequest<GetBECverifyResponse>(request, GetBECverifyEndpoint, "POST", null);
                //});
                return response;
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
                throw x;
            }
        }

        //public GetBECidentifyResponse GetProfileByNidBiometric(GetBECidentifyRequest request)
        //{
        //    if (string.IsNullOrEmpty(Users.AccessToken))
        //    {
        //        CustomMessageBox.ShowMessage("SNSOP TOOLS", "You are logged in offline");
        //        return null;
        //    }
        //    GetBECidentifyResponse response = new GetBECidentifyResponse();
        //    try
        //    {
        //        //response = NetworkService.SubmitRequest<GetBECverifyResponse>(request, GetBECidentifyEndpoint, Users.AccessToken);
        //        response = NetworkService.SubmitNidRequest<GetBECidentifyResponse>(request, GetBECidentifyEndpoint, "POST", Users.AccessToken);
        //        return response;
        //    }
        //    catch (Exception x)
        //    {
        //        logger.Error(x.ToString());
        //        throw x;
        //    }
        //}

        public GetBECidentifyResponse DoProfileRequestByNidBiometric(GetBECidentifyRequest request)
        {            
            GetBECidentifyResponse response = new GetBECidentifyResponse();
            try
            {
                response = NetworkService.SubmitNidRequest<GetBECidentifyResponse>(request, BecNidIdentificationRequestEndpoint, "POST", Users.AccessToken);
                return response;
            }
            catch (Exception x)
            {
                logger.Error("DoProfileRequestByNidBiometric: " + x.ToString());
                throw x;
            }
        }

        public GetBECidentifyResponse GetProfileResultByNidBiometric(GetBECidentifyRequest request)
        {
            GetBECidentifyResponse response = new GetBECidentifyResponse();
            try
            {
                response = NetworkService.SubmitNidRequest<GetBECidentifyResponse>(request, BecNidIdentificationResultEndpoint, "POST", Users.AccessToken);
                return response;
            }
            catch (Exception x)
            {
                logger.Error("GetProfileResultByNidBiometric: " + x.ToString());
                throw x;
            }
        }
    }
}
