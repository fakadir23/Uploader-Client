using Microsoft.Win32;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISTL.COMMON.Utility
{
    public sealed class RegistryUtils
    {
        #region Declaration(s)
        private static readonly NLog.Logger logger = LogManager.GetCurrentClassLogger();
        private static readonly string PATH = "Software\\";
        private static readonly string PATH_64 = "Software\\Wow6432Node\\";
        private static readonly string KEY_BASE = @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths";
        #endregion

        #region Method(s)
        public static bool WriteRegistryKey(string keyPath, string keyName, object value)
        {
            try
            {
                string subKey = PATH + keyPath;
                RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(subKey);
                registryKey.SetValue(keyName.ToUpper(), value);
                logger.Debug("Registry values: " + keyPath + " " + keyName + " " + value.ToString());
                return true;
            }
            catch (Exception x)
            {
                logger.ErrorException(string.Format("There was an errong when writting key {0}.", keyName), x);
            }

            return false;
        }

        public static object ReadRegistryKey(string keyPath, string keyName)
        {
            string subKey = PATH + keyPath;
            string subKey64 = PATH_64 + keyPath;

            // Open a subKey as read-only
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(subKey);
            // If the RegistrySubKey doesn't exist -> (null)
            if (registryKey != null)
            {
                try
                {
                    return registryKey.GetValue(keyName.ToUpper());
                }
                catch (Exception x)
                {
                    logger.ErrorException("There was an error when reading registry key.", x);
                }
            }

            RegistryKey registryKey64 = Registry.LocalMachine.OpenSubKey(subKey64);
            if (registryKey64 != null)
            {
                try
                {
                    return registryKey64.GetValue(keyName.ToUpper());
                }
                catch (Exception x)
                {
                    logger.ErrorException("There was an error when reading registry key on 64 bit machine.", x);
                }
            }

            return null;
        }

        public static string GetPathForExe(string fileName)
        {
            RegistryKey localMachine = Registry.LocalMachine;
            RegistryKey fileKey = localMachine.OpenSubKey(string.Format(@"{0}\{1}", KEY_BASE, fileName));
            object result = null;
            if (fileKey != null)
            {
                result = fileKey.GetValue(string.Empty);
            }
            fileKey.Close();

            return (string)result;
        }

        public static bool IsDriverInstalled(string pName)
        {
            try
            {
                RegistryKey key;
                string displayName = "";

                // search in: CurrentUser
                key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                if (key != null)
                {
                    foreach (String keyName in key.GetSubKeyNames())
                    {
                        RegistryKey subKey = key.OpenSubKey(keyName);
                        displayName = (string)subKey.GetValue("DisplayName");
                        if (displayName != null && displayName.Trim() != "" && displayName.Trim().Contains(pName))
                        {
                            return true;
                        }
                    }
                }

                // search in: LocalMachine_32
                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                if (key != null)
                {
                    foreach (String keyName in key.GetSubKeyNames())
                    {
                        RegistryKey subkey = key.OpenSubKey(keyName);
                        displayName = (string)subkey.GetValue("DisplayName");

                        if (displayName != null && displayName.Trim() != "" && displayName.Trim().Contains(pName))
                        {
                            return true;
                        }
                    }
                }

                // search in: LocalMachine_64
                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
                if (key != null)
                {
                    foreach (String keyName in key.GetSubKeyNames())
                    {
                        RegistryKey subkey = key.OpenSubKey(keyName);
                        displayName = (string)subkey.GetValue("DisplayName");
                        if (displayName != null && displayName.Trim() != "" && displayName.Trim().Contains(pName))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception x)
            {
                logger.Error("Exception caught in chacking whether a driver is intalled: {0}", x);
                return false;
            }
        }
        #endregion
    }
}
