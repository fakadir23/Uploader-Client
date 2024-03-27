using ISTL.COMMON;
using ISTL.COMMON.Common;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.RAB.Controllers;
using ISTL.RAB.Controllers.New.Home;
using ISTL.RAB.DbManager;
using ISTL.RAB.Entity.Lookup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISTL.RAB.View.New.Home
{
    public partial class DraftDataUserControl : ViewUserControl
    {
        private int totalCount;
        private DbUserManager dbUserManager;
        public DraftDataUserControl()
        {
            InitializeComponent();
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            dbUserManager = new DbUserManager();
            LoadComboBox();

            totalCount = ((DraftDataController)controller).GetSpecialDraftCount();
            OnSearch(0);
        }

        private void LoadComboBox()
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
        }

        private void cmbSubUnit_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbUnit.SelectedValue?.ToString()))
            {
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

        private void ShowDraftList(List<EnrollmentDto> list)
        {
            dgvDraftList.Rows.Clear();
            totalCount = (((DraftDataController)controller).RecordCount >= 0) ? ((DraftDataController)controller).RecordCount : 0;
            labelTotalRecords.Text = "" + totalCount;

            string createdByName = string.Empty;
            for (int i = 0; i < list.Count; i++)
            {
                int index = position + i + 1;
                if (i > 0)
                {
                    if (list[i].profile?.createdBy != list[i - 1].profile?.createdBy)
                    {
                        createdByName = dbUserManager.GetUserFullNameByUserId(Convert.ToInt32(list[i].profile?.createdBy));
                    }
                }
                else
                {
                    createdByName = dbUserManager.GetUserFullNameByUserId(Convert.ToInt32(list[i].profile?.createdBy));
                }
                dgvDraftList.Rows.Add(index, list[i].profile?.referenceNo, list[i].profile?.fullName, list[i].profile?.dateOfBirth,
                    list[i].profile?.gender, createdByName, "VIEW", list[i].profile?.hash, list[i].profile?.id, "DELETE");
            }
        }

        private void dgvDraftList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 6)
            {
                string hash = dgvDraftList.CurrentRow.Cells[7]?.Value?.ToString();
                ((DraftDataController)controller).GetDataByHash(hash);
            }
            else if (e.ColumnIndex == 9)
            {
                //DialogResult dr = MessageBoxController.ShowQuestionYesNo("RAB CDMS", "Are you sure you want to delete this draft?");
                DialogResult dr = YesNoMessageBox.YesNoMessageBoxResult("RAB CDMS", "Are you sure you want to delete this draft?");
                if (dr == DialogResult.Yes)
                {
                    string hash = dgvDraftList.CurrentRow.Cells[7]?.Value?.ToString();
                    ((DraftDataController)controller).DeleteDataByHash(hash);
                    OnSearch(0);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbDistrict.SelectedValue?.ToString()) > 0)
            {
                if (!rbPermanentAddress.Checked && !rbPresentAddress.Checked)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please select if present/permanent address");
                    return;
                }
            }
            position = 0;
            OnSearch(position);
        }

        private void OnSearch(int pos)
        {
            string whereClause = null;

            if (!string.IsNullOrEmpty(cmbUnit.SelectedValue?.ToString())) whereClause += " AND battalion='" +
                    Convert.ToInt32(cmbUnit.SelectedValue?.ToString()) + "'";
            if (!string.IsNullOrEmpty(cmbSubUnit.SelectedValue?.ToString())) whereClause += " AND sub_unit='" +
                    Convert.ToInt32(cmbSubUnit.SelectedValue?.ToString()) + "'";
            if (!string.IsNullOrEmpty(tbReferenceNumber.Text)) whereClause += " AND reference_no='" + AesCryptography.EncryptToString(tbReferenceNumber.Text) + "'";
            if (!string.IsNullOrEmpty(tbFullName.Text)) whereClause += " AND full_name='" + AesCryptography.EncryptToString(tbFullName.Text) + "'";

            int districtId = (!string.IsNullOrEmpty(cmbDistrict.SelectedValue?.ToString())) ? Convert.ToInt32(cmbDistrict.SelectedValue?.ToString()) : 0;
            int upazilaId = (!string.IsNullOrEmpty(cmbUpazilla.SelectedValue?.ToString())) ? Convert.ToInt32(cmbUpazilla.SelectedValue?.ToString()) : 0;
            int unionId = (!string.IsNullOrEmpty(cmbUnion.SelectedValue?.ToString())) ? Convert.ToInt32(cmbUnion.SelectedValue?.ToString()) : 0;

            if (rbPresentAddress.Checked)
            {
                if (districtId > 0) whereClause += " AND presentaddress.district='" + districtId + "'";
                if (upazilaId > 0) whereClause += " AND presentaddress.upazila='" + upazilaId + "'";
                if (unionId > 0) whereClause += " AND presentaddress.union_or_ward='" + unionId + "'";
            }
            if (rbPermanentAddress.Checked)
            {
                if (districtId > 0) whereClause += " AND permanentAddress.district='" + districtId + "'";
                if (upazilaId > 0) whereClause += " AND permanentAddress.upazila='" + upazilaId + "'";
                if (unionId > 0) whereClause += " AND permanentAddress.union_or_ward='" + unionId + "'";
            }

            List<EnrollmentDto> list = ((DraftDataController)controller).GetDraftData(whereClause, pos);

            if (list == null) return;
            else ShowDraftList(list);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbSubUnit.Focus();
            cmbSubUnit.SelectedIndex = -1;
            cmbSubUnit.SelectedText = null;
            cmbUnit.SelectedIndex = -1;
            tbReferenceNumber.Text = null;
            tbFullName.Text = null;
            rbPermanentAddress.Checked = false;
            rbPresentAddress.Checked = false;
            cmbUnion.Focus();
            cmbUnion.SelectedIndex = -1;
            cmbUnion.SelectedText = null;
            cmbUpazilla.Focus();
            cmbUpazilla.SelectedIndex = -1;
            cmbUpazilla.SelectedText = null;
            cmbDistrict.SelectedIndex = -1;
            tbReferenceNumber.Focus();
        }

        private int position = 0;

        private void btnStart_Click(object sender, EventArgs e)
        {
            position = 0;
            OnSearch(position);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (position >= 10) position -= 10;
            OnSearch(position);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int draftRecordTotal = totalCount;
            if (position < draftRecordTotal - 10) position += 10;
            OnSearch(position);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            int draftRecordTotal = totalCount;
            if (draftRecordTotal % 10 != 0) position = (draftRecordTotal / 10) * 10;
            else if (draftRecordTotal % 10 == 0) position = ((draftRecordTotal / 10) - 1) * 10;
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

        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSubUnit.DataSource = null;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            ((DraftDataController)controller).GoBacktoDashboard();
        }
    }
}
