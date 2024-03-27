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
    public class UserManagementController : ViewController
    {
        private UserManagementUserControl userManagementUserControl;
        public UserManagementController()
        {
            userManagementUserControl = new UserManagementUserControl();
            base.SetView((IView)userManagementUserControl);
            userManagementUserControl.SetController(this);
        }

        public override string GetName()
        {
            return Globals.ChildControllers.USER_MANAGEMENT;
        }

        public void GoBacktoDashboard()
        {
            ((MainController)parent).OnHome();
        }
    }
}
