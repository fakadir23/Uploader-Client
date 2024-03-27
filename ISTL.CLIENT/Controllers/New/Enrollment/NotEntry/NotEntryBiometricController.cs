using ISTL.COMMON;
using ISTL.PERSOGlobals;
using ISTL.RAB.View.New.Enrollment.NotEntry;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.Controllers.New.Enrollment.NotEntry
{
    public class NotEntryBiometricController : ViewController
    {
        private NotEntryBiometricUserControl NotEntryBiometric;
        private Logger logger = LogManager.GetCurrentClassLogger();

        public NotEntryBiometricController()
        {
            NotEntryBiometric = new NotEntryBiometricUserControl();
            base.SetView((IView)NotEntryBiometric);
            NotEntryBiometric.SetController(this);
        }

        public override string GetName()
        {
            return Globals.ChildControllers.NOT_ENTRY_BIOMETRIC;
        }

        public void OnBackToEntry()
        {
            ((MainController)parent).OnBackToNotEntryProfile();
        }

        public void OnNoEntryBiometric()
        {
            ((MainController)parent).OnNotEntryBiometric();
        }

        public void SubmitPreview()
        {
            ((MainController)parent).OnNotEntrySubmitPreview();
        }
    }
}
