using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;
using ISTL.RAB.Controllers;
using ISTL.PERSOGlobals;

namespace ISTL.RAB.View
{
    public partial class CustomMessageBox : Form
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
        public CustomMessageBox()
        {
            InitializeComponent();
        }

        public static void InitAll(CustomMessageBox messageBox)
        {
            messageBox.btnOk.Visible = false;
        }
        
        public static void ShowMessage(string message)
        {
            CustomMessageBox messageBox = new CustomMessageBox();
            InitAll(messageBox);
            messageBox.btnOk.Visible = true;
            messageBox.lblMessage.Text = message;
            messageBox.ShowDialog();
            messageBox.Dispose();
        }

        public static void ShowMessage(string caption, string message)
        {
            CustomMessageBox messageBox = new CustomMessageBox();
            InitAll(messageBox);
            messageBox.Text = caption;
            messageBox.lblTitle.Text = caption;
            messageBox.btnOk.Visible = true;
            messageBox.lblMessage.Text = message;
            messageBox.ShowDialog();
            messageBox.Dispose();
        }
        
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }        
    }
}
