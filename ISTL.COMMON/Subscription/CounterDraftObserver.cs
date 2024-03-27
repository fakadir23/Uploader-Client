using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISTL.COMMON.Subscription
{
    public interface ICounterDraftObservable : IObservable
    {
        void ShowDraftCount(int cnt, string name);
    }

    public class CounterDraftObserver : IObserver
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private int count = 0;
        private string name;
        private CounterDraftSubject subject;
        private ICounterDraftObservable view;


        public CounterDraftObserver(CounterDraftSubject sub, ICounterDraftObservable v)
        {
            subject = sub;
            view = v;
        }

        public void Update()
        {
            count = subject.Count;
            name = subject.Name;
            ShowStatus(view, count, name);
        }

        private delegate void InvokeCallback(ICounterDraftObservable v, int cnt, string name);

        private void ShowStatus(ICounterDraftObservable v, int cnt, string name)
        {
            try
            {
                if (((ContainerControl)v).InvokeRequired)
                {
                    // This is required because cross-thread operations are not valid with forms
                    InvokeCallback callback = new InvokeCallback(ShowStatus);
                    ((ContainerControl)v).Invoke(callback, new object[] { v, cnt, name });
                    return;
                }
                v.ShowDraftCount(cnt, name);
            }
            catch (ObjectDisposedException e)
            {
                // This may happen when the form has been disposed
                // while the SUBJECT was notifying the OBSERVERS
                logger.Debug("Exception during Count Draft Observer Update():\n" + e.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show("There was an unexpected error when updating the count draft status.");
                logger.Error("Exception during Count Draft Observer Update():\n" + e.ToString());
            }
        }
    }
}
