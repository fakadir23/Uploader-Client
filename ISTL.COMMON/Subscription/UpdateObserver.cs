using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISTL.COMMON.Subscription
{
    public interface IUpdateObservable : IObservable
    {
        void ShowUpdateAvailable();
        void ShowNoUpdates();
    }


    public class UpdateObserver : IObserver
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private UpdateSubject.Status state;
        private UpdateSubject subject;
        private IUpdateObservable view;

        public UpdateObserver(UpdateSubject sub, IUpdateObservable v)
        {
            subject = sub;
            view = v;
        }

        public void Update()
        {
            state = subject.State;
            ShowStatus(view, state);
        }

        private delegate void InvokeCallback(IUpdateObservable v, UpdateSubject.Status status);

        private void ShowStatus(IUpdateObservable v, UpdateSubject.Status status)
        {
            try
            {
                if (((ContainerControl)v).InvokeRequired)
                {
                    // This is required because cross-thread operations are not valid with forms
                    InvokeCallback callback = new InvokeCallback(ShowStatus);
                    ((ContainerControl)v).Invoke(callback, new object[] { v, status });
                    return;
                }

                switch (status)
                {
                    case UpdateSubject.Status.AVAILABLE:
                        v.ShowUpdateAvailable();
                        break;
                    case UpdateSubject.Status.NONE:
                        v.ShowNoUpdates();
                        break;
                }
            }
            catch (ObjectDisposedException e)
            {
                // This may happen when the form has been disposed
                // while the SUBJECT was notifying the OBSERVERS
                logger.Debug("Exception during UpdateObserver Update():\n" + e.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show("There was an unexpected error when showing the update status.");
                logger.Error("Exception during UpdateObserver Update():\n" + e.ToString());
            }
        }
    }
}
