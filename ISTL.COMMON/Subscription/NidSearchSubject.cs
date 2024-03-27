using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.COMMON.Subscription
{
    public class NidSearchSubject : Subject
    {
        public enum Status { IDLE, PENDING, FAILED, FOUND, NOT_FOUND, VIEWED };

        private const string NAME = "nid_search_status";
        private Status status = Status.IDLE;
        private int count = 0;

        public Status State
        {
            get { return status; }
            set
            {
                status = value;                
            }
        }

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        public static string Name
        {
            get { return NAME; }
        }
    }
}
