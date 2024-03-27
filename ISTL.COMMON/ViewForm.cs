using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ISTL.COMMON
{
    /// <summary>
    /// This should have been an abstract class, but not because 
    /// of the limitations of Windows forms designer problem
    /// </summary>
    public partial class ViewForm : Form, IView
    {
        // The instance will be ignored but is required for the windows forms designer
        protected ViewController controller = new ViewController();
        private Dictionary<string, Panel> panelList;

        public ViewForm()
        {
            InitializeComponent();
        }

        public void SetController(ViewController c)
        {
            controller = c;
        }

        public ViewController GetController()
        {
            return controller;
        }

        /// <summary>
        /// Associates a view of a controller to the corresponding panel
        /// </summary>
        /// <param name="controllerName">Name of controller whose view will be
        /// associated with the panel</param>
        /// <param name="panel"></param>
        public void RegisterPanel(string controllerName, Panel panel)
        {
            if (panelList == null)
            {
                panelList = new Dictionary<string, Panel>();
            }
            panelList.Add(controllerName, panel);
        }

        /// <summary>
        /// Remove the view of the controller from the corresponding panel.
        /// </summary>
        /// <param name="controllerName"></param>
        public void RemoveControl(string controllerName)
        {
            Panel panel = panelList[controllerName];
            panel.Controls.Clear();
        }

        /// <summary>
        /// Adds the User Control to a specific panel.
        /// Make sure to call RegisterPanel to associate
        /// controller views to panel.
        /// </summary>
        /// <param name="v">The control to be added</param>
        public void AddControl(ViewUserControl v)
        {
            Panel panel = panelList[v.GetController().GetName()];
            panel.Controls.Add(v);
        }

        protected virtual void OnLoad() { }

        public virtual void OnClosing() { }
        
        public virtual void OnClosed() { }

        protected virtual void View_Load(object sender, EventArgs e)
        {
            controller.OnLoad();
            this.OnLoad();
        }

        protected virtual void View_FormClosing(object sender, FormClosingEventArgs e)
        {
            controller.OnClosing();
            this.OnClosing();
        }

        protected virtual void View_FormClosed(object sender, FormClosedEventArgs e)
        {
            controller.OnClosed();
            this.OnClosed();
        }

    }
}
