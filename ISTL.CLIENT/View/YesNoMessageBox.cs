using ISTL.COMMON;
using NLog;
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
    public partial class YesNoMessageBox : ViewForm
    {
        // Define the CS_DROPSHADOW constant
        protected Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private const int CS_DROPSHADOW = 0x00020000;

        /// <summary>
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomMessageBox"/> class.
        /// </summary>
        public YesNoMessageBox()
        {
            InitializeComponent();
        }

        public static DialogResult YesNoMessageBoxResult(string caption, string message)
        {
            YesNoMessageBox messageBox = new YesNoMessageBox();
            InitAll(messageBox);
            messageBox.Text = caption;
            messageBox.lblTitle.Text = caption;
            messageBox.btnYes.Visible = true;
            messageBox.btnNo.Visible = true;
            messageBox.lblMessage.Text = message;
            return messageBox.ShowDialog();
        }

        public static void InitAll(YesNoMessageBox messageBox)
        {
            messageBox.btnYes.Visible = false;
            messageBox.btnNo.Visible = false;
        }

        public static void ShowMessage(string caption, string message)
        {
            YesNoMessageBox messageBox = new YesNoMessageBox();
            InitAll(messageBox);
            messageBox.Text = caption;
            messageBox.lblTitle.Text = caption;
            messageBox.btnYes.Visible = true;
            messageBox.btnNo.Visible = true;
            messageBox.lblMessage.Text = message;
            messageBox.ShowDialog();
            messageBox.Dispose();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
    }
}
