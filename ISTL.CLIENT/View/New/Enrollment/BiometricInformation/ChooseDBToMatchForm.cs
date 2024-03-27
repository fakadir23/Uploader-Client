using ISTL.PERSOGlobals;
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

namespace ISTL.RAB.View.New.Enrollment.BiometricInformation
{
    public partial class ChooseDBToMatchForm : Form
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

        public int SelectedSearchCriteria;
        public ChooseDBToMatchForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        

        private void exitButton_Click(object sender, EventArgs e)
        {
            SelectedSearchCriteria = 0;
            this.DialogResult = DialogResult.Cancel;
        }

        private void iconBtnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SelectedSearchCriteria = 0;
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //if (rdBtnCDMSsearchByImage.Checked) SelectedSearchCriteria = Globals.SearchCriteriaBeforeEnrollment.CDMS_Search_By_Image;
            //else if (rdBtnCDMSsearchByFP.Checked) SelectedSearchCriteria = Globals.SearchCriteriaBeforeEnrollment.CDMS_Search_By_FP;
            //else if (rdBtnCDMSsearchByIris.Checked) SelectedSearchCriteria = Globals.SearchCriteriaBeforeEnrollment.CDMS_Search_By_Iris;
            //else if (rdBtnBECsearchByFP.Checked) SelectedSearchCriteria = Globals.SearchCriteriaBeforeEnrollment.BEC_Search_By_FP;
            //else if (rdBtnBECsearchByNIDdob.Checked) SelectedSearchCriteria = Globals.SearchCriteriaBeforeEnrollment.BEC_Search_By_NidDob;

            this.DialogResult = DialogResult.OK;
        }

        private void btnSeachInCDMS_Click(object sender, EventArgs e)
        {
            SelectedSearchCriteria = Globals.SearchCriteriaBeforeEnrollment.CDMS_Search;
            
            btnSeachInCDMS.BackColor = Color.MediumAquamarine;
            btnSearchBEC.BackColor = Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            btnSearchJailDB.BackColor = Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));

            btnFpIdentify.Visible = true;
            btnPhotoIdentify.Visible = true;
            btnIrisIdentify.Visible = true;

            btnBECsearchByFP.Visible = false;
            btnBECsearchNID.Visible = false;

            btnJailFp.Visible = false;
            btnJailIris.Visible = false;
        }

        private void btnSearchBEC_Click(object sender, EventArgs e)
        {
            SelectedSearchCriteria = Globals.SearchCriteriaBeforeEnrollment.BEC_Search;

            btnSeachInCDMS.BackColor = Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            btnSearchJailDB.BackColor = Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            btnSearchBEC.BackColor = Color.MediumAquamarine;

            btnBECsearchByFP.Visible = true;
            btnBECsearchNID.Visible = true;

            btnFpIdentify.Visible = false;
            btnPhotoIdentify.Visible = false;
            btnIrisIdentify.Visible = false;

            btnJailFp.Visible = false;
            btnJailIris.Visible = false;

            //this.DialogResult = DialogResult.OK;
        }

        private void btnSearchOtherDB_Click(object sender, EventArgs e)
        {
            SelectedSearchCriteria = Globals.SearchCriteriaBeforeEnrollment.JailDB_Search;

            btnSeachInCDMS.BackColor = Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            btnSearchBEC.BackColor = Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            btnSearchJailDB.BackColor = Color.MediumAquamarine;

            btnBECsearchByFP.Visible = false;
            btnBECsearchNID.Visible = false;

            btnFpIdentify.Visible = false;
            btnPhotoIdentify.Visible = false;
            btnIrisIdentify.Visible = false;

            btnJailFp.Visible = true;
            btnJailIris.Visible = true;
        }

        private void btnFpIdentify_Click(object sender, EventArgs e)
        {
            StaticData.SearchByFpPhotoIris = 0;
            this.DialogResult = DialogResult.OK;
        }

        private void btnPhotoIdentify_Click(object sender, EventArgs e)
        {
            StaticData.SearchByFpPhotoIris = 1;
            this.DialogResult = DialogResult.OK;
        }

        private void btnIrisIdentify_Click(object sender, EventArgs e)
        {
            StaticData.SearchByFpPhotoIris = 2;
            this.DialogResult = DialogResult.OK;
        }

        private void btnBECsearchByFP_Click(object sender, EventArgs e)
        {
            StaticData.SearchByFpPhotoIris = 3;
            this.DialogResult = DialogResult.OK;
        }

        private void btnBECsearchNID_Click(object sender, EventArgs e)
        {
            StaticData.SearchByFpPhotoIris = 4;
            this.DialogResult = DialogResult.OK;
        }

        private void btnJailFp_Click(object sender, EventArgs e)
        {
            StaticData.SearchByFpPhotoIris = 5;
            this.DialogResult = DialogResult.OK;
        }

        private void btnJailIris_Click(object sender, EventArgs e)
        {
            StaticData.SearchByFpPhotoIris = 6;
            this.DialogResult = DialogResult.OK;
        }
    }
}
