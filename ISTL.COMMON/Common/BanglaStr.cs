using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NLog;

namespace ISTL.COMMON.Common
{
    public class BanglaStr
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static Dictionary<string, string> banglaDigit = null;

        public static Dictionary<string, string> GetBanglaDigits()
        {
            if (banglaDigit == null)
            {
                banglaDigit = new Dictionary<string, string>();
                banglaDigit.Add("0", "\u09E6");
                banglaDigit.Add("1", "\u09E7");
                banglaDigit.Add("2", "\u09E8");
                banglaDigit.Add("3", "\u09E9");
                banglaDigit.Add("4", "\u09EA");
                banglaDigit.Add("5", "\u09EB");
                banglaDigit.Add("6", "\u09EC");
                banglaDigit.Add("7", "\u09ED");
                banglaDigit.Add("8", "\u09EE");
                banglaDigit.Add("9", "\u09EF");
            }
            return banglaDigit;
        }

        public static string ConvertToBanglaNumber(string inputStr)
        {
            if (inputStr == null || inputStr.Length <= 0)
            {
                return "";
            }

            string banglaStr = "";
            inputStr = inputStr.Trim();
            for (int i = 0; i < inputStr.Length; i++)
            {
                string charAtPos = inputStr[i].ToString();
                if (Regex.IsMatch(charAtPos, @"[0-9]"))
                {
                    try
                    {
                        banglaStr += GetBanglaDigits()[charAtPos];
                    }
                    catch (Exception x)
                    {
                        logger.Error("Cannot Convert To BanglaDigit: " + x);
                    }
                }
                else
                {
                    banglaStr += charAtPos;
                }
            }

            return banglaStr;
        }

        public static string ConvertBanglaToEngNumber(string inputStr)
        {
            if (inputStr == null || inputStr.Length <= 0)
            {
                return "";
            }

            string engStr = "";
            for (int i = 0; i < inputStr.Length; i++)
            {
                string charAtPos = inputStr[i].ToString();
                var key = GetBanglaDigits().FirstOrDefault(x => x.Value == charAtPos);
                engStr += key.Key != null ? key.Key : charAtPos;
            }
            return engStr;
        }
    }
}
