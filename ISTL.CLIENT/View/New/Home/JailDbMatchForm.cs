using ISTL.COMMON;
using ISTL.MODELS.DTO.New.JailDBBioMatch;
using ISTL.RAB.DbManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISTL.RAB.View.New.Home
{
    public partial class JailDbMatchForm : Form
    {
        // Drop shadow souce code, got from:
        // http://www.codeproject.com/Articles/19277/Let-Your-Form-Drop-a-Shadow

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
        private DbLookupManager dbLookupManager;
        public List<JailProfileDto> profileList { get; set; }
        public JailDbMatchForm()
        {
            InitializeComponent();
            dbLookupManager = new DbLookupManager();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (profileList?.Count > 1)
            {
                lblJailMatchCount.Text = "Number of Matches Found: " + profileList.Count;
                
                try
                {
                    profileList = profileList.OrderByDescending(x => x.score).ToList();
                }
                catch { }
            }
            ShowMatchedProfile(0);
        }

        private void SetInitBioMatchFields()
        {
            lblJailMatchNo.Text = null;
            lblJailMatchScore.Text = null;
            pbJailPhoto.Image = null;
            tbFullName.Text = "N/A";
            tbDob.Text = "N/A";
            tbNID.Text = "N/A";
            tbConvictNo.Text = "N/A";
            tbMobileNo.Text = "N/A";
            tbReligion.Text = "N/A";
            tbPresentAddress.Text = "N/A";
            tbPermanentAddress.Text = "N/A";
        }

        private void ShowMatchedProfile(int index)
        {
            if (index >= profileList.Count)
            {
                return;
            }

            SetInitBioMatchFields();

            ProcessingDialog.Run(delegate ()
            {
                Invoke((MethodInvoker)delegate
                {
                    lblJailMatchNo.Text = "Match-" + (index + 1);
                    lblJailMatchScore.Text = "Match Score: " + profileList[index].score;

                    tbFullName.Text = profileList[index].nameEn;
                    //tbDob.Text = profileList[index].dob;
                    if (!string.IsNullOrEmpty(profileList[index].dob))
                    {
                        try
                        {
                            DateTime dt = DateTime.ParseExact(profileList[index].dob, "yyyy-MM-ddTHH:mm:ss.fffK", System.Globalization.CultureInfo.InvariantCulture);
                            tbDob.Text = dt.ToString("dd-MM-yyyy");
                        }
                        catch (Exception x)
                        {
                            tbDob.Text = profileList[index].dob;
                        }
                    }
                    tbNID.Text = profileList[index].nid;
                    tbConvictNo.Text = profileList[index].utpConvictNo;

                    tbMobileNo.Text = profileList[index].mobileNo;
                    if (!string.IsNullOrEmpty(tbMobileNo.Text))
                    {
                        tbMobileNo.Text += ", ";
                    }
                    if (!string.IsNullOrEmpty(profileList[index].phone))
                    {
                        tbMobileNo.Text += profileList[index].phone;
                    }

                    tbReligion.Text = profileList[index].religionLookup;
                    if (profileList[index].photo != null)
                    {
                        try
                        {
                            pbJailPhoto.Image = Utils.ByteToImage(profileList[index].photo);
                        }
                        catch { }
                    }

                    if (profileList[index].presentAddress != null)
                    {
                        tbPresentAddress.Text = null;
                        if (!string.IsNullOrEmpty(profileList[index].presentAddress.villageHouseRoadNo))
                        {
                            tbPresentAddress.Text += profileList[index].presentAddress.villageHouseRoadNo + ", ";
                        }
                        if (profileList[index].presentAddress.union > 0)
                        {
                            tbPresentAddress.Text += GetValueForLookups("name_in_english", "eunion",
                                Convert.ToInt32(profileList[index].presentAddress.union)) + ", ";
                        }
                        if (profileList[index].presentAddress.upazila > 0)
                        {
                            tbPresentAddress.Text += GetValueForLookups("name_in_english", "upazila",
                                Convert.ToInt32(profileList[index].presentAddress.upazila)) + ", ";
                        }
                        if (profileList[index].presentAddress.district > 0)
                        {
                            tbPresentAddress.Text += GetValueForLookups("name_in_english", "district",
                                Convert.ToInt32(profileList[index].presentAddress.district));
                        }
                    }

                    if (profileList[index].permanentAddress != null)
                    {
                        tbPermanentAddress.Text = null;
                        if (!string.IsNullOrEmpty(profileList[index].permanentAddress.villageHouseRoadNo))
                        {
                            tbPermanentAddress.Text += profileList[index].permanentAddress.villageHouseRoadNo + ", ";
                        }
                        if (profileList[index].permanentAddress.union > 0)
                        {
                            tbPermanentAddress.Text += GetValueForLookups("name_in_english", "eunion",
                                Convert.ToInt32(profileList[index].permanentAddress.union)) + ", ";
                        }
                        if (profileList[index].permanentAddress.upazila > 0)
                        {
                            tbPermanentAddress.Text += GetValueForLookups("name_in_english", "upazila",
                                Convert.ToInt32(profileList[index].permanentAddress.upazila)) + ", ";
                        }
                        if (profileList[index].permanentAddress.district > 0)
                        {
                            tbPermanentAddress.Text += GetValueForLookups("name_in_english", "district",
                                Convert.ToInt32(profileList[index].permanentAddress.district));
                        }                                                
                    }
                });
            });
        }

        public string GetValueForLookups(string columnName, string tableName, int id)
        {
            string value = dbLookupManager.GetValueForLookup(columnName, tableName, id);
            return value;
        }

        private int position = 0;

        private void btnJailMatchPrev_Click(object sender, EventArgs e)
        {
            if (position > 0)
            {
                position -= 1;
                ShowMatchedProfile(position);
            }
        }

        private void btnJailMatchNext_Click(object sender, EventArgs e)
        {
            if (position < (profileList?.Count - 1))
            {
                position += 1;
                ShowMatchedProfile(position);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
