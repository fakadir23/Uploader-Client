using System.ComponentModel;
using System.Windows.Forms;
using NLog;

namespace ISTL.RAB.View
{
    public partial class ProcessingDialog : Form
    {
        // Drop shadow souce code, got from:
        // http://www.codeproject.com/Articles/19277/Let-Your-Form-Drop-a-Shadow

        // Define the CS_DROPSHADOW constant
        private const int CS_DROPSHADOW = 0x00020000;

        // Override the CreateParams property
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        Logger logger = LogManager.GetCurrentClassLogger();
        
        public ProcessingDialog()
        {
            InitializeComponent();
        }

        public delegate void CallbackMethod();
        private CallbackMethod callbackMethod;

        /// <summary>
        /// IMPORTANT:
        /// Since this method uses a thread to execute the callback method, be careful
        /// when accessing UI objects from the callback method, because it will cause an
        /// exception. If UI objects need to be modified, then use the view's Invoke method
        /// to let the view's UI thread make the modifications.
        /// </summary>
        /// <param name="callback"></param>
        public static void Run(CallbackMethod callback)
        {
            ProcessingDialog dialog = new ProcessingDialog();
            dialog.callbackMethod = callback;
            dialog.backgroundWorker.RunWorkerAsync();
            dialog.ShowDialog();
            dialog.Dispose();
        }

        public static void Run(string strProcessing, string processingMsg, CallbackMethod callback)
        {
            ProcessingDialog dialog = new ProcessingDialog();
            dialog.StartPosition = FormStartPosition.CenterScreen;
            dialog.lblProcessing.Text = strProcessing;
            dialog.callbackMethod = callback;
            dialog.backgroundWorker.RunWorkerAsync();
            dialog.ShowDialog();
            dialog.Dispose();
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // There was an unhandled error during background operation
                logger.Error("There was an unexpected error during background operation.\n" + e.Error.Message);
                ErrorMessageBox.ShowError("There was an unexpected error.", e.Error);
            }
            this.DialogResult = DialogResult.OK;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            callbackMethod();
        }
    }
}
