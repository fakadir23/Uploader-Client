using System.Collections.Generic;
using System.Threading;
using ISTL.COMMON.Threads;

namespace ISTL.COMMON.Subscription
{
    public abstract class Subject
    {
        private List<IObserver> observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            lock (observers)
            {
                observers.Add(observer);
            }
            new Thread(observer.Update).Start();
        }

        public void Detach(IObserver observer)
        {
            lock (observers)
            {
                observers.Remove(observer);
            }
        }

        public void Notify()
        {
            // Since observers may be changed concurrently by multiple threads
            // It is important to synchronize access
            // Otherwise the enumeration below may change while the loop is in progress
            // and can throw an exception

            List<IObserver> unchangingObserverList = new List<IObserver>();
            lock (observers)
            {
                foreach (IObserver observer in observers)
                {
                    unchangingObserverList.Add(observer);
                }
            }

            foreach (IObserver observer in unchangingObserverList)
            {
                new Thread(observer.Update).Start();
            }
        }
    }
}
