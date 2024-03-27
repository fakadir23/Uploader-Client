using ISTL.COMMON;
using ISTL.COMMON.Common;
using ISTL.COMMON.Database;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.MODELS.DTO.New.Enrollment.Other;
using ISTL.MODELS.DTO.New.NotEntry;
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
    public class DbNotEntryManager
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private DbOperation dbOperation = null;
        public int RecordCount;

        public DbNotEntryManager()
        {
            dbOperation = new DbOperation(Globals.Database.NAME, Globals.Database.PASSWORD);
        }

        public bool AddNotEntry(NotEntryDto dto)
        {
            bool isAdded = false;
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("reference_no", AesCryptography.EncryptToString(dto.referenceNo));
                data.Add("no_entry_date", AesCryptography.EncryptToString(dto.noEntryDate));
                data.Add("not_entry_reason", AesCryptography.EncryptToString(dto.notEntryReason));
                data.Add("not_entry_case_type", AesCryptography.EncryptToString(dto.notEntryCaseType));
                data.Add("accused_name", AesCryptography.EncryptToString(dto.accusedName));
                data.Add("father_name", AesCryptography.EncryptToString(dto.fatherName));
                data.Add("mobile_no", AesCryptography.EncryptToString(dto.mobileNo));

                data.Add("unit", AesCryptography.EncryptToString(dto.unit));
                data.Add("sub_unit", AesCryptography.EncryptToString(dto.subUnit));

                if (dto.address != null)
                {
                    var jsonData= new JavaScriptSerializer().Serialize(dto.address);
                    data.Add("address", AesCryptography.EncryptToString(jsonData));
                }

                if (dto.attachment?.firList?.Count != null)
                {
                    if (dto.attachment?.firList?.Count > 0)
                    {
                        //var jsonFir = new JavaScriptSerializer().Serialize(obj?.firs);
                        //data.Add("fir", jsonFir);
                        AddAttachment(dto, Globals.AttachmentType.FIR);
                    }
                }

                if (dto.attachment?.complaintList != null)
                {
                    if (dto.attachment?.complaintList?.Count > 0)
                    {
                        AddAttachment(dto, Globals.AttachmentType.COMPLAIN);
                    }
                }

                if (dto.attachment?.seizureList != null)
                {
                    if (dto.attachment?.seizureList.Count > 0)
                    {
                        AddAttachment(dto, Globals.AttachmentType.SEIZURE);
                    }
                }

                data.Add("warrant_no", AesCryptography.EncryptToString(dto.warrantNo));
                data.Add("warrant_issue_date", AesCryptography.EncryptToString(dto.warrantIssueDate));

                data.Add("status", Globals.RecordState.NEW);
                data.Add("error_status", Globals.ErrorState.NOT_ERROR);
                
                data.Add("error_message", null);

                data.Add("not_entry_id", AesCryptography.EncryptToString(dto.id));

                // Photo
                data.Add("photo", AesCryptography.EncryptToByte(dto.biometric?.photo?.photo));

                // FP 
                data.Add("fp_rt", AesCryptography.EncryptToByte(dto.biometric?.fingerprint?.rt));
                data.Add("fp_ri", AesCryptography.EncryptToByte(dto.biometric?.fingerprint?.ri));
                data.Add("fp_rm", AesCryptography.EncryptToByte(dto.biometric?.fingerprint?.rm));
                data.Add("fp_rr", AesCryptography.EncryptToByte(dto.biometric?.fingerprint?.rr));
                data.Add("fp_rs", AesCryptography.EncryptToByte(dto.biometric?.fingerprint?.rs));

                data.Add("fp_lt", AesCryptography.EncryptToByte(dto.biometric?.fingerprint?.lt));
                data.Add("fp_li", AesCryptography.EncryptToByte(dto.biometric?.fingerprint?.li));
                data.Add("fp_lm", AesCryptography.EncryptToByte(dto.biometric?.fingerprint?.lm));
                data.Add("fp_lr", AesCryptography.EncryptToByte(dto.biometric?.fingerprint?.lr));
                data.Add("fp_ls", AesCryptography.EncryptToByte(dto.biometric?.fingerprint?.ls));

                // Iris
                data.Add("right_iris", AesCryptography.EncryptToByte(dto.biometric?.iris?.right));
                data.Add("left_iris", AesCryptography.EncryptToByte(dto.biometric?.iris?.left));

                string wherePart = String.Format(" reference_no='{0}'", AesCryptography.EncryptToString(dto.referenceNo));
                string checkIfExistsQuery = String.Format("select * from not_entry where {0}", wherePart);

                dbOperation.OpenDbConnection();
                string exists = dbOperation.ExecuteScalarReturnStr(checkIfExistsQuery);

                if (!string.IsNullOrEmpty(exists))
                {
                    data.Add("updated_at", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    data.Add("updated_by", Users.Id);
                    isAdded = dbOperation.Update("not_entry", data, wherePart);
                }
                else
                {
                    data.Add("created_at", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    data.Add("created_by", Users.Id);
                    isAdded = dbOperation.InsertData("not_entry", data);
                }
                dbOperation.CloseDbConnection();
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Error while insert/update in not_entry table. "+x.ToString());
                throw x;
            }
            return isAdded;
        }

        public void AddAttachment(NotEntryDto obj, int value)
        {
            try
            {
                string wherePart1 = string.Format(" reference_no = '{0}';", AesCryptography.EncryptToString(obj?.referenceNo));
                string refNoQuery1 = String.Format("select * from not_entry where " + wherePart1);
                dbOperation.OpenDbConnection();
                string refNoRec1 = dbOperation.ExecuteScalarReturnStr(refNoQuery1);
                dbOperation.CloseDbConnection();
                if (string.IsNullOrEmpty(refNoRec1))
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("reference_no", AesCryptography.EncryptToString(obj?.referenceNo));
                    data.Add("status", Globals.RecordState.DRAFT);
                    data.Add("error_status", Globals.ErrorState.NOT_ERROR);
                    DateTime dtNow = DateTime.Now;
                    data.Add("created_at", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
                    data.Add("created_by", Users.Id);
                    if (!string.IsNullOrEmpty(obj?.id)) data.Add("not_entry_id", AesCryptography.EncryptToString(obj?.id));
                    dbOperation.OpenDbConnection();
                    dbOperation.InsertData("not_entry", data);
                    dbOperation.CloseDbConnection();
                }
            }
            catch(Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Adding not_entry profile while uploading attachment failed "+x.ToString());
                throw x;
            }

            if (value == Globals.AttachmentType.COMPLAIN)
            {
                try
                {
                    for (int i=0; i < obj.attachment?.complaintList?.Count; i++)
                    {
                        if (obj.attachment?.complaintList[i].complaint != null)
                        {
                            Dictionary<string, object> attachmentData = new Dictionary<string, object>();

                            attachmentData.Add("reference_no", AesCryptography.EncryptToString(obj?.referenceNo));
                            attachmentData.Add("attachment_type", Globals.AttachmentType.COMPLAIN);
                            attachmentData.Add("attachment_no", (i + 1));
                            attachmentData.Add("extension", AesCryptography.EncryptToString(obj.attachment?.complaintList[i].extension));
                            attachmentData.Add("content_type", AesCryptography.EncryptToString(obj.attachment?.complaintList[i].contentType));
                            attachmentData.Add("attachment_byte", AesCryptography.EncryptToByte(obj.attachment?.complaintList[i].complaint));

                            dbOperation.OpenDbConnection();

                            string wherePart = string.Format(" reference_no = '{0}' AND attachment_no = '{1}' AND attachment_type='{2}'",
                                 AesCryptography.EncryptToString(obj?.referenceNo), (i + 1), Globals.AttachmentType.COMPLAIN);
                            string refNoQuery = String.Format("select * from attachment where " + wherePart);

                            string refNoRec = dbOperation.ExecuteScalarReturnStr(refNoQuery);

                            bool isAdded = false;
                            if (!string.IsNullOrEmpty(refNoRec))
                            {
                                isAdded = dbOperation.Update("attachment", attachmentData, wherePart);
                            }
                            else
                            {
                                isAdded = dbOperation.InsertData("attachment", attachmentData);
                            }
                            if (isAdded)
                            {
                                logger.Debug("Successfully added complain.");
                                dbOperation.CloseDbConnection();
                            }
                            else
                            {
                                logger.Debug("Adding complain failed!");
                                dbOperation.CloseDbConnection();
                            }
                        }
                    }
                }
                catch (Exception x)
                {
                    dbOperation.CloseDbConnection();
                    logger.Error(x.ToString());
                    throw x;
                }
            }
            else if (value == Globals.AttachmentType.FIR)
            {
                try
                {                    
                    for (int i = 0; i < obj.attachment?.firList?.Count; i++)
                    {
                        if (obj.attachment?.firList[i].fir != null)
                        {
                            Dictionary<string, object> attachmentData = new Dictionary<string, object>();

                            attachmentData.Add("reference_no", AesCryptography.EncryptToString(obj?.referenceNo));
                            attachmentData.Add("attachment_type", Globals.AttachmentType.FIR);
                            attachmentData.Add("attachment_no", (i + 1));
                            attachmentData.Add("extension", AesCryptography.EncryptToString(obj.attachment?.firList[i].extension));
                            attachmentData.Add("content_type", AesCryptography.EncryptToString(obj.attachment?.firList[i].contentType));
                            attachmentData.Add("attachment_byte", AesCryptography.EncryptToByte(obj.attachment?.firList[i].fir));
                            attachmentData.Add("fir_date", AesCryptography.EncryptToString(obj.attachment?.firList[i].firDate));
                            attachmentData.Add("fir_no", AesCryptography.EncryptToString(obj.attachment?.firList[i].firNo));
                            attachmentData.Add("fir_district", obj.attachment?.firList[i].district);
                            attachmentData.Add("fir_upazila", obj.attachment?.firList[i].upozilaOrThana);

                            dbOperation.OpenDbConnection();
                            string wherePart = string.Format(" reference_no = '{0}' AND attachment_no = '{1}' AND attachment_type='{2}'",
                                 AesCryptography.EncryptToString(obj?.referenceNo), (i + 1), Globals.AttachmentType.FIR);
                            string refNoQuery = String.Format("select * from attachment where " + wherePart);

                            string refNoRec = dbOperation.ExecuteScalarReturnStr(refNoQuery);

                            bool isAdded = false;
                            if (!string.IsNullOrEmpty(refNoRec))
                            {
                                isAdded = dbOperation.Update("attachment", attachmentData, wherePart);
                            }
                            else
                            {
                                isAdded = dbOperation.InsertData("attachment", attachmentData);
                            }
                            if (isAdded)
                            {
                                logger.Debug("Successfully added fir.");
                                dbOperation.CloseDbConnection();
                            }
                            else
                            {
                                logger.Debug("Adding fir failed!");
                                dbOperation.CloseDbConnection();
                            }
                        }
                    }
                }
                catch (Exception x)
                {
                    dbOperation.CloseDbConnection();
                    logger.Error(x.ToString());
                    throw x;
                }
            }
            else if (value == Globals.AttachmentType.SEIZURE)
            {
                try
                {
                    for (int i = 0; i < obj.attachment?.seizureList?.Count; i++)
                    {
                        if (obj.attachment?.seizureList[i].seizure != null)
                        {
                            Dictionary<string, object> attachmentData = new Dictionary<string, object>();

                            attachmentData.Add("reference_no", AesCryptography.EncryptToString(obj?.referenceNo));
                            attachmentData.Add("attachment_type", Globals.AttachmentType.SEIZURE);
                            attachmentData.Add("attachment_no", (i + 1));
                            attachmentData.Add("extension", AesCryptography.EncryptToString(obj.attachment?.seizureList[i].extension));
                            attachmentData.Add("content_type", AesCryptography.EncryptToString(obj.attachment?.seizureList[i].contentType));
                            attachmentData.Add("attachment_byte", AesCryptography.EncryptToByte(obj.attachment?.seizureList[i].seizure));

                            dbOperation.OpenDbConnection();
                            string wherePart = string.Format(" reference_no = '{0}' AND attachment_no = '{1}' AND attachment_type='{2}'",
                                 AesCryptography.EncryptToString(obj?.referenceNo), (i + 1), Globals.AttachmentType.SEIZURE);
                            string refNoQuery = String.Format("select * from attachment where " + wherePart);

                            string refNoRec = dbOperation.ExecuteScalarReturnStr(refNoQuery);

                            bool isAdded = false;
                            if (!string.IsNullOrEmpty(refNoRec))
                            {
                                isAdded = dbOperation.Update("attachment", attachmentData, wherePart);
                            }
                            else
                            {
                                isAdded = dbOperation.InsertData("attachment", attachmentData);
                            }
                            if (isAdded)
                            {
                                logger.Debug("Successfully added seizure.");
                                dbOperation.CloseDbConnection();
                            }
                            else
                            {
                                logger.Debug("Adding seizure failed!");
                                dbOperation.CloseDbConnection();
                            }
                        }
                    }
                }
                catch (Exception x)
                {
                    dbOperation.CloseDbConnection();
                    logger.Error(x.ToString());
                    throw x;
                }
            }
        }

        public NotEntryDto GetLocalNotEntry(string referencoNo)
        {
            try
            {
                NotEntryDto obj = new NotEntryDto();
                string wherePart = String.Format(" reference_no='{0}'", AesCryptography.EncryptToString(referencoNo));
                string sql = String.Format("SELECT * FROM not_entry WHERE {0};", wherePart);
                dbOperation.OpenDbConnection();
                DataTable dataTable = dbOperation.GetDataTable(sql);
                dbOperation.CloseDbConnection();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    obj.id = dataRow["not_entry_id"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString(Convert.ToString(dataRow["not_entry_id"])) : null;
                    obj.referenceNo = dataRow["reference_no"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString(Convert.ToString(dataRow["reference_no"])) : null;
                    obj.noEntryDate = dataRow["no_entry_date"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["no_entry_date"]) : null;
                    obj.notEntryReason = dataRow["not_entry_reason"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["not_entry_reason"]) : null;
                    obj.notEntryCaseType = dataRow["not_entry_case_type"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["not_entry_case_type"]) : null;
                    obj.accusedName = dataRow["accused_name"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["accused_name"]) : null;
                    obj.fatherName = dataRow["father_name"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["father_name"]) : null;
                    obj.mobileNo = dataRow["mobile_no"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["mobile_no"]) : null;
                    if (dataRow["address"].GetType() != typeof(DBNull))
                    {
                        var deserializedObject = new JavaScriptSerializer().Deserialize<AddressDto>(AesCryptography.DecryptToString((string)dataRow["address"]));
                        obj.address = deserializedObject;
                    }
                    obj.warrantNo = dataRow["warrant_no"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["warrant_no"]) : null;
                    obj.warrantIssueDate = dataRow["warrant_issue_date"].GetType() != typeof(DBNull) ?
                        AesCryptography.DecryptToString((string)dataRow["warrant_issue_date"]) : null;

                    obj.unit = dataRow["unit"].GetType() != typeof(DBNull) ? Convert.ToInt32(AesCryptography.DecryptToString((string)dataRow["unit"])) : -1;
                    if (dataRow["sub_unit"].GetType() != typeof(DBNull))
                    {
                        if (Convert.ToInt32(AesCryptography.DecryptToString((string)dataRow["sub_unit"])) > 0) obj.subUnit = Convert.ToInt32(AesCryptography.DecryptToString((string)dataRow["sub_unit"]));
                    }

                    // Fetching Attachment for Complain, Seizure & FIR

                    AttachmentDto attachmentDto = new AttachmentDto();

                    attachmentDto = GetAttachmentInfo(obj.referenceNo);
                    attachmentDto.unit = obj?.unit;
                    attachmentDto.referenceNo = obj?.referenceNo;

                    obj.attachment = attachmentDto;

                    FIRDto firDto = new FIRDto();
                    List<FIRDto> firList = new List<FIRDto>();
                    if (obj.attachment?.firList?.Count > 0)
                    {
                        firDto.firNo = obj.attachment?.firList[0].firNo;
                        firDto.firDate = obj.attachment?.firList[0].firDate;
                        firDto.district = obj.attachment?.firList[0].district;
                        firDto.upozilaOrThana = obj.attachment?.firList[0].upozilaOrThana;
                    }
                    else
                    {
                        obj.attachment.firList = null;
                        obj.fir = null;
                    }
                    if (obj.attachment?.firList?.Count > 0)
                    {
                        //firList.Add(firDto);
                        //obj.fir = firList;
                        obj.fir = firDto;
                    }

                    if (dataRow["created_at"].GetType() != typeof(DBNull))
                    {
                        DateTime dtCreatedAt = (DateTime)dataRow["created_at"];
                        obj.createdAt = dtCreatedAt.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    obj.createdBy = dataRow["created_by"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["created_by"]) : -1;

                    // Photo
                    obj.biometric = new MODELS.DTO.New.Enrollment.Biometric.BiometricDto();
                    obj.biometric.photo.photo = dataRow["photo"] != DBNull.Value
                                                        ? AesCryptography.DecryptToByte((byte[])dataRow["photo"]) : null;
                    if (obj.biometric.photo.photo != null)
                    {
                        obj.biometric.photo.unit = obj.unit;
                        obj.biometric.photo.referenceNo = obj.referenceNo;
                        obj.biometric.photo.contentType = "image/jpg";
                        obj.biometric.photo.extension = ".jpg";
                        obj.biometric.referenceNo = obj.referenceNo;
                    }
                    else obj.biometric.photo = null;
                    // FP

                    obj.biometric.fingerprint.rt = dataRow["fp_rt"] != DBNull.Value
                                                        ? AesCryptography.DecryptToByte((byte[])dataRow["fp_rt"]) : null;
                    obj.biometric.fingerprint.ri = dataRow["fp_ri"] != DBNull.Value
                                                        ? AesCryptography.DecryptToByte((byte[])dataRow["fp_ri"]) : null;
                    obj.biometric.fingerprint.rm = dataRow["fp_rm"] != DBNull.Value
                                                        ? AesCryptography.DecryptToByte((byte[])dataRow["fp_rm"]) : null;
                    obj.biometric.fingerprint.rr = dataRow["fp_rr"] != DBNull.Value
                                                        ? AesCryptography.DecryptToByte((byte[])dataRow["fp_rr"]) : null;
                    obj.biometric.fingerprint.rs = dataRow["fp_rs"] != DBNull.Value
                                                        ? AesCryptography.DecryptToByte((byte[])dataRow["fp_rs"]) : null;

                    obj.biometric.fingerprint.lt = dataRow["fp_lt"] != DBNull.Value
                                                        ? AesCryptography.DecryptToByte((byte[])dataRow["fp_lt"]) : null;
                    obj.biometric.fingerprint.li = dataRow["fp_li"] != DBNull.Value
                                                        ? AesCryptography.DecryptToByte((byte[])dataRow["fp_li"]) : null;
                    obj.biometric.fingerprint.lm = dataRow["fp_lm"] != DBNull.Value
                                                        ? AesCryptography.DecryptToByte((byte[])dataRow["fp_lm"]) : null;
                    obj.biometric.fingerprint.lr = dataRow["fp_lr"] != DBNull.Value
                                                        ? AesCryptography.DecryptToByte((byte[])dataRow["fp_lr"]) : null;
                    obj.biometric.fingerprint.ls = dataRow["fp_ls"] != DBNull.Value
                                                        ? AesCryptography.DecryptToByte((byte[])dataRow["fp_ls"]) : null;

                    if (obj.biometric.fingerprint.lt != null
                        || obj.biometric.fingerprint.li != null
                        || obj.biometric.fingerprint.lm != null
                        || obj.biometric.fingerprint.lr != null
                        || obj.biometric.fingerprint.ls != null
                        || obj.biometric.fingerprint.rt != null
                        || obj.biometric.fingerprint.ri != null
                        || obj.biometric.fingerprint.rm != null
                        || obj.biometric.fingerprint.rr != null
                        || obj.biometric.fingerprint.rs != null)
                    {
                        obj.biometric.fingerprint.unit = obj.unit;
                        obj.biometric.fingerprint.contentType = "image/wsq";
                        obj.biometric.fingerprint.extension = ".wsq";
                        obj.biometric.fingerprint.referenceNo = obj.referenceNo;
                        obj.biometric.referenceNo = obj.referenceNo;
                    }
                    else obj.biometric.fingerprint = null;

                    // Iris

                    obj.biometric.iris.right = dataRow["right_iris"] != DBNull.Value
                                                        ? AesCryptography.DecryptToByte((byte[])dataRow["right_iris"]) : null;
                    obj.biometric.iris.left = dataRow["left_iris"] != DBNull.Value
                                                        ? AesCryptography.DecryptToByte((byte[])dataRow["left_iris"]) : null;

                    if (obj.biometric.iris.right != null || obj.biometric.iris.left != null)
                    {
                        obj.biometric.iris.unit = obj.unit;
                        obj.biometric.iris.contentType = "image/jpg";
                        obj.biometric.iris.extension = ".jpg";
                        obj.biometric.iris.referenceNo = obj.referenceNo;
                        obj.biometric.referenceNo = obj.referenceNo;
                    }
                    else obj.biometric.iris = null;

                    if (obj.biometric.photo == null && obj.biometric.fingerprint == null && obj.biometric.iris == null)
                    {
                        obj.biometric = null;
                    }
                }
                return obj;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Reference No=" + referencoNo + ". Got error when getting record from not_entry table.\n" + x.ToString());
                throw x;
            }
        }

        private AttachmentDto GetAttachmentInfo(string refNo)
        {
            try
            {
                AttachmentDto attachmentDto = new AttachmentDto();
                string wherePart = String.Format("reference_no='" + AesCryptography.EncryptToString(refNo) + "'");
                string sql = String.Format("SELECT *FROM attachment WHERE {0};", wherePart);

                dbOperation.OpenDbConnection();
                DataTable dataTable = dbOperation.GetDataTable(sql);
                dbOperation.CloseDbConnection();

                List<ComplainDto> complainList = new List<ComplainDto>();
                List<FIRDto> firList = new List<FIRDto>();
                List<SeizureDto> seizureList = new List<SeizureDto>();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    if (dataRow["attachment_type"].GetType() != typeof(DBNull))
                    {
                        if (Convert.ToInt32(dataRow["attachment_type"]) == Globals.AttachmentType.COMPLAIN)
                        {
                            ComplainDto complainDto = new ComplainDto();
                            complainDto.attachmentNumber = dataRow["attachment_no"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["attachment_no"]) : 1;
                            complainDto.contentType = dataRow["content_type"].GetType() != typeof(DBNull) ?
                                AesCryptography.DecryptToString(Convert.ToString(dataRow["content_type"])) : null;
                            complainDto.extension = dataRow["extension"].GetType() != typeof(DBNull) ?
                                AesCryptography.DecryptToString(Convert.ToString(dataRow["extension"])) : null;
                            complainDto.complaint = dataRow["attachment_byte"] != DBNull.Value ?
                                AesCryptography.DecryptToByte((byte[])dataRow["attachment_byte"]) : null;

                            if (complainDto.complaint != null) complainList.Add(complainDto);
                        }
                        else if (Convert.ToInt32(dataRow["attachment_type"]) == Globals.AttachmentType.SEIZURE)
                        {
                            SeizureDto seizureDto = new SeizureDto();
                            seizureDto.attachmentNumber = dataRow["attachment_no"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["attachment_no"]) : 1;
                            seizureDto.contentType = dataRow["content_type"].GetType() != typeof(DBNull) ?
                                AesCryptography.DecryptToString(Convert.ToString(dataRow["content_type"])) : null;
                            seizureDto.extension = dataRow["extension"].GetType() != typeof(DBNull) ?
                                AesCryptography.DecryptToString(Convert.ToString(dataRow["extension"])) : null;
                            seizureDto.seizure = dataRow["attachment_byte"] != DBNull.Value ?
                                AesCryptography.DecryptToByte((byte[])dataRow["attachment_byte"]) : null;

                            if (seizureDto.seizure != null) seizureList.Add(seizureDto);
                        }
                        else if (Convert.ToInt32(dataRow["attachment_type"]) == Globals.AttachmentType.FIR)
                        {
                            FIRDto firDto = new FIRDto();
                            firDto.attachmentNumber = dataRow["attachment_no"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["attachment_no"]) : 1;
                            firDto.contentType = dataRow["content_type"].GetType() != typeof(DBNull) ?
                                AesCryptography.DecryptToString(Convert.ToString(dataRow["content_type"])) : null;
                            firDto.extension = dataRow["extension"].GetType() != typeof(DBNull) ?
                                AesCryptography.DecryptToString(Convert.ToString(dataRow["extension"])) : null;
                            firDto.fir = dataRow["attachment_byte"] != DBNull.Value ?
                                AesCryptography.DecryptToByte((byte[])dataRow["attachment_byte"]) : null;
                            firDto.firNo = dataRow["fir_no"] != DBNull.Value ?
                                AesCryptography.DecryptToString(Convert.ToString(dataRow["fir_no"])) : null;
                            firDto.firDate = dataRow["fir_date"] != DBNull.Value ?
                                AesCryptography.DecryptToString(Convert.ToString(dataRow["fir_date"])) : null;
                            firDto.district = dataRow["fir_district"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["fir_district"]) : 0;
                            firDto.upozilaOrThana = dataRow["fir_upazila"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["fir_upazila"]) : 0;

                            if (firDto.fir != null) firList.Add(firDto);
                        }
                    }
                }
                attachmentDto.complaintList = complainList;
                attachmentDto.firList = firList;
                attachmentDto.seizureList = seizureList;

                return attachmentDto;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Reference No = " + refNo + ". Got error when getting attachment.\n" + x.ToString());
                throw x;
            }
        }

        public List<string> GetLocalNotEntryListUploadPending()
        {
            List<string> UploadPendingRefNoList = new List<string>();
            try
            {
                string wherePart = String.Format("status='{0}' AND error_status='{1}'", Globals.RecordState.NEW, Globals.ErrorState.NOT_ERROR);
                string sql = String.Format("SELECT reference_no FROM not_entry WHERE {0};", wherePart);

                dbOperation.OpenDbConnection();
                DataTable dataTable = dbOperation.GetDataTable(sql);
                dbOperation.CloseDbConnection();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    UploadPendingRefNoList.Add(AesCryptography.DecryptToString(dataRow["reference_no"].ToString()));
                }
                return UploadPendingRefNoList;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.Message);
            }
            return UploadPendingRefNoList;
        }

        public List<NotEntryDto> GetLocalRecords(string filterClause, int status, int errorStatus, int offset)
        {
            List<NotEntryDto> notEntryList = new List<NotEntryDto>();
            try
            {
                string wherePart = filterClause;

                wherePart += " AND status=" + status;
                wherePart += " AND error_status=" + errorStatus;

                string sqlCount = String.Format("SELECT count(*) FROM not_entry ne WHERE 1 = 1 {0};", wherePart);

                wherePart += " ORDER BY datetime(ne.created_at) desc LIMIT 10 OFFSET " + offset;

                string sql = String.Format("SELECT * FROM not_entry ne WHERE 1 = 1 {0};", wherePart);

                dbOperation.OpenDbConnection();
                DataTable dataTable = dbOperation.GetDataTable(sql);

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    NotEntryDto obj = new NotEntryDto();

                    obj.referenceNo = dataRow["reference_no"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["reference_no"]) : null;
                    obj.noEntryDate = dataRow["no_entry_date"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["no_entry_date"]) : null;
                    obj.notEntryReason = dataRow["not_entry_reason"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["not_entry_reason"]) : null;
                    obj.notEntryCaseType = dataRow["not_entry_case_type"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["not_entry_case_type"]) : null;
                    obj.accusedName = dataRow["accused_name"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["accused_name"]) : null;
                    obj.fatherName = dataRow["father_name"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["father_name"]) : null;
                    obj.mobileNo = dataRow["mobile_no"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["mobile_no"]) : null;
                    obj.warrantNo = dataRow["warrant_no"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["warrant_no"]) : null;
                    obj.errorMessage = dataRow["error_message"].GetType() != typeof(DBNull) ? (string)dataRow["error_message"] : null;

                    notEntryList.Add(obj);
                }

                RecordCount = dbOperation.ExecuteScalarReturnInt(sqlCount);

                dbOperation.CloseDbConnection();
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
            }
            return notEntryList;
        }

        public bool UpdateStatusToUploaded(string refNo)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("status", Globals.RecordState.UPLOADED);
                data.Add("error_status", 0);
                data.Add("uploaded_at", Utils.GetCurrentDateTime());

                string wherePart = String.Format(" reference_no='{0}'", AesCryptography.EncryptToString(refNo));
                dbOperation.OpenDbConnection();
                bool isUpdated = dbOperation.Update("not_entry", data, wherePart);

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

        public bool UpdateErrorStatus(string refNo)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("error_status", 1);

                string wherePart = String.Format(" reference_no='{0}'", AesCryptography.EncryptToString(refNo));
                dbOperation.OpenDbConnection();
                bool isUpdated = dbOperation.Update("not_entry", data, wherePart);

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

        public bool UpdateErrorMessage(string refNo, string Message)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("error_message", Message);

                string wherePart = String.Format(" reference_no='{0}'", AesCryptography.EncryptToString(refNo));
                dbOperation.OpenDbConnection();
                bool isUpdated = dbOperation.Update("not_entry", data, wherePart);

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
    }
}
