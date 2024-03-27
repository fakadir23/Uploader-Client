using ISTL.COMMON.Common;
using ISTL.COMMON.Database;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.PERSOGlobals;
using ISTL.RAB.Entity;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.DbManager
{
    public class DbExistingSpecialProfileManager
    {
        public Logger logger = LogManager.GetCurrentClassLogger();
        private DbOperation dbOperation = null;
        public int RecordCount;
        public DbExistingSpecialProfileManager()
        {
            dbOperation = new DbOperation(Globals.Database.NAME, Globals.Database.PASSWORD);
        }

        public int GetSpecialEnrolledDraftOrErrorCount(string columnName)
        {
            int totalCount = 0;
            try
            {
                string wherePart = string.Empty;
                if (columnName == "status")
                {
                    wherePart = String.Format("status = {0}", Globals.RecordState.DRAFT);
                }
                else if (columnName == "error_status")
                {
                    wherePart = String.Format("error_status = {0}", Globals.ErrorState.ERROR);
                }
                string sql = String.Format("SELECT COUNT(hash) FROM special_criminal_profile WHERE {0};", wherePart);

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

        public List<SpecialEnrollmentDto> GetExistingSpecialRecords(int status, string columnName, string filterClause, int offset)
        {
            try
            {
                List<SpecialEnrollmentDto> list = new List<SpecialEnrollmentDto>();

                //string wherePart = String.Format(columnName+"='{0}' LIMIT 10", status);
                string wherePart = String.Format(columnName + "='{0}'", status);

                if (!string.IsNullOrEmpty(filterClause)) wherePart += " " + filterClause;
                string sqlCount = String.Format("SELECT count(*) FROM special_criminal_profile cp WHERE {0};", wherePart);

                wherePart += " ORDER BY datetime(created_at) desc LIMIT 10 OFFSET " + offset;

                string sql = String.Format("SELECT * FROM special_criminal_profile cp WHERE {0};", wherePart);
                dbOperation.OpenDbConnection();
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    SpecialEnrollmentDto obj = new SpecialEnrollmentDto();

                    obj.referenceNo = dataRow["reference_no"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["reference_no"]) : null;
                    obj.fullName = dataRow["full_name"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["full_name"]) : null;
                    obj.crimeType = dataRow["crime_type"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["crime_type"]) : null;
                    var arresteeType = dataRow["arrestee_type"].GetType() != typeof(DBNull) ? dataRow["arrestee_type"] : 0;
                    obj.arrestTypeIntValue = Convert.ToInt32(arresteeType);
                    if (obj.arrestTypeIntValue == 1) obj.arrestType = "MobileCourts";
                    else if (obj.arrestTypeIntValue == 2) obj.arrestType = "ContagiousPatients";
                    else if (obj.arrestTypeIntValue == 3) obj.arrestType = "directSubmitInPS";

                    obj.hash = dataRow["hash"].GetType() != typeof(DBNull) ? (string)dataRow["hash"] : null;
                    obj.id = dataRow["id"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["id"]) : 0;

                    var genderCode = dataRow["gender"].GetType() != typeof(DBNull) ? (byte)dataRow["gender"] : 10;
                    if (genderCode == 0)    obj.gender = "Male";
                    else if (genderCode == 1)   obj.gender = "Female";
                    else if (genderCode == 2) obj.gender = "Other";

                    var createdByUser = dataRow["created_by"].GetType() != typeof(DBNull) ? dataRow["created_by"] : -1;
                    obj.createdBy = Convert.ToInt32(createdByUser);

                    obj.errorMsgFromServer = dataRow["error_message"].GetType() != typeof(DBNull) ? (string)dataRow["error_message"] : null;

                    list.Add(obj);
                }

                RecordCount = dbOperation.ExecuteScalarReturnInt(sqlCount);

                dbOperation.CloseDbConnection();
                return list;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Got error when getting record.\n" + x.ToString());
                throw x;
            }
        }

        public List<SpecialEnrollmentDto> GetSpecialUploadPendingRecords(int status, string columnName, string filterClause, int offset)
        {
            try
            {
                List<SpecialEnrollmentDto> list = new List<SpecialEnrollmentDto>();

                //string wherePart = String.Format(columnName+"='{0}' LIMIT 10", status);
                string wherePart = String.Format(columnName + "='{0}'", status);
                wherePart += " AND error_status='" + Globals.ErrorState.NOT_ERROR + "' ";
                if (!string.IsNullOrEmpty(filterClause)) wherePart += " " + filterClause;
                string sqlCount = String.Format("SELECT count(*) FROM special_criminal_profile cp WHERE {0};", wherePart);

                wherePart += " ORDER BY datetime(created_at) desc LIMIT 10 OFFSET " + offset;

                string sql = String.Format("SELECT * FROM special_criminal_profile cp WHERE {0};", wherePart);
                dbOperation.OpenDbConnection();
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    SpecialEnrollmentDto obj = new SpecialEnrollmentDto();

                    obj.referenceNo = dataRow["reference_no"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["reference_no"]) : null;
                    obj.fullName = dataRow["full_name"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["full_name"]) : null;
                    obj.crimeType = dataRow["crime_type"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["crime_type"]) : null;
                    var arresteeType = dataRow["arrestee_type"].GetType() != typeof(DBNull) ? dataRow["arrestee_type"] : 0;
                    obj.arrestTypeIntValue = Convert.ToInt32(arresteeType);
                    if (obj.arrestTypeIntValue == 1) obj.arrestType = "MobileCourts";
                    else if (obj.arrestTypeIntValue == 2) obj.arrestType = "ContagiousPatients";
                    else if (obj.arrestTypeIntValue == 3) obj.arrestType = "directSubmitInPS";

                    obj.hash = dataRow["hash"].GetType() != typeof(DBNull) ? (string)dataRow["hash"] : null;
                    obj.id = dataRow["id"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["id"]) : 0;

                    var genderCode = dataRow["gender"].GetType() != typeof(DBNull) ? (byte)dataRow["gender"] : 10;
                    if (genderCode == 0) obj.gender = "Male";
                    else if (genderCode == 1) obj.gender = "Female";
                    else if (genderCode == 2) obj.gender = "Other";

                    var createdByUser = dataRow["created_by"].GetType() != typeof(DBNull) ? dataRow["created_by"] : -1;
                    obj.createdBy = Convert.ToInt32(createdByUser);

                    list.Add(obj);
                }

                RecordCount = dbOperation.ExecuteScalarReturnInt(sqlCount);

                dbOperation.CloseDbConnection();
                return list;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Got error when getting record.\n" + x.ToString());
                throw x;
            }
        }

        public bool DeleteDraftData(string hash)
        {
            try
            {
                string wherePart = String.Format("hash = '{0}'", hash);
                dbOperation.OpenDbConnection();
                bool isDeleted = dbOperation.Delete("special_criminal_profile", wherePart);
                dbOperation.CloseDbConnection();
                return isDeleted;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.ToString());
                throw x;
            }
        }
    }
}
