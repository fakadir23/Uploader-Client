using ISTL.COMMON;
using ISTL.COMMON.Threads;
using ISTL.PERSOGlobals;
using ISTL.RAB.Asynch;
using ISTL.RAB.DbManager;
using ISTL.RAB.View;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISTL.RAB.Controllers
{
    public class ExportDbController : ViewController
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private ExportDbForm exportdbForm = null;

        public ExportDbController()
        {
            exportdbForm = new ExportDbForm();
            base.SetView((IView)exportdbForm);
            exportdbForm.SetController(this);
        }

        public void ExportDb(string exportPath, System.ComponentModel.BackgroundWorker worker)
        {
            string returnMsg = "";
            long totalRows = 0;
            long numberOfRowsAffected = 0;
            int exportFlag = 0;

            logger.Debug("Stopping upload thread...");
            //ThreadHandler.GetInstance(new UploadEnrollmentAsynch()).StopThread();
            ThreadHandler.GetInstance(new UploadThreadsAsync()).StopThread();
            try
            {
                DbTransfer dbTransfer = new DbTransfer();
                dbTransfer.setProgressCallback(exportdbForm.SetProgress);
                returnMsg = dbTransfer.Transfer(false, exportPath, worker);
                if (returnMsg == null)
                {
                    // DbTranser should show appropriate messagebox if there was an error
                    //MessageBox.Show("Export operation was cancelled.", "Processing Stopped");
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Processing Stopped\n\nExport operation was cancelled.");
                    return;
                }

                string[] splitMsg = returnMsg.Split('#');
                totalRows = Convert.ToInt64(splitMsg[0]);
                numberOfRowsAffected = Convert.ToInt64(splitMsg[1]);
                exportFlag = Convert.ToInt16(splitMsg[2]);
            }
            finally
            {
                logger.Debug("Starting upload thread...");
                exportdbForm.DialogResult = DialogResult.OK;
                //ThreadHandler.GetInstance(new UploadEnrollmentAsynch()).StartThread();
                ThreadHandler.GetInstance(new UploadThreadsAsync()).StartThread();
            }

            if (totalRows > 0 && numberOfRowsAffected > 0)
            {
                //MessageBox.Show(String.Format("Number of records exported: {0}.\n"
                //    + "Number of records already exist: {1}",
                //    numberOfRowsAffected, (totalRows - numberOfRowsAffected)),
                //    "Process Completed");

                InfoMessageBox.ShowMessage("SNSOP TOOLS", "Number of records exported: " + numberOfRowsAffected);

                logger.Info("Exported Path: " + exportPath);
                logger.Info("Number of Record Exported: " + numberOfRowsAffected + "\nNumber of Record already Exist: " + (totalRows - numberOfRowsAffected));
            }
            else
            {
                //MessageBox.Show("No new record found to export.", "Process Completed");
                InfoMessageBox.ShowMessage("SNSOP TOOLS", "Process Completed.\n\nNo new record found to export.");
                if (exportFlag != 1)
                {
                    try
                    {
                        File.Delete(exportPath);
                    }catch(Exception x)
                    {
                        logger.Error("Deleting exported db with no records failed. " + x.ToString());
                    }
                }
            }
        }
    }
}
