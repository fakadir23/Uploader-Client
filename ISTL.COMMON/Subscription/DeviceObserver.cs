using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISTL.COMMON.Subscription
{
    public interface IDeviceObservable : IObservable
    {
        void ShowInstalledStatus(string name, bool status, string actualName);
        void ShowConnectedStatus(string name, bool status, string actualName);
    }


    public class DeviceObserver : IObserver
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private string name;
        private bool isChecked = false;
        private bool isInstalled = false;
        private bool isConnected = false;
        private string actualName;
        private DeviceSubject subject;
        private IDeviceObservable view;

        public DeviceObserver(DeviceSubject sub, IDeviceObservable v)
        {
            subject = sub;
            view = v;
        }

        public void Update()
        {
            name = subject.Name;
            isChecked = subject.IsChecked;
            isInstalled = subject.IsInstalled;
            isConnected = subject.IsConnected;
            actualName = subject.actualName;

            // Do not show default status
            if (isChecked)
            {
                ShowStatus(view, name, isInstalled, isConnected, actualName);
            }
        }

        private delegate void InvokeCallback(IDeviceObservable v, string device, bool installed, bool connected, string actualName);

        private void ShowStatus(IDeviceObservable v, string device, bool installed, bool connected, string actualName)
        {
            try
            {
                if (((ContainerControl)v).InvokeRequired)
                {
                    // This is required because cross-thread operations are not valid with forms
                    InvokeCallback callback = new InvokeCallback(ShowStatus);
                    ((ContainerControl)v).Invoke(callback, new object[] { v, device, installed, connected, actualName });
                    return;
                }

                v.ShowInstalledStatus(device, installed, actualName);
                v.ShowConnectedStatus(device, connected, actualName);
            }
            catch (ObjectDisposedException e)
            {
                // This may happen when the form has been disposed
                // while the SUBJECT was notifying the OBSERVERS
                logger.Debug("Exception during DeviceObserver Update():\n" + e.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show("There was an unexpected error when updating the device status.");
                logger.Error("Exception during DeviceObserver Update():\n" + e.ToString());
            }
        }
    }
}
