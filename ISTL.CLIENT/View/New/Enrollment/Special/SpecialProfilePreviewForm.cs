using ISTL.COMMON;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.MODELS.DTO.New.Enrollment.Special;
using ISTL.MODELS.Response.New;
using ISTL.RAB.ApiManager;
using ISTL.RAB.DbManager;
using ISTL.RAB.Entity;
using ISTL.RAB.Entity.Lookup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISTL.RAB.View.New.Enrollment.Special
{
    public partial class SpecialProfilePreviewForm : ViewForm
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

        private DbLookupManager dblookupManager;
        private LookupItems lookupItems = new LookupItems();
        public SpecialProfilePreviewForm()
        {
            InitializeComponent();
            dblookupManager = new DbLookupManager();
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            ProcessingDialog.Run(delegate ()
            {
                Invoke((MethodInvoker)delegate
                {
                    ShowSpecialProfilePreview(StaticData.specialEnrollment);
                });
            });
        }

        private void ShowSpecialProfilePreview(SpecialEnrollmentDto dto)
        {
            tbRefNo.Text = StaticData.specialEnrollment.referenceNo;
            tbFullName.Text = StaticData.specialEnrollment.fullName;
            tbFatherName.Text = StaticData.specialEnrollment.fatherName;
            tbGender.Text = StaticData.specialEnrollment.gender;
            tbNID.Text = StaticData.specialEnrollment.nid;
            tbPlaceOfFine.Text = StaticData.specialEnrollment.placeOfFine;
            tbFineAmount.Text = StaticData.specialEnrollment.fineAmount;
            tbRABOfficeName.Text = StaticData.specialEnrollment.rabOfficerName;
            tbMagistrateName.Text = StaticData.specialEnrollment.magistrateName;

            tbVillageRoadHouse.Text = StaticData.specialEnrollment.address?.villageHouseRoadNo;

            tbLaw.Text = StaticData.specialEnrollment.law;

            if (!string.IsNullOrEmpty(StaticData.specialEnrollment?.photoUrl))
            {
                GetProfileDataByteResponse response = new EnrollmentApiManager().GetByteDataByFilePath(StaticData.specialEnrollment?.photoUrl);
                if (response != null)
                {
                    if (response.code == 200)
                    {
                        if (StaticData.specialEnrollment.photo == null) StaticData.specialEnrollment.photo = new SpecialEnrollPhotoDto();
                        StaticData.specialEnrollment.photo.photo = response.file;
                        pbPhoto.Image = Utils.ByteToImage(StaticData.specialEnrollment?.photo?.photo);
                    }
                }
            }

            ShowLookupData();
        }

        private void ShowLookupData()
        {
            if (StaticData.specialEnrollment.address?.district > 0)
            {
                tbDistrict.Text = GetValueForLookups("name_in_english", "district", Convert.ToInt32(StaticData.specialEnrollment.address?.district));
            }
            if (StaticData.specialEnrollment.address?.district > 0 && StaticData.specialEnrollment.address?.upazila > 0)
            {
                tbThana.Text = GetValueForLookups("name_in_english", "upazila", Convert.ToInt32(StaticData.specialEnrollment.address?.upazila));
            }
            if (StaticData.specialEnrollment.address?.district > 0 && StaticData.specialEnrollment.address?.upazila > 0 && StaticData.specialEnrollment.address?.union > 0)
            {
                tbUnion.Text = GetValueForLookups("name_in_english", "eunion", Convert.ToInt32(StaticData.specialEnrollment.address?.union));
            }
            if (StaticData.specialEnrollment.unit >= 0)
            {
                tbUnit.Text = GetValueForLookups("name_en", "station", Convert.ToInt32(StaticData.specialEnrollment.unit));
            }
            if (StaticData.specialEnrollment.unit > 0 && StaticData.specialEnrollment.subUnit > 0)
            {
                tbSubUnit.Text = GetValueForLookups("name_en", "sub_station", Convert.ToInt32(StaticData.specialEnrollment.subUnit));
            }
            if (StaticData.specialEnrollment.crimeZone?.district > 0)
            {
                tbCrimeDistrict.Text = GetValueForLookups("name_in_english", "district", Convert.ToInt32(StaticData.specialEnrollment.crimeZone?.district));
            }
            if (StaticData.specialEnrollment.address?.district > 0 && StaticData.specialEnrollment.address?.upazila > 0)
            {
                tbCrimeUpazila.Text = GetValueForLookups("name_in_english", "upazila", Convert.ToInt32(StaticData.specialEnrollment.crimeZone?.upozilaOrThana));
            }

            if (!string.IsNullOrEmpty(StaticData.specialEnrollment.crimeType))
            {
                string crimeTypeName = null;
                ComboBoxItems.specialCrimeType.TryGetValue(Convert.ToString(StaticData.specialEnrollment?.crimeType), out crimeTypeName);
                tbCrimeType.Text = crimeTypeName;
            }
            if (!string.IsNullOrEmpty(StaticData.specialEnrollment?.arrestType))
            {
                if (StaticData.specialEnrollment?.arrestType == "MobileCourts") tbArrestType.Text = "Mobile Courts";
                else if (StaticData.specialEnrollment?.arrestType == "ContagiousPatients") tbArrestType.Text = "Contagious Patients";
                else if (StaticData.specialEnrollment?.arrestType == "directSubmitInPS") tbArrestType.Text = "Direct Submit In PS";
            }
        }

        public string GetValueForLookups(string columnName, string tableName, int id)
        {
            string value = dblookupManager.GetValueForLookup(columnName, tableName, id);
            return value;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            StaticData.specialEnrollment = new SpecialEnrollmentDto();
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            StaticData.specialEnrollment = new SpecialEnrollmentDto();
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
