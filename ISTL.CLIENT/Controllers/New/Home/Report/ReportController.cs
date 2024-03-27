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
    public class ReportController : ViewController
    {
        private ReportUserControl reportUserControl;
        public ReportController()
        {
            reportUserControl = new ReportUserControl();
            base.SetView((IView)reportUserControl);
            reportUserControl.SetController(this);
        }

        public override string GetName()
        {
            return Globals.ChildControllers.REPORT;
        }

        public void GoBacktoDashboard()
        {
            ((MainController)parent).OnHome();
        }
    }
}
