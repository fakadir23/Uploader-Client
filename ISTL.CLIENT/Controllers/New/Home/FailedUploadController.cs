using ISTL.COMMON;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.PERSOGlobals;
using ISTL.RAB.DbManager;
using ISTL.RAB.Entity;
using ISTL.RAB.View.New.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.Controllers.New.Home
{
    public class FailedUploadController : ViewController
    {
        private FailedUploadUserControl failedUploadUserControl;
        private DbExistingDataManager dbExistingDataManager;
        private DbEnrollClientManager dbEnrollClientManager;
        public int RecordCount;
        public FailedUploadController()
        {
            failedUploadUserControl = new FailedUploadUserControl();
            base.SetView((IView)failedUploadUserControl);
            failedUploadUserControl.SetController(this);

            dbExistingDataManager = new DbExistingDataManager();
            dbEnrollClientManager = new DbEnrollClientManager();
        }

        public override string GetName()
        {
            return Globals.ChildControllers.FAILED_UPLOAD;
        }

        public List<EnrollmentDto> GetFailedData(string whereClause, int position)
        {
            List<EnrollmentDto> list = dbExistingDataManager.GetExistingRecords(1, "error_status", whereClause, position);
            RecordCount = dbExistingDataManager.RecordCount;
            return list;
        }

        public int GetFailedUploadCount()
        {
            int count = dbExistingDataManager.GetNormalDraftOrErrorCount("error_status", Globals.ErrorState.ERROR);
            return count;
        }

        public void GetDataByHash(string hash)
        {
            EnrollmentDto enrollmentDto = dbEnrollClientManager.GetEnrolledData(hash);
            if (enrollmentDto != null)
            {
                StaticData.Enrollment = enrollmentDto;
                StaticData.ModifiableNormalEnrollment = true;
                ((MainController)parent).SearchCriteriaBeforeEnrollment = Globals.SearchCriteriaBeforeEnrollment.CDMS_Search;
                StaticData.MoveFromBiometricEntryPageWithoutCDMSsearch = true;
                //parent.AddChild(Globals.ChildControllers.BIOMETRIC);
                parent.AddChild(Globals.ChildControllers.ENROLL);
            }
        }

        public void GoBacktoDashboard()
        {
            ((MainController)parent).OnHome();
        }
    }
}
