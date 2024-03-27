using ISTL.COMMON;
using ISTL.COMMON.Common;
using ISTL.COMMON.Network;
using ISTL.MODELS.DTO.Enrollment;
using ISTL.MODELS.Request.Adjudication;
using ISTL.MODELS.Response.Adjudication;
using ISTL.RAB.Controllers;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace ISTL.RAB.View
{
    public partial class PersonMatchDialogForm : ViewForm
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        public GetMatchListRequest request = new GetMatchListRequest();
        public GetMatchListResponse response = new GetMatchListResponse();
        public string ReferenceNumber;
        private readonly string GetMatchListEndpoint = ConfigurationManager.
           AppSettings["GetMatchListEndpoint"].ToString();

        public byte[] MasterPhoto { get; set; }
        public int Position = 0;
        public int TotalMatchCount = 0;
        public List<PersonDataDto> passportDataList;

        public PersonMatchDialogForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad()
        {
            btnMatch.Hide();
            btnNotMatch.Hide();
            lblMatchFoundFlag.Text = "MATCH NOT FOUND";
            lblMatchFoundFlag.ForeColor = Color.Red;
            lblMatchPercentage.Text = "";
            btnNext.Hide();
            btnPrevious.Hide();

            LoadMatchedData();
        }

        private int count = 1;

        public void LoadMatchedData()
        {
            string erroMsg = null;

            var json = new JavaScriptSerializer().Serialize(request);

            if (request.referenceNo == null)
            {
                return;
            }

            if (count == 5)
            {
                return;
            }

            ProcessingDialog.Run(delegate ()
            {
                while (response.total == 0 || response.total == null)
                {

                    if (count == 5) break;

                    try
                    {
                        response = NetworkService.SubmitBioRequest<GetMatchListResponse>
                        (request, GetMatchListEndpoint + "?token=abc", "POST", null);
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
                        if (response.passportDataList != null && response.total > 0)
                        {
                            passportDataList = response.passportDataList;
                            TotalMatchCount = Convert.ToInt32(response.total);                            
                            break;
                        }
                    }

                    System.Threading.Thread.Sleep(Convert.ToInt32(ConfigurationManager.AppSettings["ThreadSleepTimeInMS"].ToString()));
                    count += 1;
                }
            });

            if (response.total > 0)
            {
                SetMasterData();
                SetMatchResult(0);
            }
        }
        private void SetMatchResult(int position)
        {
            if (passportDataList == null || passportDataList.Count == 0)
            {
                btnMatch.Hide();
                btnNotMatch.Hide();
                lblMatchFoundFlag.Text = "MATCH NOT FOUND";
                lblMatchFoundFlag.ForeColor = Color.Red;
                lblMatchPercentage.Text = "";
                btnNext.Hide();
                btnPrevious.Hide();

                return;
            }

            btnMatch.Show();
            btnNotMatch.Show();
            lblMatchFoundFlag.Text = "MATCH FOUND";
            lblMatchFoundFlag.ForeColor = Color.Green;
            btnNext.Show();
            btnPrevious.Show();

            this.lblNumberOfMatches.Text = "Number of Matches Found: " + TotalMatchCount;
            this.tabPage1.Text = "Match " + (Position + 1);

            PersonDataDto dto = passportDataList[position];

            if (dto.matchScore != null)
            {
                lblMatchPercentage.Text = "Match Score: " + dto.matchScore;
            }

            tbNIDmatch1.Text = dto.nationalId;
            tbFullNameMatch1.Text = dto.fullName;
            tbDOBMatch1.Text = dto.dateOfBirth;
            tbGenderMatch1.Text = dto.gender;
            tbNickNameMatch1.Text = dto.nickName;
            tbCriminalNameMatch1.Text = dto.alias;
            tbPhoneMatch1.Text = dto.phone;
            tbOccupationMatch1.Text = dto.occupation;

            pictureBoxMatch1.Image = GraphicsManager.GetInstance().ByteArrayToImage(dto?.personBiometric?.photo);
        }

        private void SetMasterData()
        {
            tbRefNo.Text = ReferenceNumber;

            if (MasterPhoto != null)
            {
                pictureBoxPhoto.Image = GraphicsManager.GetInstance().ByteArrayToImage(MasterPhoto);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (Position < (TotalMatchCount - 1))
            {
                Position += 1;
            }
            SetMatchResult(Position);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (Position > 0)
            {
                Position -= 1;
            }
            SetMatchResult(Position);
        }
    }
}