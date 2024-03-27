using ISTL.COMMON.Autoupdate;
using ISTL.COMMON.Subscription;
using ISTL.COMMON.Threads;
using ISTL.PERSOGlobals;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ISTL.RAB.Asynch
{
    public class UpdateCheckAsynch : IThreadable
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        //private const int SLEEP_TIME = 2 * 60 * 1000; // 2 minutes. But should be 2 hours in production
        private const int SLEEP_TIME = 1 * 60 * 1000; // 2 minutes. But should be 2 hours in production
        private const string NAME = "update_check";
        private AutoResetEvent waitHandle = new AutoResetEvent(false);
        private AutoResetEvent abortHandle = new AutoResetEvent(false);

        #region IThreadable Members

        public string GetName()
        {
            return NAME;
        }

        public int GetSleepTime()
        {
            return SLEEP_TIME;
        }

        public AutoResetEvent GetWaitHandle()
        {
            return waitHandle;
        }

        public AutoResetEvent GetAbortHandle()
        {
            return abortHandle;
        }

        public void Task()
        {
            try
            {
                logger.Debug("UPDATE-CHECK: task started!");
                while (true)
                {
                    if (!CheckUpdate())
                    {
                        // Only when thread stop request received
                        // Or this is not a ClickOnce application
                        break;
                    }

                    logger.Debug("UPDATE-CHECK: Going into sleep mode!");
                    GetWaitHandle().WaitOne(GetSleepTime());
                    logger.Debug("UPDATE-CHECK: Woke up from sleep!");
                    if (GetAbortHandle().WaitOne(1))
                    {
                        logger.Debug("UPDATE-CHECK: Abort request received. Getting out!");
                        break;
                    }
                }
            }
            catch (ThreadAbortException e)
            {
                logger.Debug("UPDATE-CHECK: Force shutdown initiated! ThreadAbortException: " + e.Message);
            }
        }

        #endregion

        /// <summary>
        /// Check for updates
        /// </summary>
        /// <returns>
        /// FALSE: If Abort request received (i.e. Thread stop request is received)
        /// TRUE: Does not necessarily suggest there is an update available
        /// </returns>
        private bool CheckUpdate()
        {
            UpdateSubject updateStatus = (UpdateSubject)SubjectFactory.GetInstance().GetSubject(UpdateSubject.Name);

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
                    logger.Debug("Update found.");
                    // Update available. Notify observers
                    if (updateStatus.State != UpdateSubject.Status.AVAILABLE)
                    {
                        updateStatus.State = UpdateSubject.Status.AVAILABLE;
                        updateStatus.Notify();
                    }
                }
                else
                {
                    if (updateStatus.State != UpdateSubject.Status.NONE)
                    {
                        updateStatus.State = UpdateSubject.Status.NONE;
                        updateStatus.Notify();
                    }
                }
            }
            catch (ThreadAbortException x)
            {
                logger.Debug("Inside UpdateChecker: Force shutdown initiated! ThreadAbortException: " + x.Message);
                return false; // Return from here since application is shutting down. No need to notify all observers.
            }
            catch (System.Net.WebException x)
            {
                // Network Error.
                logger.Debug("Known error, when server not found.\n" + x.Message);
                updateStatus.State = UpdateSubject.Status.NONE;
                updateStatus.Notify();
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

            return true;
        }
    }
}
