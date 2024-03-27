using ISTL.COMMON.Database;
using ISTL.MODELS.DTO.Report;
using ISTL.PERSOGlobals;
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
    public class DbReportManager
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private DbOperation dbOperation = null;

        public DbReportManager()
        {
            dbOperation = new DbOperation(Globals.Database.NAME, Globals.Database.PASSWORD);
        }

        public int GetTotalCountReport()
        {
            int totalCount = 0;
            try
            {                
                string sql = "SELECT COUNT(token) FROM report;";

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

        public bool AddReport(ReportResult obj)
        {
            bool isAdded = false;
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("report_type", obj.reportType);
                data.Add("unit", obj.unit);
                data.Add("sub_unit", obj.subUnit);
                data.Add("age_range", obj.ageRange);
                data.Add("arrest_type", obj.arrestType);
                data.Add("creation_date_from", obj.creationDateFromDt);
                data.Add("creation_date_to", obj.creationDateToDt);
                data.Add("curr_date", obj.currentDateDt);
                data.Add("crime_type", obj.crimeType);
                data.Add("gender", obj.gender);
                data.Add("report_extension", obj.reportExtension);
                data.Add("status", obj.status);
                data.Add("url", obj.url);
                data.Add("token", obj.token);
                data.Add("created_at", DateTime.Now);

                dbOperation.OpenDbConnection();
                isAdded = dbOperation.InsertData("report", data);

                dbOperation.CloseDbConnection();
                return isAdded;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.Message);
                throw x;
            }
        }

        public List<ReportResult> GetReports(string filter, int offset, int limit)
        {
            List<ReportResult> reports = new List<ReportResult>();
            try
            {
                dbOperation.OpenDbConnection();
                string wherePart = "";
                string sql = "";

                if (filter == "ALL")
                {
                    sql = "SELECT * FROM report ORDER BY id DESC LIMIT " + limit + " OFFSET " + offset + ";";
                }
                else if(filter == "PENDING")
                {
                    wherePart = "url is NULL";
                    sql = String.Format("SELECT * FROM report WHERE {0} ORDER BY id DESC;", wherePart);
                }
                else if (filter == "COMPLETED")
                {
                    wherePart = "url is NOT NULL";
                    sql = String.Format("SELECT * FROM report WHERE {0} ORDER BY id DESC;", wherePart);
                }

                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {                    
                    ReportResult obj = new ReportResult();
                    obj.id = dataRow["id"] != DBNull.Value ? Convert.ToInt32(dataRow["id"].ToString()) : 0;
                    obj.reportType = dataRow["report_type"] != DBNull.Value ? Convert.ToInt32(dataRow["report_type"].ToString()) : 0;
                    obj.unit = dataRow["unit"] != DBNull.Value ? Convert.ToInt32(dataRow["unit"].ToString()) : -1;
                    obj.subUnit = dataRow["sub_unit"] != DBNull.Value ? Convert.ToInt32(dataRow["sub_unit"].ToString()) : -1;
                    obj.ageRange = dataRow["age_range"] != DBNull.Value ? dataRow["age_range"].ToString() : "";
                    obj.arrestType = dataRow["arrest_type"] != DBNull.Value ? Convert.ToInt32(dataRow["arrest_type"].ToString()) : 0;
                    obj.creationDateFrom = dataRow["creation_date_from"] != DBNull.Value ? dataRow["creation_date_from"].ToString() : "";
                    obj.creationDateTo = dataRow["creation_date_to"] != DBNull.Value ? dataRow["creation_date_to"].ToString() : "";
                    obj.creationDateTo = dataRow["creation_date_to"] != DBNull.Value ? dataRow["creation_date_to"].ToString() : "";
                    obj.currentDate = dataRow["curr_date"] != DBNull.Value ? dataRow["curr_date"].ToString() : "";
                    obj.crimeType = dataRow["crime_type"] != DBNull.Value ? Convert.ToInt32(dataRow["crime_type"].ToString()) : 0;
                    obj.gender = dataRow["gender"] != DBNull.Value ? Convert.ToInt32(dataRow["gender"].ToString()) : -1;
                    obj.reportExtension = dataRow["report_extension"] != DBNull.Value ? dataRow["report_extension"].ToString() : "";
                    obj.status = dataRow["status"] != DBNull.Value ? dataRow["status"].ToString() : "";
                    obj.url = dataRow["url"] != DBNull.Value ? dataRow["url"].ToString() : "";
                    obj.token = dataRow["token"] != DBNull.Value ? dataRow["token"].ToString() : "";
                    obj.createdAt = dataRow["created_at"] != DBNull.Value ? dataRow["created_at"].ToString() : "";

                    reports.Add(obj);                    
                }
                dbOperation.CloseDbConnection();
                return reports;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.ToString());
                throw x;
            }
        }

        public bool UpdateReports(List<ReportResult> list)
        {
            try
            {
                dbOperation.OpenDbConnection();
                SQLiteTransaction transaction = dbOperation.GetSqliteConnection().BeginTransaction();
                foreach (var obj in list)
                {
                    string wherePart = String.Format(" token='{0}'", obj.token);

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("status", obj.status);
                    data.Add("url", obj.url);
                    dbOperation.Update("report", data, wherePart);
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

        public void DeleteReport(string token)
        {
            try
            {
                dbOperation.OpenDbConnection();
                string sql = String.Format("token= '{0}'", token);
                dbOperation.Delete("report", sql);
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
