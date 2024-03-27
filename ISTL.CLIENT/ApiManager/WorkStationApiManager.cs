using ISTL.COMMON.Network;
using ISTL.COMMON.Subscription;
using ISTL.MODELS.Common;
using ISTL.MODELS.Request;
using ISTL.MODELS.Request.WorkStation;
using ISTL.MODELS.Response;
using ISTL.MODELS.Response.WorkStation;
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
    public class WorkStationApiManager
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private readonly string SendHeartbeatEndpoint = ConfigurationManager.AppSettings["SendHeartbeatEndpoint"].ToString();

        public WorkStationAliveResponse SendHeartbeat(WorkStationAliveRequest request)
        {
            WorkStationAliveResponse response = new WorkStationAliveResponse();
            try
            {
                response = NetworkService.SubmitRequest<WorkStationAliveResponse>(request, SendHeartbeatEndpoint, Users.AccessTokenForWorkStation);
                return response;
            }            
            catch (Exception x)
            {
                logger.Error("There was an error when sending heartbeat to server. Error Message : " + x.ToString());
                throw x;
            }
        }
    }
}
