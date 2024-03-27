using ISTL.MODELS.Request.User;
using ISTL.RAB.ApiManager;
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
using System.Windows.Forms;

namespace ISTL.RAB.View.New.Home
{
    public partial class UserActivationForm : Form
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

        private Logger logger = LogManager.GetCurrentClassLogger();
        private UserApiManager userApiManager;
        public UserActivationRequest request;
        public UserActivationForm()
        {
            Font = new Font(Font.Name, 8.25f * 96f / CreateGraphics().DpiX, Font.Style, Font.Unit, Font.GdiCharSet, Font.GdiVerticalFont);
            InitializeComponent();
            userApiManager = new UserApiManager();
        }

        public bool Validatedata()
        {
            string errorMessage = "";
            
            if (string.IsNullOrWhiteSpace(tbNewPassword.Text))
            {
                errorMessage += "New Password is required." + "\n";
            }

            if (string.IsNullOrWhiteSpace(tbConfirmPassword.Text))
            {
                errorMessage += "Confirm Password is required." + "\n";
            }

            if (!string.IsNullOrWhiteSpace(tbNewPassword.Text)
                && !string.IsNullOrWhiteSpace(tbConfirmPassword.Text)
                && !string.Equals(tbNewPassword.Text.Trim(), tbConfirmPassword.Text.Trim(),
                StringComparison.CurrentCulture))
            {
                errorMessage += "New Password and Confirm Password is mismatched." + "\n";
            }
            
            if (errorMessage != "")
            {
                //MessageBox.Show("Please correct the following error(s):\n\n" + errorMessage,
                //    "RAB CDMS Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please correct the following error(s):\n\n" + errorMessage);

                return false;
            }
            return true;
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            try
            {
                if (request == null)
                {
                    logger.Error("User activation request model is null.");
                    //MessageBox.Show("User activation request model is null.", "RAB CDMS Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "User activation request model is null.");
                    return;
                }

                if (!Validatedata())
                {
                    return;
                }

                // Set nwew password
                request.password = tbNewPassword.Text.Trim();

                var response = userApiManager.ActivateUser(request);

                if(response != null && response.code == (int)HttpResponseStatus.OK)
                {
                    logger.Error("User activation is success for Username: ." + request.username);
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    logger.Error("User activation is failed for Username: " + request.username +
                        "\nError Message: " + response.message);
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            catch(Exception ex)
            {
                logger.Error("There was an unexpected error during user activation.\n" + ex.ToString());
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            logger.Error("User activation is canceled for Username: " + request?.username);
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
