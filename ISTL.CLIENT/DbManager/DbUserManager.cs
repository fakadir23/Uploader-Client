using ISTL.COMMON;
using ISTL.COMMON.Common;
using ISTL.COMMON.Database;
using ISTL.MODELS.DTO.Auth;
using ISTL.MODELS.Response;
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
    public class DbUserManager
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private DbOperation dbOperation = null;

        public DbUserManager()
        {
            dbOperation = new DbOperation(Globals.Database.NAME, Globals.Database.PASSWORD);
        }

        public bool AddUserInfo(LoginResponse userInfo)
        {
            bool isAdded = false;
            try
            {
                //string userNameCapitalized = userInfo.username?.ToUpper();
                //DeleteUser(userNameCapitalized);
                
                //DeleteUser(userInfo.username);

                //string userHashString = userInfo.username + userInfo.password + userInfo.nameEn;
                //string userHash = GenerateSecureHash.CreateSha1Hash(Utils.StringToByteArray(userHashString));

                //Dictionary<string, object> data = new Dictionary<string, object>();

                //data.Add("id", userInfo.id);
                //data.Add("username", AesCryptography.EncryptToString(userNameCapitalized));
                //data.Add("username", AesCryptography.EncryptToString(userInfo.username));
                //data.Add("password", AesCryptography.EncryptToString(userInfo.password));
                //data.Add("email", userInfo.email);
                //data.Add("phone", userInfo.phone);
                //data.Add("name_en", userInfo.nameEn);
                //data.Add("name_bn", userInfo.nameBn);
                //data.Add("user_type", userInfo.type);
                //data.Add("battalion", userInfo.unit);
                //data.Add("sub_unit", userInfo.subunit);
                //data.Add("role_id", userInfo.roleId);
                //data.Add("user_hash", userHash);
                //data.Add("user_activated", userInfo.userActivated);
                //data.Add("status", userInfo.status);

                dbOperation.OpenDbConnection();
                //isAdded = dbOperation.InsertData("user_info", data);
                
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

        public LoginResponse GetUserInfo(string username)
        {
            LoginResponse info = new LoginResponse();
            try
            {
                dbOperation.OpenDbConnection();
                string wherePart = String.Format("username='{0}'", AesCryptography.EncryptToString(username));
                string sql = String.Format("SELECT * FROM user_info WHERE {0} LIMIT 1;", wherePart);
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    info.id = dataRow["id"] != DBNull.Value ? Convert.ToInt32(dataRow["id"].ToString()) : 0;
                    info.userName = dataRow["username"] != DBNull.Value ? dataRow["username"].ToString() : "";
                    //info.status = dataRow["status"] != DBNull.Value ? Convert.ToInt32(dataRow["status"].ToString()) != 0 : false;
                }
                dbOperation.CloseDbConnection();
                return info;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + username + ". Got error when getting record.\n" + x.ToString());
                throw x;
            }
        }

        public bool AddConfiguration(string deleteDayCount, string maxUploadPending, 
            string notifyDayCount, string verifyDayCount, string reportDeleteCount, string nidDeleteCount)
        {
            bool isAdded = false;
            try
            {
                DeleteConfiguration();
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("delete_day_count", AesCryptography.EncryptToString(deleteDayCount));
                data.Add("max_upload_pending", AesCryptography.EncryptToString(maxUploadPending));
                data.Add("notify_day_count", AesCryptography.EncryptToString(notifyDayCount));
                data.Add("verify_day_count", AesCryptography.EncryptToString(verifyDayCount));
                data.Add("report_delete_day_count", AesCryptography.EncryptToString(reportDeleteCount));
                data.Add("nid_delete_day_count", AesCryptography.EncryptToString(nidDeleteCount));

                dbOperation.OpenDbConnection();
                // For Successful Logged in Delete Existing old user info
                // Then Add User Info from web api.
                isAdded = dbOperation.InsertData("configuration", data);

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

        public bool CheckUser(string username, string password)
        {
            bool isUserExists = false;
            try
            {
                dbOperation.OpenDbConnection();
                string sql = String.Format("SELECT * FROM user_info WHERE username= '{0}' And PASSWORD= '{1}'", username, password);
                isUserExists = dbOperation.ExecuteScalar(sql);
                dbOperation.CloseDbConnection();
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.Message);
                throw x;
            }

            return isUserExists;
        }

        public bool CheckUserOnly(string username)
        {
            bool isUserExists = false;
            try
            {
                dbOperation.OpenDbConnection();
                string sql = String.Format("SELECT * FROM web_users WHERE username= '{0}'", username);
                isUserExists = dbOperation.ExecuteScalar(sql);
                dbOperation.CloseDbConnection();
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.Message);
                throw x;
            }

            return isUserExists;
        }

        public void DeleteUser(string username)
        {
            try
            {
                dbOperation.OpenDbConnection();
                string sql = String.Format("username= '{0}'", AesCryptography.EncryptToString(username));
                dbOperation.Delete("user_info", sql);
                dbOperation.CloseDbConnection();
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.Message);
            }
        }

        public void DeleteRegions(string username)
        {
            try
            {
                dbOperation.OpenDbConnection();
                string sql = String.Format("username= '{0}'", AesCryptography.EncryptToString(username));
                dbOperation.Delete("regions", sql);
                dbOperation.CloseDbConnection();
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.Message);
            }
        }

        public void DeleteConfiguration()
        {
            try
            {
                dbOperation.OpenDbConnection();
                dbOperation.DeleteAll("configuration");
                dbOperation.CloseDbConnection();
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.Message);
            }
        }

        public string GetUserFullNameByUserId(int UserId)
        {
            string fullName = "";
            try
            {
                dbOperation.OpenDbConnection();
                string wherePart = String.Format("id='{0}'", UserId);
                string sql = String.Format("SELECT * FROM user_info WHERE {0} LIMIT 1;", wherePart);
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    string fN = dataRow["name_en"].ToString();
                    //fullName = AesCryptography.DecryptToString(fN);
                    fullName = fN;
                }
                dbOperation.CloseDbConnection();
                return fullName;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + UserId + ". Got error when getting record.\n" + x.ToString());
                throw x;
            }
        }

        public string GetUserFullName(string username)
        {
            string fullName = "";
            try
            {
                dbOperation.OpenDbConnection();
                string wherePart = String.Format("username='{0}'", AesCryptography.EncryptToString(username?.ToUpper()));
                string sql = String.Format("SELECT * FROM user_info WHERE {0} LIMIT 1;", wherePart);
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    string fN = dataRow["name_en"].ToString();
                    //fullName = AesCryptography.DecryptToString(fN);
                    fullName = fN;
                }
                dbOperation.CloseDbConnection();
                return fullName;

            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + username + ". Got error when getting record.\n" + x.ToString());
                throw x;
            }
        }

        public int GetUserId(string username)
        {
            int id = 0;
            try
            {
                dbOperation.OpenDbConnection();
                string wherePart = String.Format("username='{0}'", AesCryptography.EncryptToString(username?.ToUpper()));
                string sql = String.Format("SELECT id FROM user_info WHERE {0} LIMIT 1;", wherePart);
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    id = dataRow["id"] != DBNull.Value
                               ? Convert.ToInt32(dataRow["id"].ToString()) : 0;
                }
                dbOperation.CloseDbConnection();
                return id;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + username + ". Got error when getting record.\n" + x.ToString());
                throw x;
            }
        }

        public int GetOfflineUserUnit(string username)
        {
            int unit = 0;
            try
            {
                dbOperation.OpenDbConnection();
                string wherePart = String.Format("username='{0}'", AesCryptography.EncryptToString(username?.ToUpper()));
                string sql = String.Format("SELECT battalion FROM user_info WHERE {0} LIMIT 1;", wherePart);
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    unit = dataRow["battalion"] != DBNull.Value ? Convert.ToInt32(dataRow["battalion"].ToString()) : -1;
                }
                dbOperation.CloseDbConnection();
                return unit;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + username + ". Got error when getting record.\n" + x.ToString());
                throw x;
            }
        }

        public int GetOfflineUserSubUnit(string username)
        {
            int sub_unit = 0;
            try
            {
                dbOperation.OpenDbConnection();
                string wherePart = String.Format("username='{0}'", AesCryptography.EncryptToString(username?.ToUpper()));
                string sql = String.Format("SELECT sub_unit FROM user_info WHERE {0} LIMIT 1;", wherePart);
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    sub_unit = dataRow["sub_unit"] != DBNull.Value ? Convert.ToInt32(dataRow["sub_unit"].ToString()) : 0;
                }
                dbOperation.CloseDbConnection();
                return sub_unit;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + username + ". Got error when getting record.\n" + x.ToString());
                throw x;
            }
        }

        public string GetUserPassword(string username)
        {
            string pass = "";
            try
            {
                dbOperation.OpenDbConnection();
                string wherePart = String.Format("username='{0}'", AesCryptography.EncryptToString(username));
                string sql = String.Format("SELECT password FROM user_info WHERE {0} LIMIT 1;", wherePart);
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    pass = AesCryptography.DecryptToString(dataRow["password"].ToString());
                }
                dbOperation.CloseDbConnection();
                return pass;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + username + ". Got error when getting password.\n" + x.ToString());
                throw x;
            }
        }

        public void GetRegions(string username)
        {
            try
            {
                dbOperation.OpenDbConnection();
                string wherePart = String.Format("username='{0}'", AesCryptography.EncryptToString(username));
                string sql = String.Format("SELECT * FROM regions WHERE {0};", wherePart);
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    string regionCode = AesCryptography.DecryptToString(dataRow["region_code"].ToString());
                    string regionName = AesCryptography.DecryptToString(dataRow["region_name"].ToString());
                    //Users.AddRegions(regionCode, regionName);
                }
                dbOperation.CloseDbConnection();
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + username + ". Got error when getting record.\n" + x.ToString());
                throw x;
            }
        }

        public void GetConfiguration()
        {
            try
            {
                dbOperation.OpenDbConnection();
                string sql = "SELECT * FROM configuration LIMIT 1;";
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    Configuration.DeleteDayCount = Convert.ToInt32(AesCryptography.DecryptToString(dataRow["delete_day_count"].ToString()));
                    Configuration.MaxUploadPending = Convert.ToInt32(AesCryptography.DecryptToString(dataRow["max_upload_pending"].ToString()));
                    Configuration.NotifyDayCount = Convert.ToInt32(AesCryptography.DecryptToString(dataRow["notify_day_count"].ToString()));
                    Configuration.ReportDeleteDayCount = Convert.ToInt32(AesCryptography.DecryptToString(dataRow["report_delete_day_count"].ToString()));
                    Configuration.NidDeleteDayCount = Convert.ToInt32(AesCryptography.DecryptToString(dataRow["nid_delete_day_count"].ToString()));
                }
                dbOperation.CloseDbConnection();
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Got error when getting record for configuration.\n" + x.ToString());
                throw x;
            }
        }

        public bool DeletePreviousReport(int days)
        {
            try
            {
                TimeZone zone = TimeZone.CurrentTimeZone;
                DateTime currentDate = zone.ToLocalTime(DateTime.Now);
                DateTime previousDate = currentDate.AddDays(-days);

                dbOperation.OpenDbConnection();
                string wherePart = String.Format("strftime('%Y-%m-%d', created_at)<='{0}'", previousDate.ToString("yyyy-MM-dd"));
                dbOperation.Delete("report", wherePart);
                dbOperation.CloseDbConnection();
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Error while deleting previous reports dated on or before : " + x.ToString());
                throw x;
            }
            return true;
        }

        public bool DeletePreviousNidSearchData(int days)
        {
            try
            {
                TimeZone zone = TimeZone.CurrentTimeZone;
                DateTime currentDate = zone.ToLocalTime(DateTime.Now);
                DateTime previousDate = currentDate.AddDays(-days);

                dbOperation.OpenDbConnection();
                string wherePart = String.Format("strftime('%Y-%m-%d', created_at)<='{0}'", previousDate.ToString("yyyy-MM-dd"));
                dbOperation.Delete("bec_identification", wherePart);
                dbOperation.CloseDbConnection();
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Error while deleting previous nid search data. Error : " + x.ToString());
                throw x;
            }
            return true;
        }

        public bool DeletePreviousNotEntryData(int days)
        {
            try
            {
                TimeZone zone = TimeZone.CurrentTimeZone;
                DateTime currentDate = zone.ToLocalTime(DateTime.Now);
                DateTime previousDate = currentDate.AddDays(-days);

                dbOperation.OpenDbConnection();
                string wherePart = String.Format("strftime('%Y-%m-%d', uploaded_at)<='{0}' and status='{1}'", previousDate.ToString("yyyy-MM-dd"), Globals.RecordState.UPLOADED);
                dbOperation.Delete("not_entry", wherePart);
                dbOperation.CloseDbConnection();
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Error while deleting previous not_entry data dated on or before : " + x.ToString());
                throw x;
            }
            return true;
        }

        public bool DeletePreviousData(int days)
        {
            try
            {
                TimeZone zone = TimeZone.CurrentTimeZone;
                DateTime currentDate = zone.ToLocalTime(DateTime.Now);
                DateTime previousDate = currentDate.AddDays(-days);

                dbOperation.OpenDbConnection();
                string wherePart = String.Format("strftime('%Y-%m-%d', uploaded_date)<='{0}' and status='{1}'", previousDate.ToString("yyyy-MM-dd"), Globals.RecordState.VERIFIED);
                dbOperation.Delete("criminal_profile", wherePart);
                dbOperation.CloseDbConnection();
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Error while deleting previous data dated on or before : " + x.ToString());
                throw x;
            }
            return true;
        }

        public bool DeleteSpecialPreviousData(int days)
        {
            try
            {
                TimeZone zone = TimeZone.CurrentTimeZone;
                DateTime currentDate = zone.ToLocalTime(DateTime.Now);
                DateTime previousDate = currentDate.AddDays(-days);

                dbOperation.OpenDbConnection();
                string wherePart = String.Format("strftime('%Y-%m-%d', uploaded_date)<='{0}' and status='{1}'", previousDate.ToString("yyyy-MM-dd"), Globals.RecordState.VERIFIED);
                dbOperation.Delete("special_criminal_profile", wherePart);
                dbOperation.CloseDbConnection();
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Error while deleting previous data dated on or before : " + x.ToString());
                throw x;
            }
            return true;
        }

        public List<string> GetOldRefNoList(int days)
        {
            List<string> refNoList = new List<string>();
            try
            {
                TimeZone zone = TimeZone.CurrentTimeZone;
                DateTime currentDate = zone.ToLocalTime(DateTime.Now);
                DateTime previousDate = currentDate.AddDays(-days);

                string wherePart = String.Format("strftime('%Y-%m-%d', uploaded_date)<='{0}' and status='{1}'", previousDate.ToString("yyyy-MM-dd"), Globals.RecordState.VERIFIED);
                string sql = String.Format("SELECT reference_no FROM criminal_profile WHERE {0};", wherePart);
                dbOperation.OpenDbConnection();
                DataTable dataTable = dbOperation.GetDataTable(sql);
                dbOperation.CloseDbConnection();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    refNoList.Add(dataRow["reference_no"].ToString());
                }
                return refNoList;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Error while getting previous data dated on or before : " + x.ToString());
                throw x;
            }
        }

        public List<string> GetOldAddressIdList(int days)
        {
            List<string> addressIdList = new List<string>();
            try
            {
                TimeZone zone = TimeZone.CurrentTimeZone;
                DateTime currentDate = zone.ToLocalTime(DateTime.Now);
                DateTime previousDate = currentDate.AddDays(-days);

                string wherePart = String.Format("strftime('%Y-%m-%d', uploaded_date)<='{0}' and status='{1}'", previousDate.ToString("yyyy-MM-dd"), Globals.RecordState.VERIFIED);
                string sql = String.Format("SELECT present_address_id, permanent_address_id FROM criminal_profile WHERE {0};", wherePart);
                dbOperation.OpenDbConnection();
                DataTable dataTable = dbOperation.GetDataTable(sql);
                dbOperation.CloseDbConnection();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    addressIdList.Add(dataRow["present_address_id"].ToString());
                    addressIdList.Add(dataRow["permanent_address_id"].ToString());
                }
                return addressIdList;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Error while getting previous data dated on or before : " + x.ToString());
                throw x;
            }
        }

        public List<string> GetOldNotEntryRefNoList(int days)
        {
            List<string> refNoList = new List<string>();
            try
            {
                TimeZone zone = TimeZone.CurrentTimeZone;
                DateTime currentDate = zone.ToLocalTime(DateTime.Now);
                DateTime previousDate = currentDate.AddDays(-days);

                string wherePart = String.Format("strftime('%Y-%m-%d', uploaded_at)<='{0}' and status='{1}'", previousDate.ToString("yyyy-MM-dd"), Globals.RecordState.UPLOADED);
                string sql = String.Format("SELECT reference_no FROM not_entry WHERE {0};", wherePart);
                dbOperation.OpenDbConnection();
                DataTable dataTable = dbOperation.GetDataTable(sql);
                dbOperation.CloseDbConnection();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    refNoList.Add(dataRow["reference_no"].ToString());
                }
                return refNoList;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Error while getting previous not_entry data dated on or before : " + x.ToString());
                throw x;
            }
        }

        public bool DeletePreviousAddressData(int days)
        {
            try
            {
                //List<string> oldAddressIdList = GetOldAddressIdList(days);
                //if (oldAddressIdList != null)
                //{
                //    if (oldAddressIdList.Count == 0) return true;

                //    dbOperation.OpenDbConnection();
                //    for (int i = 0; i < oldAddressIdList.Count; i++)
                //    {
                //        string wherePart = String.Format(" id='{0}'", oldAddressIdList[i]);
                //        dbOperation.Delete("address", wherePart);
                //    }
                //    dbOperation.CloseDbConnection();
                //}
                List<string> oldRefNoList = GetOldRefNoList(days);
                if (oldRefNoList != null)
                {
                    if (oldRefNoList.Count == 0) return true;

                    dbOperation.OpenDbConnection();
                    for (int i = 0; i < oldRefNoList.Count; i++)
                    {
                        string wherePart = String.Format(" reference_no='{0}'", oldRefNoList[i]);
                        dbOperation.Delete("address", wherePart);
                    }
                    dbOperation.CloseDbConnection();
                }
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Error while deleting previous crime_information data dated on or before : " + x.ToString());
                throw x;
            }
            return true;
        }

        public bool DeletePreviousCrimeInfoData(int days)
        {
            try
            {
                List<string> oldRefNoList = GetOldRefNoList(days);
                if (oldRefNoList != null)
                {
                    if (oldRefNoList.Count == 0) return true;

                    dbOperation.OpenDbConnection();
                    for (int i = 0; i < oldRefNoList.Count; i++)
                    {
                        string wherePart = String.Format(" reference_no='{0}'", oldRefNoList[i]);
                        dbOperation.Delete("crime_information", wherePart);
                    }
                    dbOperation.CloseDbConnection();
                }
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Error while deleting previous crime_information data dated on or before : " + x.ToString());
                throw x;
            }
            return true;
        }

        public bool DeletePreviousAttachmentData(int days)
        {
            try
            {
                List<string> oldRefNoList = GetOldRefNoList(days);
                if (oldRefNoList != null)
                {
                    if (oldRefNoList.Count == 0) return true;

                    dbOperation.OpenDbConnection();
                    for (int i = 0; i < oldRefNoList.Count; i++)
                    {
                        string wherePart = String.Format(" reference_no='{0}'", oldRefNoList[i]);
                        dbOperation.Delete("attachment", wherePart);
                    }
                    dbOperation.CloseDbConnection();
                }
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Error while deleting previous attachment data dated on or before : " + x.ToString());
                throw x;
            }
            return true;
        }

        public bool DeletePreviousNotEntryAttachmentData(int days)
        {
            try
            {
                List<string> oldRefNoList = GetOldNotEntryRefNoList(days);
                if (oldRefNoList != null)
                {
                    if (oldRefNoList.Count == 0) return true;

                    dbOperation.OpenDbConnection();
                    for (int i = 0; i < oldRefNoList.Count; i++)
                    {
                        string wherePart = String.Format(" reference_no='{0}'", oldRefNoList[i]);
                        dbOperation.Delete("attachment", wherePart);
                    }
                    dbOperation.CloseDbConnection();
                }
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Error while deleting previous attachment data dated on or before : " + x.ToString());
                throw x;
            }
            return true;
        }

        public List<long> GetUserRoles(string username)
        {
            List<long> roleList = new List<long>();
            try
            {
                dbOperation.OpenDbConnection();
                string wherePart = String.Format("username='{0}'", AesCryptography.EncryptToString(username));
                string sql = String.Format("SELECT * FROM roles WHERE {0};", wherePart);
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    long role = Convert.ToInt64(AesCryptography.DecryptToString(
                        Utils.GetFormattedString(dataRow["user_role"])));
                    roleList.Add(role);
                }
                dbOperation.CloseDbConnection();
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + username + ". Got error when getting record.\n" + x.ToString());
                throw x;
            }

            return roleList;
        }

        public List<PermissionDto> GetUserPermissions(int userId, string username)
        {
            List<PermissionDto> permissionList = new List<PermissionDto>();
            try
            {
                dbOperation.OpenDbConnection();
                string wherePart = String.Format("user_id={0}", userId);
                string sql = String.Format("SELECT * FROM user_permission WHERE {0};", wherePart);
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    if (dataRow["permission_id"] != DBNull.Value)
                    {
                        int permissionId = Convert.ToInt32(dataRow["permission_id"].ToString());
                        permissionList.Add(new PermissionDto() { permissionId = permissionId });
                    }
                }
                dbOperation.CloseDbConnection();
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Username=" + username + ". Got error when getting user permission from db.\n" + x.ToString());
                throw x;
            }

            return permissionList;
        }

        public void DeleteRole(string username)
        {
            try
            {
                dbOperation.OpenDbConnection();
                string sql = String.Format("username= '{0}'", AesCryptography.EncryptToString(username));
                dbOperation.Delete("roles", sql);
                dbOperation.CloseDbConnection();
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.Message);
            }
        }

        public void DeleteUserPermission(int userId)
        {
            try
            {
                dbOperation.OpenDbConnection();
                string sql = String.Format("user_id= {0}", userId);
                dbOperation.Delete("user_permission", sql);
                dbOperation.CloseDbConnection();
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.Message);
            }
        }

        public bool AddUserRole(string username, List<long> roles)
        {
            try
            {
                dbOperation.OpenDbConnection();
                SQLiteTransaction transaction = dbOperation.GetSqliteConnection().BeginTransaction();
                string encryptedUsername = AesCryptography.EncryptToString(username);
                string roleNames = "";
                foreach (long role in roles)
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("username", encryptedUsername);
                    data.Add("user_role", AesCryptography.EncryptToString(role));
                    dbOperation.InsertData("roles", data);
                    roleNames += "[" + role + "] ";
                }
                logger.Debug("DB USER MANEGER:: User Roles => " + roleNames);

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

        public bool AddUserPermission(int userId, List<PermissionDto> list)
        {
            try
            {
                dbOperation.OpenDbConnection();
                SQLiteTransaction transaction = dbOperation.GetSqliteConnection().BeginTransaction();
                foreach (var obj in list)
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("user_id", userId);
                    data.Add("permission_id", obj.permissionId);
                    dbOperation.InsertData("user_permission", data);
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
    }
}
