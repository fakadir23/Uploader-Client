using ISTL.COMMON;
using ISTL.COMMON.Subscription;
using ISTL.COMMON.Threads;
using ISTL.MODELS.Common;
using ISTL.MODELS.DTO.New.NotEntry;
using ISTL.PERSOGlobals;
using ISTL.RAB.ApiManager;
using ISTL.RAB.Asynch;
using ISTL.RAB.DbManager;
using ISTL.RAB.Entity;
using ISTL.RAB.View;
using ISTL.RAB.View.New.Enrollment;
using ISTL.RAB.View.New.Enrollment.NotEntry;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.Controllers.New.Enrollment.NotEntry
{
    public class NotEntryController : ViewController
    {
        private NotEntryUserControl notEntryUserControl;
        private Logger logger = LogManager.GetCurrentClassLogger();
        private DbNotEntryManager dbNotEntryManager;

        public NotEntryController()
        {
            notEntryUserControl = new NotEntryUserControl();
            base.SetView((IView)notEntryUserControl);
            notEntryUserControl.SetController(this);

            dbNotEntryManager = new DbNotEntryManager();
        }

        public override string GetName()
        {
            return Globals.ChildControllers.NOT_ENTRY;
        }

        public void GoBackToDashboard()
        {
            ((MainController)parent).OnHome();
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
