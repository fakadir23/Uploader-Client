using ISTL.COMMON;
using ISTL.PERSOGlobals;
using ISTL.RAB.Controllers;
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
    public partial class ExportDbForm : ViewForm
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

        Logger logger = LogManager.GetCurrentClassLogger();

        public ExportDbForm()
        {
            InitializeComponent();
        }

        public void SetProgress(int total, int done)
        {
            int progress = ((done * 100) / total);
            backgroundWorker.ReportProgress(progress);
        }

        private void SelectFolder()
        {
            DialogResult result = this.folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.tbSelectedPath.Text = this.folderBrowserDialog.SelectedPath + "\\" + Globals.Database.EXTERNAL_NAME;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            SelectFolder();
        }

        private void btnExportDb_Click(object sender, EventArgs e)
        {
            string exportPath = this.tbSelectedPath.Text;
            if (exportPath == null || exportPath.Length == 0)
            {
                //MessageBox.Show("Please provide a path to start export to.", "RAB CDMS");
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please provide a path to start export to.");
                return;
            }

            ShowProcessing(true);
            backgroundWorker.RunWorkerAsync(exportPath);
        }

        /// <summary>
        /// Show a visual indicator on the form that background work is
        /// in progress
        /// </summary>
        /// <param name="show">
        /// TRUE: show
        /// FALSE: hide
        /// </param>
        private void ShowProcessing(bool show)
        {
            if (show)
            {
                this.btnExportDb.Enabled = false;
                this.btnBrowse.Enabled = false;
                this.picProcessing.Visible = true;
                this.progressBar.Value = 0;
                this.lblProgress.Text = "0%";
            }
            else
            {
                this.picProcessing.Visible = false;
                this.btnBrowse.Enabled = true;
                this.btnExportDb.Enabled = true;
                this.btnCancel.Enabled = true; // In case it was disabled when cancel was requested during transfer
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.btnCancel.Enabled = false;
            if (backgroundWorker.IsBusy)
            {
                // If transfer is in progress, stop transfer
                backgroundWorker.CancelAsync();
            }
            else
            {
                // If no transfer in progress, close form
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            string exportPath = (string)e.Argument;
            ((ExportDbController)controller).ExportDb(exportPath, (BackgroundWorker)sender);
        }

        private void backgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;
            if (progress < this.progressBar.Maximum)
            {
                this.progressBar.Value = progress;
                this.lblProgress.Text = progress + "%";
            }
            else
            {
                this.progressBar.Value = this.progressBar.Maximum;
                this.lblProgress.Text = this.progressBar.Maximum + "%";
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // There was an unhandled error during background operation
                logger.Error("There was an unexpected error during background operation.\n" + e.Error);
                ErrorMessageBox.ShowError("There was an unexpected error when trying to export db.", e.Error);
            }
            ShowProcessing(false);
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            btnCancel_Click(null, null);
        }
    }
}
