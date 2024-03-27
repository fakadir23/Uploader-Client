using ISTL.COMMON.Subscription;
using ISTL.COMMON.Threads;
using ISTL.MODELS.DTO.New.Enrollment.BECverify;
using ISTL.MODELS.Request.New;
using ISTL.MODELS.Response.New;
using ISTL.RAB.ApiManager;
using ISTL.RAB.Asynch;
using ISTL.RAB.Controllers.New.Home;
using ISTL.RAB.DbManager;
using ISTL.RAB.Entity;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISTL.RAB.View.New.Enrollment.BiometricInformation
{
    public partial class NidSearchForm : Form
    {
        // Drop shadow souce code, got from:
        // http://www.codeproject.com/Articles/19277/Let-Your-Form-Drop-a-Shadow

        // Define the CS_DROPSHADOW constant
        private const int CS_DROPSHADOW = 0x00020000;

        // Override the CreateParams property
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        private Logger logger = LogManager.GetCurrentClassLogger();
        private int totalCount = 0;
        private int offset = 0;
        private int limit = 10;
        NidSearchSubject nidSearchSubject = (NidSearchSubject)SubjectFactory.GetInstance().GetSubject(NidSearchSubject.Name);
        private BECApiManager becApiManager = new BECApiManager();
        private DbBecManager dbBecManager = new DbBecManager();

        public NidSearchForm()
        {
            InitializeComponent();

            // Stop bec server nid search result check
            ThreadHandler.GetInstance(new BecNidIdentificationAsynch()).StopThread();

            // Get pending count
            var pendingList = dbBecManager.GetRequestList("PENDING", 0, 0);

            if(pendingList != null && pendingList.Count <= 0)
            {
                nidSearchSubject.State = NidSearchSubject.Status.IDLE;
                nidSearchSubject.Notify();
            }

            ShowRecords(0);
        }

        private void ShowRecords(int pos)
        {
            try
            {
                dgvList.Rows.Clear();

                List<BECvoterInfoDto> list = new List<BECvoterInfoDto>();
                dgvList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                ProcessingDialog.Run(delegate ()
                {
                    list = GetRecords("ALL", pos);
                });

                if (list == null || list.Count <= 0) return;

                foreach (var obj in list)
                {
                    dgvList.Rows.Add(new object[]
                    {
                        obj.token,
                        obj.status,
                        obj.id,
                        obj.createdAtCustom,
                        GetStatus(Convert.ToInt32(obj.status))
                    });
                }
                dgvList.ClearSelection();

                labelTotalRecords.Text = GetTotalRecords().ToString();
            }
            catch (Exception ex)
            {
                logger.Error("There was an error when preparing Report List. Error Message:\n" + ex.ToString());
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an error when preparing Report List. Please contact with your Administrator.");
            }
        }

        private string GetStatus(int id)
        {
            string status = "N/A";

            if (id == (int)NidSearchSubject.Status.NOT_FOUND)
            {
                status = "Match Not Found";
            }
            else if (id == (int)NidSearchSubject.Status.FOUND)
            {
                status = "Match Found";
            }
            else if (id == (int)NidSearchSubject.Status.FAILED)
            {
                status = "Match Failed";
            }
            else if (id == (int)NidSearchSubject.Status.PENDING)
            {
                status = "Match Pending";
            }
            return status;
        }

        private List<BECvoterInfoDto> GetRecords(string filter, int pos)
        {
            var list = dbBecManager.GetRequestList(filter, pos, limit);
            return list;
        }

        private void dgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex <= -1)
            {
                dgvList.ClearSelection();
                return;
            }

            int index = this.dgvList.Rows[e.RowIndex].Index;
            var token = dgvList.Rows[index].Cells[0].Value != null ? dgvList.Rows[index].Cells[0].Value?.ToString() : "";
            var status = dgvList.Rows[index].Cells[1].Value != null ? dgvList.Rows[index].Cells[1].Value?.ToString() : "";

            if (e.ColumnIndex == 5)
            {
                if (!string.IsNullOrWhiteSpace(token)
                    && !string.IsNullOrWhiteSpace(token) && Convert.ToInt32(status) == (int)NidSearchSubject.Status.FOUND)
                {
                    MatchResultController matchResultController = new MatchResultController();
                    matchResultController.matchResultForm.MatchedByNID = true;
                    matchResultController.matchResultForm.openedFromUI = 0;
                    matchResultController.becDataResponse = null;
                    matchResultController.voterInfoList = dbBecManager.GetRequestList(null, 0, 0, token);
                    DialogResult dr = matchResultController.matchResultForm.ShowDialog();
                }
                else if(!string.IsNullOrWhiteSpace(token)
                    && !string.IsNullOrWhiteSpace(token) && Convert.ToInt32(status) == (int)NidSearchSubject.Status.PENDING)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Search by Fingerprint in NID Server is pending for this record!");
                }
                else if (!string.IsNullOrWhiteSpace(token)
                    && !string.IsNullOrWhiteSpace(token) && Convert.ToInt32(status) == (int)NidSearchSubject.Status.FAILED)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Search by Fingerprint in NID Server is failed for this record!");
                }
                else
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Search by Fingerprint in NID Server is not matched for this record!");
                }
            }

            if (e.ColumnIndex == 6)
            {
                DialogResult dr = YesNoMessageBox.YesNoMessageBoxResult("RAB CDMS", "Are you sure you want to remove this record?");
                if (dr == DialogResult.No)
                {
                    return;
                }

                try
                {
                    dbBecManager.DeleteRequest(token);
                    ShowRecords(0);
                    InfoMessageBox.ShowMessage("SNSOP TOOLS", "Record is deleted successfully.");
                }
                catch (Exception ex)
                {
                    logger.Error("There was an unexpected error while deleting record. Error Message:\n" + ex.ToString());
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an error while deleting this record. " +
                        "Please contact with your System Administrator.");
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            // Get pending count
            var pendingList = dbBecManager.GetRequestList("PENDING", 0, 0);

            if (pendingList != null && pendingList.Count > 0)
            {
                // Start bec server nid search result check
                ThreadHandler.GetInstance(new BecNidIdentificationAsynch()).StartThread();
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {               
                ProcessingDialog.Run(delegate ()
                {
                    Invoke((MethodInvoker)delegate
                    {
                        GetNidSearhresult();
                        ShowRecords(0);
                    });
                });
            }
            catch (Exception ex)
            {
                logger.Error("There was an critical error when refresing the process: search by fingerprint in nid server in background. Error Message: " + ex.Message);
            }
        }

        private bool GetNidSearhresult()
        {           
            try
            {
                var matchNotFoundList = new List<BECvoterInfoDto>();
                var list = dbBecManager.GetRequestList("PENDING", 0, 0);

                if (list.Count <= 0) return true;

                nidSearchSubject.State = NidSearchSubject.Status.PENDING;
                nidSearchSubject.Notify();

                GetBECidentifyRequest request = new GetBECidentifyRequest();
                GetBECidentifyResponse response = new GetBECidentifyResponse();

                foreach (var obj in list)
                {
                    request.token = obj.token;
                    response = becApiManager.GetProfileResultByNidBiometric(request);

                    if (response != null && response.code == (int)HttpResponseStatus.BEC_NID_IDENTIFICATION_ERROR)
                    {
                        // Match failed or error
                        nidSearchSubject.State = NidSearchSubject.Status.FAILED;
                        nidSearchSubject.Notify();

                        matchNotFoundList.Add(obj);
                        dbBecManager.UpdateRequest(matchNotFoundList, (int)NidSearchSubject.Status.FAILED,
                            obj.createdAt,
                            null, obj.userId, obj.token);

                        Thread.Sleep(1000);

                        return true;
                    }

                    if (response != null && response.code == (int)HttpResponseStatus.OK)
                    {
                        if (response.payloads != null && response.payloads.Count > 0)
                        {
                            // Match found
                            nidSearchSubject.State = NidSearchSubject.Status.FOUND;
                            nidSearchSubject.Notify();

                            dbBecManager.UpdateRequest(response.payloads, (int)NidSearchSubject.Status.FOUND,
                                obj.createdAt,
                                DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffK"), obj.userId, obj.token);

                            Thread.Sleep(1000);
                        }
                        else
                        {
                            // Match not found
                            nidSearchSubject.State = NidSearchSubject.Status.NOT_FOUND;
                            nidSearchSubject.Notify();

                            matchNotFoundList.Add(obj);
                            dbBecManager.UpdateRequest(matchNotFoundList, (int)NidSearchSubject.Status.NOT_FOUND,
                                obj.createdAt,
                                null, obj.userId, obj.token);

                            Thread.Sleep(1000);
                        }
                    }                    
                }
            }
            catch (ThreadAbortException x)
            {
                logger.Debug("Inside BEC NID IDENTIFICATION RESULT SENDER: Force shutdown initiated! ThreadAbortException: " + x.Message);
                return false;
            }
            catch (System.Net.WebException x)
            {
                // Network Error.
                logger.Debug("Known error, when server not found during BEC NID IDENTIFICATION RESULT FINDER.\n" + x.Message);
                nidSearchSubject.State = NidSearchSubject.Status.FAILED;
                nidSearchSubject.Notify();
            }
            catch (TimeoutException x)
            {
                logger.Debug("Connection timedout during sending BEC NID IDENTIFICATION RESULT FINDER.\n" + x.Message);
                nidSearchSubject.State = NidSearchSubject.Status.FAILED;
                nidSearchSubject.Notify();
            }
            catch (System.Exception x)
            {
                logger.Error("There was an unexpected error when BEC NID IDENTIFICATION RESULT FINDER.\n" + x.ToString());
                ErrorMessageBox.ShowError("There was an unexpected error by BEC NID IDENTIFICATION RESULT FINDER.", x);
                nidSearchSubject.State = NidSearchSubject.Status.FAILED;
                nidSearchSubject.Notify();
            }
            return true;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            int calTotal = totalCount;
            if (calTotal % limit != 0) offset = (calTotal / limit) * limit;
            else if (calTotal % limit == 0) offset = ((calTotal / limit) - 1) * limit;

            ShowRecords(offset);
        }

        private int GetTotalRecords()
        {
            ProcessingDialog.Run(delegate ()
            {
                totalCount = dbBecManager.GetTotalCount();
            });
            return totalCount;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int calTotal = totalCount;
            if (offset < calTotal - limit) offset += limit;
            ShowRecords(offset);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (offset >= limit) offset -= limit;
            ShowRecords(offset);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            offset = 0;
            ShowRecords(offset);
        }
    }
}
