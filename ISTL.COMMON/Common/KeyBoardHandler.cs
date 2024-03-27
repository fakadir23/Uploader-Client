using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using NLog;

namespace ISTL.COMMON.Common
{
    public class KeyBoardHandler
    {
        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        private const int KEYEVENTF_EXTENDEDKEY = 0x1;
        private const int KEYEVENTF_KEYUP = 0x2;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Disables the caps lock if it's on state and led light will be off.
        /// </summary>
        public static void DisableCapsLock()
        {
            if (Control.IsKeyLocked(Keys.CapsLock))
            {
                logger.Debug("Caps Lock key is ON.  We'll turn it off");
                keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
                keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (UIntPtr)0);
            }
        }

        public static void HandleKeyBoardForBangla(TextBox textBox, KeyPressEventArgs e) 
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            if (char.IsDigit(e.KeyChar))
            {
                textBox.AppendText(BanglaStr.ConvertToBanglaNumber(e.KeyChar.ToString()));
                e.Handled = true;
            }
        }
    }
}
