using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalsCommon
{
    public static class AesCryptographyConstant
    {
        public static string SECRETKEY = "@IsTlID143958164"; // must be 16 bytes
        public static string INITVECTOR = "@IsTlID143958164"; // must be 16 bytes
        public static string KEYSIZE = "128"; // Can be 192 or 256
    }

    public struct CountName
    {
        public const string ENROLLED = "ENROLLED";
        public const string ENROLLED_ERROR = "ENROLLED_ERROR";
        public const string ENROLLED_DRAFT = "ENROLLED_DRAFT";
    }

    public static class ProductionKeyConstant
    {
        public static string PATH = "Software\\";
        public static string PATH_64 = "Software\\Wow6432Node\\";
        public static string ISTL_KEY = "ISTL";
    }

    public class GlobalCultures
    {
        public static IDictionary<string, string> Cultures = new Dictionary<string, string>
        {
            {CulterKeys.EN_US, "English"},
            {CulterKeys.BN_BD, "বাংলা"}
        };

        public struct CulterKeys
        {
            public const string EN_US = "en-US";
            public const string BN_BD = "bn-BD";
        }
    }

    public struct RegistryEdit
    {
        public const string KEY_PATH = @"LOCALE-ID\ISTL";
        public const string KEY_NAME = "LOCALE";
    }
}
