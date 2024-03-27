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
    public class SpecialDraftProfileController : ViewController
    {
        private DraftSpecialProfileUserControl draftSpecialProfile;
        private DbExistingSpecialProfileManager dbExistingSpecialProfileManager;
        private DbSpecialEnrollManager dbSpecialEnrollManager;
        public int RecordCount;
        public SpecialDraftProfileController()
        {
            draftSpecialProfile = new DraftSpecialProfileUserControl();
            base.SetView((IView)draftSpecialProfile);
            draftSpecialProfile.SetController(this);

            dbExistingSpecialProfileManager = new DbExistingSpecialProfileManager();
            dbSpecialEnrollManager = new DbSpecialEnrollManager();
        }

        public override string GetName()
        {
            return Globals.ChildControllers.SPECIAL_DRAFT;
        }

        public List<SpecialEnrollmentDto> GetDraftSpecialData(string whereClause, int position)
        {
            List<SpecialEnrollmentDto> list = dbExistingSpecialProfileManager.GetExistingSpecialRecords(0, "status", whereClause, position);
            RecordCount = dbExistingSpecialProfileManager.RecordCount;
            return list;
        }

        public int GetSpecialDraftCount()
        {
            int count = dbExistingSpecialProfileManager.GetSpecialEnrolledDraftOrErrorCount("status");
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

        public void DeleteDataByHash(string hash)
        {
            bool isDeleted = dbExistingSpecialProfileManager.DeleteDraftData(hash);
        }

        public void GoBacktoDashboard()
        {
            ((MainController)parent).OnHome();
        }
    }
}
