using ISTL.COMMON;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.PERSOGlobals;
using ISTL.RAB.DbManager;
using ISTL.RAB.View.New.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.Controllers.New.Home
{
    public class UploadPendingListController : ViewController
    {
        private UploadPendingListUserControl uploadPendingListUserControl;
        private DbExistingDataManager dbExistingDataManager;
        public int RecordCount;

        public UploadPendingListController()
        {
            uploadPendingListUserControl = new UploadPendingListUserControl();
            base.SetView((IView)uploadPendingListUserControl);
            uploadPendingListUserControl.SetController(this);

            dbExistingDataManager = new DbExistingDataManager();
        }

        public override string GetName()
        {
            return Globals.ChildControllers.UPLOAD_PENDING;
        }

        public List<EnrollmentDto> GetUploadPendingData(string whereClause, int position)
        {
            List<EnrollmentDto> list = dbExistingDataManager.GetUploadPendingRecords(Globals.RecordState.NEW, "status", whereClause, position);
            RecordCount = dbExistingDataManager.RecordCount;
            return list;
        }

        public int GetUploadPendingCount()
        {
            int count = dbExistingDataManager.GetNormalUploadPendingCount("status", Globals.RecordState.NEW);
            return count;
        }

        public void GoBacktoDashboard()
        {
            ((MainController)parent).OnHome();
        }
    }
}
