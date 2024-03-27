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
using ISTL.MODELS.DTO.Search;
using ISTL.RAB.Entity.Lookup;
using ISTL.RAB.Controllers;
using ISTL.RAB.Controllers.New.Home;
using MaterialSkin.Controls;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.RAB.Entity;

namespace ISTL.RAB.View.New
{
    public partial class SearchCriminalUserControl : ViewUserControl
    {
        private DemographicSearchDto demographicSearchDto;

        public interface ISearchCriminalUser
        {
            void OnSearchProfile(string referenceNo);
        }
        public SearchCriminalUserControl()
        {
            InitializeComponent();

            LoadComboBoxItems();

            demographicSearchDto = new DemographicSearchDto();

            dtpFrom.CustomFormat = "dd/MM/yyyy hh:mm tt";
            dtpTo.CustomFormat = "dd/MM/yyyy hh:mm tt";
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            if (StaticData.dataWithBio)
            {
                chkEnrollBio.Checked = true;
                chkEnableDateFilter.Checked = true;
                chkEnableDateFilter_CheckedChanged(null, null);
                dtpFrom.Value = DateTime.Now.AddDays(-1);
            }
            else
            {
                chkEnrollBio.Checked = false;
            }

            if (StaticData.firPending)
            {
                chkFIRpending.Checked = true;
                chkEnableDateFilter.Checked = true;
                chkEnableDateFilter_CheckedChanged(null, null);
                dtpFrom.Value = DateTime.Now.AddMonths(-1);
            }
            else
            {
                chkFIRpending.Checked = false;
            }

            if (StaticData.dataWithBio || StaticData.firPending)
            {
                try
                {
                    cmbUnit.SelectedValue = Users.Unit;
                    if (Users.Unit > 0)
                    {
                        cmbSubUnit_Enter(null, null);
                        cmbSubUnit.SelectedValue = Users.SubUnit;
                    }
                }
                catch { }
            }

            btnSearch_Click(null, null);
        }
        private void LoadLookupItems()
        {
            LookupItems lookupItems = new LookupItems();

            lookupItems.LoadCrimeType();
            if (lookupItems.crimeTypeList != null)
            {
                if (lookupItems.crimeTypeList.Count > 0)
                {
                    cmbCrimeType.DataSource = new BindingSource(lookupItems.crimeTypeList, null);
                    cmbCrimeType = Utils.GeneralComboBoxFormat(cmbCrimeType);
                }
            }

            lookupItems.LoadNationality();
            if (lookupItems.nationalityList != null)
            {
                if (lookupItems.nationalityList.Count > 0)
                {
                    cmbNationality.DataSource = new BindingSource(lookupItems.nationalityList, null);
                    cmbNationality = Utils.GeneralComboBoxFormat(cmbNationality);
                }
            }

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
        }

        private void LoadComboBoxItems()
        {
            //cmbCrimeType.DataSource = new BindingSource(ComboBoxItems.crimeType, null);
            //cmbCrimeType = Utils.GeneralComboBoxFormat(cmbCrimeType);

            cmbReligion.DataSource = new BindingSource(ComboBoxItems.religion, null);
            cmbReligion = Utils.GeneralComboBoxFormat(cmbReligion);

            cmbGender.DataSource = new BindingSource(ComboBoxItems.genders, null);
            cmbGender = Utils.GeneralComboBoxFormat(cmbGender);

            LoadLookupItems();
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

        public Label labelTotalRecords1 => labelTotalRecords;
        public DataGridView dgvSearchProfileSummaries1 => dgvSearchProfileSummaries;

        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSubUnit.DataSource = null;
        }

        private void cmbSubUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void tbReferenceNumber_Click(object sender, EventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void tbFullName_Click(object sender, EventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //((SearchCriminalController) controller).OnSearch(PrepareSearchCriminalDto());
            OnSearch(0);
        }

        private void OnSearch(int pos)
        {
            //if (Convert.ToInt32(cmbDistrict.SelectedValue?.ToString()) > 0 || !string.IsNullOrEmpty(tbVillageRoadHouse.Text))
            //{
            //    if (!rbPermanentAddress.Checked && !rbPresentAddress.Checked)
            //    {
            //        CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please select if present/permanent address");
            //        return;
            //    }
            //}

            demographicSearchDto.referenceNo = (!string.IsNullOrEmpty(tbReferenceNumber.Text)) ? tbReferenceNumber.Text : null;
            demographicSearchDto.fullName = (!string.IsNullOrEmpty(tbFullName.Text)) ? tbFullName?.Text : null;
            demographicSearchDto.limit = 10;
            demographicSearchDto.startIndex = pos;

            demographicSearchDto.unit = (!string.IsNullOrEmpty(cmbUnit.SelectedValue?.ToString())) ?
                Convert.ToInt32(cmbUnit.SelectedValue?.ToString()) : -1;

            int districtId = (!string.IsNullOrEmpty(cmbDistrict.SelectedValue?.ToString())) ? Convert.ToInt32(cmbDistrict.SelectedValue?.ToString()) : 0;
            int upazilaId = (!string.IsNullOrEmpty(cmbUpazilla.SelectedValue?.ToString())) ? Convert.ToInt32(cmbUpazilla.SelectedValue?.ToString()) : 0;
            int unionId = (!string.IsNullOrEmpty(cmbUnion.SelectedValue?.ToString())) ? Convert.ToInt32(cmbUnion.SelectedValue?.ToString()) : 0;
            string villageHouseRoadNo = (!string.IsNullOrEmpty(tbVillageRoadHouse.Text)) ? tbVillageRoadHouse.Text : null;

            AddressDto addressDto = new AddressDto();
            addressDto.district = districtId;
            addressDto.upazila = upazilaId;
            addressDto.union = unionId;
            addressDto.villageHouseRoadNo = villageHouseRoadNo;

            if (addressDto.district > 0 || !string.IsNullOrEmpty(addressDto.villageHouseRoadNo)) demographicSearchDto.address = addressDto;
            else demographicSearchDto.address = null;
            //if (rbPresentAddress.Checked)
            //{
            //    demographicSearchDto.presentAddress = addressDto;
            //}
            //if (rbPermanentAddress.Checked)
            //{
            //    demographicSearchDto.permanentAddress = addressDto;
            //}

            if (dtpFrom.Enabled)
            {
                try
                {
                    DateTime DateFrom = DateTime.ParseExact(dtpFrom.Text, "dd/MM/yyyy hh:mm tt", System.Globalization.CultureInfo.CurrentCulture);
                    demographicSearchDto.uploadedDateFrom = DateFrom.ToString("yyyy-MM-dd HH:mm:ss");
                }
                catch { }
            }
            else
            {
                demographicSearchDto.uploadedDateFrom = null;
            }
            if (dtpTo.Enabled)
            {
                try
                {
                    DateTime DateTo = DateTime.ParseExact(dtpTo.Text, "dd/MM/yyyy hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                    demographicSearchDto.uploadedDateTo = DateTo.ToString("yyyy-MM-dd HH:mm:ss");
                }
                catch { }
            }
            else
            {
                demographicSearchDto.uploadedDateTo = null;
            }

            demographicSearchDto.subUnit = (!string.IsNullOrEmpty(cmbSubUnit.SelectedValue?.ToString())) ? Convert.ToInt32(cmbSubUnit.SelectedValue?.ToString()) : 0;
            demographicSearchDto.pendingFir = (chkFIRpending.Checked) ? true : false;
            //demographicSearchDto.dataWithBiometric = (chkEnrollBio.Checked) ? true : false;
            demographicSearchDto.dataWithOutBiometric = (chkEnrollBio.Checked) ? true : false;

            demographicSearchDto.nationality = (!string.IsNullOrEmpty(cmbNationality.SelectedValue?.ToString())) ?
                Convert.ToInt32(cmbNationality.SelectedValue?.ToString()) : 0;

            if (!string.IsNullOrEmpty(cmbCrimeType.SelectedValue?.ToString()))
            {
                demographicSearchDto.crimeType = Convert.ToInt32(cmbCrimeType.SelectedValue?.ToString());
            }
            if (!string.IsNullOrEmpty(cmbGender.SelectedValue?.ToString()))
            {
                demographicSearchDto.gender = Convert.ToInt32(cmbGender.SelectedValue?.ToString());
            }
            if (cmbReligion.SelectedIndex > 0)
            {
                if (cmbReligion.SelectedValue?.ToString() == "muslim") demographicSearchDto.religion = 1;
                if (cmbReligion.SelectedValue?.ToString() == "hindu") demographicSearchDto.religion = 2;
                if (cmbReligion.SelectedValue?.ToString() == "christian") demographicSearchDto.religion = 3;
                if (cmbReligion.SelectedValue?.ToString() == "buddhist") demographicSearchDto.religion = 4;
            }
            else
            {
                demographicSearchDto.religion = 0;
            }

            demographicSearchDto.nid = (!string.IsNullOrEmpty(tbNID.Text)) ? tbNID.Text : null;            

            ((SearchCriminalController)controller).OnSearchNew(demographicSearchDto);
        }

        private void dgvSearchProfileSummaries_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 7)
            {
                //string referenceNo = dgvSearchProfileSummaries.CurrentRow.Cells[1]?.Value?.ToString();
                //((SearchCriminalController) controller).OnSearchProfile(referenceNo);
                if (Convert.ToInt32(dgvSearchProfileSummaries.CurrentRow.Cells[9]?.Value?.ToString()) == Users.Id)
                {
                    string criminalId = dgvSearchProfileSummaries.CurrentRow.Cells[8]?.Value?.ToString();
                    ((SearchCriminalController)controller).OnSearchProfile(criminalId);
                }
                else
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "You are not authorized to edit this profile");
                    return;
                }
            }
            else if (e.ColumnIndex == 6)
            {
                string criminalId = dgvSearchProfileSummaries.CurrentRow.Cells[8]?.Value?.ToString();
                ((SearchCriminalController)controller).OnPreviewProfile(criminalId);
            }
        }
        
        private int position = 0;
        public int totalCount = 0;
        private void btnFirst_Click(object sender, EventArgs e)
        {
            position = 0;
            //demographicSearchDto.startIndex = position;
            //btnSearch_Click(null, null);
            OnSearch(position);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (position > 0) position -= 1;
            //demographicSearchDto.startIndex = position;
            //btnSearch_Click(null, null);
            OnSearch(position);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //if (position < (totalCount/10)) position += 1;
            if (position < (totalCount / 10))
            {
                position += 1;
                if ((totalCount % 10 == 0) && (position == totalCount / 10)) position -= 1;
            }
            //demographicSearchDto.startIndex = position;
            //btnSearch_Click(null, null);
            OnSearch(position);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (totalCount % 10 != 0) position = totalCount / 10;
            else if (totalCount % 10 == 0) position = (totalCount / 10) - 1;
            //demographicSearchDto.startIndex = position;
            //btnSearch_Click(null, null);
            OnSearch(position);
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
            ((SearchCriminalController)controller).GoBacktoDashboard();
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
    }
}