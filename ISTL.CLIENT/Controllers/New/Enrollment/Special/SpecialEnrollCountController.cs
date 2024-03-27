using ISTL.COMMON;
using ISTL.MODELS.Common;
using ISTL.MODELS.DTO.New.Enrollment.Special;
using ISTL.MODELS.Request.New;
using ISTL.MODELS.Response.New.Special;
using ISTL.PERSOGlobals;
using ISTL.RAB.ApiManager;
using ISTL.RAB.Entity;
using ISTL.RAB.View;
using ISTL.RAB.View.New.Enrollment.Special;
using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.Controllers.New.Enrollment.Special
{
    public class SpecialEnrollCountController : ViewController
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private SpecialEnrollCountUserControl specialEnrollCountUserControl;
        public SpecialEnrollCountController()
        {
            specialEnrollCountUserControl = new SpecialEnrollCountUserControl();
            base.SetView((IView)specialEnrollCountUserControl);
            specialEnrollCountUserControl.SetController(this);
        }

        public override string GetName()
        {
            return Globals.ChildControllers.SPECIAL_COUNT;
        }

        public void OnSubmit()
        {
            SpecialArrestTypeCountDto request = new SpecialArrestTypeCountDto();
            ApiResponse response = new ApiResponse();

            request.contagiousPatients = (!string.IsNullOrEmpty(specialEnrollCountUserControl.ContagoiusPatientCount)) ?
                Convert.ToInt32(specialEnrollCountUserControl.ContagoiusPatientCount) : 0;
            request.directSubmitInPS = (!string.IsNullOrEmpty(specialEnrollCountUserControl.DirectSubmitPS)) ?
                Convert.ToInt32(specialEnrollCountUserControl.DirectSubmitPS) : 0;
            request.mobileCourts = (!string.IsNullOrEmpty(specialEnrollCountUserControl.MobileCourtCount)) ?
                Convert.ToInt32(specialEnrollCountUserControl.MobileCourtCount) : 0;

            request.unit = Users.Unit;
            request.subUnit = Users.SubUnit;

            if (!string.IsNullOrEmpty(specialEnrollCountUserControl.DateOfCount))
            {
                try
                {
                    DateTime dt = DateTime.ParseExact(specialEnrollCountUserControl.DateOfCount, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    request.countDate = dt.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                }
                catch (Exception)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please input correct date");
                    return;
                }
            }

            if (string.IsNullOrEmpty(Users.AccessToken))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "You are logged in offline");
                return;
            }

            string errorMsg = null;

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = new SpecialEnrollApiManager().SubmitSpecialArrestTypeCount(request);
                }
                catch (WebException x)
                {
                    logger.Debug("Known error, when server not found while submitting count. Error Message: " + x.Message);
                    errorMsg = "Seems you are Offline while submitting count. Please contact with your Administrator.";
                }
                catch (TimeoutException x)
                {
                    logger.Debug("Connection timedout while submitting count. Error Message: " + x.Message);
                    errorMsg = "Seems you are Offline while submitting count. Please contact with your Administrator.";
                }
                catch (Exception x)
                {
                    logger.Error("There was an unexpected error while submitting count.\n" + x.ToString());
                    errorMsg = "There was an unexpected error while submitting count. Please contact with your Administrator.";
                }
            });

            if (!string.IsNullOrEmpty(errorMsg))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", errorMsg);
                return;
            }

            if (response?.code == 200)
            {
                if (!string.IsNullOrEmpty(response?.message))
                {
                    InfoMessageBox.ShowMessage("SNSOP TOOLS", response?.message);
                }
                else
                {
                    InfoMessageBox.ShowMessage("SNSOP TOOLS", "Saved count successfully");
                }
                specialEnrollCountUserControl.SetResult(request.mobileCourts?.ToString(), request.contagiousPatients?.ToString(),
                    request.directSubmitInPS?.ToString());
            }
            else
            {
                if (!string.IsNullOrEmpty(response?.message))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", response?.message);
                }
                else if (!string.IsNullOrEmpty(Users.AccessToken))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not save count successfully");
                }
            }
        }

        public void OnSearch()
        {
            if (string.IsNullOrEmpty(Users.AccessToken))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "You are logged in offline");
                return;
            }

            GetSpecialArrestTypeCountRequest request = new GetSpecialArrestTypeCountRequest();
            GetSpecialArrestTypeCountResponse response = new GetSpecialArrestTypeCountResponse();

            if (!string.IsNullOrEmpty(specialEnrollCountUserControl.DateOfCount))
            {
                try
                {
                    DateTime dt = DateTime.ParseExact(specialEnrollCountUserControl.DateOfCount, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    request.countDate = dt.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                }
                catch (Exception)
                {
                    return;
                }
            }
            request.limit = 1;
            request.subUnit = Users.SubUnit;
            //request.unit = Users.Unit;

            string errorMsg = null;

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = new SpecialEnrollApiManager().GetSpecialArrestTypeCount(request);
                }
                catch (WebException x)
                {
                    logger.Debug("Known error, when server not found while searching arrest count. Error Message: " + x.Message);
                    errorMsg = "Seems you are Offline while searching arrest count. Please contact with your Administrator.";
                }
                catch (TimeoutException x)
                {
                    logger.Debug("Connection timedout while searching arrest count. Error Message: " + x.Message);
                    errorMsg = "Seems you are Offline while searching arrest count. Please contact with your Administrator.";
                }
                catch (Exception x)
                {
                    logger.Error("There was an unexpected error while searching arrest count.\n" + x.ToString());
                    errorMsg = "There was an unexpected error while searching arrest count. Please contact with your Administrator.";
                }
            });

            if (!string.IsNullOrEmpty(errorMsg))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", errorMsg);
                return;
            }

            if (response?.code == 200)
            {
                if (!string.IsNullOrEmpty(response?.message))
                {
                    InfoMessageBox.ShowMessage("SNSOP TOOLS", response?.message);
                }
                else
                {
                    if (response?.specialCriminalCountList != null)
                    {
                        if (response?.specialCriminalCountList.Count > 0)
                        {
                            string MobileCourtCount = response?.specialCriminalCountList[0]?.mobileCourts?.ToString();
                            string contagiousCount = response?.specialCriminalCountList[0]?.contagiousPatients?.ToString();
                            string directSubmitinPSCount = response?.specialCriminalCountList[0]?.directSubmitInPS?.ToString();
                            specialEnrollCountUserControl.SetResult(MobileCourtCount, contagiousCount, directSubmitinPSCount);
                        }                        
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(response?.message))
                        {
                            CustomMessageBox.ShowMessage("SNSOP TOOLS", response?.message);
                            return;
                        }
                        else
                        {
                            CustomMessageBox.ShowMessage("SNSOP TOOLS", "No result found");
                            return;
                        }
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(response?.message))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", response?.message);
                }
                else
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "No result found");
                }
            }
        }

        public void GoBacktoDashboard()
        {
            ((MainController)parent).OnHome();
        }
    }
}
