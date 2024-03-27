using System;
using System.Windows.Forms;
using ISTL.COMMON;
using ISTL.PERSOGlobals;

namespace ISTL.RAB.View
{
    public partial class ErrorMessageBox : Form
    {
        // Drop shadow souce code, got from:
        // http://www.codeproject.com/Articles/19277/Let-Your-Form-Drop-a-Shadow

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

        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private string stackTrace;
        private bool isOffline = false;
        private bool isDisplayedWithWaitDialog = false;

        public ErrorMessageBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Try to send the error report in the background and only show error dialog
        /// when background send fails
        /// </summary>
        /// <param name="message"></param>
        /// <param name="x"></param>
        public static void ShowError(string message, Exception x)
        {
            ErrorMessageBox error = new ErrorMessageBox();
            error.lblMessage.Text = message;
            error.stackTrace = x.ToString();
            error.SendReport(true);
        }

        /// <summary>
        /// Show error dialog.
        /// Use this method when you are not trying to show error message from a background running thread.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="x"></param>
        public static void ShowErrorWithWaitDialog(string message, Exception x)
        {
            ErrorMessageBox error = new ErrorMessageBox();
            error.lblMessage.Text = message;
            error.stackTrace = x.ToString();
            error.isDisplayedWithWaitDialog = true;
            error.SendReport(false);
            error.Dispose();
        }

        /// <summary>
        /// Send error report to server along with log file
        /// </summary>
        /// <param name="isBackground">
        /// TRUE: Send report in background.
        /// FALSE: Show error and let user choose to send report.
        /// </param>
        private void SendReport(bool isBackground)
        {
            this.isOffline = false;
            if (isBackground)
            {
                if (!this.SendReport()) return;
            }
            else
            {
                ProcessingDialog.Run(delegate()
                {
                    if (!this.SendReport()) return;
                });
            }

            if (this.isOffline)
            {
                // Could not send error report
                if (isBackground || this.isDisplayedWithWaitDialog)
                {
                    // Show error message and allow user to choose to send error report
                    this.isDisplayedWithWaitDialog = false; // so that this form is not displayed again with ShowDialog()
                    //this.btnSendReport.Visible = true;
                    //this.btnIgnore.Visible = true;
                    this.btnSendReport.Visible = false;
                    this.btnIgnore.Visible = false;
                    this.lblMessage.Text = this.lblMessage.Text
                        + "\nPlease send this error report so that we can resolve the issue.";
                    this.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Could not send error report because you are offline.", "RAB CDMS");
                }
            }
            else
            {
                // The error report was sent successfully
                if (isBackground || this.isDisplayedWithWaitDialog)
                {
                    // Show actual error message
                    this.isDisplayedWithWaitDialog = false; // so that this form is not displayed again with ShowDialog()
                    this.btnOk.Visible = true;
                    this.lblMessage.Text = this.lblMessage.Text
                        + "\nSystem will send an error report to server for resolving the issue.";
                    this.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Thank you for sending this error report. We will try to solve this issue.", "RAB CDMS");
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        /// <summary>
        /// Send error report to server along with log file
        /// </summary>
        /// <returns>FALSE: Unknown exception</returns>
        private bool SendReport()
        {
            string loggingFileName = Utils.DefaultDataPath() + "\\" + Globals.Logging.FILE;
            byte[] logfile = null;
            try
            {
                logfile = Utils.FileToByteArray(loggingFileName);
            }
            catch (Exception x)
            {
                logger.Debug("There was an error getting log file, so it will not be sent with report. ERROR:" + x.Message);
            }

            try
            {
                //LoggingServiceManager logService = new LoggingServiceManager();
                //logService.UploadLogFile(logfile, stackTrace);
            }
            catch (System.Net.WebException x)
            {
                // Network Error. Try offline mode
                logger.Debug("Known error, when server not found.\n" + x.Message);
                this.isOffline = true;
            }
            catch (TimeoutException x)
            {
                logger.Debug("Connection timedout! Should attempt to login offline.\n" + x.Message);
                this.isOffline = true;
            }
          
            catch (Exception x)
            {
                logger.Error("Unexpected error.\n" + x.ToString());
                MessageBox.Show("Please contact your administrator. Something went wrong when sending this report.", "RAB CDMS");
                return false;
            }
            return true;
        }

        private void btnIgnore_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnSendReport_Click(object sender, EventArgs e)
        {
            this.SendReport(false);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
