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
    public class MatchController : ViewController
    {
        #region Declaration(s)
        private Logger logger = LogManager.GetCurrentClassLogger();
        private MatchUserControl matchUserControl;
        private List<Fingers> scannedFingers;
        private readonly string EnrollmentAddEndpoint = ConfigurationManager.AppSettings["EnrollmentAddEndpoint"].ToString();
        #endregion

        #region Constructor(s)
        public MatchController()
        {
            matchUserControl = new MatchUserControl();
            base.SetView((IView)matchUserControl);
            matchUserControl.SetController(this);

            //Use dependency injection container
        }
        #endregion

        #region Method(s)        
        public override void OnLoad()
        {
            base.OnLoad();
            InitializeController();
            InitializeCommandManager();
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
            parent.AddChild(Globals.ChildControllers.ENROLL_MATCH);
        }

        private void InitializeController()
        {
            matchUserControl.InitUserControl();
        }
        private void InitializeCommandManager()
        {
            CommandManager cmdMgr = ((MainController)parent).cmdMgr;

            //cmdMgr.Commands[Globals.Commands.SAVE].AddExecuteHandler(OnSave);
            cmdMgr.Commands[Globals.Commands.TAKE].AddExecuteHandler(OnSave);
            cmdMgr.Commands[Globals.Commands.CANCEL].AddExecuteHandler(OnCancel);

            // Show buttons
            //cmdMgr.Commands[Globals.Commands.SAVE].Checked = true;
            cmdMgr.Commands[Globals.Commands.TAKE].Checked = true;
            cmdMgr.Commands[Globals.Commands.CANCEL].Checked = true;
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
            matchUserControl.Photo = img;
        }

        public void TakeFingerprints()
        {
            MessageBox.Show("Fingerprints capture module development is on progress. \nPress 'Ctrl + Alt + Click on Take Fingerprints Button' to open and set mock Fingerprints.");
            return;

            try
            {
                if (matchUserControl.RightIndex != null || matchUserControl.RightThumb != null
                    || matchUserControl.LeftIndex != null || matchUserControl.LeftThumb != null
                    || matchUserControl.RightMiddle != null || matchUserControl.RightRing != null ||
                    matchUserControl.LeftMiddle != null || matchUserControl.LeftRing != null ||
                    matchUserControl.RightSmall != null || matchUserControl.LeftSmall != null)
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

                matchUserControl.RightThumb = null;
                matchUserControl.RightIndex = null;
                matchUserControl.RightMiddle = null;
                matchUserControl.RightRing = null;
                matchUserControl.RightSmall = null;

                matchUserControl.LeftThumb = null;
                matchUserControl.LeftIndex = null;
                matchUserControl.LeftMiddle = null;
                matchUserControl.LeftRing = null;
                matchUserControl.LeftSmall = null;

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

            matchUserControl.LeftThumb = img;
            matchUserControl.LeftIndex = img;
            matchUserControl.LeftMiddle = img;
            matchUserControl.LeftRing = img;
            matchUserControl.LeftSmall = img;

            matchUserControl.RightThumb = img;
            matchUserControl.RightIndex = img;
            matchUserControl.RightMiddle = img;
            matchUserControl.RightRing = img;
            matchUserControl.RightSmall = img;
        }

        private Image MockImageLoad()
        {
            DialogResult result = matchUserControl.OpenFile();
            if (result == DialogResult.OK)
            {
                return Image.FromFile(matchUserControl.GetFileName());
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
            if (!matchUserControl.MatchByPhoto.Checked 
                && !matchUserControl.MatchByFP.Checked
                && !matchUserControl.MatchByIris.Checked)
            {
                errorMessage += "Please select Match By.\n";
                matchUserControl.MatchByNotification = "Please select Match By.";
            }

            if (matchUserControl.MatchByPhoto.Checked
                && matchUserControl.Photo == null)
            {
                errorMessage += "Please provide Photo.\n";
                matchUserControl.PhotoNotification = "Please provide Photo.";
            }

            //TODO: do the rest for FP Iris. Apply OR condition on the bio fields

            if (matchUserControl.MatchByIris.Checked
                && matchUserControl.IrisLeft == null && matchUserControl.IrisRight == null)
            {
                errorMessage += "Please provide Iris photo.\n";
                matchUserControl.IrisNotification = "Please provide Iris photo.";
            }

            if (matchUserControl.MatchByFP.Checked
                && matchUserControl.LeftThumb == null && matchUserControl.LeftIndex == null && matchUserControl.LeftMiddle == null
                && matchUserControl.LeftRing == null && matchUserControl.LeftSmall == null
                && matchUserControl.RightThumb == null && matchUserControl.RightIndex == null && matchUserControl.RightMiddle == null
                && matchUserControl.RightRing == null && matchUserControl.RightSmall == null)
            {
                errorMessage += "Please provide Fingerprint photo.\n";
                matchUserControl.FingerNotification = "Please provide Fingerprint photo.";
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

            /*

            //TODO: Demo Code to catch Match Type from controller. Al-Amin/18-May-21
            if (matchUserControl.MatchByPhoto.Checked)
            {
                MessageBoxController.ShowInfo("RAB POC", "Match By: Photo Selected and gone for Identification.");
            }

            if (matchUserControl.MatchByFP.Checked)
            {
                MessageBoxController.ShowInfo("RAB POC", "Match By: FP Selected and gone for Identification.");
            }
            if (matchUserControl.MatchByIris.Checked)
            {
                MessageBoxController.ShowInfo("RAB POC", "Match By: Iris Selected and gone for Identification.");
            }

            return;
            //TODO: Demo Code to catch Match Type from controller. Al-Amin/18-May-21
            */

            string erroMsg = string.Empty;

            EnrollmentAddResponse response = new EnrollmentAddResponse();
            PersonDataDto request = new PersonDataDto();

            request.createdBy = Users.FullName;
            request.createdOn = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            
            PersonBiometricDto bioDto = new PersonBiometricDto();
            bioDto.leftIris = matchUserControl.irisData.LeftIris;
            bioDto.rightIris = matchUserControl.irisData.RightIris;
            bioDto.photo = matchUserControl.camData.CamImage;
            //bioDto.photo = File.ReadAllBytes(@"D:\Test\murad_photo.jpg");
            bioDto.wsqRt = matchUserControl.fingerprintData.WsqRt;
            bioDto.wsqRi = matchUserControl.fingerprintData.WsqRi;
            bioDto.wsqRm = matchUserControl.fingerprintData.WsqRm;
            bioDto.wsqRr = matchUserControl.fingerprintData.WsqRr;
            bioDto.wsqRl = matchUserControl.fingerprintData.WsqRl;

            bioDto.wsqLt = matchUserControl.fingerprintData.WsqLt;
            bioDto.wsqLi = matchUserControl.fingerprintData.WsqLi;
            bioDto.wsqLm = matchUserControl.fingerprintData.WsqLm;
            bioDto.wsqLr = matchUserControl.fingerprintData.WsqLr;
            bioDto.wsqLl = matchUserControl.fingerprintData.WsqLl;
            

            request.personBiometric = bioDto;

            //string transType = enrollMatchUserControl.TransactionType;
            //request.transactionType = transType;

            JavaScriptSerializer jSerial = new JavaScriptSerializer();
            jSerial.MaxJsonLength = 999999999;

            var json = jSerial.Serialize(request);

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = NetworkService.SubmitBioRequest<EnrollmentAddResponse>
                    (request, EnrollmentAddEndpoint + "?token=abc", "POST", null);
                }
                catch (WebException ex)
                {
                    erroMsg = "Seems that you are offline. Please check your internet connection or contact with your System Administrator.";
                    logger.ErrorException("Web Exception occurred.", ex);
                }
                catch (TimeoutException ex)
                {
                    erroMsg = "Seems that you are offline. Please check your internet connection or contact with your System Administrator.";
                    logger.ErrorException("Timeout exception occurred.", ex);
                }
                catch (EndpointNotFoundException ex)
                {
                    erroMsg = "Seems that you are offline. Please check your internet connection or contact with your System Administrator.";
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
                    //MessageBoxController.ShowInfo("RAB CDMS", response.responseMessage);
                    var personMatchDialogForm = new PersonMatchDialogForm();

                    personMatchDialogForm.ReferenceNumber = response.responseMessage;
                    personMatchDialogForm.MasterPhoto = matchUserControl.camData.CamImage;
                    personMatchDialogForm.request.referenceNo = response.responseMessage;
                    //personMatchDialogForm.request.referenceNo = "550df1d2-d811-4ba2-ba37-f6ed4b76d6df";                    

                    DialogResult dr = personMatchDialogForm.ShowDialog();
                }
            }
        }

        private void OnCancel(Command cmd)
        {
            if (MessageBox.Show("Are you sure you want to cancel this Match?", "ISTL Warning", MessageBoxButtons.YesNo)
            == DialogResult.Yes)
            {
                //Clear static data
                ((MainController)parent).PersonId = 0;
                SelectedEntity.Person = null;

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

            //cmdMgr.Commands[Globals.Commands.SAVE].RemoveExecuteHandler(OnSave);
            cmdMgr.Commands[Globals.Commands.TAKE].RemoveExecuteHandler(OnSave);
            cmdMgr.Commands[Globals.Commands.CANCEL].RemoveExecuteHandler(OnCancel);

            // Hide buttons
            //cmdMgr.Commands[Globals.Commands.SAVE].Checked = false;
            cmdMgr.Commands[Globals.Commands.TAKE].Checked = false;
            cmdMgr.Commands[Globals.Commands.CANCEL].Checked = false;
        }

        public void OnFocus()
        {
            matchUserControl.Focus();
        }
        #endregion
    }
}
