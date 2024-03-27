using ISTL.COMMON.Subscription;
using ISTL.MODELS.DTO.Beneficiary;
using ISTL.MODELS.Request.Beneficiary;
using ISTL.MODELS.Response.Beneficiary;
using ISTL.PERSOGlobals;
using ISTL.RAB.ApiManager;
using ISTL.RAB.DbManager;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace ISTL.RAB.View.Beneficiary
{
    public partial class BeneficiaryListForm : Form
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        public string pageBeneficiaryCriteria { get; set; }
        private DbEnrollClientManager dbEnrollClientManager;
        private BeneficiaryApiManager beneficiaryApiManager;

        CounterPendingSubject counterPendingStatus = (CounterPendingSubject)SubjectFactory.GetInstance().GetSubject(Globals.Common.COUNTER_PENDING_NAME);
        CounterErrorSubject counterErrorStatus = (CounterErrorSubject)SubjectFactory.GetInstance().GetSubject(Globals.Common.COUNTER_ERROR_NAME);
        CounterDraftSubject counterDraftStatus = (CounterDraftSubject)SubjectFactory.GetInstance().GetSubject(Globals.Common.COUNTER_DRAFT_NAME);

        public BeneficiaryListForm()
        {
            InitializeComponent();
            dbEnrollClientManager = new DbEnrollClientManager();
            beneficiaryApiManager = new BeneficiaryApiManager();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void BeneficiaryListForm_Shown(object sender, EventArgs e)
        {
            List<BeneficiarySummaryDto> list = new List<BeneficiarySummaryDto>();

            if (pageBeneficiaryCriteria == "pending")
            {
                lblTitle.Text = "Upload Pending Beneficiary List";
                list = dbEnrollClientManager.GetBeneficiarySummaries(0);
            } 
            else if (pageBeneficiaryCriteria == "failed")
            {
                list = dbEnrollClientManager.GetBeneficiarySummaries(2);
            } 
            else if (pageBeneficiaryCriteria == "uploaded")
            {
                cbSelectAll.Visible = false;
                btnSubmit.Visible = false;
                list = dbEnrollClientManager.GetBeneficiarySummaries(1);
            } 
            else if (pageBeneficiaryCriteria == "total")
            {
                cbSelectAll.Visible = false;
                btnSubmit.Visible = false;
                list = dbEnrollClientManager.GetBeneficiarySummaries(3);
            }

            if (list != null && list.Count > 0)
            {
                ShowResultList(list);
            }
        }

        private void ShowResultList(List<BeneficiarySummaryDto> list)
        {
            foreach(BeneficiarySummaryDto dto in list)
            {
                dgvBeneficiaryList.Rows.Add(false, dto.Serial, dto.ApplicationId, dto.BeneficiaryName, 
                    dto.Gender, dto.PhoneNo, dto.SelectionCriteria);
            }
        }

        private void dgvBeneficiaryList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            bool selected = (bool) dgvBeneficiaryList.Rows[rowIndex].Cells[0].Value;
            dgvBeneficiaryList.Rows[rowIndex].Cells[0].Value = selected != true ? true: false;
            dgvBeneficiaryList.Refresh();
        }

        private void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            bool selected = cbSelectAll.Checked;
            foreach (DataGridViewRow row in dgvBeneficiaryList.Rows)
            {
                row.Cells[0].Value = selected;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (dgvBeneficiaryList.Rows != null && dgvBeneficiaryList.Rows.Count > 0)
            {
                BatchRegisterBeneficiaryRequest request = new BatchRegisterBeneficiaryRequest();
                BatchRegisterBeneficiaryResponse response = new BatchRegisterBeneficiaryResponse();

                foreach (DataGridViewRow row in dgvBeneficiaryList.Rows)
                {
                    if (row.Cells[0].Value != null && (bool) row.Cells[0].Value == true)
                    {
                        string applicationId = row.Cells[2].Value != null ? row.Cells[2].Value.ToString() : null;
                        if (applicationId != null)
                        {
                            RegisterBeneficiaryRequest singleRequest = dbEnrollClientManager.getUnsyncedData(applicationId);
                            if (singleRequest != null)
                            {
                                request.beneficiaries.Add(singleRequest);
                            }
                        }
                    }
                }

                string errorMsg = null;

                if (request != null && request.beneficiaries != null && request.beneficiaries.Count > 0)
                {
                    //JavaScriptSerializer jSerial = new JavaScriptSerializer();
                    //jSerial.MaxJsonLength = 999999999;

                    //var json = jSerial.Serialize(request);
                    //Console.WriteLine("json: \n" + json);

                    ProcessingDialog.Run(delegate ()
                    {
                        try
                        {
                            response = beneficiaryApiManager.BatchProfileSubmit(request);
                        }
                        catch (WebException x)
                        {
                            logger.Debug("Known error, when server not found. Error Message: " + x.Message);
                            errorMsg = "Seems you are Offline. Please contact with your Administrator.";
                        }
                        catch (TimeoutException x)
                        {
                            logger.Debug("Connection timedout. Error Message: " + x.Message);
                            errorMsg = "Seems you are Offline. Please contact with your Administrator.";
                        }
                        catch (Exception x)
                        {
                            logger.Error("There was an unexpected error.\n" + x.ToString());
                            errorMsg = "There was an unexpected error. Please contact with your Administrator.";
                        }
                    });
                }

                if (!string.IsNullOrEmpty(errorMsg))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", errorMsg);
                    return;
                } 
                else
                {
                    InfoMessageBox.ShowMessage("SNSOP TOOLS", "Successfully uploaded "+request.beneficiaries.Count + " records");
                    if (response != null && response.applicationIds != null && response.applicationIds.Count > 0)
                    {
                        bool update = dbEnrollClientManager.updateIsSyncForUploadedBeneficiary(response.applicationIds);
                        if (!update)
                        {
                            ErrorMessageBox.ShowError("Could not update local db after successful upload", null);
                        }
                    }
                    this.DialogResult = DialogResult.OK;
                }

                counterPendingStatus.Count = dbEnrollClientManager.GetBeneficiaryUploadPendingCount();
                counterPendingStatus.Notify();
                counterErrorStatus.Count = dbEnrollClientManager.GetBeneficiaryUploadErrorCount();
                counterErrorStatus.Notify();
                counterDraftStatus.Count = dbEnrollClientManager.GetUploadedCount();
                counterDraftStatus.Notify();
            }
        }
    }
}
