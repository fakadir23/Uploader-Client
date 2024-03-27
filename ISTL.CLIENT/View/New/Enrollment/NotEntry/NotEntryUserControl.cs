using ISTL.COMMON;
using ISTL.COMMON.Common;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.MODELS.DTO.New.NotEntry;
using ISTL.RAB.Controllers.New.Enrollment;
using ISTL.RAB.Controllers.New.Enrollment.NotEntry;
using ISTL.RAB.Entity;
using ISTL.RAB.Entity.Lookup;
using ISTL.RAB.View.New.Enrollment.BiometricInformation;
using NLog;
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
using System.Windows.Forms;

namespace ISTL.RAB.View.New.Enrollment.NotEntry
{
    public partial class NotEntryUserControl : ViewUserControl
    {
        private Logger logger = LogManager.GetCurrentClassLogger();

        public NotEntryUserControl()
        {
            InitializeComponent();

            LoadComboItems();
            dtpNotEntryDate.CustomFormat = "dd/MM/yyyy";
            dtpFIRDate.CustomFormat = "dd/MM/yyyy";
            dtpWarrantIssueDate.CustomFormat = "dd/MM/yyyy";
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            tbRefNo.Text = Guid.NewGuid().ToString().Replace("-", "").ToUpper().Substring(0, 10);

            try
            {
                cmbUnit.SelectedValue = Users.Unit;

                cmbUnit.Enabled = false;
            }
            catch { }
            try
            {
                cmbSubUnit_Enter(null, null);
                cmbSubUnit.SelectedValue = Users.SubUnit;

                cmbSubUnit.Enabled = false;
            }
            catch { }

            if (!string.IsNullOrEmpty(StaticData.NotEntry?.referenceNo))
            {
                ShowStaticNotEntryProfile();
                if (StaticData.ModifiableNotEntry == false)
                {
                    MakeFieldsReadonly();
                }
            }
        }

        public override void OnClosing()
        {
            base.OnClosing();
            if (!btnBiometric.ContainsFocus && !btnPreviewSubmit.ContainsFocus && !btnBasicSaveNext.ContainsFocus)
            {
                StaticData.NotEntry = new NotEntryDto();
            }
        }

        private void LoadComboItems()
        {
            cmbReason.DataSource = new BindingSource(ComboBoxItems.notEntryReason, null);
            cmbReason = Utils.SuggestComboBoxFormat(cmbReason, 0);

            cmbCaseType.DataSource = new BindingSource(ComboBoxItems.caseType, null);
            cmbCaseType = Utils.SuggestComboBoxFormat(cmbCaseType, 0);

            LoadGeoLookup();
        }

        private void LoadGeoLookup()
        {
            LookupItems lookupItems = new LookupItems();
            lookupItems.LoadDistrict();
            lookupItems.LoadStations();

            if (lookupItems.districtList != null)
            {
                if (lookupItems.districtList.Count > 0)
                {
                    cmbDistrict.DataSource = new BindingSource(lookupItems.districtList, null);
                    cmbDistrict = Utils.SuggestComboBoxFormat(cmbDistrict, 1);

                    cmbFIRdistrict.DataSource = new BindingSource(lookupItems.districtList, null);
                    cmbFIRdistrict = Utils.SuggestComboBoxFormat(cmbFIRdistrict, 1);
                }
            }

            if (lookupItems.stationList != null)
            {
                if (lookupItems.stationList.Count > 0)
                {
                    cmbUnit.DataSource = new BindingSource(lookupItems.stationList, null);
                    cmbUnit = Utils.SuggestComboBoxFormat(cmbUnit, 1);
                }
            }
        }

        private void ShowStaticNotEntryProfile()
        {
            try
            {
                tbRefNo.Text = StaticData.NotEntry.referenceNo;

                if (StaticData.NotEntry.unit > 0)    cmbUnit.SelectedValue = StaticData.NotEntry.unit;

                if (StaticData.NotEntry.subUnit > 0)
                {
                    cmbSubUnit_Enter(null, null);
                    cmbSubUnit.SelectedValue = StaticData.NotEntry.subUnit;
                }

                if (!string.IsNullOrEmpty(StaticData.NotEntry.notEntryReason)) cmbReason.SelectedValue = StaticData.NotEntry.notEntryReason;
                if (!string.IsNullOrEmpty(StaticData.NotEntry.notEntryCaseType)) cmbCaseType.SelectedValue = StaticData.NotEntry.notEntryCaseType;

                try
                {
                    var formatStrings = new string[] { "yyyy-MM-dd", "yyyy-MM-ddTHH:mm:ss.fffZ", "yyyy-MM-ddTHH:mm:ss.fffK", "dd-MM-yyyy" };
                    DateTime dtValue = new DateTime();
                    DateTime.TryParseExact(StaticData.NotEntry.noEntryDate, formatStrings, CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out dtValue);
                    dtpNotEntryDate.Value = dtValue;
                }
                catch { }

                tbAccusedName.Text = StaticData.NotEntry.accusedName;
                tbFatherName.Text = StaticData.NotEntry.fatherName;
                tbMobileNumber.Text = StaticData.NotEntry.mobileNo;

                // Address Data
                if (StaticData.NotEntry.address?.district > 0)
                {
                    cmbDistrict.SelectedValue = StaticData.NotEntry.address?.district;
                    if (StaticData.NotEntry.address?.upazila > 0)
                    {
                        cmbUpazilla_Enter(null, null);
                        cmbUpazilla.SelectedValue = StaticData.NotEntry.address?.upazila;
                        if (StaticData.NotEntry.address?.union > 0)
                        {
                            cmbUnion_Enter(null, null);
                            cmbUnion.SelectedValue = StaticData.NotEntry.address?.union;
                        }
                    }
                }
                tbVillageRoadHouse.Text = StaticData.NotEntry.address?.villageHouseRoadNo;

                tbWarrantNo.Text = StaticData.NotEntry.warrantNo;
                try
                {
                    var formatStrings = new string[] { "yyyy-MM-dd", "yyyy-MM-ddTHH:mm:ss.fffZ", "yyyy-MM-ddTHH:mm:ss.fffK", "dd-MM-yyyy" };
                    DateTime dtValue = new DateTime();
                    DateTime.TryParseExact(StaticData.NotEntry.warrantIssueDate, formatStrings, CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out dtValue);
                    dtpWarrantIssueDate.Value = dtValue;
                }
                catch { }

                // Attachment
                if (StaticData.NotEntry.attachment?.complaintList?.Count > 0)
                {
                    complainList = StaticData.NotEntry.attachment?.complaintList;

                    for (int i = 0; i < StaticData.NotEntry.attachment?.complaintList?.Count; i++)
                    {
                        dgvComplainList.Rows.Add("Complain-" + (i + 1), "View", "Delete");
                    }
                }

                if (StaticData.NotEntry.attachment?.seizureList?.Count > 0)
                {
                    seizureList = StaticData.NotEntry.attachment?.seizureList;

                    for (int i = 0; i < StaticData.NotEntry.attachment?.seizureList?.Count; i++)
                    {
                        dgvSeizureList.Rows.Add("Seizure-" + (i + 1), "View", "Delete");
                    }
                }

                if (StaticData.NotEntry.attachment?.firList?.Count > 0)
                {
                    firList = StaticData.NotEntry.attachment?.firList;

                    tbFIRNumber.Text = firList[0].firNo;

                    try
                    {
                        var formatStrings = new string[] { "yyyy-MM-dd", "yyyy-MM-ddTHH:mm:ss.fffZ", "yyyy-MM-ddTHH:mm:ss.fffK", "dd-MM-yyyy" };
                        DateTime dtValue = new DateTime();
                        DateTime.TryParseExact(firList[0].firDate, formatStrings, CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out dtValue);
                        dtpFIRDate.Value = dtValue;
                    }
                    catch { }

                    try
                    {
                        cmbFIRdistrict.SelectedValue = StaticData.NotEntry.attachment?.firList[0].district;
                        if (StaticData.NotEntry.attachment?.firList[0].upozilaOrThana > 0)
                        {
                            cmbFIRthana_Enter(null, null);
                            cmbFIRthana.SelectedValue = StaticData.NotEntry.attachment?.firList[0].upozilaOrThana;
                        }
                    }
                    catch { }

                    for (int i = 0; i < StaticData.NotEntry.attachment?.firList?.Count; i++)
                    {
                        dgvFirList.Rows.Add("FIR/Warrant-" + (i + 1), "View", "Delete");
                    }
                }
            }
            catch { }
        }

        private void MakeFieldsReadonly()
        {
            if (StaticData.NotEntry.unit > 0) cmbUnit.Enabled = false;
            if (StaticData.NotEntry.subUnit > 0) cmbSubUnit.Enabled = false;

            if (!string.IsNullOrEmpty(StaticData.NotEntry.notEntryReason)) cmbReason.Enabled = false;
            if (!string.IsNullOrEmpty(StaticData.NotEntry.notEntryCaseType)) cmbCaseType.Enabled = false;
            if (!string.IsNullOrEmpty(StaticData.NotEntry.noEntryDate)) dtpNotEntryDate.Enabled = false;

            if (!string.IsNullOrEmpty(StaticData.NotEntry.accusedName)) tbAccusedName.Enabled = false;
            if (!string.IsNullOrEmpty(StaticData.NotEntry.fatherName)) tbFatherName.Enabled = false;
            if (!string.IsNullOrEmpty(StaticData.NotEntry.mobileNo)) tbMobileNumber.Enabled = false;

            if (StaticData.NotEntry.address?.district > 0)
            {
                cmbDistrict.Enabled = false;
                if (StaticData.NotEntry.address?.upazila > 0)
                {
                    cmbUpazilla.Enabled = false;
                    if (StaticData.NotEntry.address?.union > 0)
                    {
                        cmbUnion.Enabled = false;
                    }
                }
            }
            if (!string.IsNullOrEmpty(StaticData.NotEntry.address?.villageHouseRoadNo)) tbVillageRoadHouse.Enabled = false;

            if (!string.IsNullOrEmpty(StaticData.NotEntry.warrantNo)) tbWarrantNo.Enabled = false;
            if (!string.IsNullOrEmpty(StaticData.NotEntry.warrantNo) && 
                !string.IsNullOrEmpty(StaticData.NotEntry.warrantIssueDate)) dtpWarrantIssueDate.Enabled = false;

            if (StaticData.NotEntry.attachment != null)
            {
                if (StaticData.NotEntry.attachment.seizureList?.Count > 0)
                {
                    btnUploadSeizure.Enabled = false;
                    dgvSeizureList.Columns[2].Visible = false;
                }
                if (StaticData.NotEntry.attachment.complaintList?.Count > 0)
                {
                    btnComplainUpload.Enabled = false;
                    dgvComplainList.Columns[2].Visible = false;
                }
                if (StaticData.NotEntry.attachment.firList?.Count > 0)
                {
                    if (!string.IsNullOrEmpty(StaticData.NotEntry.attachment.firList[0].firNo))    tbFIRNumber.Enabled = false;
                    if (!string.IsNullOrEmpty(StaticData.NotEntry.attachment.firList[0].firDate))     dtpFIRDate.Enabled = false;
                    if (StaticData.NotEntry.attachment.firList[0].district > 0)     cmbFIRdistrict.Enabled = false;
                    if (StaticData.NotEntry.attachment.firList[0].upozilaOrThana > 0)       cmbFIRthana.Enabled = false;
                    btnUploadFIR.Enabled = false;
                    dgvFirList.Columns[2].Visible = false;
                }
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            StaticData.NotEntry = new NotEntryDto();
            ((NotEntryController)controller).GoBackToDashboard();
        }

        private bool FieldValidationCheck()
        {
            if (string.IsNullOrEmpty(cmbUnit.SelectedValue?.ToString()))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Unit can not be empty");
                return false;
            }
            if (string.IsNullOrEmpty(cmbSubUnit.SelectedValue?.ToString()))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Sub-Unit can not be empty");
                return false;
            }

            if (string.IsNullOrEmpty(cmbReason.SelectedValue?.ToString()))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Reason can not be empty");
                return false;
            }
            if (string.IsNullOrEmpty(cmbCaseType.SelectedValue?.ToString()))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Case type can not be empty");
                return false;
            }

            // Accused Name
            if (!string.IsNullOrEmpty(tbAccusedName.Text))
            {
                if (!Utils.IsAlphaCheck(tbAccusedName.Text))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Accused Name can contain only letters");
                    return false;
                }
            }
            else
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Accused Name cannot be empty");
                return false;
            }

            // Father Name
            if (!string.IsNullOrEmpty(tbFatherName.Text))
            {
                if (!Utils.IsAlphaCheck(tbFatherName.Text))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Father Name can contain only letters");
                    return false;
                }
            }
            else
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Father Name cannot be empty");
                return false;
            }

            // Mobile Number
            if (!string.IsNullOrEmpty(tbMobileNumber.Text))
            {
                if (!Utils.isDigit(tbMobileNumber.Text))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Phone number can contain only digits");
                    return false;
                }
                if (!tbMobileNumber.Text.StartsWith("01"))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Phone number must start with 01");
                    return false;
                }
                if (tbMobileNumber.Text.Length != 11)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Phone number has to be 11 digits");
                    return false;
                }
            }            

            // Address
            if (string.IsNullOrEmpty(cmbDistrict.SelectedValue?.ToString()))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "District can not be empty");
                return false;
            }
            if(string.IsNullOrEmpty(cmbUpazilla.SelectedValue?.ToString()))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Upazila can not be empty");
                return false;
            }
            if (string.IsNullOrEmpty(cmbUnion.SelectedValue?.ToString()))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Union can not be empty");
                return false;
            }
            if (string.IsNullOrEmpty(tbVillageRoadHouse.Text))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Village can not be empty");
                return false;
            }

            if (StaticData.NotEntry.attachment?.firList?.Count > 0)
            {
                if (string.IsNullOrEmpty(tbFIRNumber.Text))
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Case/FIR No can not be empty if there are FIR(s)");
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(tbFIRNumber.Text))
            {
                if (StaticData.NotEntry.attachment?.firList == null || StaticData.NotEntry.attachment?.firList?.Count == 0)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Case/FIR information can not be saved if there are no uploaded FIR(s)");
                    return false;
                }
            }

            return true;
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
        private void cmbFIRdistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbFIRthana.DataSource = null;
        }
        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSubUnit.DataSource = null;
        }
        private void cmbSubUnit_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbUnit.SelectedValue?.ToString()))
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please select battalion first");
                cmbUnit.Focus();
            }
            else
            {
                LookupItems lookupItems = new LookupItems();
                lookupItems.LoadSubStations(cmbUnit.SelectedValue?.ToString());

                if (lookupItems.subStationList != null)
                {
                    if (lookupItems.subStationList.Count > 0)
                    {
                        cmbSubUnit.DataSource = new BindingSource(lookupItems.subStationList, null);
                        cmbSubUnit = Utils.SuggestComboBoxFormat(cmbSubUnit, 1);
                    }
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
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Seizure size is too big. Size should not exceed " + (maxSize / (1000 * 1024)) + " MB");
                    return;
                }
                int currentUploadedSeizureSize = 0;
                for (int i = 0; i < StaticData.NotEntry?.attachment?.seizureList?.Count; i++)
                {
                    currentUploadedSeizureSize += (int)(StaticData.NotEntry?.attachment?.seizureList[i].seizure?.Length);
                }
                if (currentUploadedSeizureSize + uploadedFileSize > maxSize)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Seizure size is too big. Max Seizure Size is " + (maxSize / (1000 * 1024)) + " MB");
                    return;
                }
                string fileExt = Path.GetExtension(openFileDialog.FileName);

                SeizureDto seizureDto = new SeizureDto();
                seizureDto.attachmentNumber = StaticData.NotEntry?.attachment?.seizureList?.Count + 1;
                seizureDto.extension = fileExt;
                if (!string.IsNullOrEmpty(fileExt) && fileExt.Length > 2)
                {
                    if (fileExt != ".pdf" && fileExt != ".PDF") seizureDto.contentType = "image/" + fileExt.Substring(1, fileExt.Length - 1);
                    else if (fileExt == ".pdf" || fileExt == ".PDF") seizureDto.contentType = "application/" + fileExt.Substring(1, fileExt.Length - 1);
                }
                seizureDto.seizure = ByteContents;

                seizureList.Add(seizureDto);

                StaticData.NotEntry.attachment.seizureList = seizureList;

                try
                {                    
                    dgvSeizureList.Rows.Clear();
                    for (int i = 0; i < StaticData.NotEntry?.attachment?.seizureList?.Count; i++)
                    {
                        dgvSeizureList.Rows.Add("Seizure-" + (i + 1), "VIEW", "DELETE");
                    }
                }
                catch (Exception)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not upload seizure successfully");
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
                for (int i = 0; i < StaticData.NotEntry?.attachment?.complaintList?.Count; i++)
                {
                    currentUploadedComplainSize += (int)(StaticData.NotEntry?.attachment?.complaintList[i].complaint?.Length);
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

                StaticData.NotEntry.attachment.complaintList = complainList;

                try
                {                    
                    dgvComplainList.Rows.Clear();
                    for (int i = 0; i < StaticData.NotEntry?.attachment?.complaintList?.Count; i++)
                    {
                        dgvComplainList.Rows.Add("Complain-" + (i + 1), "VIEW", "DELETE");
                    }
                }
                catch (Exception)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not upload complaint successfully");
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
                for (int i = 0; i < StaticData.NotEntry?.attachment?.firList?.Count; i++)
                {
                    currentUploadedFIRSize += (int)(StaticData.NotEntry?.attachment?.firList[i].fir?.Length);
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
                        //firDto.firDate = DateFIR.ToString("yyyy-MM-dd");
                        firDto.firDate = DateFIR.ToString("dd-MM-yyyy");
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

                StaticData.NotEntry.attachment.firList = firList;

                try
                {                    
                    dgvFirList.Rows.Clear();
                    for (int i = 0; i < StaticData.NotEntry?.attachment?.firList?.Count; i++)
                    {
                        dgvFirList.Rows.Add("FIR/Warrant-" + (i + 1), "VIEW", "DELETE");
                    }
                }
                catch (Exception)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not upload FIR successfully");
                    return;
                }
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
                    string FileName = "Attachment" + StaticData.NotEntry?.attachment?.seizureList[e.RowIndex]?.extension;
                    bool writeSuccess = WriteByteContentsToFile(FileName, StaticData.NotEntry?.attachment?.seizureList[e.RowIndex]?.seizure);
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
                catch (Exception x)
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

                StaticData.NotEntry.attachment.seizureList = seizureList;

                try
                {
                    dgvSeizureList.Rows.Clear();
                    for (int i = 0; i < StaticData.NotEntry?.attachment?.seizureList?.Count; i++)
                    {
                        dgvSeizureList.Rows.Add("Seizure-" + (i + 1), "VIEW", "DELETE");
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
                    string FileName = "Attachment" + StaticData.NotEntry?.attachment?.complaintList[e.RowIndex]?.extension;
                    bool writeSuccess = WriteByteContentsToFile(FileName, StaticData.NotEntry?.attachment?.complaintList[e.RowIndex]?.complaint);
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
                catch (Exception x)
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

                StaticData.NotEntry.attachment.complaintList = complainList;

                try
                {
                    dgvComplainList.Rows.Clear();
                    for (int i = 0; i < StaticData.NotEntry?.attachment?.complaintList?.Count; i++)
                    {
                        dgvComplainList.Rows.Add("Complain-" + (i + 1), "VIEW", "DELETE");
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
                    string FileName = "Attachment" + StaticData.NotEntry?.attachment?.firList[e.RowIndex]?.extension;
                    bool writeSuccess = WriteByteContentsToFile(FileName, StaticData.NotEntry?.attachment?.firList[e.RowIndex]?.fir);
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

                StaticData.NotEntry.attachment.firList = firList;

                try
                {
                    dgvFirList.Rows.Clear();
                    for (int i = 0; i < StaticData.NotEntry?.attachment?.firList?.Count; i++)
                    {
                        dgvFirList.Rows.Add("FIR/Warrant-" + (i + 1), "VIEW", "DELETE");
                    }
                }
                catch (Exception)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Could not delete FIR");
                    return;
                }
            }
        }
        
        private bool SaveProfileToStatic()
        {
            bool valid = FieldValidationCheck();

            if (!valid) return valid;

            // Basic

            StaticData.NotEntry.referenceNo = (!string.IsNullOrEmpty(tbRefNo.Text)) ? tbRefNo.Text : null;
            if (!string.IsNullOrEmpty(cmbUnit.SelectedValue?.ToString())) StaticData.NotEntry.unit = Convert.ToInt32(cmbUnit.SelectedValue?.ToString());
            if (!string.IsNullOrEmpty(cmbSubUnit.SelectedValue?.ToString())) StaticData.NotEntry.subUnit = Convert.ToInt32(cmbSubUnit.SelectedValue?.ToString());

            if (!string.IsNullOrEmpty(cmbReason.SelectedValue?.ToString())) StaticData.NotEntry.notEntryReason = cmbReason.SelectedValue?.ToString();
            if (!string.IsNullOrEmpty(cmbCaseType.SelectedValue?.ToString())) StaticData.NotEntry.notEntryCaseType = cmbCaseType.SelectedValue?.ToString();

            if (!string.IsNullOrEmpty(dtpNotEntryDate.Text))
            {
                try
                {
                    DateTime DateFIR = DateTime.ParseExact(dtpNotEntryDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //StaticData.NotEntry.noEntryDate = DateFIR.ToString("yyyy-MM-dd");
                    StaticData.NotEntry.noEntryDate = DateFIR.ToString("dd-MM-yyyy");
                }
                catch (Exception) { }
            }

            //if (string.IsNullOrEmpty(StaticData.NotEntry.noEntryDate))
            //{
            //    // StaticData.NotEntry.noEntryDate = DateTime.Now.ToString("dd-MM-yyyy"); // Commented out as client requires this to be editable as of 17/04/2022
            //}

            StaticData.NotEntry.accusedName = (!string.IsNullOrEmpty(tbAccusedName.Text)) ? tbAccusedName.Text : null;
            StaticData.NotEntry.fatherName = (!string.IsNullOrEmpty(tbFatherName.Text)) ? tbFatherName.Text : null;
            StaticData.NotEntry.mobileNo = (!string.IsNullOrEmpty(tbMobileNumber.Text)) ? tbMobileNumber.Text : null;

            // Address

            AddressDto addressDto = new AddressDto();
            if (!string.IsNullOrEmpty(cmbDistrict.SelectedValue?.ToString())) addressDto.district = Convert.ToInt32(cmbDistrict.SelectedValue?.ToString());
            if (!string.IsNullOrEmpty(cmbUpazilla.SelectedValue?.ToString())) addressDto.upazila = Convert.ToInt32(cmbUpazilla.SelectedValue?.ToString());
            if (!string.IsNullOrEmpty(cmbUnion.SelectedValue?.ToString())) addressDto.union = Convert.ToInt32(cmbUnion.SelectedValue?.ToString());
            addressDto.villageHouseRoadNo = (!string.IsNullOrEmpty(tbVillageRoadHouse.Text)) ? tbVillageRoadHouse.Text : null;

            if (StaticData.NotEntry.address == null) StaticData.NotEntry.address = new AddressDto();
            StaticData.NotEntry.address = addressDto;

            StaticData.NotEntry.warrantNo = (!string.IsNullOrEmpty(tbWarrantNo.Text)) ? tbWarrantNo.Text : null;
            if (!string.IsNullOrEmpty(StaticData.NotEntry.warrantNo) && !string.IsNullOrEmpty(dtpWarrantIssueDate.Text))
            {
                try
                {
                    DateTime DateVal = DateTime.ParseExact(dtpWarrantIssueDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    StaticData.NotEntry.warrantIssueDate = DateVal.ToString("dd-MM-yyyy");
                }
                catch (Exception) { }
            }

            if (StaticData.NotEntry.attachment?.firList?.Count > 0)
            {
                if (!string.IsNullOrEmpty(tbFIRNumber.Text))
                {
                    StaticData.NotEntry.attachment.firList[0].firNo = tbFIRNumber.Text;
                }
                if (!string.IsNullOrEmpty(dtpFIRDate.Text))
                {
                    try
                    {
                        DateTime DateFIR = DateTime.ParseExact(dtpFIRDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //firDto.firDate = DateFIR.ToString("yyyy-MM-dd");
                        StaticData.NotEntry.attachment.firList[0].firDate = DateFIR.ToString("dd-MM-yyyy");
                    }
                    catch (Exception) { }
                }

                if (!string.IsNullOrEmpty(cmbFIRdistrict.SelectedValue?.ToString()))
                {
                    StaticData.NotEntry.attachment.firList[0].district = (!string.IsNullOrEmpty(cmbFIRdistrict.SelectedValue?.ToString())) ? Convert.ToInt32(cmbFIRdistrict.SelectedValue?.ToString()) : 0;
                }
                if (!string.IsNullOrEmpty(cmbFIRthana.SelectedValue?.ToString()))
                {
                    StaticData.NotEntry.attachment.firList[0].upozilaOrThana = (!string.IsNullOrEmpty(cmbFIRthana.SelectedValue?.ToString())) ? Convert.ToInt32(cmbFIRthana.SelectedValue?.ToString()) : 0;
                }
            }

            return true;
        }

        private void btnBasicSaveNext_Click(object sender, EventArgs e)
        {
            bool save = SaveProfileToStatic();

            if (!save) return;

            ((NotEntryController)controller).OnNoEntryBiometric();
        }

        private void btnBiometric_Click(object sender, EventArgs e)
        {
            btnBasicSaveNext_Click(null, null);
            //((NotEntryController)controller).OnNoEntryBiometric();
        }

        private void btnPreviewSubmit_Click(object sender, EventArgs e)
        {
            bool save = SaveProfileToStatic();

            if (!save) return;

            ((NotEntryController)controller).SubmitPreview();
        }
    }
}
