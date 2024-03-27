using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISTL.RAB.View
{
    public partial class InfoMessageBox : Form
    {
        public InfoMessageBox()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        public static void ShowMessage(string caption, string message)
        {
            InfoMessageBox messageBox = new InfoMessageBox();
            messageBox.Text = caption;
            messageBox.lblTitle.Text = caption;
            messageBox.btnOk.Visible = true;
            messageBox.lblMessage.Text = message;
            messageBox.ShowDialog();
            messageBox.Dispose();
        }
    }
}
