using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.COMMON.Subscription
{
    public class CounterPendingSubject : Subject
    {
        //private const string NAME = "counter_pending_status";
        private string name;

        private int count = 0;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
