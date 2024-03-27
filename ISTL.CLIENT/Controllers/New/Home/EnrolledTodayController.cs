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
    public class EnrolledTodayController : ViewController
    {
        private EnrolledTodayUserControl enrolledTodayUserControl;
        private DbExistingDataManager dbExistingDataManager;
        private DbEnrollClientManager dbEnrollClientManager;
        public int RecordCount;

        public EnrolledTodayController()
        {
            enrolledTodayUserControl = new EnrolledTodayUserControl();
            base.SetView((IView)enrolledTodayUserControl);
            enrolledTodayUserControl.SetController(this);

            dbExistingDataManager = new DbExistingDataManager();
            dbEnrollClientManager = new DbEnrollClientManager();
        }

        public override string GetName()
        {
            return Globals.ChildControllers.ENROLLED_TODAY;
        }

        public List<EnrollmentDto> GetEnrolledTodayData(string whereClause, int position)
        {
            List<EnrollmentDto> list = dbExistingDataManager.GetNormalEnrolledTodayRecords(whereClause, position);
            RecordCount = dbExistingDataManager.RecordCount;
            return list;
        }

        public int GetEnrolledTodayCount()
        {
            int count = dbExistingDataManager.GetNormalEnrolledTodayCount();
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
