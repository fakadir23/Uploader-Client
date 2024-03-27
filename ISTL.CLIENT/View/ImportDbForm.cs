using ISTL.COMMON;
using ISTL.PERSOGlobals;
using ISTL.RAB.Controllers;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISTL.RAB.View
{
    public partial class ImportDbForm : ViewForm
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

        public ImportDbForm()
        {
            InitializeComponent();

            //this.openFileDialog.FileName = Globals.Database.EXTERNAL_NAME;
            //this.openFileDialog.Filter = String.Format("Data File|{0}", Globals.Database.EXTERNAL_NAME);
            openFileDialog.Filter = "DB Files(*.db;)|" +
                    "*.db;";
        }

        public void SetProgress(int total, int done)
        {
            int progress = ((done * 100) / total);
            backgroundWorker.ReportProgress(progress);
        }

        private void SelectFile()
        {
            DialogResult result = this.openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.tbSelectedPath.Text = this.openFileDialog.FileName;
            }
        }

        private void btnImportBrowse_Click(object sender, EventArgs e)
        {
            SelectFile();
        }

        private void btnStartImportDb_Click(object sender, EventArgs e)
        {
            string importPath = this.tbSelectedPath.Text;
            if (importPath == null || importPath.Length == 0)
            {
                //MessageBox.Show("Please provide a path to start import from.", "RAB CDMS");
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please provide a path to start import from.");
                return;
            }

            if (File.Exists(importPath) != true)
            {
                //MessageBox.Show("Please provide a valid database file.", "RAB CDMS");
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please provide a valid database file.");
                return;
            }

            ShowProcessing(true);
            backgroundWorker.RunWorkerAsync(importPath);
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
                this.btnStartImportDb.Enabled = false;
                this.btnImportBrowse.Enabled = false;
                this.picProcessing.Visible = true;
                this.progressBar.Value = 0;
                this.lblProgress.Text = "0%";
            }
            else
            {
                this.picProcessing.Visible = false;
                this.btnImportBrowse.Enabled = true;
                this.btnStartImportDb.Enabled = true;
                this.btnImportCancel.Enabled = true; // In case it was disabled when cancel was requested during transfer
            }
        }

        private void btnImportCancel_Click(object sender, EventArgs e)
        {
            this.btnImportCancel.Enabled = false;
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
            string importPath = (string)e.Argument;
            ((ImportDbController)controller).ImportDb(importPath, (BackgroundWorker)sender);
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
                ErrorMessageBox.ShowError("There was an unexpected error when trying to import db.", e.Error);
            }
            ShowProcessing(false);
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            btnImportCancel_Click(null, null);
        }
    }
}
