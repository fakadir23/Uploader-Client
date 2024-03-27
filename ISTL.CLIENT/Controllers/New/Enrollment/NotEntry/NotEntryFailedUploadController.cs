using ISTL.COMMON;
using ISTL.MODELS.DTO.New.NotEntry;
using ISTL.PERSOGlobals;
using ISTL.RAB.DbManager;
using ISTL.RAB.Entity;
using ISTL.RAB.View.New.Enrollment.NotEntry;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.Controllers.New.Enrollment.NotEntry
{
    public class NotEntryFailedUploadController : ViewController
    {
        private NotEntryFailedUploadUserControl notEntryFailedUpload;
        private Logger logger = LogManager.GetCurrentClassLogger();
        private DbNotEntryManager dbNotEntryManager;
        public int RecordCount;

        public NotEntryFailedUploadController()
        {
            notEntryFailedUpload = new NotEntryFailedUploadUserControl();
            base.SetView((IView)notEntryFailedUpload);
            notEntryFailedUpload.SetController(this);

            dbNotEntryManager = new DbNotEntryManager();
        }

        public override string GetName()
        {
            return Globals.ChildControllers.NOT_ENTRY_FAILED_UPLOAD;
        }

        public List<NotEntryDto> GetFailedData(string whereClause, int position)
        {
            List<NotEntryDto> list = dbNotEntryManager.GetLocalRecords(whereClause, 1, 1, position);
            RecordCount = dbNotEntryManager.RecordCount;
            return list;
        }

        public void GetLocalNotEntry(string referenceNo)
        {
            NotEntryDto notEntryDto = dbNotEntryManager.GetLocalNotEntry(referenceNo);
            if (notEntryDto != null)
            {
                StaticData.NotEntry = notEntryDto;
                StaticData.ModifiableNotEntry = true;
                parent.AddChild(Globals.ChildControllers.NOT_ENTRY);
            }
        }

        public void GoBackToDashboard()
        {
            ((MainController)parent).OnHome();
        }
    }
}
