using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISTL.COMMON.Subscription
{
    public class OnlineSubject : Subject
    {
        private const string NAME = "online_status";
        private bool isOnline = true;

        public bool IsOnline
        {
            get { return isOnline; }
            set { isOnline = value; }
        }

        public static string Name
        {
            get { return NAME; }
        }
    }
}
