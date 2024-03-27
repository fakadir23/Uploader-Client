using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace ISTL.LOCALE
{
    public class LocaleManager : ILocaleManager
    {
        #region Declaration(s)
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly ResourceManager _resourceManager;
        private static ILocaleManager _localManager = null;
        #endregion

        #region Constructor(s)
        public static ILocaleManager GetInstance()
        {
            if (_localManager == null) _localManager = new LocaleManager();

            return _localManager;
        }
        private LocaleManager()
        {
            Assembly assembly = Assembly.Load("ISTL.LOCALE");
            _resourceManager = new ResourceManager("ISTL.LOCALE.Languages.Locale", assembly);
        }
        #endregion

        #region ILocaleManager
        public string Translate(string key)
        {
            try
            {
                CultureInfo cultureInfo = new CultureInfo(LocaleGlobals.CurrentCulture.NAME);
                return _resourceManager.GetString(key, cultureInfo).Replace("\\n", "\n");
            }
            catch (Exception x)
            {
                logger.ErrorException("There was an error when locale string for key " + key, x);
            }
            return "N/A";
        }
        #endregion
    }
}
