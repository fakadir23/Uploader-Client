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

namespace ISTL.RAB.View
{
    public partial class EnrollUserControl : ViewUserControl
    {
        public FingerprintData fingerprintData = new FingerprintData();
        public WebcamData camData = new WebcamData();
        public IrisData irisData = new IrisData();

        public EnrollUserControl()
        {
            InitializeComponent();
            SetLocalizedValue();

            LoadGender();
            LoadOccupation();
            //cbTransType.SelectedIndex = 0;
        }

        private void LoadGender()
        {
            Dictionary<string, string> genderList = new Dictionary<string, string>();
            genderList.Add("Male", "Male");
            genderList.Add("Female", "Female");
            genderList.Add("Other", "Other");

            this.cmbGender.DataSource = new BindingSource(genderList, null);
            this.cmbGender.DisplayMember = "Value";
            this.cmbGender.ValueMember = "Key";
        }

        private void LoadOccupation()
        {
            List<string> occupationList = new List<string>();
            occupationList.Add("None");
            occupationList.Add("BDR, Retired person");
            occupationList.Add("Doctor");
            occupationList.Add("bdr");
            occupationList.Add("Teacher");
            occupationList.Add("Business");
            occupationList.Add("Housewife");
            occupationList.Add("Driver");
            occupationList.Add("BDR");
            occupationList.Add("Govt service");
            occupationList.Add("Engineer");
            occupationList.Add("Banker");
            occupationList.Add("Pvt service");
            occupationList.Add("Student");
            occupationList.Add("Farm worker");
            occupationList.Add("Farmer");
            occupationList.Add("Lawer");
            occupationList.Add("Daylabour");
            occupationList.Add("Other");

            this.cmbOccupation.DataSource = new BindingSource(occupationList, null);
        }

        private void SetLocalizedValue()
        {
            //this.lblFullName.Text = _translator.Translate(LanguageConstant.FirstNameEn) + ":";
            //this.lblNickNameEn.Text = _translator.Translate(LanguageConstant.MiddleNameEn) + ":";
            //this.lblCriminalNameEn.Text = _translator.Translate(LanguageConstant.LastNameEn) + ":";
            //this.lblNIDEn.Text = _translator.Translate(LanguageConstant.FirstNameLocal) + ":";
            //this.lblPhoneEn.Text = _translator.Translate(LanguageConstant.LastNameLocal) + ":";
            //this.lblNationality.Text = _translator.Translate(LanguageConstant.Nationality) + ":";
            //this.lblDateofBirth.Text = _translator.Translate(LanguageConstant.DateOfBirth) + ":";
            //this.lblOccupationEn.Text = _translator.Translate(LanguageConstant.PlaceOfBirth) + ":";
            //this.lblGender.Text = _translator.Translate(LanguageConstant.Gender) + ":";

            //Do the rest for enroll, manage and surveillance forms
        }

        public void HideNotification()
        {
            this.FullNameNotification = string.Empty;
            this.NickNameEnNotification = string.Empty;
            this.CriminalNameNotification = string.Empty;
            this.NIDNotification = string.Empty;
            this.PhoneNotification = string.Empty;
            //this.NationalityNotification = string.Empty;
            this.OccupationNotification = string.Empty;
            this.DateOfBirthNotification = string.Empty;
            this.GenderNotification = string.Empty;            
            this.PhotoNotification = string.Empty;
        }
        public void ClearText()
        {
            FullName = "";
            NickName = "";
            CriminalName = "";
            NID = "";
            Phone = "";
            Photo = null;
            Day = "";
            Month = "";
            Year = "";
            pictureBoxIrisLeft.Image = null;
            pictureBoxIrisRight.Image = null;
            pictureBoxLI.Image = null;
            pictureBoxLM.Image = null;
            pictureBoxLR.Image = null;
            pictureBoxLS.Image = null;
            pictureBoxLT.Image = null;
            pictureBoxRI.Image = null;
            pictureBoxRM.Image = null;
            pictureBoxRR.Image = null;
            pictureBoxRS.Image = null;
            pictureBoxRT.Image = null;
        }

        #region Getters & Setters
        public string FullName
        {
            get { return this.tbFullName.Text; }
            set { this.tbFullName.Text = value; }
        }
        public string NickName
        {
            get { return this.tbNickName.Text; }
            set { this.tbNickName.Text = value; }
        }
        public string CriminalName
        {
            get { return this.tbCriminalName.Text; }
            set { this.tbCriminalName.Text = value; }
        }
        public string NID
        {
            get { return this.tbNID.Text; }
            set { this.tbNID.Text = value; }
        }        
        public string Phone
        {
            get { return this.tbPhone.Text; }
            set { this.tbPhone.Text = value; }
        }        
        public string Day
        {
            get { return this.tbDay.Text; }
            set { this.tbDay.Text = value; }
        }
        public string Month
        {
            get { return this.tbMonth.Text; }
            set { this.tbMonth.Text = value; }
        }
        public string Year
        {
            get { return this.tbYear.Text; }
            set { this.tbYear.Text = value; }
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
            get { return this.pictureBoxLS.Image; }
            set { this.pictureBoxLS.Image = value; }
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
            get { return this.pictureBoxRS.Image; }
            set { this.pictureBoxRS.Image = value; }
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
        public string Occupation
        {
            get { return this.cmbOccupation.SelectedValue?.ToString(); }
            set { this.cmbOccupation.Text = value; }
        }
        public string Gender
        {
            get { return this.cmbGender.SelectedValue?.ToString(); }
            set { this.cmbGender.SelectedValue = value; }
        }
        /*
        public string TransactionType
        {
            get { return this.cbTransType.Text; }            
        }
        */
        public string FullNameNotification
        {
            get
            {
                return lblFullNameNotification.Text;
            }
            set
            {
                this.lblFullNameNotification.Text = value;
            }
        }
        public string NickNameEnNotification
        {
            get
            {
                return lblNickNameEnNotification.Text;
            }
            set
            {
                this.lblNickNameEnNotification.Text = value;
            }
        }
        public string CriminalNameNotification
        {
            get
            {
                return lblCriminalNameNotification.Text;
            }
            set
            {
                this.lblCriminalNameNotification.Text = value;
            }
        }
        public string NIDNotification
        {
            get
            {
                return lblNIDNotification.Text;
            }
            set
            {
                this.lblNIDNotification.Text = value;
            }
        }                       
        public string PhoneNotification
        {
            get
            {
                return lblPhoneNotification.Text;
            }
            set
            {
                this.lblPhoneNotification.Text = value;
            }
        }
        public string OccupationNotification
        {
            get
            {
                return lblOccupationNotification.Text;
            }
            set
            {
                this.lblOccupationNotification.Text = value;
            }
        }       
        public string DateOfBirthNotification
        {
            get
            {
                return lblDateOfBirthNotification.Text;
            }
            set
            {
                this.lblDateOfBirthNotification.Text = value;
            }
        }
        public string GenderNotification
        {
            get
            {
                return lblGengerNotification.Text;
            }
            set
            {
                this.lblGengerNotification.Text = value;
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

        public void SetPersonEnrollmentData(PersonDataDto obj)
        {
            this.FullName = obj.fullName;
            this.NickName = obj.nickName;
            this.CriminalName = obj.alias;
            this.Year = obj.dateOfBirth?.Substring(0,4);
            this.Month = obj.dateOfBirth?.Substring(5,2);
            this.Day = obj.dateOfBirth?.Substring(8,2);
            if (obj.gender != null)
            {
                this.cmbGender.SelectedValue = obj.gender;
            }
            this.Occupation = (!string.IsNullOrEmpty(obj.occupation)) ? obj.occupation : null;
            this.Phone = obj.phone;
            this.NID = obj.nationalId;

            this.pictureBoxPhoto.Image = GraphicsManager.GetInstance().
                ByteArrayToImage(obj?.personBiometric?.photo);

            this.pictureBoxIrisLeft.Image = GraphicsManager.GetInstance().
                ByteArrayToImage(obj?.personBiometric?.leftIris);
            this.pictureBoxIrisRight.Image = GraphicsManager.GetInstance().
                ByteArrayToImage(obj?.personBiometric?.rightIris);

            this.pictureBoxLT.Image = WsqToImage(obj?.personBiometric?.wsqLt);
            this.pictureBoxLI.Image = WsqToImage(obj?.personBiometric?.wsqLi);
            this.pictureBoxLM.Image = WsqToImage(obj?.personBiometric?.wsqLm);
            this.pictureBoxLR.Image = WsqToImage(obj?.personBiometric?.wsqLr);
            this.pictureBoxLS.Image = WsqToImage(obj?.personBiometric?.wsqLl);

            this.pictureBoxRT.Image = WsqToImage(obj?.personBiometric?.wsqRt);
            this.pictureBoxRI.Image = WsqToImage(obj?.personBiometric?.wsqRi);
            this.pictureBoxRM.Image = WsqToImage(obj?.personBiometric?.wsqRm);
            this.pictureBoxRR.Image = WsqToImage(obj?.personBiometric?.wsqRr);
            this.pictureBoxRS.Image = WsqToImage(obj?.personBiometric?.wsqRr);
        }

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
                MessageBox.Show("Photo capture module development is on progress. \nPress 'Ctrl + Alt + Click on Take Photo Button' to open and set mock photo.");
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
                    pictureBoxRS.Image = Utils.ByteToImage(this.fingerprintData.FpRl);

                    pictureBoxLT.Image = Utils.ByteToImage(this.fingerprintData.FpLt);
                    pictureBoxLI.Image = Utils.ByteToImage(this.fingerprintData.FpLi);
                    pictureBoxLM.Image = Utils.ByteToImage(this.fingerprintData.FpLm);
                    pictureBoxLR.Image = Utils.ByteToImage(this.fingerprintData.FpLr);
                    pictureBoxLS.Image = Utils.ByteToImage(this.fingerprintData.FpLl);

                }
                */
            }
            catch (Exception ex)
            {
                MessageBox.Show("Photo capture module development is on progress. \nPress 'Ctrl + Alt + Click on Take Photo Button' to open and set mock photo.");
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
                MessageBox.Show("Photo capture module development is on progress. \nPress 'Ctrl + Alt + Click on Take Photo Button' to open and set mock photo.");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ((EnrollController)controller).OnSavingOperation();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ((EnrollController)controller).OnCancelOperation();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearText();
        }
    }
}
