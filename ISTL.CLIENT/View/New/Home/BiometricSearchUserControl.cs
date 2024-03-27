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
using ISTL.RAB.Controllers.New.Home;
using ISTL.MODELS.DTO.Fingerprint;
using ISTL.MODELS.DTO.Webcam;
using ISTL.MODELS.DTO.Iris;
using System.IO;
using System.Configuration;
using ISTL.COMMON.Common;
using ISTL.RAB.View.New.Enrollment.BiometricInformation;
using ISTL.RAB.Controllers;
using ISTL.MODELS.Request.New;
using ISTL.RAB.Entity;
using NLog;

namespace ISTL.RAB.View.New
{
    public partial class BiometricSearchUserControl : ViewUserControl
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        public FingerprintData fingerprintData = new FingerprintData();
        public WebcamData camData = new WebcamData();
        public IrisData irisData = new IrisData();
        public GetBECidentifyRequest getBECidentifyRequest = new GetBECidentifyRequest();
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

        public BiometricSearchUserControl()
        {
            InitializeComponent();
        }

        public void TabToShow()
        {
            int tabToShow = ((BiometricSearchController)controller).CheckSearchCriteria();
            if (tabToShow == 1)
            {
                tabControl1.TabPages.Remove(tabPage4);
                tabControl1.TabPages.Remove(tabPage5);
                tabControl1.TabPages.Remove(tabPageJailFP);
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
                tabControl1.TabPages.Remove(tabPageJailFP);
                tabControl1.TabPages.Remove(tabPageJailIris);

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
        }

        public void TakePhoto()
        {
            Image img = GetImage();
            if (img == null) return;

            btnPhotoCapture.Text = "Re-Capture Photo";

            int maxPhotoSize = Convert.ToInt32(ConfigurationManager.AppSettings["PhotoSizeInKB"].ToString());
            if (GraphicsManager.GetInstance().ImageToByteArray(img).Length > maxPhotoSize)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Photo size is too big. Size should not exceed " + (maxPhotoSize / (1024*1000)) + " MB");
                return;
            }
            pictureBoxPhoto.Image = img;
            this.camData.CamImage = Utils.ImageToByte(img);
        }

        private Image GetImage()
        {
            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.bmp;)|" +
                "*.jpg; *.jpeg; *.png; *.bmp;";
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

        private void pictureBoxPhoto_Click(object sender, EventArgs e)
        {
            TakePhoto();
        }

        public void TakeFP(PictureBox pictureBox)
        {
            //Image img = GetImage();
            //if (img == null) return;

            //pictureBox.Image = img;
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

            btnFpCapture.Text = "Re-Capture Fingerprint";

            int maxFPSize = Convert.ToInt32(ConfigurationManager.AppSettings["FPSizeInKB"].ToString());
            if (GraphicsManager.GetInstance().ImageToByteArray(img).Length > maxFPSize)
            {
                //CustomMessageBox.ShowMessage("SNSOP TOOLS", "Fingerprint photo size is too big. Size should not exceed " + (maxFPSize / (1024*1000)) + " MB");
                //return;
                //DialogResult dr = MessageBoxController.ShowQuestionYesNo("RAB CDMS", "The size of the selected WSQ exceeds 30KB. Do you wish to continue?");
                //if (dr == DialogResult.No)
                //{
                //    return;
                //}
            }

            pictureBox.Image = img;

            // Right FP
            if (pictureBox.Name == "pictureBoxRT")  this.fingerprintData.WsqRt = wsqBuffer;
            if (pictureBox.Name == "pictureBoxRI")  this.fingerprintData.WsqRi = wsqBuffer;
            if (pictureBox.Name == "pictureBoxRM")  this.fingerprintData.WsqRm = wsqBuffer;
            if (pictureBox.Name == "pictureBoxRR")  this.fingerprintData.WsqRr = wsqBuffer;
            if (pictureBox.Name == "pictureBoxRL")  this.fingerprintData.WsqRl = wsqBuffer;

            // Left FP
            if (pictureBox.Name == "pictureBoxLT")  this.fingerprintData.WsqLt = wsqBuffer;
            if (pictureBox.Name == "pictureBoxLI")  this.fingerprintData.WsqLi = wsqBuffer;
            if (pictureBox.Name == "pictureBoxLM")  this.fingerprintData.WsqLm = wsqBuffer;
            if (pictureBox.Name == "pictureBoxLR")  this.fingerprintData.WsqLr = wsqBuffer;
            if (pictureBox.Name == "pictureBoxLL")  this.fingerprintData.WsqLl = wsqBuffer;
        }

        private void pictureBoxLI_Click(object sender, EventArgs e)
        {
            TakeFP(pictureBoxLI);
        }

        private void pictureBoxLT_Click(object sender, EventArgs e)
        {
            TakeFP(pictureBoxLT);
        }

        private void pictureBoxLM_Click(object sender, EventArgs e)
        {
            TakeFP(pictureBoxLM);
        }

        private void pictureBoxLR_Click(object sender, EventArgs e)
        {
            TakeFP(pictureBoxLR);
        }

        private void pictureBoxLL_Click(object sender, EventArgs e)
        {
            TakeFP(pictureBoxLL);
        }

        private void pictureBoxRT_Click(object sender, EventArgs e)
        {
            TakeFP(pictureBoxRT);
        }

        private void pictureBoxRI_Click(object sender, EventArgs e)
        {
            TakeFP(pictureBoxRI);
        }

        private void pictureBoxRM_Click(object sender, EventArgs e)
        {
            TakeFP(pictureBoxRM);
        }

        private void pictureBoxRR_Click(object sender, EventArgs e)
        {
            TakeFP(pictureBoxRR);
        }

        private void pictureBoxRL_Click(object sender, EventArgs e)
        {
            TakeFP(pictureBoxRL);
        }

        private void pictureBoxLeftIris_Click(object sender, EventArgs e)
        {
            TakeIris(pictureBoxLeftIris);
        }

        private void pictureBoxRightIris_Click(object sender, EventArgs e)
        {
            TakeIris(pictureBoxRightIris);
        }

        public void TakeIris(PictureBox pictureBox)
        {
            Image img = GetImage();
            if (img == null) return;

            btnIrisCapture.Text = "Re-Capture Iris";

            int maxIrisSize = Convert.ToInt32(ConfigurationManager.AppSettings["IrisSizeInKB"].ToString());
            if (GraphicsManager.GetInstance().ImageToByteArray(img).Length > maxIrisSize)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Iris photo size is too big. Size should not exceed " + (maxIrisSize / (1024*1000)) + " MB");
                return;
            }

            pictureBox.Image = img;
            if (pictureBox.Name == "pictureBoxLeftIris")
            {
                this.irisData.LeftIris = Utils.ImageToByte(img);
            }
            if (pictureBox.Name == "pictureBoxRightIris")
            {
                this.irisData.RightIris = Utils.ImageToByte(img);
            }
        }

        private void iconButtonIdentifyFace_Click(object sender, EventArgs e)
        {
            //((BiometricSearchController)controller).IdentifyByBiometric();
            if (camData.CamImage == null)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Photo is needed for search");
                return;
            }
            ((BiometricSearchController)controller).IdentifyByBiometric(2);
        }

        private void iconButtonIdentifyFingerprint_Click(object sender, EventArgs e)
        {
            //((BiometricSearchController)controller).IdentifyByBiometric();
            if (fingerprintData.WsqRt == null && fingerprintData.WsqRi == null && fingerprintData.WsqRm == null && fingerprintData.WsqRr == null && fingerprintData.WsqRl == null
                && fingerprintData.WsqLt == null && fingerprintData.WsqLi == null && fingerprintData.WsqLm == null && fingerprintData.WsqLr == null && fingerprintData.WsqLl == null)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Fingerprint is needed for search");
                return;
            }
            ((BiometricSearchController)controller).IdentifyByBiometric(1);
        }

        private void iconButtonIdentifyIris_Click(object sender, EventArgs e)
        {
            //((BiometricSearchController)controller).IdentifyByBiometric();
            if (irisData.LeftIris == null && irisData.RightIris == null)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Iris is needed for search");
                return;
            }
            ((BiometricSearchController)controller).IdentifyByBiometric(3);
        }

        private void btnIdentifyNID_Click(object sender, EventArgs e)
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
                ((BiometricSearchController)controller).SearchByNID(nidReq, dobReq);
            }
            else
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please input NID and Date Of Birth");
            }
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

        private void btnPhotoCapture_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = DialogResult.OK;
                var form = new ImageCaptureDialogForm();
                dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    this.pictureBoxPhoto.Image = Utils.ByteToImage(form.CamData.CamImage);
                    this.camData.CamImage = form.CamData.CamImage;

                    if (form.CamData.CamImage != null) btnPhotoCapture.Text = "Re-Capture Photo";
                }
            }
            catch (Exception)
            {                
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an unexpected error while capturing photo. Please contact with your Administrator.");
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
                    this.pictureBoxRT.Image = Utils.ByteToImage(form.FingerData.FpRt);
                    this.pictureBoxRI.Image = Utils.ByteToImage(form.FingerData.FpRi);
                    this.pictureBoxRM.Image = Utils.ByteToImage(form.FingerData.FpRm);
                    this.pictureBoxRR.Image = Utils.ByteToImage(form.FingerData.FpRr);
                    this.pictureBoxRL.Image = Utils.ByteToImage(form.FingerData.FpRl);

                    this.pictureBoxLT.Image = Utils.ByteToImage(form.FingerData.FpLt);
                    this.pictureBoxLI.Image = Utils.ByteToImage(form.FingerData.FpLi);
                    this.pictureBoxLM.Image = Utils.ByteToImage(form.FingerData.FpLm);
                    this.pictureBoxLR.Image = Utils.ByteToImage(form.FingerData.FpLr);
                    this.pictureBoxLL.Image = Utils.ByteToImage(form.FingerData.FpLl);

                    this.fingerprintData.WsqRt = form.FingerData.WsqRt;
                    this.fingerprintData.WsqRi = form.FingerData.WsqRi;
                    this.fingerprintData.WsqRm = form.FingerData.WsqRm;
                    this.fingerprintData.WsqRr = form.FingerData.WsqRr;
                    this.fingerprintData.WsqRl = form.FingerData.WsqRl;

                    // Left FP
                    this.fingerprintData.WsqLt = form.FingerData.WsqLt;
                    this.fingerprintData.WsqLi = form.FingerData.WsqLi;
                    this.fingerprintData.WsqLm = form.FingerData.WsqLm;
                    this.fingerprintData.WsqLr = form.FingerData.WsqLr;
                    this.fingerprintData.WsqLl = form.FingerData.WsqLl;

                    if (fingerprintData.WsqRt != null || fingerprintData.WsqRi != null || fingerprintData.WsqRm != null
                        || fingerprintData.WsqRr != null || fingerprintData.WsqRl != null || fingerprintData.WsqLt != null
                            || fingerprintData.WsqLi != null || fingerprintData.WsqLm != null || fingerprintData.WsqLr != null
                            || fingerprintData.WsqLl != null)
                    {
                        btnFpCapture.Text = "Re-Capture Fingerprint";
                    }
                    else
                    {
                        btnFpCapture.Text = "Capture Fingerprint";
                    }
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an unexpected error while capturing fingerprint. Please contact with your Administrator.");
            }
        }

        private void btnIrisCapture_Click(object sender, EventArgs e)
        {
            DialogResult dr = DialogResult.OK;
            try
            {
                var form = new IrisCaptureDialogForm();
                dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    this.pictureBoxLeftIris.Image = Utils.ByteToImage(form.irisData.LeftIris);
                    this.pictureBoxRightIris.Image = Utils.ByteToImage(form.irisData.RightIris);

                    this.irisData.RightIris = form.irisData.RightIris;
                    this.irisData.LeftIris = form.irisData.LeftIris;

                    if (form.irisData.RightIris != null || form.irisData.LeftIris != null) btnIrisCapture.Text = "Re-Capture Iris";

                }
            }
            catch (Exception)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an unexpected error while capturing iris. Please contact with your Administrator.");
            }
        }

        private void btnResetPhoto_Click(object sender, EventArgs e)
        {
            pictureBoxPhoto.Image = null;
            camData.CamImage = null;
        }

        private void btnResetFp_Click(object sender, EventArgs e)
        {
            pictureBoxRT.Image = null;
            pictureBoxRI.Image = null;
            pictureBoxRM.Image = null;
            pictureBoxRR.Image = null;
            pictureBoxRL.Image = null;
            pictureBoxLT.Image = null;
            pictureBoxLI.Image = null;
            pictureBoxLM.Image = null;
            pictureBoxLR.Image = null;
            pictureBoxLL.Image = null;

            this.fingerprintData = new FingerprintData();
        }

        private void btnResetIris_Click(object sender, EventArgs e)
        {
            pictureBoxLeftIris.Image = null;
            pictureBoxRightIris.Image = null;

            this.irisData = new IrisData();
        }

        private void btnResetNID_Click(object sender, EventArgs e)
        {
            tbNID.Text = null;
            tbDOBDay.Text = null;
            tbDOBMonth.Text = null;
            tbDOBYear.Text = null;
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
                    this.pbNidRT.Image = Utils.ByteToImage(form.FingerData.FpRt);
                    this.pbNidRI.Image = Utils.ByteToImage(form.FingerData.FpRi);
                    this.pbNidRM.Image = Utils.ByteToImage(form.FingerData.FpRm);
                    this.pbNidRR.Image = Utils.ByteToImage(form.FingerData.FpRr);
                    this.pbNidRL.Image = Utils.ByteToImage(form.FingerData.FpRl);

                    this.pbNidLT.Image = Utils.ByteToImage(form.FingerData.FpLt);
                    this.pbNidLI.Image = Utils.ByteToImage(form.FingerData.FpLi);
                    this.pbNidLM.Image = Utils.ByteToImage(form.FingerData.FpLm);
                    this.pbNidLR.Image = Utils.ByteToImage(form.FingerData.FpLr);
                    this.pbNidLL.Image = Utils.ByteToImage(form.FingerData.FpLl);

                    //this.getBECidentifyRequest.wsqRt = form.FingerData.WsqRt;
                    //this.getBECidentifyRequest.wsqRi = form.FingerData.WsqRi;
                    //this.getBECidentifyRequest.wsqRm = form.FingerData.WsqRm;
                    //this.getBECidentifyRequest.wsqRr = form.FingerData.WsqRr;
                    //this.getBECidentifyRequest.wsqRl = form.FingerData.WsqRl;

                    //// Left FP
                    //this.getBECidentifyRequest.wsqLt = form.FingerData.WsqLt;
                    //this.getBECidentifyRequest.wsqLi = form.FingerData.WsqLi;
                    //this.getBECidentifyRequest.wsqLm = form.FingerData.WsqLm;
                    //this.getBECidentifyRequest.wsqLr = form.FingerData.WsqLr;
                    //this.getBECidentifyRequest.wsqLl = form.FingerData.WsqLl;

                    getBECidentifyRequest.li = form.FingerData.FpLi;
                    getBECidentifyRequest.lt = form.FingerData.FpLt;
                    getBECidentifyRequest.rt = form.FingerData.FpRt;
                    getBECidentifyRequest.ri = form.FingerData.FpRi;

                    if (form.FingerData.FpRt != null || form.FingerData.FpRi != null || form.FingerData.FpRm != null || form.FingerData.FpRr != null ||
                        form.FingerData.FpRl != null || form.FingerData.FpLt != null || form.FingerData.FpLi != null || form.FingerData.FpLm != null ||
                        form.FingerData.FpLr != null || form.FingerData.FpLl != null)
                    {
                        btnCaptureFPnidIdentify.Text = "Re-Capture Fingerprint";
                    }
                }
            }
            catch (Exception)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an unexpected error while capturing fingerprint. Please contact with your Administrator.");
            }
        }

        private void btnNidFPIdentify_Click(object sender, EventArgs e)
        {
            //((BiometricSearchController)controller).NidIdentifyByFP();
            ((BiometricSearchController)controller).OnIdentifyNIDByBiometric();
        }

        private void btnNIDIdentifyReset_Click(object sender, EventArgs e)
        {
            this.pbNidRT.Image = null;
            this.pbNidRI.Image = null;
            this.pbNidRM.Image = null;
            this.pbNidRR.Image = null;
            this.pbNidRL.Image = null;

            this.pbNidLT.Image = null;
            this.pbNidLI.Image = null;
            this.pbNidLM.Image = null;
            this.pbNidLR.Image = null;
            this.pbNidLL.Image = null;

            getBECidentifyRequest = new GetBECidentifyRequest();
        }

        public void TakeNidFPIdentify(PictureBox pictureBox)
        {            
            Image img = null;
            byte[] wsqBuffer = null;

            //openFileDialog.Filter = "Image Files(*.wsq)|" + "*.wsq";
            openFileDialog.Filter = "Image Files(*.jpg; .jpeg; .png; *.bmp;)|" +
                "*.jpg; .jpeg; .png; *.bmp;";

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

            btnCaptureFPnidIdentify.Text = "Re-Capture Fingerprint";

            pictureBox.Image = img;

            // Right FP
            //if (pictureBox.Name == "pbNidRT") this.getBECidentifyRequest.wsqRt = wsqBuffer;
            //if (pictureBox.Name == "pbNidRI") this.getBECidentifyRequest.wsqRi = wsqBuffer;
            //if (pictureBox.Name == "pbNidRM") this.getBECidentifyRequest.wsqRm = wsqBuffer;
            //if (pictureBox.Name == "pbNidRR") this.getBECidentifyRequest.wsqRr = wsqBuffer;
            //if (pictureBox.Name == "pbNidRL") this.getBECidentifyRequest.wsqRl = wsqBuffer;

            // Left FP
            //if (pictureBox.Name == "pbNidLT") this.getBECidentifyRequest.wsqLt = wsqBuffer;
            //if (pictureBox.Name == "pbNidLI") this.getBECidentifyRequest.wsqLi = wsqBuffer;
            //if (pictureBox.Name == "pbNidLM") this.getBECidentifyRequest.wsqLm = wsqBuffer;
            //if (pictureBox.Name == "pbNidLR") this.getBECidentifyRequest.wsqLr = wsqBuffer;
            //if (pictureBox.Name == "pbNidLL") this.getBECidentifyRequest.wsqLl = wsqBuffer;

            try
            {
                if (pictureBox.Name == "pbNidRT") this.getBECidentifyRequest.rt = Utils.ImageToByte(AppUtils.AppUtils.WsqToImage(wsqBuffer));
                if (pictureBox.Name == "pbNidRI") this.getBECidentifyRequest.ri = Utils.ImageToByte(AppUtils.AppUtils.WsqToImage(wsqBuffer));
                if (pictureBox.Name == "pbNidLT") this.getBECidentifyRequest.lt = Utils.ImageToByte(AppUtils.AppUtils.WsqToImage(wsqBuffer));
                if (pictureBox.Name == "pbNidLI") this.getBECidentifyRequest.li = Utils.ImageToByte(AppUtils.AppUtils.WsqToImage(wsqBuffer));
            }
            catch(Exception x)
            {
                logger.Error("Error when trying to convert wsq to image to byte");
            }
        }

        private void pbNidRT_Click(object sender, EventArgs e)
        {
            TakeNidFPIdentify(pbNidRT);
        }
        private void pbNidRI_Click(object sender, EventArgs e)
        {
            TakeNidFPIdentify(pbNidRI);
        }
        private void pbNidRM_Click(object sender, EventArgs e)
        {
            TakeNidFPIdentify(pbNidRM);
        }
        private void pbNidRR_Click(object sender, EventArgs e)
        {
            TakeNidFPIdentify(pbNidRR);
        }
        private void pbNidRL_Click(object sender, EventArgs e)
        {
            TakeNidFPIdentify(pbNidRL);
        }
        private void pbNidLT_Click(object sender, EventArgs e)
        {
            TakeNidFPIdentify(pbNidLT);
        }
        private void pbNidLI_Click(object sender, EventArgs e)
        {
            TakeNidFPIdentify(pbNidLI);
        }
        private void pbNidLM_Click(object sender, EventArgs e)
        {
            TakeNidFPIdentify(pbNidLM);
        }
        private void pbNidLR_Click(object sender, EventArgs e)
        {
            TakeNidFPIdentify(pbNidLR);
        }
        private void pbNidLL_Click(object sender, EventArgs e)
        {
            TakeNidFPIdentify(pbNidLL);
        }

        private void btnCaptFPJail_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new FingerprintCaptureDialogForm();
                DialogResult dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    this.pbJailRT.Image = Utils.ByteToImage(form.FingerData.FpRt);
                    this.pbJailRI.Image = Utils.ByteToImage(form.FingerData.FpRi);
                    this.pbJailRM.Image = Utils.ByteToImage(form.FingerData.FpRm);
                    this.pbJailRR.Image = Utils.ByteToImage(form.FingerData.FpRr);
                    this.pbJailRL.Image = Utils.ByteToImage(form.FingerData.FpRl);

                    this.pbJailLT.Image = Utils.ByteToImage(form.FingerData.FpLt);
                    this.pbJailLI.Image = Utils.ByteToImage(form.FingerData.FpLi);
                    this.pbJailLM.Image = Utils.ByteToImage(form.FingerData.FpLm);
                    this.pbJailLR.Image = Utils.ByteToImage(form.FingerData.FpLr);
                    this.pbJailLL.Image = Utils.ByteToImage(form.FingerData.FpLl);

                    this.fingerprintData.WsqRt = form.FingerData.WsqRt;
                    this.fingerprintData.WsqRi = form.FingerData.WsqRi;
                    this.fingerprintData.WsqRm = form.FingerData.WsqRm;
                    this.fingerprintData.WsqRr = form.FingerData.WsqRr;
                    this.fingerprintData.WsqRl = form.FingerData.WsqRl;

                    // Left FP
                    this.fingerprintData.WsqLt = form.FingerData.WsqLt;
                    this.fingerprintData.WsqLi = form.FingerData.WsqLi;
                    this.fingerprintData.WsqLm = form.FingerData.WsqLm;
                    this.fingerprintData.WsqLr = form.FingerData.WsqLr;
                    this.fingerprintData.WsqLl = form.FingerData.WsqLl;

                    if (fingerprintData.WsqRt != null || fingerprintData.WsqRi != null || fingerprintData.WsqRm != null
                        || fingerprintData.WsqRr != null || fingerprintData.WsqRl != null || fingerprintData.WsqLt != null
                            || fingerprintData.WsqLi != null || fingerprintData.WsqLm != null || fingerprintData.WsqLr != null
                            || fingerprintData.WsqLl != null)
                    {
                        btnCaptFPJail.Text = "Re-Capture Fingerprint";
                    }
                }
            }
            catch (Exception)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an unexpected error while capturing fingerprint. Please contact with your Administrator.");
            }
        }

        private void btnJailFpIdentify_Click(object sender, EventArgs e)
        {
            if (fingerprintData.WsqRt == null && fingerprintData.WsqRi == null && fingerprintData.WsqRm == null && fingerprintData.WsqRr == null && fingerprintData.WsqRl == null
                && fingerprintData.WsqLt == null && fingerprintData.WsqLi == null && fingerprintData.WsqLm == null && fingerprintData.WsqLr == null && fingerprintData.WsqLl == null)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Fingerprint is needed for search");
                return;
            }
            ((BiometricSearchController)controller).JailIdentifyByBiometric(1);
        }

        private void btnJailFPreset_Click(object sender, EventArgs e)
        {
            this.pbJailRT.Image = null;
            this.pbJailRI.Image = null;
            this.pbJailRM.Image = null;
            this.pbJailRR.Image = null;
            this.pbJailRL.Image = null;

            this.pbJailLT.Image = null;
            this.pbJailLI.Image = null;
            this.pbJailLM.Image = null;
            this.pbJailLR.Image = null;
            this.pbJailLL.Image = null;

            this.fingerprintData = new FingerprintData();
        }

        private void btnCaptIrisJail_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new IrisCaptureDialogForm();
                DialogResult dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    this.pbJailLeftIris.Image = Utils.ByteToImage(form.irisData.LeftIris);
                    this.pbJailRightIris.Image = Utils.ByteToImage(form.irisData.RightIris);

                    this.irisData.RightIris = form.irisData.RightIris;
                    this.irisData.LeftIris = form.irisData.LeftIris;

                    if (form.irisData.RightIris != null || form.irisData.LeftIris != null) btnCaptIrisJail.Text = "Re-Capture Iris";
                }
            }
            catch (Exception)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an unexpected error while capturing iris. Please contact with your Administrator.");
            }
        }

        private void btnJailIrisIdentify_Click(object sender, EventArgs e)
        {
            if (irisData.LeftIris == null && irisData.RightIris == null)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Iris is needed for search");
                return;
            }
            ((BiometricSearchController)controller).JailIdentifyByBiometric(3);
        }

        private void btnJailIrisReset_Click(object sender, EventArgs e)
        {
            this.pbJailLeftIris.Image = null;
            this.pbJailRightIris.Image = null;

            this.irisData = new IrisData();
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

            btnCaptFPJail.Text = "Re-Capture Fingerprint";

            pictureBox.Image = img;

            // Right FP
            if (pictureBox.Name == "pbJailRT") this.fingerprintData.WsqRt = wsqBuffer;
            if (pictureBox.Name == "pbJailRI") this.fingerprintData.WsqRi = wsqBuffer;
            if (pictureBox.Name == "pbJailRM") this.fingerprintData.WsqRm = wsqBuffer;
            if (pictureBox.Name == "pbJailRR") this.fingerprintData.WsqRr = wsqBuffer;
            if (pictureBox.Name == "pbJailRL") this.fingerprintData.WsqRl = wsqBuffer;

            // Left FP
            if (pictureBox.Name == "pbJailLT") this.fingerprintData.WsqLt = wsqBuffer;
            if (pictureBox.Name == "pbJailLI") this.fingerprintData.WsqLi = wsqBuffer;
            if (pictureBox.Name == "pbJailLM") this.fingerprintData.WsqLm = wsqBuffer;
            if (pictureBox.Name == "pbJailLR") this.fingerprintData.WsqLr = wsqBuffer;
            if (pictureBox.Name == "pbJailLL") this.fingerprintData.WsqLl = wsqBuffer;
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
        private void pbJailRightIris_Click(object sender, EventArgs e)
        {
            TakeJailIris(pbJailRightIris);
        }
        private void pbJailLeftIris_Click(object sender, EventArgs e)
        {
            TakeJailIris(pbJailLeftIris);
        }
        public void TakeJailIris(PictureBox pictureBox)
        {
            Image img = GetImage();
            if (img == null) return;

            btnCaptIrisJail.Text = "Re-Capture Iris";

            int maxIrisSize = Convert.ToInt32(ConfigurationManager.AppSettings["IrisSizeInKB"].ToString());
            if (GraphicsManager.GetInstance().ImageToByteArray(img).Length > maxIrisSize)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Iris photo size is too big. Size should not exceed " + (maxIrisSize / (1024 * 1000)) + " MB");
                return;
            }

            pictureBox.Image = img;
            if (pictureBox.Name == "pbJailLeftIris")
            {
                this.irisData.LeftIris = Utils.ImageToByte(img);
            }
            if (pictureBox.Name == "pbJailRightIris")
            {
                this.irisData.RightIris = Utils.ImageToByte(img);
            }
        }
    }
}
