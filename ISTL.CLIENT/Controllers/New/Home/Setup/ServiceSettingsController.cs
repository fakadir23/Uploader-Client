using ISTL.COMMON;
using ISTL.PERSOGlobals;
using ISTL.RAB.View.New.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.Controllers.New.Home.Setup
{
    public class ServiceSettingsController : ViewController
    {
        private ServiceSettingsUserControl serviceSettingsUserControl;
        public ServiceSettingsController()
        {
            serviceSettingsUserControl = new ServiceSettingsUserControl();
            base.SetView((IView)serviceSettingsUserControl);
            serviceSettingsUserControl.SetController(this);
        }

        public override string GetName()
        {
            return Globals.ChildControllers.SERVICE_SETTINGS;
        }

        public void BattalionSettings()
        {
            parent.AddChild(Globals.ChildControllers.BATTALION_SETTINGS);
        }

        public void ServiceSettings()
        {
            parent.AddChild(Globals.ChildControllers.SERVICE_SETTINGS);
        }
    }
}
