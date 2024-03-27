using ISTL.MODELS.DTO.New.Enrollment;
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
    public partial class OtherInfoDialogForm : Form
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
        public OtherInfoDialogForm()
        {
            InitializeComponent();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (StaticData.Enrollment.profile.otherInformationList?.Count > 0)
            {
                List<OtherInfoDto> list = StaticData.Enrollment.profile.otherInformationList;
                if (list.Count > 0)
                {
                    dgvOtherInfo.Rows.Clear();
                    for (int i=0; i<list.Count; i++)
                    {
                        dgvOtherInfo.Rows.Add(list[i].key, list[i].value);
                    }
                }
            }
            else if (StaticData.PreviewEnrollment?.profile?.otherInformationList?.Count > 0)
            {
                List<OtherInfoDto> list = StaticData.PreviewEnrollment.profile.otherInformationList;
                if (list.Count > 0)
                {
                    dgvOtherInfo.Rows.Clear();
                    for (int i = 0; i < list.Count; i++)
                    {
                        dgvOtherInfo.Rows.Add(list[i].key, list[i].value);
                    }
                }
            }
        }
    }
}
