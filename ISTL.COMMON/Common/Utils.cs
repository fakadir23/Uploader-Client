using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using NLog;
using NLog.Config;
using NLog.Targets;
using System.IO.IsolatedStorage;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using System.Management;
using Microsoft.Win32;
using System.Drawing;
using ISTL.COMMON.CustomControl;
using MaterialSkin.Controls;

namespace ISTL.COMMON
{
    public class Utils
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static string keyBase = @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths";
        public static bool isValidString(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            try
            {
                if (!String.IsNullOrEmpty(obj.ToString()))
                {
                    return true;
                }
            }
            catch (Exception x)
            {
                logger.Error(x.Message);
            }
            return false;
        }

        public static string UpperLowerReverse(string text)
        {
            StringBuilder sb = new StringBuilder(text.Trim().Length);
            foreach (char c in text.Trim())
            {
                if (Char.IsLetter(c))
                {
                    if (Char.IsLower(c))
                    {
                        sb.Append(Char.ToUpper(c));
                    }
                    else if (Char.IsUpper(c))
                    {
                        sb.Append(Char.ToLower(c));
                    }
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static string GetFormattedString(object obj)
        {
            if (obj == DBNull.Value)
            {
                return null;
            }
            if (obj == null)
            {
                return null;
            }

            try
            {
                if (!String.IsNullOrEmpty((string)obj))
                {
                    return (string)obj;
                }
            }
            catch (Exception x)
            {
                logger.Error(x.Message);
            }
            return null;
        }

        public static bool IsAlphaCheck(String str)
        {
            return str.All(c=>Char.IsLetter(c) || c==' ');
        }

        public static bool isDigit(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            try
            {
                if (!String.IsNullOrEmpty(obj.ToString()) &&
                    Regex.IsMatch(obj.ToString(), @"^[0-9]+$"))
                {
                    return true;
                }
            }
            catch (Exception x)
            {
                logger.Error(x.Message);
            }
            return false;
        }

        public static string GetAssemblyPath()
        {
            try
            {
                Uri uri = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase));
                return uri.LocalPath;
            }
            catch (Exception x)
            {
                logger.Error(x.Message);
                throw x;
            }
        }

        public static string ByteArrayToBase64String(byte[] bytes)
        {
            if (bytes == null)
            {
                return "";
            }
            try
            {
                return System.Convert.ToBase64String(bytes, 0, bytes.Length);
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
            }

            return "";
        }

        public static byte[] Base64StringToByteArray(string str)
        {
            if (str == null)
            {
                return null;
            }
            try
            {
                return System.Convert.FromBase64String(str);
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
            }

            return null;
        }

        public static byte[] StringToByteArray(string plainText)
        {
            if (plainText == null) return null;
            return Encoding.UTF8.GetBytes(plainText);
        }

        public static string HexToString(string hexValue)
        {
            string StrValue = "";
            while (hexValue.Length > 0)
            {
                char value = System.Convert.ToChar(System.Convert.ToUInt32(hexValue.Substring(0, 2), 16));
                if (value == '\0') break;
                StrValue += value.ToString();
                hexValue = hexValue.Substring(2, hexValue.Length - 2);
            }
            return StrValue;
        }

        public static string ByteArrayToHexString(byte[] data)
        {
            String hex = "";
            foreach (byte b in data)
            {
                hex += Convert.ToString(b, 16).PadLeft(2, '0');
            }
            return hex;
        }

        public static byte[] HexStringToByteArray(string hex)
        {
            hex = hex.Replace(" ", "");
            byte[] data = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                data[i / 2] = (byte)Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return data;
        }

        public static IPAddress GetClientIpAddress()
        {
            IPAddress returnValue = null;
            IPAddress[] ipAdresses = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress clientIpAddress in ipAdresses)
            {
                if (clientIpAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    returnValue = clientIpAddress;
                }
            }
            return returnValue;
        }

        public static IPAddress ParseIpAddress(string ipAddress)
        {
            if (ipAddress == null) return null;

            try
            {
                return IPAddress.Parse(ipAddress);
            }
            catch (Exception x)
            {
                logger.Error(x.Message);
            }
            return null;
        }

        public static DateTime GetCurrentDateTime()
        {
            TimeZone zone = TimeZone.CurrentTimeZone;
            return zone.ToLocalTime(DateTime.Now);
        }

        public static string DefaultDataPath()
        {
            string path;

            /* Need to figure out if Isolated Storage can be used
             * to store all log files and sqlite file.
             * 
             * 
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                // Is ClickOnce application, so get isolated storage 
                IsolatedStorageFile store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
                path = store.GetType().GetField("m_RootDir", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(store).ToString();
            }
            else
            {
                // Not ClickOnce application, so use my doc folder
                path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            */
            path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

            return path;
        }

        public static void SetupLogging(string filename, LogLevel level)
        {
            string path = DefaultDataPath();
            string archivePath = path + "\\log";

            SetupLogging(filename, level, path, archivePath);
        }

        public static void SetupLogging(string filename, LogLevel level, string path, string archivePath)
        {
            LoggingConfiguration config = new LoggingConfiguration();
            FileTarget fileTarget = new FileTarget();

            config.AddTarget("file", fileTarget);
            fileTarget.FileName = path + "\\" + filename;
            //fileTarget.Layout = "${date:format=HH\\:MM\\:ss} ${logger} ${message}";
            fileTarget.ArchiveFileName = archivePath + "\\${shortdate}.{###}." + filename;
            fileTarget.ArchiveEvery = FileArchivePeriod.Day;

            LoggingRule rule = new LoggingRule("*", level, fileTarget);
            config.LoggingRules.Add(rule);

            LogManager.Configuration = config;
        }

        public static string GetPendingTime(int doneCount, long doneDuration, int pendingCount)
        {
            string remainingTime = "";
            double milliseconds = (doneDuration / doneCount) * pendingCount;
            double seconds = milliseconds / 1000;
            double minutes = seconds / 60;
            double hours = minutes / 60;

            if (hours >= 1)
            {
                remainingTime = String.Format("{0:0.0}", hours) + " hours";
            }
            else if (minutes >= 1)
            {
                remainingTime = String.Format("{0:0.0}", minutes) + " minutes";
            }
            else if (seconds > 0)
            {
                remainingTime = String.Format("{0:0.0}", seconds) + " seconds";
            }
            return remainingTime;
        }

        public static byte[] FileToByteArray(string fileName)
        {
            byte[] buffer = null;

            try
            {
                // open file for reading
                System.IO.FileStream fileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                // attach filestream to binary reader
                System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(fileStream);

                // get total byte length of the file
                long totalBytes = new System.IO.FileInfo(fileName).Length;

                // read entire file into buffer
                buffer = binaryReader.ReadBytes((Int32)totalBytes);

                // close file reader
                fileStream.Close();
                fileStream.Dispose();
                binaryReader.Close();
            }
            catch (Exception x)
            {
                // error
                logger.Error("Exception caught in converting file to byte array: {0}", x);
                throw x;
            }

            return buffer;
        }

        public static DateTime ConvertMilliSecondToDateTime(long ms)
        {
            // NOTE:
            // A single tick represents one hundred nanoseconds or one ten-millionth of a second. 
            // There are 10,000 ticks in a millisecond.
            // The value of this property represents the number of 100-nanosecond intervals that 
            // have elapsed since 12:00:00 midnight, January 1, 0001, which represents DateTime.MinValue. 
            // It does not include the number of ticks that are attributable to leap seconds.

            //long previousTicks = new DateTime(1970, 01, 01).Ticks;
            DateTime previousDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
            long previousTicks = previousDateTime.Ticks;
            long currentTicks = ms * 10000;
            long totalTicks = previousTicks + currentTicks;
            DateTime date = new DateTime(totalTicks);
            return date;
        }

        public static long ConvertDateTimeToMilliSeconds(DateTime dt)
        {
            //create Timespan by subtracting the value provided from
            //the Unix Epoch
            TimeSpan span = (dt - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            //return the total milliseconds (which is a UNIX timestamp)
            return (long)span.TotalMilliseconds;
        }

        public static long ConvertToUnixTimestamp(DateTime value)
        {
            //create Timespan by subtracting the value provided from
            //the Unix Epoch
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());

            //return the total  miliseconds (which is a UNIX timestamp)
            return (long)(span.TotalSeconds * 1000);
        }

        /// <summary>
        /// Determine if Date String is an actual date
        /// Date format = YYYY/MM/DD
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool ValidateDate(string year, string month, string day)
        {
            try
            {
                if (year.Length != 4)
                {
                    return false;
                }
                // create new date from the parts; if this does not fail
                // the method will return true and the date is valid
                DateTime testDate = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day));
                return true;
            }
            catch
            {
                // if a test date cannot be created, the
                // method will return false
                return false;
            }
        }

        public static bool isValidDrivingLicenceNo(string dlNo)
        {
            try
            {
                bool isValidate = false;
                string str = "";

                //checking exact 15 alpha numeric length
                isValidate = Regex.IsMatch(dlNo, @"^[A-Za-z0-9]{15}$");
                if (!isValidate) return isValidate;

                //checking first two values are characters
                isValidate = Regex.IsMatch(dlNo, @"[A-Za-z]{2}");
                if (!isValidate) return isValidate;

                //checking consecutive seven values are numeric after first two fields
                str = dlNo.Substring(2, 7);
                isValidate = Regex.IsMatch(str, @"^[0-9]{7}$");
                if (!isValidate) return isValidate;

                //checking 8th field is character
                str = dlNo.Substring(9, 1);
                isValidate = Regex.IsMatch(str, @"[A-Za-z]{1}");
                if (!isValidate) return isValidate;

                //checking 15th field is numeric
                str = dlNo.Substring(14, 1);
                isValidate = Regex.IsMatch(str, @"[0-9]{1}");
                if (!isValidate) return isValidate;

                return isValidate;
            }
            catch (Exception x)
            {
                logger.Error("Exception caught in validating DL no: {0}", x);
                return false;
            }
        }

        public static int ConvertToInt(object inputObj)
        {
            int value = 0;
            try
            {
                value = Convert.ToInt32(inputObj);
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
            }

            return value;
        }

        public static int? ConvertToNullableInt(object inputObj)
        {
            int? value = null;
            try
            {
                value = Convert.ToInt32(inputObj);
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
            }

            return value;
        }

        public static long ConvertToLong(object inputObj)
        {
            long value = 0;
            try
            {
                value = Convert.ToInt64(inputObj);
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
            }

            return value;
        }

        public static long? ConvertToNullableLong(object inputObj)
        {
            long? value = null;
            try
            {
                value = Convert.ToInt64(inputObj);
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
            }

            return value;
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

        /// <summary>
        /// Some ideas taken from: http://www.codeproject.com/Articles/30031/Query-hardware-device-status-in-C
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="matchAll"></param>
        /// <param name="matchOne"></param>
        /// <param name="actualName"></param>
        /// <returns>TRUE: Connected and working</returns>
        public static bool IsDeviceConnected(string deviceName, string[] matchAll, string[] matchOne, out string actualName)
        {
            actualName = null;
            string wmiQuery = string.Format(
                "SELECT Name, Status FROM Win32_PnPEntity WHERE Name LIKE '%{0}%'", deviceName);
            ManagementObjectSearcher devices = new ManagementObjectSearcher(wmiQuery);

            foreach (ManagementObject device in devices.Get())
            {
                try
                {
                    string name = device.GetPropertyValue("Name").ToString();

                    if (matchAll != null)
                    {
                        bool allParamsFound = true;
                        foreach (string token in matchAll)
                        {
                            if (!name.ToLower().Contains(token.ToLower()))
                            {
                                allParamsFound = false;
                                break;
                            }
                        }
                        if (!allParamsFound) continue;
                    }

                    if (matchOne != null)
                    {
                        bool atleastOneParamFound = false;
                        foreach (string token in matchOne)
                        {
                            if (name.ToLower().Contains(token.ToLower()))
                            {
                                atleastOneParamFound = true;
                                break;
                            }
                        }
                        if (!atleastOneParamFound) continue;
                    }

                    // Device connected
                    actualName = name;

                    string status = device.GetPropertyValue("Status").ToString();
                    // Operational statuses include: "OK", "Degraded", and "Pred Fail" 
                    // (an element, such as a SMART-enabled hard disk drive, may be functioning 
                    // properly but predicting a failure in the near future). Nonoperational
                    // statuses include: "Error", "Starting", "Stopping", and "Service".
                    // Ref: http://msdn.microsoft.com/en-us/library/aa394353(VS.85).aspx
                    if (status == "OK" || status == "Degraded" || status == "Pred Fail")
                    {
                        // Device connected and working
                        return true;
                    }
                    else
                    {
                        // Device connected but not working
                        return false;
                    }
                }
                catch (Exception e)
                {
                    logger.Debug("Got an error when reading Device info. This can happen when devices "
                        + "are connected and disconnected very quickly. Error = " + e.Message);
                }
            }
            // Device not connected
            return false;
        }


        /// <summary>
        /// Keys the press handler.
        /// key press handler for DOB checking
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        /// <param name="textBox">The text box.</param>
        /// <param name="textBoxName">Name of the text box.</param>
        public static void KeyPressHandler(KeyPressEventArgs e, TextBox textBox, string textBoxName)
        {
            int maxValue = 0;
            if (textBoxName == "tbDay")
            {
                maxValue = 3;
            }
            if (textBoxName == "tbMonth")
            {
                maxValue = 1;
            }

            if ((!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) || e.KeyChar == '.')
            {
                e.Handled = true;
            }
            if (textBoxName != "tbYear" && char.IsDigit(e.KeyChar) && Convert.ToInt16(e.KeyChar.ToString()) > maxValue && textBox.Text.Length < 1)
            {
                textBox.Text = "0" + e.KeyChar.ToString();
            }
        }


        /// <summary>
        /// Gets the path for exe.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static string GetPathForExe(string fileName)
        {
            RegistryKey localMachine = Registry.LocalMachine;
            RegistryKey fileKey = localMachine.OpenSubKey(string.Format(@"{0}\{1}", keyBase, fileName));
            object result = null;
            if (fileKey != null)
            {
                result = fileKey.GetValue(string.Empty);
            }
            fileKey.Close();

            return (string)result;
        }

        public static bool IsFoundDevelopmentKey()
        {
            try
            {
                if (Registry.LocalMachine.OpenSubKey(GlobalsCommon.ProductionKeyConstant.PATH + GlobalsCommon.ProductionKeyConstant.ISTL_KEY) != null)
                {
                    return true;
                }
                else if (Registry.LocalMachine.OpenSubKey(GlobalsCommon.ProductionKeyConstant.PATH_64 + GlobalsCommon.ProductionKeyConstant.ISTL_KEY) != null)
                {
                    return true;
                }
            }
            catch (Exception x)
            {
                logger.Error(x);
            }

            return false;
        }

        public static byte[] ImageToByte(Image img)
        {
            try
            {
                ImageConverter converter = new ImageConverter();
                return (byte[])converter.ConvertTo(img, typeof(byte[]));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Image ByteToImage(byte[] raw)
        {
            try
            {
                if (raw == null) return null;

                return Image.FromStream(new MemoryStream(raw));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static void ByteToFile(String fileName, byte[] byteArray)
        {
            try
            {
                if (byteArray != null)
                {
                    using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                    {
                        fs.Write(byteArray, 0, byteArray.Length);
                    }
                }                
            }
            catch (Exception ex)
            {               
            }
        }

        public static MaterialComboBox GeneralComboBoxFormat(MaterialComboBox cmb)
        {
            cmb.DisplayMember = "Value";
            cmb.ValueMember = "Key";
            cmb.SelectedIndex = -1;

            return cmb;
        }

        public static SuggestComboBox SuggestComboBoxFormat(SuggestComboBox cmb, int type)
        {
            cmb.DisplayMember = "Value";
            cmb.ValueMember = "Key";

            if (type == 0) // Dictionary where key is string
            {
                cmb.PropertySelector = collection => collection.Cast<KeyValuePair<string, string>>().Select(p => p.Value);
            }
            else if (type == 1) // Dictionary where key is int
            {
                cmb.PropertySelector = collection => collection.Cast<KeyValuePair<int, string>>().Select(p => p.Value);
            }

            cmb.FilterRule = (item, text) => item.Trim().ToLower().Contains(text.Trim().ToLower());
            cmb.SuggestListOrderRule = s => s;
            cmb.SelectedIndex = -1;

            return cmb;
        }
    }
}
