using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using NLog;
using ISTL.COMMON;
using ISTL.COMMON.Common;
using ISTL.RAB.Entity;
using ISTL.RAB.View;
using ISTL.PERSOGlobals;
using ISTL.COMMON.CommandManager;
using ISTL.MODELS.DTO.Enrollment;
using ISTL.MODELS.DTO.Lookup;
using System.Drawing;
using ISTL.COMMON.Entity;
using System.Configuration;
using System.Net;
using System.ServiceModel;
using ISTL.COMMON.Network;
using ISTL.MODELS.Response;
using System.Web.Script.Serialization;
using System.IO;
using ISTL.MODELS.Request;
using ISTL.MODELS.Request.Adjudication;

namespace ISTL.RAB.Controllers
{
    public class EnrollController : ViewController
    {
        #region Declaration(s)
        private Logger logger = LogManager.GetCurrentClassLogger();
        private EnrollUserControl enrollUserControl;
        private List<LookupDto> placeOfBirthList;
        private List<LookupDto> nationalityList;
        private List<LookupDto> genderList;
        private List<LookupDto> permanentDivisionList;
        private List<LookupDto> permanentDistrictList;
        private List<LookupDto> permanentStationList;
        private List<LookupDto> permanentUpazilaList;
        private List<LookupDto> permanentUnionList;
        private List<LookupDto> permanentPostCodeList;
        private List<LookupDto> applicationStatusList;
        private List<LookupDto> bloodGroupList;
        private List<Fingers> scannedFingers;

        private readonly string EnrollmentAddEndpoint = ConfigurationManager.AppSettings["EnrollmentAddEndpoint"].ToString();
        #endregion

        #region Constructor(s)
        public EnrollController()
        {
            enrollUserControl = new EnrollUserControl();
            base.SetView((IView)enrollUserControl);
            enrollUserControl.SetController(this);

            //Use dependency injection container
        }
        #endregion

        #region Method(s)        
        public override void OnLoad()
        {
            base.OnLoad();
            this.SetViewInfo();
            InitializeCommandManager();
        }

        private void SetViewInfo()
        {
            enrollUserControl.HideNotification();
            enrollUserControl.ClearText();

            PersonDataDto dto = ((MainController)parent).PersonData;
            if (dto != null)
            {
                LoadEnrolledDataDetails(dto);
                ((MainController)parent).PersonData = null;
            }
        }
        private void InitializeCommandManager()
        {
            CommandManager cmdMgr = ((MainController)parent).cmdMgr;

            cmdMgr.Commands[Globals.Commands.SAVE].AddExecuteHandler(OnSave);
            cmdMgr.Commands[Globals.Commands.CANCEL].AddExecuteHandler(OnCancel);

            // Show buttons
            cmdMgr.Commands[Globals.Commands.SAVE].Checked = true;
            cmdMgr.Commands[Globals.Commands.CANCEL].Checked = true;
            cmdMgr.Commands[Globals.Commands.TAKE].Checked = false;
        }

        
        private void LoadEnrolledDataDetails(PersonDataDto dto)
        {
            enrollUserControl.SetPersonEnrollmentData(dto);
        }

        public void TakeMockPhoto()
        {
            Image img = MockImageLoad();
            if (img == null) return;

            if (GraphicsManager.GetInstance().ImageToByteArray(img).Length > 100 * 1024)
            {
                MessageBox.Show("Mock photo size is too long. Size should not exceed 100KB.");
                return;
            }
            enrollUserControl.Photo = img;
        }

        public void TakeFingerprints()
        {
            MessageBox.Show("Fingerprints capture module development is on progress. \nPress 'Ctrl + Alt + Click on Take Fingerprints Button' to open and set mock Fingerprints.");
            return;

            try
            {
                if (enrollUserControl.RightIndex != null || enrollUserControl.RightThumb != null
                    || enrollUserControl.LeftIndex != null || enrollUserControl.LeftThumb != null
                    || enrollUserControl.RightMiddle != null || enrollUserControl.RightRing != null ||
                    enrollUserControl.LeftMiddle != null || enrollUserControl.LeftRing != null ||
                    enrollUserControl.RightSmall != null || enrollUserControl.LeftSmall != null)
                {
                    if (MessageBox.Show("RAB POC",
                    "FP is already exist. Do you want to capture again?", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                }

                //***
                // TODO: below dummay commented code written by Al-Amin on 03 May 21, can be helpful for future development
                //***


                //if (!frmFP.DeviceFound)
                //{
                //    logger.Error("Error from Photo Capture API: " + frmFP.error);
                //    MessageBox.Show("Device not connected");
                //    return;
                //}


                //frmFP.ActiveScanList = new int[]
                //                       {
                //                          Globals.Biometric.RIGHT_INDEX,
                //                          Globals.Biometric.RIGHT_MIDDLE,
                //                          Globals.Biometric.RIGHT_RING,
                //                          Globals.Biometric.RIGHT_SMALL,
                //                          Globals.Biometric.LEFT_SMALL,
                //                          Globals.Biometric.LEFT_RING,
                //                          Globals.Biometric.LEFT_MIDDLE,
                //                          Globals.Biometric.LEFT_INDEX,
                //                          Globals.Biometric.LEFT_THUMB,
                //                          Globals.Biometric.RIGHT_THUMB
                //                       };


                //frmFP.ShowDialog();

                //System.Collections.ArrayList arrCaptured = frmFP.CaptureInfo;

                System.Collections.ArrayList arrCaptured = new System.Collections.ArrayList();
                if (arrCaptured.Count == 0)
                {
                    MessageBox.Show("RAB POC", "No FP captured.");
                    return;
                }
                else if (arrCaptured.Count > 0)
                {
                    //Call a method to bio match
                }

                scannedFingers = new List<Fingers>();

                enrollUserControl.RightThumb = null;
                enrollUserControl.RightIndex = null;
                enrollUserControl.RightMiddle = null;
                enrollUserControl.RightRing = null;
                enrollUserControl.RightSmall = null;

                enrollUserControl.LeftThumb = null;
                enrollUserControl.LeftIndex = null;
                enrollUserControl.LeftMiddle = null;
                enrollUserControl.LeftRing = null;
                enrollUserControl.LeftSmall = null;

                //for (int i = 0; i < arrCaptured.Count; i++)
                //{
                //    CaptureDetails CapturedFinger =  arrCaptured[i];
                //    if (CapturedFinger.wsq != null)
                //    {
                //        scannedFingers.Add(new Fingers(Convert.ToSByte(CapturedFinger.FingerIndex), CapturedFinger.minex, CapturedFinger.wsq, null));
                //        switch (CapturedFinger.FingerIndex)
                //        {
                //            case Globals.Biometric.RIGHT_THUMB:
                //                enrollUserControl.RightThumb = CapturedFinger.img;
                //                break;
                //            case Globals.Biometric.RIGHT_INDEX:
                //                enrollUserControl.RightIndex = CapturedFinger.img;
                //                break;

                //            case Globals.Biometric.RIGHT_MIDDLE:
                //                enrollUserControl.RightMiddle = CapturedFinger.img;
                //                break;

                //            case Globals.Biometric.RIGHT_RING:
                //                enrollUserControl.RightRing = CapturedFinger.img;
                //                break;

                //            case Globals.Biometric.RIGHT_SMALL:
                //                enrollUserControl.RightSmall = CapturedFinger.img;
                //                break;

                //            case Globals.Biometric.LEFT_THUMB:
                //                enrollUserControl.LeftThumb = CapturedFinger.img;
                //                break;

                //            case Globals.Biometric.LEFT_INDEX:
                //                enrollUserControl.LeftIndex = CapturedFinger.img;
                //                break;

                //            case Globals.Biometric.LEFT_MIDDLE:
                //                enrollUserControl.LeftMiddle = CapturedFinger.img;
                //                break;

                //            case Globals.Biometric.LEFT_RING:
                //                enrollUserControl.LeftRing = CapturedFinger.img;
                //                break;

                //            case Globals.Biometric.LEFT_SMALL:
                //                enrollUserControl.LeftSmall = CapturedFinger.img;
                //                break;
                //            default:
                //                logger.Error("There was an unidentified finger index = " + CapturedFinger.FingerIndex
                //                    + ". SKIPPING");
                //                break;
                //        }
                //    }
                //}
            }
            catch (System.Exception ex)
            {
                logger.Error("There was an unexpected error when attempting to capture fingerprints.\n", ex.ToString());
            }
        }

        public void TakeMockFingerprints()
        {
            Image img = MockImageLoad();
            if (img == null) return;

            if (GraphicsManager.GetInstance().ImageToByteArray(img).Length > 20 * 1024)
            {
                MessageBox.Show("Mock fingerprint size is too long. Size should not exceed 20KB.");
                return;
            }

            enrollUserControl.LeftThumb = img;
            enrollUserControl.LeftIndex = img;
            enrollUserControl.LeftMiddle = img;
            enrollUserControl.LeftRing = img;
            enrollUserControl.LeftSmall = img;

            enrollUserControl.RightThumb = img;
            enrollUserControl.RightIndex = img;
            enrollUserControl.RightMiddle = img;
            enrollUserControl.RightRing = img;
            enrollUserControl.RightSmall = img;
        }
        private Image MockImageLoad()
        {
            DialogResult result = enrollUserControl.OpenFile();
            if (result == DialogResult.OK)
            {
                return Image.FromFile(enrollUserControl.GetFileName());
            }
            return null;
        }
        public void TakePhoto()
        {
            MessageBox.Show("Photo capture module development is on progress. \nPress 'Ctrl + Alt + Click on Take Photo Button' to open and set mock photo.");
        }
        public bool ValidateData()
        {
            string errorMessage = "";
            if (string.IsNullOrWhiteSpace(enrollUserControl.FullName))
            {
                errorMessage += "Please provide Full Name.\n";
                enrollUserControl.FullNameNotification = "Please provide Full Name.";
            }

            if (string.IsNullOrWhiteSpace(enrollUserControl.NID))
            {
                errorMessage += "Please provide NID.\n";
                enrollUserControl.NIDNotification = "Please provide Nid.";
            }

            if (!Utils.ValidateDate(enrollUserControl.Year, enrollUserControl.Month, enrollUserControl.Day))
            {
                errorMessage += "Please provide Valid Date of Birth.\n";
                enrollUserControl.DateOfBirthNotification = "Please provide Valid Date of Birth.";
            }

            if (errorMessage != "")
            {
                MessageBox.Show("Please correct the following error(s):" + "\n\n" + errorMessage,
                    "RAB POC Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void OnSave(Command cmd)
        {
            OnSavingOperation();
        }

        public void OnSavingOperation()
        {
            bool validation = ValidateData();
            if (validation == false)
            {
                return;
            }

            string erroMsg = string.Empty;

            EnrollmentAddResponse response = new EnrollmentAddResponse();
            PersonDataDto request = new PersonDataDto();

            String dobFromEnrollForm = enrollUserControl.Day + enrollUserControl.Month 
                + enrollUserControl.Year;

            if (!string.IsNullOrEmpty(dobFromEnrollForm))
            {
                DateTime dt = DateTime.ParseExact(dobFromEnrollForm, "ddMMyyyy",
                        System.Globalization.CultureInfo.InvariantCulture);
                request.dateOfBirth = dt.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            }

            request.alias = enrollUserControl.CriminalName;
            request.createdBy = Users.FullName;
            request.createdOn = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            request.fullName = enrollUserControl.FullName;
            request.gender = enrollUserControl.Gender;
            request.nationalId = enrollUserControl.NID;
            request.nickName = enrollUserControl.NickName;
            request.occupation = enrollUserControl.Occupation;
            request.phone = enrollUserControl.Phone;


            /*

            //demo code written by Al-Amin on 8 May 2021
            request.personBiometric = new PersonBiometricDto()
            {
               // photo = File.ReadAllBytes("D:\\sample\\photo_1.png"),
               // wsqRt = File.ReadAllBytes("D:\\sample\\wsq_rt_1.wsq"),
               // wsqRi = File.ReadAllBytes("D:\\sample\\wsq_ri_1.wsq"),
                leftIris = File.ReadAllBytes("D:\\sample\\1_left_iris.png"),
                rightIris = File.ReadAllBytes("D:\\sample\\1_right_iris.png"),
            };
            //demo code written by Al-Amin on 8 May 2021

            */

            
            PersonBiometricDto bioDto = new PersonBiometricDto();
            bioDto.leftIris = enrollUserControl.irisData.LeftIris;
            bioDto.rightIris = enrollUserControl.irisData.RightIris;
            bioDto.photo = enrollUserControl.camData.CamImage;

            bioDto.wsqRt = enrollUserControl.fingerprintData.WsqRt;
            bioDto.wsqRi = enrollUserControl.fingerprintData.WsqRi;
            bioDto.wsqRm = enrollUserControl.fingerprintData.WsqRm;
            bioDto.wsqRr = enrollUserControl.fingerprintData.WsqRr;
            bioDto.wsqRl = enrollUserControl.fingerprintData.WsqRl;

            bioDto.wsqLt = enrollUserControl.fingerprintData.WsqLt;
            bioDto.wsqLi = enrollUserControl.fingerprintData.WsqLi;
            bioDto.wsqLm = enrollUserControl.fingerprintData.WsqLm;
            bioDto.wsqLr = enrollUserControl.fingerprintData.WsqLr;
            bioDto.wsqLl = enrollUserControl.fingerprintData.WsqLl;
            

            request.personBiometric = bioDto;

            //string transType = enrollUserControl.TransactionType;
            request.transactionType = "Enrol";

            JavaScriptSerializer jSerial = new JavaScriptSerializer();
            jSerial.MaxJsonLength = 999999999;

            var json = jSerial.Serialize(request);

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = NetworkService.SubmitRequest<EnrollmentAddResponse>
                    (request, EnrollmentAddEndpoint + "?token=abc", null);
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
                if (!string.IsNullOrEmpty(response.responseMessage))
                {
                    MessageBoxController.ShowInfo("RAB CDMS", response.responseMessage);
                }
            }
        }

        private void OnCancel(Command cmd)
        {
            if (MessageBox.Show("Are you sure you want to cancel this Enrollment?", "ISTL Warning", MessageBoxButtons.YesNo)
            == DialogResult.Yes)
            {
                //Clear static data
                ((MainController)parent).PersonId = 0;
                SelectedEntity.Person = null;

                logger.Debug("Calling cancel");
                parent.AddChild(Globals.ChildControllers.DEFAULT);
            }
        }

        public void OnCancelOperation()
        {
            if (MessageBox.Show("Are you sure you want to cancel this Enrollment?", "ISTL Warning", MessageBoxButtons.YesNo)
            == DialogResult.Yes)
            {
                logger.Debug("Calling cancel");
                parent.AddChild(Globals.ChildControllers.DEFAULT);
            }
        }

        public override string GetName()
        {
            return Globals.ChildControllers.ENROLL;
        }
        public override void OnClosing()
        {
            base.OnClosing();

            //Clear static data
            ((MainController)parent).PersonId = 0;
            SelectedEntity.Person = null;

            UnRegisterCommands();
        }
        private void UnRegisterCommands()
        {
            CommandManager cmdMgr = ((MainController)parent).cmdMgr;

            cmdMgr.Commands[Globals.Commands.SAVE].RemoveExecuteHandler(OnSave);
            cmdMgr.Commands[Globals.Commands.CANCEL].RemoveExecuteHandler(OnCancel);
            cmdMgr.Commands[Globals.Commands.TAKE].RemoveExecuteHandler(OnSave);

            // Hide buttons
            cmdMgr.Commands[Globals.Commands.SAVE].Checked = false;
            cmdMgr.Commands[Globals.Commands.CANCEL].Checked = false;
            cmdMgr.Commands[Globals.Commands.TAKE].Checked = false;
        }
        public void OnFocus()
        {
            enrollUserControl.Focus();
        }
        #endregion
    }
}
