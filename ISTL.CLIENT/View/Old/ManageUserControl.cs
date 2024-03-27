using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ISTL.COMMON;
using ISTL.MODELS.DTO.Enrollment;
using ISTL.RAB.Controllers;
using ISTL.PERSOGlobals;
using System.Configuration;
using ISTL.MODELS.Response;
using ISTL.COMMON.Network;
using System.Net;
using System.ServiceModel;
using NLog;
using ISTL.MODELS.Request;
using ISTL.COMMON.Common;
using System.Web.Script.Serialization;

namespace ISTL.RAB.View
{
    public partial class ManageUserControl : ViewUserControl
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private readonly string GetEnrollmentListEndpoint = ConfigurationManager.
            AppSettings["EnrollmentListEndpoint"].ToString();
        public ManageUserControl()
        {
            InitializeComponent();
        }
        public object GridViewPerson
        {
            set
            {
                gridViewEnrollment.Rows.Clear();
                int sl = 1;
                if (value != null)
                {
                    var list = value as IList<PersonEnrollmentDto>;
                    foreach (var obj in list)
                    {
                        gridViewEnrollment.Rows.Add(new object[] { sl++, obj.Id, obj.FirstNameEn, obj.MiddleNameEn,
                        obj.MobileNumber, obj.PermanentAddress });
                    }
                }
            }
        }

        public void SetEnrollmentListDetails(List<PersonDataDto> passportDataList)
        {
            if (passportDataList == null)
            {
                return;
            }
            else
            {
                gridViewEnrollment.Rows.Clear();

                if (passportDataList.Count > 0)
                {
                    for (int i = 0; i < passportDataList.Count; i++)
                    {
                        PersonDataDto dto = passportDataList[i];
                        gridViewEnrollment.Rows.Add(position + (i + 1), dto.id, dto.fullName, dto.nickName,
                            dto.alias, dto.phone, dto.dateOfBirth, dto.transactionType, dto.afisStatus,
                            dto.referenceNumber, null, null, dto.nationalId, dto.occupation, dto.gender);
                    }
                    gridViewEnrollment.Rows[0].Selected = true;
                }

                gridViewEnrollment.Focus();
            }
        }

        private void ShowProfile(string refNo)
        {
            string erroMsg = null;

            EnrollmentListSearchRequest request = new EnrollmentListSearchRequest();
            EnrollmentListSearchResponse response = new EnrollmentListSearchResponse();

            request.referenceNumber = refNo;
            request.limit = 10;

            var json = new JavaScriptSerializer().Serialize(request);

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = NetworkService.SubmitRequest<EnrollmentListSearchResponse>
                    (request, GetEnrollmentListEndpoint + "?token=abc", null);
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
                return;
            }

            PersonDataDto personObj = new PersonDataDto();

            if (response.operationResult)
            {
                personObj = response.passportDataList[0];
            }

            ((ManageController)controller).OnEnrollmentData(personObj);
        }

        private void gridViewEnrollment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 10)
            {
                if (gridViewEnrollment.CurrentRow?.Cells[7]?.Value?.ToString() == "Enrol")
                {
                    string refNoReq = gridViewEnrollment.CurrentRow?.Cells[9]?.Value?.ToString();
                    ShowProfile(refNoReq);
                }
                else if (gridViewEnrollment.CurrentRow?.Cells[7]?.Value?.ToString() == "Identification"
                    && gridViewEnrollment.CurrentRow?.Cells[8]?.Value?.ToString() == "Match Found")
                {

                    string refNoReq = gridViewEnrollment.CurrentRow?.Cells[9]?.Value?.ToString();

                    //PersonDataDto personObj = new PersonDataDto()
                    //{
                    //    referenceNumber = refNoReq,
                    //    fullName = gridViewEnrollment.CurrentRow?.Cells[2]?.Value?.ToString(),
                    //    nickName = gridViewEnrollment.CurrentRow?.Cells[3]?.Value?.ToString(),
                    //    alias = gridViewEnrollment.CurrentRow?.Cells[4]?.Value?.ToString(),
                    //    phone = gridViewEnrollment.CurrentRow?.Cells[5]?.Value?.ToString(),
                    //    dateOfBirth = gridViewEnrollment.CurrentRow?.Cells[6]?.Value?.ToString(),
                    //    nationalId = gridViewEnrollment.CurrentRow?.Cells[12]?.Value?.ToString(),
                    //    occupation = gridViewEnrollment.CurrentRow?.Cells[13]?.Value?.ToString(),
                    //    gender = gridViewEnrollment.CurrentRow?.Cells[14]?.Value?.ToString()

                    //};

                    string erroMsg = null;

                    EnrollmentListSearchRequest request = new EnrollmentListSearchRequest();
                    EnrollmentListSearchResponse response = new EnrollmentListSearchResponse();

                    request.referenceNumber = refNoReq;
                    request.limit = 10;

                    var json = new JavaScriptSerializer().Serialize(request);

                    ProcessingDialog.Run(delegate ()
                    {
                        try
                        {
                            response = NetworkService.SubmitRequest<EnrollmentListSearchResponse>
                            (request, GetEnrollmentListEndpoint + "?token=abc", null);
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
                        return;
                    }

                    PersonDataDto personObj = new PersonDataDto();

                    if (response.operationResult)
                    {
                        personObj = response.passportDataList[0];
                    }

                    ((ManageController)controller).OnShowPersonMatchResult(personObj);                    
                }
                else if (gridViewEnrollment.CurrentRow?.Cells[7]?.Value?.ToString() == "Identification"
                    && gridViewEnrollment.CurrentRow?.Cells[8]?.Value?.ToString() != "Match Found")
                {

                    string refNoReq = gridViewEnrollment.CurrentRow?.Cells[9]?.Value?.ToString();

                    string erroMsg = null;

                    EnrollmentListSearchRequest request = new EnrollmentListSearchRequest();
                    EnrollmentListSearchResponse response = new EnrollmentListSearchResponse();

                    request.referenceNumber = refNoReq;
                    request.limit = 10;

                    var json = new JavaScriptSerializer().Serialize(request);

                    ProcessingDialog.Run(delegate ()
                    {
                        try
                        {
                            response = NetworkService.SubmitRequest<EnrollmentListSearchResponse>
                            (request, GetEnrollmentListEndpoint + "?token=abc", null);
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
                        return;
                    }

                    PersonDataDto personObj = new PersonDataDto();

                    if (response.operationResult)
                    {
                        personObj = response.passportDataList[0];
                    }

                    ((ManageController)controller).OnShowPersonMatchResult(personObj);                    
                }
            }
        }

        public int position = 0;
        public int totalInEnrollmentList = 0;
        private void btnStartingPosition_Click(object sender, EventArgs e)
        {
            position = 0;

            ((ManageController)controller).request.startIndex = position;

            ((ManageController)controller).LoadEnrolledData();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (position >= 10)
            {
                position -= 10;
                ((ManageController)controller).request.startIndex = position;

                ((ManageController)controller).LoadEnrolledData();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (position + 10 < totalInEnrollmentList)
            {
                position += 10;
                ((ManageController)controller).request.startIndex = position;
                ((ManageController)controller).LoadEnrolledData();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (totalInEnrollmentList > 0)
            {
                position = totalInEnrollmentList - (totalInEnrollmentList % 10);
                ((ManageController)controller).request.startIndex = position;
                ((ManageController)controller).LoadEnrolledData();
            }
        }
    }
}
