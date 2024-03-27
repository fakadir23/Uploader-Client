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
using ISTL.RAB.Controllers.New.Enrollment;
using ISTL.RAB.Entity;
using ISTL.MODELS.DTO.New.Enrollment;
using NLog;
using ISTL.RAB.View.New.Enrollment.CriminalProfile;
using System.Globalization;
using ISTL.RAB.View.New.Enrollment;
using ISTL.MODELS.Request.Report;
using ISTL.MODELS.Response.Report;
using ISTL.RAB.ApiManager;
using System.Net;
using System.Diagnostics;
using System.IO;

namespace ISTL.RAB.View.New
{
    public partial class PreviewAndSubmitUserControl : ViewUserControl
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        public PreviewAndSubmitUserControl()
        {
            InitializeComponent();
            //SetBiometricData();
        }

        private void SetBiometricData()
        {
            try
            {
                this.pbPhoto.Image = Utils.ByteToImage(StaticData.Enrollment.profile.biometric?.photo?.photo);

                this.pbRightIris.Image = Utils.ByteToImage(StaticData.Enrollment.profile.biometric?.iris?.right);
                this.pbLeftIris.Image = Utils.ByteToImage(StaticData.Enrollment.profile.biometric?.iris?.left);

                this.pbRT.Image = AppUtils.AppUtils.WsqToImage(StaticData.Enrollment.profile.biometric?.fingerprint?.rt);
                this.pbRI.Image = AppUtils.AppUtils.WsqToImage(StaticData.Enrollment.profile.biometric?.fingerprint?.ri);
                this.pbRM.Image = AppUtils.AppUtils.WsqToImage(StaticData.Enrollment.profile.biometric?.fingerprint?.rm);
                this.pbRR.Image = AppUtils.AppUtils.WsqToImage(StaticData.Enrollment.profile.biometric?.fingerprint?.rr);
                this.pbRS.Image = AppUtils.AppUtils.WsqToImage(StaticData.Enrollment.profile.biometric?.fingerprint?.rs);

                this.pbLT.Image = AppUtils.AppUtils.WsqToImage(StaticData.Enrollment.profile.biometric?.fingerprint?.lt);
                this.pbLI.Image = AppUtils.AppUtils.WsqToImage(StaticData.Enrollment.profile.biometric?.fingerprint?.li);
                this.pbLM.Image = AppUtils.AppUtils.WsqToImage(StaticData.Enrollment.profile.biometric?.fingerprint?.lm);
                this.pbLR.Image = AppUtils.AppUtils.WsqToImage(StaticData.Enrollment.profile.biometric?.fingerprint?.lr);
                this.pbLS.Image = AppUtils.AppUtils.WsqToImage(StaticData.Enrollment.profile.biometric?.fingerprint?.ls);
            }
            catch (Exception ex)
            {
                logger.Error("There was an error when setting biometric data in the UI. Error:\n" + ex.ToString());
            }
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            if (StaticData.Enrollment.profile != null)
            {
                ProcessingDialog.Run(delegate ()
                {
                    Invoke((MethodInvoker)delegate
                    {
                        ShowPreviewInfo(StaticData.Enrollment.profile);
                    });
                });
            }

            if (StaticData.ModifiableNormalEnrollment == false) btnGetProfileReport.Visible = true;
            else btnGetProfileReport.Visible = false;
        }

        private void ShowPreviewInfo(ProfileDto obj)
        {
            tbRefNo.Text = obj.referenceNo;

            if (!string.IsNullOrEmpty(obj.arrestDate))
            {
                try
                {
                    var formatStrings = new string[] { "yyyy-MM-dd", "yyyy-MM-ddTHH:mm:ss.fffZ", "yyyy-MM-ddTHH:mm:ss.fffK" };
                    DateTime dtArrest = new DateTime();
                    DateTime.TryParseExact(obj.arrestDate, formatStrings, CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out dtArrest);
                    tbArrestDate.Text = dtArrest.ToString("dd-MM-yyyy");
                }
                catch { }
            }

            tbFullName.Text = obj.fullName;

            if (obj.nickName != null)
            {
                for (int i = 0; i < obj.nickName.Count; i++)
                {
                    tbNickName.Text += obj.nickName[i];
                    if (i != (obj.nickName.Count - 1)) tbNickName.Text += ",";
                }
            }

            tbGender.Text = obj.gender;
            if (!string.IsNullOrEmpty(obj.dateOfBirth))
            {
                try
                {
                    var formatStrings = new string[] { "yyyy-MM-dd", "yyyy-MM-ddTHH:mm:ss.fffZ", "yyyy-MM-ddTHH:mm:ss.fffK" };
                    DateTime dtDOB = new DateTime();
                    DateTime.TryParseExact(obj.dateOfBirth, formatStrings, CultureInfo.InvariantCulture, DateTimeStyles.None, out dtDOB);
                    tbDOB.Text = dtDOB.ToString("dd-MM-yyyy");
                }
                catch { }
            }

            if (obj.mobile?.Count > 0)
            {
                for (int i = 0; i < obj.mobile.Count; i++)
                {
                    tbPhoneNumber.Text += obj.mobile[i];
                    if (i != obj.mobile.Count - 1) tbPhoneNumber.Text += ", ";
                }
            }

            if (obj.height != null)
            {
                if (obj.height?.ft > 0) tbHeight.Text = obj.height?.ft + "ft ";
                if (obj.height?.inch > 0) tbHeight.Text += obj.height?.inch + "inch";
            }
            tbOccupation.Text = obj.occupation;
            tbReligion.Text = obj.religion;

            if (obj.educationalInformations != null)
            {
                string eduInfo = null;
                for (int i = 0; i < obj.educationalInformations.Count; i++)
                {
                    if (!string.IsNullOrEmpty(obj.educationalInformations[i].educationalStatus))
                        eduInfo += "Education Status: " + obj.educationalInformations[i].educationalStatus;
                    if (!string.IsNullOrEmpty(obj.educationalInformations[i].nameOfInstitution))
                    {
                        if (!string.IsNullOrEmpty(eduInfo))
                            eduInfo += ", Institute: " + obj.educationalInformations[i].nameOfInstitution;
                        else
                            eduInfo += "Institute: " + obj.educationalInformations[i].nameOfInstitution;
                    }
                    if (obj.educationalInformations[i].politicalInvolvement != null)
                    {
                        string PolInv = string.Empty;
                        if (obj.educationalInformations[i]?.politicalInvolvement == true) PolInv = "Yes";
                        else if (obj.educationalInformations[i]?.politicalInvolvement == false) PolInv = "No";

                        if (!string.IsNullOrEmpty(eduInfo))
                            eduInfo += ", Poltical Involvement: " + PolInv;
                        else
                            eduInfo += "Poltical Involvement: " + PolInv;
                    }
                    if (!string.IsNullOrEmpty(obj.educationalInformations[i].remarks))
                    {
                        if (!string.IsNullOrEmpty(eduInfo))
                            eduInfo += ", Remarks: " + obj.educationalInformations[i].remarks + ". ";
                        else
                            eduInfo += "Remarks: " + obj.educationalInformations[i].remarks + ". ";
                    }
                    tbEducation.Text = eduInfo;
                }
            }

            tbIdentificationMarks.Text = obj.identificationMark;

            if (obj.familys?.Count > 0)
            {
                for (int i = 0; i < obj.familys?.Count; i++)
                {
                    if (obj.familys[i]?.relation == "father")
                    {
                        tbFatherName.Text = obj.familys[i].name;
                        tbFatherPhone.Text = obj.familys[i].phone;
                    }
                    else if (obj.familys[i]?.relation == "mother")
                    {
                        tbMotherName.Text = obj.familys[i].name;
                    }
                    else if (obj.familys[i]?.relation == "spouse")
                    {
                        tbSpouseName.Text = obj.familys[i].name;
                        tbSpousePhone.Text = obj.familys[i].phone;
                    }
                }
            }
            tbMaritalStatus.Text = obj.maritalStatus;

            tbAge.Text = obj.age?.ToString();
            if (string.IsNullOrEmpty(obj.age?.ToString()) && !string.IsNullOrEmpty(tbDOB.Text)) tbAge.Text = CalculateAgeByDob(tbDOB.Text);
            tbNID.Text = obj.nid;

            ShowAddressInfo(obj);

            ShowCrimeInfo(obj);

            SetBiometricData();
        }

        public string CalculateAgeByDob(string dateOfBirth)
        {
            try
            {
                DateTime dob;
                DateTime.TryParseExact(dateOfBirth, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dob);

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

        private void ShowAddressInfo(ProfileDto obj)
        {
            tbPermVillage.Text = obj.permanentAddress?.villageHouseRoadNo;

            tbPresentVillage.Text = obj.presentAddress?.villageHouseRoadNo;

            tbZIPCode.Text = obj.foreignAddress?.zipOrPostCode;
            tbTown.Text = obj.foreignAddress?.town;
            tbState.Text = obj.foreignAddress?.state;

            tbCrimeVillage.Text = obj.crimeInformation?.crimeZone?.addressLine;
        }

        private void ShowCrimeInfo(ProfileDto obj)
        {
            if (obj.attachment?.firList?.Count > 0)
            {
                tbCaseNumber.Text = obj.attachment.firList[0].firNo;
                try
                {
                    DateTime firDate = DateTime.ParseExact(obj.attachment.firList[0]?.firDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    //tbFIRDate.Text = firDate.ToString("yyyy-MM-dd");
                    //DateTime firDate = new DateTime(long.Parse(obj.attachment?.firList[0]?.firDate));
                    tbFIRDate.Text = firDate.ToString("dd-MM-yyyy");

                    dgvFIRList.Rows.Clear();
                    for (int i = 0; i < obj?.attachment?.firList?.Count; i++)
                    {
                        dgvFIRList.Rows.Add("FIR-" + (i + 1), "VIEW");
                    }
                }
                catch { }
            }

            if (obj.attachment?.complaintList?.Count > 0)
            {
                try
                {
                    dgvComplainList.Rows.Clear();
                    for (int i = 0; i < StaticData.Enrollment?.profile?.attachment?.complaintList?.Count; i++)
                    {
                        dgvComplainList.Rows.Add("Complain-" + (i + 1), "VIEW");
                    }
                }
                catch { }
            }

            if (obj.attachment?.seizureList?.Count > 0)
            {
                try
                {
                    dgvSeizureList.Rows.Clear();
                    for (int i = 0; i < obj?.attachment?.seizureList?.Count; i++)
                    {
                        dgvSeizureList.Rows.Add("Seizure-" + (i + 1), "VIEW");
                    }
                }
                catch { }
            }

            tbIOName.Text = obj.investigatingOfficerName;
            tbIOMobile.Text = obj.investigatingOfficerMobile;
            tbIOBPNo.Text = obj.investigatingOfficerBPNumber;
            if (!string.IsNullOrEmpty(obj.iorank))
            {
                string ioRankName = null;
                ComboBoxItems.ioRank.TryGetValue(Convert.ToString(obj.iorank), out ioRankName);
                tbIORank.Text = ioRankName;
            }

            tbGroupName.Text = obj.crimeInformation?.groupOrGangName;
            tbPoliticalGroup.Text = obj.politicalGroup;

            if (obj.crimeInformation?.crimeHistorys != null)
            {
                if (obj.crimeInformation?.crimeHistorys.Count > 0)
                {
                    string dtCrime = obj.crimeInformation?.crimeHistorys[0].dateOfCrime;
                    if (!string.IsNullOrEmpty(dtCrime))
                    {
                        try
                        {
                            var formatStrings = new string[] { "yyyy-MM-dd", "yyyy-MM-ddTHH:mm:ss.fffZ", "yyyy-MM-ddTHH:mm:ss.fffK" };
                            DateTime dateTimeCrime = new DateTime();
                            DateTime.TryParseExact(dtCrime, formatStrings, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTimeCrime);
                            tbCrimeDate.Text = dateTimeCrime.ToString("dd-MM-yyyy");
                        }
                        catch { }
                    }
                }
            }

            tbWarrantType.Text = obj.crimeInformation?.warrant?.warrantType;
            tbWarrantNo.Text = obj.crimeInformation?.warrant?.warrantNo;
            tbSectionNo.Text = obj.crimeInformation?.warrant?.sectionNo;

            tbOfficerName.Text = obj.arrestedBy;
            
            SetValueForLookups(obj);
        }

        private void SetValueForLookups(ProfileDto obj)
        {
            if (obj.nationality?.id > 0)
            {
                tbNationality.Text = ((PreviewSubmitController)controller).GetValueForLookups("country_name_en", "nationality", Convert.ToInt32(obj.nationality?.id));
            }

            if (obj.presentAddress?.district > 0)
            {
                tbPresentDistrict.Text = ((PreviewSubmitController)controller).GetValueForLookups("name_in_english", "district", Convert.ToInt32(obj.presentAddress?.district));
            }
            if (obj.presentAddress?.upazila > 0)
            {
                tbPresentUpazila.Text = ((PreviewSubmitController)controller).GetValueForLookups("name_in_english", "upazila", Convert.ToInt32(obj.presentAddress?.upazila));
            }
            if (obj.presentAddress?.union > 0)
            {
                tbPresentUnion.Text = ((PreviewSubmitController)controller).GetValueForLookups("name_in_english", "eunion", Convert.ToInt32(obj.presentAddress?.union));
            }

            if (obj.permanentAddress?.district > 0)
            {
                tbPermDistrict.Text = ((PreviewSubmitController)controller).GetValueForLookups("name_in_english", "district", Convert.ToInt32(obj.permanentAddress?.district));
            }
            if (obj.permanentAddress?.upazila > 0)
            {
                tbPermUpazila.Text = ((PreviewSubmitController)controller).GetValueForLookups("name_in_english", "upazila", Convert.ToInt32(obj.permanentAddress?.upazila));
            }
            if (obj.permanentAddress?.union > 0)
            {
                tbPermUnion.Text = ((PreviewSubmitController)controller).GetValueForLookups("name_in_english", "eunion", Convert.ToInt32(obj.permanentAddress?.union));
            }

            if (obj.foreignAddress?.country > 0)
            {
                tbCountry.Text = ((PreviewSubmitController)controller).GetValueForLookups("country_name_en", "nationality", Convert.ToInt32(obj.foreignAddress?.country));
            }

            if (obj.crimeInformation?.crimeType > 0)
            {
                //string crimeTypeName = null;
                //ComboBoxItems.crimeType.TryGetValue(Convert.ToString(obj.crimeInformation?.crimeType), out crimeTypeName);
                //tbCrimeType.Text = crimeTypeName;
                string value = ((PreviewSubmitController)controller).GetCrimeTypeValueForLookup(Convert.ToInt32(obj.crimeInformation?.crimeType));
                tbCrimeType.Text = value;
            }
            if (obj.unit >= 0)
            {
                tbBattalion.Text = ((PreviewSubmitController)controller).GetValueForLookups("name_en", "station", Convert.ToInt32(obj.unit));
            }
            if (obj.unit > 0 && obj.subUnit > 0)
            {
                tbSubStation.Text = ((PreviewSubmitController)controller).GetValueForLookups("name_en", "sub_station", Convert.ToInt32(obj.subUnit));
            }
            if (obj.crimeInformation?.crimeZone?.district > 0)
            {
                //tbCrimeDistrict.Text = ((PreviewSubmitController)controller).GetValueForLookups("name_en", "rab_district", Convert.ToInt32(obj.crimeZoneDistrict));
                tbCrimeDistrict.Text = ((PreviewSubmitController)controller).GetValueForLookups("name_in_english", "district", Convert.ToInt32(obj.crimeInformation?.crimeZone?.district));
            }
            if (obj.crimeInformation?.crimeZone?.upozilaOrThana > 0)
            {
                tbCrimeUpazila.Text = ((PreviewSubmitController)controller).GetValueForLookups("name_in_english", "upazila", Convert.ToInt32(obj.crimeInformation?.crimeZone?.upozilaOrThana));
            }
            if (obj.crimeInformation?.crimeZone?.union > 0)
            {
                tbCrimeUnion.Text = ((PreviewSubmitController)controller).GetValueForLookups("name_in_english", "eunion", Convert.ToInt32(obj.crimeInformation?.crimeZone?.union));
            }
            if (obj.attachment?.firList?.Count > 0)
            {
                if (obj.attachment?.firList[0]?.district > 0)
                {
                    tbFIRdistrict.Text = ((PreviewSubmitController)controller).GetValueForLookups("name_in_english", "district", Convert.ToInt32(obj.attachment?.firList[0]?.district));
                }
                if (obj.attachment?.firList[0]?.upozilaOrThana > 0)
                {
                    tbFIRthana.Text = ((PreviewSubmitController)controller).GetValueForLookups("name_in_english", "upazila", Convert.ToInt32(obj.attachment?.firList[0]?.upozilaOrThana));
                }
            }            
        }

        private void btnCriminalProfile_Click(object sender, EventArgs e)
        {
            ((PreviewSubmitController)controller).CriminalProfile();
        }

        private void btnFamilyInfo_Click(object sender, EventArgs e)
        {
            ((PreviewSubmitController)controller).Family();
        }

        private void btnBiometric_Click(object sender, EventArgs e)
        {
            ((PreviewSubmitController)controller).Biometric();
        }

        private void label71_Click(object sender, EventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            ((PreviewSubmitController)controller).Submit();
        }

        private void lblIdentificationMarks_Click(object sender, EventArgs e)
        {

        }

        private void PreviewAndSubmitUserControl_Load(object sender, EventArgs e)
        {

        }

        private void btnViewOtherInfo_Click(object sender, EventArgs e)
        {
            if (StaticData.Enrollment.profile.otherInformationList?.Count > 0)
            {
                var form = new OtherInfoDialogForm();
                form.ShowDialog();
            }
            else
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "No other information to show");
            }
        }

        private void btnViewRecovery_Click(object sender, EventArgs e)
        {
            if (StaticData.Enrollment?.profile?.crimeInformation?.recoveryList?.Count > 0)
            {
                var form = new RecoveryDialogForm();
                form.ShowDialog();
            }
            else
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "No recovery item to show");
            }
        }

        private void btnGetProfileReport_Click(object sender, EventArgs e)
        {
            OnGenerateProfileReport();
        }

        private void OnGenerateProfileReport()
        {
            CriminalReportRequest request = new CriminalReportRequest();
            CriminalProfileOrCombinedReportResponse response = new CriminalProfileOrCombinedReportResponse();

            //request.referenceNo = tbRefNo.Text;
            //request.unit = StaticData.Enrollment?.profile?.unit;
            request.id = StaticData.Enrollment?.profile?.id;

            //if (string.IsNullOrEmpty(request.referenceNo)) return;
            if (string.IsNullOrEmpty(request.id))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "This record possibly does not exist in CDMS database");
                return;
            }

            string ProcessingErrorMsg = string.Empty;

            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    response = new ReportApiManager().GetCriminalProfileReport(request);
                }
                catch (WebException x)
                {
                    logger.Debug("Known error, when server not found when searching criminal. Error Message: " + x.Message);
                    ProcessingErrorMsg = "Seems you are Offline while trying to download report. Please contact with your Administrator.";
                }
                catch (TimeoutException x)
                {
                    logger.Debug("Connection timedout when searching criminal. Error Message: " + x.Message);
                    ProcessingErrorMsg = "Seems you are Offline while trying to download report. Please contact with your Administrator.";
                }
                catch (Exception x)
                {
                    logger.Error("There was an unexpected error when searching criminal.\n" + x.ToString());
                    ProcessingErrorMsg = "There was an unexpected error when trying to download report. Please contact with your Administrator.";
                }
            });

            if (!string.IsNullOrEmpty(ProcessingErrorMsg))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", ProcessingErrorMsg);
                return;
            }

            string errorMsg = string.Empty;

            if (response != null)
            {
                if (response.code == 200)
                {
                    if (response.reportResultList?.Count > 0)
                    {
                        string url = response.reportResultList[0].url;
                        if (!string.IsNullOrEmpty(url) && response.reportResultList[0].status == "TRUE")
                        {
                            try
                            {
                                Process.Start(url);
                            }
                            catch (Exception x)
                            {
                                logger.Error("Could not download individual report. " + x.ToString());
                                errorMsg = "Could not download report";
                            }
                        }
                        else
                        {
                            errorMsg = "Could not download report";
                        }
                    }
                    else
                    {
                        errorMsg = "Could not download report";
                    }
                }
                else
                {
                    errorMsg = "Could not download report";
                }
            }
            else
            {
                errorMsg = "Could not download report";
            }

            if (!string.IsNullOrEmpty(errorMsg))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", errorMsg);
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
                string FileName = "Attachment" + StaticData.Enrollment.profile.attachment.seizureList[e.RowIndex].extension;
                bool writeSuccess = WriteByteContentsToFile(FileName, StaticData.Enrollment.profile.attachment.seizureList[e.RowIndex].seizure);
                if (writeSuccess)
                {
                    try
                    {
                        ProcessingDialog.Run(delegate ()
                        {
                            Process.Start(FileName);
                        });
                    }
                    catch (Exception x)
                    {
                        CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not view Seizure");
                        logger.Error("Error viewing Seizure. " + x.ToString());
                    }
                }
                else
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not view Seizure");
                }
            }
        }

        private void dgvComplainList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                string FileName = "Attachment" + StaticData.Enrollment.profile.attachment.complaintList[e.RowIndex].extension;
                bool writeSuccess = WriteByteContentsToFile(FileName, StaticData.Enrollment.profile.attachment.complaintList[e.RowIndex].complaint);
                if (writeSuccess)
                {
                    try
                    {
                        ProcessingDialog.Run(delegate ()
                        {
                            Process.Start(FileName);
                        });
                    }
                    catch (Exception x)
                    {
                        CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not view Complain");
                        logger.Error("Error viewing Complain. " + x.ToString());
                    }
                }
                else
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not view Complain");
                }
            }
        }

        private void dgvFIRList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                string FileName = "Attachment" + StaticData.Enrollment.profile.attachment.firList[e.RowIndex].extension;
                bool writeSuccess = WriteByteContentsToFile(FileName, StaticData.Enrollment.profile.attachment.firList[e.RowIndex].fir);
                if (writeSuccess)
                {
                    try
                    {
                        ProcessingDialog.Run(delegate ()
                        {
                            Process.Start(FileName);
                        });
                    }
                    catch (Exception x)
                    {
                        CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not view FIR");
                        logger.Error("Error viewing FIR. " + x.ToString());
                    }
                }
                else
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not view FIR");
                }
            }
        }

        private void label51_Click(object sender, EventArgs e)
        {

        }

        private void tbWarrantNo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
