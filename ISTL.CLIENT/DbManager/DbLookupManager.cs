using ISTL.COMMON.Common;
using ISTL.COMMON.Database;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.MODELS.DTO.New.Lookup;
using ISTL.PERSOGlobals;
using ISTL.RAB.Entity;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.DbManager
{
    public class DbLookupManager
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private DbOperation dbOperation = null;

        public DbLookupManager()
        {
            dbOperation = new DbOperation(Globals.Database.NAME, Globals.Database.PASSWORD);
        }

        public bool AddNationality(List<NationalityDto> list)
        {
            try
            {
                dbOperation.OpenDbConnection();

                dbOperation.DeleteAll("nationality");

                SQLiteTransaction transaction = dbOperation.GetSqliteConnection().BeginTransaction();
                foreach (var obj in list)
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("id", obj.id);
                    data.Add("country_name_en", obj.countryNameEn);
                    data.Add("country_name_local", obj.countryNameLocal);
                    data.Add("country_nationality", obj.countryNationality);
                    data.Add("country_icao_code", obj.countryIcaoName);
                    data.Add("status", obj.status);
                    dbOperation.InsertData("nationality", data);
                }

                transaction.Commit();
                dbOperation.CloseDbConnection();
                return true;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.Message);
                throw x;
            }
        }

        public List<NationalityDto> GetNationality()
        {
            try
            {
                List<NationalityDto> list = new List<NationalityDto>();
                dbOperation.OpenDbConnection();
                string sql = String.Format("SELECT * FROM nationality");
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    string id = dataRow["id"].ToString();
                    string name = dataRow["country_name_en"].ToString();
                    list.Add(new NationalityDto() { id = Convert.ToInt32(id), countryNameEn = name });
                }
                dbOperation.CloseDbConnection();
                return list;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + Users.Username + ". Got error when getting nationility.\n" + x.ToString());
                throw x;
            }
        }

        public bool AddDistrict(List<DistrictDto> list)
        {
            try
            {
                dbOperation.OpenDbConnection();

                dbOperation.DeleteAll("district");

                SQLiteTransaction transaction = dbOperation.GetSqliteConnection().BeginTransaction();
                foreach (var obj in list)
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("id", obj.id);
                    data.Add("division_id", obj.divisionId);
                    data.Add("name_in_english", obj.nameInEnglish);
                    data.Add("creation_date", obj.creationDate);
                    data.Add("modification_date", obj.modificationDate);
                    data.Add("is_deleted", obj.deleted);
                    data.Add("active", obj.active);
                    data.Add("code", obj.code);
                    data.Add("name_in_bangla", obj.nameInBangla);
                    data.Add("created_by", obj.createdBy);
                    data.Add("modified_by", obj.modifiedBy);
                    dbOperation.InsertData("district", data);
                }

                transaction.Commit();
                dbOperation.CloseDbConnection();
                return true;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.Message);
                throw x;
            }
        }

        public List<DistrictDto> GetDistrict()
        {
            try
            {
                List<DistrictDto> list = new List<DistrictDto>();
                dbOperation.OpenDbConnection();
                string sql = String.Format("SELECT * FROM district");
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    string id = dataRow["id"].ToString();
                    string divId = dataRow["division_id"].ToString();
                    string name = dataRow["name_in_english"].ToString();
                    list.Add(new DistrictDto() { id = Convert.ToInt32(id), divisionId = Convert.ToInt32(id), nameInEnglish = name });
                }
                dbOperation.CloseDbConnection();
                return list;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + Users.Username + ". Got error when getting district.\n" + x.ToString());
                throw x;
            }
        }

        public bool AddUpazila(List<UpazilaDto> list)
        {
            try
            {
                dbOperation.OpenDbConnection();

                dbOperation.DeleteAll("upazila");

                SQLiteTransaction transaction = dbOperation.GetSqliteConnection().BeginTransaction();
                foreach (var obj in list)
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("id", obj.id);
                    data.Add("district_id", obj.districtId);
                    data.Add("name_in_english", obj.nameInEnglish);
                    data.Add("creation_date", obj.creationDate);
                    data.Add("modification_date", obj.modificationDate);
                    data.Add("is_deleted", obj.deleted);
                    data.Add("active", obj.active);
                    data.Add("code", obj.code);
                    data.Add("location_code", obj.locationCode);
                    data.Add("name_in_bangla", obj.nameInBangla);
                    data.Add("created_by", obj.createdBy);
                    data.Add("modified_by", obj.modifiedBy);
                    dbOperation.InsertData("upazila", data);
                }

                transaction.Commit();
                dbOperation.CloseDbConnection();
                return true;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.Message);
                throw x;
            }
        }

        public List<UpazilaDto> GetUpazila()
        {
            try
            {
                List<UpazilaDto> list = new List<UpazilaDto>();
                dbOperation.OpenDbConnection();
                string sql = String.Format("SELECT * FROM upazila");
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    string id = dataRow["id"].ToString();
                    string parentId = dataRow["district_id"].ToString();
                    string name = dataRow["name_in_english"].ToString();
                    list.Add(new UpazilaDto() { id = Convert.ToInt32(id), districtId = Convert.ToInt32(parentId), nameInEnglish = name });
                }
                dbOperation.CloseDbConnection();
                return list;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + Users.Username + ". Got error when getting upazila.\n" + x.ToString());
                throw x;
            }
        }

        public List<UpazilaDto> GetUpazilaByDistrictId(int pId)
        {
            try
            {
                List<UpazilaDto> list = new List<UpazilaDto>();
                dbOperation.OpenDbConnection();
                string wherePart = String.Format("district_id={0}", pId);
                string sql = String.Format("SELECT * FROM upazila WHERE {0} order by name_in_english;", wherePart);
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    string id = dataRow["id"].ToString();
                    string parentId = dataRow["district_id"].ToString();
                    string name = dataRow["name_in_english"].ToString();
                    list.Add(new UpazilaDto() { id = Convert.ToInt32(id), districtId = Convert.ToInt32(parentId), nameInEnglish = name });
                }
                dbOperation.CloseDbConnection();
                return list;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + Users.Username + ". Got error when getting upazila by district id.\n" + x.ToString());
                throw x;
            }
        }


        public bool AddUnion(List<UnionDto> list)
        {
            try
            {
                dbOperation.OpenDbConnection();

                dbOperation.DeleteAll("eunion");

                SQLiteTransaction transaction = dbOperation.GetSqliteConnection().BeginTransaction();
                foreach (var obj in list)
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("id", obj.id);
                    data.Add("upazilla_id", obj.upazillaId);
                    data.Add("name_in_english", obj.nameInEnglish);
                    data.Add("creation_date", obj.creationDate);
                    data.Add("modification_date", obj.modificationDate);
                    data.Add("is_deleted", obj.deleted);
                    data.Add("active", obj.active);
                    data.Add("code", obj.code);
                    data.Add("name_in_bangla", obj.nameInBangla);
                    data.Add("created_by", obj.createdBy);
                    data.Add("modified_by", obj.modifiedBy);
                    dbOperation.InsertData("eunion", data);
                }

                transaction.Commit();
                dbOperation.CloseDbConnection();
                return true;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.Message);
                throw x;
            }
        }

        public List<UnionDto> GetUnion()
        {
            try
            {
                List<UnionDto> list = new List<UnionDto>();
                dbOperation.OpenDbConnection();
                string sql = String.Format("SELECT * FROM eunion");
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    string id = dataRow["id"].ToString();
                    string parentId = dataRow["upazilla_id"].ToString();
                    string name = dataRow["name_in_english"].ToString();
                    list.Add(new UnionDto() { id = Convert.ToInt32(id), upazillaId = Convert.ToInt32(parentId), nameInEnglish = name });
                }
                dbOperation.CloseDbConnection();
                return list;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + Users.Username + ". Got error when getting union.\n" + x.ToString());
                throw x;
            }
        }

        public List<UnionDto> GetUnionByUpazilaId(int pId)
        {
            try
            {
                List<UnionDto> list = new List<UnionDto>();
                dbOperation.OpenDbConnection();
                string wherePart = String.Format("upazilla_id={0}", pId);
                string sql = String.Format("SELECT * FROM eunion WHERE {0} order by name_in_english;", wherePart);
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    string id = dataRow["id"].ToString();
                    string parentId = dataRow["upazilla_id"].ToString();
                    string name = dataRow["name_in_english"].ToString();
                    list.Add(new UnionDto() { id = Convert.ToInt32(id), upazillaId = Convert.ToInt32(parentId), nameInEnglish = name });
                }
                dbOperation.CloseDbConnection();
                return list;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + Users.Username + ". Got error when getting union by upazila id.\n" + x.ToString());
                throw x;
            }
        }

        public bool AddStation(List<StationDto> list)
        {
            try
            {
                dbOperation.OpenDbConnection();

                dbOperation.DeleteAll("station");

                SQLiteTransaction transaction = dbOperation.GetSqliteConnection().BeginTransaction();
                foreach (var obj in list)
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("id", obj.id);
                    data.Add("name_code", obj.nameCode);
                    data.Add("name_en", obj.nameEn);
                    dbOperation.InsertData("station", data);
                }

                transaction.Commit();
                dbOperation.CloseDbConnection();
                return true;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.Message);
                throw x;
            }
        }

        public List<StationDto> GetStation()
        {
            try
            {
                List<StationDto> list = new List<StationDto>();
                dbOperation.OpenDbConnection();
                string sql = String.Format("SELECT * FROM station");
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    string id = dataRow["id"].ToString();
                    string nameCode = dataRow["name_code"].ToString();
                    string nameEn = dataRow["name_en"].ToString();
                    list.Add(new StationDto() { id = Convert.ToInt32(id), nameCode = nameCode, nameEn = nameEn });
                }
                dbOperation.CloseDbConnection();
                return list;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + Users.Username + ". Got error when getting station.\n" + x.ToString());
                throw x;
            }
        }

        public bool AddSubStation(List<SubStationDto> list)
        {
            try
            {
                dbOperation.OpenDbConnection();

                dbOperation.DeleteAll("sub_station");

                SQLiteTransaction transaction = dbOperation.GetSqliteConnection().BeginTransaction();
                foreach (var obj in list)
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("id", obj.id);
                    data.Add("name_en", obj.nameEn);
                    data.Add("station_id", obj.unit);
                    data.Add("order_id", obj.orderId);
                    dbOperation.InsertData("sub_station", data);
                }
                transaction.Commit();
                dbOperation.CloseDbConnection();
                return true;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.Message);
                throw x;
            }
        }

        public List<SubStationDto> GetSubStation()
        {
            try
            {
                List<SubStationDto> list = new List<SubStationDto>();
                dbOperation.OpenDbConnection();
                string sql = String.Format("SELECT * FROM sub_station");
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    string id = dataRow["id"].ToString();
                    string nameEn = dataRow["name_en"].ToString();
                    string stationId = dataRow["station_id"].ToString();
                    list.Add(new SubStationDto() { id = Convert.ToInt32(id), nameEn = nameEn, unit = Convert.ToInt32(stationId) });
                }
                dbOperation.CloseDbConnection();
                return list;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + Users.Username + ". Got error when getting station.\n" + x.ToString());
                throw x;
            }
        }

        public List<SubStationDto> GetSubStationByStationId(int pId)
        {
            try
            {
                List<SubStationDto> list = new List<SubStationDto>();
                dbOperation.OpenDbConnection();
                string wherePart = String.Format("station_id={0}", pId);
                string sql = String.Format("SELECT * FROM sub_station WHERE {0} ORDER BY order_id;", wherePart);
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    string id = dataRow["id"].ToString();
                    string nameEn = dataRow["name_en"].ToString();
                    string stationId = dataRow["station_id"].ToString();
                    list.Add(new SubStationDto() { id = Convert.ToInt32(id), nameEn = nameEn, unit = Convert.ToInt32(stationId) });
                }
                dbOperation.CloseDbConnection();
                return list;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + Users.Username + ". Got error when getting union by upazila id.\n" + x.ToString());
                throw x;
            }
        }

        public bool AddRecovery(List<RecoveryDto> list)
        {
            try
            {
                dbOperation.OpenDbConnection();

                dbOperation.DeleteAll("recovery_lookup");

                SQLiteTransaction transaction = dbOperation.GetSqliteConnection().BeginTransaction();
                foreach (var obj in list)
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("id", obj.id);
                    data.Add("recovery_type", obj.lookupType);
                    data.Add("recovery_name_en", obj.lookupNameEn);
                    data.Add("short_id", obj.shortId);
                    dbOperation.InsertData("recovery_lookup", data);
                }
                transaction.Commit();
                dbOperation.CloseDbConnection();
                return true;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.Message);
                throw x;
            }
        }

        public List<RecoveryDto> GetRecovery()
        {
            try
            {
                List<RecoveryDto> list = new List<RecoveryDto>();
                dbOperation.OpenDbConnection();
                string sql = String.Format("SELECT * FROM recovery_lookup");
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    string id = dataRow["id"].ToString();
                    string recoveryType = dataRow["recovery_type"].ToString();
                    string recoveryName = dataRow["recovery_name_en"].ToString();
                    string shortId = dataRow["short_id"].ToString();
                    list.Add(new RecoveryDto() { id = Convert.ToInt32(id), lookupType = recoveryType, lookupNameEn = recoveryName, shortId = Convert.ToInt32(shortId) });
                }
                dbOperation.CloseDbConnection();
                return list;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + Users.Username + ". Got error when getting station.\n" + x.ToString());
                throw x;
            }
        }

        public List<RecoveryDto> GetRecoveryTypeList()
        {
            try
            {
                List<RecoveryDto> list = new List<RecoveryDto>();
                dbOperation.OpenDbConnection();
                string sql = String.Format("SELECT DISTINCT recovery_type FROM recovery_lookup");
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    string recoveryType = dataRow["recovery_type"].ToString();
                    list.Add(new RecoveryDto() { lookupType = recoveryType });
                }
                dbOperation.CloseDbConnection();
                return list;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + Users.Username + ". Got error when getting station.\n" + x.ToString());
                throw x;
            }
        }

        public List<RecoveryDto> GetRecoveryByType(string pId)
        {
            try
            {
                List<RecoveryDto> list = new List<RecoveryDto>();
                dbOperation.OpenDbConnection();
                string wherePart = String.Format("recovery_type='{0}'", pId);
                string sql = String.Format("SELECT * FROM recovery_lookup WHERE {0} order by recovery_name_en;", wherePart);
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    string id = dataRow["id"].ToString();
                    string recoveryType = dataRow["recovery_type"].ToString();
                    string recoveryName = dataRow["recovery_name_en"].ToString();
                    string shortId = dataRow["short_id"].ToString();
                    list.Add(new RecoveryDto() { id = Convert.ToInt32(id), lookupType = recoveryType, lookupNameEn = recoveryName, shortId = Convert.ToInt32(shortId) });
                }
                dbOperation.CloseDbConnection();
                return list;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + Users.Username + ". Got error when getting union by upazila id.\n" + x.ToString());
                throw x;
            }
        }

        public string GetValueForLookup(string columnName, string tableName, int id)
        {
            try
            {
                string value = string.Empty;
                string sql = String.Format("SELECT " + columnName + " FROM " + tableName + " WHERE id={0}", id);
                dbOperation.OpenDbConnection();
                //string value = dbOperation.ExecuteScalarReturnStr(sql);
                DataTable dataTable = dbOperation.GetDataTable(sql);
                dbOperation.CloseDbConnection();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    value = dataRow[columnName].GetType() != typeof(DBNull) ? Convert.ToString(dataRow[columnName]) : null;
                }
                return value;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + Users.Username + ". Got error when getting lookup data value.\n" + x.ToString());
            }
            return null;
        }

        public int GetTableVersion(string tableName)
        {
            try
            {
                int value = 0;
                string sql = String.Format("SELECT lookup_table_version FROM lookup_version WHERE lookup_table_name='{0}';", tableName);
                dbOperation.OpenDbConnection();
                //int value = dbOperation.ExecuteScalarReturnInt(sql);
                //dbOperation.CloseDbConnection();

                DataTable dataTable = dbOperation.GetDataTable(sql);
                dbOperation.CloseDbConnection();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    value = dataRow["lookup_table_version"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["lookup_table_version"]) : 0;
                }

                return value;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + Users.Username + ". Got error when getting table version.\n" + x.ToString());
                throw x;
            }
        }

        public int UpdateTableVersion(string tableName, int status)
        {
            try
            {
                string sql = String.Format("UPDATE lookup_version SET lookup_table_version = {0} WHERE lookup_table_name='{1}';", status, tableName);
                dbOperation.OpenDbConnection();
                int value = dbOperation.ExecuteQuery(sql);
                dbOperation.CloseDbConnection();
                return value;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + Users.Username + ". Got error when getting table version.\n" + x.ToString());
                throw x;
            }
        }

        public bool AddRabGeoMap(List<RabGeoMapDto> list)
        {
            try
            {
                dbOperation.OpenDbConnection();

                dbOperation.DeleteAll("rab_geo_map");

                SQLiteTransaction transaction = dbOperation.GetSqliteConnection().BeginTransaction();
                foreach (var obj in list)
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("id", obj.id);
                    data.Add("unit_id", obj.unitId);
                    data.Add("subunit_id", obj.subUnitId);
                    data.Add("district_id", obj.districtId);
                    data.Add("upazila_id", obj.upazilaId);
                    dbOperation.InsertData("rab_geo_map", data);
                }
                transaction.Commit();
                dbOperation.CloseDbConnection();
                return true;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.Message);
                throw x;
            }
        }

        public List<RabGeoMapDto> GetRabGeoMap()
        {
            try
            {
                List<RabGeoMapDto> list = new List<RabGeoMapDto>();
                dbOperation.OpenDbConnection();
                string sql = String.Format("SELECT * FROM rab_geo_map");
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    string id = dataRow["id"].GetType() != typeof(DBNull) ? Convert.ToString(Convert.ToInt64(dataRow["id"])) : null;
                    string unitId = dataRow["unit_id"].GetType() != typeof(DBNull) ? Convert.ToString(Convert.ToInt64(dataRow["unit_id"])) : null;
                    string subUnitId = dataRow["subunit_id"].GetType() != typeof(DBNull) ? Convert.ToString(Convert.ToInt64(dataRow["subunit_id"])) : null;
                    string districtId = dataRow["district_id"].GetType() != typeof(DBNull) ? Convert.ToString(Convert.ToInt64(dataRow["district_id"])) : null;
                    string upazilaId = dataRow["upazila_id"].GetType() != typeof(DBNull) ? Convert.ToString(Convert.ToInt64(dataRow["upazila_id"])) : null;
                    list.Add(new RabGeoMapDto() { 
                        id = Convert.ToInt32(id),
                        unitId = Convert.ToInt32(unitId),
                        subUnitId = Convert.ToInt32(subUnitId),
                        districtId = Convert.ToInt32(districtId),
                        upazilaId = Convert.ToInt32(upazilaId)
                    });
                }
                dbOperation.CloseDbConnection();
                return list;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + Users.Username + ". Got error when getting rab_geo_map.\n" + x.ToString());
                throw x;
            }
        }

        public List<RabGeoMapDto> GetRabGeoMapBySubUnit(int subUnit)
        {
            try
            {
                List<RabGeoMapDto> list = new List<RabGeoMapDto>();
                dbOperation.OpenDbConnection();
                string wherePart = String.Format("subunit_id={0}", subUnit);
                string sql = String.Format("SELECT * FROM rab_geo_map WHERE {0};", wherePart);
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    string id = dataRow["id"].ToString();
                    string unitId = dataRow["unit_id"].ToString();
                    string subUnitId = dataRow["subunit_id"].ToString();
                    string districtId = dataRow["district_id"].ToString();
                    string upazilaId = dataRow["upazila_id"].ToString();
                    list.Add(new RabGeoMapDto()
                    {
                        id = Convert.ToInt32(id),
                        unitId = Convert.ToInt32(unitId),
                        subUnitId = Convert.ToInt32(subUnitId),
                        districtId = Convert.ToInt32(districtId),
                        upazilaId = Convert.ToInt32(upazilaId)
                    });
                }
                dbOperation.CloseDbConnection();
                return list;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + Users.Username + ". Got error when getting rab_geo_map.\n" + x.ToString());
                throw x;
            }
        }

        public List<RabGeoMapDto> GetRabGeoMapBySubUnitAndRabDistrict(int subUnit, int district_Id)
        {
            try
            {
                List<RabGeoMapDto> list = new List<RabGeoMapDto>();
                dbOperation.OpenDbConnection();
                string wherePart = String.Format("subunit_id={0} AND district_id={1}", subUnit, district_Id);
                string sql = String.Format("SELECT * FROM rab_geo_map WHERE {0};", wherePart);
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    string id = dataRow["id"].ToString();
                    string unitId = dataRow["unit_id"].ToString();
                    string subUnitId = dataRow["subunit_id"].ToString();
                    string districtId = dataRow["district_id"].ToString();
                    string upazilaId = dataRow["upazila_id"].ToString();
                    list.Add(new RabGeoMapDto()
                    {
                        id = Convert.ToInt32(id),
                        unitId = Convert.ToInt32(unitId),
                        subUnitId = Convert.ToInt32(subUnitId),
                        districtId = Convert.ToInt32(districtId),
                        upazilaId = Convert.ToInt32(upazilaId)
                    });
                }
                dbOperation.CloseDbConnection();
                return list;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + Users.Username + ". Got error when getting rab_geo_map.\n" + x.ToString());
                throw x;
            }
        }

        public bool AddRabDistrict(List<RabDistrictDto> list)
        {
            try
            {
                dbOperation.OpenDbConnection();

                dbOperation.DeleteAll("rab_district");

                SQLiteTransaction transaction = dbOperation.GetSqliteConnection().BeginTransaction();
                foreach (var obj in list)
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("id", obj.id);
                    data.Add("name_en", obj.nameEn);
                    dbOperation.InsertData("rab_district", data);
                }
                transaction.Commit();
                dbOperation.CloseDbConnection();
                return true;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.Message);
                throw x;
            }
        }

        public List<RabDistrictDto> GetRabDistrict()
        {
            try
            {
                List<RabDistrictDto> list = new List<RabDistrictDto>();
                dbOperation.OpenDbConnection();
                string sql = String.Format("SELECT * FROM rab_district");
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    string id = dataRow["id"].ToString();
                    string nameEn = dataRow["name_en"].ToString();
                    list.Add(new RabDistrictDto()
                    {
                        id = Convert.ToInt32(id),
                        nameEn = nameEn
                    });
                }
                dbOperation.CloseDbConnection();
                return list;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + Users.Username + ". Got error when getting rab_district.\n" + x.ToString());
                throw x;
            }
        }

        public List<RabDistrictDto> GetRabDistrictsByMultipleIds(List<int> districtIds)
        {
            try
            {
                string districtIDList = string.Empty;
                for (int i=0; i<districtIds.Count; i++)
                {
                    districtIDList += districtIds[i];
                    if (i != districtIds.Count - 1) districtIDList += ", ";
                }

                List<RabDistrictDto> list = new List<RabDistrictDto>();
                dbOperation.OpenDbConnection();
                string wherePart = String.Format("id in ({0})", districtIDList);
                string sql = String.Format("SELECT * FROM rab_district WHERE {0};", wherePart);
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    string id = dataRow["id"].ToString();
                    string nameEn = dataRow["name_en"].ToString();
                    list.Add(new RabDistrictDto()
                    {
                        id = Convert.ToInt32(id),
                        nameEn = nameEn
                    });
                }
                dbOperation.CloseDbConnection();
                return list;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + Users.Username + ". Got error when getting rab_geo_map.\n" + x.ToString());
                throw x;
            }
        }

        public bool AddRabUpazila(List<RabUpazilaDto> list)
        {
            try
            {
                dbOperation.OpenDbConnection();

                dbOperation.DeleteAll("rab_upazila");

                SQLiteTransaction transaction = dbOperation.GetSqliteConnection().BeginTransaction();
                foreach (var obj in list)
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("id", obj.id);
                    data.Add("name_en", obj.nameEn);
                    data.Add("district_id", obj.districtId);
                    dbOperation.InsertData("rab_upazila", data);
                }
                transaction.Commit();
                dbOperation.CloseDbConnection();
                return true;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.Message);
                throw x;
            }
        }

        public List<RabUpazilaDto> GetRabUpazila()
        {
            try
            {
                List<RabUpazilaDto> list = new List<RabUpazilaDto>();
                dbOperation.OpenDbConnection();
                string sql = String.Format("SELECT * FROM rab_upazila");
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    string id = dataRow["id"].ToString();
                    string districtId = dataRow["district_id"].ToString();
                    string nameEn = dataRow["name_en"].ToString();
                    list.Add(new RabUpazilaDto() { id = Convert.ToInt32(id), districtId = Convert.ToInt32(districtId), nameEn = nameEn });
                }
                dbOperation.CloseDbConnection();
                return list;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + Users.Username + ". Got error when getting rab_district.\n" + x.ToString());
                throw x;
            }
        }

        public List<RabUpazilaDto> GetRabUpazilaByRabDistrictId(int pId)
        {
            try
            {
                List<RabUpazilaDto> list = new List<RabUpazilaDto>();
                dbOperation.OpenDbConnection();
                string wherePart = String.Format("district_id={0}", pId);
                string sql = String.Format("SELECT * FROM rab_upazila WHERE {0};", wherePart);
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    string id = dataRow["id"].ToString();
                    string districtId = dataRow["district_id"].ToString();
                    string nameEn = dataRow["name_en"].ToString();
                    list.Add(new RabUpazilaDto() { id = Convert.ToInt32(id), districtId = Convert.ToInt32(districtId), nameEn = nameEn });
                }
                dbOperation.CloseDbConnection();
                return list;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + Users.Username + ". Got error when getting rab_upazila by rab_district id.\n" + x.ToString());
                throw x;
            }
        }

        public bool AddCrimeType(List<CrimeTypeDto> list)
        {
            try
            {
                dbOperation.OpenDbConnection();

                dbOperation.DeleteAll("crime_type");

                SQLiteTransaction transaction = dbOperation.GetSqliteConnection().BeginTransaction();
                foreach (var obj in list)
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("id", obj.id);
                    data.Add("name_bn", obj.nameInBangla);
                    data.Add("name_en", obj.nameInEnglish);
                    data.Add("sort_id", obj.shortId);
                    dbOperation.InsertData("crime_type", data);
                }
                transaction.Commit();
                dbOperation.CloseDbConnection();
                return true;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.Message);
                throw x;
            }
        }

        public List<CrimeTypeDto> GetCrimeType()
        {
            try
            {
                List<CrimeTypeDto> list = new List<CrimeTypeDto>();
                dbOperation.OpenDbConnection();
                string sql = String.Format("SELECT * FROM crime_type order by name_en");
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    string id = dataRow["id"].ToString();
                    string nameBn = dataRow["name_bn"].ToString();
                    string nameEn = dataRow["name_en"].ToString();
                    list.Add(new CrimeTypeDto()
                    {
                        id = Convert.ToInt32(id),
                        nameInEnglish = nameEn,
                        nameInBangla = nameBn
                    });
                }
                dbOperation.CloseDbConnection();
                return list;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + Users.Username + ". Got error when getting crime_type.\n" + x.ToString());
                throw x;
            }
        }
    }
}
