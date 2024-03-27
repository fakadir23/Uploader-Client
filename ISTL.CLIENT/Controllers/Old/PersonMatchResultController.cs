using ISTL.COMMON;
using ISTL.COMMON.Network;
using ISTL.MODELS.Request.Adjudication;
using ISTL.MODELS.Response.Adjudication;
using ISTL.PERSOGlobals;
using ISTL.RAB.View;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ISTL.RAB.Controllers
{
    public class PersonMatchResultController : ViewController
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        public PersonMatchResultForm personMatchResultForm;
        public GetMatchListRequest request = new GetMatchListRequest();
        public GetMatchListResponse response;
        private readonly string GetMatchListEndpoint = ConfigurationManager.
           AppSettings["GetMatchListEndpoint"].ToString();
        public PersonMatchResultController()
        {
            personMatchResultForm = new PersonMatchResultForm();
            base.SetView((IView)personMatchResultForm);
            personMatchResultForm.SetController(this);
            response = new GetMatchListResponse();
        }

        public override string GetName()
        {
            return Globals.ChildControllers.MATCH;
        }
        public override void OnLoad()
        {
            base.OnLoad();
            InitializeController();
        }
        private void InitializeController()
        {
            request.referenceNo = ((MainController)parent).PersonData.referenceNumber;
            LoadMatchedData();
        }
        public void LoadMatchedData()
        {
            string erroMsg = null;

            var json = new JavaScriptSerializer().Serialize(request);
            
            //Console.WriteLine("Match request: "+json);

            if (request.referenceNo == null)
            {
                return;
            }

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = NetworkService.SubmitRequest<GetMatchListResponse>
                    (request, GetMatchListEndpoint + "?token=abc", null);
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
                    personMatchResultForm.passportDataList = response.passportDataList;
                    personMatchResultForm.TotalMatchCount = Convert.ToInt32(response.total);
                    personMatchResultForm.SetMatchResult(0);
                    personMatchResultForm.SetMasterData(((MainController)parent).PersonData);
                }
            }
        }
        public override void OnClosing()
        {
            base.OnClosing();
        }

        public void OnShowMatchResult()
        {
            parent.AddChild(Globals.ChildControllers.MATCH);
        }
    }
}
