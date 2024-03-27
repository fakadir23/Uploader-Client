using ISTL.COMMON.Utility;
using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ISTL.LOCALE
{
    public class LocaleUtility
    {
        #region Declaration(s)
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Method(s)
        public static void SetLatestCulture(string keyPath, string keyName)
        {
            var currentCulture = LocaleGlobals.Cultures.ENGLISH;
            switch (GetCurrentLocaleMode(keyPath, keyName))
            {
                case LocaleGlobals.Cultures.BANGLA:
                    currentCulture = LocaleGlobals.Cultures.BANGLA;
                    break;
                case LocaleGlobals.Cultures.ENGLISH:
                default:
                    currentCulture = LocaleGlobals.Cultures.ENGLISH;
                    break;
            }

            try
            {
                CultureInfo cultureInfo = new CultureInfo(currentCulture);
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Application.CurrentCulture = cultureInfo;
            }
            catch (Exception x)
            {
                logger.ErrorException("There was an error when setting culture.", x);
            }
            //RegistryUtils.WriteRegistryKey(LocaleGlobals.RegistryEdit.KEY_PATH, LocaleGlobals.RegistryEdit.KEY_NAME, currentCulture);
            RegistryUtils.WriteRegistryKey(keyPath, keyName, currentCulture);
        }
        public static void SetCulture(string keyPath, string keyName, string culture)
        {
            var currentCulture = LocaleGlobals.Cultures.ENGLISH;
            try
            {
                CultureInfo cultureInfo = new CultureInfo(culture);
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                currentCulture = culture;
                Application.CurrentCulture = cultureInfo;
            }
            catch (Exception x)
            {
                logger.ErrorException("There was an error when setting culture.", x);
            }

            RegistryUtils.WriteRegistryKey(keyPath, keyName, currentCulture);
        }
        public static string GetCurrentLocaleMode(string keyPath, string keyName)
        {

            var obj = RegistryUtils.ReadRegistryKey(keyPath, keyName);

            if (obj == null) return LocaleGlobals.Cultures.ENGLISH;

            try
            {
                return (string)obj;
            }
            catch (Exception x)
            {
                logger.Error("There was an error when getting Current Culture mode value from registry.", x);
            }

            return LocaleGlobals.Cultures.ENGLISH;
        }
        #endregion
    }
}
