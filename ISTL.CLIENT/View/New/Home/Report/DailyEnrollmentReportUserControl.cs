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
using ISTL.RAB.ControllersNew.Home.Report;

namespace ISTL.RAB.View.New.Report
{
    public partial class DailyEnrollmentReportUserControl : ViewUserControl
    {
        public DailyEnrollmentReportUserControl()
        {
            InitializeComponent();
        }

        private void btnSummaryReport_Click(object sender, EventArgs e)
        {
            ((DailyEnrollmentReportController)controller).SummaryReport();
        }

        private void btnDailyEnrollReport_Click(object sender, EventArgs e)
        {
            ((DailyEnrollmentReportController)controller).DailyEnrollmentReport();
        }
    }
}
