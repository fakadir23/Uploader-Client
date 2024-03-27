using ISTL.COMMON;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.PERSOGlobals;
using ISTL.RAB.DbManager;
using ISTL.RAB.Entity;
using ISTL.RAB.View.New.Enrollment.Special;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.Controllers.New.Enrollment.Special
{
    public class SpecialFailedUploadController : ViewController
    {
        private FailedUploadSpecialProfileUserControl failedUploadSpecial;
        private DbExistingSpecialProfileManager dbExistingSpecialProfileManager;
        private DbSpecialEnrollManager dbSpecialEnrollManager;
        public int RecordCount;
        public SpecialFailedUploadController()
        {
            failedUploadSpecial = new FailedUploadSpecialProfileUserControl();
            base.SetView((IView)failedUploadSpecial);
            failedUploadSpecial.SetController(this);

            dbExistingSpecialProfileManager = new DbExistingSpecialProfileManager();
            dbSpecialEnrollManager = new DbSpecialEnrollManager();
        }
        public override string GetName()
        {
            return Globals.ChildControllers.FAILED_UPLOAD_SPECIAL;
        }

        public List<SpecialEnrollmentDto> GetFailedData(string whereClause, int position)
        {
            List<SpecialEnrollmentDto> list = dbExistingSpecialProfileManager.GetExistingSpecialRecords(1, "error_status", whereClause, position);
            RecordCount = dbExistingSpecialProfileManager.RecordCount;
            return list;
        }

        public int GetSpecialFailedCount()
        {
            int count = dbExistingSpecialProfileManager.GetSpecialEnrolledDraftOrErrorCount("error_status");
            return count;
        }

        public void GetDataByHash(string hash)
        {
            SpecialEnrollmentDto specialEnrollmentDto = dbSpecialEnrollManager.GetLocalSpecialEnrolled(hash);
            if (specialEnrollmentDto != null)
            {
                StaticData.specialEnrollment = specialEnrollmentDto;
                StaticData.ModifiableSpecialEnrollment = true;
                parent.AddChild(Globals.ChildControllers.SPECIAL_ENTRY);
            }
        }

        public void GoBacktoDashboard()
        {
            ((MainController)parent).OnHome();
        }
    }
}
