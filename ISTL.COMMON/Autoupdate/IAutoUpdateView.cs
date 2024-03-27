using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISTL.COMMON.Autoupdate
{
    public interface IAutoUpdateView
    {
        /// <summary>
        /// The view may be a form or a control or even a stub that doesn't show anything.
        /// </summary>
        /// <param name="cancelDownload">This delegate is provided to allow cancellation
        /// of the download process. Cancellation is an asynchronous process. Do not hide 
        /// the view after calling cancel, because AutoUpdate will do that for you.</param>
        void ShowView(AutoUpdate.DownloadCancelMethod cancelDownload);

        /// <summary>
        /// The view may be a form or a control or even a stub that doesn't do anything.
        /// </summary>
        void HideView();

        /// <summary>
        /// This can be used to show download progress with UI elements.
        /// </summary>
        /// <param name="percent"></param>
        /// <param name="bytesDownloaded"></param>
        /// <param name="totalBytes"></param>
        void ShowProgress(int percent, long bytesDownloaded, long totalBytes);
    }
}
