using ISTL.COMMON;
using ISTL.COMMON.Database;
using ISTL.COMMON.Subscription;
using ISTL.PERSOGlobals;
using ISTL.RAB.View;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISTL.RAB.DbManager
{
    public class DbTransfer
    {
        public delegate void ProgressCallback(int total, int done);
        private ProgressCallback progressCallback;

        private Logger logger = LogManager.GetCurrentClassLogger();

        public void setProgressCallback(ProgressCallback callback)
        {
            progressCallback = callback;
        }

        /// <summary>
        /// Migrate the external DB
        /// </summary>
        /// <param name="path">Path of the external DB</param>
        /// <returns>FALSE: If the external DB at path is a newer version</returns>
        private bool MigrateDb(string path)
        {
            DbCreateTables dbCreateTables = new DbCreateTables(path, Globals.Database.PASSWORD);
            dbCreateTables.CreateDbTables();

            string migDbPath = Utils.GetAssemblyPath() + "\\" + Globals.Database.NAME_MIG;
            DbMigration dbM = new DbMigration(path, Globals.Database.PASSWORD, migDbPath, Globals.Database.PASSWORD_MIG);
            if (dbM.MigrateDb()) return true;
            else return false;
        }

        public string Transfer(bool isImport, string dbName, System.ComponentModel.BackgroundWorker worker)
        {
            long totalRows = 0;
            int exportFlag = 0;
            long numberOfRowsAffected = 0;
            string returnMessage = "";
            bool isAdded;
            DbOperation dbOperationSource;
            DbOperation dbOperationDestination;

            try
            {
                if (!MigrateDb(dbName))
                {
                    logger.Error("Will not start transfer because the external DB is a newer version. PATH=" + dbName);
                    MessageBox.Show("Sorry, you cannot continue with the data transfer because the external " +
                        "database is a newer version. Please update your application.", "RAB CDMS",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            catch (Exception x)
            {
                logger.Error("Unexpected error during DB migration before DB transfer.\n" + x.ToString());
                ErrorMessageBox.ShowErrorWithWaitDialog("There was an unexpected error when preparing database for data transfer.", x);
                return null;
            }

            if (!isImport)
            {
                dbOperationSource = new DbOperation(Globals.Database.NAME, Globals.Database.PASSWORD);
                dbOperationDestination = new DbOperation(dbName, Globals.Database.PASSWORD);
            }
            else
            {
                dbOperationSource = new DbOperation(dbName, Globals.Database.PASSWORD);
                dbOperationDestination = new DbOperation(Globals.Database.NAME, Globals.Database.PASSWORD);
            }
            dbOperationSource.OpenDbConnection();
            dbOperationDestination.OpenDbConnection();

            string errorMsg = string.Empty;

            try
            {
                //check the db of selected path is empty or having data
                if (!isImport)
                {
                    DataTable dt = dbOperationDestination.GetDataTable("SELECT COUNT(id) FROM beneficiary");
                    if (Convert.ToInt16(dt.Rows[0][0]) > 0)
                    {
                        exportFlag = 1;
                    }
                }
                string sql = String.Format("SELECT application_id FROM beneficiary WHERE is_synced='{0}'", Globals.RecordState.DRAFT);
                DataTable dataTable = dbOperationSource.GetDataTable(sql);
                totalRows = dataTable.Rows.Count;

                if (totalRows > 0)
                {
                    int count = 0;
                    progressCallback((int)totalRows, count);
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        object applicationId = dataRow["application_id"];
                        sql = "SELECT * FROM beneficiary WHERE application_id='" + applicationId + "'";
                        DataTable selectDataTable = dbOperationSource.GetDataTable(sql);
                        foreach (DataRow selectDataRow in selectDataTable.Rows)
                        {
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            data.Add("id", selectDataRow["id"]);
                            data.Add("is_synced", selectDataRow["is_synced"]);
                            data.Add("application_id", selectDataRow["application_id"]);
                            data.Add("respondent_first_name", selectDataRow["respondent_first_name"]);
                            data.Add("respondent_middle_name", selectDataRow["respondent_middle_name"]);
                            data.Add("respondent_last_name", selectDataRow["respondent_last_name"]);
                            data.Add("respondent_nick_name", selectDataRow["respondent_nick_name"]);
                            data.Add("spouse_first_name", selectDataRow["spouse_first_name"]);
                            data.Add("spouse_middle_name", selectDataRow["spouse_middle_name"]);
                            data.Add("spouse_last_name", selectDataRow["spouse_last_name"]);
                            data.Add("spouse_nick_name", selectDataRow["spouse_nick_name"]);
                            data.Add("relationship_with_household", selectDataRow["relationship_with_household"]);
                            data.Add("respondent_age", selectDataRow["respondent_age"]);
                            data.Add("respondent_gender", selectDataRow["respondent_gender"]);
                            data.Add("respondent_marital_status", selectDataRow["respondent_marital_status"]);
                            data.Add("respondent_legal_status", selectDataRow["respondent_legal_status"]);
                            data.Add("document_type", selectDataRow["document_type"]);
                            data.Add("document_type_other", selectDataRow["document_type_other"]);
                            data.Add("respondent_id", selectDataRow["respondent_id"]);
                            data.Add("respondent_phone_no", selectDataRow["respondent_phone_no"]);
                            data.Add("household_income_source", selectDataRow["household_income_source"]);
                            data.Add("household_monthly_avg_income", selectDataRow["household_monthly_avg_income"]);
                            data.Add("currency", selectDataRow["currency"]);
                            data.Add("selection_criteria", selectDataRow["selection_criteria"]);
                            data.Add("household_size", selectDataRow["household_size"]);
                            data.Add("is_read_write", selectDataRow["is_read_write"]);
                            data.Add("member_read_write", selectDataRow["member_read_write"]);
                            data.Add("is_other_member_perticipating", selectDataRow["is_other_member_perticipating"]);
                            data.Add("non_perticipation_other_reason", selectDataRow["non_perticipation_other_reason"]);
                            data.Add("creation_date", selectDataRow["creation_date"]);
                            data.Add("created_by", selectDataRow["created_by"]);
                            data.Add("non_perticipation_reason", selectDataRow["non_perticipation_reason"]);
                            data.Add("income_source_other", selectDataRow["income_source_other"]);
                            data.Add("relationship_other", selectDataRow["relationship_other"]);

                            DbTransferAddress((string)selectDataRow["application_id"], dbOperationSource, dbOperationDestination);
                            
                            DbTransferAlternate((string)selectDataRow["application_id"], dbOperationSource, dbOperationDestination);

                            DbTransferBiometric((string)selectDataRow["application_id"], dbOperationSource, dbOperationDestination);

                            DbTransferHouseholdInfo((string)selectDataRow["application_id"], dbOperationSource, dbOperationDestination);

                            DbTransferLocation((string)selectDataRow["application_id"], dbOperationSource, dbOperationDestination);

                            DbTransferNominee((string)selectDataRow["application_id"], dbOperationSource, dbOperationDestination);

                            DbTransferSelectionReason((string)selectDataRow["application_id"], dbOperationSource, dbOperationDestination);

                            isAdded = dbOperationDestination.InsertData("beneficiary", data);
                            if (isAdded == true)
                            {
                                numberOfRowsAffected++;
                            }

                            // Not using EXPORTED status
                            //if (selectDataRow["status"].ToString() == "1") // 1 = New
                            //{
                            //    Dictionary<string, object> updateData = new Dictionary<string, object>();
                            //    updateData.Add("status", Globals.RecordState.EXPORTED);
                            //    bool isUpdated = dbOperationSource.Update("criminal_profile", updateData, "hash='" + hash + "'");
                            //}
                            progressCallback((int)totalRows, ++count);

                            if (worker.CancellationPending)
                            {
                                logger.Debug("DbTransfer: Abort request received. Getting out!");
                                return null;
                            }
                        }
                    }
                    //exporting enroll_client data:END
                }
            }
            catch (Exception x)
            {
                logger.Error("Error while transferring data. isImport=" + isImport + "\n" + x.ToString());
                //MessageBox.Show("Sorry, there was a problem exporting data. It is possible that free space is not available in the selected path.", "RAB CDMS Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                errorMsg = "Sorry, there was a problem exporting data, possibly because free space is not available in the selected folder";
                ErrorMessageBox.ShowErrorWithWaitDialog("There was an unexpected error during data transfer.", x);
                return null;
            }
            
            finally
            {
                if (!string.IsNullOrEmpty(errorMsg))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", errorMsg);
                }
                if (isImport)
                {
                    CounterSubject counterStatus = (CounterSubject)SubjectFactory.GetInstance().GetSubject(Globals.Common.COUNTER_NAME);
                    counterStatus.Count = this.GetBeneficiaryEnrolledCount(dbOperationSource);
                    counterStatus.Notify();

                    CounterDraftSubject counterDraftStatus = (CounterDraftSubject)SubjectFactory.GetInstance().GetSubject(Globals.Common.COUNTER_DRAFT_NAME);
                    counterDraftStatus.Count = this.GetUploadedCount(dbOperationSource);
                    counterDraftStatus.Notify();

                    CounterPendingSubject counterPendingSubject = (CounterPendingSubject)SubjectFactory.GetInstance().GetSubject(Globals.Common.COUNTER_PENDING_NAME);
                    counterPendingSubject.Count = this.GetBeneficiaryUploadPendingCount(dbOperationSource);
                    counterPendingSubject.Notify();

                    CounterErrorSubject counterErrorStatus = (CounterErrorSubject)SubjectFactory.GetInstance().GetSubject(Globals.Common.COUNTER_ERROR_NAME);
                    counterErrorStatus.Count = this.GetBeneficiaryUploadErrorCount(dbOperationSource);
                    counterErrorStatus.Notify();
                }

                dbOperationSource.CloseDbConnection();
                dbOperationDestination.CloseDbConnection();
            }            

            returnMessage = Convert.ToString(totalRows) + "#" + Convert.ToString(numberOfRowsAffected) + "#" + Convert.ToString(exportFlag);
            return returnMessage;
        }

        public void DbTransferAddress(string applicationId, DbOperation dbOpSource, DbOperation dbOpDest)
        {
            try
            {
                string sql = "SELECT * FROM address WHERE application_id='" + applicationId + "'";
                DataTable selectDataTable = dbOpSource.GetDataTable(sql);
                foreach (DataRow selectDataRow in selectDataTable.Rows)
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("id", selectDataRow["id"]);
                    data.Add("application_id", selectDataRow["application_id"]);
                    data.Add("address_line", selectDataRow["address_line"]);
                    data.Add("state_id", selectDataRow["state_id"]);
                    data.Add("county_id", selectDataRow["county_id"]);
                    data.Add("payam_id", selectDataRow["payam_id"]);
                    data.Add("boma_id", selectDataRow["boma_id"]);

                    dbOpDest.InsertData("address", data);
                }
            }
            catch(Exception x)
            {
                logger.Error("Error while transferring address data. "+x.ToString());
            }
        }
        public void DbTransferAlternate(string applicationId, DbOperation dbOpSource, DbOperation dbOpDest)
        {
            try
            {
                string sql = "SELECT * FROM alternate WHERE application_id='" + applicationId + "'";
                DataTable selectDataTable = dbOpSource.GetDataTable(sql);
                foreach (DataRow selectDataRow in selectDataTable.Rows)
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("id", selectDataRow["id"]);
                    data.Add("application_id", selectDataRow["application_id"]);
                    data.Add("payee_first_name", selectDataRow["payee_first_name"]);
                    data.Add("payee_middle_name", selectDataRow["payee_middle_name"]);
                    data.Add("payee_last_name", selectDataRow["payee_last_name"]);
                    data.Add("payee_nick_name", selectDataRow["payee_nick_name"]);
                    data.Add("payee_gender", selectDataRow["payee_gender"]);
                    data.Add("payee_age", selectDataRow["payee_age"]);
                    data.Add("document_type", selectDataRow["document_type"]);
                    data.Add("document_type_other", selectDataRow["document_type_other"]);
                    data.Add("national_id", selectDataRow["national_id"]);
                    data.Add("payee_phone_no", selectDataRow["payee_phone_no"]);
                    data.Add("relationship_with_household", selectDataRow["relationship_with_household"]);
                    data.Add("relationship_other", selectDataRow["relationship_other"]);
                    data.Add("type", selectDataRow["type"]);

                    dbOpDest.InsertData("alternate", data);
                }
            }
            catch (Exception x)
            {
                logger.Error("Error while transferring crime_information data. " + x.ToString());
            }
        }

        public void DbTransferBiometric(string applicationId, DbOperation dbOpSource, DbOperation dbOpDest)
        {
            try
            {
                string sql = "SELECT * FROM biometric WHERE application_id='" + applicationId + "'";
                DataTable selectDataTable = dbOpSource.GetDataTable(sql);
                foreach (DataRow selectDataRow in selectDataTable.Rows)
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("id", selectDataRow["id"]);
                    data.Add("application_id", selectDataRow["application_id"]);
                    data.Add("biometric_user_type", selectDataRow["biometric_user_type"]);
                    data.Add("no_finger_print", selectDataRow["no_finger_print"]);
                    data.Add("no_finger_print_reason", selectDataRow["no_finger_print_reason"]);
                    data.Add("no_finger_print_reason_text", selectDataRow["no_finger_print_reason_text"]);
                    data.Add("wsq_lt", selectDataRow["wsq_lt"]);
                    data.Add("wsq_li", selectDataRow["wsq_li"]);
                    data.Add("wsq_lm", selectDataRow["wsq_lm"]);
                    data.Add("wsq_lr", selectDataRow["wsq_lr"]);
                    data.Add("wsq_ls", selectDataRow["wsq_ls"]);
                    data.Add("wsq_rt", selectDataRow["wsq_rt"]);
                    data.Add("wsq_ri", selectDataRow["wsq_ri"]);
                    data.Add("wsq_rm", selectDataRow["wsq_rm"]);
                    data.Add("wsq_rr", selectDataRow["wsq_rr"]);
                    data.Add("wsq_rs", selectDataRow["wsq_rs"]);
                    data.Add("photo", selectDataRow["photo"]);
                    data.Add("type", selectDataRow["type"]);

                    dbOpDest.InsertData("biometric", data);
                }
            }
            catch (Exception x)
            {
                logger.Error("Error while transferring attachment data. " + x.ToString());
            }
        }

        public void DbTransferHouseholdInfo(string applicationId, DbOperation dbOpSource, DbOperation dbOpDest)
        {
            try
            {
                string sql = "SELECT * FROM household_info WHERE application_id='" + applicationId + "'";
                DataTable selectDataTable = dbOpSource.GetDataTable(sql);
                foreach (DataRow selectDataRow in selectDataTable.Rows)
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("id", selectDataRow["id"]);
                    data.Add("application_id", selectDataRow["application_id"]);
                    data.Add("male_total", selectDataRow["male_total"]);
                    data.Add("female_total", selectDataRow["female_total"]);
                    data.Add("male_disable", selectDataRow["male_disable"]);
                    data.Add("male_chronically_ill", selectDataRow["male_chronically_ill"]);
                    data.Add("male_both", selectDataRow["male_both"]);
                    data.Add("female_both", selectDataRow["female_both"]);
                    data.Add("female_disable", selectDataRow["female_disable"]);
                    data.Add("female_chronically_ill", selectDataRow["female_chronically_ill"]);
                    data.Add("type", selectDataRow["type"]);

                    dbOpDest.InsertData("household_info", data);
                }
            }
            catch (Exception x)
            {
                logger.Error("Error while transferring attachment data. " + x.ToString());
            }
        }

        public void DbTransferLocation(string applicationId, DbOperation dbOpSource, DbOperation dbOpDest)
        {
            try
            {
                string sql = "SELECT * FROM location WHERE application_id='" + applicationId + "'";
                DataTable selectDataTable = dbOpSource.GetDataTable(sql);
                foreach (DataRow selectDataRow in selectDataTable.Rows)
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("id", selectDataRow["id"]);
                    data.Add("application_id", selectDataRow["application_id"]);
                    data.Add("lat", selectDataRow["lat"]);
                    data.Add("lon", selectDataRow["lon"]);

                    dbOpDest.InsertData("location", data);
                }
            }
            catch (Exception x)
            {
                logger.Error("Error while transferring attachment data. " + x.ToString());
            }
        }

        public void DbTransferNominee(string applicationId, DbOperation dbOpSource, DbOperation dbOpDest)
        {
            try
            {
                string sql = "SELECT * FROM nominee WHERE application_id='" + applicationId + "'";
                DataTable selectDataTable = dbOpSource.GetDataTable(sql);
                foreach (DataRow selectDataRow in selectDataTable.Rows)
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("id", selectDataRow["id"]);
                    data.Add("application_id", selectDataRow["application_id"]);
                    data.Add("nominee_first_name", selectDataRow["nominee_first_name"]);
                    data.Add("nominee_middle_name", selectDataRow["nominee_middle_name"]);
                    data.Add("nominee_last_name", selectDataRow["nominee_last_name"]);
                    data.Add("nominee_nick_name", selectDataRow["nominee_nick_name"]);
                    data.Add("relationship_with_household", selectDataRow["relationship_with_household"]);
                    data.Add("nominee_age", selectDataRow["nominee_age"]);
                    data.Add("nominee_gender", selectDataRow["nominee_gender"]);
                    data.Add("is_read_write", selectDataRow["is_read_write"]);
                    data.Add("nominee_occupation", selectDataRow["nominee_occupation"]);
                    data.Add("other_occupation", selectDataRow["other_occupation"]);
                    data.Add("relationship_other", selectDataRow["relationship_other"]);

                    dbOpDest.InsertData("nominee", data);
                }
            }
            catch (Exception x)
            {
                logger.Error("Error while transferring attachment data. " + x.ToString());
            }
        }

        public void DbTransferSelectionReason(string applicationId, DbOperation dbOpSource, DbOperation dbOpDest)
        {
            try
            {
                string sql = "SELECT * FROM selection_reason WHERE application_id='" + applicationId + "'";
                DataTable selectDataTable = dbOpSource.GetDataTable(sql);
                foreach (DataRow selectDataRow in selectDataTable.Rows)
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("id", selectDataRow["id"]);
                    data.Add("application_id", selectDataRow["application_id"]);
                    data.Add("selection_reason_id", selectDataRow["selection_reason_id"]);

                    dbOpDest.InsertData("selection_reason", data);
                }
            }
            catch (Exception x)
            {
                logger.Error("Error while transferring selection reason data. " + x.ToString());
            }
        }

        public int GetBeneficiaryEnrolledCount(DbOperation dbOperation)
        {
            int totalCount = 0;
            try
            {
                string wherePart = String.Format("1=1");
                string sql = String.Format("SELECT COUNT(*) FROM beneficiary WHERE {0};", wherePart);

                totalCount = Convert.ToInt32(dbOperation.GetRowCount(sql));
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
                throw x;
            }
            return totalCount;
        }

        public int GetUploadedCount(DbOperation dbOperation)
        {
            int totalCount = 0;
            try
            {
                string wherePart = String.Format("is_synced = {0}", 1);
                string sql = String.Format("SELECT COUNT(*) FROM beneficiary WHERE {0};", wherePart);

                totalCount = Convert.ToInt32(dbOperation.GetRowCount(sql));
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
                throw x;
            }
            return totalCount;
        }

        public int GetBeneficiaryUploadPendingCount(DbOperation dbOperation)
        {
            int totalCount = 0;
            try
            {
                string wherePart = String.Format("is_synced='{0}'", Globals.RecordState.DRAFT);
                string sql = String.Format("SELECT COUNT(*) FROM beneficiary WHERE {0};", wherePart);

                totalCount = Convert.ToInt32(dbOperation.GetRowCount(sql));
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
                throw x;
            }
            return totalCount;
        }

        public int GetBeneficiaryUploadErrorCount(DbOperation dbOperation)
        {
            int totalCount = 0;
            try
            {
                string wherePart = String.Format("is_synced = {0}", 2);
                string sql = String.Format("SELECT COUNT(*) FROM beneficiary WHERE {0};", wherePart);

                totalCount = Convert.ToInt32(dbOperation.GetRowCount(sql));
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
                throw x;
            }
            return totalCount;
        }

        public int GetTodayEnrollCount(DbOperation dbOperation)
        {
            int totalCount = 0;
            try
            {
                string wherePart = String.Format("strftime('%Y-%m-%d', created_at)='{0}'", DateTime.Today.ToString("yyyy-MM-dd"));
                string sql = String.Format("SELECT COUNT(hash) FROM criminal_profile WHERE {0};", wherePart);

                totalCount = Convert.ToInt32(dbOperation.GetRowCount(sql));
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
                throw x;
            }
            return totalCount;
        }

        public int GetEnrolledDraftCount(DbOperation dbOperation)
        {
            int totalCount = 0;
            try
            {
                string wherePart = String.Format("status = {0}", Globals.RecordState.DRAFT);
                string sql = String.Format("SELECT COUNT(hash) FROM criminal_profile WHERE {0};", wherePart);

                totalCount = Convert.ToInt32(dbOperation.GetRowCount(sql));
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
                throw x;
            }
            return totalCount;
        }

        public int GetNormalUploadPendingCount(DbOperation dbOperation)
        {
            int totalCount = 0;
            try
            {
                string wherePart = String.Format("status='{0}' and error_status='{1}'", Globals.RecordState.NEW, Globals.ErrorState.NOT_ERROR);
                string sql = String.Format("SELECT COUNT(hash) FROM criminal_profile WHERE {0};", wherePart);

                totalCount = Convert.ToInt32(dbOperation.GetRowCount(sql));
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
                throw x;
            }
            return totalCount;
        }

        public int GetEnrolledErrorCount(DbOperation dbOperation)
        {
            int totalCount = 0;
            try
            {
                string wherePart = String.Format("error_status = {0}", Globals.ErrorState.ERROR);
                string sql = String.Format("SELECT COUNT(hash) FROM criminal_profile WHERE {0};", wherePart);

                totalCount = Convert.ToInt32(dbOperation.GetRowCount(sql));
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
                throw x;
            }
            return totalCount;
        }
    }
}
