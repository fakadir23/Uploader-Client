using ISTL.COMMON;
using ISTL.COMMON.Common;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.RAB.Controllers.New.Enrollment.Special;
using ISTL.RAB.DbManager;
using ISTL.RAB.Entity;
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

namespace ISTL.RAB.View.New.Enrollment.Special
{
    public partial class FailedUploadSpecialProfileUserControl : ViewUserControl
    {
        private LookupItems lookupItems = new LookupItems();
        private int totalCount;
        private DbUserManager dbUserManager;
        public FailedUploadSpecialProfileUserControl()
        {
            InitializeComponent();
            LoadComboBox();
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            dbUserManager = new DbUserManager();
            totalCount = ((SpecialFailedUploadController)controller).RecordCount;
            OnSearch(0);
        }

        private void LoadComboBox()
        {
            lookupItems.LoadStations();
            if (lookupItems.stationList != null)
            {
                if (lookupItems.stationList.Count > 0)
                {
                    cmbUnit.DataSource = new BindingSource(lookupItems.stationList, null);
                    cmbUnit = Utils.GeneralComboBoxFormat(cmbUnit);
                }
            }

            cmbCrimeType.DataSource = new BindingSource(ComboBoxItems.specialCrimeType, null);
            cmbCrimeType = Utils.GeneralComboBoxFormat(cmbCrimeType); ;

            cmbArrestType.DataSource = new BindingSource(ComboBoxItems.arresteeType, null);
            cmbArrestType = Utils.GeneralComboBoxFormat(cmbArrestType);
        }

        private void cmbSubUnit_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbUnit.SelectedValue?.ToString()))
            {
                cmbUnit.Focus();
            }
            else
            {
                LookupItems LUItems = new LookupItems();
                LUItems.LoadSubStations(cmbUnit.SelectedValue?.ToString());

                if (LUItems.subStationList != null)
                {
                    if (LUItems.subStationList.Count > 0)
                    {
                        cmbSubUnit.DataSource = new BindingSource(LUItems.subStationList, null);
                        cmbSubUnit = Utils.GeneralComboBoxFormat(cmbSubUnit);
                    }
                }
            }
        }

        private void ShowFailedList(List<SpecialEnrollmentDto> list)
        {
            dgvList.Rows.Clear();
            totalCount = (((SpecialFailedUploadController)controller).RecordCount >= 0) ? ((SpecialFailedUploadController)controller).RecordCount : 0;
            labelTotalRecords.Text = "" + totalCount;

            string createdByName = string.Empty;
            for (int i = 0; i < list.Count; i++)
            {
                int index = position + i + 1;
                if (i > 0)
                {
                    if (list[i].createdBy != list[i - 1].createdBy)
                    {
                        createdByName = dbUserManager.GetUserFullNameByUserId(Convert.ToInt32(list[i].createdBy));
                    }
                }
                else
                {
                    createdByName = dbUserManager.GetUserFullNameByUserId(Convert.ToInt32(list[i].createdBy));
                }
                dgvList.Rows.Add(index, list[i].referenceNo, list[i].fullName, list[i].gender, list[i].crimeType, createdByName,
                    list[i].errorMsgFromServer, "Edit", list[i].hash, list[i].id);
            }
        }

        private void dgvDraftList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 7)
            {
                string hash = dgvList.CurrentRow.Cells[8]?.Value?.ToString();
                ((SpecialFailedUploadController)controller).GetDataByHash(hash);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            position = 0;
            OnSearch(position);
        }

        private void OnSearch(int pos)
        {
            string whereClause = null;

            if (!string.IsNullOrEmpty(tbRefNo.Text)) whereClause += " AND reference_no='" + AesCryptography.EncryptToString(tbRefNo.Text) + "'";
            if (!string.IsNullOrEmpty(tbFullName.Text)) whereClause += " AND full_name='" + AesCryptography.EncryptToString(tbFullName.Text) + "'";
            if (!string.IsNullOrEmpty(cmbCrimeType.SelectedValue?.ToString())) whereClause += " AND crime_type='" +
                    AesCryptography.EncryptToString(cmbCrimeType.SelectedValue?.ToString()) + "'";
            if (!string.IsNullOrEmpty(cmbArrestType.SelectedValue?.ToString())) whereClause += " AND arrestee_type='" +
                    Convert.ToInt32(cmbArrestType.SelectedValue?.ToString()) + "'";
            if (!string.IsNullOrEmpty(tbNID.Text)) whereClause += " AND nid='" + AesCryptography.EncryptToString(tbNID.Text) + "'";
            if (!string.IsNullOrEmpty(cmbUnit.SelectedValue?.ToString())) whereClause += " AND unit='" +
                    Convert.ToInt32(cmbUnit.SelectedValue?.ToString()) + "'";
            if (!string.IsNullOrEmpty(cmbSubUnit.SelectedValue?.ToString())) whereClause += " AND sub_unit='" +
                    Convert.ToInt32(cmbSubUnit.SelectedValue?.ToString()) + "'";

            List<SpecialEnrollmentDto> list = ((SpecialFailedUploadController)controller).GetFailedData(whereClause, pos);

            if (list == null) return;
            else ShowFailedList(list);
        }

        private int position = 0;

        private void btnFirst_Click(object sender, EventArgs e)
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
            int specialRecordTotal = totalCount;
            if (position < specialRecordTotal - 10) position += 10;
            OnSearch(position);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            int specialRecordTotal = totalCount;
            if (specialRecordTotal % 10 != 0) position = (specialRecordTotal / 10) * 10;
            else if (specialRecordTotal % 10 == 0) position = ((specialRecordTotal / 10) - 1) * 10;
            OnSearch(position);
        }

        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSubUnit.DataSource = null;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            ((SpecialFailedUploadController)controller).GoBacktoDashboard();
        }
    }
}
