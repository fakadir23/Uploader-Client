using ISTL.COMMON;
using ISTL.PERSOGlobals;
using ISTL.RAB.View.New.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.Controllers.New.Home.Report
{
    public class SummaryReportController : ViewController
    {
        private SummaryReportUserControl summaryReportUserControl;
        public SummaryReportController()
        {
            summaryReportUserControl = new SummaryReportUserControl();
            base.SetView((IView)summaryReportUserControl);
            summaryReportUserControl.SetController(this);
        }

        public override string GetName()
        {
            return Globals.ChildControllers.SUMMARY_REPORT;
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
