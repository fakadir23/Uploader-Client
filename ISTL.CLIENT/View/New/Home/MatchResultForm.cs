using ISTL.COMMON;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.MODELS.DTO.New.Enrollment.BECverify;
using ISTL.MODELS.Response.New;
using ISTL.RAB.ApiManager;
using ISTL.RAB.Controllers.New.Home;
using ISTL.RAB.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace ISTL.RAB.View.New.Home
{
    public partial class MatchResultForm : ViewForm
    {
        // Define the CS_DROPSHADOW constant
        private const int CS_DROPSHADOW = 0x00020000;

        // Override the CreateParams property
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        public string criminalId;
        public List<string> criminalIdList = new List<string>();
        public string referenceNo;
        public bool MatchedByNID;
        public int matchCount;
        public List<ProfileResponseDto> profileList = new List<ProfileResponseDto>();
        public List<int> matchScoreList = new List<int>();
        public int openedFromUI;
        public MatchResultForm()
        {
            InitializeComponent();
        }
        public string MatchedByCriteria
        {
            get { return lblMatchedBy.Text; }
            set { lblMatchedBy.Text = value; }
        }
        private void btnCDMSselect_Click(object sender, EventArgs e)
        {
            ((MatchResultController)controller).Select();
        }

        private void btnNIDselect_Click(object sender, EventArgs e)
        {
            ((MatchResultController)controller).NIDSelect();
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            if (MatchedByNID != true)
            {
                groupBox1.Location = new Point(300, 61);
                ((MatchResultController)controller).SetResultData();
                lblCDMSmatchCount.Text = "Number of matches found: " + matchCount;
            }
            else
            {
                groupBox1.Hide();
                groupBox2.Show();
                groupBox2.Location = new Point(350, 61);
                ((MatchResultController)controller).SetNIDResultData();
            }
        }

        public void ShowNIDMatchResults(GetBECdataResponse data)
        {
            lblMatchScoreNID.Text = null;
            tbNIDDob.Text = "N/A";
            tbNIDFullName.Text = "N/A";
            tbNIDNationalID.Text = "N/A";
            tbFatherName.Text = "N/A";
            pbNIDphoto.Image = null;
            tbMotherName.Text = "N/A";
            tbReligion.Text = "N/A";
            tbPresentAddress.Text = "N/A";
            tbPermanentAddress.Text = "N/A";

            btnNIDmatchPrev.Enabled = false;
            btnNIDmatchNext.Enabled = false;

            if (data != null)
            {
                tbNIDDob.Text = data.dateOfBirth;
                tbNIDFullName.Text = data.nameEn;
                tbNIDNationalID.Text = data.nationalId;
                tbFatherName.Text = data.father;
                if (!string.IsNullOrEmpty(data.pin)) tbNIDNationalID.Text += ", " + data.pin;
                tbMotherName.Text = data.mother;
                tbReligion.Text = data.religion;

                //var jsonPresentAddress = new JavaScriptSerializer().Deserialize<BECAddressDto>(data.presentAddress);
                //string PresentAddressValue = new JavaScriptSerializer().Serialize(data.presentAddress);
                //PresentAddressValue = PresentAddressValue.ToString()?.Replace("{", "");
                //PresentAddressValue = PresentAddressValue?.Replace("}", "");
                //PresentAddressValue = PresentAddressValue?.Replace('"', ' ');
                //tbPresentAddress.Text = PresentAddressValue;

                tbPresentAddress.Text = data.presentAddress?.homeOrHoldingNo + " " + data.presentAddress?.additionalMouzaOrMoholla + " " +
                    data.presentAddress?.villageOrRoad + " " + data.presentAddress?.additionalVillageOrRoad +
                    ", " + data.presentAddress?.unionOrWard + " " + data.presentAddress?.postOffice + " " +
                    data.presentAddress?.postalCode + ", " + data.presentAddress?.upozila + ", " +
                    data.presentAddress?.district + ", " + data.presentAddress?.division;

                //var jsonPermanentAddress = new JavaScriptSerializer().Deserialize<BECAddressDto>(data.permanentAddress);
                //string PermanentAddressValue = new JavaScriptSerializer().Serialize(data.permanentAddress);
                //PermanentAddressValue = PermanentAddressValue.ToString()?.Replace("{", "");
                //PermanentAddressValue = PermanentAddressValue?.Replace("}", "");
                //PermanentAddressValue = PermanentAddressValue?.Replace('"', ' ');
                //tbPermanentAddress.Text = PermanentAddressValue;

                tbPermanentAddress.Text = data.permanentAddress?.homeOrHoldingNo + " " + data.permanentAddress?.additionalMouzaOrMoholla + " " +
                    data.permanentAddress?.villageOrRoad + " " + data.permanentAddress?.additionalVillageOrRoad +
                    ", " + data.permanentAddress?.unionOrWard + " " + data.permanentAddress?.postOffice + " " +
                    data.permanentAddress?.postalCode + " " + data.permanentAddress?.upozila + ", " +
                    data.permanentAddress?.district + ", " + data.permanentAddress?.division;

                if (data.photo != null)
                {
                    try
                    {
                        pbNIDphoto.Image = Utils.ByteToImage(data.photo);
                    }
                    catch { }
                }
            }
        }

        public int NidFPmatchIndex = 0;
        public void ShowFPnidMatchResults(List<BECvoterInfoDto> list, int index)
        {
            if (list == null) return;

            lblNIDmatchCount.Text = "Number of matches found: " + list.Count;

            lblMatchScoreNID.Text = null;
            tbNIDDob.Text = "N/A";
            tbNIDFullName.Text = "N/A";
            tbNIDNationalID.Text = "N/A";
            tbFatherName.Text = "N/A";
            tbMotherName.Text = "N/A";
            tbReligion.Text = "N/A";
            tbPresentAddress.Text = "N/A";
            tbPermanentAddress.Text = "N/A";
            pbNIDphoto.Image = null;            

            ProcessingDialog.Run(delegate ()
            {
                Invoke((MethodInvoker)delegate
                {
                    lblMatchScoreNID.Text = "Match Score: " + list[index].score;
                    lblNIDMatchNo.Text = "Match-" + (index + 1);
                    tbNIDDob.Text = list[index].dob;
                    tbNIDFullName.Text = list[index].nameEn;
                    tbNIDNationalID.Text = list[index].nid;
                    tbFatherName.Text = list[index].father;
                    tbMotherName.Text = list[index].mother;
                    tbReligion.Text = list[index].religion;
                    tbPresentAddress.Text = list[index].presentAddress;
                    tbPermanentAddress.Text = list[index].permanentAddress;
                    
                    if (list[index].photo != null)
                    {
                        try
                        {
                            pbNIDphoto.Image = Utils.ByteToImage(list[index].photo);
                        }
                        catch { }
                    }
                });
            });
        }

        private void SetInitBioMatchFields()
        {
            lblMatchNo.Text = null;
            lblMatchScore.Text = null;
            pbCDMSphoto.Image = null;
            tbCDMSRefNo.Text = "N/A";
            tbCDMSUnit.Text = "N/A";
            tbCDMSSubUnit.Text = "N/A";
            tbCDMSFullName.Text = "N/A";
            tbCDMSDob.Text = "N/A";
            tbCDMSFatherName.Text = "N/A";
            tbCDMSCrimeType.Text = "N/A";
        }

        public void ShowMatchResults(List<ProfileResponseDto> list, int index)
        {
            ProcessingDialog.Run(delegate ()
            {
                Invoke((MethodInvoker)delegate
                {
                    if (index < list.Count)
                    {
                        if (index == 0) position = 0;
                        SetInitBioMatchFields();
                        lblMatchNo.Text = "Match-" + (index + 1);
                        if (index < matchScoreList.Count)
                        {
                            lblMatchScore.Text = "Match Score: " + matchScoreList[index].ToString();
                        }
                        if (!string.IsNullOrEmpty(list[index].photo))
                        {
                            byte[] photo = GetByteDataForStatic(list[index].photo, "photo");
                            if (photo != null)
                            {
                                pbCDMSphoto.Image = Utils.ByteToImage(photo);
                            }
                        }

                        tbCDMSRefNo.Text = list[index].referenceNo;
                        referenceNo = list[index].referenceNo;
                        criminalId = list[index].id;

                        if (list[index].unit != null)
                        {
                            if (list[index].unit >= 0)
                            {
                                string value = ((MatchResultController)controller).GetValueForLookup("name_en", "station", Convert.ToInt32(list[index].unit));
                                tbCDMSUnit.Text = value;
                            }
                        }

                        if (list[index].subUnit != null)
                        {
                            if (list[index].subUnit > 0)
                            {
                                string value = ((MatchResultController)controller).GetValueForLookup("name_en", "sub_station", Convert.ToInt32(list[index].subUnit));
                                tbCDMSSubUnit.Text = value;
                            }
                        }

                        tbCDMSFullName.Text = list[index].fullName;

                        if (!string.IsNullOrEmpty(list[index].dateOfBirth))
                        {
                            try
                            {
                                DateTime dt = DateTime.ParseExact(list[index].dateOfBirth, "yyyy-MM-ddTHH:mm:ss.fffK", System.Globalization.CultureInfo.InvariantCulture);
                                tbCDMSDob.Text = dt.ToString("dd-MM-yyyy");
                            }
                            catch { }
                        }

                        if (list[index].familys != null)
                        {
                            for (int i = 0; i < list[index].familys.Count; i++)
                            {
                                if (list[index].familys[i].relation == "father")
                                {
                                    tbCDMSFatherName.Text = list[index].familys[i].name;
                                    break;
                                }
                            }
                        }

                        //string crimeTypeName = null;
                        //ComboBoxItems.crimeType.TryGetValue(Convert.ToString(list[index].crimeInformation?.crimeType), out crimeTypeName);
                        //tbCDMSCrimeType.Text = crimeTypeName;

                        if (list[index].crimeInformation?.crimeType != null)
                        {
                            if (list[index].crimeInformation?.crimeType > 0)
                            {
                                string value = ((MatchResultController)controller).GetCrimeTypeValueForLookup
                                (Convert.ToInt32(list[index].crimeInformation?.crimeType));
                                tbCDMSCrimeType.Text = value;
                            }
                        }
                    }
                });
            });
        }
        private byte[] GetByteDataForStatic(string str, string attribute)
        {
            GetProfileDataByteResponse response = new EnrollmentApiManager().GetByteDataByFilePath(str);
            byte[] photo = null;
            if (response != null)
            {
                if (response.code == 200)
                {
                    if (attribute == "photo")
                    {
                        photo = response.file;
                    }
                }
            }
            return photo;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void iconBtnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private int position = 0;
        private void btnCDMSnext_Click(object sender, EventArgs e)
        {
            if (position < matchCount - 1) position += 1;
            ShowMatchResults(profileList, position);
        }

        private void btnCDMSprev_Click(object sender, EventArgs e)
        {
            if (position > 0) position -= 1;
            ShowMatchResults(profileList, position);
        }

        private void btnNIDmatchPrev_Click(object sender, EventArgs e)
        {
            ((MatchResultController)controller).GoToPrevNidFpMatch();
        }

        private void btnNIDmatchNext_Click(object sender, EventArgs e)
        {
            ((MatchResultController)controller).GoToNextNidFpMatch();
        }

        private void btnConfirmCDMSProfile_Click(object sender, EventArgs e)
        {
            ((MatchResultController)controller).ConfirmGetProfile();
        }

        private void btnGetCombinedReport_Click(object sender, EventArgs e)
        {
            if (criminalIdList != null)
            {
                ((MatchResultController)controller).GenerateCombinedReport();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
