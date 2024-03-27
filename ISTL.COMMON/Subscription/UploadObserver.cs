using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISTL.COMMON.Subscription
{
    public interface IUploadObservable : IObservable
    {
        void ShowUploadIdleStatus(int pending);
        void ShowUploadingStatus(int pending, string timeLeft);
        void ShowUploadFailedStatus(int pending);
    }

    public class UploadObserver : IObserver
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private UploadSubject subject;
        private IUploadObservable view;

        private UploadSubject.Status state;
        private int pending;
        private string timeLeft;

        public UploadObserver(UploadSubject sub, IUploadObservable v)
        {
            subject = sub;
            view = v;
        }

        public void Update()
        {
            state = subject.State;
            pending = subject.Pending;
            timeLeft = subject.TimeLeft;
            ShowStatus(view, state, pending, timeLeft);
        }

        private delegate void InvokeCallback(IUploadObservable v, UploadSubject.Status status, int count, string time);

        private void ShowStatus(IUploadObservable v, UploadSubject.Status status, int count, string time)
        {
            try
            {
                if (((ContainerControl)v).InvokeRequired)
                {
                    // This is required because cross-thread operations are not valid with forms
                    InvokeCallback callback = new InvokeCallback(ShowStatus);
                    ((ContainerControl)v).Invoke(callback, new object[] { v, status, count, time });
                    return;
                }

                switch (status)
                {
                    case UploadSubject.Status.IDLE:
                        v.ShowUploadIdleStatus(count);
                        break;
                    case UploadSubject.Status.UPLOADING:
                        v.ShowUploadingStatus(count, time);
                        break;
                    case UploadSubject.Status.FAILED:
                        v.ShowUploadFailedStatus(count);
                        break;
                }
            }
            catch (ObjectDisposedException e)
            {
                // This may happen when the form has been disposed
                // while the SUBJECT was notifying the OBSERVERS
                logger.Debug("Exception during UploadObserver Update():\n" + e.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show("There was an unexpected error when updating the upload status.");
                logger.Error("Unexpected exception during UploadObserver Update():\n" + e.ToString());
            }
        }
    }
}
