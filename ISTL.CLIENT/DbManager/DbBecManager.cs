using ISTL.COMMON.Database;
using ISTL.COMMON.Subscription;
using ISTL.MODELS.DTO.New.Enrollment.BECverify;
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
    public class DbBecManager
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private DbOperation dbOperation = null;

        public DbBecManager()
        {
            dbOperation = new DbOperation(Globals.Database.NAME, Globals.Database.PASSWORD);
        }

        public int GetTotalCount()
        {
            int totalCount = 0;
            try
            {
                string sql = "SELECT COUNT(DISTINCT token) FROM bec_identification;";

                dbOperation.OpenDbConnection();
                totalCount = Convert.ToInt32(dbOperation.GetRowCount(sql));
                dbOperation.CloseDbConnection();
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.ToString());
                throw x;
            }
            return totalCount;
        }

        public bool AddRequest(List<BECvoterInfoDto> list)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                dbOperation.OpenDbConnection();
                SQLiteTransaction transaction = dbOperation.GetSqliteConnection().BeginTransaction();

                foreach (var obj in list)
                {
                    data.Add("token", obj.token);
                    data.Add("dob", obj.dob);
                    data.Add("father", obj.father);
                    data.Add("gender", obj.gender);
                    data.Add("marital_status", obj.maritalStatus);
                    data.Add("mother", obj.mother);
                    data.Add("nameBn", obj.name);
                    data.Add("nameEn", obj.nameEn);
                    data.Add("nid", obj.nid);
                    data.Add("occupation", obj.occupation);
                    data.Add("permanent_address", obj.permanentAddress);
                    data.Add("present_address", obj.presentAddress);
                    data.Add("religion", obj.religion);
                    data.Add("score", obj.score);
                    data.Add("photo", obj.photo);
                    data.Add("blood_group", obj.bloodGroup);
                    data.Add("user_id", Users.Id);
                    data.Add("status", obj.status);
                    data.Add("created_at", obj.createdAt);
                    dbOperation.InsertData("bec_identification", data);
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

        public bool UpdateRequest(List<BECvoterInfoDto> list, int status, string createdAt, string resultFoundAt, int userId, string token)
        {
            try
            {
                if (status == (int)NidSearchSubject.Status.FOUND)
                {
                    DeleteRequest(token);
                }

                Dictionary<string, object> data = new Dictionary<string, object>();
                dbOperation.OpenDbConnection();
                SQLiteTransaction transaction = dbOperation.GetSqliteConnection().BeginTransaction();

                foreach (var obj in list)
                {
                    string wherePart = String.Format(" token='{0}'", token);

                    data = new Dictionary<string, object>();
                    data.Add("token", token);

                    if (string.IsNullOrWhiteSpace(obj.dob))
                    {
                        data.Add("dob", null);
                    }
                    else
                    {
                        data.Add("dob", obj.dob);
                    }

                    data.Add("father", obj.father);
                    data.Add("gender", obj.gender);
                    data.Add("marital_status", obj.maritalStatus);
                    data.Add("mother", obj.mother);
                    data.Add("nameBn", obj.name);
                    data.Add("nameEn", obj.nameEn);
                    data.Add("nid", obj.nid);
                    data.Add("occupation", obj.occupation);
                    data.Add("permanent_address", obj.permanentAddress);
                    data.Add("present_address", obj.presentAddress);
                    data.Add("religion", obj.religion);
                    data.Add("score", obj.score);
                    data.Add("photo", obj.photo);
                    data.Add("blood_group", obj.bloodGroup);
                    data.Add("user_id", userId);
                    data.Add("status", status);
                    data.Add("created_at", createdAt);
                    data.Add("result_found_at", resultFoundAt);

                    if (status == (int)NidSearchSubject.Status.FOUND)
                    {
                        dbOperation.InsertData("bec_identification", data);
                    }
                    else
                    {
                        dbOperation.Update("bec_identification", data, wherePart);
                    }
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
        
        public List<BECvoterInfoDto> GetRequestList(string filter, int offset, int limit, string token = null)
        {
            List<BECvoterInfoDto> list = new List<BECvoterInfoDto>();
            try
            {
                dbOperation.OpenDbConnection();
                string wherePart = "";
                string sql = "";

                if (filter == "ALL")
                {
                    sql = "SELECT * FROM bec_identification GROUP BY token ORDER BY id DESC LIMIT " + limit + " OFFSET " + offset + ";";
                }
                else if (filter == "PENDING")
                {
                    wherePart = "status = " + (int)NidSearchSubject.Status.PENDING;
                    sql = String.Format("SELECT * FROM bec_identification WHERE {0} ORDER BY id DESC;", wherePart);
                }
                else if(token != null)
                {
                    wherePart = "token = '" + token + "'";
                    sql = String.Format("SELECT * FROM bec_identification WHERE {0} ORDER BY id DESC;", wherePart);
                }
                
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    BECvoterInfoDto obj = new BECvoterInfoDto();
                    obj.id = dataRow["id"] != DBNull.Value ? Convert.ToInt32(dataRow["id"].ToString()) : 0;
                    obj.token = dataRow["token"] != DBNull.Value ? dataRow["token"].ToString() : "";
                    obj.father = dataRow["father"] != DBNull.Value ? dataRow["father"].ToString() : "";
                    obj.gender = dataRow["gender"] != DBNull.Value ? dataRow["gender"].ToString() : "";
                    obj.mother = dataRow["mother"] != DBNull.Value ? dataRow["mother"].ToString() : "";
                    obj.name = dataRow["nameBn"] != DBNull.Value ? dataRow["nameBn"].ToString() : "";
                    obj.nameEn = dataRow["nameEn"] != DBNull.Value ? dataRow["nameEn"].ToString() : "";
                    obj.nid = dataRow["nid"] != DBNull.Value ? dataRow["nid"].ToString() : "";
                    obj.occupation = dataRow["occupation"] != DBNull.Value ? dataRow["occupation"].ToString() : "";
                    obj.permanentAddress = dataRow["permanent_address"] != DBNull.Value ? dataRow["permanent_address"].ToString() : "";
                    obj.presentAddress = dataRow["present_address"] != DBNull.Value ? dataRow["present_address"].ToString() : "";
                    obj.bloodGroup = dataRow["blood_group"] != DBNull.Value ? dataRow["blood_group"].ToString() : "";
                    obj.religion = dataRow["religion"] != DBNull.Value ? dataRow["religion"].ToString() : "";
                    obj.score = dataRow["score"] != DBNull.Value ? Convert.ToInt32(dataRow["score"].ToString()) : 0;
                    obj.photo = dataRow["photo"] != DBNull.Value ? (byte[])dataRow["photo"] : null;
                    obj.userId = dataRow["user_id"] != DBNull.Value ? Convert.ToInt32(dataRow["user_id"].ToString()) : -1;
                    obj.status = dataRow["status"] != DBNull.Value ? Convert.ToInt32(dataRow["status"].ToString()) : -1;
                    obj.createdAt = dataRow["created_at"] != DBNull.Value ? ((DateTime)dataRow["created_at"]).ToString("yyyy-MM-ddTHH:mm:ss.fffK") : "";
                    obj.resultFoundAt = dataRow["result_found_at"] != DBNull.Value ? ((DateTime)dataRow["result_found_at"]).ToString("yyyy-MM-ddTHH:mm:ss.fffK") : "";
                    obj.createdAtCustom = dataRow["created_at"] != DBNull.Value ? ((DateTime)dataRow["created_at"]).ToString("dd/MM/yyyy HH:mm:ss") : "";
                    obj.resultFoundAtCustom = dataRow["result_found_at"] != DBNull.Value ? ((DateTime)dataRow["result_found_at"]).ToString("dd/MM/yyyy HH:mm:ss") : "";
                    obj.dob = dataRow["dob"] != DBNull.Value ? ((DateTime)dataRow["dob"]).ToString("dd/MM/yyyy") : "";

                    list.Add(obj);
                }
                dbOperation.CloseDbConnection();
                return list;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.ToString());
                throw x;
            }
        }

        public void DeleteRequest(string token)
        {
            try
            {
                dbOperation.OpenDbConnection();
                string sql = String.Format("token= '{0}'", token);
                dbOperation.Delete("bec_identification", sql);
                dbOperation.CloseDbConnection();
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.Message);
            }
        }
    }
}
