using ISTL.COMMON;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.PERSOGlobals;
using ISTL.RAB.DbManager;
using ISTL.RAB.View.New.Enrollment.Special;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.Controllers.New.Enrollment.Special
{
    public class SpecialUploadPendingController : ViewController
    {
        private UploadPendingSpecialUserControl uploadPendingSpecialUserControl;
        private DbExistingSpecialProfileManager dbExistingSpecialProfileManager;
        public int RecordCount;
        public SpecialUploadPendingController()
        {
            uploadPendingSpecialUserControl = new UploadPendingSpecialUserControl();
            base.SetView((IView)uploadPendingSpecialUserControl);
            uploadPendingSpecialUserControl.SetController(this);

            dbExistingSpecialProfileManager = new DbExistingSpecialProfileManager();
        }

        public override string GetName()
        {
            return Globals.ChildControllers.UPLOAD_PENDING_SPECIAL;
        }

        public List<SpecialEnrollmentDto> GetUploadPendingData(string whereClause, int position)
        {
            List<SpecialEnrollmentDto> list = dbExistingSpecialProfileManager.GetSpecialUploadPendingRecords(Globals.RecordState.NEW, "status", whereClause, position);
            RecordCount = dbExistingSpecialProfileManager.RecordCount;
            return list;
        }

        public void GoBacktoDashboard()
        {
            ((MainController)parent).OnHome();
        }
    }
}
