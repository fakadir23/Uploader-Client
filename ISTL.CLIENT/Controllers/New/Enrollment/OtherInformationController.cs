using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISTL.COMMON;
using ISTL.PERSOGlobals;
using ISTL.RAB.View.New.FamilyAlliesFoes;

namespace ISTL.RAB.Controllers.New.Enrollment
{
    public class OtherInformationController : ViewController
    {
        private OtherInformationUserControl otherInformationUserControl;


        #region Controller(s)
        public OtherInformationController()
        {
            otherInformationUserControl = new OtherInformationUserControl();
            base.SetView((IView)otherInformationUserControl);
            otherInformationUserControl.SetController(this);

        }
        #endregion

        public override string GetName()
        {
            return Globals.ChildControllers.OTHER_INFO;
        }

        public void CriminalProfile()
        {
            parent.AddChild(Globals.ChildControllers.ENROLL);
        }

        public void Family()
        {
            parent.AddChild(Globals.ChildControllers.OTHER_INFO);
        }

        public void Biometric()
        {
            ((MainController)parent).SearchCriteriaBeforeEnrollment = Globals.SearchCriteriaBeforeEnrollment.CDMS_Search;
            parent.AddChild(Globals.ChildControllers.BIOMETRIC);
        }

        public void PreviewSubmit()
        {
            parent.AddChild(Globals.ChildControllers.PREVIEW_SUBMIT);
        }
    }
}
