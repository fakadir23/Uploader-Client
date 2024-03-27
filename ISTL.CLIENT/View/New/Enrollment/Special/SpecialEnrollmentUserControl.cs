using ISTL.COMMON;
using ISTL.COMMON.Common;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.MODELS.DTO.New.Enrollment.Special;
using ISTL.MODELS.Response.New;
using ISTL.RAB.ApiManager;
using ISTL.RAB.Controllers;
using ISTL.RAB.Controllers.New.Enrollment;
using ISTL.RAB.Controllers.New.Enrollment.Special;
using ISTL.RAB.DbManager;
using ISTL.RAB.Entity;
using ISTL.RAB.Entity.Lookup;
using ISTL.RAB.View.New.Enrollment.BiometricInformation;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace ISTL.RAB.View.New.Enrollment.Special
{
    public partial class SpecialEnrollmentUserControl : ViewUserControl
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private DbSpecialEnrollManager dbSpecialEnrollManager;
        private LookupItems lookupItems = new LookupItems();
        public SpecialEnrollmentUserControl()
        {
            InitializeComponent();
            dbSpecialEnrollManager = new DbSpecialEnrollManager();
            LoadComboBox();
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            this.tbRefNo.Text = Guid.NewGuid().ToString().Replace("-", "").ToUpper().Substring(0, 10);
            try
            {
                cmbUnit.SelectedValue = Users.Unit;
                cmbSubUnit_Enter(null, null);
                cmbSubUnit.SelectedValue = Users.SubUnit;
            }
            catch { }

            if (StaticData.specialEnrollment.referenceNo != null)
            {
                ShowStaticSpecialProfile();
                MakeFieldsReadonly();
            }
        }

        private int OnCloseFlag = 0;
        public override void OnClosing()
        {
            base.OnClosing();
            OnCloseFlag = 1;
        }

        private void ShowStaticSpecialProfile()
        {
            tbRefNo.Text = StaticData.specialEnrollment.referenceNo;
            tbFullName.Text = StaticData.specialEnrollment.fullName;
            tbFatherName.Text = StaticData.specialEnrollment.fatherName;
            try
            {
                cmbGender.SelectedValue = StaticData.specialEnrollment.gender;
            }
            catch { }
            try
            {
                cmbCrimeType.SelectedValue = StaticData.specialEnrollment.crimeType;
            }
            catch { }
            tbFineAmount.Text = StaticData.specialEnrollment.fineAmount;
            tbRABOfficeName.Text = StaticData.specialEnrollment.rabOfficerName;
            tbMagistrateName.Text = StaticData.specialEnrollment.magistrateName;
            tbPlaceOfFine.Text = StaticData.specialEnrollment.placeOfFine;
            try
            {
                if (StaticData.specialEnrollment.address?.district > 0)
                    cmbDistrict.SelectedValue = StaticData.specialEnrollment.address?.district;
            }
            catch { }
            try
            {
                if (StaticData.specialEnrollment.address?.district > 0 &&
                    StaticData.specialEnrollment.address?.upazila > 0)
                {
                    cmbUpazilla_Enter(null, null);
                    cmbUpazilla.SelectedValue = StaticData.specialEnrollment.address?.upazila;
                }
            }
            catch { }
            try
            {
                if (StaticData.specialEnrollment.address?.district > 0 &&
                    StaticData.specialEnrollment.address?.upazila > 0 &&
                    StaticData.specialEnrollment.address?.union > 0)
                {
                    cmbUnion_Enter(null, null);
                    cmbUnion.SelectedValue = StaticData.specialEnrollment.address?.union;
                }                
            }
            catch { }
            tbVillageRoadHouse.Text = StaticData.specialEnrollment.address?.villageHouseRoadNo;
            
            if (!string.IsNullOrEmpty(StaticData.specialEnrollment.arrestType))
            {
                if (StaticData.specialEnrollment.arrestType == "MobileCourts")
                {
                    cmbArresteeType.SelectedIndex = 0;
                }
                else if (StaticData.specialEnrollment.arrestType == "ContagiousPatients") cmbArresteeType.SelectedIndex = 1;
                else if (StaticData.specialEnrollment.arrestType == "directSubmitInPS") cmbArresteeType.SelectedIndex = 2;
            }
            //tbWarrantNo.Text = StaticData.specialEnrollment.warrantNo;

            if (StaticData.specialEnrollment.unit >= 0)
            {
                try
                {
                    cmbUnit.SelectedValue = StaticData.specialEnrollment.unit;
                }
                catch { }
            }
            if (StaticData.specialEnrollment.unit > 0 && StaticData.specialEnrollment.subUnit > 0)
            {
                cmbSubUnit_Enter(null, null);
                try
                {
                    cmbSubUnit.SelectedValue = StaticData.specialEnrollment.subUnit;
                }
                catch { }
            }
            if (StaticData.specialEnrollment.crimeZone?.district > 0)
            {
                try
                {
                    cmbCrimeDistrict_Enter(null, null);
                    cmbCrimeDistrict.SelectedValue = StaticData.specialEnrollment.crimeZone?.district;
                }
                catch { }
            }
            if (StaticData.specialEnrollment.crimeZone?.district > 0 && StaticData.specialEnrollment.crimeZone?.upozilaOrThana > 0)
            {
                try
                {
                    cmbCrimeThana_Enter(null, null);
                    cmbCrimeThana.SelectedValue = StaticData.specialEnrollment.crimeZone?.upozilaOrThana;
                }
                catch { }
            }
            tbLaw.Text = StaticData.specialEnrollment?.law;
            if (StaticData.specialEnrollment?.photo?.photo != null)
            {
                pbPhoto.Image = Utils.ByteToImage(StaticData.specialEnrollment?.photo?.photo);
                if (pbPhoto.Image != null) btnCaptureImage.Text = "Re-Capture Photo";
            }

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
                        if (pbPhoto.Image != null) btnCaptureImage.Text = "Re-Capture Photo";
                    }
                }
            }

            tbNID.Text = StaticData.specialEnrollment?.nid;
        }

        private void MakeFieldsReadonly()
        {
            if (StaticData.ModifiableSpecialEnrollment == false)
            {
                if (!string.IsNullOrEmpty(tbRefNo.Text)) tbRefNo.Enabled = false;
                if (!string.IsNullOrEmpty(tbFullName.Text)) tbFullName.Enabled = false;
                if (!string.IsNullOrEmpty(tbFatherName.Text)) tbFatherName.Enabled = false;
                if (!string.IsNullOrEmpty(cmbGender.SelectedValue?.ToString())) cmbGender.Enabled = false;
                if (!string.IsNullOrEmpty(cmbCrimeType.SelectedValue?.ToString())) cmbCrimeType.Enabled = false;
                if (!string.IsNullOrEmpty(tbFineAmount.Text)) tbFineAmount.Enabled = false;
                if (!string.IsNullOrEmpty(tbRABOfficeName.Text)) tbRABOfficeName.Enabled = false;
                if (!string.IsNullOrEmpty(tbMagistrateName.Text)) tbMagistrateName.Enabled = false;
                if (!string.IsNullOrEmpty(tbPlaceOfFine.Text)) tbPlaceOfFine.Enabled = false;
                if (!string.IsNullOrEmpty(cmbDistrict.SelectedValue?.ToString())) cmbDistrict.Enabled = false;
                if (!string.IsNullOrEmpty(cmbUpazilla.SelectedValue?.ToString())) cmbUpazilla.Enabled = false;
                if (!string.IsNullOrEmpty(cmbUnion.SelectedValue?.ToString())) cmbUnion.Enabled = false;
                if (!string.IsNullOrEmpty(tbVillageRoadHouse.Text)) tbVillageRoadHouse.Enabled = false;
                if (!string.IsNullOrEmpty(cmbArresteeType.SelectedValue?.ToString())) cmbArresteeType.Enabled = false;
                //if (!string.IsNullOrEmpty(tbWarrantNo.Text)) tbWarrantNo.Enabled = false;
                if (!string.IsNullOrEmpty(cmbUnit.SelectedValue?.ToString())) cmbUnit.Enabled = false;
                if (!string.IsNullOrEmpty(cmbSubUnit.SelectedValue?.ToString())) cmbSubUnit.Enabled = false;
                if (!string.IsNullOrEmpty(tbLaw.Text)) tbLaw.Enabled = false;
                if (!string.IsNullOrEmpty(cmbCrimeDistrict?.SelectedValue?.ToString())) cmbCrimeDistrict.Enabled = false;
                if (!string.IsNullOrEmpty(cmbCrimeThana?.SelectedValue?.ToString())) cmbCrimeThana.Enabled = false;
                if (pbPhoto.Image != null)
                {
                    pbPhoto.Enabled = false;
                    btnCaptureImage.Enabled = false;
                    btnCaptureImage.BackColor = Color.IndianRed;
                }
                if (!string.IsNullOrEmpty(tbNID.Text)) tbNID.Enabled = false;
            }
        }

        private void LoadComboBox()
        {
            cmbCrimeType.DataSource = new BindingSource(ComboBoxItems.specialCrimeType, null);
            cmbCrimeType = Utils.SuggestComboBoxFormat(cmbCrimeType, 0);

            LoadArrestType();
            LoadGender();
            LoadLocationLookup();
        }
        private void LoadArrestType()
        {
            cmbArresteeType.DataSource = new BindingSource(ComboBoxItems.arresteeType, null);
            cmbArresteeType = Utils.GeneralComboBoxFormat(cmbArresteeType);
        }
        private void LoadGender()
        {
            Dictionary<string, string> genderList = new Dictionary<string, string>();
            genderList.Add("Male", "Male");
            genderList.Add("Female", "Female");
            genderList.Add("Other", "Other");

            cmbGender.DataSource = new BindingSource(genderList, null);
            cmbGender = Utils.GeneralComboBoxFormat(cmbGender);
        }
        private void LoadLocationLookup()
        {
            lookupItems.LoadDistrict();
            if (lookupItems.districtList != null)
            {
                if (lookupItems.districtList.Count > 0)
                {
                    cmbDistrict.DataSource = new BindingSource(lookupItems.districtList, null);
                    cmbDistrict = Utils.SuggestComboBoxFormat(cmbDistrict, 1);

                    cmbCrimeDistrict.DataSource = new BindingSource(lookupItems.districtList, null);
                    cmbCrimeDistrict = Utils.SuggestComboBoxFormat(cmbCrimeDistrict, 1);
                }
            }

            lookupItems.LoadStations();
            if (lookupItems.stationList != null)
            {
                if (lookupItems.stationList.Count > 0)
                {
                    cmbUnit.DataSource = new BindingSource(lookupItems.stationList, null);
                    cmbUnit = Utils.GeneralComboBoxFormat(cmbUnit);
                }
            }
        }

        private void cmbUpazilla_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbDistrict.SelectedValue?.ToString()))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please select District first");
                cmbDistrict.Focus();
            }
            else
            {
                LookupItems LUItems = new LookupItems();
                LUItems.LoadUpazillaByDistrict(cmbDistrict.SelectedValue?.ToString());

                if (LUItems.upazillaList != null)
                {
                    if (LUItems.upazillaList.Count > 0)
                    {
                        cmbUpazilla.DataSource = new BindingSource(LUItems.upazillaList, null);
                        cmbUpazilla = Utils.SuggestComboBoxFormat(cmbUpazilla, 1);
                    }
                }
            }
        }

        private void cmbUnion_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbDistrict.SelectedValue?.ToString()))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please select Upazilla/Thana first");
                cmbUpazilla.Focus();
            }
            else
            {
                LookupItems LUItems = new LookupItems();
                LUItems.LoadUnionByUpazilla(cmbUpazilla.SelectedValue?.ToString());
                if (LUItems.unionList != null)
                {
                    if (LUItems.unionList.Count > 0)
                    {
                        cmbUnion.DataSource = new BindingSource(LUItems.unionList, null);
                        cmbUnion = Utils.SuggestComboBoxFormat(cmbUnion, 1);
                    }
                }
            }
        }

        private void cmbSubUnit_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbUnit.SelectedValue?.ToString()))
            {
                cmbUnit.Focus();
            }
            else
            {
                LookupItems LUItems = new LookupItems();
                LUItems.LoadSubStations(cmbUnit.SelectedValue?.ToString());

                if (LUItems.subStationList != null)
                {
                    if (LUItems.subStationList.Count > 0)
                    {
                        cmbSubUnit.DataSource = new BindingSource(LUItems.subStationList, null);
                        cmbSubUnit = Utils.GeneralComboBoxFormat(cmbSubUnit);
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SpecialEnrollmentDto dto = OnSave();
            if (dto != null)
            {
                ((SpecialEnrollmentController)controller).AddSpecialProfile(dto);
            }
        }

        private SpecialEnrollmentDto OnSave()
        {
            SpecialEnrollmentDto dto = new SpecialEnrollmentDto();
            dto.arrestTypeIntValue = (!string.IsNullOrEmpty(cmbArresteeType.SelectedValue?.ToString())) ?
                Convert.ToInt32(cmbArresteeType.SelectedValue?.ToString()) : 0;
            dto.crimeType = (!string.IsNullOrEmpty(cmbCrimeType.SelectedValue?.ToString())) ?
                cmbCrimeType.SelectedValue?.ToString() : null;
            dto.fatherName = (!string.IsNullOrEmpty(tbFatherName.Text)) ? tbFatherName.Text : null;
            dto.gender = (!string.IsNullOrEmpty(cmbGender.SelectedValue?.ToString())) ? cmbGender.SelectedValue?.ToString() : null;
            dto.fullName = (!string.IsNullOrEmpty(tbFullName.Text)) ? tbFullName.Text : null;
            dto.fineAmount = (!string.IsNullOrEmpty(tbFineAmount.Text)) ? tbFineAmount.Text : null;
            dto.magistrateName = (!string.IsNullOrEmpty(tbMagistrateName.Text)) ?
                tbMagistrateName.Text : null;
            dto.placeOfFine = (!string.IsNullOrEmpty(tbPlaceOfFine.Text)) ? tbPlaceOfFine.Text : null;
            dto.rabOfficerName = (!string.IsNullOrEmpty(tbRABOfficeName.Text)) ?
                tbRABOfficeName.Text : null;
            dto.referenceNo = (!string.IsNullOrEmpty(tbRefNo.Text)) ? tbRefNo.Text : null;
            //dto.warrantNo = (!string.IsNullOrEmpty(tbWarrantNo.Text)) ? tbWarrantNo.Text : null;
            dto.unit = (!string.IsNullOrEmpty(cmbUnit.SelectedValue?.ToString())) ? Convert.ToInt32(cmbUnit.SelectedValue?.ToString()) : -1;
            dto.subUnit = (!string.IsNullOrEmpty(cmbSubUnit.SelectedValue?.ToString())) ? Convert.ToInt32(cmbSubUnit.SelectedValue?.ToString()) : 0;

            dto.law = (!string.IsNullOrEmpty(tbLaw.Text)) ? tbLaw.Text : null;
            if (dto.photo == null) dto.photo = new SpecialEnrollPhotoDto();
            dto.photo.photo = Utils.ImageToByte(pbPhoto.Image);
            dto.crimeZone = OnSaveCrimeZone();

            if (string.IsNullOrEmpty(dto.fullName))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Full Name is mandatory");
                tbFullName.Focus();
                return null;
            }

            if (string.IsNullOrEmpty(dto.gender))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Gender is mandatory");
                cmbGender.Focus();
                return null;
            }

            if (dto.arrestTypeIntValue <= 0)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Arrest Type is mandatory");
                cmbArresteeType.Focus();
                return null;
            }

            if (dto.unit < 0)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Unit is mandatory");
                cmbUnit.Focus();
                return null;
            }

            if (dto.unit > 0 && dto.subUnit <= 0)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Sub-Unit is mandatory");
                cmbSubUnit.Focus();
                return null;
            }

            if (dto.crimeZone?.district <= 0)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Crime District is mandatory");
                cmbCrimeDistrict.Focus();
                return null;
            }

            if (dto.crimeZone?.upozilaOrThana <= 0)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Crime Upozila is mandatory");
                cmbCrimeThana.Focus();
                return null;
            }

            SpecialAddressDto specialAddressDto = new SpecialAddressDto();
            specialAddressDto.district = (!string.IsNullOrEmpty(cmbDistrict.SelectedValue?.ToString()))
                ? Convert.ToInt32(cmbDistrict.SelectedValue?.ToString()) : 0;

            specialAddressDto.upazila = (!string.IsNullOrEmpty(cmbUpazilla.SelectedValue?.ToString()))
                ? Convert.ToInt32(cmbUpazilla.SelectedValue?.ToString()) : 0;
            
            specialAddressDto.union = (!string.IsNullOrEmpty(cmbUnion.SelectedValue?.ToString()))
                ? Convert.ToInt32(cmbUnion.SelectedValue?.ToString()) : 0;
            
            specialAddressDto.villageHouseRoadNo = (!string.IsNullOrEmpty(tbVillageRoadHouse.Text)) ?
                tbVillageRoadHouse.Text : null;

            dto.address = specialAddressDto;

            dto.nid = (!string.IsNullOrEmpty(tbNID.Text)) ? tbNID.Text : null;

            StaticData.specialEnrollment = dto;

            return dto;
        }

        private void tbFullName_Leave(object sender, EventArgs e)
        {
            if (!Utils.IsAlphaCheck(tbFullName.Text))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Name can contain only letters");
                tbFullName.Text = null;
                return;
            }

            string fullName = tbFullName.Text;
            //if (string.IsNullOrEmpty(fullName)) return;
            if (fullName == StaticData.specialEnrollment?.fullName) return;
            string refNo = tbRefNo.Text;
            StaticData.specialEnrollment.referenceNo = refNo;
            StaticData.specialEnrollment.fullName = fullName;
            dbSpecialEnrollManager.UpdateProfileValues(refNo, "full_name", -1, fullName);

            if (OnCloseFlag == 1) StaticData.specialEnrollment = new SpecialEnrollmentDto();
        }
        private void tbFatherName_Leave(object sender, EventArgs e)
        {
            if (!Utils.IsAlphaCheck(tbFatherName.Text))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Name can contain only letters");
                tbFatherName.Text = null;
                return;
            }

            string fatherName = tbFatherName.Text;
            //if (string.IsNullOrEmpty(value)) return;
            if (fatherName == StaticData.specialEnrollment?.fatherName) return;
            string refNo = tbRefNo.Text;
            StaticData.specialEnrollment.referenceNo = refNo;
            StaticData.specialEnrollment.fatherName = fatherName;
            dbSpecialEnrollManager.UpdateProfileValues(refNo, "father_name", -1, fatherName);

            if (OnCloseFlag == 1) StaticData.specialEnrollment = new SpecialEnrollmentDto();
        }
        private void cmbGender_Leave(object sender, EventArgs e)
        {
            string value = cmbGender?.SelectedValue?.ToString();
            if (string.IsNullOrEmpty(value)) return;
            string refNo = tbRefNo.Text;
            StaticData.specialEnrollment.referenceNo = refNo;
            StaticData.specialEnrollment.gender = value;

            if (value == "Male")    dbSpecialEnrollManager.UpdateProfileValues(refNo, "gender", 0, null);
            if (value == "Female")    dbSpecialEnrollManager.UpdateProfileValues(refNo, "gender", 1, null);
            if (value == "Other")    dbSpecialEnrollManager.UpdateProfileValues(refNo, "gender", 2, null);

            if (OnCloseFlag == 1) StaticData.specialEnrollment = new SpecialEnrollmentDto();
        }
        private void cmbCrimeType_Leave(object sender, EventArgs e)
        {
            string value = cmbCrimeType?.SelectedValue?.ToString();
            //if (string.IsNullOrEmpty(value)) return;
            string refNo = tbRefNo.Text;
            StaticData.specialEnrollment.referenceNo = refNo;
            StaticData.specialEnrollment.crimeType = value;
            dbSpecialEnrollManager.UpdateProfileValues(refNo, "crime_type", -1, value);

            if (OnCloseFlag == 1) StaticData.specialEnrollment = new SpecialEnrollmentDto();
        }
        private void tbFineAmount_Leave(object sender, EventArgs e)
        {
            string value = tbFineAmount.Text;
            //if (string.IsNullOrEmpty(value)) return;
            string refNo = tbRefNo.Text;
            StaticData.specialEnrollment.referenceNo = refNo;
            StaticData.specialEnrollment.fineAmount = value;
            dbSpecialEnrollManager.UpdateProfileValues(refNo, "fine_amount", -1, value);

            if (OnCloseFlag == 1) StaticData.specialEnrollment = new SpecialEnrollmentDto();
        }
        private void tbRABOfficeName_Leave(object sender, EventArgs e)
        {
            string value = tbRABOfficeName.Text;
            //if (string.IsNullOrEmpty(value)) return;
            string refNo = tbRefNo.Text;
            StaticData.specialEnrollment.referenceNo = refNo;
            StaticData.specialEnrollment.rabOfficerName = value;
            dbSpecialEnrollManager.UpdateProfileValues(refNo, "rab_officer_name", -1, value);

            if (OnCloseFlag == 1) StaticData.specialEnrollment = new SpecialEnrollmentDto();
        }
        private void tbMagistrateName_Leave(object sender, EventArgs e)
        {
            string value = tbMagistrateName.Text;
            //if (string.IsNullOrEmpty(value)) return;
            string refNo = tbRefNo.Text;
            StaticData.specialEnrollment.referenceNo = refNo;
            StaticData.specialEnrollment.magistrateName = value;
            dbSpecialEnrollManager.UpdateProfileValues(refNo, "magistrate_name", -1, value);

            if (OnCloseFlag == 1) StaticData.specialEnrollment = new SpecialEnrollmentDto();
        }
        private void tbPlaceOfFine_Leave(object sender, EventArgs e)
        {
            string value = tbPlaceOfFine.Text;
            //if (string.IsNullOrEmpty(value)) return;
            string refNo = tbRefNo.Text;
            StaticData.specialEnrollment.referenceNo = refNo;
            StaticData.specialEnrollment.placeOfFine = value;
            dbSpecialEnrollManager.UpdateProfileValues(refNo, "place_of_fine", -1, value);

            if (OnCloseFlag == 1) StaticData.specialEnrollment = new SpecialEnrollmentDto();
        }
        private void cmbArresteeType_Leave(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(cmbArresteeType.SelectedValue?.ToString());
            string refNo = tbRefNo.Text;
            StaticData.specialEnrollment.referenceNo = refNo;
            StaticData.specialEnrollment.arrestTypeIntValue = value;
            dbSpecialEnrollManager.UpdateProfileValues(refNo, "arrestee_type", value, null);

            if (OnCloseFlag == 1) StaticData.specialEnrollment = new SpecialEnrollmentDto();
        }
        private void cmbDistrict_Leave(object sender, EventArgs e)
        {
            OnSaveAddress();
        }
        private void cmbUpazilla_Leave(object sender, EventArgs e)
        {
            OnSaveAddress();
        }
        private void cmbUnion_Leave(object sender, EventArgs e)
        {
            OnSaveAddress();
        }
        private void tbVillageRoadHouse_Leave(object sender, EventArgs e)
        {
            OnSaveAddress();
        }
        private void OnSaveAddress()
        {
            SpecialAddressDto SpAdressDto = new SpecialAddressDto();
            SpAdressDto.district = (!string.IsNullOrEmpty(cmbDistrict.SelectedValue?.ToString())) ? Convert.ToInt32(cmbDistrict.SelectedValue?.ToString()) : 0;
            SpAdressDto.upazila = (!string.IsNullOrEmpty(cmbUpazilla.SelectedValue?.ToString())) ? Convert.ToInt32(cmbUpazilla.SelectedValue?.ToString()) : 0;
            SpAdressDto.union = (!string.IsNullOrEmpty(cmbUnion.SelectedValue?.ToString())) ? Convert.ToInt32(cmbUnion.SelectedValue?.ToString()) : 0;
            SpAdressDto.villageHouseRoadNo = (!string.IsNullOrEmpty(tbVillageRoadHouse.Text)) ? tbVillageRoadHouse.Text : null;

            string refNo = tbRefNo.Text;
            StaticData.specialEnrollment.referenceNo = refNo; ;
            StaticData.specialEnrollment.address = SpAdressDto;

            if (SpAdressDto != null)
            {
                var value = new JavaScriptSerializer().Serialize(SpAdressDto);
                dbSpecialEnrollManager.UpdateProfileValues(refNo, "address", -1, value);
            }
            if (OnCloseFlag == 1) StaticData.specialEnrollment = new SpecialEnrollmentDto();
        }

        private void cmbUnit_Leave(object sender, EventArgs e)
        {
            int unit = Convert.ToInt32(cmbUnit.SelectedValue?.ToString());
            if (unit == StaticData.specialEnrollment.unit)
            {
                return;
            }
            StaticData.specialEnrollment.referenceNo = tbRefNo.Text;
            StaticData.specialEnrollment.unit = unit;

            dbSpecialEnrollManager.UpdateProfileValuesAsync(tbRefNo.Text, "unit", unit, null);

            if (OnCloseFlag == 1) StaticData.specialEnrollment = new SpecialEnrollmentDto();
        }

        private void cmbSubUnit_Leave(object sender, EventArgs e)
        {
            int unit = Convert.ToInt32(cmbUnit.SelectedValue?.ToString());            
            StaticData.specialEnrollment.referenceNo = tbRefNo.Text;
            StaticData.specialEnrollment.unit = unit;

            dbSpecialEnrollManager.UpdateProfileValuesAsync(tbRefNo.Text, "unit", unit, null);

            int subunit = Convert.ToInt32(cmbSubUnit.SelectedValue?.ToString());
            if (subunit == StaticData.specialEnrollment.subUnit)
            {
                return;
            }
            StaticData.specialEnrollment.referenceNo = tbRefNo.Text;
            StaticData.specialEnrollment.subUnit = subunit;

            dbSpecialEnrollManager.UpdateProfileValuesAsync(tbRefNo.Text, "sub_unit", subunit, null);

            if (OnCloseFlag == 1) StaticData.specialEnrollment = new SpecialEnrollmentDto();
        }

        private void tbFullName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) tbFatherName.Focus();
        }
        private void tbFatherName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) cmbGender.Focus();
        }
        private void cmbGender_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) tbNID.Focus();
        }
        private void cmbCrimeType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) cmbArresteeType.Focus();
        }
        private void tbFineAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) tbMagistrateName.Focus();
        }
        private void tbRABOfficeName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) cmbDistrict.Focus();
        }
        private void tbMagistrateName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) tbRABOfficeName.Focus();
        }
        private void tbPlaceOfFine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) tbFineAmount.Focus();
        }
        private void cmbDistrict_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) cmbUpazilla.Focus();
        }
        private void cmbUpazilla_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) cmbUnion.Focus();
        }
        private void cmbUnion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) tbVillageRoadHouse.Focus();
        }
        private void tbVillageRoadHouse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) cmbCrimeType.Focus();
        }
        private void cmbArresteeType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) cmbUnit.Focus();
        }
        private void tbWarrantNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) tbRABOfficeName.Focus();
        }
        private void cmbUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) cmbSubUnit.Focus();
        }
        private void cmbSubUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) cmbCrimeDistrict.Focus();
        }

        private void tbFullName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || e.KeyChar == (Char)Keys.Back || e.KeyChar == ' ' || e.KeyChar == '-' || e.KeyChar == '.') e.Handled = false;
            else e.Handled = true;
        }

        private void tbFatherName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || e.KeyChar == (Char)Keys.Back || e.KeyChar == ' ' || e.KeyChar == '-' || e.KeyChar == '.') e.Handled = false;
            else e.Handled = true;
        }

        private void tbPlaceOfFine_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar) || e.KeyChar == (Char)Keys.Back || e.KeyChar == ' ' || e.KeyChar == '-' || e.KeyChar == '.') e.Handled = false;
            else e.Handled = true;
        }

        private void tbMagistrateName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || e.KeyChar == (Char)Keys.Back || e.KeyChar == ' ' || e.KeyChar == '-' || e.KeyChar == '.') e.Handled = false;
            else e.Handled = true;
        }

        private void tbRABOfficeName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || e.KeyChar == (Char)Keys.Back || e.KeyChar == ' ' || e.KeyChar == '-' || e.KeyChar == '.') e.Handled = false;
            else e.Handled = true;
        }

        private void cmbDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbUpazilla.DataSource = null;
            cmbUnion.DataSource = null;
        }

        private void cmbUpazilla_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbUnion.DataSource = null;
        }

        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSubUnit.DataSource = null;
            //cmbCrimeDistrict.DataSource = null;
            //cmbCrimeThana.DataSource = null;
        }

        private void pbPhoto_Click(object sender, EventArgs e)
        {
            Image img = GetImage();
            if (img == null) return;

            btnCaptureImage.Text = "Re-Capture Photo";

            int maxPhotoSize = Convert.ToInt32(ConfigurationManager.AppSettings["PhotoSizeInKB"].ToString());
            if (GraphicsManager.GetInstance().ImageToByteArray(img).Length > maxPhotoSize)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Photo size is too big. Size should not exceed " + (maxPhotoSize / (1024 * 1000)) + " MB");
                return;
            }
            pbPhoto.Image = img;

            var resizedImg = GraphicsManager.GetInstance().ResizeImage(this.pbPhoto.Image,
                    new Size() { Width = 200, Height = 350 }, true);

            StaticData.specialEnrollment.referenceNo = tbRefNo.Text;

            if (StaticData.specialEnrollment?.photo == null) StaticData.specialEnrollment.photo = new SpecialEnrollPhotoDto();

            StaticData.specialEnrollment.photo.photo = Utils.ImageToByte(resizedImg);

            dbSpecialEnrollManager.UpdateProfilePhoto(tbRefNo.Text, "photo", StaticData.specialEnrollment?.photo?.photo);

            if (OnCloseFlag == 1) StaticData.specialEnrollment = new SpecialEnrollmentDto();
        }

        private Image GetImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
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

                    if (form.CamData.CamImage != null) btnCaptureImage.Text = "Re-Capture Photo";

                    var resizedImg = GraphicsManager.GetInstance().ResizeImage(this.pbPhoto.Image,
                    new Size() { Width = 200, Height = 350 }, true);

                    if (StaticData.specialEnrollment.photo == null) StaticData.specialEnrollment.photo = new SpecialEnrollPhotoDto();

                    StaticData.specialEnrollment.photo.photo = Utils.ImageToByte(resizedImg);

                    dbSpecialEnrollManager.UpdateProfilePhoto(tbRefNo.Text, "photo", StaticData.specialEnrollment?.photo?.photo);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Photo capture error." + ex.ToString());
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "There was an unexpected error while capturing photo. Please contact with your Administrator.");
            }
        }

        private void cmbCrimeDistrict_Leave(object sender, EventArgs e)
        {
            int unit = Convert.ToInt32(cmbUnit.SelectedValue?.ToString());
            
            StaticData.specialEnrollment.referenceNo = tbRefNo.Text;
            StaticData.specialEnrollment.unit = unit;

            dbSpecialEnrollManager.UpdateProfileValuesAsync(tbRefNo.Text, "unit", unit, null);

            int subunit = Convert.ToInt32(cmbSubUnit.SelectedValue?.ToString());
            
            StaticData.specialEnrollment.subUnit = subunit;

            dbSpecialEnrollManager.UpdateProfileValuesAsync(tbRefNo.Text, "sub_unit", subunit, null);


            SpecialCrimeZoneDto dto = OnSaveCrimeZone();
            var json = new JavaScriptSerializer().Serialize(dto);
            string refNo = tbRefNo.Text;
            StaticData.specialEnrollment.crimeZone = dto;
            dbSpecialEnrollManager.UpdateProfileValues(refNo, "crime_zone", -1, json);
        }

        private void cmbCrimeThana_Leave(object sender, EventArgs e)
        {
            SpecialCrimeZoneDto dto = OnSaveCrimeZone();
            var json = new JavaScriptSerializer().Serialize(dto);
            string refNo = tbRefNo.Text;
            StaticData.specialEnrollment.referenceNo = refNo;
            StaticData.specialEnrollment.crimeZone = dto;
            dbSpecialEnrollManager.UpdateProfileValues(refNo, "crime_zone", -1, json);
        }

        private void tbLaw_Leave(object sender, EventArgs e)
        {
            string value = tbLaw.Text;
            //if (string.IsNullOrEmpty(value)) return;
            string refNo = tbRefNo.Text;
            StaticData.specialEnrollment.referenceNo = refNo;
            StaticData.specialEnrollment.law = value;
            dbSpecialEnrollManager.UpdateProfileValues(refNo, "law", -1, value);
        }

        private SpecialCrimeZoneDto OnSaveCrimeZone()
        {
            SpecialCrimeZoneDto dto = new SpecialCrimeZoneDto();

            dto.unit = (!string.IsNullOrEmpty(cmbUnit.SelectedValue?.ToString())) ? Convert.ToInt32(cmbUnit.SelectedValue?.ToString()) : -1;
            dto.district = (!string.IsNullOrEmpty(cmbCrimeDistrict.SelectedValue?.ToString())) ? Convert.ToInt32(cmbCrimeDistrict.SelectedValue?.ToString()) : 0;
            dto.upozilaOrThana = (!string.IsNullOrEmpty(cmbCrimeThana.SelectedValue?.ToString())) ? Convert.ToInt32(cmbCrimeThana.SelectedValue?.ToString()) : 0;
            return dto;
        }

        private void cmbSubUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmbCrimeDistrict.DataSource = null;
            //cmbCrimeThana.DataSource = null;
        }

        private void cmbCrimeDistrict_Enter(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(cmbSubUnit.SelectedValue?.ToString()))
            //{
            //    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please select Sub Station first");
            //    cmbSubUnit.Focus();
            //}
            //else
            //{
            //    LookupItems lookupItems = new LookupItems();
            //    lookupItems.LoadRabDistrictBySubUnit(Convert.ToInt32(cmbSubUnit.SelectedValue?.ToString()));

            //    if (lookupItems.districtList != null)
            //    {
            //        if (lookupItems.districtList.Count > 0)
            //        {
            //            cmbCrimeDistrict.DataSource = new BindingSource(lookupItems.districtList, null);
            //            cmbCrimeDistrict.DisplayMember = "Value";
            //            cmbCrimeDistrict.ValueMember = "Key";
            //            cmbCrimeDistrict.PropertySelector = collection => collection.Cast<KeyValuePair<int, string>>().Select(p => p.Value);
            //            cmbCrimeDistrict.FilterRule = (item, text) => item.Trim().ToLower().Contains(text.Trim().ToLower());
            //            cmbCrimeDistrict.SuggestListOrderRule = s => s;
            //            cmbCrimeDistrict.SelectedIndex = -1;
            //        }
            //    }
            //}
        }

        private void cmbCrimeThana_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbCrimeDistrict.SelectedValue?.ToString()))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please select Crime District first");
                cmbCrimeDistrict.Focus();
            }
            else
            {
                LookupItems lookupItems = new LookupItems();
                //lookupItems.LoadRabUpazillaBySubUnitRabDistrict(cmbSubUnit.SelectedValue?.ToString(),
                //    cmbCrimeDistrict.SelectedValue?.ToString());

                //if (lookupItems.upazillaList != null)
                //{
                //    if (lookupItems.upazillaList.Count > 0)
                //    {
                //        cmbCrimeThana.DataSource = new BindingSource(lookupItems.upazillaList, null);
                //        cmbCrimeThana.DisplayMember = "Value";
                //        cmbCrimeThana.ValueMember = "Key";
                //        cmbCrimeThana.PropertySelector = collection => collection.Cast<KeyValuePair<int, string>>().Select(p => p.Value);
                //        cmbCrimeThana.FilterRule = (item, text) => item.Trim().ToLower().Contains(text.Trim().ToLower());
                //        cmbCrimeThana.SuggestListOrderRule = s => s;
                //        cmbCrimeThana.SelectedIndex = -1;
                //    }
                //}
                LookupItems LUItems = new LookupItems();
                LUItems.LoadUpazillaByDistrict(cmbCrimeDistrict.SelectedValue?.ToString());

                if (LUItems.upazillaList != null)
                {
                    if (LUItems.upazillaList.Count > 0)
                    {
                        cmbCrimeThana.DataSource = new BindingSource(LUItems.upazillaList, null);
                        cmbCrimeThana = Utils.SuggestComboBoxFormat(cmbCrimeThana, 1);
                    }
                }
            }
        }

        private void cmbArresteeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbArresteeType.SelectedIndex == 2)
            {
                pbPhoto.Enabled = true;
                btnCaptureImage.Enabled = true;
                btnCaptureImage.BackColor = Color.MediumSeaGreen;
            }
            else
            {
                pbPhoto.Enabled = false;
                btnCaptureImage.Enabled = false;
                btnCaptureImage.BackColor = Color.IndianRed;
                pbPhoto.Image = null;
                if (StaticData.specialEnrollment.photo == null) StaticData.specialEnrollment.photo = new SpecialEnrollPhotoDto();                
            }

            if (cmbArresteeType.SelectedIndex == 1)
            {
                tbPlaceOfFine.Text = null;
                tbPlaceOfFine.Enabled = false;
                tbFineAmount.Text = null;
                tbFineAmount.Enabled = false;
                tbMagistrateName.Text = null;
                tbMagistrateName.Enabled = false;
                tbLaw.Text = null;
                tbLaw.Enabled = false;
            }
            else
            {
                tbPlaceOfFine.Enabled = true;
                tbFineAmount.Enabled = true;
                tbMagistrateName.Enabled = true;
                tbLaw.Enabled = true;
            }
        }

        private void cmbCrimeDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCrimeThana.DataSource = null;
        }

        private void tbNID_Leave(object sender, EventArgs e)
        {
            string value = tbNID.Text;
            //if (string.IsNullOrEmpty(value)) return;
            if (!string.IsNullOrEmpty(value))
            {
                if (!Utils.isDigit(tbNID.Text))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "NID can contain only digits");
                    tbNID.Focus();
                    return;
                }

                if (value?.Length != 10 && value?.Length != 17)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "NID has to be 10 or 17 digits");
                    tbNID.Focus();
                    return;
                }
            }

            string refNo = tbRefNo.Text;
            StaticData.specialEnrollment.referenceNo = refNo;
            StaticData.specialEnrollment.nid = value;
            dbSpecialEnrollManager.UpdateProfileValues(refNo, "nid", -1, value);

            if (OnCloseFlag == 1) StaticData.specialEnrollment = new SpecialEnrollmentDto();
        }

        private void tbNID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) tbPlaceOfFine.Focus();
        }

        private void cmbCrimeDistrict_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) cmbCrimeThana.Focus();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            ((SpecialEnrollmentController)controller).GoBacktoDashboard();
        }
    }
}
