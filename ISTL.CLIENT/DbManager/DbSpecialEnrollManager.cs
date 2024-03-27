using ISTL.COMMON;
using ISTL.COMMON.Common;
using ISTL.COMMON.Database;
using ISTL.COMMON.Subscription;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.MODELS.DTO.New.Enrollment.Special;
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
    public class DbSpecialEnrollManager
    {
        public Logger logger = LogManager.GetCurrentClassLogger();
        private DbOperation dbOperation = null;

        public DbSpecialEnrollManager()
        {
            dbOperation = new DbOperation(Globals.Database.NAME, Globals.Database.PASSWORD);
        }

        private string enrollHashValue(SpecialEnrollmentDto dto)
        {
            List<byte[]> hashList = new List<byte[]>();
            
            hashList.Add(Utils.StringToByteArray(dto?.referenceNo));
            hashList.Add(Utils.StringToByteArray(dto?.fullName));
            hashList.Add(Utils.StringToByteArray(dto?.fatherName));
            hashList.Add(Utils.StringToByteArray(dto?.crimeType));
            hashList.Add(Utils.StringToByteArray(dto?.fineAmount));
            hashList.Add(Utils.StringToByteArray(dto?.rabOfficerName));
            hashList.Add(Utils.StringToByteArray(dto?.magistrateName));
            hashList.Add(Utils.StringToByteArray(dto?.placeOfFine));
            hashList.Add(Utils.StringToByteArray(dto?.arrestType));
            hashList.Add(Utils.StringToByteArray(dto?.gender));
            hashList.Add(Utils.StringToByteArray(dto?.unit?.ToString()));
            hashList.Add(Utils.StringToByteArray(dto?.subUnit?.ToString()));
            hashList.Add(Utils.StringToByteArray(dto?.address?.district?.ToString()));
            hashList.Add(Utils.StringToByteArray(dto?.address?.upazila?.ToString()));
            hashList.Add(Utils.StringToByteArray(dto?.address?.union?.ToString()));
            hashList.Add(Utils.StringToByteArray(dto?.address?.villageHouseRoadNo?.ToString()));
            hashList.Add(StaticData.specialEnrollment?.photo?.photo);
            hashList.Add(Utils.StringToByteArray(dto.law));
            hashList.Add(Utils.StringToByteArray(dto.crimeZone?.district?.ToString()));
            hashList.Add(Utils.StringToByteArray(dto.crimeZone?.upozilaOrThana?.ToString()));
            hashList.Add(Utils.StringToByteArray(dto.nid));

            string enrollHash = GenerateSecureHash.CreateSha1Hash(hashList);
            hashList = null;

            return enrollHash;
        }

        public bool AddSpecialCriminalProfile(SpecialEnrollmentDto obj, int status)
        {            
            bool isAdded = false;
            try
            {                
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("reference_no", AesCryptography.EncryptToString(obj?.referenceNo));
                data.Add("full_name", AesCryptography.EncryptToString(obj?.fullName));
                data.Add("father_name", AesCryptography.EncryptToString(obj?.fatherName));
                data.Add("crime_type", AesCryptography.EncryptToString(obj?.crimeType));
                data.Add("fine_amount", AesCryptography.EncryptToString(obj?.fineAmount));
                data.Add("rab_officer_name", AesCryptography.EncryptToString(obj?.rabOfficerName));
                data.Add("magistrate_name", AesCryptography.EncryptToString(obj?.magistrateName));
                data.Add("place_of_fine", AesCryptography.EncryptToString(obj?.placeOfFine));

                if (obj?.address != null)
                {
                    var jsonAdr = new JavaScriptSerializer().Serialize(obj?.address);
                    data.Add("address", AesCryptography.EncryptToString(jsonAdr));
                }                

                data.Add("arrestee_type", obj?.arrestTypeIntValue);
                //data.Add("warrant_no", obj?.warrantNo);
                data.Add("unit", obj?.unit);
                data.Add("sub_unit", obj?.subUnit);

                if (obj?.gender == "Male") data.Add("gender", 0);
                else if (obj?.gender == "Female") data.Add("gender", 1);
                else if (obj?.gender == "Other") data.Add("gender", 2);

                data.Add("status", status);
                data.Add("error_status", Globals.ErrorState.NOT_ERROR);

                string enrollHash = enrollHashValue(obj);
                StaticData.specialEnrollment.hash = enrollHash;
                data.Add("hash", enrollHash);

                if (obj?.photo?.photo?.Length > 0)
                {
                    byte[] criminalPhoto = AesCryptography.EncryptToByte(obj?.photo?.photo);
                    data.Add("photo", criminalPhoto);
                }
                data.Add("law", AesCryptography.EncryptToString(obj?.law));
                var crimeZone = new JavaScriptSerializer().Serialize(obj?.crimeZone);
                data.Add("crime_zone", AesCryptography.EncryptToString(crimeZone));
                data.Add("nid", AesCryptography.EncryptToString(obj?.nid));

                //DateTime dtNow = DateTime.Now;
                //data.Add("created_at", dtNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
                //data.Add("created_by", Users.Id);

                string refNoQuery = String.Format("select * from special_criminal_profile where reference_no='{0}'", AesCryptography.EncryptToString(obj?.referenceNo));
                
                dbOperation.OpenDbConnection();
                string refNoRec = dbOperation.ExecuteScalarReturnStr(refNoQuery);
                if (!string.IsNullOrEmpty(refNoRec))
                {
                    DateTime dtNow = DateTime.Now;
                    data.Add("updated_at", dtNow.ToString("yyyy-MM-ddTHH:mm:ss.fffK"));
                    data.Add("updated_by", Users.Id);

                    string whereClause = String.Format(" reference_no='{0}'", AesCryptography.EncryptToString(obj?.referenceNo));
                    isAdded = dbOperation.Update("special_criminal_profile", data, whereClause);
                }
                else
                {
                    DateTime dtNow = DateTime.Now;
                    data.Add("created_at", dtNow.ToString("yyyy-MM-ddTHH:mm:ss.fffK"));
                    data.Add("created_by", Users.Id);
                    isAdded = dbOperation.InsertData("special_criminal_profile", data);
                }
                dbOperation.CloseDbConnection();
                if (isAdded)
                {
                    logger.Debug("Successfully Added Special Criminal Profile.");
                }
                else
                {
                    logger.Debug("Adding new criminal profile entry failed! Possibly already exists.");
                }                

            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.ToString());
                throw x;
            }

            //if (status == Globals.RecordState.DRAFT)
            //{
            //    CounterDraftSubject counterDraftStatus = (CounterDraftSubject)SubjectFactory.GetInstance().GetSubject(Globals.Common.COUNTER_DRAFT_NAME);
            //    counterDraftStatus.Count = this.GetEnrolledDraftCount() + this.GetSpecialEnrolledDraftCount();
            //    counterDraftStatus.Notify();
            //}

            //else if (status == Globals.RecordState.NEW)
            //{
            //    CounterSubject counterStatus = (CounterSubject)SubjectFactory.GetInstance().GetSubject(Globals.Common.COUNTER_NAME);
            //    counterStatus.Count = this.GetTodayEnrollCount() + this.GetTodaySpecialEnrollCount();
            //    counterStatus.Notify();
            //}

            //CounterErrorSubject counterErrorStatus = (CounterErrorSubject)SubjectFactory.GetInstance().GetSubject(Globals.Common.COUNTER_ERROR_NAME);
            //counterErrorStatus.Count = this.GetSpecialEnrolledErrorCount() + this.GetEnrolledErrorCount();
            //counterErrorStatus.Notify();

            return isAdded;
        }

        public void UpdateProfileValues(string refNo, string columnName, int valueInt, string valueStr)
        {
            //Task.Run(() => UpdateProfileValuesAsync(refNo, columnName, valueInt, valueStr));
            UpdateProfileValuesAsync(refNo, columnName, valueInt, valueStr);
        }

        public bool UpdateProfileValuesAsync(string refNo, string columnName, int valueInt, string valueStr)
        {
            try
            {
                string refNoQuery = String.Format("select * from special_criminal_profile where reference_no='{0}'", AesCryptography.EncryptToString(refNo));
                dbOperation.OpenDbConnection();
                bool exists = dbOperation.ExecuteScalar(refNoQuery);
                dbOperation.CloseDbConnection();
                Dictionary<string, object> data = new Dictionary<string, object>();
                //if (!string.IsNullOrEmpty(valueStr)) data.Add(columnName, valueStr);
                //else 
                if (valueInt >= 0)
                {
                    data.Add(columnName, valueInt);
                }
                else
                {
                    data.Add(columnName, AesCryptography.EncryptToString(valueStr));
                }

                if (!exists)
                {
                    bool val = AddSpecialCriminalProfile(StaticData.specialEnrollment, Globals.RecordState.DRAFT); ;
                    return val;
                }

                string wherePart = String.Format(" reference_no='{0}'", AesCryptography.EncryptToString(refNo));
                if (data.Count <= 0) return false;
                dbOperation.OpenDbConnection();
                bool isUpdated = dbOperation.Update("special_criminal_profile", data, wherePart);
                dbOperation.CloseDbConnection();
                return isUpdated;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x);
                throw x;
            }
        }

        public void UpdateProfilePhoto(string refNo, string columnName, byte[] photo)
        {
            //Task.Run(() => UpdateProfileValuesAsync(refNo, columnName, valueInt, valueStr));
            UpdateProfilePhotoAsync(refNo, columnName, photo);
        }

        public bool UpdateProfilePhotoAsync(string refNo, string columnName, byte[] photo)
        {
            try
            {
                string refNoQuery = String.Format("select * from special_criminal_profile where reference_no='{0}'", AesCryptography.EncryptToString(refNo));
                dbOperation.OpenDbConnection();
                bool exists = dbOperation.ExecuteScalar(refNoQuery);
                dbOperation.CloseDbConnection();
                Dictionary<string, object> data = new Dictionary<string, object>();
                if (photo != null) data.Add(columnName, AesCryptography.EncryptToByte(photo));

                if (!exists)
                {
                    bool val = AddSpecialCriminalProfile(StaticData.specialEnrollment, Globals.RecordState.DRAFT);
                    return val;
                }

                string wherePart = String.Format(" reference_no='{0}'", AesCryptography.EncryptToString(refNo));
                if (data.Count <= 0) return false;
                dbOperation.OpenDbConnection();
                bool isUpdated = dbOperation.Update("special_criminal_profile", data, wherePart);
                dbOperation.CloseDbConnection();
                return isUpdated;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x);
                throw x;
            }
        }

        public SpecialEnrollmentDto GetLocalSpecialEnrolled(string hash)
        {
            try
            {
                SpecialEnrollmentDto obj = new SpecialEnrollmentDto();
                string wherePart = String.Format("hash='{0}'", hash);
                string sql = String.Format("SELECT * FROM special_criminal_profile WHERE {0};", wherePart);
                dbOperation.OpenDbConnection();
                DataTable dataTable = dbOperation.GetDataTable(sql);
                dbOperation.CloseDbConnection();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    obj.referenceNo = dataRow["reference_no"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["reference_no"]) : null;
                    obj.fullName = dataRow["full_name"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["full_name"]) : null;
                    obj.fatherName = dataRow["father_name"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["father_name"]) : null;
                    obj.crimeType = dataRow["crime_type"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["crime_type"]) : null;
                    obj.fineAmount = dataRow["fine_amount"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["fine_amount"]) : null;
                    obj.rabOfficerName = dataRow["rab_officer_name"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["rab_officer_name"]) : null;
                    obj.magistrateName = dataRow["magistrate_name"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["magistrate_name"]) : null;
                    obj.placeOfFine = dataRow["place_of_fine"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["place_of_fine"]) : null;
                    //obj.warrantNo = dataRow["warrant_no"].GetType() != typeof(DBNull) ? (string)dataRow["warrant_no"] : null;
                    obj.hash = dataRow["hash"].GetType() != typeof(DBNull) ? (string)dataRow["hash"] : null;
                    obj.unit = dataRow["unit"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["unit"]) : -1;
                    //obj.subUnit = dataRow["sub_unit"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["sub_unit"]) : -1;
                    if (dataRow["sub_unit"].GetType() != typeof(DBNull))
                    {
                        if (Convert.ToInt32(dataRow["sub_unit"]) > 0) obj.subUnit = Convert.ToInt32(dataRow["sub_unit"]);
                    }

                    var genderCode = dataRow["gender"].GetType() != typeof(DBNull) ? (byte)dataRow["gender"] : -1;
                    if (genderCode == 0)    obj.gender = "Male";
                    else if (genderCode == 1)   obj.gender = "Female";
                    else if (genderCode == 2)   obj.gender = "Other";

                    if (dataRow["address"].GetType() != typeof(DBNull))
                    {
                        var deserializedObject = new JavaScriptSerializer().Deserialize<SpecialAddressDto>(AesCryptography.DecryptToString((string)dataRow["address"]));
                        obj.address = deserializedObject;
                        if (obj.address?.district == 0)
                        {
                            SpecialAddressDto dto = new SpecialAddressDto();
                            dto.villageHouseRoadNo = obj.address?.villageHouseRoadNo;
                            obj.address = dto;
                        }
                    }
                    if (dataRow["created_at"].GetType() != typeof(DBNull))
                    {
                        DateTime dtCreatedAt = (DateTime)dataRow["created_at"];
                        //obj.createdAt = dtCreatedAt.ToString("yyyy-MM-ddTHH:mm:ss.fffK");
                        obj.createdAt = dtCreatedAt.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    var arresteeType = dataRow["arrestee_type"].GetType() != typeof(DBNull) ? dataRow["arrestee_type"] : 0;
                    obj.arrestTypeIntValue = Convert.ToInt32(arresteeType);
                    if (obj.arrestTypeIntValue == 1) obj.arrestType = "MobileCourts";
                    //else if (obj.arrestTypeIntValue == 2) obj.arrestType = "Warrants";
                    //else if (obj.arrestTypeIntValue == 3) obj.arrestType = "Gamblers";
                    else if (obj.arrestTypeIntValue == 2) obj.arrestType = "ContagiousPatients";
                    else if (obj.arrestTypeIntValue == 3) obj.arrestType = "directSubmitInPS";

                    var createdByUser = dataRow["created_by"].GetType() != typeof(DBNull) ? dataRow["created_by"] : -1;
                    obj.createdBy = Convert.ToInt32(createdByUser);

                    if (obj.photo == null)  obj.photo = new SpecialEnrollPhotoDto();
                    obj.photo.photo = dataRow["photo"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToByte((byte[])dataRow["photo"]) : null;
                    if (obj.photo.photo != null && obj.photo.photo?.Length > 0)
                    {                        
                        obj.photo.contentType = "image/jpg";
                        obj.photo.extension = ".jpg";
                    }
                    else obj.photo = null;

                    obj.law = dataRow["law"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["law"]) : null;
                    if (dataRow["crime_zone"].GetType() != typeof(DBNull))
                    {
                        var deserializedObject = new JavaScriptSerializer().Deserialize<SpecialCrimeZoneDto>(AesCryptography.DecryptToString((string)dataRow["crime_zone"]));
                        obj.crimeZone = deserializedObject;
                    }
                    obj.nid = dataRow["nid"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["nid"]) : null;
                }
                return obj;
            }
            catch (Exception e)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Hash=" + hash + ". Got error when getting record.\n" + e.ToString());
                throw e;
            }
        }

        public bool UpdateErrorStatus(string hash)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("error_status", 1);

                string wherePart = String.Format(" hash='{0}'", hash);
                dbOperation.OpenDbConnection();
                bool isUpdated = dbOperation.Update("special_criminal_profile", data, wherePart);

                dbOperation.CloseDbConnection();
                return true;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x);
                throw x;
            }
        }

        public bool UpdateErrorMessage(string hash, string Message)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("error_message", Message);

                string wherePart = String.Format(" hash='{0}'", hash);
                dbOperation.OpenDbConnection();
                bool isUpdated = dbOperation.Update("special_criminal_profile", data, wherePart);

                dbOperation.CloseDbConnection();
                return true;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x);
                throw x;
            }
        }

        public void GetVerifyDayCount()
        {
            try
            {
                string sql = "SELECT verify_day_count FROM configuration LIMIT 1;";
                dbOperation.OpenDbConnection();
                DataTable dataTable = dbOperation.GetDataTable(sql);
                dbOperation.CloseDbConnection();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    Configuration.VerifyDayCount = Convert.ToInt32(AesCryptography.DecryptToString(dataRow["verify_day_count"].ToString()));
                }
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Got error when getting record for configuration.\n" + x.ToString());
                throw x;
            }
        }

        public List<string> GetEnrolledHash()
        {
            List<string> hashList = new List<string>();
            try
            {
                string wherePart = String.Format("(status='{0}' OR status='{1}') AND error_status='{2}'", Globals.RecordState.NEW, Globals.RecordState.EXPORTED, Globals.ErrorState.NOT_ERROR);
                string sql = String.Format("SELECT hash FROM special_criminal_profile WHERE {0};", wherePart);
                
                dbOperation.OpenDbConnection();
                DataTable dataTable = dbOperation.GetDataTable(sql);
                dbOperation.CloseDbConnection();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    hashList.Add(dataRow["hash"].ToString());
                }
                return hashList;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.Message);
            }
            return hashList;
        }

        public int GetTodaySpecialEnrollCount()
        {
            int totalCount = 0;
            try
            {
                string wherePart = String.Format("strftime('%Y-%m-%d', created_at)='{0}' and created_by='{1}'",
                    DateTime.Today.ToString("yyyy-MM-dd"), Users.Id);
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

        public int GetTodayEnrollCount()
        {
            int totalCount = 0;
            try
            {
                string wherePart = String.Format("strftime('%Y-%m-%d', created_at)='{0}' and created_by='{1}'",
                    DateTime.Today.ToString("yyyy-MM-dd"), Users.Id);
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

        public int GetSpecialEnrolledErrorCount()
        {
            int totalCount = 0;
            try
            {
                string wherePart = String.Format("strftime('%Y-%m-%d', created_at)<='{0}' and created_by='{1}' and error_status = {2}",
                    DateTime.Today.ToString("yyyy-MM-dd"), Users.Id, Globals.ErrorState.ERROR);
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

        public int GetEnrolledErrorCount()
        {
            int totalCount = 0;
            try
            {
                string wherePart = String.Format("strftime('%Y-%m-%d', created_at)<='{0}' and created_by='{1}' and error_status = {2}",
                    DateTime.Today.ToString("yyyy-MM-dd"), Users.Id, Globals.ErrorState.ERROR);
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

        public int GetSpecialEnrolledDraftCount()
        {
            int totalCount = 0;
            try
            {
                string wherePart = String.Format("strftime('%Y-%m-%d', created_at)<='{0}' and created_by='{1}' and status = {2}",
                    DateTime.Today.ToString("yyyy-MM-dd"), Users.Id, Globals.RecordState.DRAFT);
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

        public int GetEnrolledDraftCount()
        {
            int totalCount = 0;
            try
            {
                string wherePart = String.Format("strftime('%Y-%m-%d', created_at)<='{0}' and created_by='{1}' and status = {2}",
                    DateTime.Today.ToString("yyyy-MM-dd"), Users.Id, Globals.RecordState.DRAFT);
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

        public List<string> GetUploadedHashList(int days)
        {
            List<string> hashList = new List<string>();
            try
            {
                TimeZone zone = TimeZone.CurrentTimeZone;
                DateTime currentDate = zone.ToLocalTime(DateTime.Now);
                DateTime previousDate = currentDate.AddDays(-days);
                
                string wherePart = String.Format("strftime('%Y-%m-%d', uploaded_date)<='{0}' and status='{1}'",
                    previousDate.ToString("yyyy-MM-dd"), Globals.RecordState.UPLOADED);
                string sql = String.Format("SELECT hash FROM special_criminal_profile WHERE {0};", wherePart);
                
                dbOperation.OpenDbConnection();
                DataTable dataTable = dbOperation.GetDataTable(sql);
                dbOperation.CloseDbConnection();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    hashList.Add(dataRow["hash"].ToString());
                }
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.Message);
            }
            return hashList;
        }

        public bool UpdateEnrollStatusToVerified(string hash)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("status", Globals.RecordState.VERIFIED);

                string wherePart = String.Format(" hash='{0}'", hash);
                dbOperation.OpenDbConnection();
                bool isUpdated = dbOperation.Update("special_criminal_profile", data, wherePart);

                dbOperation.CloseDbConnection();
                return true;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x);
                throw x;
            }
        }

        public bool UpdateEnrollStatusToNew(string hash)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("status", Globals.RecordState.NEW);

                string wherePart = String.Format(" hash='{0}'", hash);
                dbOperation.OpenDbConnection();
                bool isUpdated = dbOperation.Update("special_criminal_profile", data, wherePart);

                dbOperation.CloseDbConnection();
                return true;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x);
                throw x;
            }
        }

        public bool UpdateStatusToUploaded(string hash)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("status", Globals.RecordState.UPLOADED);
                data.Add("error_status", 0);
                data.Add("uploaded_date", Utils.GetCurrentDateTime());

                string wherePart = String.Format(" hash='{0}'", hash);
                dbOperation.OpenDbConnection();
                bool isUpdated = dbOperation.Update("special_criminal_profile", data, wherePart);

                dbOperation.CloseDbConnection();
                return true;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x);
                throw x;
            }
        }
    }
}
