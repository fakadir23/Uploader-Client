using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ISTL.COMMON;
using ISTL.RAB.Controllers;
using ISTL.RAB.Entity;
using ISTL.COMMON.Autoupdate;
using ISTL.PERSOGlobals;
using NLog;
using System.Threading;
using System.Configuration;
using ISTL.RAB.DbManager;

namespace ISTL.RAB.View
{
    public partial class DefaultUserControl : ViewUserControl
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        public DbLookupManager dblookupManager;
        public DefaultUserControl()
        {
            InitializeComponent();
            dblookupManager = new DbLookupManager();
        }

        public string FIRuploadPending
        {
            get { return lblFIRUploadPendingCount.Text; }
            set { lblFIRUploadPendingCount.Text = value; }
        }
        public string EnrolledWithBioCount
        {
            get { return lblEnrolledWithBiometricCount.Text; }
            set { lblEnrolledWithBiometricCount.Text = value; }
        }

        public string GetValueForLookup(string columnName, string tableName, int id)
        {
            string value = dblookupManager.GetValueForLookup(columnName, tableName, id);
            return value;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            if (Users.Unit >= 0)
            {
                lblUserUnitSubUnit.Text = GetValueForLookup("name_en", "station", Users.Unit);
            }
            if (Users.SubUnit > 0)
            {
                lblUserUnitSubUnit.Text += " " + GetValueForLookup("name_en", "sub_station", Users.SubUnit);
            }

            lblFIRUploadPendingCount.Text = "N/A";
            lblEnrolledWithBiometricCount.Text = "N/A";

            ShowDashboardSummary();
        }

        public void ShowDashboardSummary()
        {
            ((DefaultController)controller).ShowDashboardCriminalSummary();
        }

        private void btnNewEntry_Click(object sender, EventArgs e)
        {
            ((DefaultController)controller).CriminalProfile();
        }

        private void btnDiagnose_Click(object sender, EventArgs e)
        {
            ((DefaultController)controller).Diagnose();
        }

        private void btnSearchCriminal_Click(object sender, EventArgs e)
        {
            StaticData.firPending = false;
            StaticData.dataWithBio = false;
            ((DefaultController)controller).SearchCriminal();
        }

        private void btnBiometricSearch_Click(object sender, EventArgs e)
        {
            ((DefaultController)controller).BiometricSearch();
        }

        private void iconBtnSetup_Click(object sender, EventArgs e)
        {
            ((DefaultController)controller).SpecialEntry();
        }

        private void iconBtnUserMgmt_Click(object sender, EventArgs e)
        {
            ((DefaultController)controller).UserManagement();
        }

        private void iconBtnReports_Click(object sender, EventArgs e)
        {
            ((DefaultController)controller).Reports();
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void btnFIRpendingList_Click(object sender, EventArgs e)
        {
            StaticData.firPending = true;
            StaticData.dataWithBio = false;
            ((DefaultController)controller).SearchCriminal();
        }

        private void btnEnrollBioList_Click(object sender, EventArgs e)
        {
            StaticData.firPending = false;
            StaticData.dataWithBio = true;
            ((DefaultController)controller).SearchCriminal();
        }

        private void btnSyncDashboard_Click(object sender, EventArgs e)
        {
            StaticData.StaticEnrolledWithBiometicCount = -1;
            StaticData.StaticFIRUploadPendingCount = -1;
            FIRuploadPending = "N/A";
            EnrolledWithBioCount = "N/A";
            ((DefaultController)controller).SyncDashboard();
        }

        private void btnUpdateCheck_Click(object sender, EventArgs e)
        {
            bool flag = false;
            bool isOffline = false;
            ProcessingDialog.Run(delegate ()
            {
                try
                {
                    string updateUrl = "";
                    if (ConfigurationManager.AppSettings["build.profile.active"].ToString() == "dev")
                    {
                        updateUrl = System.Configuration.ConfigurationManager.AppSettings["UpdateUrlDev"];
                    }
                    else
                    {
                        updateUrl = System.Configuration.ConfigurationManager.AppSettings["UpdateUrlProd"];
                    }

                    AutoUpdate updater = new AutoUpdate(Globals.Assembly.EXE_RAB_CDMS);
                    if (updater.UpdateAvailable(updateUrl))
                    {
                        logger.Debug("Manual Update Check :: Update found.");
                        flag = true;
                    }
                    else
                    {
                        logger.Debug("Manual Update Check :: Update not found.");
                        flag = false;
                    }
                }
                catch (System.Net.WebException x)
                {
                    // Network Error.
                    logger.Debug("Known error, when server not found.\n" + x.Message);
                    ISTL.RAB.View.CustomMessageBox.ShowMessage("RAB CDMS Error","Seems you are offline during update check. Please contact with your Administrator.");
                    isOffline = true;
                }
                catch (TimeoutException x)
                {
                    logger.Debug("Connection timedout.\n" + x.Message);
                }
                catch (System.Exception x)
                {
                    logger.Error("There was an unexpected error when checking for updates.\n" + x.ToString());
                    ISTL.RAB.View.ErrorMessageBox.ShowError("There was an unexpected error when checking for updates.", x);
                }
            });

            if (!isOffline)
            {
                if (flag)
                {
                    //MessageBox.Show("New update is found. Please restart the application" +
                    //" to start using the new version. You will need to be online " +
                    //"for the update to work.", "Update Available");
                    InfoMessageBox.ShowMessage("SNSOP TOOLS", "Update Available\n\nNew update is found. Please restart the application and be online to get the new version");
                }
                else
                {
                    //MessageBox.Show("New update was not found.", "Update Not Available");
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Update Not Available");
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportDbController exportDbController = new ExportDbController();
            exportDbController.ShowView();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            ImportDbController importDbController = new ImportDbController();
            importDbController.ShowView();
        }

        private void btnNotEntry_Click(object sender, EventArgs e)
        {            
            ((DefaultController)controller).NotEntry();
		}

        private void btnProfileManagement_Click(object sender, EventArgs e)
        {
            ((DefaultController)controller).ProfileManagement();
        }
    }
}
