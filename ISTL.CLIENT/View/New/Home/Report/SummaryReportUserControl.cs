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
using ISTL.RAB.Controllers.New.Home.Report;

namespace ISTL.RAB.View.New.Report
{
    public partial class SummaryReportUserControl : ViewUserControl
    {
        public SummaryReportUserControl()
        {
            InitializeComponent();
        }

        private void cmbUpazilla_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSummaryReport_Click(object sender, EventArgs e)
        {
            ((SummaryReportController)controller).SummaryReport();
        }

        private void btnDailyEnrollReport_Click(object sender, EventArgs e)
        {
            ((SummaryReportController)controller).DailyEnrollmentReport();
        }
    }
}
