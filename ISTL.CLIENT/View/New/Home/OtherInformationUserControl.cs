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
using ISTL.RAB.Controllers;
using ISTL.RAB.Controllers.New.Enrollment;
using ISTL.RAB.Entity;
using ISTL.MODELS.DTO.New.Enrollment;
using System.Web.Script.Serialization;
using ISTL.RAB.DbManager;
using ISTL.COMMON.Database;
using ISTL.PERSOGlobals;

namespace ISTL.RAB.View.New.FamilyAlliesFoes
{
    public partial class OtherInformationUserControl : ViewUserControl
    {
        private DbEnrollClientManager dbEnrollClientManager;
        private DbOperation dbOperation = null;
        public OtherInformationUserControl()
        {
            InitializeComponent();
            dbEnrollClientManager = new DbEnrollClientManager();
            dbOperation = new DbOperation(Globals.Database.NAME, Globals.Database.PASSWORD);
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            tbTitleName.Focus();

            if (StaticData.Enrollment.profile.otherInformationList != null)
            {
                ShowStaticOtherInfo();
            }            
        }

        private void ShowStaticOtherInfo()
        {
            if (StaticData.Enrollment.profile.otherInformationList.Count > 0)
            {
                for (int i=0; i<StaticData.Enrollment.profile.otherInformationList.Count; i++)
                {
                    dgvOtherInfo.Rows.Add(StaticData.Enrollment.profile.otherInformationList[i].key,
                        StaticData.Enrollment.profile.otherInformationList[i].value);
                }
            }
        }

        public override void OnClosing()
        {
            base.OnClosing();
            if (!(btnBiometric.ContainsFocus || btnPreviewSubmit.ContainsFocus || btnCriminalProfile.ContainsFocus) || btnOtherInfo.ContainsFocus)
            {
                StaticData.Enrollment.profile = new ProfileDto();
            }
            //else
            //{
            //    OnSaveOtherInformation();
            //}
        }

        private void OnSaveOtherInformation()
        {
            if (dgvOtherInfo.Rows.Count == 0)
            {
                StaticData.Enrollment.profile.otherInformationList.Clear();
                string sqlUpd = "UPDATE criminal_profile SET other_information=NULL WHERE reference_no='" + StaticData.Enrollment?.profile?.referenceNo + "';";
                dbOperation.OpenDbConnection();
                dbOperation.ExecuteQuery(sqlUpd);
                dbOperation.CloseDbConnection();
                return;
            }

            List<OtherInfoDto> otherInfoDtos = new List<OtherInfoDto>();
            for (int i=0; i<dgvOtherInfo.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dgvOtherInfo.Rows[i].Cells[0]?.Value?.ToString()))
                {
                    OtherInfoDto dto = new OtherInfoDto();
                    dto.key = dgvOtherInfo.Rows[i].Cells[0]?.Value?.ToString();
                    dto.value = dgvOtherInfo.Rows[i].Cells[1]?.Value?.ToString();
                    otherInfoDtos.Add(dto);
                }
            }
            StaticData.Enrollment.profile.otherInformationList = otherInfoDtos;
            if (!string.IsNullOrEmpty(StaticData.Enrollment.profile.referenceNo))
            {
                var json = new JavaScriptSerializer().Serialize(otherInfoDtos);
                dbEnrollClientManager.UpdateProfileValues(StaticData.Enrollment.profile.referenceNo, "other_information", -1, json);
            }            
        }

        private void btnEditFamily_Click(object sender, EventArgs e)
        {
            if (dgvOtherInfo.RowCount > 0)
            {
                int rowIndex = dgvOtherInfo.CurrentRow.Index;
                if (StaticData.ModifiableNormalEnrollment == false)
                {
                    if (rowIndex > (StaticData.Enrollment.profile?.otherInformationList?.Count - 1))
                    {
                        tbTitleName.Text = dgvOtherInfo.Rows[rowIndex].Cells[0]?.Value?.ToString();
                        tbTitleValue.Text = dgvOtherInfo.Rows[rowIndex].Cells[1]?.Value?.ToString();
                        dgvOtherInfo.Rows.RemoveAt(rowIndex);
                    }
                }
                else
                {
                    tbTitleName.Text = dgvOtherInfo.Rows[rowIndex].Cells[0]?.Value?.ToString();
                    tbTitleValue.Text = dgvOtherInfo.Rows[rowIndex].Cells[1]?.Value?.ToString();
                    dgvOtherInfo.Rows.RemoveAt(rowIndex);
                }

                tbTitleValue.Focus();
            }
        }

        private void btnRemoveFamily_Click(object sender, EventArgs e)
        {
            if (dgvOtherInfo.RowCount > 0)
            {
                int rowIndex = dgvOtherInfo.CurrentRow.Index;
                if (StaticData.ModifiableNormalEnrollment == false)
                {
                    if (rowIndex > (StaticData.Enrollment.profile?.otherInformationList?.Count - 1))
                    {
                        dgvOtherInfo.Rows.RemoveAt(rowIndex);
                    }
                }
                else
                {
                    dgvOtherInfo.Rows.RemoveAt(rowIndex);
                }
                OnSaveOtherInformation();
            }
        }

        private void btnAddFamily_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbTitleName.Text) && !string.IsNullOrEmpty(tbTitleValue.Text))
            {
                dgvOtherInfo.Rows.Add(tbTitleName.Text, tbTitleValue.Text);

                tbTitleName.Text = null;
                tbTitleValue.Text = null;
                tbTitleName.Focus();

                OnSaveOtherInformation();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

        }

        private void btnCriminalProfile_Click(object sender, EventArgs e)
        {
            ((OtherInformationController)controller).CriminalProfile();
        }

        private void btnFamily_Click(object sender, EventArgs e)
        {
            ((OtherInformationController)controller).Family();
        }

        private void btnBiometric_Click(object sender, EventArgs e)
        {
            ((OtherInformationController)controller).Biometric();
        }

        private void btnPreviewSubmit_Click(object sender, EventArgs e)
        {
            ((OtherInformationController)controller).PreviewSubmit();
        }

        private void tbTitleName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) tbTitleValue.Focus();
        }

        private void tbTitleValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnAdd.Focus();
        }
    }
}
