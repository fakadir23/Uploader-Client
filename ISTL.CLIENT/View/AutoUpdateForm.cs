using System;
using System.Windows.Forms;
using ISTL.COMMON.Autoupdate;

namespace ISTL.RAB.View
{
    public partial class AutoUpdateForm : Form, IAutoUpdateView
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

        private AutoUpdate.DownloadCancelMethod callback;

        public AutoUpdateForm()
        {
            InitializeComponent();
        }

        #region IAutoUpdateView Members

        public void ShowView(AutoUpdate.DownloadCancelMethod cancelDownload)
        {
            callback = cancelDownload;
            this.ShowDialog();
            this.Dispose();
        }

        public void HideView()
        {
            this.DialogResult = DialogResult.OK;
        }

        public void ShowProgress(int percent, long bytesDownloaded, long totalBytes)
        {
            this.progressBar.Value = percent;
            this.lblProgress.Text = percent + "%";
        }

        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            callback();
        }
    }
}
