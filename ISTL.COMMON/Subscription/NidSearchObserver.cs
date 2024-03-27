using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ISTL.COMMON.Subscription.NidSearchSubject;

namespace ISTL.COMMON.Subscription
{
    public interface INidSearchObservable : IObservable
    {
        void ShowNidResult(Status status, int count);
    }

    public class NidSearchObserver : IObserver
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private NidSearchSubject subject;
        private INidSearchObservable view;

        private NidSearchSubject.Status state;
        private int count;

        public NidSearchObserver(NidSearchSubject sub, INidSearchObservable v)
        {
            subject = sub;
            view = v;
        }

        public void Update()
        {
            state = subject.State;
            count = subject.Count;
            ShowStatus(view, state, count);
        }

        private delegate void InvokeCallback(INidSearchObservable v, NidSearchSubject.Status status, int count);

        private void ShowStatus(INidSearchObservable v, NidSearchSubject.Status status, int count)
        {
            try
            {
                if (((ContainerControl)v).InvokeRequired)
                {
                    // This is required because cross-thread operations are not valid with forms
                    InvokeCallback callback = new InvokeCallback(ShowStatus);
                    ((ContainerControl)v).Invoke(callback, new object[] { v, status, count });
                    return;
                }

                v.ShowNidResult(status, count);
            }
            catch (ObjectDisposedException e)
            {
                // This may happen when the form has been disposed
                // while the SUBJECT was notifying the OBSERVERS
                logger.Debug("Exception during nid search result Update():\n" + e.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show("There was an unexpected error when updating the Nid Search Result.");
                logger.Error("Unexpected exception during NidSearchObserver Update():\n" + e.ToString());
            }
        }
    }
}
