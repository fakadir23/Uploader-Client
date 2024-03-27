using ISTL.COMMON.Network;
using ISTL.MODELS.Common;
using ISTL.MODELS.DTO.New.NotEntry;
using ISTL.MODELS.Request.NotEntry;
using ISTL.MODELS.Response.NotEntry;
using ISTL.RAB.Entity;
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
    public class NotEntryApiManager
    {
        private Logger logger = LogManager.GetCurrentClassLogger();

        public ApiResponse NotEntryProfileSubmit(NotEntryDto enrollmentDto)
        {
            string NotEntryProfileSubmitEndpoint = string.Empty;
            try
            {
                NotEntryProfileSubmitEndpoint = ConfigurationManager.AppSettings["NotEntryProfileSubmitEndpoint"];
            }
            catch (Exception x)
            {
                logger.Error("Error resolving App config for Not Entry enrollment endpoint. " + x.ToString());
            }

            if (string.IsNullOrEmpty(NotEntryProfileSubmitEndpoint))
            {
                NotEntryProfileSubmitEndpoint = "noentry/save";
            }

            ApiResponse response = new ApiResponse();
            NotEntryDto request = enrollmentDto;

            //var jsonSave = new JavaScriptSerializer().Serialize(request);
            //logger.Debug(jsonSave);

            try
            {
                response = NetworkService.SubmitRequest<ApiResponse>(request, NotEntryProfileSubmitEndpoint, Users.AccessToken);

                if (response?.code == (int)HttpResponseStatus.OK)
                {
                    logger.Debug("Not Entry Profile Upload By API is Success. Reference No: " + enrollmentDto?.referenceNo);
                }
                else
                {
                    logger.Debug("Not Entry Profile Upload By API is Failed. Reference No: " + enrollmentDto?.referenceNo);
                    logger.Debug("Upload :: Not Entry Profile :: API Error Message :: " + response?.message + " :: Reference No :: " + enrollmentDto?.referenceNo);
                }

                return response;
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
                throw x;
            }
        }

        public NotEntrySearchResponse NotEntrySearch(NotEntrySearchRequest dto)
        {
            string SearchNotEntryProfileEndpoint = string.Empty;
            try
            {
                SearchNotEntryProfileEndpoint = ConfigurationManager.AppSettings["SearchNotEntryProfileEndpoint"];
            }
            catch (Exception x)
            {
                logger.Error("Error resolving App config for Not Entry enrollment endpoint. " + x.ToString());
            }

            if (string.IsNullOrEmpty(SearchNotEntryProfileEndpoint))
            {
                SearchNotEntryProfileEndpoint = "noentry/search";
            }

            NotEntrySearchResponse response = new NotEntrySearchResponse();
            NotEntrySearchRequest request = dto;

            try
            {
                response = NetworkService.SubmitRequest<NotEntrySearchResponse>(request, SearchNotEntryProfileEndpoint, Users.AccessToken);
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
