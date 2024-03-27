using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.COMMON.Subscription
{
    public class UploadSubject : Subject
    {
        public enum Status { UPLOADING, IDLE, FAILED };

        private const string NAME = "upload_status";
        private Status status = Status.IDLE;
        private string error;
        private int pending = 0;
        private string timeLeft;

        public Status State
        {
            get { return status; }
            set
            {
                status = value;
                if (status != Status.UPLOADING)
                {
                    timeLeft = null;
                }
            }
        }

        public string Error
        {
            get
            {
                if (status == Status.FAILED) return error;
                return null;
            }
            set
            {
                error = value;
                status = Status.FAILED;
            }
        }

        public int Pending
        {
            get { return pending; }
            set { pending = value; }
        }

        public string TimeLeft
        {
            get { return timeLeft; }
            set { timeLeft = value; }
        }

        public static string Name
        {
            get { return NAME; }
        }
    }
}
