using ISTL.COMMON.Network;
using ISTL.MODELS.DTO.New.Lookup;
using ISTL.MODELS.Request.New;
using ISTL.MODELS.Response.New.Lookup;
using ISTL.RAB.DbManager;
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
    public class LookupApiManager
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private readonly string GetAllNationalityEndpoint = ConfigurationManager.AppSettings["AllNationalityEndpoint"].ToString();
        private readonly string GetAllDistrictsEndpoint = ConfigurationManager.AppSettings["AllDistrictEndpoint"].ToString();
        private readonly string GetAllUpazillasEndpoint = ConfigurationManager.AppSettings["AllUpazillaEndpoint"].ToString();
        private readonly string GetUpazillaByDistrictEndpoint = ConfigurationManager.AppSettings["GetUpazillaListByDistrictEndpoint"].ToString();
        private readonly string GetAllUnionsEndpoint = ConfigurationManager.AppSettings["AllUnionEndpoint"].ToString();
        private readonly string GetUnionByUpazillaEndpoint = ConfigurationManager.AppSettings["GetUnionListByUpazillaEndpoint"].ToString();
        private readonly string GetAllUnitsEndpoint = ConfigurationManager.AppSettings["GetAllUnitsEndpoint"].ToString();
        private readonly string GetAllSubUnitsEndpoint = ConfigurationManager.AppSettings["GetAllSubUnitsEndpoint"].ToString();
        private readonly string GetSubUnitsByStationEndpoint = ConfigurationManager.AppSettings["GetSubUnitsByStationEndpoint"].ToString();
        private readonly string GetRabGeoMapEndpoint = ConfigurationManager.AppSettings["GetRabGeoMapEndpoint"].ToString();
        private readonly string GetRabDistrictEndpoint = ConfigurationManager.AppSettings["GetRabDistrictEndpoint"].ToString();
        private readonly string GetRabUpazilaEndpoint = ConfigurationManager.AppSettings["GetRabUpazilaEndpoint"].ToString();
        private readonly string GetRecoveryEndpoint = ConfigurationManager.AppSettings["GetRecoveryEndpoint"].ToString();
        
        public List<NationalityDto> GetAllNationality()
        {
            List<NationalityDto> response = new List<NationalityDto>();
            try
            {
                response = NetworkService.SubmitRequest<List<NationalityDto>>(null, GetAllNationalityEndpoint, "GET", Users.AccessToken);
                return response;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }

            return null;
        }

        public List<DistrictDto> GetAllDistricts()
        {
            List<DistrictDto> response = new List<DistrictDto>();
            try
            {
                response = NetworkService.SubmitRequest<List<DistrictDto>>(null, GetAllDistrictsEndpoint, "GET", Users.AccessToken);
                return response;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }

            return null;
        }

        public List<UpazilaDto> GetAllUpazilla()
        {
            List<UpazilaDto> response = new List<UpazilaDto>();
            try
            {
                response = NetworkService.SubmitRequest<List<UpazilaDto>>(null, GetAllUpazillasEndpoint, "GET", Users.AccessToken);
                return response;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }

            return null;
        }

        public List<UpazilaDto> GetUpazillaListByDistrictId(string districtId)
        {
            List<UpazilaDto> response = new List<UpazilaDto>();

            try
            {
                response = NetworkService.SubmitRequest<List<UpazilaDto>>(null, GetUpazillaByDistrictEndpoint + "?districtId=" + districtId, "GET", Users.AccessToken);

                return response;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return null;            
        }

        public List<UnionDto> GetAllUnions()
        {
            List<UnionDto> response = new List<UnionDto>();
            try
            {
                response = NetworkService.SubmitRequest<List<UnionDto>>(null, GetAllUnionsEndpoint, "GET", Users.AccessToken);
                return response;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }

            return null;
        }

        public List<UnionDto> GetUnionByUpazillaId(string upazillaId)
        {
            List<UnionDto> response = new List<UnionDto>();

            try
            {
                response = NetworkService.SubmitRequest<List<UnionDto>>(null, GetUnionByUpazillaEndpoint + "?upazilaId=" + upazillaId, "GET", Users.AccessToken);

                return response;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }

            return null;
        }

        public List<StationDto> GetAllStations()
        {
            List<StationDto> response = new List<StationDto>();

            try
            {
                response = NetworkService.SubmitRequest<List<StationDto>>(null, GetAllUnitsEndpoint, "GET", Users.AccessToken);

                return response;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }

            return null;
        }

        public List<SubStationDto> GetAllSubStations()
        {
            List<SubStationDto> response = new List<SubStationDto>();

            try
            {
                response = NetworkService.SubmitRequest<List<SubStationDto>>(null, GetAllSubUnitsEndpoint, "GET", Users.AccessToken);

                return response;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return null;
        }

        public List<SubStationDto> GetSubStationListByStationId(string stationId)
        {
            List<SubStationDto> response = new List<SubStationDto>();

            try
            {
                response = NetworkService.SubmitRequest<List<SubStationDto>>(null, GetSubUnitsByStationEndpoint + "?stationId=" + stationId, "GET", Users.AccessToken);

                return response;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return null;
        }

        public List<RabGeoMapDto> GetRabGeoMap()
        {
            GetGeoMapListResponse response = new GetGeoMapListResponse();

            try
            {
                response = NetworkService.SubmitRequest<GetGeoMapListResponse>(null, GetRabGeoMapEndpoint, "GET", Users.AccessToken);

                return response?.rabGeoMapList;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return null;
        }

        public List<RabDistrictDto> GetAllRabDistrict()
        {
            GetRabDistrictResponse response = new GetRabDistrictResponse();

            try
            {
                response = NetworkService.SubmitRequest<GetRabDistrictResponse>(null, GetRabDistrictEndpoint, "GET", Users.AccessToken);

                return response?.rabDistrictList;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return null;
        }

        public List<RabUpazilaDto> GetAllRabUpazila()
        {
            GetRabUpazilaResponse response = new GetRabUpazilaResponse();

            try
            {
                response = NetworkService.SubmitRequest<GetRabUpazilaResponse>(null, GetRabUpazilaEndpoint, "GET", Users.AccessToken);

                return response?.rabUpazilaList;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return null;
        }

        public GetRecoveryResponse GetAllRecovery()
        {
            GetRecoveryResponse response = new GetRecoveryResponse();
            GetRecoveryRequest request = new GetRecoveryRequest();

            try
            {
                response = NetworkService.SubmitRequest<GetRecoveryResponse>(request, GetRecoveryEndpoint, "POST", Users.AccessToken);

                return response;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return null;
        }

        public List<CrimeTypeDto> GetAllCrimeType()
        {
            string GetCrimeTypeEndpoint = string.Empty;
            try
            {
                GetCrimeTypeEndpoint = ConfigurationManager.AppSettings["GetAllCrimeTypeEndpoint"];
            }
            catch (Exception x)
            {
                logger.Error("Error resolving App config for all crime type endpoint" + x.ToString());
                GetCrimeTypeEndpoint = "api/lookup/crimeType/all";
            }

            if (string.IsNullOrEmpty(GetCrimeTypeEndpoint) || string.IsNullOrWhiteSpace(GetCrimeTypeEndpoint))
            {
                GetCrimeTypeEndpoint = "api/lookup/crimeType/all";
            }

            List<CrimeTypeDto> response = new List<CrimeTypeDto>();

            try
            {
                response = NetworkService.SubmitRequest<List<CrimeTypeDto>>(null, GetCrimeTypeEndpoint, "GET", Users.AccessToken);

                return response;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return null;
        }
    }
}
