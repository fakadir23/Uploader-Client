using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISTL.COMMON;
using ISTL.PERSOGlobals;
using ISTL.RAB.View.New;

namespace ISTL.RAB.Controllers.New.Enrollment
{
    public class CriminalProfileController : ViewController
    {
        private CriminalProfileUserControl criminalProfileControl;

        #region Controller(s)
        public CriminalProfileController()
        {
            criminalProfileControl = new CriminalProfileUserControl();
            base.SetView((IView)criminalProfileControl);
            criminalProfileControl.SetController(this);

        }
        #endregion

        public override string GetName()
        {
            return Globals.ChildControllers.ENROLL;
        }

        public void CriminalProfile()
        {
            parent.AddChild(Globals.ChildControllers.ENROLL);
        }

        public void OtherInfo()
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
