using ISTL.COMMON.Network;
using ISTL.MODELS.Common;
using ISTL.MODELS.DTO.Report;
using ISTL.MODELS.Request.New.Report;
using ISTL.MODELS.Request.Report;
using ISTL.MODELS.Response.Report;
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
    public class ReportApiManager
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private readonly string CriminalReportEndpoint = ConfigurationManager.AppSettings["CriminalReportEndpoint"].ToString();
        private readonly string DailyReportEndpoint = ConfigurationManager.AppSettings["DailyReportEndpoint"].ToString();
        private readonly string SpecialCriminalReportEndpoint = ConfigurationManager.AppSettings["SpecialCriminalReportEndpoint"].ToString();
        private readonly string ReportResultEndpoint = ConfigurationManager.AppSettings["ReportResultEndpoint"].ToString();
        private readonly string DeleteReportEndpoint = ConfigurationManager.AppSettings["DeleteReportEndpoint"].ToString();
        private readonly string CriminalHistoryReportEndpoint = ConfigurationManager.AppSettings["CriminalHistoryReportEndpoint"].ToString();
        private readonly string CriminalProfileReportEndpoint = ConfigurationManager.AppSettings["CriminalProfileReportEndpoint"].ToString();
        //private readonly string CrimeTypeWiseReportEndpoint = ConfigurationManager.AppSettings["CrimeTypeWiseReportEndpoint"].ToString();
        private readonly string CrimeTypeWiseReportEndpoint = "report/crimeTypeWiseReport/desktop";

        public CriminalProfileOrCombinedReportResponse GetCriminalHistoryReport(CriminalReportRequest request)
        {
            CriminalProfileOrCombinedReportResponse response = new CriminalProfileOrCombinedReportResponse();
            try
            {
                response = NetworkService.SubmitRequest<CriminalProfileOrCombinedReportResponse>
                    (request, CriminalHistoryReportEndpoint, Users.AccessToken);
                return response;
            }
            catch (Exception x)
            {
                logger.Error("There was an error when gettint criminal report response from server. Error Message :\n" + x.ToString());
                throw x;
            }
        }

        public CriminalProfileOrCombinedReportResponse GetCriminalProfileReport(CriminalReportRequest request)
        {
            CriminalProfileOrCombinedReportResponse response = new CriminalProfileOrCombinedReportResponse();
            try
            {
                response = NetworkService.SubmitRequest<CriminalProfileOrCombinedReportResponse>
                    (request, CriminalProfileReportEndpoint, Users.AccessToken);
                return response;
            }
            catch (Exception x)
            {
                logger.Error("There was an error when gettint criminal report response from server. Error Message :\n" + x.ToString());
                throw x;
            }
        }

        public ReportResponse InitDailyEnrollmentReport(ReportResult request)
        {
            ReportResponse response = new ReportResponse();
            try
            {
                response = NetworkService.SubmitRequest<ReportResponse>(request, DailyReportEndpoint, Users.AccessToken);
                return response;
            }
            catch (Exception x)
            {
                logger.Error("There was an error when gettint criminal report response from server. Error Message :\n" + x.ToString());
                throw x;
            }
        }

        public ReportResponse InitCriminalProfileReport(ReportResult request)
        {
            ReportResponse response = new ReportResponse();
            try
            {
                response = NetworkService.SubmitRequest<ReportResponse>(request, CriminalReportEndpoint, Users.AccessToken);
                return response;
            }
            catch (Exception x)
            {
                logger.Error("There was an error when gettint criminal report response from server. Error Message :\n" + x.ToString());
                throw x;
            }
        }

        public ReportResponse InitSpecialCriminalProfileReport(ReportResult request)
        {
            ReportResponse response = new ReportResponse();
            try
            {
                response = NetworkService.SubmitRequest<ReportResponse>(request, SpecialCriminalReportEndpoint, Users.AccessToken);
                return response;
            }
            catch (Exception x)
            {
                logger.Error("There was an error when gettint criminal report response from server. Error Message :\n" + x.ToString());
                throw x;
            }
        }

        public ReportResponse InitCrimeTypeWiseReport(ReportResult request)
        {
            ReportResponse response = new ReportResponse();
            try
            {
                response = NetworkService.SubmitRequest<ReportResponse>(request, CrimeTypeWiseReportEndpoint, Users.AccessToken);
                return response;
            }
            catch (Exception x)
            {
                logger.Error("There was an error when gettint crime type wise report response from server. Error Message :\n" + x.ToString());
                throw x;
            }
        }

        public ReportResultResponse GetReportResult(ReportResultRequest request)
        {
            ReportResultResponse response = new ReportResultResponse();
            try
            {
                response = NetworkService.SubmitRequest<ReportResultResponse>(request, ReportResultEndpoint, Users.AccessToken);
                return response;
            }
            catch (Exception x)
            {
                logger.Error("There was an error when gettint criminal report response from server. Error Message :\n" + x.ToString());
                throw x;
            }
        }

        public ApiResponse DeleteReport(ReportDeleteRequest request)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                response = NetworkService.SubmitRequest<ApiResponse>(request, DeleteReportEndpoint, Users.AccessToken);
                return response;
            }
            catch (Exception x)
            {
                logger.Error("There was an error when deleting from server. Error Message :\n" + x.ToString());
                throw x;
            }
        }
    }
}
