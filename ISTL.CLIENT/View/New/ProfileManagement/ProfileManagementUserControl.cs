using ISTL.COMMON;
using ISTL.MODELS.DTO.ProfileManagement.Enrollment;
using ISTL.MODELS.Request.ProfileManagement;
using ISTL.MODELS.Response.ProfileManagement;
using ISTL.RAB.Controllers.New.ProfileManagement;
using ISTL.RAB.Entity;
using ISTL.RAB.View.New.Enrollment.BiometricInformation;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISTL.RAB.View.New.ProfileManagement
{
    public partial class ProfileManagementUserControl : ViewUserControl
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        public ProfileManagementEnrollmentRequest enrollmentDto;
        public ProfileManagementUserControl()
        {
            InitializeComponent();
            LoadComboBox();            
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            enrollmentDto = new ProfileManagementEnrollmentRequest();

            if (enrollmentDto.photo == null)
            {
                enrollmentDto.photo = new ProfilePhotoDto();                
            }

            if (enrollmentDto.fingerprint == null)
            {
                enrollmentDto.fingerprint = new ProfileFingerprintDto();
            }

            if (enrollmentDto.iris == null)
            {
                enrollmentDto.iris = new ProfileIrisDto();
            }
        }

        public string ProfileId
        {
            get { return tbProfileId.Text; }
            set { tbProfileId.Text = value; }
        }

        public string ProfileLongId { get; set; }

        private void LoadComboBox()
        {
            cmbGender.DataSource = new BindingSource(ComboBoxItems.genders, null);
            cmbGender.DisplayMember = "Value";
            cmbGender.ValueMember = "Key";
            cmbGender.SelectedIndex = -1;

            cmbProfileType.DataSource = new BindingSource(ComboBoxItems.profileType, null);
            cmbProfileType = Utils.SuggestComboBoxFormat(cmbProfileType, 1);
        }

        private void pbPhoto_Click(object sender, EventArgs e)
        {
            UploadImage(pbPhoto);

            if (pbPhoto.Image != null)
            {
                enrollmentDto.photo.photo = Utils.ImageToByte(pbPhoto.Image);
            }
        }

        public void UploadFP(PictureBox pictureBox)
        {
            Image img = null;
            byte[] wsqBuffer = null;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.wsq)|" + "*.wsq";

            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                wsqBuffer = File.ReadAllBytes(openFileDialog.FileName);
                img = AppUtils.AppUtils.WsqToImage(wsqBuffer);
            }

            if (img == null || wsqBuffer == null || wsqBuffer?.Length <= 0) return;

            pictureBox.Image = img;

            if (pictureBox == pbRT) enrollmentDto.fingerprint.rt = wsqBuffer;
            else if (pictureBox == pbRI) enrollmentDto.fingerprint.ri = wsqBuffer;
            else if (pictureBox == pbRM) enrollmentDto.fingerprint.rm = wsqBuffer;
            else if (pictureBox == pbRR) enrollmentDto.fingerprint.rr = wsqBuffer;
            else if (pictureBox == pbRL) enrollmentDto.fingerprint.rs = wsqBuffer;

            else if (pictureBox == pbLT) enrollmentDto.fingerprint.lt = wsqBuffer;
            else if (pictureBox == pbLI) enrollmentDto.fingerprint.li = wsqBuffer;
            else if (pictureBox == pbLM) enrollmentDto.fingerprint.lm = wsqBuffer;
            else if (pictureBox == pbLR) enrollmentDto.fingerprint.lr = wsqBuffer;
            else if (pictureBox == pbLL) enrollmentDto.fingerprint.ls = wsqBuffer;
        }

        private void pictureBoxRT_Click(object sender, EventArgs e)
        {
            UploadFP(pbRT);
        }

        private void pictureBoxRI_Click(object sender, EventArgs e)
        {
            UploadFP(pbRI);
        }

        private void pictureBoxRM_Click(object sender, EventArgs e)
        {
            UploadFP(pbRM);
        }

        private void pictureBoxRR_Click(object sender, EventArgs e)
        {
            UploadFP(pbRR);
        }

        private void pictureBoxRL_Click(object sender, EventArgs e)
        {
            UploadFP(pbRL);
        }

        private void pictureBoxLT_Click(object sender, EventArgs e)
        {
            UploadFP(pbLT);
        }

        private void pictureBoxLI_Click(object sender, EventArgs e)
        {
            UploadFP(pbLI);
        }

        private void pictureBoxLM_Click(object sender, EventArgs e)
        {
            UploadFP(pbLM);
        }

        private void pictureBoxLR_Click(object sender, EventArgs e)
        {
            UploadFP(pbLR);
        }

        private void pictureBoxLL_Click(object sender, EventArgs e)
        {
            UploadFP(pbLL);
        }

        private void pictureBoxRightIris_Click(object sender, EventArgs e)
        {
            UploadImage(pbRightIris);

            if (pbRightIris.Image != null)   enrollmentDto.iris.right = Utils.ImageToByte(pbRightIris.Image);
        }

        private void pictureBoxLeftIris_Click(object sender, EventArgs e)
        {
            UploadImage(pbLeftIris);

            if (pbLeftIris.Image != null)   enrollmentDto.iris.left = Utils.ImageToByte(pbLeftIris.Image);
        }

        private void UploadImage(PictureBox pb)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.bmp;)|" +
                    "*.jpg; *.jpeg; *.png; *.bmp;";
            
            DialogResult result = openFileDialog.ShowDialog();

            Image image = null;

            if (result == DialogResult.OK)
            {
                try
                {
                    image = Image.FromFile(openFileDialog.FileName);
                }
                catch (Exception x)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Error occured while selecting this file. Please select another");
                    logger.Error("Error occured while selecting file.\n" + x.ToString());
                }
            }

            if (image == null) return;

            pb.Image = image;
        }

        private void btnPhotoCapture_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = DialogResult.OK;
                var form = new ImageCaptureDialogForm();
                dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    pbPhoto.Image = Utils.ByteToImage(form.CamData?.CamImage);

                    enrollmentDto.photo.photo = form.CamData?.CamImage;
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.ShowMessage("RAB Profile Management", "There was an unexpected error while capturing photo. Please contact with your Administrator.");
                logger.Error("Photo capture error. " + ex.ToString());
            }
        }

        private void btnResetPhoto_Click(object sender, EventArgs e)
        {
            pbPhoto.Image = null;

            if (enrollmentDto.photo == null)
            {
                enrollmentDto.photo = new ProfilePhotoDto();
            }
            else
            {
                enrollmentDto.photo.photo = null;
            }
        }

        private void btnIrisCapture_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new IrisCaptureDialogForm();
                DialogResult dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    pbLeftIris.Image = Utils.ByteToImage(form.irisData?.LeftIris);
                    pbRightIris.Image = Utils.ByteToImage(form.irisData?.RightIris);

                    enrollmentDto.iris.left = form.irisData?.LeftIris;
                    enrollmentDto.iris.right = form.irisData?.RightIris;
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.ShowMessage("RAB Profile Management", "There was an unexpected error while capturing iris. Please contact with your Administrator.");
                logger.Error("Iris capture error. " + ex.ToString());
            }
        }

        private void btnResetIris_Click(object sender, EventArgs e)
        {
            pbRightIris.Image = null;
            pbLeftIris.Image = null;

            if (enrollmentDto.iris == null)
            {
                enrollmentDto.iris = new ProfileIrisDto();
            }
            else
            {
                enrollmentDto.iris.left = null;
                enrollmentDto.iris.right = null;
            }            
        }

        private void btnFpCapture_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = DialogResult.OK;
                var form = new FingerprintCaptureDialogForm();
                dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    pbRT.Image = Utils.ByteToImage(form.FingerData.FpRt);
                    pbRI.Image = Utils.ByteToImage(form.FingerData.FpRi);
                    pbRM.Image = Utils.ByteToImage(form.FingerData.FpRm);
                    pbRR.Image = Utils.ByteToImage(form.FingerData.FpRr);
                    pbRL.Image = Utils.ByteToImage(form.FingerData.FpRl);

                    pbLT.Image = Utils.ByteToImage(form.FingerData.FpLt);
                    pbLI.Image = Utils.ByteToImage(form.FingerData.FpLi);
                    pbLM.Image = Utils.ByteToImage(form.FingerData.FpLm);
                    pbLR.Image = Utils.ByteToImage(form.FingerData.FpLr);
                    pbLL.Image = Utils.ByteToImage(form.FingerData.FpLl);

                    enrollmentDto.fingerprint.rt = form.FingerData.WsqRt;
                    enrollmentDto.fingerprint.ri = form.FingerData.WsqRi;
                    enrollmentDto.fingerprint.rm = form.FingerData.WsqRm;
                    enrollmentDto.fingerprint.rr = form.FingerData.WsqRr;
                    enrollmentDto.fingerprint.rs = form.FingerData.WsqRl;

                    enrollmentDto.fingerprint.lt = form.FingerData.WsqLt;
                    enrollmentDto.fingerprint.li = form.FingerData.WsqLi;
                    enrollmentDto.fingerprint.lm = form.FingerData.WsqLm;
                    enrollmentDto.fingerprint.lr = form.FingerData.WsqLr;
                    enrollmentDto.fingerprint.ls = form.FingerData.WsqLl;
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.ShowMessage("RAB Profile Management", "There was an unexpected error while capturing fingerprint. Please contact with your Administrator.");
                logger.Error("Fp capture error. "+ex.ToString());
            }
        }

        private void btnResetFp_Click(object sender, EventArgs e)
        {
            pbRT.Image = null;
            pbRI.Image = null;
            pbRM.Image = null;
            pbRR.Image = null;
            pbRL.Image = null;

            pbLT.Image = null;
            pbLI.Image = null;
            pbLM.Image = null;
            pbLR.Image = null;
            pbLL.Image = null;

            if (enrollmentDto.fingerprint == null)
            {
                enrollmentDto.fingerprint = new ProfileFingerprintDto();
            }
            else
            {
                enrollmentDto.fingerprint.rt = null;
                enrollmentDto.fingerprint.ri = null;
                enrollmentDto.fingerprint.rm = null;
                enrollmentDto.fingerprint.rr = null;
                enrollmentDto.fingerprint.rs = null;

                enrollmentDto.fingerprint.lt = null;
                enrollmentDto.fingerprint.li = null;
                enrollmentDto.fingerprint.lm = null;
                enrollmentDto.fingerprint.lr = null;
                enrollmentDto.fingerprint.ls = null;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbProfileId.Text))
            {
                CustomMessageBox.ShowMessage("RAB Profile Management", "Insert profile id to search by");
                return;
            }

            ClearEnrollmentPage(1);

            ProfileManagementEnrollmentRequest response = ((ProfileManagementController)controller).FetchProfileById(tbProfileId.Text);

            if (response != null)
            {
                //ProcessingDialog.Run(delegate ()
                //{
                //    Invoke((MethodInvoker)delegate
                //    {
                        ShowProfileData(response);
                //    });
                //});
            }
            else
            {
                CustomMessageBox.ShowMessage("RAB Profile Management", "No profile found");
            }
        }

        private void ShowProfileData(ProfileManagementEnrollmentRequest profile)
        {
            if (!string.IsNullOrEmpty(profile.fullName))
            {
                tbFullName.Text = profile.fullName;
                tbFullName.Enabled = false;
            }

            if (profile.gender != null)
            {
                try
                {
                    cmbGender.SelectedValue = profile.gender;
                    cmbGender.Enabled = false;
                }
                catch { }
            }

            if (profile.profileType != null)
            {
                try
                {
                    cmbProfileType.SelectedValue = profile.profileType;
                    cmbProfileType.Enabled = false;
                }
                catch { }                
            }

            if (profile.photo?.photo?.Length > 0)
            {
                if (enrollmentDto.photo == null)
                {
                    enrollmentDto.photo = new ProfilePhotoDto();
                }

                pbPhoto.Image = Utils.ByteToImage(profile.photo?.photo);
                enrollmentDto.photo.photo = profile.photo?.photo;

                pbPhoto.Enabled = false;
                btnPhotoCapture.Enabled = false;
                btnResetPhoto.Enabled = false;
            }

            if (profile.fingerprint != null)
            {
                if (enrollmentDto.fingerprint == null)
                {
                    enrollmentDto.fingerprint = new ProfileFingerprintDto();
                }
                // Right finger
                if (profile.fingerprint.rt?.Length > 0)
                {
                    enrollmentDto.fingerprint.rt = profile.fingerprint?.rt;
                    pbRT.Image = AppUtils.AppUtils.WsqToImage(profile.fingerprint?.rt);

                    pbRT.Enabled = false;
                }
                if (profile.fingerprint.ri?.Length > 0)
                {
                    enrollmentDto.fingerprint.ri = profile.fingerprint?.ri;
                    pbRI.Image = AppUtils.AppUtils.WsqToImage(profile.fingerprint?.ri);

                    pbRI.Enabled = false;
                }
                if (profile.fingerprint.rm?.Length > 0)
                {
                    enrollmentDto.fingerprint.rm = profile.fingerprint?.rm;
                    pbRM.Image = AppUtils.AppUtils.WsqToImage(profile.fingerprint?.rm);

                    pbRM.Enabled = false;
                }
                if (profile.fingerprint.rr?.Length > 0)
                {
                    enrollmentDto.fingerprint.rr = profile.fingerprint?.rr;
                    pbRR.Image = AppUtils.AppUtils.WsqToImage(profile.fingerprint?.rr);

                    pbRR.Enabled = false;
                }
                if (profile.fingerprint.rs?.Length > 0)
                {
                    enrollmentDto.fingerprint.rs = profile.fingerprint?.rs;
                    pbRL.Image = AppUtils.AppUtils.WsqToImage(profile.fingerprint?.rs);

                    pbRL.Enabled = false;
                }

                // Left finger
                if (profile.fingerprint.lt?.Length > 0)
                {
                    enrollmentDto.fingerprint.lt = profile.fingerprint?.lt;
                    pbLT.Image = AppUtils.AppUtils.WsqToImage(profile.fingerprint?.lt);

                    pbLT.Enabled = false;
                }
                if (profile.fingerprint.li?.Length > 0)
                {
                    enrollmentDto.fingerprint.li = profile.fingerprint?.li;
                    pbLI.Image = AppUtils.AppUtils.WsqToImage(profile.fingerprint?.li);

                    pbLI.Enabled = false;
                }
                if (profile.fingerprint.lm?.Length > 0)
                {
                    enrollmentDto.fingerprint.lm = profile.fingerprint?.lm;
                    pbLM.Image = AppUtils.AppUtils.WsqToImage(profile.fingerprint?.lm);

                    pbLM.Enabled = false;
                }
                if (profile.fingerprint.lr?.Length > 0)
                {
                    enrollmentDto.fingerprint.lr = profile.fingerprint?.lr;
                    pbLR.Image = AppUtils.AppUtils.WsqToImage(profile.fingerprint?.lr);

                    pbLR.Enabled = false;
                }
                if (profile.fingerprint.ls?.Length > 0)
                {
                    enrollmentDto.fingerprint.ls = profile.fingerprint?.ls;
                    pbLL.Image = AppUtils.AppUtils.WsqToImage(profile.fingerprint?.ls);

                    pbLL.Enabled = false;
                }

                if (!pbRT.Enabled && !pbRI.Enabled && !pbRM.Enabled && !pbRR.Enabled && !pbRL.Enabled &&
                    !pbLT.Enabled && !pbLI.Enabled && !pbLM.Enabled && !pbLR.Enabled && !pbLL.Enabled)
                {
                    btnFpCapture.Enabled = false;
                    btnResetFp.Enabled = false;
                }

            }

            if (profile.iris != null)
            {
                if (enrollmentDto.iris == null)
                {
                    enrollmentDto.iris = new ProfileIrisDto();
                }

                if (profile.iris.left?.Length > 0)
                {
                    pbLeftIris.Image = Utils.ByteToImage(profile.iris?.left);
                    enrollmentDto.iris.left = profile.iris?.left;

                    pbLeftIris.Enabled = false;
                }

                if (profile.iris.right?.Length > 0)
                {
                    pbRightIris.Image = Utils.ByteToImage(profile.iris?.right);
                    enrollmentDto.iris.right = profile.iris?.right;

                    pbRightIris.Enabled = false;
                }

                if (pbLeftIris.Enabled == false && pbRightIris.Enabled == false)
                {
                    btnIrisCapture.Enabled = false;
                    btnResetIris.Enabled = false;
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            enrollmentDto.id = (!string.IsNullOrEmpty(ProfileLongId)) ? ProfileLongId : null;
            enrollmentDto.fullName = (!string.IsNullOrEmpty(tbFullName.Text)) ? tbFullName.Text : null;

            if (!string.IsNullOrEmpty(cmbGender.SelectedValue?.ToString()))
            {
                enrollmentDto.gender = Convert.ToInt32(cmbGender.SelectedValue?.ToString());
            }

            if (!string.IsNullOrEmpty(cmbProfileType.SelectedValue?.ToString()))
            {
                enrollmentDto.profileType = Convert.ToInt32(cmbProfileType.SelectedValue?.ToString());
            }

            if (string.IsNullOrEmpty(enrollmentDto.fullName) || enrollmentDto.gender == null || enrollmentDto.profileType == null)
            {
                CustomMessageBox.ShowMessage("RAB Profile Management", "Full name, gender and profile type is mandatory for enrollment");
                return;
            }

            bool value = ((ProfileManagementController)controller).SubmitProfileManagement(enrollmentDto);

            if (value)
            {
                ClearEnrollmentPage(0);
            }
        }

        private void ClearEnrollmentPage(int flag)
        {
            if (flag == 0)
            {
                tbProfileId.Text = null;
            }
            ProfileLongId = null;
            tbFullName.Text = null;
            cmbGender.SelectedIndex = -1;
            cmbProfileType.SelectedIndex = -1;

            btnResetPhoto_Click(null, null);
            btnResetFp_Click(null, null);
            btnResetIris_Click(null, null);

            // Basic
            tbFullName.Enabled = true;
            cmbGender.Enabled = true;
            cmbProfileType.Enabled = true;

            // Photo
            pbPhoto.Enabled = true;
            btnPhotoCapture.Enabled = true;
            btnResetPhoto.Enabled = true;

            // Fingerprint
            pbRT.Enabled = true;
            pbRI.Enabled = true;
            pbRM.Enabled = true;
            pbRR.Enabled = true;
            pbRI.Enabled = true;

            pbLT.Enabled = true;
            pbLI.Enabled = true;
            pbLM.Enabled = true;
            pbLR.Enabled = true;
            pbLI.Enabled = true;

            btnFpCapture.Enabled = true;
            btnResetFp.Enabled = true;

            // Iris
            pbLeftIris.Enabled = true;
            pbRightIris.Enabled = true;
            btnIrisCapture.Enabled = true;
            btnResetIris.Enabled = true;
        }
    }
}
