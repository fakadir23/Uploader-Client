using ISTL.COMMON;
using ISTL.MODELS.DTO.New.NotEntry;
using ISTL.PERSOGlobals;
using ISTL.RAB.DbManager;
using ISTL.RAB.View.New.Enrollment.NotEntry;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.Controllers.New.Enrollment.NotEntry
{
    public class NotEntryUploadPendingController : ViewController
    {
        private NotEntryUploadPendingUserControl notEntryUploadPending;
        private Logger logger = LogManager.GetCurrentClassLogger();
        private DbNotEntryManager dbNotEntryManager;
        public int RecordCount;

        public NotEntryUploadPendingController()
        {
            notEntryUploadPending = new NotEntryUploadPendingUserControl();
            base.SetView((IView)notEntryUploadPending);
            notEntryUploadPending.SetController(this);

            dbNotEntryManager = new DbNotEntryManager();
        }

        public override string GetName()
        {
            return Globals.ChildControllers.NOT_ENTRY_UPLOAD_PENDING;
        }

        public List<NotEntryDto> GetUploadPendingData(string whereClause, int position)
        {
            List<NotEntryDto> list = dbNotEntryManager.GetLocalRecords(whereClause, 1, 0, position);
            RecordCount = dbNotEntryManager.RecordCount;
            return list;
        }

        public void GoBackToDashboard()
        {
            ((MainController)parent).OnHome();
        }
    }
}
