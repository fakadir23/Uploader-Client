using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.COMMON.Subscription
{
    public class UpdateSubject : Subject
    {
        public enum Status { AVAILABLE, NONE };

        private const string NAME = "update_status";
        private Status status = Status.NONE;

        public Status State
        {
            get { return status; }
            set { status = value; }
        }

        public static string Name
        {
            get { return NAME; }
        }
    }
}
