using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ISTL.COMMON;
using ISTL.RAB.Controllers.New.Home.Setup;

namespace ISTL.RAB.View.New.Setup
{
    public partial class BattalionSettingsUserControl : ViewUserControl
    {
        public BattalionSettingsUserControl()
        {
            InitializeComponent();
        }

        private void btnBattalionSettings_Click(object sender, EventArgs e)
        {
            ((BattalionSettingsController)controller).BattalionSettings();
        }

        private void btnServiceSettings_Click(object sender, EventArgs e)
        {
            ((BattalionSettingsController)controller).ServiceSettings();
        }
    }
}
