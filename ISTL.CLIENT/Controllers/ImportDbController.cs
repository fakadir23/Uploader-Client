using ISTL.COMMON;
using ISTL.COMMON.Threads;
using ISTL.RAB.Asynch;
using ISTL.RAB.DbManager;
using ISTL.RAB.View;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISTL.RAB.Controllers
{
    public class ImportDbController : ViewController
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private ImportDbForm importdbForm = null;

        public ImportDbController()
        {
            importdbForm = new ImportDbForm();
            base.SetView((IView)importdbForm);
            importdbForm.SetController(this);
        }

        public void ImportDb(string importPath, System.ComponentModel.BackgroundWorker worker)
        {
            string returnMsg = "";
            long totalRows = 0;
            long numberOfRowsAffected = 0;

            logger.Debug("Stopping upload thread...");
            //ThreadHandler.GetInstance(new UploadEnrollmentAsynch()).StopThread();
            //ThreadHandler.GetInstance(new UploadThreadsAsync()).StopThread();
            try
            {
                DbTransfer dbTransfer = new DbTransfer();
                dbTransfer.setProgressCallback(importdbForm.SetProgress);
                returnMsg = dbTransfer.Transfer(true, importPath, worker);
                if (returnMsg == null)
                {
                    // DbTranser should show appropriate messagebox if there was an error
                    //MessageBox.Show("Import operation was cancelled.", "Processing Stopped");
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Processing Stopped\n\nImport operation was cancelled.");
                    return;
                }

                string[] splitMsg = returnMsg.Split('#');
                totalRows = Convert.ToInt64(splitMsg[0]);
                numberOfRowsAffected = Convert.ToInt64(splitMsg[1]);
            }
            finally
            {
                logger.Debug("Starting upload thread...");
                importdbForm.DialogResult = DialogResult.OK;
                //ThreadHandler.GetInstance(new UploadEnrollmentAsynch()).StartThread();
                //ThreadHandler.GetInstance(new UploadThreadsAsync()).StartThread();
            }

            if (totalRows > 0 && numberOfRowsAffected > 0)
            {
                //MessageBox.Show(String.Format("Number of records imported: {0}.\n"
                //    + "Number of records already exist: {1}",
                //    numberOfRowsAffected, (totalRows - numberOfRowsAffected)),
                //    "Process Completed");

                InfoMessageBox.ShowMessage("SNSOP TOOLS", "Number of records imported: " + numberOfRowsAffected);

                logger.Info("Import Path: " + importPath);
                logger.Info("Number of Record Imported: " + numberOfRowsAffected + "\nNumber of Record already Exist: " + (totalRows - numberOfRowsAffected));
            }
            else
            {
                //MessageBox.Show("No new record found to import.", "Process Completed");
                InfoMessageBox.ShowMessage("SNSOP TOOLS", "Process Completed.\n\nNo new record found to import.");
            }
        }
    }
}
