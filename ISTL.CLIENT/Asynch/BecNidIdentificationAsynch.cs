using ISTL.COMMON.Subscription;
using ISTL.COMMON.Threads;
using ISTL.MODELS.DTO.New.Enrollment.BECverify;
using ISTL.MODELS.Request.New;
using ISTL.MODELS.Response.New;
using ISTL.RAB.ApiManager;
using ISTL.RAB.DbManager;
using ISTL.RAB.View;
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
    public class BecNidIdentificationAsynch : IThreadable
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        //private const int SLEEP_TIME = 5 * 60 * 1000;
        private int SLEEP_TIME = Convert.ToInt32(ConfigurationManager.AppSettings["NidSearchTimeIntervalInMS"].ToString());
        private const string NAME = "bec_nid_identification_result";
        private AutoResetEvent waitHandle = new AutoResetEvent(false);
        private AutoResetEvent abortHandle = new AutoResetEvent(false);
        private NidSearchSubject nidSearchSubject;
        private BECApiManager becApiManager;
        private DbBecManager dbBecManager;

        public BecNidIdentificationAsynch()
        {
            becApiManager = new BECApiManager();
            dbBecManager = new DbBecManager();
            nidSearchSubject = (NidSearchSubject)SubjectFactory.GetInstance().GetSubject(NidSearchSubject.Name);
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
                logger.Debug("BEC NID IDENTIFICATION RESULT FINDER: task started!");
                while (true)
                {
                    if (!GetNidSearhresult())
                    {
                        // Only when thread stop request received
                        break;
                    }

                    logger.Debug("BEC NID IDENTIFICATION RESULT FINDER: Going into sleep mode!");
                    GetWaitHandle().WaitOne(GetSleepTime());
                    logger.Debug("BEC NID IDENTIFICATION RESULT FINDER: Woke up from sleep!");
                    if (GetAbortHandle().WaitOne(1))
                    {
                        logger.Debug("BEC NID IDENTIFICATION RESULT FINDER: Abort request received. Getting out!");
                        break;
                    }
                }
            }
            catch (ThreadAbortException e)
            {
                logger.Debug("BEC NID IDENTIFICATION RESULT FINDER: Force shutdown initiated! ThreadAbortException: " + e.Message);
            }
        }

        private bool GetNidSearhresult()
        {
            Exception ex = null;
            try
            {
                var matchNotFoundList = new List<BECvoterInfoDto>();
                var list = dbBecManager.GetRequestList("PENDING", 0, 0);

                if (list.Count <= 0) return true;

                nidSearchSubject.State = NidSearchSubject.Status.PENDING;
                nidSearchSubject.Notify();

                GetBECidentifyRequest request = new GetBECidentifyRequest();
                GetBECidentifyResponse response = new GetBECidentifyResponse();

                foreach (var obj in list)
                {
                    request.token = obj.token;
                    response = becApiManager.GetProfileResultByNidBiometric(request);

                    if (response != null && response.code == (int)HttpResponseStatus.BEC_NID_IDENTIFICATION_ERROR)
                    {
                        // Match failed or error
                        nidSearchSubject.State = NidSearchSubject.Status.FAILED;
                        nidSearchSubject.Notify();

                        matchNotFoundList.Add(obj);
                        dbBecManager.UpdateRequest(matchNotFoundList, (int)NidSearchSubject.Status.FAILED, 
                            obj.createdAt,
                            null, obj.userId, obj.token);

                        Thread.Sleep(1000);

                        return true;
                    }

                    if (response != null && response.code == (int)HttpResponseStatus.OK)
                    {
                        if (response.payloads != null && response.payloads.Count > 0)
                        {
                            // Match found
                            nidSearchSubject.State = NidSearchSubject.Status.FOUND;
                            nidSearchSubject.Notify();

                            dbBecManager.UpdateRequest(response.payloads, (int)NidSearchSubject.Status.FOUND, 
                                obj.createdAt,
                                DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffK"), obj.userId, obj.token);

                            Thread.Sleep(1000);
                        }
                        else
                        {
                            // Match not found
                            nidSearchSubject.State = NidSearchSubject.Status.NOT_FOUND;
                            nidSearchSubject.Notify();

                            matchNotFoundList.Add(obj);
                            dbBecManager.UpdateRequest(matchNotFoundList, (int)NidSearchSubject.Status.NOT_FOUND, 
                                obj.createdAt,
                                null, obj.userId, obj.token);

                            Thread.Sleep(1000);
                        }
                    }
                }
            }
            catch (ThreadAbortException x)
            {
                logger.Debug("Inside BEC NID IDENTIFICATION RESULT SENDER: Force shutdown initiated! ThreadAbortException: " + x.Message);
                return false;
            }
            catch (System.Net.WebException x)
            {
                // Network Error.
                logger.Debug("Known error, when server not found during BEC NID IDENTIFICATION RESULT FINDER.\n" + x.Message);
                nidSearchSubject.State = NidSearchSubject.Status.FAILED;
                nidSearchSubject.Notify();
            }
            catch (TimeoutException x)
            {
                logger.Debug("Connection timedout during sending BEC NID IDENTIFICATION RESULT FINDER.\n" + x.Message);
                nidSearchSubject.State = NidSearchSubject.Status.FAILED;
                nidSearchSubject.Notify();
            }
            catch (System.Exception x)
            {
                logger.Error("There was an unexpected error when BEC NID IDENTIFICATION RESULT FINDER.\n" + x.ToString());
                ex = x;
                nidSearchSubject.State = NidSearchSubject.Status.FAILED;
                nidSearchSubject.Notify();
            }
            if (ex != null) ErrorMessageBox.ShowError("There was an unexpected error by BEC NID IDENTIFICATION RESULT FINDER.",ex);
            return true;
        }
    }
}
