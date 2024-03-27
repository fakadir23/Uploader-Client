using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ISTL.COMMON;
using ISTL.RAB.ControllersNew.Home.Report;
using NLog;
using ISTL.RAB.Entity;
using ISTL.RAB.Entity.Lookup;
using ISTL.MODELS.Request.Report;
using ISTL.RAB.ApiManager;
using System.Globalization;
using System.Diagnostics;
using System.Web.Script.Serialization;
using ISTL.MODELS.Response.Report;
using System.Net;
using ISTL.MODELS.DTO.Report;
using ISTL.RAB.DbManager;
using ISTL.MODELS.Common;
using ISTL.MODELS.Request.New.Report;
using ISTL.RAB.Controllers.New.Home.Report;
using ISTL.PERSOGlobals;

namespace ISTL.RAB.View.New.Report
{
    public partial class ReportUserControl : ViewUserControl
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private LookupItems lookupItems;
        private ReportApiManager reportApiManager;
        private DbReportManager dbReportManager;
        private int totalCount = 0;
        private int offset = 0;
        private int limit = 10;
        public ReportUserControl()
        {
            try
            {
                InitializeComponent();
                lookupItems = new LookupItems();
                reportApiManager = new ReportApiManager();
                dbReportManager = new DbReportManager();
                Init();
            }
            catch (Exception ex)
            {
                logger.Error("There was an error when init report form (Constructor). Error Message: " + ex.ToString());
            }
        }

        private void Init()
        {
            try
            {
                radioPDF.Checked = true;
                radioExcel.Checked = false;

                GetTotalRecords();
                lookupItems.LoadStations();
                lookupItems.LoadNationality();
                lookupItems.LoadCrimeType();
                EnableDisable();
                OnCheckReport();
                ShowReports(0);
                LoadReportType();
                LoadUnit();
                LoadCrimeType();
                LoadGender();
                LoadAgeRange();
                LoadAgeRange();
                LoadArrestType();
                LoadNationality();
            }
            catch(Exception ex)
            {
                logger.Error("There was an error when init report form (invoke all methods to load master data). Error Message: " + ex.ToString());
            }
        }

        private int GetTotalRecords()
        {
            ProcessingDialog.Run(delegate ()
            {
                totalCount = dbReportManager.GetTotalCountReport();
            });

            return totalCount;           
        }

        private void ResetData()
        {
            radioPDF.Checked = true;
            radioExcel.Checked = false;

            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;
            dtpCurrentDate.Value = DateTime.Now;

            cmbReportType.SelectedIndex = -1;
            cmbUnit.SelectedIndex = -1;
            cmbSubUnit.SelectedIndex = -1;
            cmbCrimeType.SelectedIndex = -1;
            cmbGender.SelectedIndex = -1;
            cmbAgeRange.SelectedIndex = -1;
            cmbArrestType.SelectedIndex = -1;
            cmbNationality.SelectedIndex = -1;

            cmbReportType.Hint = "Report Type";
            cmbUnit.Hint = "Unit";
            cmbSubUnit.Hint = "Sub Unit";
            cmbCrimeType.Hint = "Crime Type";
            cmbGender.Hint = "Gender";
            cmbAgeRange.Hint = "Age Range";
            cmbArrestType.Hint = "Arrest Type";
            cmbNationality.Hint = "Nationality";

            cmbUnit.Enabled = false;
            cmbSubUnit.Enabled = false;
            cmbCrimeType.Enabled = false;
            dtpFrom.Enabled = false;
            dtpTo.Enabled = false;
            dtpCurrentDate.Enabled = false;
            cmbGender.Enabled = false;
            cmbAgeRange.Enabled = false;
            cmbArrestType.Enabled = false;
        }

        private void EnableDisable()
        {
            cmbUnit.Enabled = false;
            cmbSubUnit.Enabled = false;
            cmbCrimeType.Enabled = false;
            dtpFrom.Enabled = false;
            dtpTo.Enabled = false;
            dtpCurrentDate.Enabled = false;
            cmbGender.Enabled = false;
            cmbAgeRange.Enabled = false;
            cmbArrestType.Enabled = false;
            cmbNationality.Enabled = false;
            chkEnableDateFilter.Checked = false;
            chkEnableDateFilter.Enabled = false;


            if (cmbReportType.SelectedIndex <= -1)
            {
                dtpFrom.Value = DateTime.Now;
                dtpTo.Value = DateTime.Now;
                return;
            }

            chkEnableDateFilter.Enabled = true;
            var reportTypeId = cmbReportType.SelectedValue.ToString();

            if (reportTypeId == "1") // Daily Enrollment Report
            {
                cmbUnit.Enabled = true;
                cmbSubUnit.Enabled = true;
                dtpCurrentDate.Enabled = true;
                cmbNationality.Enabled = true;
                //dtpFrom.Enabled = true;
                //dtpTo.Enabled = true;
            }
            else if (reportTypeId == "2") // Criminal Profile Report
            {
                cmbUnit.Enabled = true;
                cmbSubUnit.Enabled = true;
                cmbCrimeType.Enabled = true;
                cmbNationality.Enabled = true;
                //dtpFrom.Enabled = true;
                //dtpTo.Enabled = true;
                dtpCurrentDate.Enabled = false;
                cmbGender.Enabled = true;
                cmbAgeRange.Enabled = true;
                cmbArrestType.Enabled = false;
            }
            else if (reportTypeId == "3") // Special Criminal Profile Report
            {
                cmbUnit.Enabled = true;
                cmbSubUnit.Enabled = true;
                cmbCrimeType.Enabled = true;
                cmbNationality.Enabled = true;
                //dtpFrom.Enabled = true;
                //dtpTo.Enabled = true;
                dtpCurrentDate.Enabled = false;
                cmbGender.Enabled = true;
                cmbAgeRange.Enabled = false;
                cmbArrestType.Enabled = true;
            }
            else if (reportTypeId == "4") // Crime Type Wise Report
            {
                cmbUnit.Enabled = true;
                cmbSubUnit.Enabled = true;
                chkEnableDateFilter.Enabled = false;
            }
        }

        private void LoadReportType()
        {
            cmbReportType.DataSource = new BindingSource(ComboBoxItems.reportType, null);
            cmbReportType = Utils.GeneralComboBoxFormat(cmbReportType);
        }

        private void LoadUnit()
        {
            if (lookupItems.stationList?.Count > 0)
            {
                cmbUnit.DataSource = new BindingSource(lookupItems.stationList, null);
                cmbUnit = Utils.GeneralComboBoxFormat(cmbUnit);
            }
        }

        private void LoadNationality()
        {
            if (lookupItems.nationalityList != null)
            {
                if (lookupItems.nationalityList.Count > 0)
                {
                    cmbNationality.DataSource = new BindingSource(lookupItems.nationalityList, null);
                    cmbNationality = Utils.GeneralComboBoxFormat(cmbNationality);
                }
            }
        }

        private void LoadCrimeType()
        {
            //cmbCrimeType.DataSource = new BindingSource(ComboBoxItems.crimeType, null);
            //cmbCrimeType = Utils.GeneralComboBoxFormat(cmbCrimeType);
            if (lookupItems.crimeTypeList != null)
            {
                if (lookupItems.crimeTypeList.Count > 0)
                {
                    cmbCrimeType.DataSource = new BindingSource(lookupItems.crimeTypeList, null);
                    cmbCrimeType = Utils.GeneralComboBoxFormat(cmbCrimeType);
                }
            }
        }

        private void LoadGender()
        {
            cmbGender.DataSource = new BindingSource(ComboBoxItems.genders, null);
            cmbGender = Utils.GeneralComboBoxFormat(cmbGender);
        }

        private void LoadAgeRange()
        {
            cmbAgeRange.DataSource = new BindingSource(ComboBoxItems.ageRanges, null);
            cmbAgeRange = Utils.GeneralComboBoxFormat(cmbAgeRange);
        }

        private void LoadArrestType()
        {
            cmbArrestType.DataSource = new BindingSource(ComboBoxItems.arresteeType, null);
            cmbArrestType = Utils.GeneralComboBoxFormat(cmbArrestType);
        }

        private void cmbSubUnit_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbUnit.SelectedValue?.ToString()))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please select battalion first");
                cmbUnit.Focus();
            }
            else
            {
                lookupItems.LoadSubStations(cmbUnit.SelectedValue?.ToString());

                if (lookupItems.subStationList != null)
                {
                    if (lookupItems.subStationList.Count > 0)
                    {
                        cmbSubUnit.DataSource = new BindingSource(lookupItems.subStationList, null);
                        cmbSubUnit = Utils.GeneralComboBoxFormat(cmbSubUnit);
                    }
                }
            }
        }

        public bool Validatedata()
        {
            string errorMessage = "";

            if (cmbReportType.SelectedIndex <= -1)
            {
                errorMessage += "Report Type is required." + "\n";
            }

            if (errorMessage != "")
            {
                //MessageBox.Show("Please correct the following error(s):\n\n" + errorMessage,
                //    "RAB CDMS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please correct the following error(s):\n" + errorMessage);

                return false;
            }
            return true;
        }

        private void btnInitRequest_Click(object sender, EventArgs e)
        {
            if (!Validatedata()) return;

            InitReportRequest();
        }

        private void OnCheckReport()
        {
            List<ReportResult> reports = new List<ReportResult>();

            ProcessingDialog.Run(delegate ()
            {
                reports = GetReports("PENDING", offset);
            });

            if (reports == null || reports.Count <= 0) return;

            ReportResultRequest request = new ReportResultRequest();
            request.tokenList = reports.Select(x => x.token).ToList();

            ReportResultResponse response = GetReportResult(request);

            if (response?.code == (int)HttpResponseStatus.OK)
            {
                UpdateReports(response);
            }
            else
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an unexpected error while getting report result. Please contact with your Administrator.");
            }
        }

        private void InitReportRequest()
        {
            ReportResult request = new ReportResult();

            request.reportExtension = Globals.ReportExtension.PDF;

            if(radioPDF.Checked)
            {
                request.reportExtension = Globals.ReportExtension.PDF;
            }
            else if (radioExcel.Checked)
            {
                request.reportExtension = Globals.ReportExtension.EXCEL;
            }

            if (cmbReportType.SelectedIndex > -1)
            {
                request.reportType = Convert.ToInt32(cmbReportType.SelectedValue.ToString());
            }

            if (cmbUnit.SelectedIndex > -1)
            {
                request.unit = Convert.ToInt32(cmbUnit.SelectedValue.ToString());
            }

            if (cmbSubUnit.SelectedIndex > -1)
            {
                request.subUnit = Convert.ToInt32(cmbSubUnit.SelectedValue.ToString());
            }

            if (cmbCrimeType.SelectedIndex > -1)
            {
                request.crimeType = Convert.ToInt32(cmbCrimeType.SelectedValue.ToString());
            }

            if (cmbNationality.SelectedIndex > -1)
            {
                request.nationality = Convert.ToInt32(cmbNationality.SelectedValue.ToString());
            }

            string fromDate = null;
            string toDate = null;
            string curDate = null;

            if (chkEnableDateFilter.Checked && !string.IsNullOrWhiteSpace(dtpFrom.Text.Trim()))
            {
                try
                {
                    DateTime dt = DateTime.ParseExact(dtpFrom.Text.Trim(), "dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture);
                    //fromDate = dt.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                    fromDate = dt.ToString("yyyy-MM-dd HH:mm:ss");
                }
                catch (Exception)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please input correct From Date");
                    return;
                }
            }

            if (chkEnableDateFilter.Checked && !string.IsNullOrWhiteSpace(dtpTo.Text.Trim()))
            {
                try
                {
                    DateTime dt = DateTime.ParseExact(dtpTo.Text.Trim(), "dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture);
                    //toDate = dt.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                    toDate = dt.ToString("yyyy-MM-dd HH:mm:ss");
                }
                catch (Exception)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please input correct To Date");
                    return;
                }
            }

            if (chkEnableDateFilter.Checked && !string.IsNullOrWhiteSpace(dtpCurrentDate.Text.Trim()))
            {
                try
                {
                    DateTime dt = DateTime.ParseExact(dtpCurrentDate.Text.Trim(), "dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture);
                    //curDate = dt.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                    curDate = dt.ToString("yyyy-MM-dd HH:mm:ss");
                }
                catch (Exception)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please input correct Current Date");
                    return;
                }
            }

            request.creationDateFrom = fromDate;
            request.creationDateTo = toDate;
            request.currentDate = curDate;

            if (cmbGender.SelectedIndex > -1)
            {
                request.gender = Convert.ToInt32(cmbGender.SelectedValue.ToString());
            }

            if (cmbAgeRange.SelectedIndex > -1)
            {
                request.ageRange = cmbAgeRange.SelectedValue.ToString();
            }

            if (cmbArrestType.SelectedIndex > -1)
            {
                request.arrestType = Convert.ToInt32(cmbArrestType.SelectedValue.ToString());
            }

            string jsonRequst = new JavaScriptSerializer().Serialize(request);
            logger.Debug("Report request: \n" + jsonRequst);

            var reportTypeId = cmbReportType.SelectedValue.ToString();

            if (reportTypeId == "1") // Daily Enrollment Report
            {
                var response = InitDailyEnrollmentReport(request);

                if (response != null && response.code == (int)HttpStatusCode.OK)
                {
                    request.token = response.token;

                    AddReports(request);

                    OnCheckReport();

                    ShowReports(0);

                    ResetData();
                }
            }
            else if (reportTypeId == "2") // Criminal Profile Report
            {
                var response = InitCriminalProfileReport(request);

                if (response != null && response.code == (int)HttpStatusCode.OK)
                {
                    request.token = response.token;

                    AddReports(request);

                    OnCheckReport();

                    ShowReports(0);

                    ResetData();
                }
            }
            else if (reportTypeId == "3") // Special Criminal Profile Report
            {
                var response = InitSpecialCriminalProfileReport(request);

                if (response != null && response.code == (int)HttpStatusCode.OK)
                {
                    request.token = response.token;

                    AddReports(request);

                    OnCheckReport();

                    ShowReports(0);

                    ResetData();
                }
            }
            else if (reportTypeId == "4") // Crime Type Wise Report
            {
                var response = InitCrimeTypeWiseReport(request);

                if (response != null && response.code == (int)HttpStatusCode.OK)
                {
                    request.token = response.token;

                    AddReports(request);

                    OnCheckReport();

                    ShowReports(0);

                    ResetData();
                }
            }
        }

        private ReportResponse InitDailyEnrollmentReport(ReportResult request)
        {
            var response = new ReportResponse();

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = reportApiManager.InitDailyEnrollmentReport(request);
                }
                catch (WebException x)
                {
                    logger.Debug("Known error, when server not found while Initiating Daily Enrollment Report by Web API. Error Message: " + x.Message);
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Seems you are Offline while Initiating Daily Enrollment Report API. Please contact with your Administrator.");
                }
                catch (TimeoutException x)
                {
                    logger.Debug("Connection timedout while Initiating Daily Enrollment Report by Web API. Error Message: " + x.Message);
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Seems you are Offline while Initiating Daily Enrollment Report by Web API. Please contact with your Administrator.");
                }
                catch (Exception x)
                {
                    logger.Error("There was an unexpected error while Initiating Daily Enrollment Report by Web API.\n" + x.ToString());
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an unexpected error while Initiating Daily Enrollment Report by Web API. Please contact with your Administrator.");
                }
            });

            return response;
        }

        private ReportResponse InitCriminalProfileReport(ReportResult request)
        {
            var response = new ReportResponse();

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = reportApiManager.InitCriminalProfileReport(request);
                }
                catch (WebException x)
                {
                    logger.Debug("Known error, when server not found while Initiating Criminal Profile Report by Web API. Error Message: " + x.Message);
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Seems you are Offline while Initiating Criminal Profile Report by Web API. Please contact with your Administrator.");
                }
                catch (TimeoutException x)
                {
                    logger.Debug("Connection timedout while Initiating Criminal Profile Report by Web API. Error Message: " + x.Message);
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Seems you are Offline while Initiating Criminal Profile Report by Web API. Please contact with your Administrator.");
                }
                catch (Exception x)
                {
                    logger.Error("There was an unexpected error while Initiating Criminal Profile Report by Web API.\n" + x.ToString());
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an unexpected error while Initiating Criminal Profile Report by Web API. Please contact with your Administrator.");
                }
            });

            return response;
        }

        private ReportResponse InitSpecialCriminalProfileReport(ReportResult request)
        {
            var response = new ReportResponse();

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = reportApiManager.InitSpecialCriminalProfileReport(request);
                }
                catch (WebException x)
                {
                    logger.Debug("Known error, when server not found while Initiating Special Criminal Profile Report by Web API. Error Message: " + x.Message);
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Seems you are Offline while Initiating Special Criminal Profile Report by Web API. Please contact with your Administrator.");
                }
                catch (TimeoutException x)
                {
                    logger.Debug("Connection timedout while Initiating Special Criminal Profile Report by Web API. Error Message: " + x.Message);
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Seems you are Offline while Initiating Special Criminal Profile Report by Web API. Please contact with your Administrator.");
                }
                catch (Exception x)
                {
                    logger.Error("There was an unexpected error while Initiating Special Criminal Profile Report by Web API.\n" + x.ToString());
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an unexpected error while Special Criminal Profile Report by Web API. Please contact with your Administrator.");
                }
            });

            return response;
        }

        private ReportResponse InitCrimeTypeWiseReport(ReportResult request)
        {
            var response = new ReportResponse();

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = reportApiManager.InitCrimeTypeWiseReport(request);
                }
                catch (WebException x)
                {
                    logger.Debug("Known error, when server not found while Initiating Crime Type Wise Report by Web API. Error Message: " + x.Message);
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Seems you are Offline while Initiating Crime Type Wise Report by Web API. Please contact with your Administrator.");
                }
                catch (TimeoutException x)
                {
                    logger.Debug("Connection timedout while Initiating Crime Type Wise Report by Web API. Error Message: " + x.Message);
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Seems you are Offline while Initiating Crime Type Wise Report by Web API. Please contact with your Administrator.");
                }
                catch (Exception x)
                {
                    logger.Error("There was an unexpected error while Initiating Crime Type Wise Report by Web API.\n" + x.ToString());
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an unexpected error while Crime Type Wise Report by Web API. Please contact with your Administrator.");
                }
            });

            return response;
        }

        private ReportResultResponse GetReportResult(ReportResultRequest request)
        {
            var response = new ReportResultResponse();

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = reportApiManager.GetReportResult(request);
                }
                catch (WebException x)
                {
                    logger.Debug("Known error, when server not found while Initiating Daily Criminal Enrollment Report by Web API. Error Message: " + x.Message);
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Seems you are Offline while Initiating Daily Criminal Enrollment Report by Web API. Please contact with your Administrator.");
                }
                catch (TimeoutException x)
                {
                    logger.Debug("Connection timedout while Initiating Daily Criminal Enrollment Report by Web API. Error Message: " + x.Message);
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Seems you are Offline while Initiating Daily Criminal Enrollment Report by Web API. Please contact with your Administrator.");
                }
                catch (Exception x)
                {
                    logger.Error("There was an unexpected error while Initiating Daily Criminal Enrollment Report by Web API.\n" + x.ToString());
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an unexpected error while Initiating Daily Criminal Enrollment Report by Web API. Please contact with your Administrator.");
                }
            });

            return response;
        }

        private ApiResponse DeleteReport(List<string> tokenList)
        {
            var response = new ApiResponse();
            var request = new ReportDeleteRequest()
            {
                tokenList = tokenList
            };

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = reportApiManager.DeleteReport(request);
                }
                catch (WebException x)
                {
                    logger.Debug("Known error, when server not found while deleting a Report by Web API. Error Message: " + x.Message);
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Seems you are Offline while deleting a Report by Web API. Please contact with your Administrator.");
                }
                catch (TimeoutException x)
                {
                    logger.Debug("Connection timedout while deleting a Report by Web API. Error Message: " + x.Message);
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Seems you are Offline while deleting a Report by Web API. Please contact with your Administrator.");
                }
                catch (Exception x)
                {
                    logger.Error("There was an unexpected error while deleting a Report by Web API.\n" + x.ToString());
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an unexpected error while deleting a Report by Web API. Please contact with your Administrator.");
                }
            });

            return response;
        }

        private void UpdateReports(ReportResultResponse reports)
        {
            var ret = dbReportManager.UpdateReports(reports.reportResultList);
        }

        private void ShowReports(int pos)
        {
            try
            {
                List<ReportResult> list = new List<ReportResult>();
                dgvList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                ProcessingDialog.Run(delegate ()
                {
                    list = GetReports("ALL", pos);
                });

                if (list == null || list.Count <= 0) return;

                dgvList.Rows.Clear();

                foreach (var obj in list)
                {
                    dgvList.Rows.Add(new object[]
                    {
                        obj.id,
                        obj.createdAt,
                        GetReportType(obj.reportType),
                        obj.reportExtension,
                        GetUnit(obj.unit),
                        GetSubUnit(obj.unit.ToString(), obj.subUnit),
                        obj.token,
                        obj.url,
                        obj.status
                    });
                }
                dgvList.ClearSelection();

                labelTotalRecords.Text =GetTotalRecords().ToString();
            }
            catch (Exception ex)
            {
                logger.Error("There was an error when preparing Report List. Error Message:\n" + ex.ToString());
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an error when preparing Report List. Please contact with your Administrator.");
            }
        }

        private void AddReports(ReportResult report)
        {
            if (!string.IsNullOrWhiteSpace(report.creationDateFrom))
            {
                report.creationDateFromDt = GetDate(report.creationDateFrom);
            }

            if (!string.IsNullOrWhiteSpace(report.creationDateTo))
            {
                report.creationDateToDt = GetDate(report.creationDateTo);
            }

            if (!string.IsNullOrWhiteSpace(report.currentDate))
            {
                report.currentDateDt = GetDate(report.currentDate);
            }

            var ret = dbReportManager.AddReport(report);
        }

        private DateTime GetDate(string strDate)
        {
            DateTime myDate = new DateTime();
            try
            {
                var formatStrings = new string[] { "yyyy-MM-dd", "yyyy-MM-ddTHH:mm:ss.fffZ", "yyyy-MM-ddTHH:mm:ss.fffK" };
                DateTime.TryParseExact(strDate, formatStrings, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out myDate);
            }
            catch (Exception) { }
            return myDate;
        }

        private List<ReportResult> GetReports(string filter, int pos)
        {
            var list = dbReportManager.GetReports(filter, pos, limit);
            return list;
        }

        private void cmbReportType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            EnableDisable();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            OnCheckReport();
            ShowReports(0);
        }

        private string GetReportType(int? key)
        {
            if (!key.HasValue) return null;

            var value = ComboBoxItems.reportType.Where(x => x.Key == key).Select(x => x.Value).FirstOrDefault();
            return value;
        }

        private string GetUnit(int? key)
        {
            if (key.HasValue && key.Value == -1) return "All";

            if (!key.HasValue) return null;

            var value = lookupItems.stationList.Where(x => x.Key == Convert.ToInt32(key)).Select(x => x.Value).FirstOrDefault();
            return value;
        }

        private string GetSubUnit(string unitId, int? key)
        {
            if (key.HasValue && key.Value == -1) return "All";

            if (!key.HasValue) return null;

            lookupItems.LoadSubStations(unitId);
            var value = lookupItems.subStationList.Where(x => x.Key == key).Select(x => x.Value).FirstOrDefault();
            return value;
        }

        private void dgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex <= -1)
            {
                dgvList.ClearSelection();
                return;
            }

            int index = this.dgvList.Rows[e.RowIndex].Index;

            if (e.ColumnIndex == 9)
            {
                var url = dgvList.Rows[index].Cells[7].Value != null ? dgvList.Rows[index].Cells[7].Value?.ToString() : "";
                var status = dgvList.Rows[index].Cells[8].Value != null ? dgvList.Rows[index].Cells[8].Value?.ToString() : "";

                if (!string.IsNullOrWhiteSpace(status)
                    && status == "TRUE"
                    && !string.IsNullOrWhiteSpace(url))
                {
                    Process.Start(url);
                }
                else
                {
                    //MessageBox.Show("This report is not ready or it has error during generation. Please click on 'Refresh' button below.");
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "This report is not ready or it has error during generation. " +
                        "Please click on 'Refresh' button below.");
                }
            }

            if (e.ColumnIndex == 10)
            {
                //if (MessageBox.Show("Are you sure you want to remove this Report?", "Confirm Remove Report", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                DialogResult dr = YesNoMessageBox.YesNoMessageBoxResult("RAB CDMS", "Are you sure you want to remove this report?");
                if (dr == DialogResult.No)
                {
                    return;
                }

                try
                {
                    var token = dgvList.Rows[index].Cells[6].Value != null ? dgvList.Rows[index].Cells[6].Value?.ToString() : "";
                    var list = new List<string>();
                    list.Add(token);
                    var response = DeleteReport(list);

                    if (response.code == (int)HttpStatusCode.OK)
                    {
                        dbReportManager.DeleteReport(token);
                        dgvList.Rows.RemoveAt(index);
                        OnCheckReport();
                        ShowReports(0);
                        InfoMessageBox.ShowMessage("SNSOP TOOLS", "Report is deleted successfully.");
                    }
                }
                catch(Exception ex)
                {
                    logger.Error("There was an unexpected error while deleting report. Error Message:\n" + ex.ToString());
                    //MessageBox.Show("There was an error while deleting this Report. Please contact with your System Administrator.");
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an error while deleting this Report. " +
                        "Please contact with your System Administrator.");
                }                
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            GetTotalRecords();
            offset = 0;
            OnCheckReport();
            ShowReports(offset);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            GetTotalRecords();
            if (offset >= limit) offset -= limit;
            OnCheckReport();
            ShowReports(offset);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            GetTotalRecords();
            int calTotal = totalCount;
            if (offset < calTotal - limit) offset += limit;
            OnCheckReport();
            ShowReports(offset);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            GetTotalRecords();
            
            int calTotal = totalCount;
            if (calTotal % limit != 0) offset = (calTotal / limit) * limit;
            else if (calTotal % limit == 0) offset = ((calTotal / limit) - 1) * limit;

            OnCheckReport();
            ShowReports(offset);
        }

        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSubUnit.DataSource = null;
        }

        private void lblUIName_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void labelTotalRecords_Click(object sender, EventArgs e)
        {

        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            ((ReportController)controller).GoBacktoDashboard();
        }

        private void chkEnableDateFilter_Click(object sender, EventArgs e)
        {
            if (chkEnableDateFilter.Checked)
            {
                dtpFrom.Enabled = true;
                dtpTo.Enabled = true;
            }
            else
            {
                dtpFrom.Value = DateTime.Now;
                dtpTo.Value = DateTime.Now;

                dtpFrom.Enabled = false;
                dtpTo.Enabled = false;
            }
        }
    }
}
