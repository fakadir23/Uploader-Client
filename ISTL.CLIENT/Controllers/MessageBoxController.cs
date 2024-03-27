using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ISTL.RAB.Controllers
{
    public class MessageBoxController
    {
        public static void ShowWarning(string caption, string message)
        {
            MessageBox.Show(null,
                message,
                caption,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        public static void ShowWarning(string caption, string message, Exception x)
        {
            MessageBox.Show(null,
                message + "\n" + x.Message,
                caption,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        public static void ShowInfo(string caption, string message)
        {
            MessageBox.Show(null,
                message,
                caption,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        public static void ShowError(string caption, string message)
        {
            MessageBox.Show(null,
                message,
                caption,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }


        public static void ShowError(string caption, string message, Exception x)
        {
            MessageBox.Show(null,
                message + "\n" + x.Message,
                caption,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        public static DialogResult ShowQuestionYesNo(string caption, string message)
        {
            return MessageBox.Show(null,
                message,
                caption,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
        }
    }
}
