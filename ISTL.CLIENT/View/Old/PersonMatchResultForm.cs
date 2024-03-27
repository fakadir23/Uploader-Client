using ISTL.COMMON;
using ISTL.COMMON.Common;
using ISTL.MODELS.DTO.Enrollment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISTL.RAB.View
{
    public partial class PersonMatchResultForm : ViewUserControl
    {
        public PersonMatchResultForm()
        {
            InitializeComponent();
        }
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
        public string Occupation
        {
            get { return this.tbOccupation.Text; }
            set { this.tbOccupation.Text = value; }
        }
        //public string Gender
        //{
        //    get { return this.tbGender.Text; }
        //    set { this.tbGender.Text = value; }
        //}
        //public string DOB
        //{
        //    get { return this.tbDOB.Text; }
        //    set { this.tbDOB.Text = value; }
        //}
        public int Position = 0;
        public int TotalMatchCount = 0;
        public List<PersonDataDto> passportDataList;
        public void SetMatchResult(int position)
        {
            if (passportDataList == null || passportDataList.Count == 0)
            {
                //btnMatch.Hide();
                //btnNotMatch.Hide();
                lblMatchFoundFlag.Text = "MATCH NOT FOUND";
                lblMatchFoundFlag.ForeColor = Color.Red;
                //lblMatchPercentage.Text = "";
                btnNext.Hide();
                btnPrevious.Hide();

                return;
            }

            lblMatchFoundFlag.Text = "MATCH FOUND";
            lblMatchFoundFlag.ForeColor = Color.Green;

            this.lblNumberOfMatches.Text = "Number of Matches Found: " + TotalMatchCount;
            //this.tabPage1.Text = "Match " + (Position + 1);

            PersonDataDto dto = passportDataList[position];

            //if (dto.matchScore != null)
            //{
              //  lblMatchPercentage.Text = "Match Score: "+ dto.matchScore;
            //}

            //tbNIDmatch1.Text = dto.nationalId;
            //tbFullNameMatch1.Text = dto.fullName;
            //tbDOBMatch1.Text = dto.dateOfBirth;
            //tbGenderMatch1.Text = dto.gender;
            //tbNickNameMatch1.Text = dto.nickName;
            //tbCriminalNameMatch1.Text = dto.alias;
            //tbPhoneMatch1.Text = dto.phone;
            //tbOccupationMatch1.Text = dto.occupation;

            pictureBoxMatch1.Image = GraphicsManager.GetInstance().ByteArrayToImage(dto?.personBiometric?.photo);
        }

        public void SetMasterData(PersonDataDto obj)
        {
            //tbRefNo.Text = obj.referenceNumber;

            //if (obj.fullName != null)
            //{
            //    this.FullName = obj.fullName;
            //    this.NickName = obj.nickName;
            //    this.CriminalName = obj.alias;
            //    this.DOB = obj.dateOfBirth;
            //    this.tbGender.Text = obj.gender;
            //    this.Occupation = obj.occupation;
            //    this.Phone = obj.phone;
            //    this.NID = obj.nationalId;
            //}

            //else
            //{
            //    TextBox[] MasterTextBoxList = { tbFullName, tbNickName, tbCriminalName,
            //    tbDOB, tbNID, tbOccupation, tbPhone, tbGender };


            //    Label[] MasterLabelList = {lblFullName, lblNickName, lblCriminalName,
            //    lblDOB, lblNID, lblOccupation, lblPhone, lblGender };

            //    for (int i=0; i< MasterTextBoxList.Length; i++)
            //    {
            //        MasterTextBoxList[i].Hide();
            //        MasterLabelList[i].Hide();
            //    }
            //}

            //this.pictureBoxPhoto.Image = GraphicsManager.GetInstance().ByteArrayToImage(obj?.personBiometric?.photo);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (Position < (TotalMatchCount-1))
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

        private void panelMatch1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
