using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ISTL.COMMON;
using ISTL.MODELS.DTO.Lookup;
using ISTL.RAB.Entity;
using ISTL.RAB.Controllers;
using ISTL.MODELS.DTO.Webcam;
using ISTL.MODELS.DTO.Iris;
using ISTL.MODELS.DTO.Fingerprint;
using ISTL.MODELS.DTO.Enrollment;
using ISTL.COMMON.Common;
using System.IO;

namespace ISTL.RAB.View
{
    public partial class MatchUserControl : ViewUserControl
    {
        public FingerprintData fingerprintData = new FingerprintData();
        public WebcamData camData = new WebcamData();
        public IrisData irisData = new IrisData();

        public MatchUserControl()
        {
            InitializeComponent();
            //pictureBoxPhoto.Image = 
        }
        
        public void HideNotification()
        {
            this.MatchByNotification = string.Empty;
            this.PhotoNotification = string.Empty;
            this.FingerNotification = string.Empty;
            this.IrisNotification = string.Empty;
        }
        public void ClearFields()
        {
            pictureBoxPhoto.Image = null;
            pictureBoxIrisLeft.Image = null;
            pictureBoxIrisRight.Image = null;
            pictureBoxLI.Image = null;
            pictureBoxLM.Image = null;
            pictureBoxLR.Image = null;
            pictureBoxLL.Image = null;
            pictureBoxLT.Image = null;
            pictureBoxRI.Image = null;
            pictureBoxRM.Image = null;
            pictureBoxRR.Image = null;
            pictureBoxRL.Image = null;
            pictureBoxRT.Image = null;

            fingerprintData = new FingerprintData();
            camData = new WebcamData();
            irisData = new IrisData();

        }

        public void EnableDisableButtons()
        {
            btnPhoto.Enabled = false;
            btnFP.Enabled = false;
            btnIris.Enabled = false;

            if (radioPhoto.Checked) btnPhoto.Enabled = true;
            if (radioFP.Checked) btnFP.Enabled = true;
            if (radioIris.Checked) btnIris.Enabled = true;
        }

        public void InitUserControl()
        {
            btnPhoto.Enabled = false;
            btnFP.Enabled = false;
            btnIris.Enabled = false;

            radioPhoto.Checked = true;
            radioFP.Checked = false;
            radioIris.Checked = false;

            HideNotification();
            EnableDisableButtons();
            ClearFields();
        }

        #region Getters & Setters   
        public RadioButton MatchByPhoto
        {
            get { return this.radioPhoto; }
            set { this.radioPhoto = value; }
        }

        public RadioButton MatchByFP
        {
            get { return this.radioFP; }
            set { this.radioFP = value; }
        }
        public RadioButton MatchByIris
        {
            get { return this.radioIris; }
            set { this.radioIris = value; }
        }
        public Image Photo
        {
            get { return this.pictureBoxPhoto.Image; }
            set { this.pictureBoxPhoto.Image = value; }
        }
        public Image LeftThumb
        {
            get { return this.pictureBoxLT.Image; }
            set { this.pictureBoxLT.Image = value; }
        }
        public Image LeftIndex
        {
            get { return this.pictureBoxLI.Image; }
            set { this.pictureBoxLI.Image = value; }
        }
        public Image LeftMiddle
        {
            get { return this.pictureBoxLM.Image; }
            set { this.pictureBoxLM.Image = value; }
        }
        public Image LeftRing
        {
            get { return this.pictureBoxLR.Image; }
            set { this.pictureBoxLR.Image = value; }
        }
        public Image LeftSmall
        {
            get { return this.pictureBoxLL.Image; }
            set { this.pictureBoxLL.Image = value; }
        }
        public Image RightThumb
        {
            get { return this.pictureBoxRT.Image; }
            set { this.pictureBoxRT.Image = value; }
        }
        public Image RightIndex
        {
            get { return this.pictureBoxRI.Image; }
            set { this.pictureBoxRI.Image = value; }
        }
        public Image RightMiddle
        {
            get { return this.pictureBoxRM.Image; }
            set { this.pictureBoxRM.Image = value; }
        }
        public Image RightRing
        {
            get { return this.pictureBoxRR.Image; }
            set { this.pictureBoxRR.Image = value; }
        }
        public Image RightSmall
        {
            get { return this.pictureBoxRL.Image; }
            set { this.pictureBoxRL.Image = value; }
        }
        public Image IrisLeft
        {
            get { return this.pictureBoxIrisLeft.Image; }
            set { this.pictureBoxIrisLeft.Image = value; }
        }
        public Image IrisRight
        {
            get { return this.pictureBoxIrisRight.Image; }
            set { this.pictureBoxIrisRight.Image = value; }
        }
        
        public string MatchByNotification
        {
            get
            {
                return lblMatchByNotification.Text;
            }
            set
            {
                this.lblMatchByNotification.Text = value;
            }
        }        
        public string PhotoNotification
        {
            get
            {
                return lblPhotoNotification.Text;
            }
            set
            {
                this.lblPhotoNotification.Text = value;
            }
        }
        public string FingerNotification
        {
            get
            {
                return lblFingerNotification.Text;
            }
            set
            {
                this.lblFingerNotification.Text = value;
            }
        }
        public string IrisNotification
        {
            get
            {
                return lblIrisNotification.Text;
            }
            set
            {
                this.lblIrisNotification.Text = value;
            }
        }

        public DialogResult OpenFile()
        {
            DialogResult result = this.openFileDialog.ShowDialog();
            return result;
        }

        public string GetFileName()
        {
            return this.openFileDialog.FileName;
        }
        #endregion

        private Image WsqToImage(byte[] wsq)
        {
            if (wsq == null)
            {
                return null;
            }
            try
            {
                int width = 0;
                int height = 0;
                int depth = 0;
                int dpi = 0;
                byte[] rawData = new byte[800 * 800];
                Image finalImage;
                CustomFpEngine.WsqToBmp(wsq, wsq.Length, rawData, ref width, ref height, ref depth, ref dpi);
                finalImage = Utils.ByteToImage(rawData);
                return finalImage;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #region Event(s)        
        private void btnPhoto_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = DialogResult.OK;
                var form = new WEBCAM.Cam();
                dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    this.camData = form.CamData;
                    pictureBoxPhoto.Image = Utils.ByteToImage(this.camData.CamImage);                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Photo capture module error. [" + ex.Message + "]");
            }
        }
        #endregion

        private void btnFP_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = DialogResult.OK;
                /*
                var form = new FP.FpForm();
                dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    this.fingerprintData = form.FpData;
                    pictureBoxRT.Image = Utils.ByteToImage(this.fingerprintData.FpRt);
                    pictureBoxRI.Image = Utils.ByteToImage(this.fingerprintData.FpRi);
                    pictureBoxRM.Image = Utils.ByteToImage(this.fingerprintData.FpRm);
                    pictureBoxRR.Image = Utils.ByteToImage(this.fingerprintData.FpRr);
                    pictureBoxRL.Image = Utils.ByteToImage(this.fingerprintData.FpRl);

                    pictureBoxLT.Image = Utils.ByteToImage(this.fingerprintData.FpLt);
                    pictureBoxLI.Image = Utils.ByteToImage(this.fingerprintData.FpLi);
                    pictureBoxLM.Image = Utils.ByteToImage(this.fingerprintData.FpLm);
                    pictureBoxLR.Image = Utils.ByteToImage(this.fingerprintData.FpLr);
                    pictureBoxLL.Image = Utils.ByteToImage(this.fingerprintData.FpLl);

                }
                */
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fingerprint capture error. [" + ex.Message + "]");
            }
        }

        private void btnIris_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = DialogResult.OK;
                /*
                var form = new IRIS.Iris();
                dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    this.irisData = form.IrisData;
                    pictureBoxIrisLeft.Image = Utils.ByteToImage(this.irisData.LeftIris);
                    pictureBoxIrisRight.Image = Utils.ByteToImage(this.irisData.RightIris);
                }
                */
            }
            catch (Exception ex)
            {
                MessageBox.Show("Iris capture module error. [" + ex.Message + "]");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ((MatchController)controller).OnSavingOperation();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ((EnrollController)controller).OnCancelOperation();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearFields();
            InitUserControl();
        }

        private void radioPhoto_CheckedChanged(object sender, EventArgs e)
        {
            HideNotification();
            EnableDisableButtons();
//            ClearFields();
        }

        private void radioFP_CheckedChanged(object sender, EventArgs e)
        {
            HideNotification();
            EnableDisableButtons();
//            ClearFields();
        }

        private void radioIris_CheckedChanged(object sender, EventArgs e)
        {
            HideNotification();
            EnableDisableButtons();
//            ClearFields();
        }

        private void btnIdentify_Click(object sender, EventArgs e)
        {
            ((MatchController)controller).OnSavingOperation();
        }

        private void pictureBoxPhoto_Click(object sender, EventArgs e)
        {
            TakePhoto();
        }

        public void TakePhoto()
        {
            Image img = GetImage();
            if (img == null) return;

            pictureBoxPhoto.Image = img;
            this.camData.CamImage = Utils.ImageToByte(img);
        }

        private Image GetImage()
        {
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                return Image.FromFile(openFileDialog.FileName);
            }
            return null;
        }

        private Image GetImageFromFP()
        {
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                byte[] buffer = File.ReadAllBytes(openFileDialog.FileName);

                return WsqToImage(buffer);
            }
            return null;
        }

        private void pictureBoxRT_Click(object sender, EventArgs e)
        {
            TakeFP(pictureBoxRT);
        }

        public void TakeFP(PictureBox pictureBox)
        {
            //Image img = GetImageFromFP();

            Image img = null;
            byte[] wsqBuffer = null;

            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                wsqBuffer = File.ReadAllBytes(openFileDialog.FileName);

                img = WsqToImage(wsqBuffer);
            }

            if (img == null || wsqBuffer == null) return;

            pictureBox.Image = img;

            // Right FP
            if (pictureBox.Name == "pictureBoxRT")
            {
                this.fingerprintData.WsqRt = wsqBuffer;
            }
            if (pictureBox.Name == "pictureBoxRI")
            {
                this.fingerprintData.WsqRi = wsqBuffer;
            }
            if (pictureBox.Name == "pictureBoxRM")
            {
                this.fingerprintData.WsqRm = wsqBuffer;
            }
            if (pictureBox.Name == "pictureBoxRR")
            {
                this.fingerprintData.WsqRr = wsqBuffer;
            }
            if (pictureBox.Name == "pictureBoxRL")
            {
                this.fingerprintData.WsqRl = wsqBuffer;
            }


            // Left FP
            if (pictureBox.Name == "pictureBoxLT")
            {
                this.fingerprintData.WsqLt = wsqBuffer;
            }
            if (pictureBox.Name == "pictureBoxLI")
            {
                this.fingerprintData.WsqLi = wsqBuffer;
            }
            if (pictureBox.Name == "pictureBoxLM")
            {
                this.fingerprintData.WsqLm = wsqBuffer;
            }
            if (pictureBox.Name == "pictureBoxLR")
            {
                this.fingerprintData.WsqLr = wsqBuffer;
            }
            if (pictureBox.Name == "pictureBoxLL")
            {
                this.fingerprintData.WsqLl = wsqBuffer;
            }
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

        private void pictureBoxLT_Click(object sender, EventArgs e)
        {
            TakeFP(pictureBoxLT);
        }

        private void pictureBoxLI_Click(object sender, EventArgs e)
        {
            TakeFP(pictureBoxLI);
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

        private void pictureBoxIrisRight_Click(object sender, EventArgs e)
        {
            TakeIris(pictureBoxIrisRight);
        }

        private void pictureBoxIrisLeft_Click(object sender, EventArgs e)
        {
            TakeIris(pictureBoxIrisLeft);
        }

        public void TakeIris(PictureBox pictureBox)
        {
            Image img = GetImage();
            if (img == null) return;

            pictureBox.Image = img;


            if (pictureBox.Name == "pictureBoxIrisLeft")
            {
                this.irisData.LeftIris = Utils.ImageToByte(img);
            }
            if (pictureBox.Name == "pictureBoxIrisRight")
            {
                this.irisData.RightIris = Utils.ImageToByte(img);
            }
        }
    }
}
