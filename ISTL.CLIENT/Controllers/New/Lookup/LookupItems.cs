using ISTL.COMMON.Network;
using ISTL.MODELS.DTO.New.Lookup;
using ISTL.RAB.ApiManager;
using ISTL.RAB.DbManager;
using ISTL.RAB.View;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.Entity.Lookup
{
    public static class Extensions
    {
        public static void Append<K, V>(this Dictionary<K, V> first, Dictionary<K, V> second)
        {
            List<KeyValuePair<K, V>> pairs = second.ToList();
            pairs.ForEach(pair => first.Add(pair.Key, pair.Value));
        }
    }
    public class LookupItems
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        public Dictionary<int, string> nationalityList = new Dictionary<int, string>();
        public Dictionary<int, string> foreignNationalityList = new Dictionary<int, string>();
        public Dictionary<int, string> districtList = new Dictionary<int, string>();
        public Dictionary<int, string> upazillaList = new Dictionary<int, string>();
        public Dictionary<int, string> unionList = new Dictionary<int, string>();
        public Dictionary<int, string> stationList = new Dictionary<int, string>();
        public Dictionary<int, string> subStationList = new Dictionary<int, string>();
        public Dictionary<int, string> crimeTypeList = new Dictionary<int, string>();
        private LookupApiManager lookupApiManager;
        private DbLookupManager dbLookup;

        public LookupItems()
        {
            lookupApiManager = new LookupApiManager();
            dbLookup = new DbLookupManager();
        }

        public void LoadCrimeType()
        {
            crimeTypeList = new Dictionary<int, string>();

            List<CrimeTypeDto> list = new List<CrimeTypeDto>();
            list = dbLookup.GetCrimeType();

            crimeTypeList.Add(0, "Select Crime Type");

            foreach (var obj in list)
            {
                string CrimeTypeEnBn = obj.nameInEnglish;
                if (!string.IsNullOrEmpty(obj.nameInBangla)) CrimeTypeEnBn += " (" + obj.nameInBangla + ")";
                crimeTypeList.Add(Convert.ToInt32(obj.id), CrimeTypeEnBn);
            }
        }

        public void LoadNationality()
        {
            nationalityList = new Dictionary<int, string>();

            List<NationalityDto> list = new List<NationalityDto>();
            list = dbLookup.GetNationality();

            Dictionary<int, string> priorityNationalityList = new Dictionary<int, string>();

            // Test code
            //nationalityList.Add(-1, "Select Nationality");

            for (int i=0; i<list.Count; i++)
            {
                if (list[i].countryNameEn == "Bangladesh")
                {
                    nationalityList.Add(Convert.ToInt32(list[i].id), list[i].countryNameEn);
                }
                else if (list[i].countryNameEn == "Rohingya")
                {
                    nationalityList.Add(Convert.ToInt32(list[i].id), list[i].countryNameEn);
                }
                else
                {
                    priorityNationalityList.Add(Convert.ToInt32(list[i].id), list[i].countryNameEn);
                }
            }
            nationalityList.Append(priorityNationalityList);
        }

        public void LoadForeignNationality()
        {
            foreignNationalityList = new Dictionary<int, string>();

            List<NationalityDto> list = new List<NationalityDto>();
            list = dbLookup.GetNationality();

            Dictionary<int, string> priorityNationalityList = new Dictionary<int, string>();

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].countryNameEn == "Bangladesh")
                {
                    continue;
                }
                else if (list[i].countryNameEn == "Rohingya")
                {
                    foreignNationalityList.Add(Convert.ToInt32(list[i].id), list[i].countryNameEn);
                }
                else
                {
                    priorityNationalityList.Add(Convert.ToInt32(list[i].id), list[i].countryNameEn);
                }
            }
            foreignNationalityList.Append(priorityNationalityList);
        }

        public void LoadDistrict()
        {
            districtList = new Dictionary<int, string>();

            List<DistrictDto> list = new List<DistrictDto>();
            list = dbLookup.GetDistrict();
            for (int i = 0; i < list.Count; i++)
            {
                districtList.Add(Convert.ToInt32(list[i].id), list[i].nameInEnglish);
            }
        }

        public void LoadUpazillaByDistrict(string districtId)
        {
            upazillaList = new Dictionary<int, string>();

            List<UpazilaDto> list = new List<UpazilaDto>();
            list = dbLookup.GetUpazilaByDistrictId(Convert.ToInt32(districtId));
            for (int i = 0; i < list.Count; i++)
            {
                upazillaList.Add(Convert.ToInt32(list[i].id), list[i].nameInEnglish);
            }
        }

        public void LoadUnionByUpazilla(string upazillaId)
        {
            unionList = new Dictionary<int, string>();

            List<UnionDto> list = new List<UnionDto>();
            list = dbLookup.GetUnionByUpazilaId(Convert.ToInt32(upazillaId));
            for (int i = 0; i < list.Count; i++)
            {
                unionList.Add(Convert.ToInt32(list[i].id), list[i].nameInEnglish);
            }
        }

        public void LoadStations()
        {
            stationList = new Dictionary<int, string>(); // Added by Al-Amin

            List<StationDto> list = new List<StationDto>();
            list = dbLookup.GetStation();
            for (int i = 0; i < list.Count; i++)
            {
                stationList.Add(Convert.ToInt32(list[i].id), list[i].nameEn);
            }
        }

        public void LoadSubStations(string stationId)
        {
            subStationList = new Dictionary<int, string>(); // Added by Al-Amin

            List<SubStationDto> list = new List<SubStationDto>();
            list = dbLookup.GetSubStationByStationId(Convert.ToInt32(stationId));
            for (int i = 0; i < list.Count; i++)
            {
                subStationList.Add(Convert.ToInt32(list[i].id), list[i].nameEn);
            }
        }

        public void LoadRabDistrictBySubUnit(int pId)
        {
            districtList = new Dictionary<int, string>();

            List<RabGeoMapDto> geoMaplist = new List<RabGeoMapDto>();
            geoMaplist = dbLookup.GetRabGeoMapBySubUnit(pId);

            List<int> rabDistrictIdList = new List<int>();

            for (int i=0; i < geoMaplist.Count; i++)
            {
                if (geoMaplist[i].districtId != null)   rabDistrictIdList.Add(Convert.ToInt32(geoMaplist[i].districtId));
            }

            List<RabDistrictDto> rabDisrictList = dbLookup.GetRabDistrictsByMultipleIds(rabDistrictIdList);
            if (rabDisrictList != null)
            {
                for (int i=0; i < rabDisrictList.Count; i++)
                {
                    districtList.Add(Convert.ToInt32(rabDisrictList[i].id), rabDisrictList[i].nameEn);
                }
            }
        }

        public void LoadRabUpazillaBySubUnitRabDistrict(string subUnitId, string districtId)
        {
            if (string.IsNullOrEmpty(subUnitId) || string.IsNullOrEmpty(districtId)) return;
            upazillaList = new Dictionary<int, string>();

            List<RabGeoMapDto> geoMaplist = new List<RabGeoMapDto>();
            geoMaplist = dbLookup.GetRabGeoMapBySubUnitAndRabDistrict(Convert.ToInt32(subUnitId), Convert.ToInt32(districtId));

            List<int> rabUpazilaIdList = new List<int>();

            for (int i = 0; i < geoMaplist.Count; i++)
            {
                if (geoMaplist[i].districtId != null) rabUpazilaIdList.Add(Convert.ToInt32(geoMaplist[i].upazilaId));
            }

            List<RabUpazilaDto> rabUpazilaList = new List<RabUpazilaDto>();
            rabUpazilaList = dbLookup.GetRabUpazilaByRabDistrictId(Convert.ToInt32(districtId));
            for (int i = 0; i < rabUpazilaList.Count; i++)
            {
                upazillaList.Add(Convert.ToInt32(rabUpazilaList[i].id), rabUpazilaList[i].nameEn);
            }
        }
    }
}
