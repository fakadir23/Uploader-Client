using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.COMMON.Subscription
{
    public class DeviceSubject : Subject
    {
        private string name;
        private bool isChecked = false;
        private bool isInstalled = false;
        private bool isConnected = false;

        // Making this public instead of creating a property, because may want to pass this
        // as out parameter
        public string actualName;

        public DeviceSubject(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// This property should be set when the actual checking of the device is
        /// done. It is useful when you need to show the actual device status
        /// instead of getting the default status values. For example:
        /// 
        ///     if (IsChecked) {
        ///         // show connected status;
        ///     }
        ///     
        /// </summary>
        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; }
        }

        public bool IsInstalled
        {
            get { return isInstalled; }
            set { isInstalled = value; }
        }

        public bool IsConnected
        {
            get { return isConnected; }
            set { isConnected = value; }
        }

        public string Name
        {
            get { return name; }
        }
    }
}
