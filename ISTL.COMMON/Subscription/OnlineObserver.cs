using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;
using System.Threading;

namespace ISTL.COMMON.Subscription
{
    public interface IOnlineObservable : IObservable
    {
        void ShowOnlineStatus();
        void ShowOfflineStatus();
    }


    public class OnlineObserver : IObserver
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private bool isOnline = true;
        private OnlineSubject subject;
        private IOnlineObservable view;

        public OnlineObserver(OnlineSubject sub, IOnlineObservable v)
        {
            subject = sub;
            view = v;
        }

        public void Update()
        {
            isOnline = subject.IsOnline;
            ShowStatus(view, isOnline);
        }

        private delegate void InvokeCallback(IOnlineObservable v, bool online);

        private void ShowStatus(IOnlineObservable v, bool online)
        {
            try
            {
                if (((ContainerControl)v).InvokeRequired)
                {
                    // This is required because cross-thread operations are not valid with forms
                    InvokeCallback callback = new InvokeCallback(ShowStatus);
                    ((ContainerControl)v).Invoke(callback, new object[] { v, online });
                    return;
                }

                if (online)
                {
                    v.ShowOnlineStatus();
                }
                else
                {
                    v.ShowOfflineStatus();
                }
            }
            catch (ObjectDisposedException e)
            {
                // This may happen when the form has been disposed
                // while the SUBJECT was notifying the OBSERVERS
                logger.Debug("Exception during OnlineObserver Update():\n" + e.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show("There was an unexpected error when updating the online status.");
                logger.Error("Exception during OnlineObserver Update():\n" + e.ToString());
            }
        }
    }
}
