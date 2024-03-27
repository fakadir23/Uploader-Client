using ISTL.MODELS.DTO.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.Entity
{
    public class Configuration
    {
        private static int deleteDayCount;
        private static int maxUploadPending;
        private static int notifyDayCount;
        private static int verifyDayCount;

        public static DeviceDto Cam { get; set; }
        public static DeviceDto FP { get; set; }
        public static DeviceDto Iris { get; set; }
        public static int ReportDeleteDayCount { get; set; }
        public static int NidDeleteDayCount { get; set; }

        public static void ClearAll()
        {
            deleteDayCount = 0;
            maxUploadPending = 0;
            notifyDayCount = 0;
            verifyDayCount = 0;
            Cam = null;
            FP = null;
            Iris = null;
            ReportDeleteDayCount = 0;
            NidDeleteDayCount = 0;
        }

        public static int VerifyDayCount
        {
            get { return Configuration.verifyDayCount; }
            set { Configuration.verifyDayCount = value; }
        }

        public static int DeleteDayCount
        {
            get { return Configuration.deleteDayCount; }
            set { Configuration.deleteDayCount = value; }
        }

        public static int MaxUploadPending
        {
            get { return Configuration.maxUploadPending; }
            set { Configuration.maxUploadPending = value; }
        }

        public static int NotifyDayCount
        {
            get { return Configuration.notifyDayCount; }
            set { Configuration.notifyDayCount = value; }
        }
    }
}
