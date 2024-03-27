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
    public class BattalionSettingsController : ViewController
    {
        private BattalionSettingsUserControl battalionSettingsUserControl;
        public BattalionSettingsController()
        {
            battalionSettingsUserControl = new BattalionSettingsUserControl();
            base.SetView((IView)battalionSettingsUserControl);
            battalionSettingsUserControl.SetController(this);
        }

        public override string GetName()
        {
            return Globals.ChildControllers.BATTALION_SETTINGS;
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
