using ISTL.RAB.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISTL.RAB.View.New.CriminalProfile
{
    public partial class AddNickNameDialogForm : Form
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
        public List<string> nickNameList = new List<string>();
        public AddNickNameDialogForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            tbNickName.Focus();

            if (StaticData.Enrollment?.profile?.nickName?.Count > 0)
            {
                for (int i= 0; i < StaticData.Enrollment?.profile?.nickName?.Count; i++)
                {
                    dataGridView1.Rows.Add(StaticData.Enrollment?.profile?.nickName[i]);
                }
            }
        }

        private void tbBankName_TextChanged(object sender, EventArgs e)
        {

        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void iconBtnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void iconBtnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbNickName.Text))
            {
                dataGridView1.Rows.Add(tbNickName.Text);
                tbNickName.Text = null;
            }
            else
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Type a nickname");
            }
        }

        private void iconBtnSave_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dataGridView1.Rows[i].Cells[0]?.Value?.ToString()))
                    {
                        nickNameList.Add(dataGridView1.Rows[i].Cells[0]?.Value?.ToString());
                    }
                }
            }
            this.DialogResult = DialogResult.OK;
        }

        private void iconBtnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                string value = dataGridView1.CurrentCell.Value?.ToString();
                int rowIndex = dataGridView1.CurrentRow.Index;
                dataGridView1.Rows.RemoveAt(rowIndex);
                tbNickName.Text = value;
            }
        }

        private void iconBtnRemove_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                int rowIndex = dataGridView1.CurrentRow.Index;
                dataGridView1.Rows.RemoveAt(rowIndex);
            }
        }
    }
}
