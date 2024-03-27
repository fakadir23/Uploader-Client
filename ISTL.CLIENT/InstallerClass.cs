using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ISTL.RAB
{
    /// <summary>
    /// This class is required for post setup operation (using custom actions)
    /// so that the application starts up after setup is completed.
    /// 
    /// Ref: http://msdn.microsoft.com/en-us/library/d9k65z2d%28v=vs.100%29.aspx
    /// </summary>
    [RunInstaller(true)]
    public partial class InstallerClass : System.Configuration.Install.Installer
    {
        public InstallerClass()
        {
            InitializeComponent();
        }

        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);

            Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            Process.Start(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\SNSOP-TOOLS.exe");
        }
    }
}
