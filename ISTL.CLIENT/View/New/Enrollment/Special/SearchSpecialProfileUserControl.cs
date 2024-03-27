using ISTL.COMMON;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.MODELS.Request.New.Enrollment;
using ISTL.MODELS.Response.New.Special;
using ISTL.RAB.ApiManager;
using ISTL.RAB.Controllers.New.Enrollment.Special;
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

namespace ISTL.RAB.View.New.Enrollment.Special
{
    public partial class SearchSpecialProfileUserControl : ViewUserControl
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private LookupItems lookupItems;
        private GetSpecialProfileRequest request;
        private GetSpecialProfileResponse response;
        public SearchSpecialProfileUserControl()
        {
            InitializeComponent();

            lookupItems = new LookupItems();
            LoadComboBox();

            request = new GetSpecialProfileRequest();
            response = new GetSpecialProfileResponse();
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            btnSearch_Click(null, null);

            dtpFrom.CustomFormat = "dd/MM/yyyy hh:mm tt";
            dtpTo.CustomFormat = "dd/MM/yyyy hh:mm tt";
        }


        private void LoadComboBox()
        {
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

            cmbArrestType.DataSource = new BindingSource(ComboBoxItems.arresteeType, null);
            cmbArrestType = Utils.GeneralComboBoxFormat(cmbArrestType);

            cmbCrimeType.DataSource = new BindingSource(ComboBoxItems.crimeType, null);
            cmbCrimeType = Utils.GeneralComboBoxFormat(cmbCrimeType);

            cmbGender.DataSource = new BindingSource(ComboBoxItems.genders, null);
            cmbGender = Utils.GeneralComboBoxFormat(cmbGender);
        }

        private void cmbUpazilla_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbDistrict.SelectedValue?.ToString()))
            {
                cmbDistrict.Focus();
            }
            else
            {
                LookupItems LUItems = new LookupItems();
                LUItems.LoadUpazillaByDistrict(cmbDistrict.SelectedValue?.ToString());

                if (LUItems.upazillaList != null)
                {
                    if (LUItems.upazillaList.Count > 0)
                    {
                        cmbUpazilla.DataSource = new BindingSource(LUItems.upazillaList, null);
                        cmbUpazilla = Utils.GeneralComboBoxFormat(cmbUpazilla);
                    }
                }
            }
        }

        private void cmbUnion_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbDistrict.SelectedValue?.ToString()))
            {
                cmbUpazilla.Focus();
            }
            else
            {
                LookupItems LUItems = new LookupItems();
                LUItems.LoadUnionByUpazilla(cmbUpazilla.SelectedValue?.ToString());

                if (LUItems.unionList != null)
                {
                    if (LUItems.unionList.Count > 0)
                    {
                        cmbUnion.DataSource = new BindingSource(LUItems.unionList, null);
                        cmbUnion = Utils.GeneralComboBoxFormat(cmbUnion);
                    }
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            startIndexPos = 0;
            OnSearch(startIndexPos);
        }

        private void OnSearch(int pos)
        {
            if (string.IsNullOrEmpty(Users.AccessToken))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Seems you are Offline while searching criminal. Please contact with your Administrator.");
                return;
            }

            request.limit = 10;
            request.startIndex = pos;

            request.unit = (!string.IsNullOrEmpty(cmbUnit.SelectedValue?.ToString())) ?
                Convert.ToInt32(cmbUnit.SelectedValue?.ToString()) : -1;
            request.subUnit = (!string.IsNullOrEmpty(cmbSubUnit.SelectedValue?.ToString())) ? Convert.ToInt32(cmbSubUnit.SelectedValue?.ToString()) : 0;
            if (!string.IsNullOrEmpty(cmbCrimeType.SelectedValue?.ToString()))
            {
                request.crimeType = Convert.ToInt32(cmbCrimeType.SelectedValue?.ToString());
            }
            if (!string.IsNullOrEmpty(cmbGender.SelectedValue?.ToString()))
            {
                request.gender = Convert.ToInt32(cmbGender.SelectedValue?.ToString());
            }

            request.referenceNo = (!string.IsNullOrEmpty(tbRefNo.Text)) ? tbRefNo.Text : null;
            request.fullName = (!string.IsNullOrEmpty(tbFullName.Text)) ? tbFullName.Text : null;
            request.arrestType = (!string.IsNullOrEmpty(cmbArrestType.SelectedValue?.ToString())) ? Convert.ToInt32(cmbArrestType.SelectedValue?.ToString()) : 0;
            request.district = (!string.IsNullOrEmpty(cmbDistrict.SelectedValue?.ToString())) ? Convert.ToInt32(cmbDistrict.SelectedValue?.ToString()) : 0;
            request.upazila = (!string.IsNullOrEmpty(cmbUpazilla.SelectedValue?.ToString())) ? Convert.ToInt32(cmbUpazilla.SelectedValue?.ToString()) : 0;
            request.union = (!string.IsNullOrEmpty(cmbUnion.SelectedValue?.ToString())) ? Convert.ToInt32(cmbUnion.SelectedValue?.ToString()) : 0;

            if (dtpFrom.Enabled)
            {
                try
                {
                    DateTime dtFrom = DateTime.ParseExact(dtpFrom.Text, "dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture);
                    request.creationDateFrom = dtFrom.ToString("yyyy-MM-dd HH:mm:ss");
                }
                catch { }
            }
            else
            {
                request.creationDateFrom = null;
            }
            if (dtpTo.Enabled)
            {
                try
                {
                    DateTime dtTo = DateTime.ParseExact(dtpTo.Text, "dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture);
                    request.creationDateTo = dtTo.ToString("yyyy-MM-dd HH:mm:ss");
                }
                catch { }
            }
            else
            {
                request.creationDateTo = null;
            }

            string errorMsg = null;

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = new SpecialEnrollApiManager().GetSpecialProfile(request);
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
                if (response.specialCriminalProfileList != null)
                {
                    totalCount = Convert.ToInt32(response.total);
                    ShowSpecialProfileList(response.specialCriminalProfileList);
                }
            }
        }

        private void ShowSpecialProfileList(List<SpecialEnrollmentDto> list)
        {
            dgvList.Rows.Clear();
            labelTotalRecords.Text = "" + totalCount;
            for (int i=0; i<list.Count; i++)
            {
                int index = (startIndexPos * 10) + i + 1;

                string arrestType = null;
                if (!string.IsNullOrEmpty(list[i].arrestType))
                {
                    if (list[i].arrestType == "MobileCourts") arrestType = "Mobile Courts";
                    else if (list[i].arrestType == "ContagiousPatients") arrestType = "Contagious Patients";
                    else if (list[i].arrestType == "directSubmitInPS") arrestType = "Direct Submit In PS";
                }
                dgvList.Rows.Add(index, list[i].referenceNo, list[i].fullName, list[i].gender, list[i].crimeType, arrestType, "Preview", "Edit", list[i].createdBy);

                if (list[i].createdBy == Users.Id)
                {
                    dgvList.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(202, 214, 192);
                }
            }
        }

        private int startIndexPos = 0;
        private int totalCount = 0;
        private void btnFirst_Click(object sender, EventArgs e)
        {
            //request.startIndex = startIndexPos;
            //btnSearch_Click(null, null);
            startIndexPos = 0;
            OnSearch(startIndexPos);
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (startIndexPos > 0) startIndexPos -= 1;
            //request.startIndex = startIndexPos;
            //btnSearch_Click(null, null);
            OnSearch(startIndexPos);
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            //if (startIndexPos < (totalCount/10)) startIndexPos += 1;
            if (startIndexPos < (totalCount / 10))
            {
                startIndexPos += 1;
                if ((totalCount % 10 == 0) && (startIndexPos == totalCount / 10)) startIndexPos -= 1;
            }
            //request.startIndex = startIndexPos;
            //btnSearch_Click(null, null);
            OnSearch(startIndexPos);
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            if (totalCount % 10 != 0) startIndexPos = totalCount / 10;
            else if (totalCount % 10 == 0) startIndexPos = (totalCount / 10) - 1;
            //request.startIndex = startIndexPos;
            //btnSearch_Click(null, null);
            OnSearch(startIndexPos);
        }

        private void dgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 7)
            {
                if (Convert.ToInt32(dgvList.CurrentRow.Cells[8]?.Value?.ToString()) == Users.Id)
                {
                    string refNo = dgvList.CurrentRow.Cells[1]?.Value?.ToString();
                    ((SpecialSearchProfileController)controller).GetSpecialData(refNo, 1);
                }
                else
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "You are not authorized to edit this profile");
                    return;
                }
            }
            else if (e.ColumnIndex == 6)
            {
                string refNo = dgvList.CurrentRow.Cells[1]?.Value?.ToString();
                ((SpecialSearchProfileController)controller).GetSpecialData(refNo, 2);
            }
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

        private void exitButton_Click(object sender, EventArgs e)
        {
            ((SpecialSearchProfileController)controller).GoBacktoDashboard();
        }

        private void chkEnableDateFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnableDateFilter.Checked)
            {
                dtpFrom.Enabled = true;
                dtpTo.Enabled = true;
            }
            else
            {
                dtpFrom.Enabled = false;
                dtpTo.Enabled = false;
            }
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
                        cmbSubUnit.DisplayMember = "Value";
                        cmbSubUnit.ValueMember = "Key";
                        cmbSubUnit.SelectedIndex = -1;
                    }
                }
            }
        }

        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSubUnit.DataSource = null;
        }
    }
}
