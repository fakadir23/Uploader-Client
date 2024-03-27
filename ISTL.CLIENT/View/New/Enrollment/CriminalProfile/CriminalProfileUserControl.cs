using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using ISTL.COMMON;
using ISTL.COMMON.Common;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.MODELS.DTO.New.Enrollment.CrimeInformation;
using ISTL.PERSOGlobals;
using ISTL.RAB.Controllers;
using ISTL.RAB.Controllers.New.Enrollment;
using ISTL.RAB.DbManager;
using ISTL.RAB.Entity;
using ISTL.RAB.Entity.Lookup;
using ISTL.RAB.View.New.CriminalProfile;
using ISTL.RAB.View.New.Enrollment.CriminalProfile;
using NLog;

namespace ISTL.RAB.View.New
{
    public partial class CriminalProfileUserControl : ViewUserControl
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private DbEnrollClientManager dbEnrollClientManager;
        public CriminalProfileUserControl()
        {
            InitializeComponent();

            LoadComboItems();

            dbEnrollClientManager = new DbEnrollClientManager();

            // dateOfBirth.CustomFormat = "dd/MM/yyyy";

            dtpArrestDate.CustomFormat = "dd/MM/yyyy";
            dtpDateOfCrime.CustomFormat = "dd/MM/yyyy";
            dtpFIRDate.CustomFormat = "dd/MM/yyyy";
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            this.tbRefNo.Text = Guid.NewGuid().ToString().Replace("-", "").ToUpper().Substring(0, 10);

            if (!string.IsNullOrEmpty(StaticData.Enrollment.profile.referenceNo))
            {
                //ShowStaticProfileData(StaticData.Enrollment.profile);
                DoGUISwitch();                
            }
        }

        private void DoGUISwitch()
        {
            ProcessingDialog.Run(delegate ()
                {
                    Invoke((MethodInvoker)delegate
                    {
                        ShowStaticProfileData(StaticData.Enrollment.profile);

                        if (StaticData.ModifiableNormalEnrollment == false)
                        {
                            MakeFieldsReadonly();
                        }
                    });
                });            
        }

        private int OnCloseFlag = 0;
        public override void OnClosing()
        {
            base.OnClosing();
            if (!(btnBiometric.ContainsFocus || btnPreviewSubmit.ContainsFocus || btnOtherInfo.ContainsFocus || btnCriminalProfile.ContainsFocus))
            {
                StaticData.Enrollment.profile = new ProfileDto();
                OnCloseFlag = 1;
            }
        }

        private void MakeFieldsReadonly()
        {
            ProfileDto dto = StaticData.Enrollment.profile;
            if (!string.IsNullOrEmpty(dto.arrestDate)) dtpArrestDate.Enabled = false;
            if (!string.IsNullOrEmpty(dto.fullName)) tbFullName.Enabled = false;
            if (dto?.nickName?.Count > 0)
            {
                tbNickName.ReadOnly = true;
                //btnAddNickName.Enabled = false;
            }
            if (!string.IsNullOrEmpty(dto?.gender))
            {
                rdBtnMale.Enabled = false;
                rdBtnFemale.Enabled = false;
                rdbtnOther.Enabled = false;
            }
            if (dto?.nationality?.id > 0)
            {
                cmbNationality.Enabled = false;
                if (dto.nationality.id == 14)
                {
                    tbPostCode.Enabled = false;
                    tbTown.Enabled = false;
                    tbState.Enabled = false;
                    cmbCountry.Enabled = false;

                    cmbDistrictPerm.Enabled = true;
                    cmbUpazillaPerm.Enabled = true;
                    cmbUnionPerm.Enabled = true;
                    tbVillagePerm.Enabled = true;
                }
                else
                {
                    tbPostCode.Enabled = true;
                    tbTown.Enabled = true;
                    tbState.Enabled = true;
                    cmbCountry.Enabled = true;

                    cmbDistrictPerm.Enabled = false;
                    cmbUpazillaPerm.Enabled = false;
                    cmbUnionPerm.Enabled = false;
                    tbVillagePerm.Enabled = false;
                }
            }
            if (dto.mobile?.Count > 0) tbPhone.Enabled = false;
            if (!string.IsNullOrEmpty(dto.religion)) cmbReligion.Enabled = false;
            if (!string.IsNullOrEmpty(dto.maritalStatus)) cmbMaritalStatus.Enabled = false;
            if (!string.IsNullOrEmpty(dto.occupation)) cmbOccupation.Enabled = false;
            if (dto.height?.ft > 0) tbHeightFeet.Enabled = false;
            if (dto.height?.inch > 0) tbHeightInch.Enabled = false;
            if (dto.familys?.Count > 0)
            {
                for (int i = 0; i < dto.familys?.Count; i++)
                {
                    if (dto.familys[i]?.relation == "father")
                    {
                        if (!string.IsNullOrEmpty(dto.familys[i].name))   tbFatherName.Enabled = false;
                        if (!string.IsNullOrEmpty(dto.familys[i].phone))   tbFatherPhone.Enabled = false;
                    }
                    else if (dto.familys[i]?.relation == "mother")
                    {
                        if (!string.IsNullOrEmpty(dto.familys[i].name))     tbMotherName.Enabled = false;
                    }
                    else if (dto.familys[i]?.relation == "spouse")
                    {
                        if (!string.IsNullOrEmpty(dto.familys[i].name))     tbSpouseName.Enabled = false;
                        if (!string.IsNullOrEmpty(dto.familys[i].phone))     tbSpousePhone.Enabled = false;
                    }
                }
            }
            if (!string.IsNullOrEmpty(dto.nid)) tbNID.Enabled = false;
            if (!string.IsNullOrEmpty(dto.dateOfBirth) || dto.age > 0)
            {
                tbDOBDay.Enabled = false;
                tbDOBMonth.Enabled = false;
                tbDOBYear.Enabled = false;
                tbAge.Enabled = false;
            }
            if (dto.educationalInformations?.Count > 0)
            {
                //tbEducation.Enabled = false;
                btnAddEducation.Enabled = false;
            }
            if (!string.IsNullOrEmpty(dto.identificationMark)) tbIdentificationMark.Enabled = false;

            //For address

            if (dto.presentAddress != null)
            {
                if (dto.presentAddress?.district > 0) cmbDistrict.Enabled = false;
                if (dto.presentAddress?.upazila > 0) cmbUpazilla.Enabled = false;
                if (dto.presentAddress?.union > 0) cmbUnion.Enabled = false;
                if (!string.IsNullOrEmpty(dto.presentAddress?.villageHouseRoadNo)) tbVillageRoadHouse.Enabled = false;
            }
            if (dto.permanentAddress != null)
            {
                if (dto.permanentAddress?.district > 0) cmbDistrictPerm.Enabled = false;
                if (dto.permanentAddress?.upazila > 0) cmbUpazillaPerm.Enabled = false;
                if (dto.permanentAddress?.union > 0) cmbUnionPerm.Enabled = false;
                if (!string.IsNullOrEmpty(dto.permanentAddress?.villageHouseRoadNo)) tbVillagePerm.Enabled = false;
            }
            if (dto.foreignAddress != null)
            {
                if (!string.IsNullOrEmpty(dto.foreignAddress.zipOrPostCode)) tbPostCode.Enabled = false;
                if (!string.IsNullOrEmpty(dto.foreignAddress.town)) tbTown.Enabled = false;
                if (!string.IsNullOrEmpty(dto.foreignAddress.state)) tbState.Enabled = false;
                if (dto.foreignAddress.country > 0) cmbCountry.Enabled = false;
            }

            // For Crime Information
            if (dto.crimeInformation != null)
            {
                if (!string.IsNullOrEmpty(dto.crimeInformation.groupOrGangName)) tbGroupGangName.Enabled = false;
                if (dto.crimeInformation.crimeHistorys?.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dto.crimeInformation.crimeHistorys[0].dateOfCrime)) dtpDateOfCrime.Enabled = false;
                }
                if (dto.crimeInformation.crimeType > 0) cmbCrimeType.Enabled = false;
                if (dto.unit >= 0) cmbBattalion.Enabled = false;
                if (dto.subUnit > 0) cmbSubStation.Enabled = false;
                if (dto.crimeInformation.crimeZone != null)
                {
                    if (dto.crimeInformation.crimeZone.district > 0) cmbCrimeDistrict.Enabled = false;
                    if (dto.crimeInformation.crimeZone.upozilaOrThana > 0) cmbCrimeThana.Enabled = false;
                    if (dto.crimeInformation?.crimeZone?.union > 0) cmbCrimeUnion.Enabled = false;
                    if (!string.IsNullOrEmpty(dto.crimeInformation?.crimeZone?.addressLine)) tbCrimeVillage.Enabled = false;
                }
                if (!string.IsNullOrEmpty(dto.arrestedBy)) tbArrestedByOfficerName.Enabled = false;
                //if (dto.crimeZoneDistrict > 0) cmbCrimeDistrict.Enabled = false;
                //if (dto.crimeZoneUpazila > 0) cmbCrimeThana.Enabled = false;
            }

            if(dto.attachment != null)
            {
                if (dto.attachment.seizureList?.Count > 0)
                {
                    btnUploadSeizure.Enabled = false;
                    dgvSeizureList.Columns[2].Visible = false;
                }
                if (dto.attachment.complaintList?.Count > 0)
                {                    
                    btnComplainUpload.Enabled = false;
                    dgvComplainList.Columns[2].Visible = false;
                }
                if (dto.attachment.firList?.Count > 0)
                {
                    tbFIRNumber.Enabled = false;
                    dtpFIRDate.Enabled = false;
                    cmbFIRdistrict.Enabled = false;
                    cmbFIRthana.Enabled = false;
                    btnUploadFIR.Enabled = false;
                    dgvFirList.Columns[2].Visible = false;
                }
            }

            if (!string.IsNullOrEmpty(dto.investigatingOfficerName)) tbIOName.Enabled = false;
            if (!string.IsNullOrEmpty(dto.investigatingOfficerMobile)) tbIOMobile.Enabled = false;
            if (!string.IsNullOrEmpty(dto.investigatingOfficerBPNumber)) tbIOBPNo.Enabled = false;
            if (!string.IsNullOrEmpty(dto.iorank)) cmbIOrank.Enabled = false;

            if (!string.IsNullOrEmpty(dto.politicalGroup)) cmbPoliticalGroup.Enabled = false;

            if (!string.IsNullOrEmpty(dto.crimeInformation?.warrant?.warrantType)) cmbWarrantType.Enabled = false;
            if (!string.IsNullOrEmpty(dto.crimeInformation?.warrant?.warrantNo)) tbWarrantNo.Enabled = false;
            if (!string.IsNullOrEmpty(dto.crimeInformation?.warrant?.sectionNo)) tbSectionNo.Enabled = false;

            if (dto.crimeInformation?.recoveryList?.Count > 0)
            {
                btnAddRecovery.Enabled = false;
                //btnAddRecovery.BackColor = Color.IndianRed;
            }
        }

        private void ShowStaticProfileData(ProfileDto dto)
        {
            if (dto == null) return;

            if (!string.IsNullOrEmpty(dto.referenceNo)) tbRefNo.Text = dto.referenceNo;

            if (!string.IsNullOrEmpty(dto.arrestDate))
            {
                try
                {
                    var formatStrings = new string[] { "yyyy-MM-dd", "yyyy-MM-ddTHH:mm:ss.fffZ", "yyyy-MM-ddTHH:mm:ss.fffK" };
                    DateTime dtArrest = new DateTime();
                    DateTime.TryParseExact(dto.arrestDate, formatStrings, CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out dtArrest);
                    dtpArrestDate.Value = dtArrest;
                }
                catch { }
            }

            tbFullName.Text = dto.fullName;
            if (dto.nickName != null)
            {
                if (dto.nickName.Count > 0)
                {
                    nickNameList = dto.nickName;
                    for (int i = 0; i < dto.nickName.Count; i++)
                    {
                        tbNickName.Text += dto.nickName[i];
                        if (i < (dto.nickName.Count - 1))
                        {
                            tbNickName.Text += ",";
                        }
                    }
                }
            }
            if (dto.gender == "Male") rdBtnMale.Checked = true;
            else if (dto.gender == "Female") rdBtnFemale.Checked = true;
            else if (dto.gender == "Other") rdbtnOther.Checked = true;

            if (dto.nationality != null)
            {
                cmbNationality.SelectedIndex = -1;
                try
                {
                    cmbNationality.SelectedValue = dto.nationality?.id;
                    if (dto.nationality.id == 14)
                    {
                        tbPostCode.Enabled = false;
                        tbTown.Enabled = false;
                        tbState.Enabled = false;
                        cmbCountry.Enabled = false;

                        cmbDistrictPerm.Enabled = true;
                        cmbUpazillaPerm.Enabled = true;
                        cmbUnionPerm.Enabled = true;
                        tbVillagePerm.Enabled = true;

                        chkPresentPermanent.Enabled = true;
                    }
                    else if (dto.nationality?.id > 0 && dto.nationality?.id != 14)
                    {
                        tbPostCode.Enabled = true;
                        tbTown.Enabled = true;
                        tbState.Enabled = true;
                        cmbCountry.Enabled = true;

                        cmbDistrictPerm.Enabled = false;
                        cmbUpazillaPerm.Enabled = false;
                        cmbUnionPerm.Enabled = false;
                        tbVillagePerm.Enabled = false;

                        chkPresentPermanent.Enabled = false;
                    }
                }
                catch (Exception) { }
            }
            if (dto.mobile != null)
            {
                for (int i = 0; i < dto.mobile.Count; i++)
                {
                    tbPhone.Text = dto.mobile[i];
                    if (i != dto.mobile.Count - 1) tbPhone.Text += ", ";
                }
            }
            try
            {
                cmbReligion.SelectedValue = dto.religion;
            }
            catch (Exception) { }
            try
            {
                cmbMaritalStatus.SelectedValue = dto.maritalStatus;
                if (dto.maritalStatus == "Single")
                {
                    tbSpouseName.Enabled = false;
                    tbSpousePhone.Enabled = false;
                }
                else
                {
                    tbSpouseName.Enabled = true;
                    tbSpousePhone.Enabled = true;
                }
            }
            catch (Exception) { }
            try
            {
                cmbOccupation.SelectedValue = dto?.occupation;
            }
            catch { }

            if (dto.height?.ft > 0) tbHeightFeet.Text = dto.height?.ft?.ToString();
            if (dto.height?.inch > 0)   tbHeightInch.Text = dto.height?.inch?.ToString();

            if (!string.IsNullOrEmpty(dto.nid)) tbNID.Text = dto.nid;

            if (dto.familys?.Count > 0)
            {
                for (int i = 0; i < dto.familys?.Count; i++)
                {
                    if (dto.familys[i]?.relation == "father")
                    {
                        tbFatherName.Text = dto.familys[i].name;
                        tbFatherPhone.Text = dto.familys[i].phone;
                    }
                    else if (dto.familys[i]?.relation == "mother")
                    {
                        tbMotherName.Text = dto.familys[i].name;
                    }
                    else if (dto.familys[i]?.relation == "spouse")
                    {
                        tbSpouseName.Text = dto.familys[i].name;
                        tbSpousePhone.Text = dto.familys[i].phone;
                    }
                }
            }

            tbAge.Text = "" + dto.age;

            if (!string.IsNullOrEmpty(dto.dateOfBirth))
            {
                try
                {
                    var formatStrings = new string[] { "yyyy-MM-dd", "yyyy-MM-ddTHH:mm:ss.fffZ", "yyyy-MM-ddTHH:mm:ss.fffK" };
                    DateTime dtDOB = new DateTime();
                    DateTime.TryParseExact(dto.dateOfBirth, formatStrings, CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out dtDOB);

                    //DateTime DOB = DateTime.ParseExact(dto.dateOfBirth, "yyyy-MM-ddTHH:mm:ss.fffK", CultureInfo.InvariantCulture);
                    //dateOfBirth.Text = DOB.ToString("dd/MM/yyyy");
                    tbDOBDay.Text = dtDOB.Day.ToString();
                    if (tbDOBDay.Text.Length == 1) tbDOBDay.Text = "0" + dtDOB.Day.ToString();
                    tbDOBMonth.Text = dtDOB.Month.ToString();
                    if (tbDOBMonth.Text.Length == 1) tbDOBMonth.Text = "0" + dtDOB.Month.ToString();
                    tbDOBYear.Text = dtDOB.Year.ToString();

                    string dobStr = tbDOBDay.Text + "/" + tbDOBMonth.Text + "/" + tbDOBYear.Text;
                    string AgeInYears = CalculateAgeByDob(dobStr);
                    tbAge.Text = AgeInYears;
                }
                catch (Exception) { }
            }
            if (dto.educationalInformations != null)
            {
                eduList = dto.educationalInformations;
                if (dto.educationalInformations.Count > 0)
                {
                    EducationStatus = dto.educationalInformations[0].educationalStatus;
                    EducationInstitute = dto.educationalInformations[0].nameOfInstitution;
                    PoliticalInvolvementInInstitute = Convert.ToBoolean(dto.educationalInformations[0].politicalInvolvement);
                    EducationRemarks = dto.educationalInformations[0].remarks;
                }
                string eduInfo = null;
                for (int i = 0; i < dto.educationalInformations.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dto.educationalInformations[i].educationalStatus))
                        eduInfo += "Education Status: " + dto.educationalInformations[i].educationalStatus;
                    if (!string.IsNullOrEmpty(dto.educationalInformations[i].nameOfInstitution))
                    {
                        if (!string.IsNullOrEmpty(eduInfo))
                            eduInfo += ", Institute: " + dto.educationalInformations[i].nameOfInstitution;
                        else
                            eduInfo += "Institute: " + dto.educationalInformations[i].nameOfInstitution;
                    }
                    if (dto.educationalInformations[i].politicalInvolvement != null)
                    {
                        string PolInv = string.Empty;
                        if (dto.educationalInformations[i]?.politicalInvolvement == true) PolInv = "Yes";
                        else if (dto.educationalInformations[i]?.politicalInvolvement == false) PolInv = "No";
                        
                        if (!string.IsNullOrEmpty(eduInfo))
                            eduInfo += ", Poltical Involvement: " + PolInv;
                        else
                            eduInfo += "Poltical Involvement: " + PolInv;
                    }
                    if (!string.IsNullOrEmpty(dto.educationalInformations[i].remarks))
                    {
                        if (!string.IsNullOrEmpty(eduInfo))
                            eduInfo += ", Remarks: " + dto.educationalInformations[i].remarks + ". ";
                        else
                            eduInfo += "Remarks: " + dto.educationalInformations[i].remarks + ". ";
                    }
                    tbEducation.Text = eduInfo;
                }
            }
            tbIdentificationMark.Text = dto.identificationMark;

            // Setting Address information
            if (dto.presentAddress?.district != null)
            {
                if (dto.presentAddress?.district > 0)
                {
                    try
                    {
                        cmbDistrict.SelectedValue = dto.presentAddress?.district;
                        cmbUpazilla_Enter(null, null);
                    }
                    catch (Exception) { }
                }
            }
            if (dto.presentAddress?.upazila != null)
            {
                if (dto.presentAddress?.district > 0 && dto.presentAddress?.upazila > 0)
                {
                    try
                    {
                        cmbUpazilla.SelectedValue = dto.presentAddress?.upazila;
                        cmbUnion_Enter(null, null);
                    }
                    catch (Exception) { }
                }
            }
            if (dto.presentAddress?.union != null)
            {
                if (dto.presentAddress?.district > 0 && dto.presentAddress?.upazila > 0 && dto.presentAddress?.union > 0)
                {
                    try
                    {
                        cmbUnion.SelectedValue = dto.presentAddress?.union;
                    }
                    catch (Exception) { }
                }
            }
            tbVillageRoadHouse.Text = dto.presentAddress?.villageHouseRoadNo;

            if (dto.permanentAddress?.district != null)
            {
                if (dto.permanentAddress?.district > 0)
                {
                    try
                    {
                        cmbDistrictPerm.SelectedValue = dto.permanentAddress?.district;
                        cmbUpazillaPerm_Enter(null, null);
                    }
                    catch (Exception) { }
                }
            }
            if (dto.permanentAddress?.upazila != null)
            {
                if (dto.permanentAddress?.district > 0 && dto.permanentAddress?.upazila > 0)
                {
                    try
                    {
                        cmbUpazillaPerm.SelectedValue = dto.permanentAddress?.upazila;
                        cmbUnionPerm_Enter(null, null);
                    }
                    catch (Exception) { }
                }
            }
            if (dto.permanentAddress?.union != null)
            {
                if (dto.permanentAddress?.district > 0 && dto.permanentAddress?.upazila > 0 && dto.permanentAddress?.union > 0)
                {
                    try
                    {
                        cmbUnionPerm.SelectedValue = dto.permanentAddress?.union;
                    }
                    catch (Exception) { }
                }
            }
            tbVillagePerm.Text = dto.permanentAddress?.villageHouseRoadNo;

            tbPostCode.Text = dto.foreignAddress?.zipOrPostCode;
            tbTown.Text = dto.foreignAddress?.town;
            tbState.Text = dto.foreignAddress?.state;
            if (dto.foreignAddress?.country != null)
            {
                if (dto.foreignAddress?.country > 0)
                {
                    try
                    {
                        cmbCountry.SelectedValue = dto.foreignAddress?.country;
                    }
                    catch (Exception) { }
                }
            }

            // Setting static crime information
            if (dto.attachment != null)
            {
                if (dto.attachment.firList != null)
                {
                    if (dto.attachment.firList.Count > 0)
                    {
                        firList = dto.attachment.firList;

                        tbFIRNumber.Text = dto.attachment.firList[0].firNo;

                        try
                        {
                            if (dto.attachment.firList[0].district > 0)
                            {
                                cmbFIRdistrict.SelectedValue = dto.attachment.firList[0].district;
                                if (dto.attachment.firList[0].upozilaOrThana > 0)
                                {
                                    cmbFIRthana_Enter(null, null);
                                    cmbFIRthana.SelectedValue = dto.attachment.firList[0].upozilaOrThana;
                                }
                            }
                        }
                        catch (Exception) { }

                        try
                        {
                            //DateTime firDate = DateTime.ParseExact(dto.firs[0]?.firDate, "yyyy-MM-ddTHH:mm:ss.fffZ", System.Globalization.CultureInfo.InvariantCulture);
                            ////DateTime firDate = new DateTime(long.Parse(dto.attachment.firList[0]?.firDate));  Changing as server is changing date format
                            //dtpFIRDate.Text = firDate.ToString("yyyy-MM-dd");

                            var formatStrings = new string[] { "yyyy-MM-dd", "yyyy-MM-ddTHH:mm:ss.fffZ", "yyyy-MM-ddTHH:mm:ss.fffK" };
                            DateTime dtFIR = new DateTime();
                            DateTime.TryParseExact(dto.firs[0]?.firDate, formatStrings, CultureInfo.InvariantCulture, DateTimeStyles.None, out dtFIR);
                            dtpFIRDate.Value = dtFIR;
                        }
                        catch { }

                        dgvFirList.Rows.Clear();
                        for (int i = 0; i < StaticData.Enrollment?.profile?.attachment?.firList?.Count; i++)
                        {
                            dgvFirList.Rows.Add("FIR-" + (i + 1), "VIEW", "DELETE");
                        }
                    }
                }

                if (dto.attachment.complaintList?.Count > 0)
                {
                    complainList = dto.attachment.complaintList;
                    dgvComplainList.Rows.Clear();
                    for (int i = 0; i < StaticData.Enrollment?.profile?.attachment?.complaintList?.Count; i++)
                    {
                        dgvComplainList.Rows.Add("Complain-" + (i + 1), "VIEW", "DELETE");
                    }
                }

                if (dto.attachment.seizureList?.Count > 0)
                {
                    seizureList = dto.attachment.seizureList;
                    dgvSeizureList.Rows.Clear();
                    for (int i = 0; i < StaticData.Enrollment?.profile?.attachment?.seizureList?.Count; i++)
                    {
                        dgvSeizureList.Rows.Add("Seizure-" + (i + 1), "VIEW", "DELETE");
                    }
                }
            }            
            
            tbIOBPNo.Text = dto.investigatingOfficerBPNumber;
            tbIOMobile.Text = dto.investigatingOfficerMobile;
            tbIOName.Text = dto.investigatingOfficerName;
            if (!string.IsNullOrEmpty(dto.iorank))
            {
                try
                {
                    cmbIOrank.SelectedValue = dto.iorank;
                }
                catch { }
            }            

            tbGroupGangName.Text = dto.crimeInformation?.groupOrGangName;

            if (!string.IsNullOrEmpty(dto.politicalGroup))
            {
                try
                {
                    cmbPoliticalGroup.SelectedValue = dto.politicalGroup;
                }
                catch { }
            }

            if (dto.crimeInformation?.crimeHistorys != null)
            {
                if (dto.crimeInformation.crimeHistorys.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dto.crimeInformation.crimeHistorys[0].dateOfCrime))
                    {
                        try
                        {
                            var formatStrings = new string[] { "yyyy-MM-dd", "yyyy-MM-ddTHH:mm:ss.fffZ", "yyyy-MM-ddTHH:mm:ss.fffK" };
                            DateTime dtCrime = new DateTime();
                            DateTime.TryParseExact(dto.crimeInformation.crimeHistorys[0].dateOfCrime, formatStrings, CultureInfo.InvariantCulture,
                                DateTimeStyles.None, out dtCrime);
                            dtpDateOfCrime.Value = dtCrime;
                        }
                        catch { }
                    }
                }                
            }

            if (dto.crimeInformation?.crimeType != null)
            {
                if (dto.crimeInformation?.crimeType > 0)
                {
                    try
                    {
                        cmbCrimeType.SelectedValue = dto.crimeInformation?.crimeType;
                    }
                    catch (Exception) { }
                }
            }

            // Arested By Section

            tbArrestedByOfficerName.Text = dto.arrestedBy;

            if (dto?.unit != null)
            {
                if (dto?.unit >= 0)
                {
                    try
                    {
                        cmbBattalion.SelectedValue = dto?.unit;

                        if (dto.unit == 0)
                        {
                            lblSubUnitMandatory.Visible = false;
                        }
                        else if (dto.unit > 0)
                        {
                            lblSubUnitMandatory.Visible = true;
                        }
                    }
                    catch (Exception) { }
                }
            }

            if (dto.subUnit != null)
            {
                if (dto.unit > 0 && dto.subUnit > 0)
                {
                    try
                    {
                        cmbSubStation_Enter(null, null);
                        cmbSubStation.SelectedValue = dto.subUnit;
                    }
                    catch { }
                }
            }

            // Place of Crime Section

            if (dto.crimeInformation?.crimeZone?.district != null)
            {
                if (dto.crimeInformation?.crimeZone?.district > 0)
                {
                    try
                    {
                        cmbCrimeDistrict_Enter(null, null);
                        cmbCrimeDistrict.SelectedValue = dto.crimeInformation?.crimeZone?.district;
                    }
                    catch (Exception) { }
                }
            }
            if (dto.crimeInformation?.crimeZone?.upozilaOrThana != null)
            {
                if (dto.crimeInformation?.crimeZone?.district > 0 && dto.crimeInformation?.crimeZone?.upozilaOrThana > 0)
                {
                    try
                    {
                        cmbCrimeThana_Enter(null, null);
                        cmbCrimeThana.SelectedValue = dto.crimeInformation?.crimeZone?.upozilaOrThana;
                    }
                    catch (Exception) { }
                }
            }

            if (dto.crimeInformation?.crimeZone?.union != null)
            {
                if (dto.crimeInformation?.crimeZone?.district > 0 && 
                    dto.crimeInformation?.crimeZone?.upozilaOrThana > 0 &&
                    dto.crimeInformation?.crimeZone?.union > 0)
                {
                    try
                    {
                        cmbCrimeUnion_Enter(null, null);
                        cmbCrimeUnion.SelectedValue = dto.crimeInformation?.crimeZone?.union;
                    }
                    catch (Exception) { }
                }
            }

            tbCrimeVillage.Text = dto.crimeInformation?.crimeZone?.addressLine;

            //if (dto.crimeZoneDistrict != null)
            //{
            //    if (dto.unit >= 0 && dto.subUnit > 0 && dto.crimeZoneDistrict > 0)
            //    {
            //        try
            //        {
            //            cmbCrimeDistrict_Enter(null, null);
            //            cmbCrimeDistrict.SelectedValue = dto.crimeZoneDistrict;
            //        }
            //        catch (Exception) { }
            //    }
            //}
            //if (dto.crimeZoneUpazila != null)
            //{
            //    if (dto.unit >= 0 && dto.subUnit > 0 && dto.crimeZoneDistrict > 0 && dto.crimeZoneUpazila > 0)
            //    {
            //        try
            //        {
            //            cmbCrimeThana_Enter(null, null);
            //            cmbCrimeThana.SelectedValue = dto.crimeZoneUpazila;
            //        }
            //        catch (Exception) { }
            //    }
            //}

            if (!string.IsNullOrEmpty(dto.crimeInformation?.warrant?.warrantType)
                || !string.IsNullOrEmpty(dto.crimeInformation?.warrant?.warrantNo)
                || !string.IsNullOrEmpty(dto.crimeInformation?.warrant?.sectionNo))
            {
                // chkWarrant.Checked = true;
                cmbWarrantType.Visible = true;
                tbWarrantNo.Visible = true;
                tbSectionNo.Visible = true;
            }

            if (!string.IsNullOrEmpty(dto.crimeInformation?.warrant?.warrantType))
            {
                try
                {                    
                    cmbWarrantType.SelectedValue = dto.crimeInformation?.warrant?.warrantType;
                }
                catch { }
            }
            tbWarrantNo.Text = dto.crimeInformation?.warrant?.warrantNo;
            tbSectionNo.Text = dto.crimeInformation?.warrant?.sectionNo;

            string recoveries = string.Empty;
            for (int i = 0; i < dto.crimeInformation?.recoveryList?.Count; i++)
            {
                recoveries += "("+(i+1)+") Type: " + dto.crimeInformation?.recoveryList[i].recoveryType +
                    ", Name: " + dto.crimeInformation?.recoveryList[i].recoveryItemName +
                    ", Amount: " + dto.crimeInformation?.recoveryList[i].amount + ". ";
            }
            tbRecovery.Text = recoveries;
        }

        private void LoadLookupItems()
        {
            LookupItems lookupItems = new LookupItems();
            lookupItems.LoadNationality();
            lookupItems.LoadDistrict();
            lookupItems.LoadStations();
            lookupItems.LoadForeignNationality();
            lookupItems.LoadCrimeType();

            if (lookupItems.crimeTypeList != null)
            {
                if (lookupItems.crimeTypeList.Count > 0)
                {
                    cmbCrimeType.DataSource = new BindingSource(lookupItems.crimeTypeList, null);
                    cmbCrimeType = Utils.SuggestComboBoxFormat(cmbCrimeType, 1);
                }
            }

            if (lookupItems.nationalityList != null)
            {
                if (lookupItems.nationalityList.Count > 0)
                {
                    //cmbNationality.DataSource = new BindingSource(lookupItems.nationalityList, null);
                    //cmbNationality.DisplayMember = "Value";
                    //cmbNationality.ValueMember = "Key";
                    //cmbNationality.SelectedIndex = -1;

                    // New code by Al-Amin for custom editable drop down
                    cmbNationality.DataSource = new BindingSource(lookupItems.nationalityList, null);
                    cmbNationality = Utils.SuggestComboBoxFormat(cmbNationality, 1);

                    cmbCountry.DataSource = new BindingSource(lookupItems.foreignNationalityList, null);
                    cmbCountry = Utils.SuggestComboBoxFormat(cmbCountry, 1);
                }
            }

            if (lookupItems.districtList != null)
            {
                if (lookupItems.districtList.Count > 0)
                {
                    cmbDistrict.DataSource = new BindingSource(lookupItems.districtList, null);
                    cmbDistrict = Utils.SuggestComboBoxFormat(cmbDistrict, 1);

                    cmbDistrictPerm.DataSource = new BindingSource(lookupItems.districtList, null);
                    cmbDistrictPerm = Utils.SuggestComboBoxFormat(cmbDistrictPerm, 1);

                    cmbCrimeDistrict.DataSource = new BindingSource(lookupItems.districtList, null);
                    cmbCrimeDistrict = Utils.SuggestComboBoxFormat(cmbCrimeDistrict, 1);

                    cmbFIRdistrict.DataSource = new BindingSource(lookupItems.districtList, null);
                    cmbFIRdistrict = Utils.SuggestComboBoxFormat(cmbFIRdistrict, 1);
                }
            }

            if (lookupItems.stationList != null)
            {
                if (lookupItems.stationList.Count > 0)
                {
                    cmbBattalion.DataSource = new BindingSource(lookupItems.stationList, null);
                    cmbBattalion = Utils.SuggestComboBoxFormat(cmbBattalion, 1);
                }
            }
        }

        private void LoadComboItems()
        {
            LoadLookupItems();

            cmbReligion.DataSource = new BindingSource(ComboBoxItems.religion, null);
            cmbReligion = Utils.SuggestComboBoxFormat(cmbReligion, 0);

            cmbMaritalStatus.DataSource = new BindingSource(ComboBoxItems.maritalStatus, null);
            cmbMaritalStatus = Utils.SuggestComboBoxFormat(cmbMaritalStatus, 0);

            cmbOccupation.DataSource = new BindingSource(ComboBoxItems.occupation, null);
            cmbOccupation = Utils.SuggestComboBoxFormat(cmbOccupation, 0);

            //cmbCrimeType.DataSource = new BindingSource(ComboBoxItems.crimeType, null);
            //cmbCrimeType = Utils.SuggestComboBoxFormat(cmbCrimeType, 0);

            cmbPoliticalGroup.DataSource = new BindingSource(ComboBoxItems.politicalGroup, null);
            cmbPoliticalGroup = Utils.SuggestComboBoxFormat(cmbPoliticalGroup, 0);

            cmbWarrantType.DataSource = new BindingSource(ComboBoxItems.warrantType, null);
            cmbWarrantType.DisplayMember = "Value";
            cmbWarrantType.ValueMember = "Key";
            cmbWarrantType.SelectedIndex = -1;

            cmbIOrank.DataSource = new BindingSource(ComboBoxItems.ioRank, null);
            cmbIOrank = Utils.SuggestComboBoxFormat(cmbIOrank, 0);
        }

        private void btnOtherInfo_Click(object sender, EventArgs e)
        {
            OnSaveBasicInfo();
            OnSaveAddress();
            OnSaveCrimeInfo();
            //if (tabControlCriminalProfile.SelectedIndex == 0)
            //{
            //    OnSaveBasicInfo();
            //    OnSaveAddress();
            //}
            //else if (tabControlCriminalProfile.SelectedIndex == 1)
            //{
            //    OnSaveCrimeInfo();
            //}
            ((CriminalProfileController)controller).OtherInfo();
        }

        private void btnBiometric_Click(object sender, EventArgs e)
        {
            OnSaveBasicInfo();
            OnSaveAddress();
            OnSaveCrimeInfo();
            //if (tabControlCriminalProfile.SelectedIndex == 0)
            //{
            //    OnSaveBasicInfo();
            //    OnSaveAddress();
            //}
            //else if (tabControlCriminalProfile.SelectedIndex == 1)
            //{
            //    OnSaveCrimeInfo();
            //}
            ((CriminalProfileController)controller).Biometric();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            ((CriminalProfileController)controller).CriminalProfile();
        }

        public List<string> nickNameList = new List<string>();
        private void btnNickNameAdd_Click(object sender, EventArgs e)
        {
            var form = new AddNickNameDialogForm();
            DialogResult dr = form.ShowDialog();

            if (dr == DialogResult.OK)
            {
                tbNickName.Text = null;
                nickNameList = form.nickNameList;

                StaticData.Enrollment.profile.nickName = (nickNameList?.Count >= 0) ? nickNameList : null;
                string nickNames = string.Empty;
                for (int i = 0; i < nickNameList.Count; i++)
                {
                    tbNickName.Text += nickNameList[i];
                    if (i != nickNameList.Count - 1) tbNickName.Text += ", ";
                    nickNames += nickNameList[i];
                    if (i != nickNameList.Count - 1) nickNames += ", ";
                }
                dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "nick_name", -1, nickNames);
            }
        }

        private void btnPreviewSubmit_Click(object sender, EventArgs e)
        {
            OnSaveBasicInfo();
            OnSaveAddress();
            OnSaveCrimeInfo();
            ((CriminalProfileController)controller).PreviewSubmit();
        }

        private void btnAddNickName_Click(object sender, EventArgs e)
        {
            var form = new AddNickNameDialogForm();
            DialogResult dr = form.ShowDialog();
        }

        List<EducationInfoDto> eduList = new List<EducationInfoDto>();
        public string EducationStatus;
        public string EducationInstitute;
        public bool PoliticalInvolvementInInstitute;
        public string EducationRemarks;
        private void btnAddEducation_Click(object sender, EventArgs e)
        {
            var form = new AddEducationDialogForm();
            DialogResult dr = form.ShowDialog();

            if (dr == DialogResult.OK)
            {
                string eduInfo = string.Empty;

                if (!string.IsNullOrEmpty(form.EducationStatusValue)) eduInfo += "Education Status: " + form.EducationStatusValue;
                if (!string.IsNullOrEmpty(form.EducationInstitute)) 
                {
                    if (!string.IsNullOrEmpty(eduInfo)) eduInfo += ", Institute: " + form.EducationInstitute;
                    else eduInfo += "Institute: " + form.EducationInstitute;
                }
                if (!string.IsNullOrEmpty(eduInfo))     eduInfo += ", Political Involvement: " + ((form.PoliticalInvolvementInInstitute) ? "Yes" : "No");
                else    eduInfo += "Political Involvement: " + ((form.PoliticalInvolvementInInstitute) ? "Yes" : "No");
                if (!string.IsNullOrEmpty(form.EducationRemarks))    eduInfo += ", Remarks: " + form.EducationRemarks + ". ";
                else
                {
                    eduInfo += "Remarks: " + form.EducationRemarks + ". ";
                }

                tbEducation.Text += eduInfo;

                EducationStatus = form.EducationStatusKey;
                EducationInstitute = form.EducationInstitute;
                PoliticalInvolvementInInstitute = form.PoliticalInvolvementInInstitute;
                EducationRemarks = form.EducationRemarks;

                EducationInfoDto edu = new EducationInfoDto();
                edu.educationalStatus = EducationStatus;
                edu.nameOfInstitution = EducationInstitute;
                edu.politicalInvolvement = PoliticalInvolvementInInstitute;
                edu.remarks = EducationRemarks;

                eduList.Add(edu);
                StaticData.Enrollment.profile.educationalInformations = eduList;

                var jsonEdu = new JavaScriptSerializer().Serialize(eduList);
                dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "education_information", -1, jsonEdu);
            }
        }

        private void OnSaveBasicInfo()
        {
            StaticData.Enrollment.profile.referenceNo = (!string.IsNullOrEmpty(tbRefNo.Text)) ? tbRefNo.Text : null;

            if (!string.IsNullOrEmpty(dtpArrestDate.Text))
            {
                try
                {
                    DateTime dateOfArrest = DateTime.ParseExact(dtpArrestDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    StaticData.Enrollment.profile.arrestDate = dateOfArrest.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                }
                catch (Exception) { }
            }

            StaticData.Enrollment.profile.fullName = (!string.IsNullOrEmpty(tbFullName.Text)) ? tbFullName.Text : null;
            StaticData.Enrollment.profile.nickName = (nickNameList?.Count >= 0) ? nickNameList : null;

            if (rdBtnMale.Checked) StaticData.Enrollment.profile.gender = "Male";
            if (rdBtnFemale.Checked) StaticData.Enrollment.profile.gender = "Female";
            if (rdbtnOther.Checked) StaticData.Enrollment.profile.gender = "Other";

            if (!string.IsNullOrEmpty(tbNID.Text)) StaticData.Enrollment.profile.nid = tbNID.Text;

            if (!string.IsNullOrEmpty(tbDOBDay.Text) && !string.IsNullOrEmpty(tbDOBMonth.Text) && !string.IsNullOrEmpty(tbDOBYear.Text))
            {
                try
                {
                    string dobStr = tbDOBDay.Text + "/" + tbDOBMonth.Text + "/" + tbDOBYear.Text;
                    DateTime DOB = DateTime.ParseExact(dobStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    StaticData.Enrollment.profile.dateOfBirth = DOB.ToString("yyyy-MM-dd");
                }
                catch (Exception)
                {

                }
            }
            StaticData.Enrollment.profile.age = (!string.IsNullOrEmpty(tbAge.Text)) ? Convert.ToInt32(tbAge.Text) : 0;

            if (!string.IsNullOrEmpty(tbIOMobile.Text))
            {
                string[] mobileList = tbIOMobile.Text.Split(',');
                StaticData.Enrollment.profile.mobile = new List<string>();
                StaticData.Enrollment.profile.mobile = mobileList.ToList();
            }

            NationalityProfileDto nationalityDto = new NationalityProfileDto();
            nationalityDto.id = (!string.IsNullOrEmpty(cmbNationality.SelectedValue?.ToString())) ? Convert.ToInt32(cmbNationality.SelectedValue?.ToString()) : 0;
            //nationalityDto.NationalityName = (!string.IsNullOrEmpty(cmbNationality.SelectedValue?.ToString())) ? cmbNationality.Text : null;

            StaticData.Enrollment.profile.nationality = nationalityDto;
            if (cmbReligion.SelectedIndex > 0)
            {
                StaticData.Enrollment.profile.religion = (!string.IsNullOrEmpty(cmbReligion.SelectedValue?.ToString())) ? cmbReligion.SelectedValue?.ToString() : null;
            }
            if (cmbMaritalStatus.SelectedIndex > 0)
            {
                StaticData.Enrollment.profile.maritalStatus = (!string.IsNullOrEmpty(cmbMaritalStatus.SelectedValue?.ToString())) ? cmbMaritalStatus.SelectedValue?.ToString() : null;
            }

            HeightDto heightDto = new HeightDto();
            heightDto.ft = (!string.IsNullOrEmpty(tbHeightFeet.Text)) ? Convert.ToDouble(tbHeightFeet.Text) : 0;
            heightDto.inch = (!string.IsNullOrEmpty(tbHeightInch.Text)) ? Convert.ToDouble(tbHeightInch.Text) : 0;
            StaticData.Enrollment.profile.height = heightDto;

            WeightDto weightDto = new WeightDto();
            StaticData.Enrollment.profile.weight = weightDto;

            StaticData.Enrollment.profile.occupation = cmbOccupation.SelectedValue?.ToString();

            //List<EducationInfoDto> educationInfoDto = new List<EducationInfoDto>();
            //educationInfoDto.Add(new EducationInfoDto(EducationStatus, EducationInstitute, PoliticalInvolvementInInstitute, EducationRemarks));
            //StaticData.Enrollment.profile.educationalInformations = educationInfoDto;

            StaticData.Enrollment.profile.identificationMark = (!string.IsNullOrEmpty(tbIdentificationMark.Text)) ? tbIdentificationMark.Text : null;

            List<FamilyDto> familyList = new List<FamilyDto>();
            if (!string.IsNullOrEmpty(tbFatherName.Text) || !string.IsNullOrEmpty(tbFatherPhone.Text))
            {
                familyList.Add(new FamilyDto { name = tbFatherName.Text, phone = tbFatherPhone.Text, relation = "father" });
            }
            if (!string.IsNullOrEmpty(tbMotherName.Text))
            {
                familyList.Add(new FamilyDto { name = tbMotherName.Text, relation = "mother" });
            }
            if (!string.IsNullOrEmpty(tbSpouseName.Text) || !string.IsNullOrEmpty(tbSpousePhone.Text))
            {
                familyList.Add(new FamilyDto { name = tbSpouseName.Text, phone = tbSpousePhone.Text, relation = "spouse" });
            }
            if (familyList.Count > 0) StaticData.Enrollment.profile.familys = familyList;
        }


        private void btnSaveNext_Click(object sender, EventArgs e)
        {
            OnSaveBasicInfo();
            OnSaveAddress();
            ProcessingDialog.Run(delegate ()
            {
                dbEnrollClientManager.AddCriminalProfile(StaticData.Enrollment, Globals.RecordState.DRAFT);
            });
            tabControlCriminalProfile.SelectedIndex = 1;
            try
            {                
                if (!(StaticData.Enrollment.profile.unit >= 0))
                {
                    cmbBattalion.SelectedValue = Users.Unit;
                }
                if (!(StaticData.Enrollment?.profile?.subUnit > 0))
                {
                    cmbSubStation_Enter(null, null);
                    cmbSubStation.SelectedValue = Users.SubUnit;
                }
            }
            catch { }
        }

        private void OnSaveAddress()
        {
            AddressDto presentAddressDto = new AddressDto();
            presentAddressDto.district = (!string.IsNullOrEmpty(cmbDistrict.SelectedValue?.ToString())) ? Convert.ToInt32(cmbDistrict.SelectedValue?.ToString()) : 0;
            presentAddressDto.upazila = (!string.IsNullOrEmpty(cmbUpazilla.SelectedValue?.ToString())) ? Convert.ToInt32(cmbUpazilla.SelectedValue?.ToString()) : 0;
            presentAddressDto.union = (!string.IsNullOrEmpty(cmbUnion.SelectedValue?.ToString())) ? Convert.ToInt32(cmbUnion.SelectedValue?.ToString()) : 0;
            presentAddressDto.villageHouseRoadNo = (!string.IsNullOrEmpty(tbVillageRoadHouse.Text)) ? tbVillageRoadHouse.Text : null;

            StaticData.Enrollment.profile.presentAddress = presentAddressDto;

            AddressDto permanentAddressDto = new AddressDto();
            permanentAddressDto.district = (!string.IsNullOrEmpty(cmbDistrictPerm.SelectedValue?.ToString())) ? Convert.ToInt32(cmbDistrictPerm.SelectedValue?.ToString()) : 0;
            permanentAddressDto.upazila = (!string.IsNullOrEmpty(cmbUpazillaPerm.SelectedValue?.ToString())) ? Convert.ToInt32(cmbUpazillaPerm.SelectedValue?.ToString()) : 0;
            permanentAddressDto.union = (!string.IsNullOrEmpty(cmbUnionPerm.SelectedValue?.ToString())) ? Convert.ToInt32(cmbUnionPerm.SelectedValue?.ToString()) : 0;
            permanentAddressDto.villageHouseRoadNo = (!string.IsNullOrEmpty(tbVillagePerm.Text)) ? tbVillagePerm.Text : null;

            StaticData.Enrollment.profile.permanentAddress = permanentAddressDto;

            ForeignAddressDto foreignAddressDto = new ForeignAddressDto();
            foreignAddressDto.zipOrPostCode = (!string.IsNullOrEmpty(tbPostCode.Text)) ? tbPostCode.Text : null;
            foreignAddressDto.town = (!string.IsNullOrEmpty(tbTown.Text)) ? tbTown.Text : null;
            foreignAddressDto.state = (!string.IsNullOrEmpty(tbState.Text)) ? tbState.Text : null;
            foreignAddressDto.country = (!string.IsNullOrEmpty(cmbCountry.SelectedValue?.ToString())) ? Convert.ToInt32(cmbCountry.SelectedValue?.ToString()) : 0;

            StaticData.Enrollment.profile.foreignAddress = foreignAddressDto;
        }

        private void OnSaveCrimeInfo()
        {
            StaticData.Enrollment.profile.investigatingOfficerBPNumber = (!string.IsNullOrEmpty(tbIOBPNo.Text)) ? tbIOBPNo.Text : null;
            StaticData.Enrollment.profile.investigatingOfficerMobile = (!string.IsNullOrEmpty(tbIOMobile.Text)) ? tbIOMobile.Text : null;
            StaticData.Enrollment.profile.investigatingOfficerName = (!string.IsNullOrEmpty(tbIOName.Text)) ? tbIOName.Text : null;
            StaticData.Enrollment.profile.iorank = (!string.IsNullOrEmpty(cmbIOrank.SelectedValue?.ToString())) ? cmbIOrank.SelectedValue?.ToString() : null;
            StaticData.Enrollment.profile.arrestedBy = (!string.IsNullOrEmpty(tbArrestedByOfficerName.Text)) ? tbArrestedByOfficerName.Text : null;

            CrimeInformationDto crimeInformationDto = new CrimeInformationDto();
            crimeInformationDto.criminal_id = StaticData.Enrollment?.profile?.crimeInformation?.criminal_id;
            crimeInformationDto.recoveryList = StaticData.Enrollment?.profile?.crimeInformation?.recoveryList;

            crimeInformationDto.referenceNo = tbRefNo.Text;

            crimeInformationDto.groupOrGangName = (!string.IsNullOrEmpty(tbGroupGangName.Text)) ? tbGroupGangName.Text : null;

            //IllegalArmsPossessionDto illegalArmsPossessionDto = new IllegalArmsPossessionDto();

            //crimeInformationDto.illegalArmsPossession = illegalArmsPossessionDto;
            crimeInformationDto.crimeType = (!string.IsNullOrEmpty(cmbCrimeType.SelectedValue?.ToString())) ? Convert.ToInt32(cmbCrimeType.SelectedValue?.ToString()) : -1;
            //crimeInformationDto.crimeTypeName = (!string.IsNullOrEmpty(cmbCrimeType.SelectedValue?.ToString())) ? cmbCrimeType.Text : null;
            //crimeInformationDto.latestState = (!string.IsNullOrEmpty(cmbLatestState.SelectedValue?.ToString())) ? cmbLatestState.SelectedValue?.ToString() : null;  //Not using for now

            //ActivityDto activityDto = new ActivityDto();
            //crimeInformationDto.activities = activityDto;

            CrimeZoneDto crimeZoneDto = OnSaveCrimeZone();
            crimeInformationDto.crimeZone = crimeZoneDto;

            if (!string.IsNullOrEmpty(dtpDateOfCrime.Text))
            {
                CrimeHistoryDto crimeHistory = new CrimeHistoryDto();
                try
                {
                    DateTime dateOfCrime = DateTime.ParseExact(dtpDateOfCrime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    crimeHistory.dateOfCrime = dateOfCrime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                    crimeInformationDto.crimeHistorys.Add(crimeHistory);
                }
                catch { }
            }
            else if (StaticData.Enrollment?.profile?.crimeInformation?.crimeHistorys?.Count > 0)
            {
                crimeInformationDto.crimeHistorys = StaticData.Enrollment?.profile?.crimeInformation?.crimeHistorys;
            }

            //crimeInformationDto.crimeHistorys.Add(crimeHistory);

            StaticData.Enrollment.profile.unit = Convert.ToInt32(cmbBattalion.SelectedValue?.ToString());
            StaticData.Enrollment.profile.subUnit = Convert.ToInt32(cmbSubStation.SelectedValue?.ToString());
            //StaticData.Enrollment.profile.subUnitName = (!string.IsNullOrEmpty(cmbSubStation.SelectedValue?.ToString())) ? cmbSubStation.Text : null;

            StaticData.Enrollment.profile.crimeInformation = crimeInformationDto;

            //StaticData.Enrollment.profile.crimeZoneDistrict = Convert.ToInt32(cmbCrimeDistrict.SelectedValue?.ToString());
            //StaticData.Enrollment.profile.crimeZoneUpazila = Convert.ToInt32(cmbCrimeThana.SelectedValue?.ToString());

            ArrestInfoDto arrestInfoDto = new ArrestInfoDto();
            //arrestInfoDto.district = (!string.IsNullOrEmpty(cmbCrimeDistrict.SelectedValue?.ToString())) ? Convert.ToInt32(cmbCrimeDistrict.SelectedValue?.ToString()) : 0;
            //arrestInfoDto.upozilaOrThana = (!string.IsNullOrEmpty(cmbCrimeThana.SelectedValue?.ToString())) ? Convert.ToInt32(cmbCrimeThana.SelectedValue?.ToString()) : 0;
            try
            {
                DateTime dateOfArrest = DateTime.ParseExact(dtpArrestDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                arrestInfoDto.dateOfArrest = dateOfArrest.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            }
            catch (Exception) { }

            List<ArrestInfoDto> arrestInfoList = new List<ArrestInfoDto>();
            arrestInfoList.Add(arrestInfoDto);
            StaticData.Enrollment.profile.arrestInfos = arrestInfoList;

            StaticData.Enrollment.profile.politicalGroup = (!string.IsNullOrEmpty(cmbPoliticalGroup.SelectedValue?.ToString()))
                ? cmbPoliticalGroup.SelectedValue?.ToString() : null;

            if (StaticData.Enrollment.profile.crimeInformation.warrant == null)
            {
                StaticData.Enrollment.profile.crimeInformation.warrant = new WarrantDto();
            }
            StaticData.Enrollment.profile.crimeInformation.warrant.warrantType =
                (!string.IsNullOrEmpty(cmbWarrantType.SelectedValue?.ToString())) ? cmbWarrantType.SelectedValue?.ToString() : null;
            StaticData.Enrollment.profile.crimeInformation.warrant.warrantNo =
                (!string.IsNullOrEmpty(tbWarrantNo.Text)) ? tbWarrantNo.Text : null;
            StaticData.Enrollment.profile.crimeInformation.warrant.sectionNo =
                (!string.IsNullOrEmpty(tbSectionNo.Text)) ? tbSectionNo.Text : null;
        }

        private void btnSaveNextCrimeInfo_Click(object sender, EventArgs e)
        {
            OnSaveCrimeInfo();
            ProcessingDialog.Run(delegate ()
            {
                dbEnrollClientManager.AddCriminalProfile(StaticData.Enrollment, Globals.RecordState.DRAFT);
            });
            btnOtherInfo.Focus();
            btnOtherInfo_Click(null, null);
            
        }

        private void tabControlCriminalProfile_Deselected(object sender, TabControlEventArgs e)
        {
        }

        private void tabControlCriminalProfile_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            if (tabControlCriminalProfile.SelectedIndex == 0)
            {
                OnSaveBasicInfo();
                OnSaveAddress();
                //OnSaveCrimeInfo();
            }
            if (tabControlCriminalProfile.SelectedIndex == 1)
            {
                OnSaveCrimeInfo();
            }
        }

        private void chkPresentPermanent_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chkPresentPermanent_Click(object sender, EventArgs e)
        {
            if (chkPresentPermanent.Checked)
            {
                if (cmbDistrict.SelectedValue != null)
                {
                    cmbDistrictPerm.Focus();
                    cmbDistrictPerm.Text = cmbDistrict.SelectedText;
                    cmbDistrictPerm.SelectedValue = cmbDistrict.SelectedValue;
                }
                if (cmbDistrict.SelectedValue != null && cmbUpazilla.SelectedValue != null)
                {
                    cmbUpazillaPerm_Enter(null, null);
                    cmbUpazillaPerm.Text = cmbUpazilla.SelectedText;
                    cmbUpazillaPerm.SelectedValue = cmbUpazilla.SelectedValue;
                }
                if (cmbDistrict.SelectedValue != null && cmbUpazilla.SelectedValue != null && cmbUnion.SelectedValue != null)
                {
                    cmbUnionPerm_Enter(null, null);
                    cmbUnionPerm.Text = cmbUnion.SelectedText;
                    cmbUnionPerm.SelectedValue = cmbUnion.SelectedValue;
                }
                tbVillagePerm.Text = tbVillageRoadHouse.Text;
            }
            else
            {
                if (cmbUnionPerm.SelectedValue != null)
                {
                    cmbUnionPerm.Focus();
                    cmbUnionPerm.Text = null;
                }
                if (cmbUpazillaPerm.SelectedValue != null)
                {
                    cmbUpazillaPerm.Focus();
                    cmbUpazillaPerm.Text = null;
                }
                if (cmbDistrictPerm.SelectedValue != null)
                {
                    cmbDistrictPerm.Focus();
                    cmbDistrictPerm.Text = null;
                }
                tbVillagePerm.Text = null;
            }
        }


        List<SeizureDto> seizureList = new List<SeizureDto>();

        private void btnUploadSeizure_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "(*.jpg; *.jpeg; *.png; *.bmp; *pdf)|" + "*.jpg; *.jpeg; *.png; *.bmp; *pdf";
            DialogResult dr = openFileDialog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                byte[] ByteContents = null;
                try
                {
                    ByteContents = File.ReadAllBytes(openFileDialog.FileName);
                    if (ByteContents == null) return;
                }
                catch 
                {
                    return;
                }

                int maxSize = Convert.ToInt32(ConfigurationManager.AppSettings["SeizureSizeInKB"].ToString());
                int uploadedFileSize = ByteContents.Length;
                if (uploadedFileSize > maxSize)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Seizure size is too big. Size should not exceed " + (maxSize/(1000*1024)) + " MB");
                    return;
                }
                int currentUploadedSeizureSize = 0;
                for (int i=0; i < StaticData.Enrollment?.profile?.attachment?.seizureList?.Count; i++)
                {
                    currentUploadedSeizureSize += (int)(StaticData.Enrollment?.profile?.attachment?.seizureList[i].seizure?.Length);
                }
                if (currentUploadedSeizureSize + uploadedFileSize > maxSize)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Seizure size is too big. Max Seizure Size is " + (maxSize / (1000 * 1024)) + " MB");
                    return;
                }
                string fileExt = Path.GetExtension(openFileDialog.FileName);

                SeizureDto seizureDto = new SeizureDto();
                seizureDto.attachmentNumber = seizureList.Count + 1;
                seizureDto.extension = fileExt;
                if (!string.IsNullOrEmpty(fileExt) && fileExt.Length > 2)
                {
                    if (fileExt != ".pdf" && fileExt != ".PDF") seizureDto.contentType = "image/" + fileExt.Substring(1, fileExt.Length - 1);
                    else if (fileExt == ".pdf" || fileExt == ".PDF") seizureDto.contentType = "application/" + fileExt.Substring(1, fileExt.Length - 1);
                }
                seizureDto.seizure = ByteContents;

                seizureList.Add(seizureDto);

                StaticData.Enrollment.profile.attachment.seizureList = seizureList;
                StaticData.Enrollment.profile.seizures = seizureList;

                try
                {
                    if (string.IsNullOrEmpty(StaticData.Enrollment.profile.id))
                    {
                        // Will not be draft saving already uploaded profile so that already existing data cannot be modified
                        dbEnrollClientManager.AddAttachment(StaticData.Enrollment.profile, Globals.AttachmentType.SEIZURE);
                    }

                    dgvSeizureList.Rows.Clear();
                    for (int i = 0; i < StaticData.Enrollment?.profile?.attachment?.seizureList?.Count; i++)
                    {
                        dgvSeizureList.Rows.Add("Seizure-" + (i + 1), "VIEW", "DELETE");
                    }
                }
                catch (Exception)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not save seizure");
                    return;
                }
            }
        }

        List<ComplainDto> complainList = new List<ComplainDto>();

        private void btnComplainUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "(*.jpg; *.jpeg; *.png; *.bmp; *pdf)|" + "*.jpg; *.jpeg; *.png; *.bmp; *pdf";
            DialogResult dr = openFileDialog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                byte[] ByteContents = null;
                try
                {
                    ByteContents = File.ReadAllBytes(openFileDialog.FileName);
                    if (ByteContents == null) return;
                }
                catch
                {
                    return;
                }

                int maxSize = Convert.ToInt32(ConfigurationManager.AppSettings["ComplainSizeInKB"].ToString());
                int uploadedFileSize = ByteContents.Length;
                if (uploadedFileSize > maxSize)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Complain size is too big. Size should not exceed " + (maxSize / (1000 * 1024)) + " MB");
                    return;
                }
                int currentUploadedComplainSize = 0;
                for (int i = 0; i < StaticData.Enrollment?.profile?.attachment?.complaintList?.Count; i++)
                {
                    currentUploadedComplainSize += (int)(StaticData.Enrollment?.profile?.attachment?.complaintList[i].complaint?.Length);
                }
                if (currentUploadedComplainSize + uploadedFileSize > maxSize)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Complain size is too big. Max Complain Size is " + (maxSize / (1000 * 1024)) + " MB");
                    return;
                }

                string fileExt = Path.GetExtension(openFileDialog.FileName);

                ComplainDto complainDto = new ComplainDto();
                complainDto.attachmentNumber = complainList.Count + 1;
                complainDto.extension = fileExt;
                if (!string.IsNullOrEmpty(fileExt) && fileExt.Length > 2)
                {
                    if (fileExt != ".pdf" && fileExt != ".PDF") complainDto.contentType = "image/" + fileExt.Substring(1, fileExt.Length - 1);
                    else if (fileExt == ".pdf" || fileExt == ".PDF") complainDto.contentType = "application/" + fileExt.Substring(1, fileExt.Length - 1);
                }
                complainDto.complaint = ByteContents;
                complainList.Add(complainDto);

                StaticData.Enrollment.profile.attachment.complaintList = complainList;
                StaticData.Enrollment.profile.complains = complainList;

                try
                {
                    if (string.IsNullOrEmpty(StaticData.Enrollment.profile.id))
                    {
                        // Will not be draft saving already uploaded profile so that already existing data cannot be modified
                        dbEnrollClientManager.AddAttachment(StaticData.Enrollment.profile, Globals.AttachmentType.COMPLAIN);
                    }

                    dgvComplainList.Rows.Clear();
                    for (int i = 0; i < StaticData.Enrollment?.profile?.attachment?.complaintList?.Count; i++)
                    {
                        dgvComplainList.Rows.Add("Complain-" + (i + 1), "VIEW", "DELETE");
                    }
                }
                catch (Exception)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not save complaint");
                    return;
                }
            }
        }

        List<FIRDto> firList = new List<FIRDto>();

        private void btnUploadFIR_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbFIRNumber.Text))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please input case number");
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "(*.jpg; *.jpeg; *.png; *.bmp; *pdf)|" + "*.jpg; *.jpeg; *.png; *.bmp; *pdf";
            DialogResult dr = openFileDialog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                byte[] ByteContents = null;
                try
                {
                    ByteContents = File.ReadAllBytes(openFileDialog.FileName);
                    if (ByteContents == null) return;
                }
                catch
                {
                    return;
                }

                int maxSize = Convert.ToInt32(ConfigurationManager.AppSettings["FIRSizeInKB"].ToString());
                int uploadedFileSize = ByteContents.Length;
                if (uploadedFileSize > maxSize)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "FIR size is too big. Size should not exceed " + (maxSize / (1000 * 1024)) + " MB");
                    return;
                }
                int currentUploadedFIRSize = 0;
                for (int i = 0; i < StaticData.Enrollment?.profile?.attachment?.firList?.Count; i++)
                {
                    currentUploadedFIRSize += (int)(StaticData.Enrollment?.profile?.attachment?.firList[i].fir?.Length);
                }
                if (currentUploadedFIRSize + uploadedFileSize > maxSize)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "FIR size is too big. Max FIR Size is " + (maxSize / (1000 * 1024)) + " MB");
                    return;
                }

                string fileExt = Path.GetExtension(openFileDialog.FileName);

                FIRDto firDto = new FIRDto();
                firDto.attachmentNumber = firList.Count + 1;
                firDto.extension = fileExt;
                if (!string.IsNullOrEmpty(fileExt) && fileExt.Length > 2)
                {
                    if (fileExt != ".pdf" && fileExt != ".PDF") firDto.contentType = "image/" + fileExt.Substring(1, fileExt.Length - 1);
                    else if (fileExt == ".pdf" || fileExt == ".PDF") firDto.contentType = "application/" + fileExt.Substring(1, fileExt.Length - 1);
                }
                firDto.firNo = tbFIRNumber.Text;
                firDto.fir = ByteContents;
                if (!string.IsNullOrEmpty(dtpFIRDate.Text))
                {
                    try
                    {
                        DateTime DateFIR = DateTime.ParseExact(dtpFIRDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        firDto.firDate = DateFIR.ToString("yyyy-MM-dd");
                    }
                    catch (Exception) { }
                }

                if (!string.IsNullOrEmpty(cmbFIRdistrict.SelectedValue?.ToString()))
                {
                    firDto.district = (!string.IsNullOrEmpty(cmbFIRdistrict.SelectedValue?.ToString())) ? Convert.ToInt32(cmbFIRdistrict.SelectedValue?.ToString()) : 0;
                }
                if (!string.IsNullOrEmpty(cmbFIRthana.SelectedValue?.ToString()))
                {
                    firDto.upozilaOrThana = (!string.IsNullOrEmpty(cmbFIRthana.SelectedValue?.ToString())) ? Convert.ToInt32(cmbFIRthana.SelectedValue?.ToString()) : 0;
                }

                firList.Add(firDto);

                StaticData.Enrollment.profile.attachment.firList = firList;
                StaticData.Enrollment.profile.firs = firList;

                try
                {
                    if (string.IsNullOrEmpty(StaticData.Enrollment.profile.id))
                    {
                        // Will not be draft saving already uploaded profile so that already existing data cannot be modified
                        dbEnrollClientManager.AddAttachment(StaticData.Enrollment.profile, Globals.AttachmentType.FIR);
                    }

                    dgvFirList.Rows.Clear();
                    for (int i = 0; i < StaticData.Enrollment?.profile?.attachment?.firList?.Count; i++)
                    {
                        dgvFirList.Rows.Add("FIR-" + (i + 1), "VIEW", "DELETE");
                    }
                }
                catch (Exception)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not save FIR");
                    return;
                }
            }
        }

        private void tbFullName_Leave(object sender, EventArgs e)
        {
            string fullName = tbFullName.Text;

            if (!string.IsNullOrEmpty(tbFullName.Text))
            {
                if (!Utils.IsAlphaCheck(tbFullName.Text))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Name can contain only letters");
                    tbFullName.Text = null;
                    return;
                }
            }
            //else return;

            if (fullName == StaticData.Enrollment.profile.fullName)
            {
                return;
            }
            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            StaticData.Enrollment.profile.fullName = fullName;
            dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "full_name", -1, fullName);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void tbNickName_Leave(object sender, EventArgs e)
        {
            nickNameList.Clear();

            if (!string.IsNullOrEmpty(tbNickName.Text))
            {
                nickNameList.Add(tbNickName.Text);
            }

            StaticData.Enrollment.profile.nickName = (nickNameList?.Count >= 0) ? nickNameList : null;
            string nickNames = string.Empty;
            for (int i = 0; i < nickNameList.Count; i++)
            {
                nickNames += nickNameList[i];
                if (i != nickNameList.Count - 1) nickNames += ",";
            }
            dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "nick_name", -1, nickNames);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void dtpArrestDate_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(dtpArrestDate.Text))
            {
                try
                {
                    DateTime dateOfArrest = DateTime.ParseExact(dtpArrestDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    StaticData.Enrollment.profile.arrestDate = dateOfArrest.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                }
                catch (Exception) { }
            }
            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "arrest_date", -1, StaticData.Enrollment.profile.arrestDate);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void groupBox4_Leave(object sender, EventArgs e)
        {
            string gender = null;
            if (rdBtnMale.Checked) gender = "Male";
            else if (rdBtnFemale.Checked) gender = "Female";
            else gender = "Other";

            if (gender == StaticData.Enrollment?.profile?.gender)
            {
                return;
            }

            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            StaticData.Enrollment.profile.gender = gender;

            int updateGender = -1;
            if (gender == "Male") updateGender = 0;
            else if (gender == "Female") updateGender = 1;
            else if (gender == "Other") updateGender = 2;

            dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "gender", updateGender, null);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void tbPhone_Leave(object sender, EventArgs e)
        {
            string phone = (!string.IsNullOrEmpty(tbPhone.Text)) ? tbPhone.Text : null;

            if (!string.IsNullOrEmpty(tbPhone.Text))
            {
                if (!Utils.isDigit(tbPhone.Text))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Phone can contain only digits");
                    tbPhone.Text = null;
                    return;
                }
                if (!phone.StartsWith("01"))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Phone number must start with 01");
                    tbPhone.Text = null;
                    return;
                }
                if (phone?.Length != 11)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Phone number has to be 11 digits");
                    tbPhone.Focus();
                    return;
                }
            }
            //else return;

            //if (StaticData.Enrollment.profile.mobile != null
            //    && StaticData.Enrollment.profile.mobile.Count > 0)
            //{
            //    if (phone == StaticData.Enrollment.profile.mobile[0] || phone == null)
            //    {
            //        return;
            //    }
            //}
            //else
            if (StaticData.Enrollment?.profile?.mobile == null)
            {
                StaticData.Enrollment.profile.mobile = new List<string>();
            }

            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            StaticData.Enrollment.profile.mobile.Add(phone);
            dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "mobile", -1, phone);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void tbNID_Leave(object sender, EventArgs e)
        {
            string nid = (!string.IsNullOrEmpty(tbNID.Text)) ? tbNID.Text : null;
            if (nid == StaticData.Enrollment?.profile?.nid) return;
            if (!string.IsNullOrEmpty(nid))
            {
                if (!Utils.isDigit(nid))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "NID can contain only digits");
                    tbNID.Focus();
                    return;
                }
                if (nid?.Length != 10 && nid?.Length != 17)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "NID has to be 10 or 17 digits");
                    tbNID.Focus();
                    return;
                }
            }
            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            StaticData.Enrollment.profile.nid = nid;
            dbEnrollClientManager.UpdateProfileValuesAsync(tbRefNo.Text, "nid", -1, nid);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void tbDOBDay_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbDOBDay.Text))
            {
                if (!Utils.isDigit(tbDOBDay.Text))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Day can contain only digits");
                    tbDOBDay.Text = null;
                    return;
                }
                AutoSaveDOB();
            }
        }

        private void tbDOBMonth_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbDOBMonth.Text))
            {
                if (!Utils.isDigit(tbDOBMonth.Text))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Month can contain only digits");
                    tbDOBMonth.Text = null;
                    return;
                }
                if (Convert.ToInt32(tbDOBMonth.Text) > 0 && Convert.ToInt32(tbDOBMonth.Text) <= 12)
                {
                    AutoSaveDOB();
                }
                else
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Date of birth month wrong input");
                    tbDOBMonth.Text = null;
                    return;
                }
            }
        }

        private void tbDOBYear_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbDOBYear.Text))
            {
                if (!Utils.isDigit(tbDOBYear.Text))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Year can contain only digits");
                    tbDOBYear.Text = null;
                    return;
                }
                if (tbDOBYear.Text.StartsWith("20") || tbDOBYear.Text.StartsWith("19") || tbDOBYear.Text.StartsWith("18"))
                {
                    AutoSaveDOB();
                }
                else
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Date of birth year cannot be before 1800");
                    tbDOBYear.Text = null;
                    return;
                }
            }
        }

        public string CalculateAgeByDob(string dateOfBirth)
        {
            try
            {
                DateTime dob;
                DateTime.TryParseExact(dateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dob);

                DateTime zeroTime = new DateTime(1, 1, 1);
                DateTime curdate = DateTime.Now.ToLocalTime();

                TimeSpan span = curdate - dob;

                int years = (zeroTime + span).Year - 1;
                //int months = (zeroTime + span).Month - 1;
                //int days = (zeroTime + span).Day;

                //return string.Format("{0} Yr {1}-Mon {2}-Dy", years, months, days);
                return string.Format("{0}", years);
            }
            catch { }

            return string.Empty;
        }

        private void AutoSaveDOB()
        {
            if (string.IsNullOrEmpty(tbDOBDay.Text) || string.IsNullOrEmpty(tbDOBMonth.Text) || string.IsNullOrEmpty(tbDOBYear.Text))
            {
                return;
            }
            try
            {
                string dobStr = tbDOBDay.Text + "/" + tbDOBMonth.Text + "/" + tbDOBYear.Text;
                DateTime DOB = DateTime.ParseExact(dobStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                StaticData.Enrollment.profile.dateOfBirth = DOB.ToString("yyyy-MM-dd");

                dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "date_of_birth", -1, StaticData.Enrollment.profile.dateOfBirth);

                string AgeInYears = CalculateAgeByDob(dobStr);
                tbAge.Text = AgeInYears;

                if (!string.IsNullOrEmpty(tbAge.Text))
                {
                    StaticData.Enrollment.profile.age = Convert.ToInt32(tbAge.Text);
                    dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "age", Convert.ToInt32(StaticData.Enrollment.profile.age), null);
                    //tbAge.ReadOnly = true;
                }

                if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();

            }
            catch (Exception)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please input date of birth correctly");
                tbDOBYear.Text = null;
                tbDOBMonth.Text = null;
                tbDOBDay.Text = null;
                return;
            }
        }

        private void tbAge_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbAge.Text) || string.IsNullOrWhiteSpace(tbAge.Text)) return;
            int age = Convert.ToInt32(tbAge.Text);
            if (age == StaticData.Enrollment?.profile?.age) return;

            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            StaticData.Enrollment.profile.age = age;
            dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "age", age, null);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void cmbNationality_Leave(object sender, EventArgs e)
        {
            int nationalityId = Convert.ToInt32(cmbNationality.SelectedValue?.ToString());
            NationalityProfileDto updatedNationality = new NationalityProfileDto();
            updatedNationality.id = nationalityId;

            if (StaticData.Enrollment?.profile?.nationality != null)
            {
                if (nationalityId == StaticData.Enrollment.profile.nationality?.id)
                {
                    return;
                }
            }

            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            StaticData.Enrollment.profile.nationality = updatedNationality;

            var jsonNat = new JavaScriptSerializer().Serialize(updatedNationality);
            dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "nationality", -1, jsonNat);

            if (nationalityId == 14)
            {
                tbPostCode.Enabled = false;
                tbTown.Enabled = false;
                tbState.Enabled = false;
                cmbCountry.Enabled = false;

                cmbDistrictPerm.Enabled = true;
                cmbUpazillaPerm.Enabled = true;
                cmbUnionPerm.Enabled = true;
                tbVillagePerm.Enabled = true;

                chkPresentPermanent.Enabled = true;
            }
            else if (nationalityId > 0 && nationalityId != 14)
            {
                tbPostCode.Enabled = true;
                tbTown.Enabled = true;
                tbState.Enabled = true;
                cmbCountry.Enabled = true;

                cmbDistrictPerm.Enabled = false;
                cmbUpazillaPerm.Enabled = false;
                cmbUnionPerm.Enabled = false;
                tbVillagePerm.Enabled = false;

                chkPresentPermanent.Enabled = false;
            }

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void cmbOccupation_Leave(object sender, EventArgs e)
        {
            string occupation = cmbOccupation.SelectedValue?.ToString();

            if (occupation == StaticData.Enrollment.profile.occupation)
            {
                return;
            }
            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            StaticData.Enrollment.profile.occupation = occupation;
            dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "occupation", -1, occupation);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void cmbReligion_Leave(object sender, EventArgs e)
        {
            //if (cmbReligion.SelectedValue?.ToString() == "None") return;
            int religion = -1;
            if (cmbReligion.SelectedValue?.ToString() == "muslim") religion = 0;
            if (cmbReligion.SelectedValue?.ToString() == "hindu") religion = 1;
            if (cmbReligion.SelectedValue?.ToString() == "christian") religion = 2;
            if (cmbReligion.SelectedValue?.ToString() == "buddhist") religion = 3;

            if (cmbReligion.SelectedValue?.ToString() == StaticData.Enrollment.profile.religion)
            {
                return;
            }
            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;

            StaticData.Enrollment.profile.religion = cmbReligion.SelectedValue?.ToString();
            dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "religion", religion, null);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void tbHeightFeet_Leave(object sender, EventArgs e)
        {
            double heightFt = 0.0;
            double heightInch = 0.0;

            if (!string.IsNullOrEmpty(tbHeightFeet.Text))
            {
                if (!Utils.isDigit(tbHeightFeet.Text))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Height can contain only digits");
                    tbHeightFeet.Text = null;
                    return;
                }
            }

            if (!string.IsNullOrEmpty(tbHeightFeet.Text))
            {
                heightFt = Convert.ToDouble(tbHeightFeet.Text);
            }
            if (!string.IsNullOrEmpty(tbHeightInch.Text))
            {
                heightInch = Convert.ToDouble(tbHeightInch.Text);
            }
            if (heightFt == StaticData.Enrollment?.profile?.height?.ft
                && heightInch == StaticData.Enrollment?.profile?.height?.inch)
            {
                return;
            }
            HeightDto heightDto = new HeightDto { ft = heightFt, inch = heightInch };
            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            StaticData.Enrollment.profile.height = heightDto;
            string jsonSerialized = new JavaScriptSerializer().Serialize(heightDto);
            dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "height", -1, jsonSerialized);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void tbHeightInch_Leave(object sender, EventArgs e)
        {
            double heightFt = 0.0;
            double heightInch = 0.0;

            if (!string.IsNullOrEmpty(tbHeightInch.Text))
            {
                if (!Utils.isDigit(tbHeightInch.Text))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Height can contain only digits");
                    tbHeightInch.Text = null;
                    return;
                }
            }

            if (!string.IsNullOrEmpty(tbHeightFeet.Text))
            {
                heightFt = Convert.ToDouble(tbHeightFeet.Text);
            }
            if (!string.IsNullOrEmpty(tbHeightInch.Text))
            {
                heightInch = Convert.ToDouble(tbHeightInch.Text);
            }
            if (heightFt == StaticData.Enrollment?.profile?.height?.ft
                && heightInch == StaticData.Enrollment?.profile?.height?.inch)
            {
                return;
            }
            HeightDto heightDto = new HeightDto { ft = heightFt, inch = heightInch };
            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            StaticData.Enrollment.profile.height = heightDto;
            string jsonSerialized = new JavaScriptSerializer().Serialize(heightDto);
            dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "height", -1, jsonSerialized);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void tbIdentificationMark_Leave(object sender, EventArgs e)
        {
            string idMark = (!string.IsNullOrEmpty(tbIdentificationMark.Text)) ? tbIdentificationMark.Text : null;
            if (idMark == StaticData.Enrollment?.profile?.identificationMark) return;

            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            StaticData.Enrollment.profile.identificationMark = idMark;
            dbEnrollClientManager.UpdateProfileValuesAsync(tbRefNo.Text, "identification_mark", -1, idMark);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void tbFatherName_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbFatherName.Text))
            {
                if (!Utils.IsAlphaCheck(tbFatherName.Text))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Name can contain only letters");
                    tbFatherName.Text = null;
                    return;
                }
            }
            var jsonFamily = new JavaScriptSerializer().Serialize(OnFamilySave());
            dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "family_information", -1, jsonFamily);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void tbFatherPhone_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbFatherPhone.Text))
            {
                if (!Utils.isDigit(tbFatherPhone.Text))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Phone can contain only digits");
                    tbFatherPhone.Text = null;
                    return;
                }
                if (!tbFatherPhone.Text.StartsWith("01"))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Phone number must start with 01");
                    tbFatherPhone.Text = null;
                    return;
                }
                if (tbFatherPhone.Text.Length != 11)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Phone number has to be 11 digits");
                    tbFatherPhone.Focus();
                    return;
                }
            }
            var jsonFamily = new JavaScriptSerializer().Serialize(OnFamilySave());
            dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "family_information", -1, jsonFamily);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void tbMotherName_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbMotherName.Text))
            {
                if (!Utils.IsAlphaCheck(tbMotherName.Text))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Name can contain only letters");
                    tbMotherName.Text = null;
                    return;
                }
            }
            var jsonFamily = new JavaScriptSerializer().Serialize(OnFamilySave());
            dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "family_information", -1, jsonFamily);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void cmbMaritalStatus_Leave(object sender, EventArgs e)
        {
            if (cmbMaritalStatus.SelectedValue?.ToString() == "None") return;
            int maritalStatus = -1;
            if (cmbMaritalStatus.SelectedValue?.ToString() == "Single") maritalStatus = 0;
            if (cmbMaritalStatus.SelectedValue?.ToString() == "Married") maritalStatus = 1;
            if (cmbMaritalStatus.SelectedValue?.ToString() == "Widowed") maritalStatus = 2;
            if (cmbMaritalStatus.SelectedValue?.ToString() == "Divorced") maritalStatus = 3;

            if (cmbMaritalStatus.SelectedValue?.ToString() == "Single")
            {
                tbSpouseName.Enabled = false;
                tbSpousePhone.Enabled = false;
            }
            else
            {
                tbSpouseName.Enabled = true;
                tbSpousePhone.Enabled = true;
            }

            if (cmbMaritalStatus.SelectedValue?.ToString() == StaticData.Enrollment.profile.maritalStatus)
            {
                return;
            }

            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            StaticData.Enrollment.profile.maritalStatus = cmbMaritalStatus.SelectedValue?.ToString();

            dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "marital_status", maritalStatus, null);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void tbSpouseName_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbSpouseName.Text))
            {
                if (!Utils.IsAlphaCheck(tbSpouseName.Text))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Name can contain only letters");
                    tbSpouseName.Text = null;
                    return;
                }
            }
            var jsonFamily = new JavaScriptSerializer().Serialize(OnFamilySave());
            dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "family_information", -1, jsonFamily);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void tbSpousePhone_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbSpousePhone.Text))
            {
                if (!Utils.isDigit(tbSpousePhone.Text))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Phone can contain only digits");
                    tbSpousePhone.Text = null;
                    return;
                }
                if (!tbSpousePhone.Text.StartsWith("01"))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Phone number must start with 01");
                    tbSpousePhone.Text = null;
                    return;
                }
                if (tbSpousePhone.Text.Length != 11)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Phone number has to be 11 digits");
                    tbSpousePhone.Focus();
                    return;
                }
            }
            var jsonFamily = new JavaScriptSerializer().Serialize(OnFamilySave());
            dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "family_information", -1, jsonFamily);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private List<FamilyDto> OnFamilySave()
        {
            List<FamilyDto> familyList = new List<FamilyDto>();
            if (!string.IsNullOrEmpty(tbFatherName.Text) || !string.IsNullOrEmpty(tbFatherPhone.Text))
            {
                familyList.Add(new FamilyDto { name = tbFatherName.Text, phone = tbFatherPhone.Text, relation = "father" });
            }
            if (!string.IsNullOrEmpty(tbMotherName.Text))
            {
                familyList.Add(new FamilyDto { name = tbMotherName.Text, relation = "mother" });
            }
            if (!string.IsNullOrEmpty(tbSpouseName.Text) || !string.IsNullOrEmpty(tbSpousePhone.Text))
            {
                familyList.Add(new FamilyDto { name = tbSpouseName.Text, phone = tbSpousePhone.Text, relation = "spouse" });
            }
            if (familyList.Count > 0) StaticData.Enrollment.profile.familys = familyList;
            return familyList;
        }


        private void cmbDistrict_Leave(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(cmbDistrict.SelectedValue?.ToString());

            if (id == StaticData.Enrollment?.profile?.presentAddress?.district)
            {
                return;
            }
            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            OnSaveAddress();
            dbEnrollClientManager.UpdateAddressValues(tbRefNo.Text, "district", id, null, "present_address_id");

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void cmbUpazilla_Leave(object sender, EventArgs e)
        {
            //if (cmbUpazilla.SelectedValue?.ToString() == null)
            //{
            //    return;
            //}

            int id = Convert.ToInt32(cmbUpazilla.SelectedValue?.ToString());

            //if (id == StaticData.Enrollment?.profile?.presentAddress?.upazila)
            //{
            //    return;
            //}

            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            OnSaveAddress();
            dbEnrollClientManager.UpdateAddressValues(tbRefNo.Text, "upazila", id, null, "present_address_id");

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void cmbUnion_Leave(object sender, EventArgs e)
        {
            //if (cmbUnion.SelectedValue?.ToString() == null)
            //{
            //    return;
            //}

            int id = Convert.ToInt32(cmbUnion.SelectedValue?.ToString());

            //if (id == StaticData.Enrollment?.profile?.presentAddress?.union)
            //{
            //    return;
            //}

            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            OnSaveAddress();
            dbEnrollClientManager.UpdateAddressValues(tbRefNo.Text, "union_or_ward", id, null, "present_address_id");

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void tbVillageRoadHouse_Leave(object sender, EventArgs e)
        {
            string VillageRoadHouse = (!string.IsNullOrEmpty(tbVillageRoadHouse.Text)) ? tbVillageRoadHouse.Text : null;
            if (VillageRoadHouse == StaticData.Enrollment?.profile?.presentAddress?.villageHouseRoadNo)
            {
                return;
            }
            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            OnSaveAddress();
            dbEnrollClientManager.UpdateAddressValues(tbRefNo.Text, "village_house_road_no", -1, VillageRoadHouse, "present_address_id");

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void cmbDistrictPerm_Leave(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(cmbDistrictPerm.SelectedValue?.ToString());

            if (id == StaticData.Enrollment?.profile?.permanentAddress?.district)
            {
                return;
            }
            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            OnSaveAddress();
            dbEnrollClientManager.UpdateAddressValues(tbRefNo.Text, "district", id, null, "permanent_address_id");

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void cmbUpazillaPerm_Leave(object sender, EventArgs e)
        {
            //if (cmbUpazillaPerm.SelectedValue?.ToString() == null)
            //{
            //    return;
            //}

            int id = Convert.ToInt32(cmbUpazillaPerm.SelectedValue?.ToString());

            //if (id == StaticData.Enrollment?.profile?.permanentAddress?.upazila)
            //{
            //    return;
            //}

            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            OnSaveAddress();
            dbEnrollClientManager.UpdateAddressValues(tbRefNo.Text, "upazila", id, null, "permanent_address_id");

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void cmbUnionPerm_Leave(object sender, EventArgs e)
        {
            //if (cmbUnionPerm.SelectedValue?.ToString() == null)
            //{
            //    return;
            //}

            int id = Convert.ToInt32(cmbUnionPerm.SelectedValue?.ToString());

            //if (id == StaticData.Enrollment?.profile?.permanentAddress?.union)
            //{
            //    return;
            //}

            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            OnSaveAddress();
            dbEnrollClientManager.UpdateAddressValues(tbRefNo.Text, "union_or_ward", id, null, "permanent_address_id");

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void tbVillagePerm_Leave(object sender, EventArgs e)
        {
            string VillageRoadHouse = (!string.IsNullOrEmpty(tbVillagePerm.Text)) ? tbVillagePerm.Text : null;
            if (VillageRoadHouse == StaticData.Enrollment?.profile?.permanentAddress?.villageHouseRoadNo)
            {
                return;
            }
            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            OnSaveAddress();
            dbEnrollClientManager.UpdateAddressValues(tbRefNo.Text, "village_house_road_no", -1, VillageRoadHouse,"permanent_address_id");

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void OnSaveForeignAddress()
        {
            ForeignAddressDto foreignAddressDto = new ForeignAddressDto();
            foreignAddressDto.zipOrPostCode = (!string.IsNullOrEmpty(tbPostCode.Text)) ? tbPostCode.Text : null;
            foreignAddressDto.town = (!string.IsNullOrEmpty(tbTown.Text)) ? tbTown.Text : null;
            foreignAddressDto.state = (!string.IsNullOrEmpty(tbState.Text)) ? tbState.Text : null;
            foreignAddressDto.country = (!string.IsNullOrEmpty(cmbCountry.SelectedValue?.ToString())) ? Convert.ToInt32(cmbCountry.SelectedValue?.ToString()) : 0;

            StaticData.Enrollment.profile.foreignAddress = foreignAddressDto;
        }

        private void tbPostCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbPostCode.Text))
            {
                if (!Utils.isDigit(tbPostCode.Text))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Post Code can contain only digits");
                    tbPostCode.Text = null;
                    return;
                }
            }
            OnSaveForeignAddress();
            var jsonForeign = new JavaScriptSerializer().Serialize(StaticData.Enrollment?.profile?.foreignAddress);
            dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "foreigner_address", -1, jsonForeign);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void tbTown_Leave(object sender, EventArgs e)
        {
            OnSaveForeignAddress();
            var jsonForeign = new JavaScriptSerializer().Serialize(StaticData.Enrollment?.profile?.foreignAddress);
            dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "foreigner_address", -1, jsonForeign);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void tbState_Leave(object sender, EventArgs e)
        {
            OnSaveForeignAddress();
            var jsonForeign = new JavaScriptSerializer().Serialize(StaticData.Enrollment?.profile?.foreignAddress);
            dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "foreigner_address", -1, jsonForeign);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void cmbCountry_Leave(object sender, EventArgs e)
        {
            OnSaveForeignAddress();
            var jsonForeign = new JavaScriptSerializer().Serialize(StaticData.Enrollment?.profile?.foreignAddress);
            dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "foreigner_address", -1, jsonForeign);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void cmbCrimeType_Leave(object sender, EventArgs e)
        {
            int CrimeType = Convert.ToInt32(cmbCrimeType.SelectedValue?.ToString());
            if (CrimeType == StaticData.Enrollment?.profile?.crimeInformation?.crimeType)
            {
                return;
            }
            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            StaticData.Enrollment.profile.crimeInformation.crimeType = CrimeType;
            dbEnrollClientManager.UpdateCrimeInfo(tbRefNo.Text, "crime_type", CrimeType, null);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void dtpDateOfCrime_Leave(object sender, EventArgs e)
        {
            CrimeHistoryDto crimeHistory = new CrimeHistoryDto();
            try
            {
                DateTime dateOfCrime = DateTime.ParseExact(dtpDateOfCrime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                crimeHistory.dateOfCrime = dateOfCrime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            }
            catch { }

            StaticData.Enrollment.profile.crimeInformation.crimeHistorys.Add(crimeHistory);

            var json = new JavaScriptSerializer().Serialize(StaticData.Enrollment?.profile?.crimeInformation?.crimeHistorys);

            dbEnrollClientManager.UpdateCrimeInfo(tbRefNo.Text, "crime_history", -1, json);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void cmbPoliticalGroup_Leave(object sender, EventArgs e)
        {
            int polGroup = 0;

            if (cmbPoliticalGroup.SelectedValue?.ToString() == "AwamiLeague") polGroup = 1;
            else if (cmbPoliticalGroup.SelectedValue?.ToString() == "BNP") polGroup = 2;
            else if (cmbPoliticalGroup.SelectedValue?.ToString() == "JatiyaParty_Ershad") polGroup = 3;
            else if (cmbPoliticalGroup.SelectedValue?.ToString() == "WorkersPartyofBangladesh") polGroup = 4;
            else if (cmbPoliticalGroup.SelectedValue?.ToString() == "JatiyaSamajtantrikDal") polGroup = 5;
            else if (cmbPoliticalGroup.SelectedValue?.ToString() == "BikalpaDharaBangladesh") polGroup = 6;
            else if (cmbPoliticalGroup.SelectedValue?.ToString() == "GanoForum") polGroup = 7;
            else if (cmbPoliticalGroup.SelectedValue?.ToString() == "JatiyaParty_Manju") polGroup = 8;
            else if (cmbPoliticalGroup.SelectedValue?.ToString() == "BangladesTarikatFederation") polGroup = 9;

            if (cmbPoliticalGroup.SelectedValue?.ToString() == StaticData.Enrollment.profile.politicalGroup)
            {
                return;
            }

            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            StaticData.Enrollment.profile.politicalGroup = cmbPoliticalGroup.SelectedValue?.ToString();

            dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "political_group", polGroup, null);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void tbGroupGangName_Leave(object sender, EventArgs e)
        {
            string GroupGangName = (!string.IsNullOrEmpty(tbGroupGangName.Text)) ? tbGroupGangName.Text : null;
            if (GroupGangName == StaticData.Enrollment?.profile?.crimeInformation?.groupOrGangName)
            {
                return;
            }
            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            StaticData.Enrollment.profile.crimeInformation.groupOrGangName = GroupGangName;
            dbEnrollClientManager.UpdateCrimeInfo(tbRefNo.Text, "group_or_gang_name", -1, GroupGangName);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void cmbCrimeDistrict_Leave(object sender, EventArgs e)
        {
            //int Battalion = Convert.ToInt32(cmbBattalion.SelectedValue?.ToString());

            //StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            //StaticData.Enrollment.profile.unit = Battalion;
            //StaticData.Enrollment.profile.crimeInformation.crimeZone.unit = Battalion;

            //dbEnrollClientManager.UpdateProfileValuesAsync(tbRefNo.Text, "battalion", Battalion, null, null);

            //int subUnit = Convert.ToInt32(cmbSubStation.SelectedValue?.ToString());
            //StaticData.Enrollment.profile.subUnit = subUnit;
            ////StaticData.Enrollment.profile.subUnitName = cmbSubStation.Text;
            //dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "sub_unit", subUnit, null, null);

            ////StaticData.Enrollment.profile.crimeInformation.crimeZone.district = Convert.ToInt32(cmbCrimeDistrict?.SelectedValue?.ToString());
            ////var json = new JavaScriptSerializer().Serialize(OnSaveCrimeZone());

            ////dbEnrollClientManager.UpdateCrimeInfo(tbRefNo.Text, "crime_zone", -1, json);

            //int crimeDistrict = Convert.ToInt32(cmbCrimeDistrict.SelectedValue?.ToString());
            //if (crimeDistrict == StaticData.Enrollment.profile?.crimeZoneDistrict) return;
            //StaticData.Enrollment.profile.crimeZoneDistrict = crimeDistrict;

            //dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "crime_zone_district", crimeDistrict, null, null);

            // The above is not needed as rab geo map does not need to be followed

            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            if (StaticData.Enrollment.profile.crimeInformation.crimeZone == null)
            {
                StaticData.Enrollment.profile.crimeInformation.crimeZone = new CrimeZoneDto();
            }
            StaticData.Enrollment.profile.crimeInformation.crimeZone.district = Convert.ToInt32(cmbCrimeDistrict?.SelectedValue?.ToString());
            var json = new JavaScriptSerializer().Serialize(OnSaveCrimeZone());

            dbEnrollClientManager.UpdateCrimeInfo(tbRefNo.Text, "crime_zone", -1, json);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void cmbCrimeThana_Leave(object sender, EventArgs e)
        {
            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            if (StaticData.Enrollment.profile.crimeInformation.crimeZone == null)
            {
                StaticData.Enrollment.profile.crimeInformation.crimeZone = new CrimeZoneDto();
            }
            StaticData.Enrollment.profile.crimeInformation.crimeZone.upozilaOrThana = Convert.ToInt32(cmbCrimeThana?.SelectedValue?.ToString());
            var json = new JavaScriptSerializer().Serialize(OnSaveCrimeZone());

            dbEnrollClientManager.UpdateCrimeInfo(tbRefNo.Text, "crime_zone", -1, json);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }


        private void cmbCrimeUnion_Leave(object sender, EventArgs e)
        {
            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            if (StaticData.Enrollment.profile.crimeInformation.crimeZone == null)
            {
                StaticData.Enrollment.profile.crimeInformation.crimeZone = new CrimeZoneDto();
            }
            StaticData.Enrollment.profile.crimeInformation.crimeZone.union = Convert.ToInt32(cmbCrimeUnion?.SelectedValue?.ToString());
            var json = new JavaScriptSerializer().Serialize(OnSaveCrimeZone());

            dbEnrollClientManager.UpdateCrimeInfo(tbRefNo.Text, "crime_zone", -1, json);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void tbCrimeVillage_Leave(object sender, EventArgs e)
        {
            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            if (StaticData.Enrollment.profile.crimeInformation.crimeZone == null)
            {
                StaticData.Enrollment.profile.crimeInformation.crimeZone = new CrimeZoneDto();
            }
            StaticData.Enrollment.profile.crimeInformation.crimeZone.addressLine = tbCrimeVillage.Text;

            var json = new JavaScriptSerializer().Serialize(OnSaveCrimeZone());

            dbEnrollClientManager.UpdateCrimeInfo(tbRefNo.Text, "crime_zone", -1, json);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private CrimeZoneDto OnSaveCrimeZone()
        {
            CrimeZoneDto dto = new CrimeZoneDto();

            dto.unit = (!string.IsNullOrEmpty(cmbBattalion.SelectedValue?.ToString())) ? Convert.ToInt32(cmbBattalion.SelectedValue?.ToString()) : -1;
            dto.district = (!string.IsNullOrEmpty(cmbCrimeDistrict.SelectedValue?.ToString())) ? Convert.ToInt32(cmbCrimeDistrict.SelectedValue?.ToString()) : 0;
            dto.upozilaOrThana = (!string.IsNullOrEmpty(cmbCrimeThana.SelectedValue?.ToString())) ? Convert.ToInt32(cmbCrimeThana.SelectedValue?.ToString()) : 0;
            dto.union = (!string.IsNullOrEmpty(cmbCrimeUnion.SelectedValue?.ToString())) ? Convert.ToInt32(cmbCrimeUnion.SelectedValue?.ToString()) : 0;
            dto.addressLine = (!string.IsNullOrEmpty(tbCrimeVillage.Text)) ? tbCrimeVillage.Text : null;

            return dto;
        }

        private void tbArrestedByOfficerName_Leave(object sender, EventArgs e)
        {
            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            StaticData.Enrollment.profile.arrestedBy = (!string.IsNullOrEmpty(tbArrestedByOfficerName.Text)) ? tbArrestedByOfficerName.Text : null;

            //if (string.IsNullOrEmpty(StaticData.Enrollment.profile.arrestedBy)) return;

            dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "arrested_by", -1, StaticData.Enrollment.profile.arrestedBy);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void cmbBattalion_Leave(object sender, EventArgs e)
        {
            int Battalion = -1;
            if (!string.IsNullOrEmpty(cmbBattalion.SelectedValue?.ToString()))
            {
                Battalion = Convert.ToInt32(cmbBattalion.SelectedValue?.ToString());
            }

            if (Battalion > 0)
            {
                lblSubUnitMandatory.Visible = true;
            }
            else
            {
                lblSubUnitMandatory.Visible = false;
            }

            if (Battalion == StaticData.Enrollment?.profile?.unit)
            {
                return;
            }
            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            StaticData.Enrollment.profile.unit = Battalion;

            dbEnrollClientManager.UpdateProfileValuesAsync(tbRefNo.Text, "battalion", Battalion, null);

            //var json = new JavaScriptSerializer().Serialize(OnSaveCrimeZone());
            //dbEnrollClientManager.UpdateCrimeInfoAsync(tbRefNo.Text, "crime_zone", -1, json);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void cmbSubStation_Leave(object sender, EventArgs e)
        {
            int Battalion = Convert.ToInt32(cmbBattalion.SelectedValue?.ToString());

            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            StaticData.Enrollment.profile.unit = Battalion;

            dbEnrollClientManager.UpdateProfileValuesAsync(tbRefNo.Text, "battalion", Battalion, null);

            //var json = new JavaScriptSerializer().Serialize(OnSaveCrimeZone());
            //dbEnrollClientManager.UpdateCrimeInfoAsync(tbRefNo.Text, "crime_zone", -1, json);

            int subUnit = Convert.ToInt32(cmbSubStation.SelectedValue?.ToString());
            if (subUnit == StaticData.Enrollment?.profile?.subUnit) return;

            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            StaticData.Enrollment.profile.subUnit = subUnit;
            //StaticData.Enrollment.profile.subUnitName = cmbSubStation.Text;
            dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "sub_unit", subUnit, null);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void tbFIRNumber_Leave(object sender, EventArgs e)
        {
            string firNo = (!string.IsNullOrEmpty(tbFIRNumber.Text)) ? tbFIRNumber.Text : null;
            //if (firNo == null) return;
            if (StaticData.Enrollment.profile.attachment.firList?.Count > 0)
            {
                firList[0].firNo = firNo;
            }
            dbEnrollClientManager.UpdateAttachment(tbRefNo.Text, "fir_no", firNo, -1);
        }

        private void dtpFIRDate_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(dtpFIRDate.Text))
            {
                try
                {
                    if (StaticData.Enrollment.profile.attachment.firList?.Count > 0)
                    {
                        DateTime DateFIR = DateTime.ParseExact(dtpFIRDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        firList[0].firDate = DateFIR.ToString("yyyy-MM-dd");

                        dbEnrollClientManager.UpdateAttachment(tbRefNo.Text, "fir_date", firList[0].firDate, -1);
                    }
                }
                catch (Exception) { }
            }
        }

        private void cmbFIRdistrict_Leave(object sender, EventArgs e)
        {
            int firDistrict = (!string.IsNullOrEmpty(cmbFIRdistrict.SelectedValue?.ToString())) ? Convert.ToInt32(cmbFIRdistrict.SelectedValue?.ToString()) : 0;
            //if (firDistrict == 0) return;
            if (StaticData.Enrollment.profile.attachment.firList?.Count > 0)
            {
                firList[0].district = firDistrict;
            }
            dbEnrollClientManager.UpdateAttachment(tbRefNo.Text, "fir_district", null, firDistrict);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void cmbFIRthana_Leave(object sender, EventArgs e)
        {
            int firThana = (!string.IsNullOrEmpty(cmbFIRthana.SelectedValue?.ToString())) ? Convert.ToInt32(cmbFIRthana.SelectedValue?.ToString()) : 0;
            //if (firThana == 0) return;
            if (StaticData.Enrollment.profile.attachment.firList?.Count > 0)
            {
                firList[0].upozilaOrThana = firThana;
            }
            dbEnrollClientManager.UpdateAttachment(tbRefNo.Text, "fir_upazila", null, firThana);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }


        private void tbIOName_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbIOName.Text))
            {
                if (!Utils.IsAlphaCheck(tbIOName.Text))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Name can contain only letters");
                    tbIOName.Text = null;
                    return;
                }
            }
            string IOName = (!string.IsNullOrEmpty(tbIOName.Text)) ? tbIOName.Text : null;
            if (IOName == StaticData.Enrollment?.profile?.investigatingOfficerName)
            {
                return;
            }
            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;

            dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "investigatingofficer_name", -1, IOName);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void cmbIOrank_Leave(object sender, EventArgs e)
        {
            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            StaticData.Enrollment.profile.iorank = cmbIOrank.SelectedValue?.ToString();

            //if (string.IsNullOrEmpty(StaticData.Enrollment.profile.iorank)) return;

            dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "io_rank", -1, StaticData.Enrollment.profile.iorank);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void tbIOMobile_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbIOMobile.Text))
            {
                if (!Utils.isDigit(tbIOMobile.Text))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Phone Number can contain only digits");
                    tbIOMobile.Text = null;
                    return;
                }
                if (!tbIOMobile.Text.StartsWith("01"))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Phone number must start with 01");
                    tbIOMobile.Text = null;
                    return;
                }
                if (tbIOMobile.Text?.Length != 11)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Phone number has to be 11 digits");
                    tbIOMobile.Focus();
                    return;
                }
            }
            string IOMobile = (!string.IsNullOrEmpty(tbIOMobile.Text)) ? tbIOMobile.Text : null;
            if (IOMobile == StaticData.Enrollment?.profile?.investigatingOfficerMobile)
            {
                return;
            }
            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;

            dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "investigatingofficer_mobile", -1, IOMobile);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void tbIOBPNo_Leave(object sender, EventArgs e)
        {
            string IOBPNo = (!string.IsNullOrEmpty(tbIOBPNo.Text)) ? tbIOBPNo.Text : null;
            if (IOBPNo == StaticData.Enrollment?.profile?.investigatingOfficerBPNumber)
            {
                return;
            }
            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;

            dbEnrollClientManager.UpdateProfileValues(tbRefNo.Text, "investigatingofficer_bp", -1, IOBPNo);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void cmbWarrantType_Leave(object sender, EventArgs e)
        {
            string warrantType = cmbWarrantType.SelectedValue?.ToString();
            if (warrantType == StaticData.Enrollment?.profile?.crimeInformation?.warrant?.warrantType)
            {
                return;
            }
            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            if (StaticData.Enrollment.profile.crimeInformation.warrant == null)
            {
                StaticData.Enrollment.profile.crimeInformation.warrant = new WarrantDto();
            }
            StaticData.Enrollment.profile.crimeInformation.warrant.warrantType = warrantType;
            dbEnrollClientManager.UpdateCrimeInfo(tbRefNo.Text, "warrant_type", -1, warrantType);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void tbWarrantNo_Leave(object sender, EventArgs e)
        {
            string warrantNo = tbWarrantNo.Text;
            if (warrantNo == StaticData.Enrollment?.profile?.crimeInformation?.warrant?.warrantNo)
            {
                return;
            }
            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            if (StaticData.Enrollment.profile.crimeInformation.warrant == null)
            {
                StaticData.Enrollment.profile.crimeInformation.warrant = new WarrantDto();
            }
            StaticData.Enrollment.profile.crimeInformation.warrant.warrantNo = warrantNo;
            dbEnrollClientManager.UpdateCrimeInfo(tbRefNo.Text, "warrant_no", -1, warrantNo);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void tbSectionNo_Leave(object sender, EventArgs e)
        {
            string sectionNo = tbSectionNo.Text;
            if (sectionNo == StaticData.Enrollment?.profile?.crimeInformation?.warrant?.sectionNo)
            {
                return;
            }
            StaticData.Enrollment.profile.referenceNo = tbRefNo.Text;
            if (StaticData.Enrollment.profile.crimeInformation.warrant == null)
            {
                StaticData.Enrollment.profile.crimeInformation.warrant = new WarrantDto();
            }
            StaticData.Enrollment.profile.crimeInformation.warrant.sectionNo = sectionNo;
            dbEnrollClientManager.UpdateCrimeInfo(tbRefNo.Text, "section_no", -1, sectionNo);

            if (OnCloseFlag == 1) StaticData.Enrollment.profile = new ProfileDto();
        }

        private void tbRefNo_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) dtpArrestDate.Focus();
        }
        private void dtpArrestDate_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) tbFullName.Focus();
        }
        private void tbFullName_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) btnAddNickName.Focus();
        }
        private void cmbNationality_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) tbPhone.Focus();
        }
        private void tbPhone_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) cmbReligion.Focus();
        }
        private void cmbReligion_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) btnAddEducation.Focus();
        }
        private void cmbMaritalStatus_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) tbSpouseName.Focus();
        }
        private void cmbOccupation_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) tbHeightFeet.Focus();
        }
        private void tbHeightFeet_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) tbHeightInch.Focus();
        }
        private void tbHeightInch_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) tbFatherName.Focus();
        }
        private void tbFatherName_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) tbFatherPhone.Focus();
        }
        private void tbFatherPhone_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) tbMotherName.Focus();
        }
        private void tbMotherName_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) cmbMaritalStatus.Focus();
        }
        private void tbSpouseName_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) tbSpousePhone.Focus();
        }
        private void tbMotherPhone_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) tbSpouseName.Focus();
        }
        private void tbSpousePhone_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) tbNID.Focus();
        }
        private void tbNID_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) tbDOBYear.Focus();
        }
        private void tbDOBYear_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) tbDOBMonth.Focus();
        }
        private void tbDOBDay_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) btnAddEducation.Focus();
        }
        private void tbDOBMonth_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) tbDOBDay.Focus();
        }
        private void tbVillageRoadHouse_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) cmbDistrictPerm.Focus();
        }
        private void tbVillagePerm_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) tbPostCode.Focus();
        }
        private void cmbDistrict_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) cmbUpazilla.Focus();
        }
        private void cmbUpazilla_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) cmbUnion.Focus();
        }
        private void cmbUnion_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) tbVillageRoadHouse.Focus();
        }
        private void cmbDistrictPerm_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) cmbUpazillaPerm.Focus();
        }
        private void cmbUpazillaPerm_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) cmbUnionPerm.Focus();
        }
        private void cmbUnionPerm_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) tbVillagePerm.Focus();
        }
        private void tbPostCode_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) tbTown.Focus();
        }
        private void tbTown_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) tbState.Focus();
        }
        private void tbState_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) cmbCountry.Focus();
        }
        private void cmbCountry_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) tbSocialSecurityNo.Focus();
        }

        private void tbFIRNumber_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) dtpFIRDate.Focus();
        }
        private void dtpFIRDate_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) tbIOName.Focus();
        }
        private void tbIOName_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) tbIOMobile.Focus();
        }
        private void tbIOBPNo_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) tbGroupGangName.Focus();
        }
        private void tbIOMobile_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) tbIOBPNo.Focus();
        }
        private void tbGroupGangName_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) cmbPoliticalGroup.Focus();
        }
        private void cmbPoliticalGroup_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) dtpDateOfCrime.Focus();
        }
        private void cmbCrimeType_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) cmbBattalion.Focus();
        }
        private void cmbBattalion_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) cmbSubStation.Focus();
        }
        private void cmbSubStation_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) cmbCrimeDistrict.Focus();
        }
        
        private void tbFullName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || e.KeyChar == (Char)Keys.Back || e.KeyChar == ' ' || e.KeyChar == '-' || e.KeyChar == '.') e.Handled = false;
            else e.Handled = true;
        }
        private void tbPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == (Char)Keys.Back) e.Handled = false;
            else e.Handled = true;
        }
        private void tbHeightFeet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == (Char)Keys.Back || e.KeyChar == '.') e.Handled = false;
            else e.Handled = true;
        }
        private void tbHeightInch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == (Char)Keys.Back || e.KeyChar == '.') e.Handled = false;
            else e.Handled = true;
        }
        private void tbFatherName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || e.KeyChar == (Char)Keys.Back || e.KeyChar == ' ' || e.KeyChar == '-' || e.KeyChar == '.') e.Handled = false;
            else e.Handled = true;
        }
        private void tbFatherPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == (Char)Keys.Back) e.Handled = false;
            else e.Handled = true;
        }
        private void tbMotherName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || e.KeyChar == (Char)Keys.Back || e.KeyChar == ' ' || e.KeyChar == '-' || e.KeyChar == '.') e.Handled = false;
            else e.Handled = true;
        }
        private void tbSpouseName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || e.KeyChar == (Char)Keys.Back || e.KeyChar == ' ' || e.KeyChar == '-' || e.KeyChar == '.') e.Handled = false;
            else e.Handled = true;
        }
        private void tbSpousePhone_KeyPress(object sender, KeyPressEventArgs e)
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
        private void tbPostCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == (Char)Keys.Back) e.Handled = false;
            else e.Handled = true;
        }
        private void tbIOName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || e.KeyChar == (Char)Keys.Back || e.KeyChar == ' ' || e.KeyChar == '-' || e.KeyChar == '.') e.Handled = false;
            else e.Handled = true;
        }
        private void tbIOMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == (Char)Keys.Back) e.Handled = false;
            else e.Handled = true;
        }
        private void tbIOBPNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || Char.IsLetter(e.KeyChar) || e.KeyChar == (Char)Keys.Back || e.KeyChar == '-') e.Handled = false;
            else e.Handled = true;
        }
        private void tbNID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == (Char)Keys.Back) e.Handled = false;
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
        private void cmbDistrictPerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbUpazillaPerm.DataSource = null;
            cmbUnionPerm.DataSource = null;
        }
        private void cmbUpazillaPerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbUnionPerm.DataSource = null;
        }
        private void cmbBattalion_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSubStation.DataSource = null;
            //cmbCrimeDistrict.DataSource = null;
            //cmbCrimeThana.DataSource = null;
        }
        private void cmbSubStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmbCrimeDistrict.DataSource = null;
            //cmbCrimeThana.DataSource = null;
        }
        private void cmbCrimeDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCrimeThana.DataSource = null;
            cmbCrimeUnion.DataSource = null;
        }
        private void tabControlCriminalProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!(StaticData.Enrollment.profile.unit >= 0))
                {
                    cmbBattalion.SelectedValue = Users.Unit;
                }
                if (!(StaticData.Enrollment?.profile?.subUnit > 0))
                {
                    cmbSubStation_Enter(null, null);
                    cmbSubStation.SelectedValue = Users.SubUnit;
                }
            }
            catch { }
        }

        private void tbAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == (Char)Keys.Back) e.Handled = false;
            else e.Handled = true;
        }

        private void btnAddRecovery_Click(object sender, EventArgs e)
        {
            var form = new AddRecoveryDialogForm();
            DialogResult dr = form.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (form.recoveryDto != null)
                {
                    if (StaticData.Enrollment.profile.crimeInformation.recoveryList == null)
                    {
                        StaticData.Enrollment.profile.crimeInformation.recoveryList = new List<RecoveryEntryDto>();
                    }
                    StaticData.Enrollment.profile.crimeInformation.recoveryList.Add(form.recoveryDto);
                }

                string recoveries = string.Empty;
                for (int i = 0; i < StaticData.Enrollment.profile.crimeInformation.recoveryList?.Count; i++)
                {                    
                    recoveries += "(" + (i + 1) + ") Type: " + StaticData.Enrollment.profile.crimeInformation.recoveryList[i].recoveryType +
                        ", Name: "+StaticData.Enrollment.profile.crimeInformation.recoveryList[i].recoveryItemName +
                        ", Amount: "+StaticData.Enrollment.profile.crimeInformation.recoveryList[i].amount+". ";
                }
                tbRecovery.Text = recoveries;

                var jsonRecoveries = new JavaScriptSerializer().Serialize
                    (StaticData.Enrollment.profile.crimeInformation.recoveryList);
                dbEnrollClientManager.UpdateCrimeInfo(tbRefNo.Text, "recoveries", -1, jsonRecoveries);
            }
        }

        private void cmbCrimeThana_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCrimeUnion.DataSource = null;
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
                LookupItems lookupItems = new LookupItems();
                lookupItems.LoadUpazillaByDistrict(cmbDistrict.SelectedValue?.ToString());

                if (lookupItems.upazillaList != null)
                {
                    if (lookupItems.upazillaList.Count > 0)
                    {
                        cmbUpazilla.DataSource = new BindingSource(lookupItems.upazillaList, null);
                        cmbUpazilla = Utils.SuggestComboBoxFormat(cmbUpazilla, 1);
                    }
                }
            }
        }

        private void cmbUnion_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbUpazilla.SelectedValue?.ToString()))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please select Upazilla/Thana first");
                cmbUpazilla.Focus();
            }
            else
            {
                LookupItems lookupItems = new LookupItems();
                lookupItems.LoadUnionByUpazilla(cmbUpazilla.SelectedValue?.ToString());

                if (lookupItems.unionList != null)
                {
                    if (lookupItems.unionList.Count > 0)
                    {
                        cmbUnion.DataSource = new BindingSource(lookupItems.unionList, null);
                        cmbUnion = Utils.SuggestComboBoxFormat(cmbUnion, 1);
                    }
                }
            }
        }

        private void cmbUpazillaPerm_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbDistrictPerm.SelectedValue?.ToString()))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please select permanent District first");
                cmbDistrictPerm.Focus();
            }
            else
            {
                LookupItems lookupItems = new LookupItems();
                lookupItems.LoadUpazillaByDistrict(cmbDistrictPerm.SelectedValue?.ToString());

                if (lookupItems.upazillaList != null)
                {
                    if (lookupItems.upazillaList.Count > 0)
                    {
                        cmbUpazillaPerm.DataSource = new BindingSource(lookupItems.upazillaList, null);
                        cmbUpazillaPerm = Utils.SuggestComboBoxFormat(cmbUpazillaPerm, 1);
                    }
                }
            }
        }

        private void cmbUnionPerm_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbUpazillaPerm.SelectedValue?.ToString()))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please select permanent Upazilla/Thana first");
                cmbUnionPerm.Focus();
            }
            else
            {
                LookupItems lookupItems = new LookupItems();
                lookupItems.LoadUnionByUpazilla(cmbUpazillaPerm.SelectedValue?.ToString());

                if (lookupItems.unionList != null)
                {
                    if (lookupItems.unionList.Count > 0)
                    {
                        cmbUnionPerm.DataSource = new BindingSource(lookupItems.unionList, null);
                        cmbUnionPerm = Utils.SuggestComboBoxFormat(cmbUnionPerm, 1);
                    }
                }
            }
        }
        
        private void cmbSubStation_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbBattalion.SelectedValue?.ToString()))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please select battalion first");
                cmbBattalion.Focus();
            }
            else
            {
                LookupItems lookupItems = new LookupItems();
                lookupItems.LoadSubStations(cmbBattalion.SelectedValue?.ToString());

                if (lookupItems.subStationList != null)
                {
                    if (lookupItems.subStationList.Count > 0)
                    {
                        cmbSubStation.DataSource = new BindingSource(lookupItems.subStationList, null);
                        cmbSubStation = Utils.SuggestComboBoxFormat(cmbSubStation, 1);
                    }
                }
            }
        }

        private void cmbCrimeDistrict_Enter(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(cmbSubStation.SelectedValue?.ToString()))
            //{
            //    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please select Sub Station first");
            //    cmbSubStation.Focus();
            //}
            //else
            //{
            //    LookupItems lookupItems = new LookupItems();
            //    lookupItems.LoadRabDistrictBySubUnit(Convert.ToInt32(cmbSubStation.SelectedValue?.ToString()));

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
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please select District first");
                cmbCrimeDistrict.Focus();
            }
            else
            {
                LookupItems lookupItems = new LookupItems();
                lookupItems.LoadUpazillaByDistrict(cmbCrimeDistrict.SelectedValue?.ToString());
                //lookupItems.LoadRabUpazillaBySubUnitRabDistrict(cmbSubStation.SelectedValue?.ToString(), 
                //    cmbCrimeDistrict.SelectedValue?.ToString());

                if (lookupItems.upazillaList != null)
                {
                    if (lookupItems.upazillaList.Count > 0)
                    {
                        cmbCrimeThana.DataSource = new BindingSource(lookupItems.upazillaList, null);
                        cmbCrimeThana = Utils.SuggestComboBoxFormat(cmbCrimeThana, 1);
                    }
                }
            }
        }

        private void cmbCrimeUnion_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbCrimeThana.SelectedValue?.ToString()))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please select Upazilla/Thana first");
                cmbCrimeThana.Focus();
            }
            else
            {
                LookupItems lookupItems = new LookupItems();
                lookupItems.LoadUnionByUpazilla(cmbCrimeThana.SelectedValue?.ToString());

                if (lookupItems.unionList != null)
                {
                    if (lookupItems.unionList.Count > 0)
                    {
                        cmbCrimeUnion.DataSource = new BindingSource(lookupItems.unionList, null);
                        cmbCrimeUnion = Utils.SuggestComboBoxFormat(cmbCrimeUnion, 1);
                    }
                }
            }
        }

        private void cmbFIRthana_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbFIRdistrict.SelectedValue?.ToString()))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please select District first");
                cmbFIRdistrict.Focus();
            }
            else
            {
                LookupItems lookupItems = new LookupItems();
                lookupItems.LoadUpazillaByDistrict(cmbFIRdistrict.SelectedValue?.ToString());

                if (lookupItems.upazillaList != null)
                {
                    if (lookupItems.upazillaList.Count > 0)
                    {
                        cmbFIRthana.DataSource = new BindingSource(lookupItems.upazillaList, null);
                        cmbFIRthana = Utils.SuggestComboBoxFormat(cmbFIRthana, 1);
                    }
                }
            }
        }

        private void tbAge_Enter(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(tbAge.Text)) tbAge.Enabled = false;
            if (!string.IsNullOrEmpty(tbDOBDay.Text) && !string.IsNullOrEmpty(tbDOBMonth.Text) && !string.IsNullOrEmpty(tbDOBYear.Text))
            {
                tbAge.ReadOnly = true;
            }
            else
            {
                tbAge.ReadOnly = false;
            }
        }

        private bool WriteByteContentsToFile(string fileName, byte[] byteArray)
        {
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                    return true;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Exception caught in process: {0}", ex.ToString());
                return false;
            }
        }

        private void dgvSeizureList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                try
                {
                    string FileName = "Attachment" + StaticData.Enrollment?.profile?.attachment?.seizureList[e.RowIndex]?.extension;
                    bool writeSuccess = WriteByteContentsToFile(FileName, StaticData.Enrollment?.profile?.attachment?.seizureList[e.RowIndex]?.seizure);
                    if (writeSuccess)
                    {
                        ProcessingDialog.Run(delegate ()
                        {
                            Process.Start(FileName);
                        });
                    }
                    else
                    {
                        CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not view seizure");
                    }
                }
                catch(Exception x)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not view seizure");
                    logger.Error("Error viewing Seizure. " + x.ToString());
                }
            }
            else if (e.ColumnIndex == 2)
            {
                seizureList.RemoveAt(e.RowIndex);

                if (e.RowIndex < seizureList.Count - 1)
                {
                    for (int i = 0; i < seizureList.Count; i++)
                    {
                        if (seizureList[i].attachmentNumber > 1) seizureList[i].attachmentNumber -= 1;
                    }
                }

                StaticData.Enrollment.profile.seizures = seizureList;
                StaticData.Enrollment.profile.attachment.seizureList = seizureList;

                try
                {
                    dbEnrollClientManager.DeleteAttachment(StaticData.Enrollment.profile, Globals.AttachmentType.SEIZURE, (e.RowIndex + 1));

                    dgvSeizureList.Rows.Clear();
                    for (int i = 0; i < StaticData.Enrollment?.profile?.attachment?.seizureList?.Count; i++)
                    {
                        dgvSeizureList.Rows.Add("Seizure-"+(i+1), "VIEW", "DELETE");
                    }
                }
                catch (Exception)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not delete seizure");
                    return;
                }
            }
        }

        private void dgvComplainList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                try
                {
                    string FileName = "Attachment" + StaticData.Enrollment?.profile?.attachment?.complaintList[e.RowIndex]?.extension;
                    bool writeSuccess = WriteByteContentsToFile(FileName, StaticData.Enrollment?.profile?.attachment?.complaintList[e.RowIndex]?.complaint);
                    if (writeSuccess)
                    {
                        ProcessingDialog.Run(delegate ()
                        {
                            Process.Start(FileName);
                        });
                    }
                    else
                    {
                        CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not view complain");
                    }
                }
                catch(Exception x)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not view complain");
                    logger.Error("Error viewing Complain. " + x.ToString());
                }
            }
            else if (e.ColumnIndex == 2)
            {
                complainList.RemoveAt(e.RowIndex);

                if (e.RowIndex < complainList.Count - 1)
                {
                    for (int i = 0; i < complainList.Count; i++)
                    {
                        if (complainList[i].attachmentNumber > 1) complainList[i].attachmentNumber -= 1;
                    }
                }

                StaticData.Enrollment.profile.complains = complainList;
                StaticData.Enrollment.profile.attachment.complaintList = complainList;

                try
                {
                    dbEnrollClientManager.DeleteAttachment(StaticData.Enrollment.profile, Globals.AttachmentType.COMPLAIN, (e.RowIndex + 1));

                    dgvComplainList.Rows.Clear();
                    for (int i = 0; i < StaticData.Enrollment?.profile?.attachment?.complaintList?.Count; i++)
                    {
                        dgvComplainList.Rows.Add("Complain-"+(i+1), "VIEW", "DELETE");
                    }
                }
                catch (Exception)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not delete complain");
                    return;
                }
            }
        }

        private void dgvFirList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                try
                {
                    string FileName = "Attachment" + StaticData.Enrollment?.profile?.attachment?.firList[e.RowIndex]?.extension;
                    bool writeSuccess = WriteByteContentsToFile(FileName, StaticData.Enrollment?.profile?.attachment?.firList[e.RowIndex]?.fir);
                    if (writeSuccess)
                    {
                        ProcessingDialog.Run(delegate ()
                        {
                            Process.Start(FileName);
                        });
                    }
                    else
                    {
                        CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not view FIR");
                    }
                }
                catch (Exception x)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not view FIR");
                    logger.Error("Error viewing FIR. " + x.ToString());
                }
            }
            else if (e.ColumnIndex == 2)
            {
                firList.RemoveAt(e.RowIndex);

                if (e.RowIndex < firList.Count - 1)
                {
                    for (int i = 0; i < firList.Count; i++)
                    {
                        if (firList[i].attachmentNumber > 1) firList[i].attachmentNumber -= 1;
                    }
                }

                StaticData.Enrollment.profile.firs = firList;
                StaticData.Enrollment.profile.attachment.firList = firList;

                try
                {
                    dbEnrollClientManager.DeleteAttachment(StaticData.Enrollment.profile, Globals.AttachmentType.FIR, (e.RowIndex + 1));
                    dgvFirList.Rows.Clear();
                    for (int i = 0; i < StaticData.Enrollment?.profile?.attachment?.firList?.Count; i++)
                    {
                        dgvFirList.Rows.Add("FIR-"+(i+1), "VIEW", "DELETE");
                    }
                }
                catch (Exception)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not delete FIR");
                    return;
                }
            }
        }

        private void cmbFIRdistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbFIRthana.DataSource = null;
        }

        private void btnBackToCriminalInfo_Click(object sender, EventArgs e)
        {
            tabControlCriminalProfile.SelectedIndex = 0;
        }
    }
}
