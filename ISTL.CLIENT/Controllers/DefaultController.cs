using NLog;
using ISTL.COMMON;
using ISTL.RAB.View;
using ISTL.PERSOGlobals;
using ISTL.COMMON.CommandManager;
using ISTL.RAB.Entity;
using ISTL.RAB.ApiManager;
using ISTL.MODELS.Response.New;
using ISTL.MODELS.Request.New;
using ISTL.COMMON.Subscription;
using System;
using System.Net;
using ISTL.RAB.View.New.Enrollment.BiometricInformation;
using System.Windows.Forms;

namespace ISTL.RAB.Controllers
{
    public class DefaultController : ViewController
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private DefaultUserControl defaultUserControl;
        private OnlineSubject onlineStatus = (OnlineSubject)SubjectFactory.GetInstance().GetSubject(OnlineSubject.Name);

        public DefaultController()
        {
            defaultUserControl = new DefaultUserControl();
            base.SetView((IView)defaultUserControl);
            defaultUserControl.SetController(this);
        }

        public override string GetName()
        {
            return Globals.ChildControllers.DEFAULT;
        }

        public void SyncDashboard()
        {
            
        }

        public void ShowDashboardCriminalSummary()
        {

        }

        public void CriminalProfile()
        {
            StaticData.MoveFromBiometricEntryPageWithoutCDMSsearch = false;
            StaticData.Enrollment.profile = new MODELS.DTO.New.Enrollment.ProfileDto();
            StaticData.ModifiableNormalEnrollment = true;

            var form = new ChooseDBToMatchForm();
            DialogResult dr = form.ShowDialog();
            if (dr == DialogResult.OK)
            {
                ((MainController)parent).SearchCriteriaBeforeEnrollment = form.SelectedSearchCriteria;
                parent.AddChild(Globals.ChildControllers.BIOMETRIC);
            }

            //else
            //{
            //    parent.AddChild(Globals.ChildControllers.BIOMETRIC);
            //}
        }

        public void Diagnose()
        {
            parent.AddChild(Globals.ChildControllers.DIAGNOSE);
        }

        public void SearchCriminal()
        {
            parent.AddChild(Globals.ChildControllers.SEARCH_CRIMINAL);
        }

        public void BiometricSearch()
        {
            var form = new ChooseDBToMatchForm();
            DialogResult dr = form.ShowDialog();
            if (dr == DialogResult.OK)
            {
                ((MainController)parent).SearchCriteriaBeforeEnrollment = form.SelectedSearchCriteria;
                parent.AddChild(Globals.ChildControllers.BIOMETRIC_SEARCH);
            }
        }

        public void UserManagement()
        {
            parent.AddChild(Globals.ChildControllers.USER_MANAGEMENT);
        }

        public void Setup()
        {
            parent.AddChild(Globals.ChildControllers.BATTALION_SETTINGS);
        }

        public void SpecialEntry()
        {
            parent.AddChild(Globals.ChildControllers.SPECIAL_ENTRY);
        }

        public void Reports()
        {
            parent.AddChild(Globals.ChildControllers.REPORT);
        }

        public void NotEntry()
        {
            StaticData.ModifiableNotEntry = true;
            StaticData.NotEntry = new MODELS.DTO.New.NotEntry.NotEntryDto();
            parent.AddChild(Globals.ChildControllers.NOT_ENTRY);
		}
		
        public void ProfileManagement()
        {
            parent.AddChild(Globals.ChildControllers.PROFILE_MANAGEMENT);
        }

        public override void OnLoad()
        {
            base.OnLoad();
            InitializeController();
        }
        private void InitializeController()
        {
            InitializeCommandManager();
        }
        private void InitializeCommandManager()
        {
            CommandManager cmdMgr = ((MainController)parent).cmdMgr;

            // Hide buttons
            if (cmdMgr != null && cmdMgr.Commands.Count > 0)
            {
                cmdMgr.Commands[Globals.Commands.SAVE].Checked = false;
                cmdMgr.Commands[Globals.Commands.CANCEL].Checked = false;
                cmdMgr.Commands[Globals.Commands.TAKE].Checked = false;
            }
        }

        public override void OnClosing()
        {
            base.OnClosing();
        }
    }
}
