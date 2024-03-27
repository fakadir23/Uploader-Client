using ISTL.MODELS.DTO.New.Enrollment.CrimeInformation;
using ISTL.MODELS.DTO.New.Lookup;
using ISTL.RAB.DbManager;
using ISTL.RAB.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISTL.RAB.View.New.Enrollment.CriminalProfile
{
    public partial class AddRecoveryDialogForm : Form
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

        private DbLookupManager dbLookupManager;
        private List<RecoveryDto> recoveryTypeList;
        private Dictionary<int, string> recoveryNameList;

        public RecoveryEntryDto recoveryDto = new RecoveryEntryDto();

        public AddRecoveryDialogForm()
        {
            InitializeComponent();
            dbLookupManager = new DbLookupManager();
            recoveryTypeList = new List<RecoveryDto>();
            recoveryNameList = new Dictionary<int, string>();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void iconBtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbRecoveryType.SelectedValue?.ToString()))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please select Recovery Type");
                return;
            }
            if (string.IsNullOrEmpty(tbRecoveryItemAmount.Text))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please input Amount");
                return;
            }

            recoveryDto.recoveryType = (!string.IsNullOrEmpty(cmbRecoveryType.SelectedValue?.ToString())) ?
                cmbRecoveryType.SelectedValue?.ToString() : null;
            //if (!string.IsNullOrEmpty(cmbRecoveryName.SelectedValue?.ToString()))
            //{
                recoveryDto.lookupId = Convert.ToInt32(cmbRecoveryName.SelectedValue?.ToString());
                recoveryDto.recoveryItemName = cmbRecoveryName.Text;
            //}
            if (!string.IsNullOrEmpty(tbRecoveryItemAmount.Text))
            {
                recoveryDto.amount = tbRecoveryItemAmount.Text;
            }            

            this.DialogResult = DialogResult.OK;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            recoveryTypeList = dbLookupManager.GetRecoveryTypeList();

            LoadRecoveryType();
        }

        private void LoadRecoveryType()
        {
            int index = 0;
            var recoveryTypeDictionary = recoveryTypeList.ToDictionary(x=> index++, y=>y.lookupType);
            cmbRecoveryType.DataSource = new BindingSource(recoveryTypeDictionary, null);
            cmbRecoveryType.DisplayMember = "Value";
            cmbRecoveryType.ValueMember = "Value";
            cmbRecoveryType.PropertySelector = collection => collection.Cast<KeyValuePair<int, string>>().Select(p => p.Value);
            cmbRecoveryType.FilterRule = (item, text) => item.Trim().ToLower().Contains(text.Trim().ToLower());
            cmbRecoveryType.SuggestListOrderRule = s => s;
            cmbRecoveryType.SelectedIndex = -1;
        }

        private void cmbRecoveryName_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbRecoveryType.SelectedValue?.ToString()))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please select recovery type first");
                cmbRecoveryType.Focus();
                return;
            }
            else
            {
                List<RecoveryDto> recoveryNameList = dbLookupManager.
                    GetRecoveryByType(cmbRecoveryType.SelectedValue?.ToString());
                var recoveryNameDictionary = recoveryNameList.ToDictionary(x => x.id, y => y.lookupNameEn);
                cmbRecoveryName.DataSource = new BindingSource(recoveryNameDictionary, null);
                cmbRecoveryName.DisplayMember = "Value";
                cmbRecoveryName.ValueMember = "Key";
                cmbRecoveryName.PropertySelector = collection => collection.Cast<KeyValuePair<int, string>>().Select(p => p.Value);
                cmbRecoveryName.FilterRule = (item, text) => item.Trim().ToLower().Contains(text.Trim().ToLower());
                cmbRecoveryName.SuggestListOrderRule = s => s;
                cmbRecoveryName.SelectedIndex = -1;
            }
        }

        private void tbRecoveryItemAmount_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbRecoveryType.SelectedValue?.ToString()))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please select recovery type first");
                cmbRecoveryType.Focus();
                return;
            }
        }

        private void cmbRecoveryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbRecoveryName.DataSource = null;
        }
    }
}
