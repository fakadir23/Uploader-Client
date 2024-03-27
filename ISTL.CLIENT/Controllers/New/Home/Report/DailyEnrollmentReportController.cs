using ISTL.COMMON;
using ISTL.PERSOGlobals;
using ISTL.RAB.View.New.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.ControllersNew.Home.Report
{
    public class DailyEnrollmentReportController : ViewController
    {
        private DailyEnrollmentReportUserControl dailyEnrollmentReportUserControl;
        public DailyEnrollmentReportController()
        {
            dailyEnrollmentReportUserControl = new DailyEnrollmentReportUserControl();
            base.SetView((IView)dailyEnrollmentReportUserControl);
            dailyEnrollmentReportUserControl.SetController(this);
        }

        public override string GetName()
        {
            return Globals.ChildControllers.DAILY_ENROLLMENT_REPORT;
        }

        public void DailyEnrollmentReport()
        {
            parent.AddChild(Globals.ChildControllers.DAILY_ENROLLMENT_REPORT);
        }

        public void SummaryReport()
        {
            parent.AddChild(Globals.ChildControllers.SUMMARY_REPORT);
        }
    }
}
