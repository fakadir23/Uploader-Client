using ISTL.COMMON.Network;
using ISTL.COMMON.Subscription;
using ISTL.MODELS.Request.New;
using ISTL.MODELS.Response.New;
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
    public class DashboardApiManager
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private readonly string DashboardSummaryEndpoint = ConfigurationManager.AppSettings["DashboardSummaryEndpoint"];

        public DashboardSummaryResponse GetDashboardSummary(DashboardSummaryRequest dto)
        {            
            DashboardSummaryResponse response = new DashboardSummaryResponse();
            DashboardSummaryRequest request = dto;
            try
            {
                response = NetworkService.SubmitRequest<DashboardSummaryResponse>(request, DashboardSummaryEndpoint, "POST", Users.AccessToken);
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
