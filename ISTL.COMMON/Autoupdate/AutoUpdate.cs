using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;
using System.Xml.Linq;
using NLog;
using ISTL.COMMON.Common;

namespace ISTL.COMMON.Autoupdate
{
    /// <summary>
    /// A generic auto update implementation that is independent of the UI.
    /// It expects an XML file with the following format:
    /// 
    /// 
    /// <?xml version="1.0" encoding="utf-8"?>
    /// <application name="perso">
	///     <version>major.minor.build</version>
	///     <setup>name of setup msi</setup>
	///     <download>http://host/path/</download>
	///     <hash>SHA1 hash of setup msi</hash>
    /// </application>
    /// 
    /// 
    /// It is assumed that the download path does not include the version path. So the path 
    /// is assumed to be:
    /// http://host/path/major_minor_build/setupFile
    /// 
    /// Note that the dots in the version are converted to underscores.
    /// </summary>
    public class AutoUpdate
    {
        private string currentVersion;
        private string latestVersion;
        private string download;
        private string setup;
        private string hash;
        private string downloadedPath; // Path where setup file will be downloaded
        private IAutoUpdateView auView;
        private WebClient webClient;
        private Logger logger = LogManager.GetCurrentClassLogger();

        public delegate void DownloadCancelMethod();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="version">The running application version</param>
        public AutoUpdate(string version)
        {
            currentVersion = version;
        }

        /// <summary>
        /// Check for latest update. This method may throw exceptions.
        /// </summary>
        /// <param name="xmlUrl">The url where update.xml can be found</param>
        /// <returns>
        /// TRUE: Update available
        /// FALSE: No updates
        /// </returns>
        public bool UpdateAvailable(string path)
        {
            string xml;

            webClient = new WebClient();
            xml = webClient.DownloadString(path + "update.xml");
            XDocument doc = XDocument.Parse(xml);
            XElement app = doc.Root;

            latestVersion = app.Element("version").Value;
            download = path + "files/";
            setup = app.Element("setup").Value;
            hash = app.Element("hash").Value;

            Version cVer = new Version(currentVersion);
            Version lVer = new Version(latestVersion);
            if (lVer.CompareTo(cVer) > 0)
            {
                return true;
            }
            return false;
        }

        public void Update()
        {
            Update(new DefaultAutoUpdateForm());
        }

        public void Update(IAutoUpdateView view)
        {
            auView = view;
            // Url of the setup file to download
            string url = download + latestVersion.Replace('.', '_') + "/" + setup;
            downloadedPath = System.IO.Path.GetTempPath() + setup;

            webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadCompleted);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            webClient.DownloadFileAsync(new Uri(url), downloadedPath);

            view.ShowView(CancelDownload);
        }

        private void CancelDownload()
        {
            webClient.CancelAsync();
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            auView.ShowProgress(e.ProgressPercentage, e.BytesReceived, e.TotalBytesToReceive);
        }

        private void DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled)
                {
                    logger.Debug("Download cancelled by user.");
                    MessageBox.Show("You have cancelled the installation of a new update. It is recommended " +
                        "that you update to the latest version, because some features may not function " +
                        "correctly. Please restart the application as soon as possible to continue with " +
                        "the update process.", "Auto Update");
                    return;
                }
                else if (e.Error != null)
                {
                    logger.Error("There was an unexpected error when downloading app update.\n" + e.Error.ToString());
                    MessageBox.Show("There was an unexpected error when downloading an application update. Please " +
                        "contact your administrator", "Auto Update");
                    return;
                }

                // Verify file hash
                string downloadedFileHash = GenerateSecureHash.CreateSha1Hash(Utils.FileToByteArray(downloadedPath));
                if (hash != "" && downloadedFileHash != hash)
                {
                    MessageBox.Show("There was a problem downloading the application update. It is recommended " +
                        "that you update to the latest version, because some features may not function " +
                        "correctly. Please restart the application as soon as possible to continue with " +
                        "the update process.", "Auto Update");
                    return;
                }

                // Do not install if running in IDE
                if (Debugger.IsAttached) return;

                Install();
            }
            finally
            {
                // It is important that HideView be the last statement in this method
                // because if the view was a modal dialog, as soon as the view closes,
                // the thread may continue asynchronously before this method returns.
                auView.HideView();
            }

            Environment.Exit(0);
        }

        private void Install()
        {
            Process process = new Process();
            process.StartInfo.FileName = "msiexec.exe";
            // Install: /i
            // Unattended mode - progress bar only: /passive
            process.StartInfo.Arguments = String.Format("/i \"{0}\" /passive", downloadedPath);
            // For some reason, passive mode does not work if not executing from shell
            process.StartInfo.UseShellExecute = true;
            process.Start();
        }
    }
}
