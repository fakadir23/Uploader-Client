using ISTL.COMMON;
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

namespace ISTL.RAB.View.New.CriminalProfile
{
    public partial class AddEducationDialogForm : Form
    {
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

        public string EducationStatusKey;
        public string EducationStatusValue;
        public string EducationInstitute;
        public bool PoliticalInvolvementInInstitute;
        public string EducationRemarks;
        public AddEducationDialogForm()
        {
            InitializeComponent();
            rdBtnNo.Checked = true;
            LoadComboItems();
        }

        private void LoadComboItems()
        {
            cmbEducationStatus.DataSource = new BindingSource(ComboBoxItems.educationStatus, null);
            cmbEducationStatus = Utils.GeneralComboBoxFormat(cmbEducationStatus);
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void iconBtnSave_Click(object sender, EventArgs e)
        {
            OnSave();
        }

        private void OnSave()
        {
            EducationStatusKey = (!string.IsNullOrEmpty(cmbEducationStatus.SelectedValue?.ToString())) ? cmbEducationStatus.SelectedValue?.ToString() : null;
            EducationStatusValue = (!string.IsNullOrEmpty(cmbEducationStatus.Text?.ToString())) ? cmbEducationStatus.Text?.ToString() : null;
            EducationInstitute = (!string.IsNullOrEmpty(tbInstituteName.Text)) ? tbInstituteName.Text : null;
            EducationRemarks = (!string.IsNullOrEmpty(tbRemarks.Text)) ? tbRemarks.Text : null;

            if (rdBtnYes.Checked) PoliticalInvolvementInInstitute = true;
            else if (rdBtnNo.Checked)
            {
                PoliticalInvolvementInInstitute = false;
            }


            DialogResult = DialogResult.OK;
        }
    }
}
