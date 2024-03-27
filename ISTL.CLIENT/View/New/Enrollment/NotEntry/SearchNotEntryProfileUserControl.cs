using ISTL.COMMON;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.MODELS.DTO.New.NotEntry;
using ISTL.MODELS.Request.NotEntry;
using ISTL.MODELS.Response.NotEntry;
using ISTL.RAB.ApiManager;
using ISTL.RAB.Controllers.New.Enrollment.NotEntry;
using ISTL.RAB.Entity;
using ISTL.RAB.Entity.Lookup;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISTL.RAB.View.New.Enrollment.NotEntry
{
    public partial class SearchNotEntryProfileUserControl : ViewUserControl
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private NotEntryApiManager notEntryApiManager;
        public SearchNotEntryProfileUserControl()
        {
            InitializeComponent();

            notEntryApiManager = new NotEntryApiManager();
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            LoadLookupItems();

            dtpCreationFrom.CustomFormat = "dd/MM/yyyy hh:mm tt";
            dtpCreationTo.CustomFormat = "dd/MM/yyyy hh:mm tt";
            dtpUploadFrom.CustomFormat = "dd/MM/yyyy hh:mm tt";
            dtpUploadTo.CustomFormat = "dd/MM/yyyy hh:mm tt";

            btnSearch_Click(null, null);
        }

        private void LoadLookupItems()
        {
            LookupItems lookupItems = new LookupItems();

            lookupItems.LoadDistrict();
            if (lookupItems.districtList != null)
            {
                if (lookupItems.districtList.Count > 0)
                {
                    cmbDistrict.DataSource = new BindingSource(lookupItems.districtList, null);
                    cmbDistrict = Utils.GeneralComboBoxFormat(cmbDistrict);
                }
            }

            lookupItems.LoadStations();
            if (lookupItems.stationList != null)
            {
                if (lookupItems.stationList.Count > 0)
                {
                    cmbUnit.DataSource = new BindingSource(lookupItems.stationList, null);
                    cmbUnit = Utils.GeneralComboBoxFormat(cmbUnit);
                }
            }

            cmbCaseType.DataSource = new BindingSource(ComboBoxItems.caseType, null);
            cmbCaseType = Utils.GeneralComboBoxFormat(cmbCaseType);

            cmbNotEntryReason.DataSource = new BindingSource(ComboBoxItems.notEntryReason, null);
            cmbNotEntryReason = Utils.GeneralComboBoxFormat(cmbNotEntryReason);
        }

        private void cmbSubUnit_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbUnit.SelectedValue?.ToString()))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please select unit first");
                cmbUnit.Focus();
            }
            else
            {
                LookupItems lookupItems = new LookupItems();
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

        private void cmbUpazilla_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbDistrict.SelectedValue?.ToString()))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please select District first");
                cmbDistrict.Focus();
            }
            else
            {
                LookupItems lookupItems = new LookupItems();
                lookupItems.LoadUpazillaByDistrict(cmbDistrict.SelectedValue?.ToString());

                if (lookupItems.upazillaList != null)
                {
                    if (lookupItems.upazillaList.Count > 0)
                    {
                        cmbUpazilla.DataSource = new BindingSource(lookupItems.upazillaList, null);
                        cmbUpazilla = Utils.GeneralComboBoxFormat(cmbUpazilla);
                    }
                }
            }
        }

        private void cmbUnion_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbDistrict.SelectedValue?.ToString()))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please select Upazilla/Thana first");
                cmbUpazilla.Focus();
            }
            else
            {
                LookupItems lookupItems = new LookupItems();
                lookupItems.LoadUnionByUpazilla(cmbUpazilla.SelectedValue?.ToString());

                if (lookupItems.unionList != null)
                {
                    if (lookupItems.unionList.Count > 0)
                    {
                        cmbUnion.DataSource = new BindingSource(lookupItems.unionList, null);
                        cmbUnion = Utils.GeneralComboBoxFormat(cmbUnion);
                    }
                }
            }
        }

        private int startIndex = 0;
        private int totalCount = 0;

        private void btnSearch_Click(object sender, EventArgs e)
        {
            startIndex = 0;
            OnSearch(startIndex);
        }

        private void OnSearch(int startIndex)
        {
            dgvList.Rows.Clear();
            labelTotalRecords.Text = "0";
            totalCount = 0;

            if (string.IsNullOrEmpty(Users.AccessToken))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Seems you are Offline while searching not entry. Please contact with your Administrator.");
                return;
            }

            NotEntrySearchRequest request = new NotEntrySearchRequest();
            NotEntrySearchResponse response = new NotEntrySearchResponse();

            request.limit = 10;
            request.startIndex = startIndex;

            if (!string.IsNullOrEmpty(cmbUnit.SelectedValue?.ToString()))
            {
                request.unit = Convert.ToInt32(cmbUnit.SelectedValue?.ToString());
            }
            if (!string.IsNullOrEmpty(cmbSubUnit.SelectedValue?.ToString()))
            {
                request.subUnit = Convert.ToInt32(cmbSubUnit.SelectedValue?.ToString());
            }

            request.referenceNo = (!string.IsNullOrEmpty(tbRefNo.Text)) ? tbRefNo.Text : null;
            request.accusedName = (!string.IsNullOrEmpty(tbFullName.Text)) ? tbFullName.Text : null;

            if (!string.IsNullOrEmpty(cmbCaseType.SelectedValue?.ToString()))
            {
                request.notEntryCaseType = cmbCaseType.SelectedIndex + 1;
            }
            if (!string.IsNullOrEmpty(cmbNotEntryReason.SelectedValue?.ToString()))
            {
                request.notEntryReason = cmbNotEntryReason.SelectedIndex + 1;
            }            

            AddressDto addressDto = new AddressDto();
            if (!string.IsNullOrEmpty(cmbDistrict.SelectedValue?.ToString()))
            {
                addressDto.district = Convert.ToInt32(cmbDistrict.SelectedValue?.ToString());
                if (!string.IsNullOrEmpty(cmbUpazilla.SelectedValue?.ToString()))
                {
                    addressDto.upazila = Convert.ToInt32(cmbUpazilla.SelectedValue?.ToString());
                    if (!string.IsNullOrEmpty(cmbUnion.SelectedValue?.ToString()))
                    {
                        addressDto.union = Convert.ToInt32(cmbUnion.SelectedValue?.ToString());
                    }
                }
            }
            addressDto.villageHouseRoadNo = (!string.IsNullOrEmpty(tbVillage.Text)) ? tbVillage.Text : null;
            request.address = addressDto;

            if (dtpCreationFrom.Enabled)
            {
                try
                {
                    DateTime dtFrom = DateTime.ParseExact(dtpCreationFrom.Text, "dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture);
                    request.creationDateFrom = dtFrom.ToString("yyyy-MM-dd HH:mm:ss");
                }
                catch { }
            }
            else
            {
                request.creationDateFrom = null;
            }
            if (dtpCreationTo.Enabled)
            {
                try
                {
                    DateTime dtTo = DateTime.ParseExact(dtpCreationTo.Text, "dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture);
                    request.creationDateTo = dtTo.ToString("yyyy-MM-dd HH:mm:ss");
                }
                catch { }
            }
            else
            {
                request.creationDateTo = null;
            }

            if (dtpUploadFrom.Enabled)
            {
                try
                {
                    DateTime dtFrom = DateTime.ParseExact(dtpUploadFrom.Text, "dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture);
                    request.uploadedDateFrom = dtFrom.ToString("yyyy-MM-dd HH:mm:ss");
                }
                catch { }
            }
            else
            {
                request.uploadedDateFrom = null;
            }
            if (dtpUploadTo.Enabled)
            {
                try
                {
                    DateTime dtTo = DateTime.ParseExact(dtpUploadTo.Text, "dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture);
                    request.uploadedDateTo = dtTo.ToString("yyyy-MM-dd HH:mm:ss");
                }
                catch { }
            }
            else
            {
                request.uploadedDateTo = null;
            }

            string errorMsg = null;

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = notEntryApiManager.NotEntrySearch(request);
                }
                catch (WebException x)
                {
                    logger.Debug("Known error, when server not found when searching criminal. Error Message: " + x.Message);
                    errorMsg = "Seems you are Offline while searching criminal. Please contact with your Administrator.";
                }
                catch (TimeoutException x)
                {
                    logger.Debug("Connection timedout when searching criminal. Error Message: " + x.Message);
                    errorMsg = "Seems you are Offline while searching criminal. Please contact with your Administrator.";
                }
                catch (Exception x)
                {
                    logger.Error("There was an unexpected error when searching criminal.\n" + x.ToString());
                    errorMsg = "There was an unexpected error when searching criminal. Please contact with your Administrator.";
                }
            });

            if (!string.IsNullOrEmpty(errorMsg))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", errorMsg);
            }

            if (response != null)
            {
                if (response.code == 200)
                {
                    labelTotalRecords.Text = "" + response.total;
                    if (response.noEntryList?.Count > 0)
                    {
                        totalCount = Convert.ToInt32(response.total);
                        ShowNotEntryList(response.noEntryList);
                    }                    
                }
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {            
            startIndex = 0;
            OnSearch(startIndex);
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (startIndex > 0) startIndex -= 1;
            OnSearch(startIndex);
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (startIndex < (totalCount / 10))
            {
                startIndex += 1;
                if ((totalCount % 10 == 0) && (startIndex == totalCount / 10)) startIndex -= 1;
            }
            OnSearch(startIndex);
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            if (totalCount % 10 != 0) startIndex = totalCount / 10;
            else if (totalCount % 10 == 0) startIndex = (totalCount / 10) - 1;
            OnSearch(startIndex);
        }

        private void ShowNotEntryList(List<NotEntryDto> list)
        {
            for (int i = 0; i < list?.Count; i++)
            {
                int index = (startIndex * 10) + i + 1;

                string reason = null;
                ComboBoxItems.notEntryReason.TryGetValue(Convert.ToString(list[i].notEntryReason), out reason);

                string caseType = null;
                ComboBoxItems.caseType.TryGetValue(Convert.ToString(list[i].notEntryCaseType), out caseType);

                dgvList.Rows.Add(index, list[i].referenceNo, list[i].accusedName, reason, caseType, "Preview", "Edit", list[i].createdBy);

                if (list[i].createdBy == Users.Id)
                {
                    dgvList.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(202, 214, 192);
                }
            }
        }

        private void chkUploadDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUploadDate.Checked)
            {
                dtpUploadFrom.Enabled = true;
                dtpUploadTo.Enabled = true;
            }
            else
            {
                dtpUploadFrom.Enabled = false;
                dtpUploadTo.Enabled = false;
            }
        }

        private void chkCreationDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCreationDate.Checked)
            {
                dtpCreationFrom.Enabled = true;
                dtpCreationTo.Enabled = true;
            }
            else
            {
                dtpCreationFrom.Enabled = false;
                dtpCreationTo.Enabled = false;
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            ((SearchNotEntryProfileController)controller).GoBackToDashboard();
        }

        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSubUnit.DataSource = null;
        }

        private void cmbDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbUpazilla.DataSource = null;
            cmbUnion.DataSource = null;
        }

        private void cmbUpazilla_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbUnion.DataSource = null;
        }

        private void dgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 6)
            {
                if (Convert.ToInt32(dgvList.CurrentRow.Cells[7]?.Value?.ToString()) == Users.Id)
                {
                    string refNo = dgvList.CurrentRow.Cells[1]?.Value?.ToString();
                    ((SearchNotEntryProfileController)controller).GetNotEntryProfile(refNo, 1);
                }
                else
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "You are not authorized to edit this profile");
                    return;
                }
            }
            else if (e.ColumnIndex == 5)
            {
                string refNo = dgvList.CurrentRow.Cells[1]?.Value?.ToString();
                ((SearchNotEntryProfileController)controller).GetNotEntryProfile(refNo, 2);
            }
        }
    }
}
