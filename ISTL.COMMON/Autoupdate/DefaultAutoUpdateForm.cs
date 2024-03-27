using System;
using System.Windows.Forms;
using ISTL.COMMON.Autoupdate;

namespace ISTL.COMMON.Autoupdate
{
    public partial class DefaultAutoUpdateForm : Form, IAutoUpdateView
    {
        private AutoUpdate.DownloadCancelMethod callback;

        public DefaultAutoUpdateForm()
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
        }

        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            callback();
        }
    }
}
