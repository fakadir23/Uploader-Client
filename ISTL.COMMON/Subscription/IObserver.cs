using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ISTL.COMMON.Subscription
{
    public interface IObserver
    {
        void Update();
    }

    public interface IObservable
    {
        void SubscribeToNotifications();
        void UnsubscribeFromNotifications();
    }
}
