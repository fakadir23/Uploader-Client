using ISTL.COMMON;
using ISTL.COMMON.CommandManager;
using ISTL.MODELS.Common;
using ISTL.MODELS.DTO.Enrollment;
using ISTL.RAB.View;
using ISTL.PERSOGlobals;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using ISTL.MODELS.Response;
using ISTL.COMMON.Network;
using System.Net;
using System.ServiceModel;
using ISTL.MODELS.Request;

namespace ISTL.RAB.Controllers
{
    public class ManageController : ViewController
    {
        #region Declaration(s)
        private Logger logger = LogManager.GetCurrentClassLogger();
        private ManageUserControl manageUserControl;

        private readonly string GetEnrollmentSummaryEndpoint = ConfigurationManager.
            AppSettings["EnrollmentSummaryEndpoint"].ToString();

        public EnrollmentListSearchRequest request;
        public EnrollmentListSearchResponse response;
        #endregion

        #region Controller(s)
        public ManageController()
        {
            manageUserControl = new ManageUserControl();
            base.SetView((IView)manageUserControl);
            manageUserControl.SetController(this);

            request = new EnrollmentListSearchRequest();
            response = new EnrollmentListSearchResponse();
        }
        #endregion

        #region Method(s)
        public override string GetName()
        {
            return Globals.ChildControllers.ENROLL;
        }
        public override void OnLoad()
        {
            base.OnLoad();
            InitializeController();
        }
        public void LoadEnrolledData()
        {
            string erroMsg = null;

            request.limit = 10;

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = NetworkService.SubmitRequest<EnrollmentListSearchResponse>
                    (request, GetEnrollmentSummaryEndpoint + "?token=abc", null);
                }
                catch (WebException ex)
                {
                    erroMsg = "There was an error during API Communication. Please contact with your System Administrator.";
                    logger.ErrorException("Web Exception occurred.", ex);
                }
                catch (TimeoutException ex)
                {
                    erroMsg = "There was an error during API Communication. Please contact with your System Administrator.";
                    logger.ErrorException("Timeout exception occurred.", ex);
                }
                catch (EndpointNotFoundException ex)
                {
                    erroMsg = "There was an error during API Communication. Please contact with your System Administrator.";
                    logger.ErrorException("Timeout exception occurred.", ex);
                }
                catch (Exception ex)
                {
                    erroMsg = "There was an error during API Communication. Please contact with your System Administrator.";
                    logger.ErrorException("Critical exception occurred.", ex);
                }
            });

            if (!response.operationResult)
            {
                erroMsg = response?.errorMsg;
                if (!string.IsNullOrEmpty(response.errorMsg))
                {
                    MessageBoxController.ShowWarning("RAB CDMS", response.errorMsg);
                }
                Console.WriteLine("Process unsuccessful " + erroMsg);
                return;
            }

            else if (response.operationResult)
            {
                if (response.passportDataList != null)
                {
                    manageUserControl.SetEnrollmentListDetails(response.passportDataList);
                    manageUserControl.totalInEnrollmentList = Convert.ToInt32(response.total);
                }
            }
        }
        private void InitializeController()
        {
            LoadEnrolledData();
        }
        public override void OnClosing()
        {
            base.OnClosing();
        }
        public void OnShowEnrolledData(long id)
        {
            ((MainController)parent).PersonId = id;
            parent.AddChild(Globals.ChildControllers.ENROLL);
        }
        public void OnShowPersonMatchResult(PersonDataDto personObj)
        {
            ((MainController)parent).PersonData = personObj;
            ((MainController)parent).AddChild(Globals.ChildControllers.MATCH);
        }

        public void OnEnrollmentData(PersonDataDto personObj)
        {
            ((MainController)parent).PersonData = personObj;
            ((MainController)parent).AddChild(Globals.ChildControllers.ENROLL);
        }
        #endregion
    }
}
