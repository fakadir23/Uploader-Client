using ISTL.COMMON;
using ISTL.RAB.Controllers;
using ISTL.RAB.Controllers.New.Enrollment;
using ISTL.RAB.Controllers.New.Enrollment.Special;
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
    public partial class SpecialEnrollCountUserControl : ViewUserControl
    {
        public SpecialEnrollCountUserControl()
        {
            InitializeComponent();
        }

        public string MobileCourtCount
        {
            get { return tbMobileCourtCount.Text; }
            set { tbMobileCourtCount.Text = value; }
        }
        public string DirectSubmitPS
        {
            get { return tbDirectSubmitPSCount.Text; }
            set { tbDirectSubmitPSCount.Text = value; }
        }
        public string ContagoiusPatientCount
        {
            get { return tbContagiousCount.Text; }
            set { tbContagiousCount.Text = value; }
        }
        public string DateOfCount
        {
            get { return dtpCountDate.Text; }
            set { dtpCountDate.Text = value; }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ((SpecialEnrollCountController)controller).OnSubmit();
        }

        private void tbMobileCourtCount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) tbContagiousCount.Focus();
        }
        private void tbWarrantCount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) tbDirectSubmitPSCount.Focus();
        }
        private void tbGamblerCount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnSave.Focus();
        }
        private void tbContagiousCount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) tbDirectSubmitPSCount.Focus();
        }
        private void tbMobileCourtCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == (Char)Keys.Back) e.Handled = false;
            else e.Handled = true;
        }
        private void tbWarrantCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == (Char)Keys.Back) e.Handled = false;
            else e.Handled = true;
        }
        private void tbGamblerCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == (Char)Keys.Back) e.Handled = false;
            else e.Handled = true;
        }
        private void tbContagiousCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == (Char)Keys.Back) e.Handled = false;
            else e.Handled = true;
        }

        private void dtpCountDate_ValueChanged(object sender, EventArgs e)
        {
            SetResult(null, null, null);
            ((SpecialEnrollCountController)controller).OnSearch();
        }

        public void SetResult(string mbCount, string cpCount, string directSubmitinPSCount)
        {
            tbMobileCourtCount.Text = null;
            tbContagiousCount.Text = null;
            tbDirectSubmitPSCount.Text = null;

            tbMBCount.Text = mbCount;
            tbCPCount.Text = cpCount;
            tbDirectSubmitCount.Text = directSubmitinPSCount;

            tbMobileCourtCount.Text = mbCount;
            tbContagiousCount.Text = cpCount;
            tbDirectSubmitPSCount.Text = directSubmitinPSCount;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            ((SpecialEnrollCountController)controller).GoBacktoDashboard();
        }
    }
}
