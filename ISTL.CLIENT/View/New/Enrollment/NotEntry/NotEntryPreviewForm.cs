using ISTL.COMMON;
using ISTL.MODELS.DTO.New.NotEntry;
using ISTL.RAB.DbManager;
using ISTL.RAB.Entity;
using ISTL.RAB.Entity.Lookup;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class NotEntryPreviewForm : ViewForm
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private DbLookupManager dblookupManager;

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

        public NotEntryPreviewForm()
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
                    ShowNotEntryProfilePreview(StaticData.NotEntry);
                });
            });
        }

        public override void OnClosing()
        {
            base.OnClosing();

            StaticData.NotEntry = new NotEntryDto();
        }

        private void ShowNotEntryProfilePreview(NotEntryDto dto)
        {
            try
            {
                tbPreviewRefNo.Text = dto.referenceNo;
                tbPreviewUnit.Text = GetValueForLookups("name_en", "station", Convert.ToInt32(dto.unit));
                tbPreviewSubUnit.Text = GetValueForLookups("name_en", "sub_station", Convert.ToInt32(dto.subUnit));
                if (dto.notEntryReason != null)
                {
                    string val = null;
                    ComboBoxItems.notEntryReason.TryGetValue(Convert.ToString(dto.notEntryReason), out val);
                    tbPreviewReason.Text = val;
                }
                if (dto.notEntryCaseType != null)
                {
                    string val = null;
                    ComboBoxItems.caseType.TryGetValue(Convert.ToString(dto.notEntryCaseType), out val);
                    tbPreviewCaseType.Text = val;
                }

                tbPreviewNotEntryDate.Text = dto.noEntryDate;
                tbPreviewAccusedName.Text = dto.accusedName;
                tbPreviewFatherName.Text = dto.fatherName;
                tbPreviewMobileNo.Text = dto.mobileNo;

                if (dto.address?.district > 0)
                {
                    tbPreviewDistrict.Text = GetValueForLookups("name_in_english", "district", Convert.ToInt32(dto.address?.district));
                }
                if (dto.address?.upazila > 0)
                {
                    tbPreviewThana.Text = GetValueForLookups("name_in_english", "upazila", Convert.ToInt32(dto.address?.upazila));
                }
                if (dto.address?.district > 0)
                {
                    tbPreviewUnion.Text = GetValueForLookups("name_in_english", "eunion", Convert.ToInt32(dto.address?.union));
                }
                tbPreviewVillage.Text = dto.address?.villageHouseRoadNo;

                // Attachment
                if (dto.attachment?.complaintList?.Count > 0)
                {
                    for (int i = 0; i < dto.attachment?.complaintList?.Count; i++)
                    {
                        dgvPreviewComplain.Rows.Add("Complain-" + (i + 1), "View", "Delete");
                    }
                }

                if (dto.attachment?.seizureList?.Count > 0)
                {
                    for (int i = 0; i < dto.attachment?.seizureList?.Count; i++)
                    {
                        dgvPreviewSeizure.Rows.Add("Seizure-" + (i + 1), "View", "Delete");
                    }
                }

                if (dto.attachment?.firList?.Count > 0)
                {
                    tbPreviewFIRno.Text = dto.attachment?.firList[0].firNo;

                    try
                    {
                        var formatStrings = new string[] { "yyyy-MM-dd", "yyyy-MM-ddTHH:mm:ss.fffZ", "yyyy-MM-ddTHH:mm:ss.fffK", "dd-MM-yyyy" };
                        DateTime dtValue = new DateTime();
                        DateTime.TryParseExact(dto.attachment?.firList[0].firDate, formatStrings, CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out dtValue);
                        tbPreviewFIRdate.Text = dtValue.ToString("dd-MM-yyyy");
                    }
                    catch { }

                    try
                    {
                        if (dto.attachment?.firList[0].district > 0)
                        {
                            tbPreviewFIRdistrict.Text = GetValueForLookups("name_in_english", "district", Convert.ToInt32(dto.attachment?.firList[0].district));
                        }
                        if (dto.attachment?.firList[0].upozilaOrThana > 0)
                        {
                            tbPreviewFIRthana.Text = GetValueForLookups("name_in_english", "upazila", Convert.ToInt32(dto.attachment?.firList[0].upozilaOrThana));
                        }
                    }
                    catch { }

                    for (int i = 0; i < dto.attachment?.firList?.Count; i++)
                    {
                        dgvPreviewFIR.Rows.Add("FIR/Warrant-" + (i + 1), "View", "Delete");
                    }
                }
                tbPreviewWarrantNo.Text = dto.warrantNo;
                tbWarrantIssueDate.Text = dto.warrantIssueDate;

                ShowBiometricInfo(dto);
            }
            catch { }
        }

        private void ShowBiometricInfo(NotEntryDto dto)
        {
            try
            {
                this.pbPreviewPhoto.Image = Utils.ByteToImage(dto.biometric?.photo?.photo);

                this.pbPreviewRightIris.Image = Utils.ByteToImage(dto.biometric?.iris?.right);
                this.pbPreviewLeftIris.Image = Utils.ByteToImage(dto.biometric?.iris?.left);

                this.pbPreviewRT.Image = AppUtils.AppUtils.WsqToImage(dto.biometric?.fingerprint?.rt);
                this.pbPreviewRI.Image = AppUtils.AppUtils.WsqToImage(dto.biometric?.fingerprint?.ri);
                this.pbPreviewRM.Image = AppUtils.AppUtils.WsqToImage(dto.biometric?.fingerprint?.rm);
                this.pbPreviewRR.Image = AppUtils.AppUtils.WsqToImage(dto.biometric?.fingerprint?.rr);
                this.pbPreviewRL.Image = AppUtils.AppUtils.WsqToImage(dto.biometric?.fingerprint?.rs);

                this.pbPreviewLT.Image = AppUtils.AppUtils.WsqToImage(dto.biometric?.fingerprint?.lt);
                this.pbPreviewLI.Image = AppUtils.AppUtils.WsqToImage(dto.biometric?.fingerprint?.li);
                this.pbPreviewLM.Image = AppUtils.AppUtils.WsqToImage(dto.biometric?.fingerprint?.lm);
                this.pbPreviewLR.Image = AppUtils.AppUtils.WsqToImage(dto.biometric?.fingerprint?.lr);
                this.pbPreviewLL.Image = AppUtils.AppUtils.WsqToImage(dto.biometric?.fingerprint?.ls);
            }
            catch (Exception ex)
            {

            }
        }

        public string GetValueForLookups(string columnName, string tableName, int id)
        {
            string value = dblookupManager.GetValueForLookup(columnName, tableName, id);
            return value;
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
        }        

        private void iconBtnMinimize_Click(object sender, EventArgs e)
        {

        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            StaticData.NotEntry = new NotEntryDto();
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
