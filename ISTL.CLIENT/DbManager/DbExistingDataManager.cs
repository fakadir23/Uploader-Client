using ISTL.COMMON.Common;
using ISTL.COMMON.Database;
using ISTL.COMMON.Subscription;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.MODELS.DTO.New.Enrollment.CrimeInformation;
using ISTL.MODELS.DTO.New.Enrollment.Other;
using ISTL.PERSOGlobals;
using ISTL.RAB.Entity;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ISTL.RAB.DbManager
{
    public class DbExistingDataManager
    {
        public Logger logger = LogManager.GetCurrentClassLogger();
        private DbOperation dbOperation = null;
        public int RecordCount;

        public DbExistingDataManager()
        {
            dbOperation = new DbOperation(Globals.Database.NAME, Globals.Database.PASSWORD);
        }

        public List<EnrollmentDto> GetExistingRecords(int status, string columnName, string filterClause, int offset)
        {
            try
            {
                List<EnrollmentDto> list = new List<EnrollmentDto>();

                //string wherePart = String.Format(columnName+"='{0}' LIMIT 10", status);
                string wherePart = String.Format(columnName + "='{0}'", status);

                if (!string.IsNullOrEmpty(filterClause)) wherePart += " " + filterClause;
                string sqlCount = String.Format("SELECT count(*) FROM criminal_profile cp WHERE {0};", wherePart);

                if (!string.IsNullOrEmpty(filterClause))
                {
                    if (filterClause.Contains("presentaddress"))
                    {
                        sqlCount = String.Format("SELECT count(*) FROM criminal_profile cp INNER JOIN address presentaddress on " +
                            "cp.present_address_id=presentaddress.id WHERE {0};", wherePart);
                    }
                    else if (filterClause.Contains("permanentAddress"))
                    {
                        sqlCount = String.Format("SELECT count(*) FROM criminal_profile cp INNER JOIN address permanentAddress on " +
                            "cp.permanent_address_id=permanentAddress.id WHERE {0};", wherePart);
                    }
                }

                wherePart += " ORDER BY datetime(cp.created_at) desc LIMIT 10 OFFSET " + offset;

                string sql = String.Format("SELECT * FROM criminal_profile cp WHERE {0};", wherePart);

                if (wherePart.Contains("presentaddress"))
                {
                    sql = String.Format("SELECT * FROM criminal_profile cp INNER JOIN address presentaddress on " +
                        "cp.present_address_id=presentaddress.id WHERE {0};", wherePart);
                }
                if (wherePart.Contains("permanentAddress"))
                {
                    sql = String.Format("SELECT * FROM criminal_profile cp INNER JOIN address permanentAddress on " +
                        "cp.permanent_address_id=permanentAddress.id WHERE {0};", wherePart);
                }
                dbOperation.OpenDbConnection();
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    EnrollmentDto obj = new EnrollmentDto();
                    obj.profile = new ProfileDto();

                    obj.profile.referenceNo = dataRow["reference_no"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["reference_no"]) : null;
                    obj.profile.fullName = dataRow["full_name"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["full_name"]) : null;

                    var genderCode = dataRow["gender"].GetType() != typeof(DBNull) ? (byte)dataRow["gender"] : 10;
                    if (genderCode == 0)
                    {
                        obj.profile.gender = "Male";
                    }
                    else if (genderCode == 1)
                    {
                        obj.profile.gender = "Female";
                    }
                    else if (genderCode == 2) obj.profile.gender = "Other";

                    if (dataRow["date_of_birth"].GetType() != typeof(DBNull))
                    {
                        DateTime dob = (DateTime)dataRow["date_of_birth"];
                        obj.profile.dateOfBirth = dob.ToString("dd-MM-yyyy");
                    }

                    var religion = dataRow["religion"].GetType() != typeof(DBNull) ? (byte)dataRow["religion"] : 10;
                    if (religion == 0) obj.profile.religion = "muslim";
                    else if (religion == 1) obj.profile.religion = "hindu";
                    else if (religion == 2) obj.profile.religion = "christian";
                    else if (religion == 3) obj.profile.religion = "buddhist";

                    obj.profile.hash = dataRow["hash"].GetType() != typeof(DBNull) ? (string)dataRow["hash"] : null;
                    //obj.profile.id = dataRow["id"].GetType() != typeof(DBNull) ? Convert.ToString(dataRow["id"]) : null;

                    var createdByUser = dataRow["created_by"].GetType() != typeof(DBNull) ? dataRow["created_by"] : -1;
                    obj.profile.createdBy = Convert.ToInt32(createdByUser);

                    obj.profile.errorMsgFromServer = dataRow["error_message"].GetType() != typeof(DBNull) ? (string)dataRow["error_message"] : null;

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

        public int GetNormalDraftOrErrorCount(string columnName, int status)
        {
            int totalCount = 0;
            try
            {
                string wherePart = string.Empty;
                if (columnName == "status")
                {
                    wherePart = String.Format("status = {0}", status);
                }
                else if (columnName == "error_status")
                {
                    wherePart = String.Format("error_status = {0}", status);
                }
                string sql = String.Format("SELECT COUNT(hash) FROM criminal_profile WHERE {0};", wherePart);

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

        public bool DeleteDraftData(string hash)
        {
            try
            {
                string refNoRec = string.Empty;
                int present_addressId = -1;
                int permanent_addressId = -1;
                string wherePart = String.Format("hash='{0}'", hash);
                string sql = String.Format("SELECT reference_no, present_address_id, permanent_address_id FROM criminal_profile WHERE {0} limit 1;", wherePart);
                dbOperation.OpenDbConnection();
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    refNoRec = dataRow["reference_no"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["reference_no"]) : null;
                    present_addressId = dataRow["present_address_id"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["present_address_id"]) : -1;
                    permanent_addressId = dataRow["permanent_address_id"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["permanent_address_id"]) : -1;
                }               
                dbOperation.CloseDbConnection();

                if (!string.IsNullOrEmpty(refNoRec))
                {
                    string deleteFromWhere = String.Format("reference_no = '{0}'", refNoRec);
                    dbOperation.OpenDbConnection();
                    dbOperation.Delete("crime_information", deleteFromWhere);
                    dbOperation.Delete("attachment", deleteFromWhere);
                    dbOperation.CloseDbConnection();
                }
                if (present_addressId > 0)
                {
                    string deleteFromWhere = String.Format("id = '{0}'", present_addressId);
                    dbOperation.OpenDbConnection();
                    dbOperation.Delete("address", deleteFromWhere);
                    dbOperation.CloseDbConnection();
                }
                if (permanent_addressId > 0)
                {
                    string deleteFromWhere = String.Format("id = '{0}'", permanent_addressId);
                    dbOperation.OpenDbConnection();
                    dbOperation.Delete("address", deleteFromWhere);
                    dbOperation.CloseDbConnection();
                }

                dbOperation.OpenDbConnection();
                bool isDeleted = dbOperation.Delete("criminal_profile", wherePart);
                dbOperation.CloseDbConnection();

                CounterDraftSubject counterDraftStatus = (CounterDraftSubject)SubjectFactory.GetInstance().GetSubject(Globals.Common.COUNTER_DRAFT_NAME);
                counterDraftStatus.Count = this.GetNormalDraftCount();
                counterDraftStatus.Notify();

                CounterSubject counterStatus = (CounterSubject)SubjectFactory.GetInstance().GetSubject(Globals.Common.COUNTER_NAME);
                counterStatus.Count = this.GetTodayEnrollCount();
                counterStatus.Notify();

                return isDeleted;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.ToString());
                throw x;
            }
        }

        public int GetTodayEnrollCount()
        {
            int totalCount = 0;
            try
            {
                string wherePart = String.Format("strftime('%Y-%m-%d', created_at)='{0}'",
                    DateTime.Today.ToString("yyyy-MM-dd"));
                string sql = String.Format("SELECT COUNT(hash) FROM criminal_profile WHERE {0};", wherePart);

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

        public int GetNormalDraftCount()
        {
            int totalCount = 0;
            try
            {
                //string wherePart = String.Format("strftime('%Y-%m-%d', created_at)<='{0}' and created_by='{1}' and status = {2}",
                //    DateTime.Today.ToString("yyyy-MM-dd"), Users.Id, Globals.RecordState.DRAFT);
                string wherePart = String.Format("status = {0}",Globals.RecordState.DRAFT);
                string sql = String.Format("SELECT COUNT(hash) FROM criminal_profile WHERE {0};", wherePart);

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

        public List<EnrollmentDto> GetUploadPendingRecords(int status, string columnName, string filterClause, int offset)
        {
            try
            {
                List<EnrollmentDto> list = new List<EnrollmentDto>();

                //string wherePart = String.Format(columnName+"='{0}' LIMIT 10", status);
                string wherePart = String.Format(columnName + "='{0}'", status);
                wherePart += " AND error_status='" + Globals.ErrorState.NOT_ERROR + "' ";
                if (!string.IsNullOrEmpty(filterClause)) wherePart += " " + filterClause;
                string sqlCount = String.Format("SELECT count(*) FROM criminal_profile cp WHERE {0};", wherePart);

                if (!string.IsNullOrEmpty(filterClause))
                {
                    if (filterClause.Contains("presentaddress"))
                    {
                        sqlCount = String.Format("SELECT count(*) FROM criminal_profile cp INNER JOIN address presentaddress on " +
                            "cp.present_address_id=presentaddress.id WHERE {0};", wherePart);
                    }
                    else if (filterClause.Contains("permanentAddress"))
                    {
                        sqlCount = String.Format("SELECT count(*) FROM criminal_profile cp INNER JOIN address permanentAddress on " +
                            "cp.permanent_address_id=permanentAddress.id WHERE {0};", wherePart);
                    }
                }

                wherePart += " ORDER BY datetime(cp.created_at) desc LIMIT 10 OFFSET " + offset;

                string sql = String.Format("SELECT * FROM criminal_profile cp WHERE {0};", wherePart);

                if (wherePart.Contains("presentaddress"))
                {
                    sql = String.Format("SELECT *FROM criminal_profile cp INNER JOIN address presentaddress on " +
                        "cp.present_address_id=presentaddress.id WHERE {0};", wherePart);
                }
                if (wherePart.Contains("permanentAddress"))
                {
                    sql = String.Format("SELECT *FROM criminal_profile cp INNER JOIN address permanentAddress on " +
                        "cp.permanent_address_id=permanentAddress.id WHERE {0};", wherePart);
                }
                dbOperation.OpenDbConnection();
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    EnrollmentDto obj = new EnrollmentDto();
                    obj.profile = new ProfileDto();

                    obj.profile.referenceNo = dataRow["reference_no"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["reference_no"]) : null;
                    obj.profile.fullName = dataRow["full_name"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["full_name"]) : null;

                    var genderCode = dataRow["gender"].GetType() != typeof(DBNull) ? (byte)dataRow["gender"] : 10;
                    if (genderCode == 0)
                    {
                        obj.profile.gender = "Male";
                    }
                    else if (genderCode == 1)
                    {
                        obj.profile.gender = "Female";
                    }
                    else if (genderCode == 2) obj.profile.gender = "Other";

                    if (dataRow["date_of_birth"].GetType() != typeof(DBNull))
                    {
                        DateTime dob = (DateTime)dataRow["date_of_birth"];
                        obj.profile.dateOfBirth = dob.ToString("dd-MM-yyyy");
                    }

                    var religion = dataRow["religion"].GetType() != typeof(DBNull) ? (byte)dataRow["religion"] : 10;
                    if (religion == 0) obj.profile.religion = "muslim";
                    else if (religion == 1) obj.profile.religion = "hindu";
                    else if (religion == 2) obj.profile.religion = "christian";
                    else if (religion == 3) obj.profile.religion = "buddhist";

                    obj.profile.hash = dataRow["hash"].GetType() != typeof(DBNull) ? (string)dataRow["hash"] : null;
                    //obj.profile.id = dataRow["id"].GetType() != typeof(DBNull) ? Convert.ToString(dataRow["id"]) : null;

                    var maritalStatus = dataRow["marital_status"].GetType() != typeof(DBNull) ? (byte)dataRow["marital_status"] : 10;
                    if (maritalStatus == 0) obj.profile.maritalStatus = "Single";
                    else if (maritalStatus == 1) obj.profile.maritalStatus = "Married";
                    else if (maritalStatus == 2) obj.profile.maritalStatus = "Widowed";
                    else if (maritalStatus == 3) obj.profile.maritalStatus = "Divorced";

                    var createdByUser = dataRow["created_by"].GetType() != typeof(DBNull) ? dataRow["created_by"] : -1;
                    obj.profile.createdBy = Convert.ToInt32(createdByUser);

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

        public int GetNormalUploadPendingCount(string columnName, int status)
        {
            int totalCount = 0;
            try
            {
                string wherePart = string.Empty;
                if (columnName == "status")
                {
                    wherePart = String.Format("status = {0}", status);
                }
                wherePart += String.Format(" and error_status = {0}", Globals.ErrorState.NOT_ERROR);
                string sql = String.Format("SELECT COUNT(hash) FROM criminal_profile WHERE {0};", wherePart);

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

        public List<EnrollmentDto> GetNormalEnrolledTodayRecords(string filterClause, int offset)
        {
            try
            {
                List<EnrollmentDto> list = new List<EnrollmentDto>();

                string wherePart = String.Format("strftime('%Y-%m-%d', cp.created_at)='{0}'", DateTime.Today.ToString("yyyy-MM-dd"));
                if (!string.IsNullOrEmpty(filterClause)) wherePart += " " + filterClause;
                string sqlCount = String.Format("SELECT count(*) FROM criminal_profile cp WHERE {0};", wherePart);

                if (!string.IsNullOrEmpty(filterClause))
                {
                    if (filterClause.Contains("presentaddress"))
                    {
                        sqlCount = String.Format("SELECT count(*) FROM criminal_profile cp INNER JOIN address presentaddress on " +
                            "cp.present_address_id=presentaddress.id WHERE {0};", wherePart);
                    }
                    else if (filterClause.Contains("permanentAddress"))
                    {
                        sqlCount = String.Format("SELECT count(*) FROM criminal_profile cp INNER JOIN address permanentAddress on " +
                            "cp.permanent_address_id=permanentAddress.id WHERE {0};", wherePart);
                    }
                }

                wherePart += " ORDER BY datetime(cp.created_at) desc LIMIT 10 OFFSET " + offset;

                string sql = String.Format("SELECT * FROM criminal_profile cp WHERE {0};", wherePart);

                if (wherePart.Contains("presentaddress"))
                {
                    sql = String.Format("SELECT *FROM criminal_profile cp INNER JOIN address presentaddress on " +
                        "cp.present_address_id=presentaddress.id WHERE {0};", wherePart);
                }
                if (wherePart.Contains("permanentAddress"))
                {
                    sql = String.Format("SELECT *FROM criminal_profile cp INNER JOIN address permanentAddress on " +
                        "cp.permanent_address_id=permanentAddress.id WHERE {0};", wherePart);
                }
                dbOperation.OpenDbConnection();
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    EnrollmentDto obj = new EnrollmentDto();
                    obj.profile = new ProfileDto();

                    obj.profile.referenceNo = dataRow["reference_no"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["reference_no"]) : null;
                    obj.profile.fullName = dataRow["full_name"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["full_name"]) : null;

                    var genderCode = dataRow["gender"].GetType() != typeof(DBNull) ? (byte)dataRow["gender"] : 10;
                    if (genderCode == 0)
                    {
                        obj.profile.gender = "Male";
                    }
                    else if (genderCode == 1)
                    {
                        obj.profile.gender = "Female";
                    }
                    else if (genderCode == 2) obj.profile.gender = "Other";

                    if (dataRow["date_of_birth"].GetType() != typeof(DBNull))
                    {
                        DateTime dob = (DateTime)dataRow["date_of_birth"];
                        obj.profile.dateOfBirth = dob.ToString("dd-MM-yyyy");
                    }

                    obj.profile.hash = dataRow["hash"].GetType() != typeof(DBNull) ? (string)dataRow["hash"] : null;
                    //obj.profile.id = dataRow["id"].GetType() != typeof(DBNull) ? Convert.ToString(dataRow["id"]) : null;

                    var createdByUser = dataRow["created_by"].GetType() != typeof(DBNull) ? dataRow["created_by"] : -1;
                    obj.profile.createdBy = Convert.ToInt32(createdByUser);

                    obj.profile.status = dataRow["status"].GetType() != typeof(DBNull) ? Convert.ToInt32((byte)dataRow["status"]) : -1;

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

        public int GetNormalEnrolledTodayCount()
        {
            int totalCount = 0;
            try
            {
                //string wherePart = String.Format("strftime('%Y-%m-%d', created_at)='{0}' and created_by='{1}'",
                //    DateTime.Today.ToString("yyyy-MM-dd"), Users.Id);
                string wherePart = String.Format("strftime('%Y-%m-%d', created_at)='{0}'", DateTime.Today.ToString("yyyy-MM-dd"));
                string sql = String.Format("SELECT COUNT(hash) FROM criminal_profile WHERE {0};", wherePart);

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
    }
}
