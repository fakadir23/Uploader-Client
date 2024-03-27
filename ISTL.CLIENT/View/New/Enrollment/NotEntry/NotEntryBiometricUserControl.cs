using ISTL.COMMON;
using ISTL.COMMON.Common;
using ISTL.MODELS.DTO.New.NotEntry;
using ISTL.RAB.Controllers.New.Enrollment.NotEntry;
using ISTL.RAB.Entity;
using ISTL.RAB.View.New.Enrollment.BiometricInformation;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISTL.RAB.View.New.Enrollment.NotEntry
{
    public partial class NotEntryBiometricUserControl : ViewUserControl
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        public NotEntryBiometricUserControl()
        {
            InitializeComponent();
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            if (StaticData.NotEntry?.biometric == null)
            {
                StaticData.NotEntry.biometric = new MODELS.DTO.New.Enrollment.Biometric.BiometricDto();
            }
            if (StaticData.NotEntry?.biometric?.photo == null)
            {
                StaticData.NotEntry.biometric.photo = new MODELS.DTO.New.Enrollment.Biometric.PhotoDto();
            }
            if (StaticData.NotEntry?.biometric?.fingerprint == null)
            {
                StaticData.NotEntry.biometric.fingerprint = new MODELS.DTO.New.Enrollment.Biometric.FingerprintDto();
            }
            if (StaticData.NotEntry?.biometric?.iris == null)
            {
                StaticData.NotEntry.biometric.iris = new MODELS.DTO.New.Enrollment.Biometric.IrisDto();
            }

            if (StaticData.NotEntry.biometric != null)
            {
                ShowBiometricInfo(StaticData.NotEntry);
                if (StaticData.ModifiableNotEntry == false)
                {
                    MakeFieldsReadonly();
                }
            }
        }

        public override void OnClosing()
        {
            base.OnClosing();

            if (!btnBackToBasicInfo.ContainsFocus && !btnBiometricSaveNext.ContainsFocus 
                && !btnPreviewSubmit.ContainsFocus && !btnCriminalProfile.ContainsFocus)
            {
                StaticData.NotEntry = new NotEntryDto();
            }
        }

        private void btnCriminalProfile_Click(object sender, EventArgs e)
        {
            ((NotEntryBiometricController)controller).OnBackToEntry();
        }

        private void btnPreviewSubmit_Click(object sender, EventArgs e)
        {
            ((NotEntryBiometricController)controller).SubmitPreview();
        }

        private void ShowBiometricInfo(NotEntryDto dto)
        {
            try
            {
                this.pbPhoto.Image = Utils.ByteToImage(dto.biometric?.photo?.photo);

                this.pbRightIris.Image = Utils.ByteToImage(dto.biometric?.iris?.right);
                this.pbLeftIris.Image = Utils.ByteToImage(dto.biometric?.iris?.left);

                this.pbRT.Image = AppUtils.AppUtils.WsqToImage(dto.biometric?.fingerprint?.rt);
                this.pbRI.Image = AppUtils.AppUtils.WsqToImage(dto.biometric?.fingerprint?.ri);
                this.pbRM.Image = AppUtils.AppUtils.WsqToImage(dto.biometric?.fingerprint?.rm);
                this.pbRR.Image = AppUtils.AppUtils.WsqToImage(dto.biometric?.fingerprint?.rr);
                this.pbRS.Image = AppUtils.AppUtils.WsqToImage(dto.biometric?.fingerprint?.rs);

                this.pbLT.Image = AppUtils.AppUtils.WsqToImage(dto.biometric?.fingerprint?.lt);
                this.pbLI.Image = AppUtils.AppUtils.WsqToImage(dto.biometric?.fingerprint?.li);
                this.pbLM.Image = AppUtils.AppUtils.WsqToImage(dto.biometric?.fingerprint?.lm);
                this.pbLR.Image = AppUtils.AppUtils.WsqToImage(dto.biometric?.fingerprint?.lr);
                this.pbLS.Image = AppUtils.AppUtils.WsqToImage(dto.biometric?.fingerprint?.ls);
            }
            catch (Exception ex)
            {

            }
        }

        private void MakeFieldsReadonly()
        {
            NotEntryDto dto = StaticData.NotEntry;
            if (dto.biometric != null)
            {
                if (dto.biometric.photo?.photo != null)
                {
                    pbPhoto.Enabled = false;
                    btnTakePhoto.Enabled = false;
                    btnTakePhoto.BackColor = Color.IndianRed;
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
                    btnTakeFP.Enabled = false;
                    btnTakeFP.BackColor = Color.IndianRed;
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

        private Image GetImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.bmp;)|" + "*.jpg; *.jpeg; *.png; *.bmp;";

            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                try
                {
                    return Image.FromFile(openFileDialog.FileName);
                }
                catch (Exception x)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Error occured while selecting this file. Please select another");
                    logger.Error("Error occured while selecting file.\n" + x.ToString());
                }
            }
            return null;
        }

        private void btnTakePhoto_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = DialogResult.OK;
                var form = new ImageCaptureDialogForm();
                dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    this.pbPhoto.Image = Utils.ByteToImage(form.CamData.CamImage);
                    btnTakePhoto.Text = "Re-Capture Photo";
                    StaticData.NotEntry.biometric.photo.photo = form.CamData.CamImage;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Photo capture error." + ex.ToString());
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an unexpected error while capturing photo. Please contact with your Administrator.");
            }
        }

        private void pbPhoto_Click(object sender, EventArgs e)
        {
            Image img = GetImage();
            if (img == null) return;

            int maxPhotoSize = Convert.ToInt32(ConfigurationManager.AppSettings["PhotoSizeInKB"].ToString());
            if (GraphicsManager.GetInstance().ImageToByteArray(img).Length > maxPhotoSize)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Photo size is too big. Size should not exceed " + (maxPhotoSize / (1024 * 1000)) + " MB");
                return;
            }

            pbPhoto.Image = img;
            btnTakePhoto.Text = "Re-Capture Photo";
            StaticData.NotEntry.biometric.photo.photo = Utils.ImageToByte(img);
        }

        private void btnResetPhoto_Click(object sender, EventArgs e)
        {
            pbPhoto.Image = null;

            if (StaticData.NotEntry.biometric == null)
            {
                StaticData.NotEntry.biometric = new MODELS.DTO.New.Enrollment.Biometric.BiometricDto();
            }

            StaticData.NotEntry.biometric.photo = new MODELS.DTO.New.Enrollment.Biometric.PhotoDto();
        }

        private void btnTakeFP_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = DialogResult.OK;
                var form = new FingerprintCaptureDialogForm();
                dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    pbRT.Image = Utils.ByteToImage(form.FingerData.FpRt);
                    StaticData.NotEntry.biometric.fingerprint.rt = form.FingerData.WsqRt;
                    pbRI.Image = Utils.ByteToImage(form.FingerData.FpRi);
                    StaticData.NotEntry.biometric.fingerprint.ri = form.FingerData.WsqRi;
                    pbRM.Image = Utils.ByteToImage(form.FingerData.FpRm);
                    StaticData.NotEntry.biometric.fingerprint.rm = form.FingerData.WsqRm;
                    pbRR.Image = Utils.ByteToImage(form.FingerData.FpRr);
                    StaticData.NotEntry.biometric.fingerprint.rr = form.FingerData.WsqRr;
                    pbRS.Image = Utils.ByteToImage(form.FingerData.FpRl);
                    StaticData.NotEntry.biometric.fingerprint.rs = form.FingerData.WsqRl;

                    pbLT.Image = Utils.ByteToImage(form.FingerData.FpLt);
                    StaticData.NotEntry.biometric.fingerprint.lt = form.FingerData.WsqLt;
                    pbLI.Image = Utils.ByteToImage(form.FingerData.FpLi);
                    StaticData.NotEntry.biometric.fingerprint.li = form.FingerData.WsqLi;
                    pbLM.Image = Utils.ByteToImage(form.FingerData.FpLm);
                    StaticData.NotEntry.biometric.fingerprint.lm = form.FingerData.WsqLm;
                    pbLR.Image = Utils.ByteToImage(form.FingerData.FpLr);
                    StaticData.NotEntry.biometric.fingerprint.lr = form.FingerData.WsqLr;
                    pbLS.Image = Utils.ByteToImage(form.FingerData.FpLl);
                    StaticData.NotEntry.biometric.fingerprint.ls = form.FingerData.WsqLl;

                    if (StaticData.NotEntry.biometric.fingerprint.lt != null ||
                        StaticData.NotEntry.biometric.fingerprint.li != null ||
                        StaticData.NotEntry.biometric.fingerprint.lm != null ||
                        StaticData.NotEntry.biometric.fingerprint.lr != null ||
                        StaticData.NotEntry.biometric.fingerprint.ls != null ||
                        StaticData.NotEntry.biometric.fingerprint.rt != null ||
                        StaticData.NotEntry.biometric.fingerprint.ri != null ||
                        StaticData.NotEntry.biometric.fingerprint.rm != null ||
                        StaticData.NotEntry.biometric.fingerprint.rr != null ||
                        StaticData.NotEntry.biometric.fingerprint.rs != null)
                    {
                        btnTakeFP.Text = "Re-Capture Fingerprint";
                    }
                }
            }
            catch (Exception)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an unexpected error while capturing fingerprint. Please contact with your Administrator.");
            }
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

            if (StaticData.NotEntry.biometric == null)
            {
                StaticData.NotEntry.biometric = new MODELS.DTO.New.Enrollment.Biometric.BiometricDto();
            }

            StaticData.NotEntry.biometric.fingerprint = new MODELS.DTO.New.Enrollment.Biometric.FingerprintDto();
        }

        private void btnTakeIris_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new IrisCaptureDialogForm();
                DialogResult dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    this.pbLeftIris.Image = Utils.ByteToImage(form.irisData.LeftIris);
                    this.pbRightIris.Image = Utils.ByteToImage(form.irisData.RightIris);

                    StaticData.NotEntry.biometric.iris.right = form.irisData.RightIris;
                    StaticData.NotEntry.biometric.iris.left = form.irisData.LeftIris;

                    if (StaticData.NotEntry.biometric.iris.right != null || StaticData.NotEntry.biometric.iris.left != null)
                    {
                        btnTakeIris.Text = "Re-Capture Iris";
                    }
                }
            }
            catch (Exception)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an unexpected error while capturing iris. Please contact with your Administrator.");
            }
        }

        private void btnResetIris_Click(object sender, EventArgs e)
        {
            pbLeftIris.Image = null;
            pbRightIris.Image = null;

            if (StaticData.NotEntry.biometric == null)
            {
                StaticData.NotEntry.biometric = new MODELS.DTO.New.Enrollment.Biometric.BiometricDto();
            }

            StaticData.NotEntry.biometric.iris = new MODELS.DTO.New.Enrollment.Biometric.IrisDto();
        }

        public void TakeIris(PictureBox pictureBox)
        {
            Image img = GetImage();
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
                StaticData.NotEntry.biometric.iris.right = Utils.ImageToByte(img);
            }
            if (pictureBox.Name == "pbLeftIris")
            {
                StaticData.NotEntry.biometric.iris.left = Utils.ImageToByte(img);
            }
        }

        private void pbRightIris_Click(object sender, EventArgs e)
        {
            TakeIris(pbRightIris);
        }

        private void pbLeftIris_Click(object sender, EventArgs e)
        {
            TakeIris(pbLeftIris);
        }

        public void TakeFP(PictureBox pictureBox)
        {
            Image img = null;
            byte[] wsqBuffer = null;

            OpenFileDialog openFileDialog = new OpenFileDialog();

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

            pictureBox.Image = img;
            btnTakeFP.Text = "Re-Capture Fingerprint";

            // Right FP
            if (pictureBox.Name == "pbRT")
            {
                StaticData.NotEntry.biometric.fingerprint.rt = wsqBuffer;
            }
            if (pictureBox.Name == "pbRI")
            {
                StaticData.NotEntry.biometric.fingerprint.ri = wsqBuffer;
            }
            if (pictureBox.Name == "pbRM")
            {
                StaticData.NotEntry.biometric.fingerprint.rm = wsqBuffer;
            }
            if (pictureBox.Name == "pbRR")
            {
                StaticData.NotEntry.biometric.fingerprint.rr = wsqBuffer;
            }
            if (pictureBox.Name == "pbRS")
            {
                StaticData.NotEntry.biometric.fingerprint.rs = wsqBuffer;
            }

            // Left FP
            if (pictureBox.Name == "pbLT")
            {
                StaticData.NotEntry.biometric.fingerprint.lt = wsqBuffer;
            }
            if (pictureBox.Name == "pbLI")
            {
                StaticData.NotEntry.biometric.fingerprint.li = wsqBuffer;
            }
            if (pictureBox.Name == "pbLM")
            {
                StaticData.NotEntry.biometric.fingerprint.lm = wsqBuffer;
            }
            if (pictureBox.Name == "pbLR")
            {
                StaticData.NotEntry.biometric.fingerprint.lr = wsqBuffer;
            }
            if (pictureBox.Name == "pbLS")
            {
                StaticData.NotEntry.biometric.fingerprint.ls = wsqBuffer;
            }
        }

        private void pbRT_Click(object sender, EventArgs e)
        {
            TakeFP(pbRT);
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

        private void pbLT_Click(object sender, EventArgs e)
        {
            TakeFP(pbLT);
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
    }
}
