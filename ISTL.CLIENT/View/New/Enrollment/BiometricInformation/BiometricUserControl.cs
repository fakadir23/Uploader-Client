using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ISTL.COMMON;
using ISTL.RAB.Controllers;
using ISTL.RAB.Controllers.New.Enrollment;
using ISTL.RAB.View.New.Enrollment.BiometricInformation;
using ISTL.COMMON.Common;
using ISTL.RAB.Entity;
using NLog;
using System.IO;
using ISTL.MODELS.DTO.New.Enrollment;
using System.Configuration;
using System.Text.RegularExpressions;
using ISTL.RAB.DbManager;
using ISTL.PERSOGlobals;
using ISTL.MODELS.Request.New;

namespace ISTL.RAB.View.New
{
    public partial class BiometricUserControl : ViewUserControl
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private DbEnrollClientManager dbEnrollClientManager;
        public BiometricUserControl()
        {
            InitializeComponent();
            dbEnrollClientManager = new DbEnrollClientManager();
        }

        public void TabToShow()
        {
            int tabToShow = ((BiometricController)controller).CheckSearchCriteria();
            if (tabToShow == 1)
            {
                tabControl1.TabPages.Remove(tabPage4);
                tabControl1.TabPages.Remove(tabPage5);

                tabControl1.TabPages.Remove(tabPageJailFp);
                tabControl1.TabPages.Remove(tabPageJailIris);

                if (StaticData.SearchByFpPhotoIris >= 0)
                {
                    tabControl1.SelectedIndex = StaticData.SearchByFpPhotoIris;
                    StaticData.SearchByFpPhotoIris = 0;
                }
            }            
            else if (tabToShow == 2)
            {
                tabControl1.TabPages.Remove(tabPage1);
                tabControl1.TabPages.Remove(tabPage2);
                tabControl1.TabPages.Remove(tabPage3);

                tabControl1.TabPages.Remove(tabPageJailFp);
                tabControl1.TabPages.Remove(tabPageJailIris);

                btnSearch.Visible = false;
                btnSave.Visible = false;

                if (StaticData.SearchByFpPhotoIris >= 3)
                {
                    tabControl1.SelectedIndex = StaticData.SearchByFpPhotoIris - 3;
                    StaticData.SearchByFpPhotoIris = 0;
                }
            }
            else if (tabToShow == 3)
            {
                tabControl1.TabPages.Remove(tabPage1);
                tabControl1.TabPages.Remove(tabPage2);
                tabControl1.TabPages.Remove(tabPage3);
                tabControl1.TabPages.Remove(tabPage4);
                tabControl1.TabPages.Remove(tabPage5);

                btnSave.Visible = false;

                if (StaticData.SearchByFpPhotoIris >= 5)
                {
                    tabControl1.SelectedIndex = StaticData.SearchByFpPhotoIris - 5;
                    StaticData.SearchByFpPhotoIris = 0;
                }
            }
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            TabToShow();

            if (StaticData.Enrollment?.profile?.biometric == null)
            {
                StaticData.Enrollment.profile.biometric = new MODELS.DTO.New.Enrollment.Biometric.BiometricDto();
            }
            if (StaticData.Enrollment?.profile?.biometric?.photo == null)
            {
                StaticData.Enrollment.profile.biometric.photo = new MODELS.DTO.New.Enrollment.Biometric.PhotoDto();
            }
            if (StaticData.Enrollment?.profile?.biometric?.fingerprint == null)
            {
                StaticData.Enrollment.profile.biometric.fingerprint = new MODELS.DTO.New.Enrollment.Biometric.FingerprintDto();
            }
            if (StaticData.Enrollment?.profile?.biometric?.iris == null)
            {
                StaticData.Enrollment.profile.biometric.iris = new MODELS.DTO.New.Enrollment.Biometric.IrisDto();
            }

            ProcessingDialog.Run(delegate ()
            {
                Invoke((MethodInvoker)delegate
                {
                    SetPhoto();
                    SetFingerPrint();
                    SetIris();

                    if (!string.IsNullOrEmpty(StaticData.Enrollment.profile.referenceNo))
                    {
                        if (StaticData.ModifiableNormalEnrollment == false)
                        {
                            MakeFieldsReadonly();
                        }
                    }
                    else
                    {
                        StaticData.Enrollment.profile.referenceNo = Guid.NewGuid().ToString().Replace("-", "").ToUpper().Substring(0, 10);
                    }
                });
            });


        }

        private void MakeFieldsReadonly()
        {
            ProfileDto dto = StaticData.Enrollment.profile;
            if (dto.biometric != null)
            {
                if (dto.biometric.photo?.photo != null)
                {
                    pbPhoto.Enabled = false;
                    btnCaptureImage.Enabled = false;
                    btnCaptureImage.BackColor = Color.IndianRed;
                    btnResetPhoto.Enabled = false;
                    btnResetPhoto.BackColor = Color.IndianRed;
                }
                if (dto.biometric.fingerprint?.lt != null) pbLT.Enabled = false;
                if (dto.biometric.fingerprint?.li != null) pbLI.Enabled = false;
                if (dto.biometric.fingerprint?.lm != null) pbLM.Enabled = false;
                if (dto.biometric.fingerprint?.lr != null) pbLR.Enabled = false;
                if (dto.biometric.fingerprint?.ls != null) pbLS.Enabled = false;
                if (dto.biometric.fingerprint?.rt != null) pbRT.Enabled = false;
                if (dto.biometric.fingerprint?.ri != null) pbRI.Enabled = false;
                if (dto.biometric.fingerprint?.rm != null) pbRM.Enabled = false;
                if (dto.biometric.fingerprint?.rr != null) pbRR.Enabled = false;
                if (dto.biometric.fingerprint?.rs != null) pbRS.Enabled = false;

                if (!pbLT.Enabled && !pbLI.Enabled && !pbLM.Enabled && !pbLR.Enabled && !pbLS.Enabled
                    && !pbRT.Enabled && !pbRI.Enabled && !pbRM.Enabled && !pbRR.Enabled && !pbRS.Enabled)
                {
                    btnTakeFinger.Enabled = false;
                    btnTakeFinger.BackColor = Color.IndianRed;
                    btnResetFP.Enabled = false;
                    btnResetFP.BackColor = Color.IndianRed;
                }

                if (dto.biometric.iris?.left != null) pbLeftIris.Enabled = false;
                if (dto.biometric.iris?.right != null) pbRightIris.Enabled = false;

                if (!pbLeftIris.Enabled && !pbRightIris.Enabled)
                {
                    btnTakeIris.Enabled = false;
                    btnTakeIris.BackColor = Color.IndianRed;
                    btnResetIris.Enabled = false;
                    btnResetIris.BackColor = Color.IndianRed;
                }
            }
        }

        public string NID
        {
            get { return tbNID.Text; }
        }
        public string dobDay
        {
            get { return tbDOBDay.Text; }
        }
        public string dobMonth
        {
            get { return tbDOBMonth.Text; }
        }
        public string dobYear
        {
            get { return tbDOBYear.Text; }
        }

        public override void OnClosing()
        {
            base.OnClosing();

            if (!(btnBiometric.ContainsFocus || btnPreviewSubmit.ContainsFocus || btnOtherInfo.ContainsFocus || btnCriminalProfile.ContainsFocus))
            {
                StaticData.Enrollment.profile = new ProfileDto();
            }
        }

        public void UpdateBiometricDataLocal()
        {
            ProcessingDialog.Run(delegate ()
            {
                dbEnrollClientManager.AddCriminalProfile(StaticData.Enrollment, Globals.RecordState.DRAFT);
            });
        }

        public void SetPhoto()
        {
            try
            {
                //if (!string.IsNullOrWhiteSpace(StaticData.Enrollment.profile.photo))
                //{
                //    this.pbPhoto.Image = Utils.ByteToImage(Utils.Base64StringToByteArray(StaticData.Enrollment.profile.photo));
                //}
                if (StaticData.Enrollment.profile?.biometric?.photo?.photo != null)
                {
                    pbPhoto.Image = Utils.ByteToImage(StaticData.Enrollment.profile?.biometric?.photo?.photo);

                    btnCaptureImage.Text = "Re-Capture Image";
                }
            }
            catch { }
        }

        public void SetFingerPrint()
        {
            try
            {
                bool AlreadyFPpresent = false;
                if (StaticData.Enrollment.profile?.biometric?.fingerprint?.lt != null)
                {
                    pbLT.Image = AppUtils.AppUtils.WsqToImage(StaticData.Enrollment.profile?.biometric?.fingerprint?.lt);
                    AlreadyFPpresent = true;
                }
                if (StaticData.Enrollment.profile?.biometric?.fingerprint?.li != null)
                {
                    pbLI.Image = AppUtils.AppUtils.WsqToImage(StaticData.Enrollment.profile?.biometric?.fingerprint?.li);
                    AlreadyFPpresent = true;
                }
                if (StaticData.Enrollment.profile?.biometric?.fingerprint?.lm != null)
                {
                    pbLM.Image = AppUtils.AppUtils.WsqToImage(StaticData.Enrollment.profile?.biometric?.fingerprint?.lm);
                    AlreadyFPpresent = true;
                }
                if (StaticData.Enrollment.profile?.biometric?.fingerprint?.lr != null)
                {
                    pbLR.Image = AppUtils.AppUtils.WsqToImage(StaticData.Enrollment.profile?.biometric?.fingerprint?.lr);
                    AlreadyFPpresent = true;
                }
                if (StaticData.Enrollment.profile?.biometric?.fingerprint?.ls != null)
                {
                    pbLS.Image = AppUtils.AppUtils.WsqToImage(StaticData.Enrollment.profile?.biometric?.fingerprint?.ls);
                    AlreadyFPpresent = true;
                }
                if (StaticData.Enrollment.profile?.biometric?.fingerprint?.rt != null)
                {
                    pbRT.Image = AppUtils.AppUtils.WsqToImage(StaticData.Enrollment.profile?.biometric?.fingerprint?.rt);
                    AlreadyFPpresent = true;
                }
                if (StaticData.Enrollment.profile?.biometric?.fingerprint?.ri != null)
                {
                    pbRI.Image = AppUtils.AppUtils.WsqToImage(StaticData.Enrollment.profile?.biometric?.fingerprint?.ri);
                    AlreadyFPpresent = true;
                }
                if (StaticData.Enrollment.profile?.biometric?.fingerprint?.rm != null)
                {
                    pbRM.Image = AppUtils.AppUtils.WsqToImage(StaticData.Enrollment.profile?.biometric?.fingerprint?.rm);
                    AlreadyFPpresent = true;
                }
                if (StaticData.Enrollment.profile?.biometric?.fingerprint?.rr != null)
                {
                    pbRR.Image = AppUtils.AppUtils.WsqToImage(StaticData.Enrollment.profile?.biometric?.fingerprint?.rr);
                    AlreadyFPpresent = true;
                }
                if (StaticData.Enrollment.profile?.biometric?.fingerprint?.rs != null)
                {
                    pbRS.Image = AppUtils.AppUtils.WsqToImage(StaticData.Enrollment.profile?.biometric?.fingerprint?.rs);
                    AlreadyFPpresent = true;
                }
                if (AlreadyFPpresent) btnTakeFinger.Text = "Re-Capture Fingerprint";
            }
            catch { }
        }

        public void SetIris()
        {
            try
            {
                bool AlreadyIrispresent = false;
                if (StaticData.Enrollment.profile?.biometric?.iris?.left != null)
                {
                    pbLeftIris.Image = Utils.ByteToImage(StaticData.Enrollment.profile?.biometric?.iris?.left);
                    AlreadyIrispresent = true;
                }
                if (StaticData.Enrollment.profile?.biometric?.iris?.right != null)
                {
                    pbRightIris.Image = Utils.ByteToImage(StaticData.Enrollment.profile?.biometric?.iris?.right);
                    AlreadyIrispresent = true;
                }
                if (AlreadyIrispresent) btnTakeIris.Text = "Re-Capture Iris";
            }
            catch { }
        }

        public void CriminalProfileClickOperation()
        {
            btnCriminalProfile.Focus();
            btnCriminalProfile_Click(null, null);
        }

        private void btnCriminalProfile_Click(object sender, EventArgs e)
        {
            if (StaticData.MoveFromBiometricEntryPageWithoutCDMSsearch == false &&
                ((BiometricController)controller).ValidBiometricSearch == false)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "CDMS Search is Mandatory for New Entry.");
                return;
            }
            StaticData.MoveFromBiometricEntryPageWithoutCDMSsearch = true;
            ((BiometricController)controller).GoToCriminalProfile();
        }

        private void btnFamily_Click(object sender, EventArgs e)
        {
            if (StaticData.MoveFromBiometricEntryPageWithoutCDMSsearch == false &&
                ((BiometricController)controller).ValidBiometricSearch == false)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "CDMS Search is Mandatory for New Entry.");
                return;
            }
            StaticData.MoveFromBiometricEntryPageWithoutCDMSsearch = true;
            ((BiometricController)controller).Family();
        }

        private void btnBiometric_Click(object sender, EventArgs e)
        {
            ((BiometricController)controller).Biometric();
        }

        private void btnPreviewSubmit_Click(object sender, EventArgs e)
        {
            if (StaticData.MoveFromBiometricEntryPageWithoutCDMSsearch == false &&
                ((BiometricController)controller).ValidBiometricSearch == false)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "CDMS Search is Mandatory for New Entry.");
                return;
            }
            StaticData.MoveFromBiometricEntryPageWithoutCDMSsearch = true;
            ((BiometricController)controller).PreviewSubmit();
        }

        private void btnTakeFinger_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = DialogResult.OK;
                var form = new FingerprintCaptureDialogForm();
                dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    //this.pbRT.Image = this.pbRT.Image == null ? Utils.ByteToImage(form.FingerData.FpRt)  : this.pbRT.Image;
                    //this.pbRI.Image = this.pbRI.Image ==  null ? Utils.ByteToImage(form.FingerData.FpRi) : this.pbRI.Image;
                    //this.pbRM.Image = this.pbRM.Image == null ? Utils.ByteToImage(form.FingerData.FpRm)  : this.pbRM.Image;
                    //this.pbRR.Image = this.pbRR.Image == null ? Utils.ByteToImage(form.FingerData.FpRr)  : this.pbRR.Image;
                    //this.pbRS.Image = this.pbRS.Image == null ? Utils.ByteToImage(form.FingerData.FpRl)  : this.pbRS.Image;

                    //this.pbLT.Image = this.pbLT.Image == null ? Utils.ByteToImage(form.FingerData.FpLt)  : this.pbLT.Image;
                    //this.pbLI.Image = this.pbLI.Image == null ? Utils.ByteToImage(form.FingerData.FpLi)  : this.pbLI.Image;
                    //this.pbLM.Image = this.pbLM.Image == null ? Utils.ByteToImage(form.FingerData.FpLm)  : this.pbLM.Image;
                    //this.pbLR.Image = this.pbLR.Image == null ? Utils.ByteToImage(form.FingerData.FpLr)  : this.pbLR.Image;
                    //this.pbLS.Image = this.pbLS.Image == null ? Utils.ByteToImage(form.FingerData.FpLl)  : this.pbLS.Image;                                        

                    // Right FP
                    //StaticData.Enrollment.profile.biometric.fingerprint.rt = form.FingerData.WsqRt;
                    //StaticData.Enrollment.profile.biometric.fingerprint.ri = form.FingerData.WsqRi;
                    //StaticData.Enrollment.profile.biometric.fingerprint.rm = form.FingerData.WsqRm;
                    //StaticData.Enrollment.profile.biometric.fingerprint.rr = form.FingerData.WsqRr;
                    //StaticData.Enrollment.profile.biometric.fingerprint.rs = form.FingerData.WsqRl;

                    // Left FP
                    //StaticData.Enrollment.profile.biometric.fingerprint.lt = form.FingerData.WsqLt;
                    //StaticData.Enrollment.profile.biometric.fingerprint.li = form.FingerData.WsqLi;
                    //StaticData.Enrollment.profile.biometric.fingerprint.lm = form.FingerData.WsqLm;
                    //StaticData.Enrollment.profile.biometric.fingerprint.lr = form.FingerData.WsqLr;
                    //StaticData.Enrollment.profile.biometric.fingerprint.ls = form.FingerData.WsqLl;

                    //if (form.FingerData.FpRt != null)
                    //{
                        pbRT.Image = Utils.ByteToImage(form.FingerData.FpRt);
                        StaticData.Enrollment.profile.biometric.fingerprint.rt = form.FingerData.WsqRt;
                    //}
                    //if (form.FingerData.FpRi != null)
                    //{
                        pbRI.Image = Utils.ByteToImage(form.FingerData.FpRi);
                        StaticData.Enrollment.profile.biometric.fingerprint.ri = form.FingerData.WsqRi;
                    //}
                    //if (form.FingerData.FpRm != null)
                    //{
                        pbRM.Image = Utils.ByteToImage(form.FingerData.FpRm);
                        StaticData.Enrollment.profile.biometric.fingerprint.rm = form.FingerData.WsqRm;
                    //}
                    //if (form.FingerData.FpRr != null)
                    //{
                        pbRR.Image = Utils.ByteToImage(form.FingerData.FpRr);
                        StaticData.Enrollment.profile.biometric.fingerprint.rr = form.FingerData.WsqRr;
                    //}
                    //if (form.FingerData.FpRl != null)
                    //{
                        pbRS.Image = Utils.ByteToImage(form.FingerData.FpRl);
                        StaticData.Enrollment.profile.biometric.fingerprint.rs = form.FingerData.WsqRl;
                    //}
                    //if (form.FingerData.FpLt != null)
                    //{
                        pbLT.Image = Utils.ByteToImage(form.FingerData.FpLt);
                        StaticData.Enrollment.profile.biometric.fingerprint.lt = form.FingerData.WsqLt;
                    //}
                    //if (form.FingerData.FpLi != null)
                    //{
                        pbLI.Image = Utils.ByteToImage(form.FingerData.FpLi);
                        StaticData.Enrollment.profile.biometric.fingerprint.li = form.FingerData.WsqLi;
                    //}
                    //if (form.FingerData.FpLm != null)
                    //{
                        pbLM.Image = Utils.ByteToImage(form.FingerData.FpLm);
                        StaticData.Enrollment.profile.biometric.fingerprint.lm = form.FingerData.WsqLm;
                    //}
                    //if (form.FingerData.FpLr != null)
                    //{
                        pbLR.Image = Utils.ByteToImage(form.FingerData.FpLr);
                        StaticData.Enrollment.profile.biometric.fingerprint.lr = form.FingerData.WsqLr;
                    //}
                    //if (form.FingerData.FpLl != null)
                    //{
                        pbLS.Image = Utils.ByteToImage(form.FingerData.FpLl);
                        StaticData.Enrollment.profile.biometric.fingerprint.ls = form.FingerData.WsqLl;
                    //}

                    if (StaticData.Enrollment.profile?.biometric?.fingerprint?.rt == null && StaticData.Enrollment.profile?.biometric?.fingerprint?.ri == null &&
                StaticData.Enrollment.profile?.biometric?.fingerprint?.rm == null && StaticData.Enrollment.profile?.biometric?.fingerprint?.rr == null &&
                StaticData.Enrollment.profile?.biometric?.fingerprint?.rs == null && StaticData.Enrollment.profile?.biometric?.fingerprint?.lt == null &&
                StaticData.Enrollment.profile?.biometric?.fingerprint?.li == null && StaticData.Enrollment.profile?.biometric?.fingerprint?.lm == null &&
                StaticData.Enrollment.profile?.biometric?.fingerprint?.lr == null && StaticData.Enrollment.profile?.biometric?.fingerprint?.ls == null)
                    {
                        btnTakeFinger.Text = "Capture Fingerprint";
                    }
                    else
                    {
                        btnTakeFinger.Text = "Re-Capture Fingerprint";
                    }

                    UpdateBiometricDataLocal();
                }
            }
            catch (Exception)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an unexpected error while capturing fingerprint. Please contact with your Administrator.");
            }
        }

        private void btnPhotoSave_Click(object sender, EventArgs e)
        {
            if (pbPhoto.Image == null)
            {
                StaticData.Enrollment.profile.photo = null;
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please capture a Photo.");
                return;
            }

            var photoByte = GraphicsManager.GetInstance().ImageToByteArray(pbPhoto.Image);
            //var photoStr = Utils.ByteArrayToBase64String(photoByte);
            StaticData.Enrollment.profile.biometric.photo.photo = photoByte;
            InfoMessageBox.ShowMessage("SNSOP TOOLS", "Photo saved successfully.");
        }

        private void pbPhoto_Click(object sender, EventArgs e)
        {
            TakePhoto();
        }

        public void TakePhoto()
        {
            Image img = GetImage(pbPhoto);
            if (img == null) return;

            btnCaptureImage.Text = "Re-Capture Photo";

            int maxPhotoSize = Convert.ToInt32(ConfigurationManager.AppSettings["PhotoSizeInKB"].ToString());
            if (GraphicsManager.GetInstance().ImageToByteArray(img).Length > maxPhotoSize)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Photo size is too big. Size should not exceed " + (maxPhotoSize / (1024 * 1000)) + " MB");
                return;
            }
            pbPhoto.Image = img;

            //var resizedImg = GraphicsManager.GetInstance().ResizeImage(this.pbPhoto.Image,
            //        new Size() { Width = 200, Height = 350 }, true);

            //StaticData.Enrollment.profile.biometric.photo.photo = Utils.ImageToByte(resizedImg);

            StaticData.Enrollment.profile.biometric.photo.photo = Utils.ImageToByte(img);

            //UpdateBiometricDataLocal();

            dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "criminal_photo",
                StaticData.Enrollment?.profile?.biometric?.photo?.photo);
        }

        private Image GetImage(PictureBox pb)
        {
            if (pb == pbPhoto)
            {
                openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.bmp;)|" +
                    "*.jpg; *.jpeg; *.png; *.bmp;";
            }
            else if (pb == pbLeftIris || pb == pbRightIris)
            {
                openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.bmp;)|" +
                    "*.jpg; *.jpeg; *.png; *.bmp;";
            }
            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                try
                {
                    return Image.FromFile(openFileDialog.FileName);
                }
                catch(Exception x)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Error occured while selecting this file. Please select another");
                    logger.Error("Error occured while selecting file.\n" + x.ToString());
                }
            }
            return null;
        }

        private void btnCaptureImage_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = DialogResult.OK;
                var form = new ImageCaptureDialogForm();
                dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    this.pbPhoto.Image = Utils.ByteToImage(form.CamData.CamImage);


                    //var resizedImg = GraphicsManager.GetInstance().ResizeImage(this.pbPhoto.Image,
                    //new Size() { Width = 200, Height = 350 }, true);


                    StaticData.Enrollment.profile.biometric.photo.photo = form.CamData.CamImage;

                    //StaticData.Enrollment.profile.biometric.photo.photo = Utils.ImageToByte(resizedImg);

                    if (StaticData.Enrollment.profile.biometric.photo.photo != null)
                    {
                        btnCaptureImage.Text = "Re-Capture Photo";
                    }
                    else
                    {
                        btnCaptureImage.Text = "Capture Photo";
                    }

                    dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "criminal_photo",
                StaticData.Enrollment?.profile?.biometric?.photo?.photo);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Photo capture error." + ex.ToString());
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an unexpected error while capturing photo. Please contact with your Administrator.");
            }
        }

        private void pbRT_Click(object sender, EventArgs e)
        {
            TakeFP(pbRT);
        }

        public void TakeFP(PictureBox pictureBox)
        {
            Image img = null;
            byte[] wsqBuffer = null;

            openFileDialog.Filter = "Image Files(*.wsq)|" + "*.wsq";

            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    wsqBuffer = File.ReadAllBytes(openFileDialog.FileName);
                    img = AppUtils.AppUtils.WsqToImage(wsqBuffer);
                }
                catch 
                {
                    return;
                }
            }

            if (img == null || wsqBuffer == null) return;

            btnTakeFinger.Text = "Re-Capture Fingerprint";

            int maxFPSize = Convert.ToInt32(ConfigurationManager.AppSettings["FPSizeInKB"].ToString());
            if (GraphicsManager.GetInstance().ImageToByteArray(img).Length > maxFPSize)
            {
                //CustomMessageBox.ShowMessage("SNSOP TOOLS", "Fingerprint photo size is too big. Size should not exceed " + (maxFPSize / 1024) + " KB");
                //return;
                //DialogResult dr = MessageBoxController.ShowQuestionYesNo("RAB CDMS", "The size of the selected WSQ exceeds 30KB. Do you wish to continue?");
                //if (dr == DialogResult.No)
                //{
                //    return;
                //}
            }

            pictureBox.Image = img;

            // Right FP
            if (pictureBox.Name == "pbRT")
            {
                StaticData.Enrollment.profile.biometric.fingerprint.rt = wsqBuffer;
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "fp_rt",
                StaticData.Enrollment?.profile?.biometric?.fingerprint?.rt);
            }
            if (pictureBox.Name == "pbRI")
            {
                StaticData.Enrollment.profile.biometric.fingerprint.ri = wsqBuffer;
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "fp_ri",
                StaticData.Enrollment?.profile?.biometric?.fingerprint?.ri);
            }
            if (pictureBox.Name == "pbRM")
            {
                StaticData.Enrollment.profile.biometric.fingerprint.rm = wsqBuffer;
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "fp_rm",
                StaticData.Enrollment?.profile?.biometric?.fingerprint?.rm);
            }
            if (pictureBox.Name == "pbRR")
            {
                StaticData.Enrollment.profile.biometric.fingerprint.rr = wsqBuffer;
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "fp_rr",
                StaticData.Enrollment?.profile?.biometric?.fingerprint?.rr);
            }
            if (pictureBox.Name == "pbRS")
            {
                StaticData.Enrollment.profile.biometric.fingerprint.rs = wsqBuffer;
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "fp_rs",
                StaticData.Enrollment?.profile?.biometric?.fingerprint?.rs);
            }

            // Left FP
            if (pictureBox.Name == "pbLT")
            {
                StaticData.Enrollment.profile.biometric.fingerprint.lt = wsqBuffer;
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "fp_lt",
                StaticData.Enrollment?.profile?.biometric?.fingerprint?.lt);
            }
            if (pictureBox.Name == "pbLI")
            {
                StaticData.Enrollment.profile.biometric.fingerprint.li = wsqBuffer;
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "fp_li",
                StaticData.Enrollment?.profile?.biometric?.fingerprint?.li);
            }
            if (pictureBox.Name == "pbLM")
            {
                StaticData.Enrollment.profile.biometric.fingerprint.lm = wsqBuffer;
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "fp_lm",
                StaticData.Enrollment?.profile?.biometric?.fingerprint?.lm);
            }
            if (pictureBox.Name == "pbLR")
            {
                StaticData.Enrollment.profile.biometric.fingerprint.lr = wsqBuffer;
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "fp_lr",
                StaticData.Enrollment?.profile?.biometric?.fingerprint?.lr);
            }
            if (pictureBox.Name == "pbLS")
            {
                StaticData.Enrollment.profile.biometric.fingerprint.ls = wsqBuffer;
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "fp_ls",
                StaticData.Enrollment?.profile?.biometric?.fingerprint?.ls);
            }
            //UpdateBiometricDataLocal();
        }

        private void pbRighttIris_Click(object sender, EventArgs e)
        {
            TakeIris(pbRightIris);
        }

        private void pbLeftIris_Click(object sender, EventArgs e)
        {
            TakeIris(pbLeftIris);
        }

        public void TakeIris(PictureBox pictureBox)
        {
            Image img = GetImage(pictureBox);
            if (img == null) return;

            btnTakeIris.Text = "Re-Capture Iris";

            int maxIrisSize = Convert.ToInt32(ConfigurationManager.AppSettings["IrisSizeInKB"].ToString());
            if (GraphicsManager.GetInstance().ImageToByteArray(img).Length > maxIrisSize)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Iris photo size is too big. Size should not exceed " + (maxIrisSize / (1024 * 1000)) + " MB");
                return;
            }

            pictureBox.Image = img;


            if (pictureBox.Name == "pbRightIris")
            {
                //var resizedRightIrisImg = GraphicsManager.GetInstance().ResizeImage(img,
                //   new Size() { Width = 600, Height = 400 }, true);

                //StaticData.Enrollment.profile.biometric.iris.right = Utils.ImageToByte(resizedRightIrisImg);
                StaticData.Enrollment.profile.biometric.iris.right = Utils.ImageToByte(img);
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "right_iris",
                StaticData.Enrollment?.profile?.biometric?.iris?.right);
            }
            if (pictureBox.Name == "pbLeftIris")
            {
                //var resizedLeftIrisImg = GraphicsManager.GetInstance().ResizeImage(img,
                //   new Size() { Width = 600, Height = 400 }, true);

                //StaticData.Enrollment.profile.biometric.iris.left = Utils.ImageToByte(resizedLeftIrisImg);

                StaticData.Enrollment.profile.biometric.iris.left = Utils.ImageToByte(img);

                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "left_iris",
                StaticData.Enrollment?.profile?.biometric?.iris?.left);
            }
            //UpdateBiometricDataLocal();
        }

        private void pbLT_Click(object sender, EventArgs e)
        {
            TakeFP(pbLT);
        }

        private void btnTakeIris_Click(object sender, EventArgs e)
        {
            DialogResult dr = DialogResult.OK;
            try
            {
                var form = new IrisCaptureDialogForm();
                dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    this.pbLeftIris.Image = Utils.ByteToImage(form.irisData.LeftIris);
                    this.pbRightIris.Image = Utils.ByteToImage(form.irisData.RightIris);


                   // var resizedRightIrisImg = GraphicsManager.GetInstance().ResizeImage(this.pbLeftIris.Image,
                   //new Size() { Width = 200, Height = 350 }, true);

                   // var resizedLeftIrisImg = GraphicsManager.GetInstance().ResizeImage(this.pbLeftIris.Image,
                   //new Size() { Width = 200, Height = 350 }, true);

                    StaticData.Enrollment.profile.biometric.iris.right = form.irisData.RightIris;
                    StaticData.Enrollment.profile.biometric.iris.left = form.irisData.LeftIris;

                    //StaticData.Enrollment.profile.biometric.iris.right = Utils.ImageToByte(resizedRightIrisImg);
                    //StaticData.Enrollment.profile.biometric.iris.left = Utils.ImageToByte(resizedLeftIrisImg);

                    if (StaticData.Enrollment.profile.biometric.iris.right != null ||
                        StaticData.Enrollment.profile.biometric.iris.left != null)
                    {
                        btnTakeIris.Text = "Re-Capture Iris";
                    }

                        dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "right_iris",
                 StaticData.Enrollment?.profile?.biometric?.iris?.right);
                    dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "left_iris",
                StaticData.Enrollment?.profile?.biometric?.iris?.left);
                }
            }
            catch (Exception)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an unexpected error while capturing iris. Please contact with your Administrator.");
            }

        }

        private void pbRI_Click(object sender, EventArgs e)
        {
            TakeFP(pbRI);
        }

        private void pbRM_Click(object sender, EventArgs e)
        {
            TakeFP(pbRM);
        }

        private void pbRR_Click(object sender, EventArgs e)
        {
            TakeFP(pbRR);
        }

        private void pbRS_Click(object sender, EventArgs e)
        {
            TakeFP(pbRS);
        }

        private void pbLI_Click(object sender, EventArgs e)
        {
            TakeFP(pbLI);
        }

        private void pbLM_Click(object sender, EventArgs e)
        {
            TakeFP(pbLM);
        }

        private void pbLR_Click(object sender, EventArgs e)
        {
            TakeFP(pbLR);
        }

        private void pbLS_Click(object sender, EventArgs e)
        {
            TakeFP(pbLS);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            OnBiometricSearch();
        }

        private void OnBiometricSearch()
        {
            //((BiometricController)controller).SearchByBiometric();
        }

        private void tbNID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == (Char)Keys.Back) e.Handled = false;
            else e.Handled = true;
        }

        private void tbDOBYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == (Char)Keys.Back) e.Handled = false;
            else e.Handled = true;
        }

        private void tbDOBMonth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == (Char)Keys.Back) e.Handled = false;
            else e.Handled = true;
        }

        private void tbDOBDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == (Char)Keys.Back) e.Handled = false;
            else e.Handled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                tabControl1.SelectedIndex = 1;
                return;
            }
            if (tabControl1.SelectedIndex == 1)
            {
                tabControl1.SelectedIndex = 2;
                return;
            }
            if (tabControl1.SelectedIndex == 2)
            {
                btnCriminalProfile.Focus();
                btnCriminalProfile_Click(null, null);
            }
        }

        private void identifyNID_Click(object sender, EventArgs e)
        {
            string nidReq = null;
            string dobReq = null;
            if (!string.IsNullOrEmpty(dobDay) && !string.IsNullOrEmpty(dobMonth) && !string.IsNullOrEmpty(dobYear))
            {
                try
                {
                    string dt = dobDay + "/" + dobMonth + "/" + dobYear;
                    DateTime dtForm = DateTime.ParseExact(dt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    dobReq = dtForm.ToString("yyyy-MM-dd");
                }
                catch (Exception)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please input Date of birth correctly");
                    return;
                }
            }
            else
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please input Date of birth");
                return;
            }

            if (!string.IsNullOrEmpty(NID) && NID?.Length == 13 && !string.IsNullOrEmpty(dobYear) && dobYear?.Length == 4)
            {
                tbNID.Text = tbDOBYear.Text + tbNID.Text;
            }

            if (!string.IsNullOrEmpty(NID) && (NID?.Length == 10 || NID?.Length == 17))
            {
                nidReq = NID;
            }
            else
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please input NID correctly");
                return;
            }

            if (!Utils.isDigit(nidReq))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "NID can contain only digits");
                return;
            }
            if (!Utils.isDigit(dobDay))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Day can contain only digits");
                return;
            }
            if (!Utils.isDigit(dobMonth))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Month can contain only digits");
                return;
            }
            if (!Utils.isDigit(dobYear))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Year can contain only digits");
                return;
            }

            if (!string.IsNullOrEmpty(nidReq) && !string.IsNullOrEmpty(dobReq))
            {
                ((BiometricController)controller).SearchByNID(nidReq, dobReq);
            }
            else
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please input NID and Date Of Birth");
            }

        }

        private void btnResetPhoto_Click(object sender, EventArgs e)
        {
            pbPhoto.Image = null;

            if (StaticData.Enrollment.profile.biometric == null)
            {
                StaticData.Enrollment.profile.biometric = new MODELS.DTO.New.Enrollment.Biometric.BiometricDto();
            }

            StaticData.Enrollment.profile.biometric.photo = new MODELS.DTO.New.Enrollment.Biometric.PhotoDto();
        }

        private void btnResetFP_Click(object sender, EventArgs e)
        {
            pbRT.Image = null;
            pbRI.Image = null;
            pbRM.Image = null;
            pbRR.Image = null;
            pbRS.Image = null;
            pbLT.Image = null;
            pbLI.Image = null;
            pbLM.Image = null;
            pbLR.Image = null;
            pbLS.Image = null;

            if (StaticData.Enrollment.profile.biometric == null)
            {
                StaticData.Enrollment.profile.biometric = new MODELS.DTO.New.Enrollment.Biometric.BiometricDto();
            }

            StaticData.Enrollment.profile.biometric.fingerprint = new MODELS.DTO.New.Enrollment.Biometric.FingerprintDto();
        }

        private void btnResetIris_Click(object sender, EventArgs e)
        {
            pbLeftIris.Image = null;
            pbRightIris.Image = null;

            if (StaticData.Enrollment.profile.biometric == null)
            {
                StaticData.Enrollment.profile.biometric = new MODELS.DTO.New.Enrollment.Biometric.BiometricDto();
            }

            StaticData.Enrollment.profile.biometric.iris = new MODELS.DTO.New.Enrollment.Biometric.IrisDto();
        }

        private void btnNIDIdentifyFP_Click(object sender, EventArgs e)
        {
            pbNidRT.Image = null;
            pbNidRI.Image = null;
            pbNidRM.Image = null;
            pbNidRR.Image = null;
            pbNidRL.Image = null;
            pbNidLT.Image = null;
            pbNidLI.Image = null;
            pbNidLM.Image = null;
            pbNidLR.Image = null;
            pbNidLL.Image = null;
            getBECidentifyrequest = new GetBECidentifyRequest();
            StaticData.Enrollment.profile.biometric.fingerprint = new MODELS.DTO.New.Enrollment.Biometric.FingerprintDto();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (tabControl1.SelectedIndex == 3 || tabControl1.SelectedIndex == 4)
            //{
            //    btnSearch.Hide();
            //}
            //else
            //{
            //    btnSearch.Show();
            //}
        }

        public GetBECidentifyRequest getBECidentifyrequest = new GetBECidentifyRequest();

        private void btnFPidentifyBEC_Click(object sender, EventArgs e)
        {

            //((BiometricController)controller).IdentifyNIDByBiometric();
            ((BiometricController)controller).OnIdentifyNIDByBiometric();
        }

        private void pbNidLT_Click(object sender, EventArgs e)
        {
            TakeFPForNIDidentify(pbNidLT);
        }

        private void pbNidLI_Click(object sender, EventArgs e)
        {
            TakeFPForNIDidentify(pbNidLI);
        }
        private void pbNidLM_Click(object sender, EventArgs e)
        {
            TakeFPForNIDidentify(pbNidLM);
        }

        private void pbNidLR_Click(object sender, EventArgs e)
        {
            TakeFPForNIDidentify(pbNidLR);
        }

        private void pbNidLL_Click(object sender, EventArgs e)
        {
            TakeFPForNIDidentify(pbNidLL);
        }

        private void pbNidRT_Click(object sender, EventArgs e)
        {
            TakeFPForNIDidentify(pbNidRT);
        }

        private void pbNidRI_Click(object sender, EventArgs e)
        {
            TakeFPForNIDidentify(pbNidRI);
        }

        private void pbNidRM_Click(object sender, EventArgs e)
        {
            TakeFPForNIDidentify(pbNidRM);
        }

        private void pbNidRR_Click(object sender, EventArgs e)
        {
            TakeFPForNIDidentify(pbNidRR);
        }

        private void pbNidRL_Click(object sender, EventArgs e)
        {
            TakeFPForNIDidentify(pbNidRL);
        }

        public void TakeFPForNIDidentify(PictureBox pictureBox)
        {
            Image img = null;
            byte[] wsqBuffer = null;

            //openFileDialog.Filter = "Image Files(*.wsq)|" + "*.wsq";
            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.bmp;)|" +
                    "*.jpg; *.jpeg; *.png; *.bmp;";

            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    wsqBuffer = File.ReadAllBytes(openFileDialog.FileName);
                    //img = AppUtils.AppUtils.WsqToImage(wsqBuffer);
                    img = Utils.ByteToImage(wsqBuffer);
                }
                catch 
                {
                    return;
                }
            }

            if (img == null || wsqBuffer == null) return;

            btnCaptFpNidIdentify.Text = "Re-Capture Fingerprint";

            int maxFPSize = Convert.ToInt32(ConfigurationManager.AppSettings["FPSizeInKB"].ToString());
            if (GraphicsManager.GetInstance().ImageToByteArray(img).Length > maxFPSize)
            {
                //CustomMessageBox.ShowMessage("SNSOP TOOLS", "Fingerprint photo size is too big. Size should not exceed " + (maxFPSize / 1024) + " KB");
                //return;
                //DialogResult dr = MessageBoxController.ShowQuestionYesNo("RAB CDMS", "The size of the selected WSQ exceeds 30KB. Do you wish to continue?");
                //if (dr == DialogResult.No)
                //{
                //    return;
                //}
            }

            pictureBox.Image = img;

            // Right FP
            if (pictureBox.Name == "pbNidRT")
            {
                StaticData.Enrollment.profile.biometric.fingerprint.rt = wsqBuffer;
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "fp_rt",
                StaticData.Enrollment?.profile?.biometric?.fingerprint?.rt);
                try
                {
                    getBECidentifyrequest.rt = Utils.ImageToByte(AppUtils.AppUtils.WsqToImage(wsqBuffer));
                }
                catch { }
            }
            if (pictureBox.Name == "pbNidRI")
            {
                StaticData.Enrollment.profile.biometric.fingerprint.ri = wsqBuffer;
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "fp_ri",
                StaticData.Enrollment?.profile?.biometric?.fingerprint?.ri);
                try
                {
                    getBECidentifyrequest.ri = Utils.ImageToByte(AppUtils.AppUtils.WsqToImage(wsqBuffer));
                }
                catch { }
            }
            if (pictureBox.Name == "pbNidRM")
            {
                StaticData.Enrollment.profile.biometric.fingerprint.rm = wsqBuffer;
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "fp_rm",
                StaticData.Enrollment?.profile?.biometric?.fingerprint?.rm);
            }
            if (pictureBox.Name == "pbNidRR")
            {
                StaticData.Enrollment.profile.biometric.fingerprint.rr = wsqBuffer;
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "fp_rr",
                StaticData.Enrollment?.profile?.biometric?.fingerprint?.rr);
            }
            if (pictureBox.Name == "pbNidRL")
            {
                StaticData.Enrollment.profile.biometric.fingerprint.rs = wsqBuffer;
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "fp_rs",
                StaticData.Enrollment?.profile?.biometric?.fingerprint?.rs);
            }

            // Left FP
            if (pictureBox.Name == "pbNidLT")
            {
                StaticData.Enrollment.profile.biometric.fingerprint.lt = wsqBuffer;
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "fp_lt",
                StaticData.Enrollment?.profile?.biometric?.fingerprint?.lt);
                try
                {
                    getBECidentifyrequest.lt = Utils.ImageToByte(AppUtils.AppUtils.WsqToImage(wsqBuffer));
                }
                catch { }
            }
            if (pictureBox.Name == "pbNidLI")
            {
                StaticData.Enrollment.profile.biometric.fingerprint.li = wsqBuffer;
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "fp_li",
                StaticData.Enrollment?.profile?.biometric?.fingerprint?.li);
                try
                {
                    getBECidentifyrequest.li = Utils.ImageToByte(AppUtils.AppUtils.WsqToImage(wsqBuffer));
                }
                catch { }
            }
            if (pictureBox.Name == "pbNidLM")
            {
                StaticData.Enrollment.profile.biometric.fingerprint.lm = wsqBuffer;
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "fp_lm",
                StaticData.Enrollment?.profile?.biometric?.fingerprint?.lm);
            }
            if (pictureBox.Name == "pbNidLR")
            {
                StaticData.Enrollment.profile.biometric.fingerprint.lr = wsqBuffer;
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "fp_lr",
                StaticData.Enrollment?.profile?.biometric?.fingerprint?.lr);
            }
            if (pictureBox.Name == "pbNidLL")
            {
                StaticData.Enrollment.profile.biometric.fingerprint.ls = wsqBuffer;
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "fp_ls",
                StaticData.Enrollment?.profile?.biometric?.fingerprint?.ls);
            }
        }

        private void btnCaptFpNidIdentify_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = DialogResult.OK;
                var form = new FingerprintCaptureDialogForm();
                dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    //this.pbRT.Image = Utils.ByteToImage(form.FingerData.FpRt);
                    //this.pbRI.Image = Utils.ByteToImage(form.FingerData.FpRi);
                    //this.pbRM.Image = Utils.ByteToImage(form.FingerData.FpRm);
                    //this.pbRR.Image = Utils.ByteToImage(form.FingerData.FpRr);
                    //this.pbRS.Image = Utils.ByteToImage(form.FingerData.FpRl);

                    //this.pbLT.Image = Utils.ByteToImage(form.FingerData.FpLt);
                    //this.pbLI.Image = Utils.ByteToImage(form.FingerData.FpLi);
                    //this.pbLM.Image = Utils.ByteToImage(form.FingerData.FpLm);
                    //this.pbLR.Image = Utils.ByteToImage(form.FingerData.FpLr);
                    //this.pbLS.Image = Utils.ByteToImage(form.FingerData.FpLl);

                    this.pbNidRT.Image = Utils.ByteToImage(form.FingerData.FpRt);
                    this.pbNidRI.Image = Utils.ByteToImage(form.FingerData.FpRi);
                    this.pbNidRM.Image = Utils.ByteToImage(form.FingerData.FpRm);
                    this.pbNidRR.Image = Utils.ByteToImage(form.FingerData.FpRr);
                    this.pbNidRL.Image = Utils.ByteToImage(form.FingerData.FpRl);

                    getBECidentifyrequest.li = form.FingerData.FpLi;
                    getBECidentifyrequest.lt = form.FingerData.FpLt;

                    this.pbNidLT.Image = Utils.ByteToImage(form.FingerData.FpLt);
                    this.pbNidLI.Image = Utils.ByteToImage(form.FingerData.FpLi);
                    this.pbNidLM.Image = Utils.ByteToImage(form.FingerData.FpLm);
                    this.pbNidLR.Image = Utils.ByteToImage(form.FingerData.FpLr);
                    this.pbNidLL.Image = Utils.ByteToImage(form.FingerData.FpLl);

                    getBECidentifyrequest.rt = form.FingerData.FpRt;
                    getBECidentifyrequest.ri = form.FingerData.FpRi;

                    // Right FP
                    StaticData.Enrollment.profile.biometric.fingerprint.rt = form.FingerData.WsqRt;
                    StaticData.Enrollment.profile.biometric.fingerprint.ri = form.FingerData.WsqRi;
                    StaticData.Enrollment.profile.biometric.fingerprint.rm = form.FingerData.WsqRm;
                    StaticData.Enrollment.profile.biometric.fingerprint.rr = form.FingerData.WsqRr;
                    StaticData.Enrollment.profile.biometric.fingerprint.rs = form.FingerData.WsqRl;

                    // Left FP
                    StaticData.Enrollment.profile.biometric.fingerprint.lt = form.FingerData.WsqLt;
                    StaticData.Enrollment.profile.biometric.fingerprint.li = form.FingerData.WsqLi;
                    StaticData.Enrollment.profile.biometric.fingerprint.lm = form.FingerData.WsqLm;
                    StaticData.Enrollment.profile.biometric.fingerprint.lr = form.FingerData.WsqLr;
                    StaticData.Enrollment.profile.biometric.fingerprint.ls = form.FingerData.WsqLl;

                    if (StaticData.Enrollment.profile?.biometric?.fingerprint?.rt == null && StaticData.Enrollment.profile?.biometric?.fingerprint?.ri == null &&
                StaticData.Enrollment.profile?.biometric?.fingerprint?.rm == null && StaticData.Enrollment.profile?.biometric?.fingerprint?.rr == null &&
                StaticData.Enrollment.profile?.biometric?.fingerprint?.rs == null && StaticData.Enrollment.profile?.biometric?.fingerprint?.lt == null &&
                StaticData.Enrollment.profile?.biometric?.fingerprint?.li == null && StaticData.Enrollment.profile?.biometric?.fingerprint?.lm == null &&
                StaticData.Enrollment.profile?.biometric?.fingerprint?.lr == null && StaticData.Enrollment.profile?.biometric?.fingerprint?.ls == null)
                    {
                        btnCaptFpNidIdentify.Text = "Capture Fingerprint";
                    }
                    else
                    {
                        btnCaptFpNidIdentify.Text = "Re-Capture Fingerprint";
                    }

                    UpdateBiometricDataLocal();
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an unexpected error while capturing fingerprint. Please contact with your Administrator.");
            }
        }

        private void tbNID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) tbDOBYear.Focus();
        }

        private void tbDOBYear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) tbDOBMonth.Focus();
        }

        private void tbDOBMonth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) tbDOBDay.Focus();
        }

        private void btnResetNidVerify_Click(object sender, EventArgs e)
        {
            tbNID.Text = null;
            tbDOBDay.Text = null;
            tbDOBMonth.Text = null;
            tbDOBYear.Text = null;
        }

        private void btnSearchFP_Click(object sender, EventArgs e)
        {
            //btnSearch_Click(null, null);
            if (StaticData.Enrollment.profile?.biometric?.fingerprint?.rt == null && StaticData.Enrollment.profile?.biometric?.fingerprint?.ri == null &&
                StaticData.Enrollment.profile?.biometric?.fingerprint?.rm == null && StaticData.Enrollment.profile?.biometric?.fingerprint?.rr == null &&
                StaticData.Enrollment.profile?.biometric?.fingerprint?.rs == null && StaticData.Enrollment.profile?.biometric?.fingerprint?.lt == null &&
                StaticData.Enrollment.profile?.biometric?.fingerprint?.li == null && StaticData.Enrollment.profile?.biometric?.fingerprint?.lm == null &&
                StaticData.Enrollment.profile?.biometric?.fingerprint?.lr == null && StaticData.Enrollment.profile?.biometric?.fingerprint?.ls == null)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Fingerprint is needed for search");
                return;
            }
            ((BiometricController)controller).SearchByBiometric(1);
        }

        private void btnSearchFaceImage_Click(object sender, EventArgs e)
        {
            //btnSearch_Click(null, null);
            if (StaticData.Enrollment.profile?.biometric?.photo?.photo == null)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Photo is needed for search");
                return;
            }
            ((BiometricController)controller).SearchByBiometric(2);
        }

        private void btnSearchIris_Click(object sender, EventArgs e)
        {
            //btnSearch_Click(null, null);
            if (StaticData.Enrollment.profile?.biometric?.iris?.left == null && StaticData.Enrollment.profile?.biometric?.iris?.right == null)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Iris is needed for search");
                return;
            }
            ((BiometricController)controller).SearchByBiometric(3);
        }

        private void btnCaptFpJail_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = DialogResult.OK;
                var form = new FingerprintCaptureDialogForm();
                dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {                    
                    pbJailRT.Image = Utils.ByteToImage(form.FingerData.FpRt);
                    StaticData.Enrollment.profile.biometric.fingerprint.rt = form.FingerData.WsqRt;
                    
                    pbJailRI.Image = Utils.ByteToImage(form.FingerData.FpRi);
                    StaticData.Enrollment.profile.biometric.fingerprint.ri = form.FingerData.WsqRi;
                    
                    pbJailRM.Image = Utils.ByteToImage(form.FingerData.FpRm);
                    StaticData.Enrollment.profile.biometric.fingerprint.rm = form.FingerData.WsqRm;
                    
                    pbJailRR.Image = Utils.ByteToImage(form.FingerData.FpRr);
                    StaticData.Enrollment.profile.biometric.fingerprint.rr = form.FingerData.WsqRr;
                    
                    pbJailRL.Image = Utils.ByteToImage(form.FingerData.FpRl);
                    StaticData.Enrollment.profile.biometric.fingerprint.rs = form.FingerData.WsqRl;
                    
                    pbJailLL.Image = Utils.ByteToImage(form.FingerData.FpLt);
                    StaticData.Enrollment.profile.biometric.fingerprint.lt = form.FingerData.WsqLt;
                    
                    pbJailLI.Image = Utils.ByteToImage(form.FingerData.FpLi);
                    StaticData.Enrollment.profile.biometric.fingerprint.li = form.FingerData.WsqLi;
                    
                    pbJailLM.Image = Utils.ByteToImage(form.FingerData.FpLm);
                    StaticData.Enrollment.profile.biometric.fingerprint.lm = form.FingerData.WsqLm;
                    
                    pbJailLR.Image = Utils.ByteToImage(form.FingerData.FpLr);
                    StaticData.Enrollment.profile.biometric.fingerprint.lr = form.FingerData.WsqLr;
                    
                    pbJailLL.Image = Utils.ByteToImage(form.FingerData.FpLl);
                    StaticData.Enrollment.profile.biometric.fingerprint.ls = form.FingerData.WsqLl;
                    
                    if (StaticData.Enrollment.profile?.biometric?.fingerprint?.rt == null || StaticData.Enrollment.profile?.biometric?.fingerprint?.ri == null ||
                StaticData.Enrollment.profile?.biometric?.fingerprint?.rm == null || StaticData.Enrollment.profile?.biometric?.fingerprint?.rr == null ||
                StaticData.Enrollment.profile?.biometric?.fingerprint?.rs == null || StaticData.Enrollment.profile?.biometric?.fingerprint?.lt == null ||
                StaticData.Enrollment.profile?.biometric?.fingerprint?.li == null || StaticData.Enrollment.profile?.biometric?.fingerprint?.lm == null ||
                StaticData.Enrollment.profile?.biometric?.fingerprint?.lr == null || StaticData.Enrollment.profile?.biometric?.fingerprint?.ls == null)
                    {
                        btnCaptFpJail.Text = "Re-Capture Fingerprint";
                    }

                    UpdateBiometricDataLocal();
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an unexpected error while capturing fingerprint. Please contact with your Administrator.");
            }
        }

        private void btnJailIdentifyFp_Click(object sender, EventArgs e)
        {
            if (StaticData.Enrollment.profile?.biometric?.fingerprint?.rt == null && StaticData.Enrollment.profile?.biometric?.fingerprint?.ri == null &&
                StaticData.Enrollment.profile?.biometric?.fingerprint?.rm == null && StaticData.Enrollment.profile?.biometric?.fingerprint?.rr == null &&
                StaticData.Enrollment.profile?.biometric?.fingerprint?.rs == null && StaticData.Enrollment.profile?.biometric?.fingerprint?.lt == null &&
                StaticData.Enrollment.profile?.biometric?.fingerprint?.li == null && StaticData.Enrollment.profile?.biometric?.fingerprint?.lm == null &&
                StaticData.Enrollment.profile?.biometric?.fingerprint?.lr == null && StaticData.Enrollment.profile?.biometric?.fingerprint?.ls == null)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Fingerprint is needed for search");
                return;
            }
            ((BiometricController)controller).JailIdentifyByBiometric(1);
        }

        private void btnJailFpReset_Click(object sender, EventArgs e)
        {
            pbJailRT.Image = null;
            StaticData.Enrollment.profile.biometric.fingerprint.rt = null;
            pbJailRI.Image = null;
            StaticData.Enrollment.profile.biometric.fingerprint.ri = null;
            pbJailRM.Image = null;
            StaticData.Enrollment.profile.biometric.fingerprint.rm = null;
            pbJailRR.Image = null;
            StaticData.Enrollment.profile.biometric.fingerprint.rr = null;
            pbJailRL.Image = null;
            StaticData.Enrollment.profile.biometric.fingerprint.rs = null;

            pbJailLT.Image = null;
            StaticData.Enrollment.profile.biometric.fingerprint.lt = null;
            pbJailLI.Image = null;
            StaticData.Enrollment.profile.biometric.fingerprint.li = null;
            pbJailLM.Image = null;
            StaticData.Enrollment.profile.biometric.fingerprint.lm = null;
            pbJailLR.Image = null;
            StaticData.Enrollment.profile.biometric.fingerprint.lr = null;
            pbJailLL.Image = null;
            StaticData.Enrollment.profile.biometric.fingerprint.ls = null;
        }

        private void btnIrisCaptJail_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new IrisCaptureDialogForm();
                DialogResult dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    this.pbJailLeftIris.Image = Utils.ByteToImage(form.irisData.LeftIris);
                    this.pbJailRightIris.Image = Utils.ByteToImage(form.irisData.RightIris);

                    if (StaticData.Enrollment.profile.biometric.iris.right != null ||
                        StaticData.Enrollment.profile.biometric.iris.left != null)
                    {
                        btnTakeIris.Text = "Re-Capture Iris";
                    }

                    dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "right_iris",
             StaticData.Enrollment?.profile?.biometric?.iris?.right);
                    dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "left_iris",
                StaticData.Enrollment?.profile?.biometric?.iris?.left);
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an unexpected error while capturing iris. Please contact with your Administrator.");
            }
        }

        private void btnJailIdetifyIris_Click(object sender, EventArgs e)
        {
            if (StaticData.Enrollment.profile?.biometric?.iris?.left == null && StaticData.Enrollment.profile?.biometric?.iris?.right == null)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Iris is needed for search");
                return;
            }
            ((BiometricController)controller).JailIdentifyByBiometric(3);
        }

        private void btnJailIrisReset_Click(object sender, EventArgs e)
        {
            pbJailLeftIris.Image = null;
            StaticData.Enrollment.profile.biometric.iris.left = null;
            pbJailRightIris.Image = null;
            StaticData.Enrollment.profile.biometric.iris.right = null;
        }

        public void TakeJailFP(PictureBox pictureBox)
        {
            Image img = null;
            byte[] wsqBuffer = null;

            openFileDialog.Filter = "Image Files(*.wsq)|" + "*.wsq";

            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    wsqBuffer = File.ReadAllBytes(openFileDialog.FileName);
                    img = AppUtils.AppUtils.WsqToImage(wsqBuffer);
                }
                catch 
                {
                    return;
                }
            }

            if (img == null || wsqBuffer == null) return;

            btnCaptFpJail.Text = "Re-Capture Fingerprint";

            pictureBox.Image = img;

            // Right FP
            if (pictureBox.Name == "pbJailRT")
            {
                StaticData.Enrollment.profile.biometric.fingerprint.rt = wsqBuffer;
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "fp_rt",
                StaticData.Enrollment?.profile?.biometric?.fingerprint?.rt);
            }
            if (pictureBox.Name == "pbJailRI")
            {
                StaticData.Enrollment.profile.biometric.fingerprint.ri = wsqBuffer;
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "fp_ri",
                StaticData.Enrollment?.profile?.biometric?.fingerprint?.ri);
            }
            if (pictureBox.Name == "pbJailRM")
            {
                StaticData.Enrollment.profile.biometric.fingerprint.rm = wsqBuffer;
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "fp_rm",
                StaticData.Enrollment?.profile?.biometric?.fingerprint?.rm);
            }
            if (pictureBox.Name == "pbJailRR")
            {
                StaticData.Enrollment.profile.biometric.fingerprint.rr = wsqBuffer;
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "fp_rr",
                StaticData.Enrollment?.profile?.biometric?.fingerprint?.rr);
            }
            if (pictureBox.Name == "pbJailRL")
            {
                StaticData.Enrollment.profile.biometric.fingerprint.rs = wsqBuffer;
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "fp_rs",
                StaticData.Enrollment?.profile?.biometric?.fingerprint?.rs);
            }

            // Left FP
            if (pictureBox.Name == "pbJailLT")
            {
                StaticData.Enrollment.profile.biometric.fingerprint.lt = wsqBuffer;
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "fp_lt",
                StaticData.Enrollment?.profile?.biometric?.fingerprint?.lt);
            }
            if (pictureBox.Name == "pbJailLI")
            {
                StaticData.Enrollment.profile.biometric.fingerprint.li = wsqBuffer;
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "fp_li",
                StaticData.Enrollment?.profile?.biometric?.fingerprint?.li);
            }
            if (pictureBox.Name == "pbJailLM")
            {
                StaticData.Enrollment.profile.biometric.fingerprint.lm = wsqBuffer;
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "fp_lm",
                StaticData.Enrollment?.profile?.biometric?.fingerprint?.lm);
            }
            if (pictureBox.Name == "pbJailLR")
            {
                StaticData.Enrollment.profile.biometric.fingerprint.lr = wsqBuffer;
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "fp_lr",
                StaticData.Enrollment?.profile?.biometric?.fingerprint?.lr);
            }
            if (pictureBox.Name == "pbJailLL")
            {
                StaticData.Enrollment.profile.biometric.fingerprint.ls = wsqBuffer;
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "fp_ls",
                StaticData.Enrollment?.profile?.biometric?.fingerprint?.ls);
            }
        }

        private void pbJailRT_Click(object sender, EventArgs e)
        {
            TakeJailFP(pbJailRT);
        }
        private void pbJailRI_Click(object sender, EventArgs e)
        {
            TakeJailFP(pbJailRI);
        }
        private void pbJailRM_Click(object sender, EventArgs e)
        {
            TakeJailFP(pbJailRM);
        }
        private void pbJailRR_Click(object sender, EventArgs e)
        {
            TakeJailFP(pbJailRR);
        }
        private void pbJailRL_Click(object sender, EventArgs e)
        {
            TakeJailFP(pbJailRL);
        }
        private void pbJailLT_Click(object sender, EventArgs e)
        {
            TakeJailFP(pbJailLT);
        }
        private void pbJailLI_Click(object sender, EventArgs e)
        {
            TakeJailFP(pbJailLI);
        }
        private void pbJailLM_Click(object sender, EventArgs e)
        {
            TakeJailFP(pbJailLM);
        }
        private void pbJailLR_Click(object sender, EventArgs e)
        {
            TakeJailFP(pbJailLR);
        }
        private void pbJailLL_Click(object sender, EventArgs e)
        {
            TakeJailFP(pbJailLL);
        }

        public void TakeJailIris(PictureBox pictureBox)
        {
            Image img = null;
            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.bmp;)|" +
                    "*.jpg; *.jpeg; *.png; *.bmp;";

            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                img = Image.FromFile(openFileDialog.FileName);
            }
            if (img == null) return;

            pictureBox.Image = img;

            btnIrisCaptJail.Text = "Re-Capture Iris";

            int maxIrisSize = Convert.ToInt32(ConfigurationManager.AppSettings["IrisSizeInKB"].ToString());
            if (GraphicsManager.GetInstance().ImageToByteArray(img).Length > maxIrisSize)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Iris photo size is too big. Size should not exceed " + (maxIrisSize / (1024 * 1000)) + " MB");
                return;
            }

            pictureBox.Image = img;

            if (pictureBox.Name == "pbJailRightIris")
            {
                StaticData.Enrollment.profile.biometric.iris.right = Utils.ImageToByte(img);
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "right_iris",
                StaticData.Enrollment?.profile?.biometric?.iris?.right);
            }
            if (pictureBox.Name == "pbJailLeftIris")
            {
                StaticData.Enrollment.profile.biometric.iris.left = Utils.ImageToByte(img);
                dbEnrollClientManager.UpdateProfileBiometricValues(StaticData.Enrollment?.profile?.referenceNo, "left_iris",
                StaticData.Enrollment?.profile?.biometric?.iris?.left);
            }
        }

        private void pbJailRightIris_Click(object sender, EventArgs e)
        {
            TakeJailIris(pbJailRightIris);
        }
        private void pbJailLeftIris_Click(object sender, EventArgs e)
        {
            TakeJailIris(pbJailLeftIris);
        }
    }
}
