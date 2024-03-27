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
    public class DraftDataController : ViewController
    {
        private DraftDataUserControl draftDataUserControl;
        private DbExistingDataManager dbExistingDataManager;
        private DbEnrollClientManager dbEnrollClientManager;
        public int RecordCount;
        public DraftDataController()
        {
            draftDataUserControl = new DraftDataUserControl();
            base.SetView((IView)draftDataUserControl);
            draftDataUserControl.SetController(this);

            dbExistingDataManager = new DbExistingDataManager();
            dbEnrollClientManager = new DbEnrollClientManager();
        }

        public override string GetName()
        {
            return Globals.ChildControllers.DRAFT_LIST;
        }

        public List<EnrollmentDto> GetDraftData(string whereClause, int position)
        {
            List<EnrollmentDto> list = dbExistingDataManager.GetExistingRecords(0, "status", whereClause, position);
            RecordCount = dbExistingDataManager.RecordCount;
            return list;
        }

        public int GetSpecialDraftCount()
        {
            int count = dbExistingDataManager.GetNormalDraftOrErrorCount("status", Globals.RecordState.DRAFT);
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

        public void DeleteDataByHash(string hash)
        {
            bool isDeleted = dbExistingDataManager.DeleteDraftData(hash);
        }

        public void GoBacktoDashboard()
        {
            ((MainController)parent).OnHome();
        }
    }
}
