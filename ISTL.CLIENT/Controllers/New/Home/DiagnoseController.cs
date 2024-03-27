using ISTL.COMMON;
using ISTL.PERSOGlobals;
using ISTL.RAB.View.New;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.Controllers.New.Home
{
    public class DiagnoseController : ViewController
    {
        private DiagnoseUserControl diagnoseUserControl;
        public DiagnoseController()
        {
            diagnoseUserControl = new DiagnoseUserControl();
            base.SetView((IView)diagnoseUserControl);
            diagnoseUserControl.SetController(this);
        }

        public override string GetName()
        {
            return Globals.ChildControllers.DIAGNOSE;
        }

        public void GoBacktoDashboard()
        {
            ((MainController)parent).OnHome();
        }
    }
}
