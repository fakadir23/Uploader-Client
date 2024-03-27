using ISTL.COMMON;
using ISTL.COMMON.Common;
using ISTL.MODELS.DTO.New.NotEntry;
using ISTL.RAB.Controllers.New.Enrollment.NotEntry;
using ISTL.RAB.Entity;
using ISTL.RAB.Entity.Lookup;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISTL.RAB.View.New.Enrollment.NotEntry
{
    public partial class NotEntryFailedUploadUserControl : ViewUserControl
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        public NotEntryFailedUploadUserControl()
        {
            InitializeComponent();

            LoadLookupItems();            
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            btnSearch_Click(null, null);
        }

        private void LoadLookupItems()
        {
            cmbCaseType.DataSource = new BindingSource(ComboBoxItems.caseType, null);
            cmbCaseType = Utils.GeneralComboBoxFormat(cmbCaseType);

            cmbNotEntryReason.DataSource = new BindingSource(ComboBoxItems.notEntryReason, null);
            cmbNotEntryReason = Utils.GeneralComboBoxFormat(cmbNotEntryReason);
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
            string whereClause = null;

            if (!string.IsNullOrEmpty(tbRefNo.Text)) whereClause += " AND reference_no='" + AesCryptography.EncryptToString(tbRefNo.Text) + "'";
            if (!string.IsNullOrEmpty(tbFullName.Text)) whereClause += " AND accused_name='" + AesCryptography.EncryptToString(tbFullName.Text) + "'";
            if (!string.IsNullOrEmpty(cmbCaseType.SelectedValue?.ToString())) whereClause += " AND not_entry_case_type='" +
                    AesCryptography.EncryptToString(cmbCaseType.SelectedValue?.ToString()) + "'";
            if (!string.IsNullOrEmpty(cmbNotEntryReason.SelectedValue?.ToString())) whereClause += " AND not_entry_reason='" +
                    AesCryptography.EncryptToString(cmbNotEntryReason.SelectedValue?.ToString()) + "'";
            if (!string.IsNullOrEmpty(tbFatherName.Text)) whereClause += " AND father_name='" + AesCryptography.EncryptToString(tbFatherName.Text) + "'";
            if (!string.IsNullOrEmpty(tbMobileNo.Text)) whereClause += " AND mobile_no='" + AesCryptography.EncryptToString(tbMobileNo.Text) + "'";
            if (!string.IsNullOrEmpty(tbWarrantNo.Text)) whereClause += " AND warrant_no='" + AesCryptography.EncryptToString(tbWarrantNo.Text) + "'";

            List<NotEntryDto> list = ((NotEntryFailedUploadController)controller).GetFailedData(whereClause, startIndex);

            if (list == null) return;
            else ShowNotEntryList(list);
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
            dgvList.Rows.Clear();
            totalCount = (((NotEntryFailedUploadController)controller).RecordCount >= 0) ? ((NotEntryFailedUploadController)controller).RecordCount : 0;
            labelTotalRecords.Text = "" + totalCount;

            for (int i = 0; i < list?.Count; i++)
            {
                int index = (startIndex * 10) + i + 1;

                string reason = null;

                try
                {
                    ComboBoxItems.notEntryReason.TryGetValue(Convert.ToString(list[i].notEntryReason), out reason);
                }
                catch { }

                dgvList.Rows.Add(index, list[i].referenceNo, list[i].accusedName, reason, list[i].errorMessage, "Edit", list[i].createdBy);

                if (list[i].createdBy == Users.Id)
                {
                    dgvList.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(202, 214, 192);
                }
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            ((NotEntryFailedUploadController)controller).GoBackToDashboard();
        }

        private void dgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 5)
            {
                string refNo = dgvList.CurrentRow.Cells[1]?.Value?.ToString();
                ((NotEntryFailedUploadController)controller).GetLocalNotEntry(refNo);
            }
        }
    }
}
