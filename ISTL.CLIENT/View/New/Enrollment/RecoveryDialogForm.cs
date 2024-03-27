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

namespace ISTL.RAB.View.New.Enrollment
{
    public partial class RecoveryDialogForm : Form
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
        public RecoveryDialogForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (StaticData.Enrollment?.profile?.crimeInformation?.recoveryList?.Count > 0)
            {
                for (int i=0; i<StaticData.Enrollment.profile.crimeInformation.recoveryList.Count; i++)
                {
                    dgvRecovery.Rows.Add(
                        StaticData.Enrollment.profile.crimeInformation.recoveryList[i].recoveryType,
                        StaticData.Enrollment.profile.crimeInformation.recoveryList[i].recoveryItemName,
                        StaticData.Enrollment.profile.crimeInformation.recoveryList[i].amount);
                }
            }
            else if (StaticData.PreviewEnrollment?.profile?.crimeInformation?.recoveryList?.Count > 0)
            {
                for (int i = 0; i < StaticData.PreviewEnrollment.profile.crimeInformation.recoveryList.Count; i++)
                {
                    dgvRecovery.Rows.Add(
                        StaticData.PreviewEnrollment.profile.crimeInformation.recoveryList[i].recoveryType,
                        StaticData.PreviewEnrollment.profile.crimeInformation.recoveryList[i].recoveryItemName,
                        StaticData.PreviewEnrollment.profile.crimeInformation.recoveryList[i].amount);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
