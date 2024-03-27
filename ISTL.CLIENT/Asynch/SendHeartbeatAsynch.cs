using ISTL.COMMON;
using ISTL.COMMON.Subscription;
using ISTL.COMMON.Threads;
using ISTL.MODELS.DTO.New.Enrollment;
using ISTL.MODELS.Request.WorkStation;
using ISTL.MODELS.Response.New;
using ISTL.PERSOGlobals;
using ISTL.RAB.ApiManager;
using ISTL.RAB.DbManager;
using ISTL.RAB.Entity;
using ISTL.RAB.View;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ISTL.RAB.Asynch
{
    public class SendHeartbeatAsynch : IThreadable
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        //private const int SLEEP_TIME = 5 * 60 * 1000;
        private int SLEEP_TIME = Convert.ToInt32(ConfigurationManager.AppSettings["HeartbeatSendIntervalInMillisecond"].ToString());
        private const string NAME = "heartbeat_sender";
        private AutoResetEvent waitHandle = new AutoResetEvent(false);
        private AutoResetEvent abortHandle = new AutoResetEvent(false);
        private WorkStationApiManager workStationApiManager;

        public SendHeartbeatAsynch()
        {
            workStationApiManager = new WorkStationApiManager();
        }
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
                logger.Debug("HEARTBEAT SENDER: task started!");
                while (true)
                {
                    if (!SendHeartbeat())
                    {
                        // Only when thread stop request received
                        break;
                    }

                    logger.Debug("HEARTBEAT SENDER: Going into sleep mode!");
                    GetWaitHandle().WaitOne(GetSleepTime());
                    logger.Debug("HEARTBEAT SENDER: Woke up from sleep!");
                    if (GetAbortHandle().WaitOne(1))
                    {
                        logger.Debug("HEARTBEAT SENDER: Abort request received. Getting out!");
                        break;
                    }
                }
            }
            catch (ThreadAbortException e)
            {
                logger.Debug("HEARTBEAT SENDER: Force shutdown initiated! ThreadAbortException: " + e.Message);
            }
        }

        private bool SendHeartbeat()
        {
            OnlineSubject onlineStatus = (OnlineSubject)SubjectFactory.GetInstance().GetSubject(OnlineSubject.Name);

            // Set access token
            //AppUtils.AppUtils.SetTokenByAdminUser();
            Exception CriticalException = null;

            AppUtils.AppUtils.SetTokenByLoggedInUser();

            try
            {
                var request = new WorkStationAliveRequest() { code = Users.WorkStationCode };
                var response = workStationApiManager.SendHeartbeat(request);

                if (response != null && response.code != (int)HttpResponseStatus.OK)
                {
                    logger.Debug("Sending heartbeat to server by API call is not success. Error Message: " + response.message);
                }

                if (!onlineStatus.IsOnline)
                {
                    onlineStatus.IsOnline = true;
                    onlineStatus.Notify();
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
                logger.Debug("Known error, when server not found during sending heartbeat to server.\n" + x.Message);
                onlineStatus.IsOnline = false;
                onlineStatus.Notify();
            }
            catch (TimeoutException x)
            {
                logger.Debug("Connection timedout during sending heartbeat to server.\n" + x.Message);
                onlineStatus.IsOnline = false;
                onlineStatus.Notify();
            }
            catch (System.Exception x)
            {
                logger.Error("There was an unexpected error when sending heartbeat to server.\n" + x.ToString());
                CriticalException = x;
            }
            if (CriticalException != null) ErrorMessageBox.ShowError("There was an unexpected error when sending heartbeat to server.", CriticalException);
            return true;
        }
    }
}
