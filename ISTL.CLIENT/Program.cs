using System;
using System.Windows.Forms;
using NLog;
using ISTL.COMMON;
using ISTL.COMMON.Autoupdate;
using ISTL.COMMON.Database;
using ISTL.COMMON.Subscription;
using ISTL.RAB.Controllers;
using System.Diagnostics;
using ISTL.RAB.Entity;
using ISTL.PERSOGlobals;
using System.Security.Principal;
using System.Globalization;
using System.Reflection;
using System.Resources;
using ISTL.RAB.View;
using ISTL.RAB.DbManager;
using System.Threading.Tasks;

namespace ISTL.RAB
{
    static class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();

#if (!DEBUG)
            RunAsAdmin();
#endif
            if (PriorProcess() != null)
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Another instance of the app is already running.");
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ISTL.RAB.View.ProcessingDialog.Run(StartupProcessing);

            try
            {
                while (true)
                {
                    LoginController loginController = new LoginController();
                    DialogResult dialogResult = loginController.ShowView();
                    if (dialogResult == DialogResult.Cancel) return;

                    MainController mainController = new MainController();
                    DialogResult dialogResultMain = mainController.ShowView();
                    if (dialogResultMain == DialogResult.Cancel) return;
                }
            }
            finally
            {
                ISTL.RAB.View.ProcessingDialog.Run("Closing the application.", null, ISTL.COMMON.Threads.ThreadHandler.shutdown);
                //Task.Run(() => StopAllRunningThreads());
            }
        }

        private static void StopAllRunningThreads()
        {
            ISTL.COMMON.Threads.ThreadHandler.shutdown();
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        private static bool IsRunAsAdministrator()
        {
            var wi = WindowsIdentity.GetCurrent();
            var wp = new WindowsPrincipal(wi);

            return wp.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private static void RunAsAdmin()
        {
            logger.Debug("Enter :: RunAsAdmin");

            bool isAdminFlag = IsRunAsAdministrator();

            logger.Debug("Is Admin Flag :: " + isAdminFlag);

            if (!isAdminFlag)
            {
                ProcessStartInfo proc = new ProcessStartInfo();
                proc.UseShellExecute = true;
                proc.WorkingDirectory = Environment.CurrentDirectory;
                proc.FileName = Application.ExecutablePath;
                proc.Verb = "runas";

                try
                {
                    //Application.Exit();
                    Process.Start(proc);
                }
                catch(Exception ex)
                {
                    // The user refused the elevation.
                    // Do nothing and return directly ...
                    //return;

                    logger.Error("There was an unexpected error when starting the app RunAsAdmin. Error Message:\n", ex.ToString());

                    Environment.Exit(1);
                }
                Environment.Exit(1);
            }
        }

        private static void StartupProcessing()
        {
            
            // Get assembly versions
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            Globals.Assembly.EXE_RAB_CDMS = String.Format("{0}.{1}.{2}", version.Major, version.Minor, version.Revision);

            // Initialize Logging
            Utils.SetupLogging(Globals.Logging.FILE, Globals.Logging.DEFAULT_LEVEL);

            // Write active build profile in the log file
            var buildProfile = System.Configuration.ConfigurationManager.AppSettings["build.profile.active"];
            logger.Info("Build Profile Active: " + buildProfile);

            // Check for application update
            AutoUpdate updater = new AutoUpdate(Globals.Assembly.EXE_RAB_CDMS);
            bool updateFound = false;

            string updateUrl = "";
            if (System.Configuration.ConfigurationManager.AppSettings["build.profile.active"].ToString() == "dev")
            {
                updateUrl = System.Configuration.ConfigurationManager.AppSettings["UpdateUrlDev"];
            }
            else
            {
                updateUrl = System.Configuration.ConfigurationManager.AppSettings["UpdateUrlProd"];
            }

            try
            {
                //updateFound = update.UpdateAvailable(Globals.Assembly.UPDATE_URL);
                updateFound = updater.UpdateAvailable(updateUrl);
            }
            catch (System.Net.WebException x)
            {
                // Network Error.
                logger.Debug("Known error, when server not found.\n" + x.Message);
            }
            catch (TimeoutException x)
            {
                logger.Debug("Connection timedout.\n" + x.Message);
            }
            catch (Exception x)
            {
                logger.Error("There was an error checking for application update.\n" + x.ToString());
                ISTL.RAB.View.ErrorMessageBox.ShowError("There was an error checking for application update.", x);
            }
            if (updateFound)
            {
                // Will show modal form and if download successful, application will exit
                // and start the setup process
                updater.Update(new ISTL.RAB.View.AutoUpdateForm());
            }

            // Create database and initial table(s)
            DbCreateTables dbCreateTables = new DbCreateTables(Globals.Database.NAME, Globals.Database.PASSWORD);
            dbCreateTables.CreateDbTables();

            // Migrate database
            try
            {
                string dbNameMig = Utils.GetAssemblyPath() +
                    "\\" + Globals.Database.NAME_MIG;
                DbMigration dbM = new DbMigration(Globals.Database.NAME,
                    Globals.Database.PASSWORD,
                    dbNameMig,
                    Globals.Database.PASSWORD_MIG);
                bool flag = dbM.MigrateDb();
                if (!flag)
                {
                    //MessageBox.Show("You are running an old version of the application.\n Please install the latest version and try again.",
                    //    "RAB CDMS",
                    //    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "You are running an old version of the application.\n Please install the latest version and try again.");
                    logger.Error("The version of rab_cdms.db is higher than migscript.db, probably because the application was downgraded to an older version.");
                    Environment.Exit(0);
                }
            }
            catch (Exception x)
            {
                logger.Error("There was an error updating the database.\n" + x.ToString());
                ErrorMessageBox.ShowError("There was an error updating the database.", x);
            }

            // Fetch device models
            // DbDeviceManager dbDeviceManager = new DbDeviceManager();

            // Configuration.Cam = new MODELS.DTO.Device.DeviceDto();
            // Configuration.FP = new MODELS.DTO.Device.DeviceDto();
            // Configuration.Iris = new MODELS.DTO.Device.DeviceDto();

            // Configuration.Cam.Name = Globals.Device.Cam.NAME;
            // Configuration.FP.Name = Globals.Device.FP.NAME;
            // Configuration.Iris.Name = Globals.Device.Iris.NAME;

            // var camD = dbDeviceManager.GetDevice(Globals.DeviceCategory.CAM);
            // var fpD = dbDeviceManager.GetDevice(Globals.DeviceCategory.FP);
            // var irisD = dbDeviceManager.GetDevice(Globals.DeviceCategory.IRIS);

            // if (camD != null) Configuration.Cam = camD;
            // if (fpD != null) Configuration.FP = fpD;
            // if (irisD != null) Configuration.Iris = irisD;

            // Initialize all notifying subjects
            SubjectFactory.GetInstance().Add(OnlineSubject.Name, new OnlineSubject());
            SubjectFactory.GetInstance().Add(UploadSubject.Name, new UploadSubject());
            SubjectFactory.GetInstance().Add(Globals.Common.COUNTER_NAME, new CounterSubject());
            SubjectFactory.GetInstance().Add(Globals.Common.COUNTER_PENDING_NAME, new CounterPendingSubject());
            SubjectFactory.GetInstance().Add(Globals.Common.COUNTER_ERROR_NAME, new CounterErrorSubject());
            SubjectFactory.GetInstance().Add(Globals.Common.COUNTER_DRAFT_NAME, new CounterDraftSubject());
            SubjectFactory.GetInstance().Add(UpdateSubject.Name, new UpdateSubject());


            SubjectFactory.GetInstance().Add(Globals.DeviceSubject.CAM, new DeviceSubject(Globals.DeviceSubject.CAM));
            SubjectFactory.GetInstance().Add(Globals.DeviceSubject.FP, new DeviceSubject(Globals.DeviceSubject.FP));
            SubjectFactory.GetInstance().Add(Globals.DeviceSubject.IRIS, new DeviceSubject(Globals.DeviceSubject.IRIS));

            SubjectFactory.GetInstance().Add(NidSearchSubject.Name, new NidSearchSubject());
        }

        /// <summary>
        /// Priors the process.
        /// </summary>
        /// <returns></returns>
        private static Process PriorProcess()
        // Returns a System.Diagnostics.Process pointing to
        // a pre-existing process with the same name as the
        // current one, if any; or null if the current process
        // is unique.
        {
            Process curr = Process.GetCurrentProcess();
            Process[] procs = Process.GetProcessesByName(curr.ProcessName);
            foreach (Process p in procs)
            {
                if ((p.Id != curr.Id) &&
                    (p.MainModule.FileName == curr.MainModule.FileName))
                    return p;
            }
            return null;
        }
    }
}
