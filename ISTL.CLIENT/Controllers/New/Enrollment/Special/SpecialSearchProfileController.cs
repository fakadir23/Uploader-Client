using ISTL.COMMON;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.MODELS.Request.New.Enrollment;
using ISTL.MODELS.Response.New.Special;
using ISTL.PERSOGlobals;
using ISTL.RAB.ApiManager;
using ISTL.RAB.Entity;
using ISTL.RAB.View;
using ISTL.RAB.View.New.Enrollment.Special;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.Controllers.New.Enrollment.Special
{
    public class SpecialSearchProfileController : ViewController
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private SearchSpecialProfileUserControl searchSpecialProfileUserControl;
        public SpecialSearchProfileController()
        {
            searchSpecialProfileUserControl = new SearchSpecialProfileUserControl();
            base.SetView((IView)searchSpecialProfileUserControl);
            searchSpecialProfileUserControl.SetController(this);
        }

        public override string GetName()
        {
            return Globals.ChildControllers.SPECIAL_SEARCH_PROFILE;
        }

        public void GetSpecialData(string refNo, int EditOrPrview)
        {
            GetSpecialProfileRequest request = new GetSpecialProfileRequest();
            GetSpecialProfileResponse response = new GetSpecialProfileResponse();
            request.limit = 1;
            request.referenceNo = refNo;

            string errorMsg = string.Empty;

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = new SpecialEnrollApiManager().GetSpecialProfile(request);
                }
                catch (WebException x)
                {
                    logger.Debug("Known error, when server not found when searching criminal. Error Message: " + x.Message);
                    errorMsg = "Seems you are Offline while searching criminal. Please contact with your Administrator.";
                }
                catch (TimeoutException x)
                {
                    logger.Debug("Connection timedout when searching criminal. Error Message: " + x.Message);
                    errorMsg = "Seems you are Offline while searching criminal. Please contact with your Administrator.";
                }
                catch (Exception x)
                {
                    logger.Error("There was an unexpected error when searching criminal.\n" + x.ToString());
                    errorMsg = "There was an unexpected error when searching criminal. Please contact with your Administrator.";                    
                }
            });

            if (!string.IsNullOrEmpty(errorMsg))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", errorMsg);
                return;
            }

            if (response != null)
            {
                if (response.code == 200 && response.specialCriminalProfileList != null)
                {
                    if (response.specialCriminalProfileList.Count > 0)
                    {
                        if (EditOrPrview == 1)
                        {
                            StaticData.specialEnrollment = response.specialCriminalProfileList[0];
                            StaticData.ModifiableSpecialEnrollment = false;
                            parent.AddChild(Globals.ChildControllers.SPECIAL_ENTRY);
                        }
                        else if (EditOrPrview == 2)
                        {
                            StaticData.specialEnrollment = response.specialCriminalProfileList[0];

                            SpecialProfilePreviewForm previewForm = new SpecialProfilePreviewForm();
                            previewForm.ShowDialog();
                        }
                    }
                    else
                    {
                        CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not fetch profile");
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(response.message))
                    {
                        CustomMessageBox.ShowMessage("SNSOP TOOLS", response.message);
                    }
                    else
                    {
                        CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not fetch profile");
                    }
                }
            }
            else
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not fetch profile");
            }
        }

        public void GoBacktoDashboard()
        {
            ((MainController)parent).OnHome();
        }
    }
}
