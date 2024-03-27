using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ISTL.COMMON
{
    /// <summary>
    /// This should have been an abstract class, but not because 
    /// of the limitations of Windows forms designer problem
    /// </summary>
    public class ViewController
    {
        protected IView myview;
        protected ViewController parent;
        private Dictionary<string, ViewController> controllerList = new Dictionary<string, ViewController>();

        public DialogResult ShowView()
        {
            DialogResult result = ((Form)myview).ShowDialog();
            RemoveView();
            return result;
        }

        protected void SetView(IView v) 
        {
            myview = v;
        }


        internal IView GetView()
        {
            return myview;
        }

        /// <summary>
        /// Set the parent controller, if this is a child.
        /// Views of child controllers are assumed to be
        /// UserControls or similar, but should not be Forms
        /// </summary>
        /// <param name="c"></param>
        public void SetParent(ViewController c)
        {
            parent = c;
        }

        /// <summary>
        /// Get the unique name of the controller.
        /// This is meant to be overriden.
        /// Should be implemented by all child controllers.
        /// </summary>
        /// <returns></returns>
        public virtual string GetName() 
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Triggered by the Views load event.
        /// </summary>
        public virtual void OnLoad() { }

        /// <summary>
        /// Calls all child controller's IsCancelClosing()
        /// This should handle closing confirmations as well.
        /// 
        /// NOTE: Ideally, the overriding method should set DialogResult
        /// to initiate the form closing (if this is the parent controller).
        /// </summary>
        /// <returns>TRUE: If closing should be cancelled</returns>
        public virtual bool IsCancelClosing()
        {
            foreach (KeyValuePair<string, ViewController> pair in controllerList)
            {
                ViewController controller = pair.Value;
                if (controller.IsCancelClosing())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Also calls all child controller's OnClosing()
        /// Since Child controller views are assumed to UserControls or similar,
        /// it will not have any default closing event.
        /// </summary>
        public virtual void OnClosing()
        {
            // Will not write code to call child controller's OnClosing in this method because that
            // is called from RemoveChild method.

            //foreach (KeyValuePair<string, ViewController> pair in controllerList)
            //{
            //    ViewController controller = pair.Value;
            //    controller.OnClosing();
            //    controller.myview.OnClosing();
            //}
            RemoveAllChild();
            //RemoveView();
        }

        /// <summary>
        /// Also calls all child controller's OnClosed()
        /// Since Child controller views are assumed to UserControls or similar,
        /// it will not have any default closing event.
        /// </summary>
        public virtual void OnClosed() 
        {
            // Will not write code to call child controller's OnClosed in this method because that
            // was called from RemoveChild method.

            //foreach (KeyValuePair<string, ViewController> pair in controllerList)
            //{
            //    ViewController controller = pair.Value;
            //    controller.OnClosed();
            //    controller.myview.OnClosed();
            //}
        }

        /// <summary>
        /// This is meant to be overriden.
        /// Should be implemented by all controllers who have decendents.
        /// </summary>
        /// <param name="name">Unique name of the child controller</param>
        public virtual void AddChild(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Views of child controllers are assumed to be
        /// UserControls or similar, but should not be Forms
        /// </summary>
        /// <param name="c"></param>
        protected void AddChild(ViewController c)
        {
            controllerList.Add(c.GetName(), c);
            c.SetParent(this);
            myview.AddControl((ViewUserControl)c.GetView());
        }

        /// <summary>
        /// Remove controller and all its child controllers
        /// </summary>
        /// <param name="name">Unique name of controller</param>
        protected void RemoveChild(string name)
        {
            ViewController controller = GetChild(name);
            RemoveChild(controller);
        }

        /// <summary>
        /// Remove controller and all its decendants
        /// </summary>
        /// <param name="controller"></param>
        protected void RemoveChild(ViewController controller)
        {
            // Removes all child controls and controllers
            controller.RemoveAllChild();
            // Allow cleanup by calling the child controller/view's OnClosing/OnClosed methods
            controller.OnClosing();
            controller.GetView().OnClosing();
            controller.OnClosed();
            controller.GetView().OnClosed();
            // Removes the child control from myview's panel
            myview.RemoveControl(controller.GetName());
            // Dispose the child control
            controller.RemoveView();
            // Remove controller from child list
            controllerList.Remove(controller.GetName());
        }

        /// <summary>
        /// Dispose the view of the current controller.
        /// Only required if current controller is a parent.
        /// </summary>
        protected void RemoveView()
        {
            ((ContainerControl)myview).Dispose();
        }

        protected ViewController GetChild(string name)
        {
            return controllerList[name];
        }

        /// <summary>
        /// Removes all controller decendants
        /// This method will also dispose all controls in the panel
        /// </summary>
        public void RemoveAllChild()
        {
            // Since removing each child also updates the controller list
            // need to create a copy of the list to prevent error during
            // remove loop
            List<ViewController> list = new List<ViewController>();
            foreach (KeyValuePair<string, ViewController> pair in controllerList)
            {
                ViewController controller = pair.Value;
                list.Add(controller);
            }

            foreach (ViewController controller in list)
            {
                // Removes the child controller, its control and decendants
                RemoveChild(controller);
            }
        }

    }
}
