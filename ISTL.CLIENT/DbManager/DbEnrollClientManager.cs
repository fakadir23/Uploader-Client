using ISTL.COMMON;
using ISTL.COMMON.Common;
using ISTL.COMMON.Database;
using ISTL.COMMON.Subscription;
using ISTL.MODELS.DTO.Beneficiary;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.MODELS.DTO.New.Enrollment.CrimeInformation;
using ISTL.MODELS.DTO.New.Enrollment.Other;
using ISTL.MODELS.Request.Beneficiary;
using ISTL.MODELS.Request.Beneficiary.Alternate;
using ISTL.MODELS.Request.Beneficiary.Biometric;
using ISTL.MODELS.Request.Beneficiary.Enums;
using ISTL.PERSOGlobals;
using ISTL.RAB.Entity;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web.Script.Serialization;

namespace ISTL.RAB.DbManager
{
    public class DbEnrollClientManager
    {
        public Logger logger = LogManager.GetCurrentClassLogger();
        private DbOperation dbOperation = null;

        public DbEnrollClientManager()
        {
            dbOperation = new DbOperation(Globals.Database.NAME, Globals.Database.PASSWORD);
        }

        public int AddCrimeInfo(EnrollmentDto obj)
        {
            logger.Debug("\n\n------------------ Check Memory Usage --------------------");
            Process proc = Process.GetCurrentProcess();
            logger.Debug("Memory Usage Before: " + (proc.WorkingSet64 / (1024 * 1024)) + "MB");
            bool isAdded = false;
            try
            {
                Dictionary<string, object> CrimeInfoData = new Dictionary<string, object>();
                CrimeInfoData.Add("reference_no", AesCryptography.EncryptToString(obj?.profile?.referenceNo));
                CrimeInfoData.Add("crime_type", obj?.profile?.crimeInformation?.crimeType);
                CrimeInfoData.Add("criminal_status", obj?.profile?.crimeInformation?.criminalStatus);
                CrimeInfoData.Add("group_or_gang_name", AesCryptography.EncryptToString(obj?.profile?.crimeInformation?.groupOrGangName));

                var jsonIAP = new JavaScriptSerializer().Serialize(obj?.profile?.crimeInformation?.illegalArmsPossession);
                CrimeInfoData.Add("illegal_arms_possession", jsonIAP);

                CrimeInfoData.Add("legal_arms_possession", obj?.profile?.crimeInformation?.legalArmsPossession);
                CrimeInfoData.Add("priority_list_govt", obj?.profile?.crimeInformation?.priorityListGovt);
                CrimeInfoData.Add("details", obj?.profile?.crimeInformation?.details);

                var jsonCrimeZone = new JavaScriptSerializer().Serialize(obj?.profile?.crimeInformation?.crimeZone);
                CrimeInfoData.Add("crime_zone", AesCryptography.EncryptToString(jsonCrimeZone));

                var jsonActivities = new JavaScriptSerializer().Serialize(obj?.profile?.crimeInformation?.activities);
                CrimeInfoData.Add("activities", jsonActivities);

                var crimeHistory = new JavaScriptSerializer().Serialize(obj?.profile?.crimeInformation?.crimeHistorys);
                CrimeInfoData.Add("crime_history", AesCryptography.EncryptToString(crimeHistory));

                CrimeInfoData.Add("warrant_type", AesCryptography.EncryptToString(obj?.profile?.crimeInformation?.warrant?.warrantType));
                CrimeInfoData.Add("warrant_no", AesCryptography.EncryptToString(obj?.profile?.crimeInformation?.warrant?.warrantNo));
                CrimeInfoData.Add("section_no", AesCryptography.EncryptToString(obj?.profile?.crimeInformation?.warrant?.sectionNo));

                var jsonRecoveries = new JavaScriptSerializer().Serialize(obj?.profile?.crimeInformation?.recoveryList);
                CrimeInfoData.Add("recoveries", AesCryptography.EncryptToString(jsonRecoveries));

                string refNoQuery = String.Format("select *from crime_information where reference_no='{0}'", AesCryptography.EncryptToString(obj?.profile?.referenceNo));

                dbOperation.OpenDbConnection();

                string refNoRec = dbOperation.ExecuteScalarReturnStr(refNoQuery);
                if (!string.IsNullOrEmpty(refNoRec))
                {
                    DateTime dtNow = DateTime.Now;
                    CrimeInfoData.Add("updated_at", dtNow);
                    CrimeInfoData.Add("updated_by", Convert.ToInt32(Users.Id));

                    string whereClause = String.Format(" reference_no='{0}'", AesCryptography.EncryptToString(obj?.profile?.referenceNo));
                    isAdded = dbOperation.Update("crime_information", CrimeInfoData, whereClause);
                }
                else
                {
                    DateTime dtNow = DateTime.Now;
                    CrimeInfoData.Add("created_at", dtNow);
                    CrimeInfoData.Add("created_by", Convert.ToInt32(Users.Id));

                    isAdded = dbOperation.InsertData("crime_information", CrimeInfoData);
                }

                if (isAdded)
                {
                    logger.Debug("Successfully added Crime Information.");
                    string sqlGetLastId = "select last_insert_rowid()";
                    int lastId = dbOperation.ExecuteScalarReturnInt(sqlGetLastId);
                    dbOperation.CloseDbConnection();

                    if (lastId > 0) return lastId;
                }
                else
                {
                    logger.Debug("Adding new crime information entry failed! Possibly already exists.");
                    dbOperation.CloseDbConnection();
                }
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.ToString());
                throw x;
            }

            return -1;
        }

        private string enrollHashValue()
        {
            byte[] nickName = null;
            if (StaticData.Enrollment?.profile?.nickName?.Count > 0)
            {
                string nickNameStr = string.Empty;
                for (int i = 0; i < StaticData.Enrollment?.profile?.nickName?.Count; i++)
                {
                    nickNameStr += StaticData.Enrollment.profile.nickName[i];
                }
                nickName = Utils.StringToByteArray(nickNameStr);
            }

            byte[] familyInfo = null;
            if (StaticData.Enrollment?.profile?.familys?.Count > 0)
            {
                string familyInfoStr = string.Empty;
                for (int i = 0; i < StaticData.Enrollment?.profile?.familys?.Count; i++)
                {
                    familyInfoStr += StaticData.Enrollment.profile.familys[i].name + StaticData.Enrollment.profile.familys[i].phone;
                }
                familyInfo = Utils.StringToByteArray(familyInfoStr);
            }

            byte[] eduInfo = null;
            if (StaticData.Enrollment?.profile?.educationalInformations?.Count > 0)
            {
                string eduInfoStr = string.Empty;
                for (int i = 0; i < StaticData.Enrollment?.profile?.educationalInformations?.Count; i++)
                {
                    eduInfoStr += StaticData.Enrollment?.profile?.educationalInformations[i].educationalStatus
                        + StaticData.Enrollment?.profile?.educationalInformations[i].nameOfInstitution
                        + StaticData.Enrollment?.profile?.educationalInformations[i].politicalInvolvement?.ToString()
                        + StaticData.Enrollment?.profile?.educationalInformations[i].remarks;
                }
                eduInfo = Utils.StringToByteArray(eduInfoStr);
            }

            byte[] otherInfo = null;
            if (StaticData.Enrollment?.profile?.otherInformationList?.Count > 0)
            {
                string otherInfoStr = null;
                for (int i = 0; i < StaticData.Enrollment?.profile?.otherInformationList?.Count; i++)
                {
                    otherInfoStr += StaticData.Enrollment?.profile?.otherInformationList[i].key +
                                    StaticData.Enrollment?.profile?.otherInformationList[i].value;
                }
                otherInfo = Utils.StringToByteArray(otherInfoStr);
            }

            byte[] firNo = null;
            byte[] firDate = null;
            byte[] fir = null;
            byte[] firDistrict = null;
            byte[] firUpazila = null;
            string firStr = null;
            if (StaticData.Enrollment?.profile?.attachment?.firList?.Count > 0)
            {
                for (int i = 0; i < StaticData.Enrollment.profile.attachment.firList.Count; i++)
                {
                    firStr += StaticData.Enrollment?.profile?.attachment?.firList[i].fir?.ToString();
                }
                firNo = Utils.StringToByteArray(StaticData.Enrollment?.profile?.attachment?.firList[0].firNo);
                firDate = Utils.StringToByteArray(StaticData.Enrollment?.profile?.attachment?.firList[0].firDate);
                firDistrict = Utils.StringToByteArray(StaticData.Enrollment?.profile?.attachment?.firList[0].district?.ToString());
                firUpazila = Utils.StringToByteArray(StaticData.Enrollment?.profile?.attachment?.firList[0].upozilaOrThana?.ToString());
                firStr = firStr + firNo + firDate + firDistrict + firUpazila;
                fir = Utils.StringToByteArray(firStr);
            }

            byte[] complain = null;
            string complainStr = null;
            if (StaticData.Enrollment?.profile?.attachment?.complaintList?.Count > 0)
            {
                for (int i = 0; i < StaticData.Enrollment.profile.attachment.complaintList.Count; i++)
                {
                    complainStr += StaticData.Enrollment?.profile?.attachment?.complaintList[i].complaint?.ToString();
                }
                complain = Utils.StringToByteArray(complainStr);
            }

            byte[] seizure = null;
            string seizureStr = null;
            if (StaticData.Enrollment?.profile?.attachment?.seizureList?.Count > 0)
            {
                for (int i = 0; i < StaticData.Enrollment.profile.attachment.seizureList.Count; i++)
                {
                    seizureStr += StaticData.Enrollment?.profile?.attachment?.seizureList[i].seizure?.ToString();
                }
                seizure = Utils.StringToByteArray(seizureStr);
            }

            byte[] recoveryData = null;
            if (StaticData.Enrollment?.profile?.crimeInformation?.recoveryList?.Count > 0)
            {
                string valueStr = string.Empty;
                for (int i = 0; i < StaticData.Enrollment?.profile?.crimeInformation?.recoveryList?.Count; i++)
                {
                    valueStr += StaticData.Enrollment?.profile?.crimeInformation?.recoveryList[i]?.recoveryType
                        + StaticData.Enrollment?.profile?.crimeInformation?.recoveryList[i]?.recoveryItemName
                        + StaticData.Enrollment?.profile?.crimeInformation?.recoveryList[i]?.amount;
                }
                recoveryData = Utils.StringToByteArray(valueStr);
            }

            List<byte[]> hashList = new List<byte[]>();
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.referenceNo));
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.fullName));

            hashList.Add(StaticData.Enrollment?.profile?.biometric?.photo?.photo);

            hashList.Add(StaticData.Enrollment?.profile?.biometric?.fingerprint?.rt);
            hashList.Add(StaticData.Enrollment?.profile?.biometric?.fingerprint?.ri);
            hashList.Add(StaticData.Enrollment?.profile?.biometric?.fingerprint?.rm);
            hashList.Add(StaticData.Enrollment?.profile?.biometric?.fingerprint?.rr);
            hashList.Add(StaticData.Enrollment?.profile?.biometric?.fingerprint?.rs);

            hashList.Add(StaticData.Enrollment?.profile?.biometric?.fingerprint?.lt);
            hashList.Add(StaticData.Enrollment?.profile?.biometric?.fingerprint?.li);
            hashList.Add(StaticData.Enrollment?.profile?.biometric?.fingerprint?.lm);
            hashList.Add(StaticData.Enrollment?.profile?.biometric?.fingerprint?.lr);
            hashList.Add(StaticData.Enrollment?.profile?.biometric?.fingerprint?.ls);

            hashList.Add(StaticData.Enrollment?.profile?.biometric?.iris?.right);
            hashList.Add(StaticData.Enrollment?.profile?.biometric?.iris?.left);

            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.gender));
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.height?.ft?.ToString() + StaticData.Enrollment?.profile?.height?.inch?.ToString()));
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.nid));
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.dateOfBirth));

            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.presentAddress?.district?.ToString()));
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.presentAddress?.upazila?.ToString()));
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.presentAddress?.union?.ToString()));
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.presentAddress?.villageHouseRoadNo));

            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.permanentAddress?.district?.ToString()));
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.permanentAddress?.upazila?.ToString()));
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.permanentAddress?.union?.ToString()));
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.permanentAddress?.villageHouseRoadNo));

            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.foreignAddress?.zipOrPostCode));
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.foreignAddress?.town));
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.foreignAddress?.state));
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.foreignAddress?.country?.ToString()));

            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.crimeInformation?.groupOrGangName));
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.crimeInformation?.crimeType?.ToString()));

            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.unit?.ToString()));
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.subUnit?.ToString()));

            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.crimeInformation?.crimeZone?.district?.ToString()));
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.crimeInformation?.crimeZone?.upozilaOrThana?.ToString()));
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.crimeInformation?.crimeZone?.union?.ToString()));

            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.identificationMark));
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.arrestDate));
            hashList.Add(nickName);

            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.nationality?.id?.ToString()));
            if (StaticData.Enrollment?.profile?.mobile?.Count > 0)
            {
                hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.mobile[0]));
            }
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.maritalStatus));
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.occupation));
            hashList.Add(familyInfo);
            hashList.Add(eduInfo);
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.investigatingOfficerName));
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.investigatingOfficerMobile));
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.investigatingOfficerBPNumber));
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.politicalGroup));
            if (StaticData.Enrollment?.profile?.crimeInformation?.crimeHistorys?.Count > 0)
            {
                hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.crimeInformation?.crimeHistorys[0].dateOfCrime));
            }
            hashList.Add(otherInfo);
            hashList.Add(firNo);
            hashList.Add(firDate);
            hashList.Add(fir);
            hashList.Add(firDistrict);
            hashList.Add(firUpazila);
            hashList.Add(complain);
            hashList.Add(seizure);
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.religion));
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.age?.ToString()));
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.crimeInformation?.warrant?.warrantType));
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.crimeInformation?.warrant?.warrantNo));
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.crimeInformation?.warrant?.sectionNo));
            hashList.Add(recoveryData);
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.arrestedBy));
            hashList.Add(Utils.StringToByteArray(StaticData.Enrollment?.profile?.iorank));

            string enrollHash = GenerateSecureHash.CreateSha1Hash(hashList);
            hashList = null;

            return enrollHash;
        }

        private bool CheckFieldsEmpty(ProfileDto obj)
        {
            if (string.IsNullOrEmpty(obj.fullName)
                && obj.nickName?.Count == 0
                && string.IsNullOrEmpty(obj.gender)
                && string.IsNullOrEmpty(obj.dateOfBirth)
                && string.IsNullOrEmpty(obj.religion)
                && string.IsNullOrEmpty(obj.maritalStatus)
                && string.IsNullOrEmpty(obj.occupation)
                && obj.nationality?.id <= 0
                && (obj.height?.ft == 0 && obj.height?.inch == 0)
                && string.IsNullOrEmpty(obj.nid)
                && string.IsNullOrEmpty(obj.identificationMark)
                && obj.familys?.Count == 0
                && obj.presentAddress?.district <= 0
                && string.IsNullOrEmpty(obj.presentAddress?.villageHouseRoadNo)
                && obj.permanentAddress?.district <= 0
                && string.IsNullOrEmpty(obj.permanentAddress?.villageHouseRoadNo)
                && string.IsNullOrEmpty(obj.foreignAddress?.zipOrPostCode)
                && string.IsNullOrEmpty(obj.foreignAddress?.town)
                && string.IsNullOrEmpty(obj.foreignAddress?.state)
                && obj.foreignAddress?.country <= 0
                && string.IsNullOrEmpty(obj.foreignAddress?.socialSecurityNo)
                && obj.familys == null
                && obj.unit == null
                && obj.biometric?.photo?.photo == null
                && obj.biometric?.fingerprint?.lt == null
                && obj.biometric?.fingerprint?.li == null
                && obj.biometric?.fingerprint?.lm == null
                && obj.biometric?.fingerprint?.lr == null
                && obj.biometric?.fingerprint?.ls == null
                && obj.biometric?.fingerprint?.rt == null
                && obj.biometric?.fingerprint?.ri == null
                && obj.biometric?.fingerprint?.rm == null
                && obj.biometric?.fingerprint?.rr == null
                && obj.biometric?.fingerprint?.rs == null
                && obj.biometric?.iris?.left == null
                && obj.biometric?.iris?.right == null
                && string.IsNullOrEmpty(obj.politicalGroup)
                && string.IsNullOrEmpty(obj.investigatingOfficerName)
                && string.IsNullOrEmpty(obj.investigatingOfficerMobile)
                && string.IsNullOrEmpty(obj.investigatingOfficerBPNumber)
                && obj.subUnit == null
                && obj.attachment?.complaintList?.Count == 0
                && obj.attachment?.firList?.Count == 0
                && obj.attachment?.seizureList?.Count == 0
                && obj.otherInformationList?.Count == 0
                && string.IsNullOrEmpty(obj.nid)
                && string.IsNullOrEmpty(obj.crimeInformation?.warrant?.warrantType)
                && string.IsNullOrEmpty(obj.crimeInformation?.warrant?.warrantNo)
                && string.IsNullOrEmpty(obj.crimeInformation?.warrant?.sectionNo)
                && string.IsNullOrEmpty(obj.arrestedBy)
                && string.IsNullOrEmpty(obj.iorank))
            {
                return true;
            }
            return false;
        }

        public bool AddCriminalProfile(EnrollmentDto obj, int status)
        {
            bool AllInputEmpty = CheckFieldsEmpty(obj?.profile);
            if (AllInputEmpty) return false;

            logger.Debug("\n\n------------------ Check Memory Usage --------------------");
            Process proc = Process.GetCurrentProcess();
            logger.Debug("Memory Usage Before: " + (proc.WorkingSet64 / (1024 * 1024)) + "MB");
            bool isAdded = false;
            try
            {
                if (!string.IsNullOrEmpty(obj?.profile?.id) && status == 0)
                {
                    // Will not be draft saving already uploaded profile so that already existing data cannot be modified
                    return false;
                }

                byte[] criminalPhoto = StaticData.Enrollment?.profile?.biometric?.photo?.photo;

                byte[] rt = StaticData.Enrollment?.profile?.biometric?.fingerprint?.rt;
                byte[] ri = StaticData.Enrollment?.profile?.biometric?.fingerprint?.ri;
                byte[] rm = StaticData.Enrollment?.profile?.biometric?.fingerprint?.rm;
                byte[] rr = StaticData.Enrollment?.profile?.biometric?.fingerprint?.rr;
                byte[] rs = StaticData.Enrollment?.profile?.biometric?.fingerprint?.rs;

                byte[] lt = StaticData.Enrollment?.profile?.biometric?.fingerprint?.lt;
                byte[] li = StaticData.Enrollment?.profile?.biometric?.fingerprint?.li;
                byte[] lm = StaticData.Enrollment?.profile?.biometric?.fingerprint?.lm;
                byte[] lr = StaticData.Enrollment?.profile?.biometric?.fingerprint?.lr;
                byte[] ls = StaticData.Enrollment?.profile?.biometric?.fingerprint?.ls;

                byte[] rightIris = StaticData.Enrollment?.profile?.biometric?.iris?.right;
                byte[] leftIris = StaticData.Enrollment?.profile?.biometric?.iris?.left;

                proc = Process.GetCurrentProcess();
                logger.Debug("Memory Usage After Hash: " + (proc.WorkingSet64 / (1024 * 1024)) + "MB");

                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("reference_no", AesCryptography.EncryptToString(obj?.profile?.referenceNo));
                data.Add("full_name", AesCryptography.EncryptToString(obj?.profile?.fullName));
                data.Add("status", status);
                data.Add("error_status", Globals.ErrorState.NOT_ERROR);

                // Photo
                data.Add("criminal_photo", AesCryptography.EncryptToByte(criminalPhoto));

                // FP 
                data.Add("fp_rt", AesCryptography.EncryptToByte(rt));
                data.Add("fp_ri", AesCryptography.EncryptToByte(ri));
                data.Add("fp_rm", AesCryptography.EncryptToByte(rm));
                data.Add("fp_rr", AesCryptography.EncryptToByte(rr));
                data.Add("fp_rs", AesCryptography.EncryptToByte(rs));

                data.Add("fp_lt", AesCryptography.EncryptToByte(lt));
                data.Add("fp_li", AesCryptography.EncryptToByte(li));
                data.Add("fp_lm", AesCryptography.EncryptToByte(lm));
                data.Add("fp_lr", AesCryptography.EncryptToByte(lr));
                data.Add("fp_ls", AesCryptography.EncryptToByte(ls));

                // Iris
                data.Add("right_iris", AesCryptography.EncryptToByte(rightIris));
                data.Add("left_iris", AesCryptography.EncryptToByte(leftIris));

                //DateTime dtNow = DateTime.Now;

                //data.Add("created_at", dtNow);
                //data.Add("created_by", Users.Id);
                data.Add("uploaded_by", Users.Id);

                string enrollHash = enrollHashValue();
                StaticData.Enrollment.profile.hash = enrollHash;
                data.Add("hash", enrollHash);

                if (obj?.profile?.nickName != null)
                {
                    string NickNames = null;
                    for (int i = 0; i < obj.profile.nickName.Count; i++)
                    {
                        NickNames += obj.profile.nickName[i];
                        if (i != obj.profile.nickName.Count - 1)
                        {
                            NickNames += ",";
                        }
                    }
                    data.Add("nick_name", AesCryptography.EncryptToString(NickNames));
                }

                data.Add("criminal_name", obj?.profile?.criminalName);

                if (obj?.profile?.gender == "Male") data.Add("gender", 0);
                if (obj?.profile?.gender == "Female") data.Add("gender", 1);
                if (obj?.profile?.gender == "Other") data.Add("gender", 2);

                data.Add("date_of_birth", obj?.profile?.dateOfBirth);
                data.Add("region_of_birth", obj?.profile?.regionOfBirth);

                if (obj?.profile?.religion == "muslim") data.Add("religion", 0);
                if (obj?.profile?.religion == "hindu") data.Add("religion", 1);
                if (obj?.profile?.religion == "christian") data.Add("religion", 2);
                if (obj?.profile?.religion == "buddhist") data.Add("religion", 3);

                if (obj?.profile?.bloodGroup == "A_Plus") data.Add("blood_group", 0);
                if (obj?.profile?.bloodGroup == "A_Minus") data.Add("blood_group", 1);
                if (obj?.profile?.bloodGroup == "B_Plus") data.Add("blood_group", 2);
                if (obj?.profile?.bloodGroup == "B_Minus") data.Add("blood_group", 3);
                if (obj?.profile?.bloodGroup == "AB_Plus") data.Add("blood_group", 4);
                if (obj?.profile?.bloodGroup == "AB_Minus") data.Add("blood_group", 5);
                if (obj?.profile?.bloodGroup == "O_Plus") data.Add("blood_group", 6);
                if (obj?.profile?.bloodGroup == "O_Minus") data.Add("blood_group", 7);

                if (obj?.profile?.maritalStatus == "Single") data.Add("marital_status", 0);
                if (obj?.profile?.maritalStatus == "Married") data.Add("marital_status", 1);
                if (obj?.profile?.maritalStatus == "Widowed") data.Add("marital_status", 2);
                if (obj?.profile?.maritalStatus == "Divorced") data.Add("marital_status", 3);

                data.Add("occupation", (!string.IsNullOrEmpty(obj?.profile?.occupation) ? AesCryptography.EncryptToString(obj.profile?.occupation) : null));

                var jsonNationality = new JavaScriptSerializer().Serialize(obj?.profile?.nationality);
                data.Add("nationality", AesCryptography.EncryptToString(jsonNationality));

                data.Add("nid", AesCryptography.EncryptToString(obj?.profile?.nid));

                var jsonHeight = new JavaScriptSerializer().Serialize(obj?.profile?.height);
                data.Add("height", AesCryptography.EncryptToString(jsonHeight));

                var jsonWeight = new JavaScriptSerializer().Serialize(obj?.profile?.weight);
                data.Add("weight", jsonWeight);

                if (obj?.profile?.eyeColor == "Black") data.Add("eye_color", 0);
                else if (obj?.profile?.eyeColor == "Blue") data.Add("eye_color", 1);
                else if (obj?.profile?.eyeColor == "Brown") data.Add("eye_color", 2);

                data.Add("identification_mark", AesCryptography.EncryptToString(obj?.profile?.identificationMark));
                if (obj?.profile?.mobile != null)
                {
                    string mobile = null;
                    for (int i = 0; i < obj.profile.mobile.Count; i++)
                    {
                        mobile += obj.profile.mobile[i];
                        if (i != obj.profile.mobile.Count - 1)
                        {
                            mobile += ", ";
                        }
                    }
                    data.Add("mobile", AesCryptography.EncryptToString(mobile));
                }

                var jsonEdu = new JavaScriptSerializer().Serialize(obj?.profile?.educationalInformations);
                data.Add("education_information", AesCryptography.EncryptToString(jsonEdu));

                //data.Add("battalion", obj?.profile?.crimeInformation?.crimeZone?.unit);
                data.Add("battalion", obj?.profile?.unit);
                data.Add("sub_unit", obj?.profile?.subUnit);

                //if (status != Globals.RecordState.NEW)
                //{
                int[] result = AddAddressInfo(obj);
                if (result != null)
                {
                    data.Add("present_address_id", result[0]);
                    data.Add("permanent_address_id", result[1]);
                }
                //}

                if (obj?.profile?.foreignAddress != null)
                {
                    var jsonForeignAddress = new JavaScriptSerializer().Serialize(obj?.profile?.foreignAddress);
                    data.Add("foreigner_address", AesCryptography.EncryptToString(jsonForeignAddress));
                }

                if (StaticData.Enrollment.profile.crimeInformation != null)
                {
                    int lastInsetId = AddCrimeInfo(StaticData.Enrollment);
                    if (lastInsetId > 0) data.Add("crime_information_id", lastInsetId);
                }

                if (obj?.profile?.firs != null)
                {
                    if (obj?.profile?.firs.Count > 0)
                    {
                        //var jsonFir = new JavaScriptSerializer().Serialize(obj?.profile?.firs);
                        //data.Add("fir", jsonFir);
                        AddAttachment(obj?.profile, Globals.AttachmentType.FIR);
                    }
                }

                if (obj?.profile?.complains != null)
                {
                    if (obj?.profile?.complains.Count > 0)
                    {
                        //var jsonFir = new JavaScriptSerializer().Serialize(obj?.profile?.complains);
                        //data.Add("complain", jsonFir);
                        AddAttachment(obj?.profile, Globals.AttachmentType.COMPLAIN);
                    }
                }

                if (obj?.profile?.seizures != null)
                {
                    if (obj?.profile?.seizures.Count > 0)
                    {
                        //var jsonFir = new JavaScriptSerializer().Serialize(obj?.profile?.seizures);
                        //data.Add("seizure", jsonFir);
                        AddAttachment(obj?.profile, Globals.AttachmentType.SEIZURE);
                    }
                }

                if (obj?.profile?.familys != null)
                {
                    if (obj?.profile?.familys.Count > 0)
                    {
                        var jsonFamily = new JavaScriptSerializer().Serialize(obj?.profile?.familys);
                        data.Add("family_information", AesCryptography.EncryptToString(jsonFamily));
                    }
                }

                if (obj?.profile?.arrestInfos != null)
                {
                    if (obj?.profile?.arrestInfos.Count > 0)
                    {
                        var jsonFir = new JavaScriptSerializer().Serialize(obj?.profile?.arrestInfos);
                        data.Add("arrest_info", AesCryptography.EncryptToString(jsonFir));
                    }
                }

                data.Add("arrest_date", obj?.profile?.arrestDate);

                if (obj?.profile?.politicalGroup == "AwamiLeague") data.Add("political_group", 1);
                else if (obj?.profile?.politicalGroup == "BNP") data.Add("political_group", 2);
                else if (obj?.profile?.politicalGroup == "JatiyaParty_Ershad") data.Add("political_group", 3);
                else if (obj?.profile?.politicalGroup == "WorkersPartyofBangladesh") data.Add("political_group", 4);
                else if (obj?.profile?.politicalGroup == "JatiyaSamajtantrikDal") data.Add("political_group", 5);
                else if (obj?.profile?.politicalGroup == "BikalpaDharaBangladesh") data.Add("political_group", 6);
                else if (obj?.profile?.politicalGroup == "GanoForum") data.Add("political_group", 7);
                else if (obj?.profile?.politicalGroup == "JatiyaParty_Manju") data.Add("political_group", 8);
                else if (obj?.profile?.politicalGroup == "BangladesTarikatFederation") data.Add("political_group", 9);

                data.Add("investigatingofficer_name", AesCryptography.EncryptToString(obj?.profile?.investigatingOfficerName));
                data.Add("investigatingofficer_bp", AesCryptography.EncryptToString(obj?.profile?.investigatingOfficerBPNumber));
                data.Add("investigatingofficer_mobile", AesCryptography.EncryptToString(obj?.profile?.investigatingOfficerMobile));

                if (obj?.profile?.otherInformationList != null)
                {
                    if (obj?.profile?.otherInformationList.Count > 0)
                    {
                        var json = new JavaScriptSerializer().Serialize(obj?.profile?.otherInformationList);
                        data.Add("other_information", AesCryptography.EncryptToString(json));
                    }
                }

                //data.Add("crime_zone_district", obj?.profile?.crimeZoneDistrict);
                //data.Add("crime_zone_upazila", obj?.profile?.crimeZoneUpazila);

                data.Add("criminal_id", AesCryptography.EncryptToString(obj?.profile?.id));

                data.Add("arrested_by", AesCryptography.EncryptToString(obj?.profile?.arrestedBy));
                data.Add("io_rank", AesCryptography.EncryptToString(obj?.profile?.iorank));

                proc = Process.GetCurrentProcess();
                logger.Debug("Memory Usage After EncryptToString on: " + (proc.WorkingSet64 / (1024 * 1024)) + "MB");

                string refNoQuery = String.Format("select * from criminal_profile where reference_no='{0}'", AesCryptography.EncryptToString(obj?.profile?.referenceNo));

                dbOperation.OpenDbConnection();
                string refNoRec = dbOperation.ExecuteScalarReturnStr(refNoQuery);

                if (!string.IsNullOrEmpty(refNoRec))
                {
                    DateTime dtNow = DateTime.Now;

                    data.Add("updated_at", dtNow.ToString("yyyy-MM-ddTHH:mm:ss.fffK"));
                    data.Add("updated_by", Users.Id);
                    string whereClause = String.Format(" reference_no='{0}'", AesCryptography.EncryptToString(obj?.profile?.referenceNo));
                    isAdded = dbOperation.Update("criminal_profile", data, whereClause);
                }
                else
                {
                    DateTime dtNow = DateTime.Now;

                    data.Add("created_at", dtNow.ToString("yyyy-MM-ddTHH:mm:ss.fffK"));
                    data.Add("created_by", Users.Id);
                    isAdded = dbOperation.InsertData("criminal_profile", data);
                }
                dbOperation.CloseDbConnection();

                if (isAdded)
                {
                    logger.Debug("Successfully added Criminal Profile.");
                }
                else
                {
                    logger.Debug("Adding new criminal profile entry failed! Possibly already exists.");
                }
                proc = Process.GetCurrentProcess();
                logger.Debug("Memory Usage Insert Data To Db: " + (proc.WorkingSet64 / (1024 * 1024)) + "MB");
                logger.Debug("--------------------------------------\n\n");

            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.ToString());
                throw x;
            }

            CounterDraftSubject counterDraftStatus = (CounterDraftSubject)SubjectFactory.GetInstance().GetSubject(Globals.Common.COUNTER_DRAFT_NAME);
            //counterDraftStatus.Count = this.GetEnrolledDraftCount() + this.GetSpecialEnrolledDraftCount();
            counterDraftStatus.Count = this.GetEnrolledDraftCount();
            counterDraftStatus.Notify();

            CounterSubject counterStatus = (CounterSubject)SubjectFactory.GetInstance().GetSubject(Globals.Common.COUNTER_NAME);
            counterStatus.Count = this.GetTodayEnrollCount();
            counterStatus.Notify();

            CounterPendingSubject counterPendingSubject = (CounterPendingSubject)SubjectFactory.GetInstance().GetSubject(Globals.Common.COUNTER_PENDING_NAME);
            counterPendingSubject.Count = this.GetNormalUploadPendingCount();
            counterPendingSubject.Notify();

            CounterErrorSubject counterErrorStatus = (CounterErrorSubject)SubjectFactory.GetInstance().GetSubject(Globals.Common.COUNTER_ERROR_NAME);
            //counterErrorStatus.Count = this.GetEnrolledErrorCount() + this.GetSpecialEnrolledErrorCount();
            counterErrorStatus.Count = this.GetEnrolledErrorCount();
            counterErrorStatus.Notify();

            return isAdded;
        }

        public void DeleteAttachment(ProfileDto obj, int value, int attachmentNo)
        {
            if (value == Globals.AttachmentType.COMPLAIN)
            {
                try
                {
                    string wherePart1 = string.Format(" reference_no = '{0}' AND attachment_type='{1}' and attachment_no='{2}'",
                        AesCryptography.EncryptToString(obj?.referenceNo), Globals.AttachmentType.COMPLAIN, attachmentNo);
                    string refNoQuery1 = String.Format("select * from attachment where " + wherePart1);
                    dbOperation.OpenDbConnection();
                    string refNoRec1 = dbOperation.ExecuteScalarReturnStr(refNoQuery1);
                    if (!string.IsNullOrEmpty(refNoRec1))
                    {
                        dbOperation.Delete("attachment", wherePart1);
                        string sql = "UPDATE attachment SET attachment_no = (attachment_no - 1) WHERE attachment_no>" + attachmentNo +
                            " AND attachment_type=" + Globals.AttachmentType.COMPLAIN + " AND reference_no='" + AesCryptography.EncryptToString(obj?.referenceNo) + "'";
                        dbOperation.ExecuteQuery(sql);
                    }
                    dbOperation.CloseDbConnection();
                }
                catch (Exception x)
                {
                    logger.Error("Error while deleting complain for reference no=" + obj?.referenceNo + "\n" + x.ToString());
                }
            }
            else if (value == Globals.AttachmentType.FIR)
            {
                try
                {
                    string wherePart1 = string.Format(" reference_no = '{0}' AND attachment_type='{1}' and attachment_no='{2}'",
                        AesCryptography.EncryptToString(obj?.referenceNo), Globals.AttachmentType.FIR, attachmentNo);
                    string refNoQuery1 = String.Format("select * from attachment where " + wherePart1);
                    dbOperation.OpenDbConnection();
                    string refNoRec1 = dbOperation.ExecuteScalarReturnStr(refNoQuery1);
                    if (!string.IsNullOrEmpty(refNoRec1))
                    {
                        dbOperation.Delete("attachment", wherePart1);
                        string sql = "UPDATE attachment SET attachment_no = (attachment_no - 1) WHERE attachment_no>" + attachmentNo
                            + " AND attachment_type=" + Globals.AttachmentType.FIR + " AND reference_no='" + AesCryptography.EncryptToString(obj?.referenceNo) + "'";
                        dbOperation.ExecuteQuery(sql);
                    }
                    dbOperation.CloseDbConnection();
                }
                catch (Exception x)
                {
                    logger.Error("Error while deleting FIR for reference no=" + obj?.referenceNo + "\n" + x.ToString());
                }
            }
            else if (value == Globals.AttachmentType.SEIZURE)
            {
                try
                {
                    string wherePart1 = string.Format(" reference_no = '{0}' AND attachment_type='{1}' and attachment_no='{2}'",
                        AesCryptography.EncryptToString(obj?.referenceNo), Globals.AttachmentType.SEIZURE, attachmentNo);
                    string refNoQuery1 = String.Format("select * from attachment where " + wherePart1);
                    dbOperation.OpenDbConnection();
                    string refNoRec1 = dbOperation.ExecuteScalarReturnStr(refNoQuery1);
                    if (!string.IsNullOrEmpty(refNoRec1))
                    {
                        dbOperation.Delete("attachment", wherePart1);
                        string sql = "UPDATE attachment SET attachment_no = (attachment_no - 1) WHERE attachment_no>" + attachmentNo
                            + " AND attachment_type=" + Globals.AttachmentType.SEIZURE + " AND reference_no='" + AesCryptography.EncryptToString(obj?.referenceNo) + "'";
                        dbOperation.ExecuteQuery(sql);
                    }
                    dbOperation.CloseDbConnection();
                }
                catch (Exception x)
                {
                    logger.Error("Error while deleting seizure for reference no=" + obj?.referenceNo + "\n" + x.ToString());
                }
            }
        }

        public void UpdateAttachment(string referenceNo, string columnName, string value, int intValue)
        {
            try
            {
                string wherePart = string.Format(" reference_no = '{0}' AND attachment_type='{1}'", AesCryptography.EncryptToString(referenceNo), Globals.AttachmentType.FIR);
                string refNoQuery1 = String.Format("select * from attachment where " + wherePart);
                dbOperation.OpenDbConnection();
                string refNoRec1 = dbOperation.ExecuteScalarReturnStr(refNoQuery1);
                if (!string.IsNullOrEmpty(refNoRec1))
                {
                    string sql = string.Empty;
                    if (intValue >= 0)
                    {
                        sql = String.Format("UPDATE attachment SET " + columnName + "='" + intValue + "' where {0}", wherePart);
                    }
                    else
                    {
                        sql = String.Format("UPDATE attachment SET " + columnName + "='" + AesCryptography.EncryptToString(value) + "' where {0}", wherePart);
                    }
                    dbOperation.ExecuteQuery(sql);
                }
                dbOperation.CloseDbConnection();
            }
            catch { }
        }

        public void AddAttachment(ProfileDto obj, int value)
        {
            try
            {
                string wherePart1 = string.Format(" reference_no = '{0}';", AesCryptography.EncryptToString(obj?.referenceNo));
                string refNoQuery1 = String.Format("select * from criminal_profile where " + wherePart1);
                dbOperation.OpenDbConnection();
                string refNoRec1 = dbOperation.ExecuteScalarReturnStr(refNoQuery1);
                dbOperation.CloseDbConnection();
                if (string.IsNullOrEmpty(refNoRec1))
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("reference_no", AesCryptography.EncryptToString(obj?.referenceNo));
                    data.Add("status", Globals.RecordState.DRAFT);
                    data.Add("error_status", Globals.ErrorState.NOT_ERROR);
                    data.Add("hash", enrollHashValue());
                    DateTime dtNow = DateTime.Now;
                    data.Add("created_at", dtNow.ToString("yyyy-MM-ddTHH:mm:ss.fffK"));
                    data.Add("created_by", Users.Id);
                    data.Add("uploaded_by", Users.Id);
                    if (!string.IsNullOrEmpty(obj?.id)) data.Add("criminal_id", AesCryptography.EncryptToString(obj?.id));
                    dbOperation.OpenDbConnection();
                    dbOperation.InsertData("criminal_profile", data);
                    dbOperation.CloseDbConnection();
                }
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Adding criminal profile while uploading attachment failed " + x.ToString());
                throw x;
            }

            if (value == Globals.AttachmentType.COMPLAIN)
            {
                try
                {
                    //string wherePart1 = string.Format(" reference_no = '{0}' AND attachment_type='{1}'", obj?.referenceNo, Globals.AttachmentType.COMPLAIN);
                    //string refNoQuery1 = String.Format("select * from attachment where " + wherePart1);
                    //dbOperation.OpenDbConnection();
                    //string refNoRec1 = dbOperation.ExecuteScalarReturnStr(refNoQuery1);
                    //if (!string.IsNullOrEmpty(refNoRec1))   dbOperation.Delete("attachment", wherePart1);
                    //dbOperation.CloseDbConnection();

                    for (int i = 0; i < obj.complains.Count; i++)
                    {
                        if (obj.complains[i].complaint != null)
                        {
                            Dictionary<string, object> attachmentData = new Dictionary<string, object>();

                            attachmentData.Add("reference_no", AesCryptography.EncryptToString(obj?.referenceNo));
                            attachmentData.Add("attachment_type", Globals.AttachmentType.COMPLAIN);
                            attachmentData.Add("attachment_no", (i + 1));
                            attachmentData.Add("extension", AesCryptography.EncryptToString(obj.complains[i].extension));
                            attachmentData.Add("content_type", AesCryptography.EncryptToString(obj.complains[i].contentType));
                            attachmentData.Add("attachment_byte", AesCryptography.EncryptToByte(obj.complains[i].complaint));

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
                    //string wherePart1 = string.Format(" reference_no = '{0}' AND attachment_type='{1}'", obj?.referenceNo, Globals.AttachmentType.FIR);
                    //string refNoQuery1 = String.Format("select * from attachment where " + wherePart1);
                    //dbOperation.OpenDbConnection();
                    //string refNoRec1 = dbOperation.ExecuteScalarReturnStr(refNoQuery1);
                    //if (!string.IsNullOrEmpty(refNoRec1)) dbOperation.Delete("attachment", wherePart1);
                    //dbOperation.CloseDbConnection();

                    for (int i = 0; i < obj.firs.Count; i++)
                    {
                        if (obj.firs[i].fir != null)
                        {
                            Dictionary<string, object> attachmentData = new Dictionary<string, object>();

                            attachmentData.Add("reference_no", AesCryptography.EncryptToString(obj?.referenceNo));
                            attachmentData.Add("attachment_type", Globals.AttachmentType.FIR);
                            attachmentData.Add("attachment_no", (i + 1));
                            attachmentData.Add("extension", AesCryptography.EncryptToString(obj.firs[i].extension));
                            attachmentData.Add("content_type", AesCryptography.EncryptToString(obj.firs[i].contentType));
                            attachmentData.Add("attachment_byte", AesCryptography.EncryptToByte(obj.firs[i].fir));
                            attachmentData.Add("fir_date", AesCryptography.EncryptToString(obj.firs[i].firDate));
                            attachmentData.Add("fir_no", AesCryptography.EncryptToString(obj.firs[i].firNo));
                            attachmentData.Add("fir_district", obj.firs[i].district);
                            attachmentData.Add("fir_upazila", obj.firs[i].upozilaOrThana);

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
                    //string wherePart1 = string.Format(" reference_no = '{0}' AND attachment_type='{1}'", obj?.referenceNo, Globals.AttachmentType.SEIZURE);
                    //string refNoQuery1 = String.Format("select * from attachment where " + wherePart1);
                    //dbOperation.OpenDbConnection();
                    //string refNoRec1 = dbOperation.ExecuteScalarReturnStr(refNoQuery1);
                    //if (!string.IsNullOrEmpty(refNoRec1)) dbOperation.Delete("attachment", wherePart1);
                    //dbOperation.CloseDbConnection();

                    for (int i = 0; i < obj.seizures.Count; i++)
                    {
                        if (obj.seizures[i].seizure != null)
                        {
                            Dictionary<string, object> attachmentData = new Dictionary<string, object>();

                            attachmentData.Add("reference_no", AesCryptography.EncryptToString(obj?.referenceNo));
                            attachmentData.Add("attachment_type", Globals.AttachmentType.SEIZURE);
                            attachmentData.Add("attachment_no", (i + 1));
                            attachmentData.Add("extension", AesCryptography.EncryptToString(obj.seizures[i].extension));
                            attachmentData.Add("content_type", AesCryptography.EncryptToString(obj.seizures[i].contentType));
                            attachmentData.Add("attachment_byte", AesCryptography.EncryptToByte(obj.seizures[i].seizure));

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

        private int[] AddAddressInfo(EnrollmentDto obj)
        {
            bool isAdded = false;

            int[] presentPermanentId = new int[2] { -1, -1 };

            try
            {
                Dictionary<string, object> AddressData = new Dictionary<string, object>();
                if (obj?.profile?.presentAddress != null)
                {
                    AddressData.Add("reference_no", AesCryptography.EncryptToString(obj?.profile?.referenceNo));
                    AddressData.Add("district", obj?.profile?.presentAddress?.district);
                    AddressData.Add("upazila", obj?.profile?.presentAddress?.upazila);
                    AddressData.Add("union_or_ward", obj?.profile?.presentAddress?.union);
                    AddressData.Add("village_house_road_no", AesCryptography.EncryptToString(obj?.profile?.presentAddress?.villageHouseRoadNo));

                    DateTime dtNow = DateTime.Now;
                    AddressData.Add("created_at", dtNow);
                    AddressData.Add("created_by", Convert.ToInt32(Users.Id));
                }

                if (AddressData.Count <= 0) return null;

                dbOperation.OpenDbConnection();

                int presentAddressId = 0;
                string sqlPresentAddressId = "SELECT present_address_id FROM criminal_profile where reference_no='" + AesCryptography.EncryptToString(obj?.profile?.referenceNo) + "';";
                DataTable dataTable = dbOperation.GetDataTable(sqlPresentAddressId);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    presentAddressId = dataRow["present_address_id"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["present_address_id"]) : 0;
                }
                if (presentAddressId > 0)
                {
                    string wherePart = "reference_no='" + AesCryptography.EncryptToString(obj?.profile?.referenceNo) + "' AND id=" + presentAddressId + ";";
                    isAdded = dbOperation.Update("address", AddressData, wherePart);
                    presentPermanentId[0] = Convert.ToInt32(presentAddressId);
                    dbOperation.CloseDbConnection();
                }
                else
                {
                    isAdded = dbOperation.InsertData("address", AddressData);
                    if (isAdded)
                    {
                        logger.Debug("Successfully added present address.");

                        string sqlGetLastId = "select last_insert_rowid();";
                        int? lastId = dbOperation.ExecuteScalarReturnInt(sqlGetLastId);
                        dbOperation.CloseDbConnection();
                        if (lastId != null) presentPermanentId[0] = Convert.ToInt32(lastId);
                    }
                    else
                    {
                        logger.Debug("Adding new address entry failed! Possibly already exists.");
                        dbOperation.CloseDbConnection();
                    }
                }
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.ToString());
                throw x;
            }

            try
            {
                Dictionary<string, object> AddressData = new Dictionary<string, object>();
                if (obj?.profile?.permanentAddress != null)
                {
                    AddressData.Add("reference_no", AesCryptography.EncryptToString(obj?.profile?.referenceNo));
                    AddressData.Add("district", obj?.profile?.permanentAddress?.district);
                    AddressData.Add("upazila", obj?.profile?.permanentAddress?.upazila);
                    AddressData.Add("union_or_ward", obj?.profile?.permanentAddress?.union);
                    AddressData.Add("village_house_road_no", AesCryptography.EncryptToString(obj?.profile?.permanentAddress?.villageHouseRoadNo));

                    DateTime dtNow = DateTime.Now;
                    AddressData.Add("created_at", dtNow);
                    AddressData.Add("created_by", Convert.ToInt32(Users.Id));

                    dbOperation.OpenDbConnection();

                    int permanentAddressId = 0;
                    string sqlPermanentAddressId = "SELECT permanent_address_id FROM criminal_profile where reference_no='" + AesCryptography.EncryptToString(obj?.profile?.referenceNo) + "';";
                    DataTable dataTable = dbOperation.GetDataTable(sqlPermanentAddressId);
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        permanentAddressId = dataRow["permanent_address_id"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["permanent_address_id"]) : 0;
                    }
                    if (permanentAddressId > 0)
                    {
                        string wherePart = "reference_no='" + AesCryptography.EncryptToString(obj?.profile?.referenceNo) + "' AND id='" + permanentAddressId + "';";
                        isAdded = dbOperation.Update("address", AddressData, wherePart);
                        presentPermanentId[1] = Convert.ToInt32(permanentAddressId);
                        dbOperation.CloseDbConnection();
                    }
                    else
                    {
                        isAdded = dbOperation.InsertData("address", AddressData);

                        if (isAdded)
                        {
                            logger.Debug("Successfully added permanent address.");

                            string sqlGetLastId = "select last_insert_rowid()";
                            int? lastId = dbOperation.ExecuteScalarReturnInt(sqlGetLastId);
                            dbOperation.CloseDbConnection();
                            if (lastId != null) presentPermanentId[1] = Convert.ToInt32(lastId);
                        }

                        else
                        {
                            logger.Debug("Adding new address entry failed! Possibly already exists.");
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

            return presentPermanentId;
        }

        public int GetTodayEnrollCount()
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

        public int GetNormalUploadPendingCount()
        {
            int totalCount = 0;
            try
            {
                string wherePart = String.Format("status='{0}' and error_status='{1}'", Globals.RecordState.NEW, Globals.ErrorState.NOT_ERROR);
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

        public int GetEnrolledErrorCount()
        {
            int totalCount = 0;
            try
            {
                //string wherePart = String.Format("strftime('%Y-%m-%d', created_at)<='{0}' and created_by='{1}' and error_status = {2}",
                //    DateTime.Today.ToString("yyyy-MM-dd"), Users.Id, Globals.ErrorState.ERROR);
                //string wherePart = String.Format("created_by='{0}' and error_status = {1}", Users.Id, Globals.ErrorState.ERROR);
                string wherePart = String.Format("error_status = {0}", Globals.ErrorState.ERROR);
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

        public int GetEnrolledDraftCount()
        {
            int totalCount = 0;
            try
            {
                //string wherePart = String.Format("strftime('%Y-%m-%d', created_at)<='{0}' and created_by='{1}' and status = {2}",
                //    DateTime.Today.ToString("yyyy-MM-dd"), Users.Id, Globals.RecordState.DRAFT);
                //string wherePart = String.Format("created_by='{0}' and status = {1}", Users.Id, Globals.RecordState.DRAFT);
                string wherePart = String.Format("status = {0}", Globals.RecordState.DRAFT);
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
                string sql = String.Format("SELECT hash FROM criminal_profile WHERE {0};", wherePart);

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
                bool isUpdated = dbOperation.Update("criminal_profile", data, wherePart);

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

        public void UpdateProfileValues(string refNo, string columnName, int valueInt, string valueStr)
        {
            //Task.Run(() => UpdateProfileValuesAsync(refNo, columnName, valueInt, valueStr, json));
            UpdateProfileValuesAsync(refNo, columnName, valueInt, valueStr);
        }

        public bool UpdateProfileValuesAsync(string refNo, string columnName, int valueInt, string valueStr)
        {
            try
            {
                string refNoQuery = String.Format("select * from criminal_profile where reference_no='{0}'", AesCryptography.EncryptToString(refNo));
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
                    if (columnName == "full_name" || columnName == "nick_name" || columnName == "occupation" ||
                        columnName == "nationality" || columnName == "nid" || columnName == "height" ||
                        columnName == "identification_mark" || columnName == "mobile" || columnName == "foreigner_address" ||
                        columnName == "family_information" || columnName == "arrest_info" || columnName == "education_information" ||
                        columnName == "investigatingofficer_name" || columnName == "investigatingofficer_bp" || columnName == "investigatingofficer_mobile" ||
                        columnName == "other_information" || columnName == "arrested_by" || columnName == "io_rank")
                    {
                        data.Add(columnName, AesCryptography.EncryptToString(valueStr));
                    }
                    else
                    {
                        data.Add(columnName, valueStr);
                    }
                }

                if (!string.IsNullOrEmpty(StaticData.Enrollment?.profile?.id)) data.Add("criminal_id", AesCryptography.EncryptToString(StaticData.Enrollment?.profile?.id));

                string wherePart = String.Format(" reference_no='{0}'", AesCryptography.EncryptToString(refNo));

                if (!exists)
                {
                    bool val = AddCriminalProfile(StaticData.Enrollment, Globals.RecordState.DRAFT); ;
                    return val;
                }

                //string updatedHash = enrollHashValue();
                //data.Add("hash", updatedHash);
                bool isUpdated = false;
                if (data.Count > 0)
                {
                    dbOperation.OpenDbConnection();
                    isUpdated = dbOperation.Update("criminal_profile", data, wherePart);
                    dbOperation.CloseDbConnection();
                }

                return isUpdated;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x);
                throw x;
            }
        }

        public void UpdateProfileBiometricValues(string refNo, string columnName, byte[] data)
        {
            //Task.Run(() => UpdateProfileValuesAsync(refNo, columnName, valueInt, valueStr, json));
            UpdateProfileBiometricValuesAsync(refNo, columnName, data);
        }

        public bool UpdateProfileBiometricValuesAsync(string refNo, string columnName, byte[] byteData)
        {
            try
            {
                string refNoQuery = String.Format("select * from criminal_profile where reference_no='{0}'", AesCryptography.EncryptToString(refNo));
                dbOperation.OpenDbConnection();
                bool exists = dbOperation.ExecuteScalar(refNoQuery);
                dbOperation.CloseDbConnection();
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add(columnName, AesCryptography.EncryptToByte(byteData));
                if (!string.IsNullOrEmpty(StaticData.Enrollment?.profile?.id)) data.Add("criminal_id", AesCryptography.EncryptToString(StaticData.Enrollment?.profile?.id));

                string wherePart = String.Format(" reference_no='{0}'", AesCryptography.EncryptToString(refNo));

                if (!exists)
                {
                    bool val = AddCriminalProfile(StaticData.Enrollment, Globals.RecordState.DRAFT); ;
                    return val;
                }

                dbOperation.OpenDbConnection();
                bool isUpdated = dbOperation.Update("criminal_profile", data, wherePart);

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

        public void UpdateAddressValues(string refNo, string columnName, int valueInt, string valueStr, string specificColumnName)
        {
            //Task.Run(() => UpdateAddressAsync(refNo, columnName, valueInt, valueStr, specificColumnName));
            UpdateAddressAsync(refNo, columnName, valueInt, valueStr, specificColumnName);
        }

        public bool UpdateAddressAsync(string refNo, string columnName, int valueInt, string valueStr, string specificColumnName)
        {
            try
            {
                string refNoQuery = String.Format("select " + specificColumnName + " from criminal_profile where reference_no='{0}'", AesCryptography.EncryptToString(refNo));

                dbOperation.OpenDbConnection();
                string addressIdStr = dbOperation.ExecuteScalarReturnStr(refNoQuery);
                dbOperation.CloseDbConnection();
                if (string.IsNullOrEmpty(addressIdStr))
                {
                    bool val = AddCriminalProfile(StaticData.Enrollment, Globals.RecordState.DRAFT); ;
                    return val;
                }

                Dictionary<string, object> data = new Dictionary<string, object>();
                //if (!string.IsNullOrEmpty(valueStr)) data.Add(columnName, valueStr);
                if (valueInt >= 0)
                {
                    data.Add(columnName, valueInt);
                }
                else
                {
                    data.Add(columnName, AesCryptography.EncryptToString(valueStr));
                }
                int addressId = Convert.ToInt32(addressIdStr);
                string wherePart = String.Format(" id='{0}'", addressId);
                if (data.Count > 0)
                {
                    dbOperation.OpenDbConnection();
                    bool isUpdated = dbOperation.Update("address", data, wherePart);

                    dbOperation.CloseDbConnection();
                    return isUpdated;
                }
                return false;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x);
                throw x;
            }
        }

        public void UpdateCrimeInfo(string refNo, string columnName, int valueInt, string valueStr)
        {
            //Task.Run(() => UpdateCrimeInfoAsync(refNo, columnName, valueInt, valueStr));
            UpdateCrimeInfoAsync(refNo, columnName, valueInt, valueStr);
        }

        public bool UpdateCrimeInfoAsync(string refNo, string columnName, int valueInt, string valueStr)
        {
            try
            {
                string refNoQuery = String.Format("select crime_information_id" +
                    " from criminal_profile where reference_no='{0}'", AesCryptography.EncryptToString(refNo));

                dbOperation.OpenDbConnection();
                string crimeInfoIdStr = dbOperation.ExecuteScalarReturnStr(refNoQuery);
                dbOperation.CloseDbConnection();

                if (string.IsNullOrEmpty(crimeInfoIdStr))
                {
                    bool val = AddCriminalProfile(StaticData.Enrollment, Globals.RecordState.DRAFT); ;
                    return val;
                }

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
                if (!string.IsNullOrEmpty(crimeInfoIdStr))
                {
                    int crimeInfoId = Convert.ToInt32(crimeInfoIdStr);

                    string wherePart = String.Format(" id='{0}'", crimeInfoId);
                    if (data.Count > 0)
                    {
                        dbOperation.OpenDbConnection();
                        bool isUpdated = dbOperation.Update("crime_information", data, wherePart);

                        dbOperation.CloseDbConnection();
                        return isUpdated;
                    }
                    return false;
                }
                return false;
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
                bool isUpdated = dbOperation.Update("criminal_profile", data, wherePart);

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
                bool isUpdated = dbOperation.Update("criminal_profile", data, wherePart);

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

        public bool UpdateErrorStatus(string hash)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("error_status", 1);

                string wherePart = String.Format(" hash='{0}'", hash);
                dbOperation.OpenDbConnection();
                bool isUpdated = dbOperation.Update("criminal_profile", data, wherePart);

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
                bool isUpdated = dbOperation.Update("criminal_profile", data, wherePart);

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
                string sql = String.Format("SELECT hash FROM criminal_profile WHERE {0};", wherePart);
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

        public int GetBeneficiaryEnrolledCount()
        {
            int totalCount = 0;
            try
            {
                //string wherePart = String.Format("strftime('%Y-%m-%d', created_at)='{0}' and created_by='{1}'",
                //    DateTime.Today.ToString("yyyy-MM-dd"), Users.Id);
                //string wherePart = String.Format("strftime('%Y-%m-%d', created_at)='{0}'", DateTime.Today.ToString("yyyy-MM-dd"));
                string wherePart = String.Format("1=1");
                string sql = String.Format("SELECT COUNT(*) FROM beneficiary WHERE {0};", wherePart);

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

        public List<string> GetUnsyncedApplicationIds()
        {
            List<string> list = new List<string>();
            try
            {
                string wherePart = String.Format("is_synced='{0}'", Globals.RecordState.DRAFT);
                string sql = String.Format("SELECT application_id FROM beneficiary WHERE {0};", wherePart);
                dbOperation.OpenDbConnection();
                DataTable dataTable = dbOperation.GetDataTable(sql);
                dbOperation.CloseDbConnection();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    list.Add(dataRow["application_id"].ToString());
                }
                return list;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.Message);
            }
            return list;
        }

        public List<BeneficiarySummaryDto> GetBeneficiarySummaries(int isSynced)
        {
            List<BeneficiarySummaryDto> list = new List<BeneficiarySummaryDto>();
            try
            {
                string wherePart = String.Format("is_synced='{0}'", isSynced);
                if (isSynced == 3)
                {
                    wherePart = "1=1";
                }
                string sql = String.Format("SELECT application_id, (respondent_first_name || ' ' || respondent_middle_name " +
                    "|| ' ' || respondent_last_name) full_name, " +
                    "case when respondent_gender = 1 then 'Male' when respondent_gender=2 then 'Female' end gender, " +
                    "respondent_phone_no, " +
                    "case when selection_criteria = 1 then 'LIPW' when selection_criteria=2 then 'DIS' end criteria " +
                    "FROM beneficiary WHERE {0}; ", wherePart);
                dbOperation.OpenDbConnection();
                DataTable dataTable = dbOperation.GetDataTable(sql);
                dbOperation.CloseDbConnection();

                int serial = 1;
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    BeneficiarySummaryDto dto = new BeneficiarySummaryDto();
                    dto.Serial = serial;
                    dto.ApplicationId = dataRow["application_id"].GetType() != typeof(DBNull) ? (string)dataRow["application_id"] : null;
                    dto.BeneficiaryName = dataRow["full_name"].GetType() != typeof(DBNull) ? (string)dataRow["full_name"] : null;
                    dto.Gender = dataRow["gender"].GetType() != typeof(DBNull) ? (string)dataRow["gender"] : null;
                    dto.PhoneNo = dataRow["respondent_phone_no"].GetType() != typeof(DBNull) ? (string)dataRow["respondent_phone_no"] : null;
                    dto.SelectionCriteria = dataRow["criteria"].GetType() != typeof(DBNull) ? (string)dataRow["criteria"] : null;

                    list.Add(dto);

                    serial++;
                }
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.Message);
            }

            return list;
        }

        public bool updateIsSyncForUploadedBeneficiary(List<string> applicationIds)
        {
            try
            {
                string ids = "";
                foreach(String id in applicationIds)
                {
                    ids += "'" + id + "',";
                }
                ids = ids.Substring(0, ids.Length - 1);

                string sql = "update beneficiary set is_synced=1 where application_id in (" + ids + ")";
                dbOperation.OpenDbConnection();
                dbOperation.ExecuteQuery(sql);
                dbOperation.CloseDbConnection();

                return true;
            } catch (Exception e)
            {
                dbOperation.CloseDbConnection();
                logger.Error(e.Message);
                throw e;
            }
        }

        public RegisterBeneficiaryRequest getUnsyncedData(string applicationId)
        {
            try
            {
                RegisterBeneficiaryRequest dto = new RegisterBeneficiaryRequest();

                string wherePart = String.Format("application_id='{0}'", applicationId);
                string sql = String.Format("SELECT * FROM beneficiary WHERE {0};", wherePart);
                dbOperation.OpenDbConnection();
                DataTable dataTable = dbOperation.GetDataTable(sql);
                dbOperation.CloseDbConnection();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    dto.applicationId = dataRow["application_id"].GetType() != typeof(DBNull) ?
                        (string)dataRow["application_id"] : null;

                    dto.respondentFirstName = dataRow["respondent_first_name"].GetType() != typeof(DBNull) ?
                        (string)dataRow["respondent_first_name"] : null;

                    dto.respondentMiddleName = dataRow["respondent_middle_name"].GetType() != typeof(DBNull) ?
                        (string)dataRow["respondent_middle_name"] : null;

                    dto.respondentLastName = dataRow["respondent_last_name"].GetType() != typeof(DBNull) ?
                        (string)dataRow["respondent_last_name"] : null;

                    dto.respondentNickName = dataRow["respondent_nick_name"].GetType() != typeof(DBNull) ?
                        (string)dataRow["respondent_nick_name"] : null;

                    dto.spouseFirstName = dataRow["spouse_first_name"].GetType() != typeof(DBNull) ?
                        (string)dataRow["spouse_first_name"] : null;

                    dto.spouseMiddleName = dataRow["spouse_middle_name"].GetType() != typeof(DBNull) ?
                        (string)dataRow["spouse_middle_name"] : null;

                    dto.spouseLastName = dataRow["spouse_last_name"].GetType() != typeof(DBNull) ?
                        (string)dataRow["spouse_last_name"] : null;

                    dto.spouseNickName = dataRow["spouse_nick_name"].GetType() != typeof(DBNull) ?
                        (string)dataRow["spouse_nick_name"] : null;

                    var relationshipValue = dataRow["relationship_with_household"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["relationship_with_household"]) : 0;
                    if (relationshipValue > 0)
                    {
                        RelationshipWithHouseholdHead r = (RelationshipWithHouseholdHead)(relationshipValue+1);
                        dto.relationshipWithHouseholdHead = r.ToString();
                    }

                    var respondentAge = dataRow["respondent_age"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["respondent_age"]) : -1;
                    if (respondentAge > -1)
                    {
                        dto.respondentAge = dataRow["respondent_age"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["respondent_age"]) : -1;
                    }

                    var respondentGender = dataRow["respondent_gender"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["respondent_gender"]) : 0;
                    if (respondentGender > 0)
                    {
                        Gender r = (Gender)(respondentGender+1);
                        dto.respondentGender = r.ToString();
                    }

                    var respondentMaritalStatus = dataRow["respondent_marital_status"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["respondent_marital_status"]) : 0;
                    if (respondentMaritalStatus > 0)
                    {
                        MaritalStatus r = (MaritalStatus)(respondentMaritalStatus+1);
                        dto.respondentMaritalStatus = r.ToString();
                    }

                    var respondentLegalStatus = dataRow["respondent_legal_status"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["respondent_legal_status"]) : 0;
                    if (respondentLegalStatus > 0)
                    {
                        LegalStatus r = (LegalStatus)(respondentLegalStatus+1);
                        dto.respondentLegalStatus = r.ToString();
                    }

                    var documentType = dataRow["document_type"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["document_type"]) : -1;
                    if (documentType > -1)
                    {
                        DocumentType r = (DocumentType)(documentType+1);
                        dto.documentType = r.ToString();
                    }

                    dto.documentTypeOther = dataRow["document_type_other"].GetType() != typeof(DBNull) ?
                        (string)dataRow["document_type_other"] : null;

                    dto.respondentId = dataRow["respondent_id"].GetType() != typeof(DBNull) ?
                        (string)dataRow["respondent_id"] : null;

                    dto.respondentPhoneNo = dataRow["respondent_phone_no"].GetType() != typeof(DBNull) ?
                        (string)dataRow["respondent_phone_no"] : null;

                    var householdIncomeSource = dataRow["household_income_source"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["household_income_source"]) : 0;
                    if (householdIncomeSource > 0)
                    {
                        HouseholdIncomeSource r = (HouseholdIncomeSource)(householdIncomeSource+1);
                        dto.householdIncomeSource = r.ToString();
                    }

                    var householdMonthlyAvgIncome = dataRow["household_monthly_avg_income"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["household_monthly_avg_income"]) : -1;
                    if (householdMonthlyAvgIncome > -1)
                    {
                        dto.householdMonthlyAvgIncome = householdMonthlyAvgIncome;
                    }

                    //var currency = dataRow["currency"].GetType() != typeof(DBNull) ?
                    //    Convert.ToInt32(dataRow["currency"]) : 0;
                    //if (currency > 0)
                    //{
                    //    Currency r = (Currency)(currency+1);
                    //    dto.currency = r.ToString();
                    //}
                    // Commented out after discussing with Emon Raihan bhai

                    dto.currency = Currency.SUDANESE_POUND.ToString();

                    var selectionCriteria = dataRow["selection_criteria"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["selection_criteria"]) : 0;
                    if (selectionCriteria > 0)
                    {
                        SelectionCriteria r = (SelectionCriteria)(selectionCriteria+1);
                        dto.selectionCriteria = r.ToString();
                    }

                    var householdSize = dataRow["household_size"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["household_size"]) : -1;
                    if (householdSize > -1)
                    {
                        dto.householdSize = householdSize;
                    }

                    var isReadWrite = dataRow["is_read_write"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["is_read_write"]) : -1;
                    if (isReadWrite > -1)
                    {
                        dto.isReadWrite = isReadWrite == 1 ? true : false;
                    }

                    var memberReadWrite = dataRow["member_read_write"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["member_read_write"]) : -1;
                    if (memberReadWrite > -1)
                    {
                        dto.memberReadWrite = memberReadWrite;
                    }

                    var isOtherMemberParticipating = dataRow["is_other_member_perticipating"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["is_other_member_perticipating"]) : -1;
                    if (isOtherMemberParticipating > -1)
                    {
                        dto.isOtherMemberPerticipating = isOtherMemberParticipating == 1 ? true : false;
                    }

                    dto.notPerticipationOtherReason = dataRow["non_perticipation_other_reason"].GetType() != typeof(DBNull) ?
                        (string)dataRow["non_perticipation_other_reason"] : null;

                    var createdBy = dataRow["created_by"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["created_by"]) : -1;
                    if (createdBy > -1)
                    {
                        dto.createdBy = createdBy;
                    }

                    var nonParticipationReason = dataRow["non_perticipation_reason"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["non_perticipation_reason"]) : 0;
                    if (nonParticipationReason > 0)
                    {
                        NotPerticipationReason r = (NotPerticipationReason)(nonParticipationReason+1);
                        dto.notPerticipationReason = r.ToString();
                    }

                    dto.incomeSourceOther = dataRow["income_source_other"].GetType() != typeof(DBNull) ?
                        (string)dataRow["income_source_other"] : null;

                    dto.relationshipOther = dataRow["relationship_other"].GetType() != typeof(DBNull) ?
                        (string)dataRow["relationship_other"] : null;

                    List<string> selectionReasons = GetSelectionReasons(dto.applicationId);
                    if (selectionReasons != null && selectionReasons.Count > 0)
                    {
                        dto.selectionReason = selectionReasons;
                    }

                    Address address = getBeneficiaryAddress(dto.applicationId);
                    if (address != null)
                    {
                        dto.address = address;
                    }

                    LocationDto locationDto = getBeneficiaryLocation(dto.applicationId);
                    if (locationDto != null)
                    {
                        dto.location = locationDto;
                    }

                    List<NomineeDto> nomineeList = getBeneficiaryNominee(dto.applicationId);
                    if (nomineeList != null)
                    {
                        dto.nominees = nomineeList;
                    }

                    AlternateDto alternate1 = GetAlternate(dto.applicationId, "ALT1");
                    if (alternate1 != null)
                    {
                        dto.alternatePayee1 = alternate1;
                    }

                    AlternateDto alternate2 = GetAlternate(dto.applicationId, "ALT2");
                    if (alternate2 != null)
                    {
                        dto.alternatePayee2 = alternate2;
                    }

                    getBeneficiaryBiometric(dto, "BENE");
                    getBeneficiaryBiometric(dto, "ALT1");
                    getBeneficiaryBiometric(dto, "ALT2");

                    HouseholdInfo householdInfo2 = GetHousehold(dto.applicationId, "M2");
                    if (householdInfo2 != null)
                    {
                        dto.householdMember2 = householdInfo2;
                    }

                    HouseholdInfo householdInfo5 = GetHousehold(dto.applicationId, "M5");
                    if (householdInfo5 != null)
                    {
                        dto.householdMember5 = householdInfo5;
                    }

                    HouseholdInfo householdInfo17 = GetHousehold(dto.applicationId, "M17");
                    if (householdInfo17 != null)
                    {
                        dto.householdMember17 = householdInfo17;
                    }

                    HouseholdInfo householdInfo35 = GetHousehold(dto.applicationId, "M35");
                    if (householdInfo35 != null)
                    {
                        dto.householdMember35 = householdInfo35;
                    }

                    HouseholdInfo householdInfo64 = GetHousehold(dto.applicationId, "M64");
                    if (householdInfo64 != null)
                    {
                        dto.householdMember64 = householdInfo64;
                    }

                    HouseholdInfo householdInfo65 = GetHousehold(dto.applicationId, "M65");
                    if (householdInfo65 != null)
                    {
                        dto.householdMember65 = householdInfo65;
                    }
                }

                return dto;
            }
            catch (Exception e)
            {
                dbOperation.CloseDbConnection();
                logger.Error("There was an unexpected error when getting beneficiary record from db. applicationId = " + applicationId + "\nError Message:\n" + e.ToString());
                throw e;
            }
        }

        public Address getBeneficiaryAddress(string applicationId)
        {
            try
            {
                Address dto = new Address();

                string wherePart = String.Format("application_id='{0}'", applicationId);
                string sql = String.Format("SELECT * FROM address WHERE {0};", wherePart);

                dbOperation.OpenDbConnection();
                DataTable dataTable = dbOperation.GetDataTable(sql);
                dbOperation.CloseDbConnection();

                if (dataTable.Rows == null || dataTable.Rows.Count == 0)
                {
                    return null;
                }

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    var bomaId = dataRow["boma_id"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["boma_id"]) : -1;
                    if (bomaId > -1)
                    {
                        dto.boma = bomaId;
                    }

                    var countyId = dataRow["county_id"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["county_id"]) : -1;
                    if (countyId > -1)
                    {
                        dto.countyId = countyId;
                    }

                    var payamId = dataRow["payam_id"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["payam_id"]) : -1;
                    if (payamId > -1)
                    {
                        dto.payam = payamId;
                    }

                    var stateId = dataRow["state_id"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["state_id"]) : -1;
                    if (stateId > -1)
                    {
                        dto.stateId = stateId;
                    }
                }

                return dto;
            }
            catch (Exception e)
            {
                logger.Error("There was an unexpected error when getting beneficiary address record from db. applicationId = " + applicationId + "\nError Message:\n" + e.ToString());
                throw e;
            }
        }

        public LocationDto getBeneficiaryLocation(string applicationId)
        {
            try
            {
                LocationDto dto = new LocationDto();

                string wherePart = String.Format("application_id='{0}'", applicationId);
                string sql = String.Format("SELECT * FROM location WHERE {0};", wherePart);

                dbOperation.OpenDbConnection();
                DataTable dataTable = dbOperation.GetDataTable(sql);
                dbOperation.CloseDbConnection();

                if (dataTable.Rows == null || dataTable.Rows.Count == 0)
                {
                    return null;
                }

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    var lat = dataRow["lat"].GetType() != typeof(DBNull) ?
                        Convert.ToDouble(dataRow["lat"]) : -1;
                    if (lat > -1)
                    {
                        dto.lat = lat;
                    }

                    var lon = dataRow["lon"].GetType() != typeof(DBNull) ?
                        Convert.ToDouble(dataRow["lon"]) : -1;
                    if (lon > -1)
                    {
                        dto.lon = lon;
                    }
                }

                return dto;
            }
            catch (Exception e)
            {
                logger.Error("There was an unexpected error when getting beneficiary location record from db. applicationId = " + applicationId + "\nError Message:\n" + e.ToString());
                throw e;
            }
        }

        public List<NomineeDto> getBeneficiaryNominee(string applicationId)
        {
            try
            {
                List<NomineeDto> list = new List<NomineeDto>();
                NomineeDto dto = new NomineeDto();

                string wherePart = String.Format("application_id='{0}'", applicationId);
                string sql = String.Format("SELECT * FROM nominee WHERE {0};", wherePart);

                dbOperation.OpenDbConnection();
                DataTable dataTable = dbOperation.GetDataTable(sql);
                dbOperation.CloseDbConnection();

                if (dataTable.Rows == null || dataTable.Rows.Count == 0)
                {
                    return null;
                }

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    dto.applicationId = applicationId;

                    dto.nomineeFirstName = dataRow["nominee_first_name"].GetType() != typeof(DBNull) ?
                        (string)dataRow["nominee_first_name"] : null;

                    dto.nomineeMiddleName = dataRow["nominee_middle_name"].GetType() != typeof(DBNull) ?
                        (string)dataRow["nominee_middle_name"] : null;

                    dto.nomineeLastName = dataRow["nominee_last_name"].GetType() != typeof(DBNull) ?
                        (string)dataRow["nominee_last_name"] : null;

                    dto.nomineeNickName = dataRow["nominee_nick_name"].GetType() != typeof(DBNull) ?
                        (string)dataRow["nominee_nick_name"] : null;

                    var relationshipValue = dataRow["relationship_with_household"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["relationship_with_household"]) : 0;
                    if (relationshipValue > 0)
                    {
                        RelationshipWithHouseholdHead r = (RelationshipWithHouseholdHead)(relationshipValue+1);
                        dto.relationshipWithHouseholdHead = r.ToString();
                    }

                    var nomineeAge = dataRow["nominee_age"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["nominee_age"]) : -1;
                    if (nomineeAge > -1)
                    {
                        dto.nomineeAge = nomineeAge;
                    }

                    var nomineeGender = dataRow["nominee_gender"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["nominee_gender"]) : 0;
                    if (nomineeGender > 0)
                    {
                        Gender r = (Gender)(nomineeGender+1);
                        dto.nomineeGender = r.ToString();
                    }

                    var isReadWrite = dataRow["is_read_write"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["is_read_write"]) : -1;
                    if (isReadWrite > -1)
                    {
                        dto.isReadWrite = isReadWrite == 1 ? true : false;
                    }

                    var nomineeOccupation = dataRow["nominee_occupation"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["nominee_occupation"]) : -1;
                    if (nomineeOccupation > -1)
                    {
                        Occupation r = (Occupation)nomineeOccupation;
                        dto.nomineeOccupation = r.ToString();
                    }

                    dto.otherOccupation = dataRow["other_occupation"].GetType() != typeof(DBNull) ?
                        (string)dataRow["other_occupation"] : null;

                    dto.relationshipOther = dataRow["relationship_other"].GetType() != typeof(DBNull) ?
                        (string)dataRow["relationship_other"] : null;

                    list.Add(dto);
                }

                return list;
            }
            catch (Exception e)
            {
                logger.Error("There was an unexpected error when getting beneficiary address record from db. applicationId = " + applicationId + "\nError Message:\n" + e.ToString());
                throw e;
            }
        }

        public HouseholdInfo GetHousehold(string applicationId, string type)
        {
            try
            {
                HouseholdInfo dto = new HouseholdInfo();

                string wherePart = String.Format("application_id='{0}' and type='{1}'", applicationId, type);
                string sql = String.Format("SELECT * FROM household_info WHERE {0};", wherePart);

                dbOperation.OpenDbConnection();
                DataTable dataTable = dbOperation.GetDataTable(sql);
                dbOperation.CloseDbConnection();

                if (dataTable.Rows == null || dataTable.Rows.Count == 0)
                {
                    return null;
                }

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    dto.applicationId = applicationId;

                    var maleTotal = dataRow["male_total"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["male_total"]) : -1;
                    if (maleTotal > -1)
                    {
                        dto.maleTotal = maleTotal;
                    }

                    var maleDisable = dataRow["male_disable"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["male_disable"]) : -1;
                    if (maleDisable > -1)
                    {
                        dto.maleDisable = maleDisable;
                    }

                    var maleChronicallyIll = dataRow["male_chronically_ill"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["male_chronically_ill"]) : -1;
                    if (maleChronicallyIll > -1)
                    {
                        dto.maleChronicalIll = maleChronicallyIll;
                    }

                    var maleBoth = dataRow["male_both"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["male_both"]) : -1;
                    if (maleBoth > -1)
                    {
                        dto.maleBoth = maleBoth;
                    }

                    var femaleTotal = dataRow["female_total"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["female_total"]) : -1;
                    if (femaleTotal > -1)
                    {
                        dto.femaleTotal = femaleTotal;
                    }

                    var femaleDisable = dataRow["female_disable"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["female_disable"]) : -1;
                    if (femaleDisable > -1)
                    {
                        dto.femaleDisable = femaleDisable;
                    }

                    var femaleChronicallyIll = dataRow["female_chronically_ill"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["female_chronically_ill"]) : -1;
                    if (femaleChronicallyIll > -1)
                    {
                        dto.femaleChronicalIll = femaleChronicallyIll;
                    }

                    var femaleBoth = dataRow["female_both"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["female_both"]) : -1;
                    if (femaleBoth > -1)
                    {
                        dto.femaleBoth = femaleBoth;
                    }
                }

                return dto;
            }
            catch (Exception e)
            {
                logger.Error("There was an unexpected error when getting beneficiary address record from db. applicationId = " + applicationId + "\nError Message:\n" + e.ToString());
                throw e;
            }
        }

        public AlternateDto GetAlternate(string applicationId, string type)
        {
            try
            {
                AlternateDto dto = new AlternateDto();

                string wherePart = String.Format("application_id='{0}' and type='{1}'", applicationId, type);
                string sql = String.Format("SELECT * FROM alternate WHERE {0};", wherePart);

                dbOperation.OpenDbConnection();
                DataTable dataTable = dbOperation.GetDataTable(sql);
                dbOperation.CloseDbConnection();

                if (dataTable.Rows == null || dataTable.Rows.Count == 0)
                {
                    return null;
                }

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    var documentType = dataRow["document_type"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["document_type"]) : -1;
                    if (documentType > -1)
                    {
                        DocumentType r = (DocumentType)(documentType+1);
                        dto.documentType = r.ToString();
                    }

                    dto.documentTypeOther = dataRow["document_type_other"].GetType() != typeof(DBNull) ?
                        (string)dataRow["document_type_other"] : null;

                    dto.nationalId = dataRow["national_id"].GetType() != typeof(DBNull) ?
                        (string)dataRow["national_id"] : null;

                    var payeeAge = dataRow["payee_age"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["payee_age"]) : -1;
                    if (payeeAge > -1)
                    {
                        dto.payeeAge = payeeAge;
                    }

                    dto.payeeFirstName = dataRow["payee_first_name"].GetType() != typeof(DBNull) ?
                        (string)dataRow["payee_first_name"] : null;

                    var payeeGender = dataRow["payee_gender"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["payee_gender"]) : 0;
                    if (payeeGender > 0)
                    {
                        Gender r = (Gender)(payeeGender + 1);
                        dto.payeeGender = r.ToString();
                    }

                    dto.payeeLastName = dataRow["payee_last_name"].GetType() != typeof(DBNull) ?
                        (string)dataRow["payee_last_name"] : null;

                    dto.payeeMiddleName = dataRow["payee_middle_name"].GetType() != typeof(DBNull) ?
                        (string)dataRow["payee_middle_name"] : null;

                    dto.payeeNickName = dataRow["payee_nick_name"].GetType() != typeof(DBNull) ?
                        (string)dataRow["payee_nick_name"] : null;

                    dto.payeePhoneNo = dataRow["payee_phone_no"].GetType() != typeof(DBNull) ?
                        (string)dataRow["payee_phone_no"] : null;

                    dto.relationshipOther = dataRow["relationship_other"].GetType() != typeof(DBNull) ?
                        (string)dataRow["relationship_other"] : null;

                    if (dataRow["relationship_with_household"].GetType() != typeof(DBNull))
                    {
                        dto.relationshipWithHouseholdHead = (string)dataRow["relationship_with_household"];
                    }
                }

                return dto;
            }
            catch (Exception e)
            {
                logger.Error("There was an unexpected error when getting beneficiary address record from db. applicationId = " + applicationId + "\nError Message:\n" + e.ToString());
                throw e;
            }
        }

        public List<string> GetSelectionReasons(string applicationId)
        {
            try
            {
                List<string> list = new List<string>();

                string wherePart = String.Format("application_id='{0}'", applicationId);
                string sql = String.Format("SELECT * FROM selection_reason WHERE {0};", wherePart);

                dbOperation.OpenDbConnection();
                DataTable dataTable = dbOperation.GetDataTable(sql);
                dbOperation.CloseDbConnection();

                if (dataTable.Rows == null || dataTable.Rows.Count == 0)
                {
                    return null;
                }

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    if (dataRow["selection_reason_id"].GetType() != typeof(DBNull))
                    {
                        list.Add((string)dataRow["selection_reason_id"]);
                    }
                }

                return list;
            }
            catch (Exception e)
            {
                logger.Error("There was an unexpected error when getting beneficiary address record from db. applicationId = " + applicationId + "\nError Message:\n" + e.ToString());
                throw e;
            }
        }

        public void getBeneficiaryBiometric(RegisterBeneficiaryRequest dto, string type)
        {
            try
            {
                string wherePart = String.Format("application_id='{0}' and type='{1}'", dto.applicationId, type);
                string sql = String.Format("SELECT * FROM biometric WHERE {0};", wherePart);

                dbOperation.OpenDbConnection();
                DataTable dataTable = dbOperation.GetDataTable(sql);
                dbOperation.CloseDbConnection();

                if (dataTable.Rows == null || dataTable.Rows.Count == 0)
                {
                    return;
                }

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    List<Biometrics> list = new List<Biometrics>();

                    BiometricUserType biometricUserTypeEnum = new BiometricUserType();
                    var biometricUserTypeValue = dataRow["biometric_user_type"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["biometric_user_type"]) : 0;
                    if (biometricUserTypeValue > 0)
                    {
                        biometricUserTypeEnum = (BiometricUserType)biometricUserTypeValue;
                    }

                    NoFingerprintReason noFingerprintReasonEnum = new NoFingerprintReason();
                    var noFingerprintReasonValue = dataRow["no_finger_print_reason"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["no_finger_print_reason"]) : -1;
                    if (noFingerprintReasonValue > -1)
                    {
                        noFingerprintReasonEnum = (NoFingerprintReason)noFingerprintReasonValue;
                    }

                    bool noFingerPrint = false;
                    var noFingerprintValue = dataRow["no_finger_print"].GetType() != typeof(DBNull) ?
                        Convert.ToInt32(dataRow["no_finger_print"]) : -1;
                    if (noFingerprintValue > -1)
                    {
                        noFingerPrint = noFingerprintValue == 1 ? true : false;
                    }

                    var noFingerprintReasonText = dataRow["no_finger_print_reason_text"].GetType() != typeof(DBNull) ?
                        (string)dataRow["no_finger_print_reason_text"] : null;

                    if (dataRow["wsq_lt"] != DBNull.Value)
                    {
                        Biometrics obj = new Biometrics();
                        obj.biometricData = (byte[])dataRow["wsq_lt"];
                        obj.biometricType = BiometricType.LT.ToString();
                        SetBiometricDtoCommonValues(obj, dto.applicationId, biometricUserTypeEnum,
                            noFingerPrint, noFingerprintReasonEnum, noFingerprintReasonText);

                        list.Add(obj);
                    }

                    if (dataRow["wsq_li"] != DBNull.Value)
                    {
                        Biometrics obj = new Biometrics();
                        obj.biometricData = (byte[])dataRow["wsq_li"];
                        obj.biometricType = BiometricType.LI.ToString();
                        SetBiometricDtoCommonValues(obj, dto.applicationId, biometricUserTypeEnum,
                            noFingerPrint, noFingerprintReasonEnum, noFingerprintReasonText);

                        list.Add(obj);
                    }

                    if (dataRow["wsq_lm"] != DBNull.Value)
                    {
                        Biometrics obj = new Biometrics();
                        obj.biometricData = (byte[])dataRow["wsq_lm"];
                        obj.biometricType = BiometricType.LM.ToString();
                        SetBiometricDtoCommonValues(obj, dto.applicationId, biometricUserTypeEnum,
                            noFingerPrint, noFingerprintReasonEnum, noFingerprintReasonText);

                        list.Add(obj);
                    }

                    if (dataRow["wsq_lr"] != DBNull.Value)
                    {
                        Biometrics obj = new Biometrics();
                        obj.biometricData = (byte[])dataRow["wsq_lr"];
                        obj.biometricType = BiometricType.LR.ToString();
                        SetBiometricDtoCommonValues(obj, dto.applicationId, biometricUserTypeEnum,
                            noFingerPrint, noFingerprintReasonEnum, noFingerprintReasonText);

                        list.Add(obj);
                    }

                    if (dataRow["wsq_ls"] != DBNull.Value)
                    {
                        Biometrics obj = new Biometrics();
                        obj.biometricData = (byte[])dataRow["wsq_ls"];
                        obj.biometricType = BiometricType.LL.ToString();
                        SetBiometricDtoCommonValues(obj, dto.applicationId, biometricUserTypeEnum,
                            noFingerPrint, noFingerprintReasonEnum, noFingerprintReasonText);

                        list.Add(obj);
                    }

                    if (dataRow["wsq_rt"] != DBNull.Value)
                    {
                        Biometrics obj = new Biometrics();
                        obj.biometricData = (byte[])dataRow["wsq_rt"];
                        obj.biometricType = BiometricType.RT.ToString();
                        SetBiometricDtoCommonValues(obj, dto.applicationId, biometricUserTypeEnum,
                            noFingerPrint, noFingerprintReasonEnum, noFingerprintReasonText);

                        list.Add(obj);
                    }

                    if (dataRow["wsq_ri"] != DBNull.Value)
                    {
                        Biometrics obj = new Biometrics();
                        obj.biometricData = (byte[])dataRow["wsq_ri"];
                        obj.biometricType = BiometricType.RI.ToString();
                        SetBiometricDtoCommonValues(obj, dto.applicationId, biometricUserTypeEnum,
                            noFingerPrint, noFingerprintReasonEnum, noFingerprintReasonText);

                        list.Add(obj);
                    }

                    if (dataRow["wsq_rm"] != DBNull.Value)
                    {
                        Biometrics obj = new Biometrics();
                        obj.biometricData = (byte[])dataRow["wsq_rm"];
                        obj.biometricType = BiometricType.RM.ToString();
                        SetBiometricDtoCommonValues(obj, dto.applicationId, biometricUserTypeEnum,
                            noFingerPrint, noFingerprintReasonEnum, noFingerprintReasonText);

                        list.Add(obj);
                    }

                    if (dataRow["wsq_rr"] != DBNull.Value)
                    {
                        Biometrics obj = new Biometrics();
                        obj.biometricData = (byte[])dataRow["wsq_rr"];
                        obj.biometricType = BiometricType.RR.ToString();
                        SetBiometricDtoCommonValues(obj, dto.applicationId, biometricUserTypeEnum,
                            noFingerPrint, noFingerprintReasonEnum, noFingerprintReasonText);

                        list.Add(obj);
                    }

                    if (dataRow["wsq_rs"] != DBNull.Value)
                    {
                        Biometrics obj = new Biometrics();
                        obj.biometricData = (byte[])dataRow["wsq_rs"];
                        obj.biometricType = BiometricType.RL.ToString();
                        SetBiometricDtoCommonValues(obj, dto.applicationId, biometricUserTypeEnum,
                            noFingerPrint, noFingerprintReasonEnum, noFingerprintReasonText);

                        list.Add(obj);
                    }

                    if (dataRow["photo"] != DBNull.Value)
                    {
                        Biometrics obj = new Biometrics();
                        obj.biometricData = (byte[])dataRow["photo"];
                        obj.biometricType = BiometricType.PHOTO.ToString();
                        SetBiometricDtoCommonValues(obj, dto.applicationId, biometricUserTypeEnum,
                            noFingerPrint, noFingerprintReasonEnum, noFingerprintReasonText);

                        list.Add(obj);
                    }

                    if (type == "BENE")
                    {
                        dto.biometrics = list;
                    }
                    else if (type == "ALT1")
                    {
                        if (dto.alternatePayee1 != null)
                        {
                            dto.alternatePayee1.biometrics = list;
                        }
                        else
                        {
                            dto.alternatePayee1 = new AlternateDto();
                            dto.alternatePayee1.biometrics = list;
                        }
                    }
                    else if (type == "ALT2")
                    {
                        if (dto.alternatePayee2 != null)
                        {
                            dto.alternatePayee2.biometrics = list;
                        }
                        else
                        {
                            dto.alternatePayee2 = new AlternateDto();
                            dto.alternatePayee2.biometrics = list;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("There was an unexpected error when getting beneficiary address record from db. applicationId = " + dto.applicationId + "\nError Message:\n" + e.ToString());
                throw e;
            }
        }

        private void SetBiometricDtoCommonValues(Biometrics dto, string applicationId,
            BiometricUserType biometricUserTypeEnum, bool noFingerPrint,
            NoFingerprintReason noFingerprintReasonEnum, string noFingerprintReasonText)
        {
            dto.applicationId = applicationId;
            dto.biometricUserType = biometricUserTypeEnum.ToString();
            dto.noFingerPrint = noFingerPrint;
            dto.noFingerprintReason = noFingerprintReasonEnum.ToString();
            dto.noFingerprintReasonText = noFingerprintReasonText;
        }

        public int GetBeneficiaryUploadPendingCount()
        {
            int totalCount = 0;
            try
            {
                string wherePart = String.Format("is_synced='{0}'", Globals.RecordState.DRAFT);
                string sql = String.Format("SELECT COUNT(*) FROM beneficiary WHERE {0};", wherePart);

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

        public bool UpdateStatusToSynced(string applicationId)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("is_synced", Globals.RecordState.NEW);

                string wherePart = String.Format(" application_id='{0}'", applicationId);
                dbOperation.OpenDbConnection();
                bool isUpdated = dbOperation.Update("beneficiary", data, wherePart);

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

        public bool UpdateBeneficiaryUploadErrorStatus(string applicationId)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("is_synced", 2);

                string wherePart = String.Format(" application_id='{0}'", applicationId);
                dbOperation.OpenDbConnection();
                bool isUpdated = dbOperation.Update("beneficiary", data, wherePart);

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

        public int GetBeneficiaryUploadErrorCount()
        {
            int totalCount = 0;
            try
            {
                //string wherePart = String.Format("strftime('%Y-%m-%d', created_at)<='{0}' and created_by='{1}' and error_status = {2}",
                //    DateTime.Today.ToString("yyyy-MM-dd"), Users.Id, Globals.ErrorState.ERROR);
                //string wherePart = String.Format("created_by='{0}' and error_status = {1}", Users.Id, Globals.ErrorState.ERROR);
                string wherePart = String.Format("is_synced = {0}", 2);
                string sql = String.Format("SELECT COUNT(*) FROM beneficiary WHERE {0};", wherePart);

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

        public int GetUploadedCount()
        {
            int totalCount = 0;
            try
            {
                string wherePart = String.Format("is_synced = {0}", 1);
                string sql = String.Format("SELECT COUNT(*) FROM beneficiary WHERE {0};", wherePart);

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

        public EnrollmentDto GetEnrolledData(string hash)
        {
            try
            {
                EnrollmentDto obj = new EnrollmentDto();
                obj.profile = new ProfileDto();

                string wherePart = String.Format("hash='{0}'", hash);
                string sql = String.Format("SELECT * FROM criminal_profile WHERE {0};", wherePart);
                dbOperation.OpenDbConnection();
                DataTable dataTable = dbOperation.GetDataTable(sql);
                dbOperation.CloseDbConnection();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    obj.profile.id = dataRow["criminal_id"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["criminal_id"]) : null;
                    obj.profile.referenceNo = dataRow["reference_no"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["reference_no"]) : null;
                    obj.profile.fullName = dataRow["full_name"].GetType() != typeof(DBNull) ? AesCryptography.DecryptToString((string)dataRow["full_name"]) : null;
                    if (dataRow["nick_name"].GetType() != typeof(DBNull))
                    {
                        string[] nickNameAr = AesCryptography.DecryptToString(dataRow["nick_name"]?.ToString())?.Split(',');
                        obj.profile.nickName = nickNameAr.ToList<string>();
                    }
                    obj.profile.criminalName = dataRow["criminal_name"].GetType() != typeof(DBNull) ? (string)dataRow["criminal_name"] : null;
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
                        obj.profile.dateOfBirth = dob.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                    }
                    obj.profile.regionOfBirth = dataRow["region_of_birth"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["region_of_birth"]) : 0;

                    if (dataRow["nationality"].GetType() != typeof(DBNull))
                    {
                        var deserializedNationality = new JavaScriptSerializer().Deserialize<NationalityProfileDto>
                            (AesCryptography.DecryptToString((string)dataRow["nationality"]));
                        obj.profile.nationality = deserializedNationality;
                    }
                    if (obj.profile?.nationality?.id == 0) obj.profile.nationality = null;

                    var religion = dataRow["religion"].GetType() != typeof(DBNull) ? (byte)dataRow["religion"] : 10;
                    if (religion == 0) obj.profile.religion = "muslim";
                    else if (religion == 1) obj.profile.religion = "hindu";
                    else if (religion == 2) obj.profile.religion = "christian";
                    else if (religion == 3) obj.profile.religion = "buddhist";

                    var bloodGroup = dataRow["blood_group"].GetType() != typeof(DBNull) ? (byte)dataRow["blood_group"] : 100;
                    if (bloodGroup == 0) obj.profile.bloodGroup = "A_Plus";
                    else if (bloodGroup == 1) obj.profile.bloodGroup = "A_Minus";
                    else if (bloodGroup == 2) obj.profile.bloodGroup = "B_Plus";
                    else if (bloodGroup == 3) obj.profile.bloodGroup = "B_Minus";
                    else if (bloodGroup == 4) obj.profile.bloodGroup = "AB_Plus";
                    else if (bloodGroup == 5) obj.profile.bloodGroup = "AB_Minus";
                    else if (bloodGroup == 6) obj.profile.bloodGroup = "O_Plus";
                    else if (bloodGroup == 7) obj.profile.bloodGroup = "O_Minus";

                    var maritalStatus = dataRow["marital_status"].GetType() != typeof(DBNull) ? (byte)dataRow["marital_status"] : 10;
                    if (maritalStatus == 0) obj.profile.maritalStatus = "Single";
                    else if (maritalStatus == 1) obj.profile.maritalStatus = "Married";
                    else if (maritalStatus == 2) obj.profile.maritalStatus = "Widowed";
                    else if (maritalStatus == 3) obj.profile.maritalStatus = "Divorced";

                    var eyeColor = dataRow["eye_color"].GetType() != typeof(DBNull) ? (byte)dataRow["eye_color"] : 10;
                    if (eyeColor == 0) obj.profile.eyeColor = "Black";
                    else if (eyeColor == 1) obj.profile.eyeColor = "Blue";
                    else if (eyeColor == 2) obj.profile.eyeColor = "Brown";

                    if (dataRow["height"].GetType() != typeof(DBNull))
                    {
                        var deserializedHeight = new JavaScriptSerializer().Deserialize<HeightDto>
                            (AesCryptography.DecryptToString((string)dataRow["height"]));
                        obj.profile.height = deserializedHeight;
                    }
                    if (dataRow["weight"].GetType() != typeof(DBNull))
                    {
                        var deserializedWeight = new JavaScriptSerializer().Deserialize<WeightDto>((string)dataRow["weight"]);
                        obj.profile.weight = deserializedWeight;
                    }

                    obj.profile.occupation = dataRow["occupation"].GetType() != typeof(DBNull) ?
                        AesCryptography.DecryptToString((string)dataRow["occupation"]) : null;
                    obj.profile.nid = dataRow["nid"].GetType() != typeof(DBNull) ?
                        AesCryptography.DecryptToString((string)dataRow["nid"]) : null;

                    if (dataRow["education_information"].GetType() != typeof(DBNull))
                    {
                        var deserializedObject = new JavaScriptSerializer().Deserialize<List<EducationInfoDto>>
                            (AesCryptography.DecryptToString((string)dataRow["education_information"]));
                        obj.profile.educationalInformations = deserializedObject;
                    }

                    obj.profile.identificationMark = dataRow["identification_mark"].GetType() != typeof(DBNull) ?
                        AesCryptography.DecryptToString((string)dataRow["identification_mark"]) : null;

                    if (dataRow["mobile"].GetType() != typeof(DBNull))
                    {
                        string[] mobileAr = AesCryptography.DecryptToString(dataRow["mobile"]?.ToString())?.Split(',');
                        obj.profile.mobile = mobileAr.ToList<string>();
                    }

                    obj.profile.unit = dataRow["battalion"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["battalion"]) : -1;
                    if (dataRow["sub_unit"].GetType() != typeof(DBNull))
                    {
                        if (Convert.ToInt32(dataRow["sub_unit"]) > 0) obj.profile.subUnit = Convert.ToInt32(dataRow["sub_unit"]);
                    }
                    //obj.profile.subUnit = dataRow["sub_unit"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["sub_unit"]) : -1;

                    var crimeInfoId = dataRow["crime_information_id"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["crime_information_id"]) : -1;
                    if (crimeInfoId != -1)
                    {
                        obj.profile.crimeInformation = GetCrimeInfo(crimeInfoId);
                    }

                    if (dataRow["created_at"].GetType() != typeof(DBNull))
                    {
                        DateTime dtCreatedAt = (DateTime)dataRow["created_at"];
                        //obj.profile.createdAt = dtCreatedAt.ToString("yyyy-MM-ddTHH:mm:ss.fffK");
                        obj.profile.createdAtDesktop = dtCreatedAt.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    var createdByUser = dataRow["created_by"].GetType() != typeof(DBNull) ? dataRow["created_by"] : -1;
                    obj.profile.createdBy = Convert.ToInt32(createdByUser);

                    var uploadedByUser = dataRow["uploaded_by"].GetType() != typeof(DBNull) ? dataRow["uploaded_by"] : -1;
                    obj.profile.uploadedBy = Convert.ToInt32(uploadedByUser);

                    obj.profile.status = dataRow["status"].GetType() != typeof(DBNull) ? Convert.ToInt32((byte)dataRow["status"]) : -1;
                    obj.profile.hash = dataRow["hash"].GetType() != typeof(DBNull) ? (string)dataRow["hash"] : null;

                    var presentAddressId = dataRow["present_address_id"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["present_address_id"]) : -1;
                    if (presentAddressId != -1)
                    {
                        obj.profile.presentAddress = GetAddressData(presentAddressId);
                    }

                    var permanentAddressId = dataRow["permanent_address_id"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["permanent_address_id"]) : -1;
                    if (permanentAddressId != -1)
                    {
                        obj.profile.permanentAddress = GetAddressData(permanentAddressId);
                    }

                    if (dataRow["foreigner_address"].GetType() != typeof(DBNull))
                    {
                        var deserializedObject = new JavaScriptSerializer().Deserialize<ForeignAddressDto>
                            (AesCryptography.DecryptToString((string)dataRow["foreigner_address"]));
                        obj.profile.foreignAddress = deserializedObject;
                    }

                    // Photo
                    obj.profile.biometric = new MODELS.DTO.New.Enrollment.Biometric.BiometricDto();
                    obj.profile.biometric.photo.photo = dataRow["criminal_photo"] != DBNull.Value
                                                        ? AesCryptography.DecryptToByte((byte[])dataRow["criminal_photo"]) : null;
                    if (obj.profile.biometric.photo.photo != null)
                    {
                        obj.profile.biometric.photo.unit = obj.profile.unit;
                        obj.profile.biometric.photo.referenceNo = obj.profile.referenceNo;
                        obj.profile.biometric.photo.contentType = "image/jpg";
                        obj.profile.biometric.photo.extension = ".jpg";
                        obj.profile.biometric.referenceNo = obj.profile.referenceNo;
                    }
                    else obj.profile.biometric.photo = null;
                    // FP

                    obj.profile.biometric.fingerprint.rt = dataRow["fp_rt"] != DBNull.Value
                                                        ? AesCryptography.DecryptToByte((byte[])dataRow["fp_rt"]) : null;
                    obj.profile.biometric.fingerprint.ri = dataRow["fp_ri"] != DBNull.Value
                                                        ? AesCryptography.DecryptToByte((byte[])dataRow["fp_ri"]) : null;
                    obj.profile.biometric.fingerprint.rm = dataRow["fp_rm"] != DBNull.Value
                                                        ? AesCryptography.DecryptToByte((byte[])dataRow["fp_rm"]) : null;
                    obj.profile.biometric.fingerprint.rr = dataRow["fp_rr"] != DBNull.Value
                                                        ? AesCryptography.DecryptToByte((byte[])dataRow["fp_rr"]) : null;
                    obj.profile.biometric.fingerprint.rs = dataRow["fp_rs"] != DBNull.Value
                                                        ? AesCryptography.DecryptToByte((byte[])dataRow["fp_rs"]) : null;

                    obj.profile.biometric.fingerprint.lt = dataRow["fp_lt"] != DBNull.Value
                                                        ? AesCryptography.DecryptToByte((byte[])dataRow["fp_lt"]) : null;
                    obj.profile.biometric.fingerprint.li = dataRow["fp_li"] != DBNull.Value
                                                        ? AesCryptography.DecryptToByte((byte[])dataRow["fp_li"]) : null;
                    obj.profile.biometric.fingerprint.lm = dataRow["fp_lm"] != DBNull.Value
                                                        ? AesCryptography.DecryptToByte((byte[])dataRow["fp_lm"]) : null;
                    obj.profile.biometric.fingerprint.lr = dataRow["fp_lr"] != DBNull.Value
                                                        ? AesCryptography.DecryptToByte((byte[])dataRow["fp_lr"]) : null;
                    obj.profile.biometric.fingerprint.ls = dataRow["fp_ls"] != DBNull.Value
                                                        ? AesCryptography.DecryptToByte((byte[])dataRow["fp_ls"]) : null;

                    if (obj.profile.biometric.fingerprint.lt != null
                        || obj.profile.biometric.fingerprint.li != null
                        || obj.profile.biometric.fingerprint.lm != null
                        || obj.profile.biometric.fingerprint.lr != null
                        || obj.profile.biometric.fingerprint.ls != null
                        || obj.profile.biometric.fingerprint.rt != null
                        || obj.profile.biometric.fingerprint.ri != null
                        || obj.profile.biometric.fingerprint.rm != null
                        || obj.profile.biometric.fingerprint.rr != null
                        || obj.profile.biometric.fingerprint.rs != null)
                    {
                        obj.profile.biometric.fingerprint.unit = obj.profile.unit;
                        obj.profile.biometric.fingerprint.contentType = "image/wsq";
                        obj.profile.biometric.fingerprint.extension = ".wsq";
                        obj.profile.biometric.fingerprint.referenceNo = obj.profile.referenceNo;
                        obj.profile.biometric.referenceNo = obj.profile.referenceNo;
                    }
                    else obj.profile.biometric.fingerprint = null;

                    // Iris

                    obj.profile.biometric.iris.right = dataRow["right_iris"] != DBNull.Value
                                                        ? AesCryptography.DecryptToByte((byte[])dataRow["right_iris"]) : null;
                    obj.profile.biometric.iris.left = dataRow["left_iris"] != DBNull.Value
                                                        ? AesCryptography.DecryptToByte((byte[])dataRow["left_iris"]) : null;

                    if (obj.profile.biometric.iris.right != null || obj.profile.biometric.iris.left != null)
                    {
                        obj.profile.biometric.iris.unit = obj.profile.unit;
                        obj.profile.biometric.iris.contentType = "image/jpg";
                        obj.profile.biometric.iris.extension = ".jpg";
                        obj.profile.biometric.iris.referenceNo = obj.profile.referenceNo;
                        obj.profile.biometric.referenceNo = obj.profile.referenceNo;
                    }
                    else obj.profile.biometric.iris = null;

                    if (obj.profile.biometric.photo == null
                        && obj.profile.biometric.fingerprint == null
                        && obj.profile.biometric.iris == null) obj.profile.biometric = null;

                    AttachmentDto attachmentDto = new AttachmentDto();

                    attachmentDto = GetAttachmentInfo(obj.profile.referenceNo);
                    attachmentDto.unit = obj?.profile?.unit;
                    attachmentDto.referenceNo = obj?.profile?.referenceNo;

                    //if (dataRow["fir"].GetType() != typeof(DBNull))
                    //{
                    //    var deserializedObject = new JavaScriptSerializer().Deserialize<List<FIRDto>>((string)dataRow["fir"]);
                    //    obj.profile.firs = deserializedObject;
                    //    attachmentDto.firList = deserializedObject;
                    //}

                    //if (dataRow["complain"].GetType() != typeof(DBNull))
                    //{
                    //    var deserializedObject = new JavaScriptSerializer().Deserialize<List<ComplainDto>>((string)dataRow["complain"]);
                    //    obj.profile.complains = deserializedObject;
                    //    attachmentDto.complaintList = deserializedObject;
                    //}

                    //if (dataRow["seizure"].GetType() != typeof(DBNull))
                    //{
                    //    var deserializedObject = new JavaScriptSerializer().Deserialize<List<SeizureDto>>((string)dataRow["seizure"]);
                    //    obj.profile.seizures = deserializedObject;
                    //    attachmentDto.seizureList = deserializedObject;
                    //}

                    //obj.profile.attachment = attachmentDto;

                    obj.profile.attachment = attachmentDto;

                    FIRDto firDto = new FIRDto();
                    List<FIRDto> firList = new List<FIRDto>();
                    if (obj.profile?.attachment?.firList?.Count > 0)
                    {
                        firDto.firNo = obj.profile?.attachment?.firList[0].firNo;
                        firDto.firDate = obj.profile?.attachment?.firList[0].firDate;
                        firDto.district = obj.profile?.attachment?.firList[0].district;
                        firDto.upozilaOrThana = obj.profile?.attachment?.firList[0].upozilaOrThana;
                    }
                    else
                    {
                        obj.profile.attachment.firList = null;
                        obj.profile.firs = null;
                    }
                    if (obj.profile?.attachment?.firList?.Count > 0)
                    {
                        firList.Add(firDto);
                        obj.profile.firs = firList;
                    }

                    //obj.profile.firs = obj.profile?.attachment?.firList;
                    //obj.profile.complains = obj.profile?.attachment?.complaintList;
                    //obj.profile.seizures = obj.profile?.attachment?.seizureList;

                    if (dataRow["family_information"].GetType() != typeof(DBNull))
                    {
                        var deserializedObject = new JavaScriptSerializer().Deserialize<List<FamilyDto>>
                            (AesCryptography.DecryptToString((string)dataRow["family_information"]));
                        obj.profile.familys = deserializedObject;
                    }

                    if (dataRow["arrest_info"].GetType() != typeof(DBNull))
                    {
                        var deserializedObject = new JavaScriptSerializer().Deserialize<List<ArrestInfoDto>>
                            (AesCryptography.DecryptToString((string)dataRow["arrest_info"]));
                        obj.profile.arrestInfos = deserializedObject;
                    }

                    if (dataRow["arrest_date"].GetType() != typeof(DBNull))
                    {
                        DateTime arrestDateTime = (DateTime)dataRow["arrest_date"];
                        obj.profile.arrestDate = arrestDateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffK");
                    }

                    int politicalGroup = dataRow["political_group"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["political_group"]) : 0;
                    if (politicalGroup > 0)
                    {
                        if (politicalGroup == 1) obj.profile.politicalGroup = "AwamiLeague";
                        if (politicalGroup == 2) obj.profile.politicalGroup = "BNP";
                        if (politicalGroup == 3) obj.profile.politicalGroup = "JatiyaParty_Ershad";
                        if (politicalGroup == 4) obj.profile.politicalGroup = "WorkersPartyofBangladesh";
                        if (politicalGroup == 5) obj.profile.politicalGroup = "JatiyaSamajtantrikDal";
                        if (politicalGroup == 6) obj.profile.politicalGroup = "BikalpaDharaBangladesh";
                        if (politicalGroup == 7) obj.profile.politicalGroup = "GanoForum";
                        if (politicalGroup == 8) obj.profile.politicalGroup = "JatiyaParty_Manju";
                        if (politicalGroup == 9) obj.profile.politicalGroup = "BangladesTarikatFederation";
                    }

                    obj.profile.investigatingOfficerBPNumber = dataRow["investigatingofficer_bp"].GetType() != typeof(DBNull) ?
                        AesCryptography.DecryptToString((string)dataRow["investigatingofficer_bp"]) : null;
                    obj.profile.investigatingOfficerName = dataRow["investigatingofficer_name"].GetType() != typeof(DBNull) ?
                        AesCryptography.DecryptToString((string)dataRow["investigatingofficer_name"]) : null;
                    obj.profile.investigatingOfficerMobile = dataRow["investigatingofficer_mobile"].GetType() != typeof(DBNull) ?
                        AesCryptography.DecryptToString((string)dataRow["investigatingofficer_mobile"]) : null;

                    if (dataRow["other_information"].GetType() != typeof(DBNull))
                    {
                        var deserializedObject = new JavaScriptSerializer().Deserialize<List<OtherInfoDto>>
                            (AesCryptography.DecryptToString((string)dataRow["other_information"]));
                        obj.profile.otherInformationList = deserializedObject;
                    }

                    obj.profile.age = dataRow["age"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["age"]) : 0;

                    obj.profile.arrestedBy = dataRow["arrested_by"].GetType() != typeof(DBNull) ?
                        AesCryptography.DecryptToString((string)dataRow["arrested_by"]) : null;
                    obj.profile.iorank = dataRow["io_rank"].GetType() != typeof(DBNull) ?
                        AesCryptography.DecryptToString((string)dataRow["io_rank"]) : null;

                    //if (dataRow["crime_zone_district"].GetType() != typeof(DBNull))
                    //{
                    //    obj.profile.crimeZoneDistrict = Convert.ToInt32(dataRow["crime_zone_district"]);
                    //}
                    //if (dataRow["crime_zone_upazila"].GetType() != typeof(DBNull))
                    //{
                    //    obj.profile.crimeZoneUpazila = Convert.ToInt32(dataRow["crime_zone_upazila"]);
                    //}
                }
                return obj;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("There was an unexpected error when getting criminal profile record from db. Hash = " + hash + "\nError Message:\n" + x.ToString());
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

        private CrimeInformationDto GetCrimeInfo(int pId)
        {
            try
            {
                CrimeInformationDto crimeInfo = new CrimeInformationDto();
                string wherePart = String.Format("id={0}", pId);
                string sql = String.Format("SELECT * FROM crime_information WHERE {0};", wherePart);

                dbOperation.OpenDbConnection();
                DataTable dataTable = dbOperation.GetDataTable(sql);
                dbOperation.CloseDbConnection();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    if (dataRow["created_at"].GetType() != typeof(DBNull))
                    {
                        DateTime dtCreatedAt = (DateTime)dataRow["created_at"];
                        crimeInfo.createdAt = dtCreatedAt.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                    }
                    if (dataRow["updated_at"].GetType() != typeof(DBNull))
                    {
                        DateTime dtCreatedAt = (DateTime)dataRow["updated_at"];
                        crimeInfo.updatedAt = dtCreatedAt.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                    }
                    crimeInfo.createdBy = dataRow["created_by"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["created_by"]) : 0;
                    crimeInfo.crimeType = dataRow["crime_type"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["crime_type"]) : 0;
                    crimeInfo.criminalStatus = dataRow["criminal_status"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["criminal_status"]) : 0;
                    crimeInfo.groupOrGangName = dataRow["group_or_gang_name"].GetType() != typeof(DBNull) ?
                        AesCryptography.DecryptToString(Convert.ToString(dataRow["group_or_gang_name"])) : null;
                    crimeInfo.priorityListGovt = dataRow["priority_list_govt"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["priority_list_govt"]) : 0;
                    crimeInfo.referenceNo = dataRow["reference_no"].GetType() != typeof(DBNull) ?
                        AesCryptography.DecryptToString(Convert.ToString(dataRow["reference_no"])) : null;
                    crimeInfo.details = dataRow["details"].GetType() != typeof(DBNull) ? Convert.ToString(dataRow["details"]) : null;
                    crimeInfo.updatedBy = dataRow["updated_by"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["updated_by"]) : 0;

                    if (dataRow["activities"].GetType() != typeof(DBNull))
                    {
                        var deserializedObject = new JavaScriptSerializer().Deserialize<ActivityDto>((string)dataRow["activities"]);
                        crimeInfo.activities = deserializedObject;
                    }
                    if (dataRow["case_details"].GetType() != typeof(DBNull))
                    {
                        var deserializedObject = new JavaScriptSerializer().Deserialize<List<CaseDetailDto>>((string)dataRow["case_details"]);
                        crimeInfo.caseDetails = deserializedObject;
                    }
                    if (dataRow["crime_zone"].GetType() != typeof(DBNull))
                    {
                        var deserializedObject = new JavaScriptSerializer().Deserialize<CrimeZoneDto>
                            (AesCryptography.DecryptToString((string)dataRow["crime_zone"]));
                        crimeInfo.crimeZone = deserializedObject;
                    }
                    if (dataRow["crime_history"].GetType() != typeof(DBNull))
                    {
                        var deserializedObject = new JavaScriptSerializer().Deserialize<List<CrimeHistoryDto>>
                            (AesCryptography.DecryptToString((string)dataRow["crime_history"]));
                        crimeInfo.crimeHistorys = deserializedObject;
                    }
                    if (dataRow["remand_information"].GetType() != typeof(DBNull))
                    {
                        var deserializedObject = new JavaScriptSerializer().Deserialize<List<RemandInformationDto>>((string)dataRow["remand_information"]);
                        crimeInfo.remandInformations = deserializedObject;
                    }

                    if (crimeInfo.warrant == null) crimeInfo.warrant = new WarrantDto();
                    crimeInfo.warrant.warrantType = dataRow["warrant_type"].GetType() != typeof(DBNull) ?
                        AesCryptography.DecryptToString(Convert.ToString(dataRow["warrant_type"])) : null;
                    crimeInfo.warrant.warrantNo = dataRow["warrant_no"].GetType() != typeof(DBNull) ?
                        AesCryptography.DecryptToString(Convert.ToString(dataRow["warrant_no"])) : null;
                    crimeInfo.warrant.sectionNo = dataRow["section_no"].GetType() != typeof(DBNull) ?
                        AesCryptography.DecryptToString(Convert.ToString(dataRow["section_no"])) : null;

                    if (dataRow["recoveries"].GetType() != typeof(DBNull))
                    {
                        var deserializedObject = new JavaScriptSerializer().Deserialize<List<RecoveryEntryDto>>
                            (AesCryptography.DecryptToString((string)dataRow["recoveries"]));
                        crimeInfo.recoveryList = deserializedObject;
                    }
                }
                return crimeInfo;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Error when reading crime_information for crime_information_id = " + pId + x.ToString());
            }
            return new CrimeInformationDto();
        }

        private AddressDto GetAddressData(int pId)
        {
            try
            {
                AddressDto addressInfo = new AddressDto();
                string wherePart = String.Format("id={0}", pId);
                string sql = String.Format("SELECT * FROM address WHERE {0};", wherePart);

                dbOperation.OpenDbConnection();
                DataTable dataTable = dbOperation.GetDataTable(sql);
                dbOperation.CloseDbConnection();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    if (dataRow["created_at"].GetType() != typeof(DBNull))
                    {
                        DateTime dtCreatedAt = (DateTime)dataRow["created_at"];
                        addressInfo.createdAt = dtCreatedAt.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                    }
                    addressInfo.createdBy = dataRow["created_by"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["created_by"]) : 0;
                    if (dataRow["district"].GetType() != typeof(DBNull))
                    {
                        if (Convert.ToInt32(dataRow["district"]) > 0) addressInfo.district = Convert.ToInt32(dataRow["district"]);
                    }
                    if (dataRow["upazila"].GetType() != typeof(DBNull))
                    {
                        if (Convert.ToInt32(dataRow["upazila"]) > 0) addressInfo.upazila = Convert.ToInt32(dataRow["upazila"]);
                    }
                    if (dataRow["union_or_ward"].GetType() != typeof(DBNull))
                    {
                        if (Convert.ToInt32(dataRow["union_or_ward"]) > 0) addressInfo.union = Convert.ToInt32(dataRow["union_or_ward"]);
                    }
                    //addressInfo.district = dataRow["district"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["district"]) : 0;
                    //addressInfo.union = dataRow["union_or_ward"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["union_or_ward"]) : 0;
                    //addressInfo.upazila = dataRow["upazila"].GetType() != typeof(DBNull) ? Convert.ToInt32(dataRow["upazila"]) : 0;
                    addressInfo.villageHouseRoadNo = dataRow["village_house_road_no"].GetType() != typeof(DBNull) ?
                        AesCryptography.DecryptToString((string)dataRow["village_house_road_no"]) : null;
                }
                return addressInfo;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Address Id = " + pId + ". Got error when getting address.\n" + x.ToString());
                throw x;
            }
        }
    }
}
