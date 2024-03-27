using System;
using System.Collections;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel;

namespace ISTL.COMMON.CommandManager
{
    // Command Executor base class
    public abstract class CommandExecutor
    {
        protected Hashtable hashInstances = new Hashtable();

        public virtual void InstanceAdded(object item, Command cmd)
        {
            hashInstances.Add(item, cmd);
        }

        public virtual void InstanceRemoved(object item)
        {
            hashInstances.Remove(item);
        }

        protected Command GetCommandForInstance(object item)
        {
            return hashInstances[item] as Command;
        }

        // Interface for derived classed to implement
        public abstract void Enable(object item, bool bEnable);
        public abstract void Check(object item, bool bCheck);
    }

    // Menu command executor
    public class MenuCommandExecutor : CommandExecutor
    {
        public override void InstanceAdded(object item, Command cmd)
        {
            MenuItem mi = (MenuItem)item;
            mi.Click += new System.EventHandler(menuItem_Click);

            base.InstanceAdded(item, cmd);
        }

        // State setters
        public override void Enable(object item, bool bEnable)
        {
            MenuItem mi = (MenuItem)item;
            mi.Enabled = bEnable;
        }

        public override void Check(object item, bool bCheck)
        {
            MenuItem mi = (MenuItem)item;
            mi.Checked = bCheck;
        }


        // Execution event handler
        private void menuItem_Click(object sender, System.EventArgs e)
        {
            Command cmd = GetCommandForInstance(sender);
            cmd.Execute();
        }
    }

    // Toolbar command executor
    public class ToolbarCommandExecutor : CommandExecutor
    {
        public override void InstanceAdded(object item, Command cmd)
        {
            ToolBarButton button = (ToolBarButton)item;
            ToolBarButtonClickEventHandler handler =
                new ToolBarButtonClickEventHandler(toolbar_ButtonClick);

            // Attempt to remove the handler first, in case we have already 
            // signed up for the event in this toolbar
            button.Parent.ButtonClick -= handler;
            button.Parent.ButtonClick += handler;

            base.InstanceAdded(item, cmd);
        }


        // State setters
        public override void Enable(object item, bool bEnable)
        {
            ToolBarButton button = (ToolBarButton)item;
            button.Enabled = bEnable;
        }

        public override void Check(object item, bool bCheck)
        {
            ToolBarButton button = (ToolBarButton)item;
            button.Style = ToolBarButtonStyle.ToggleButton;
            button.Pushed = bCheck;
        }

        // Execution event handler
        private void toolbar_ButtonClick(object sender,
                                            ToolBarButtonClickEventArgs args)
        {
            Command cmd = GetCommandForInstance(args.Button);
            cmd.Execute();
        }

    }

    // Menu command executor
    public class ButtonCommandExecutor : CommandExecutor
    {
        public override void InstanceAdded(object item, Command cmd)
        {
            Button button = (Button)item;

            RemoveClickEvent(button);
            button.Click += new System.EventHandler(button_Click);

            base.InstanceAdded(item, cmd);
        }

        // State setters
        public override void Enable(object item, bool bEnable)
        {
            Button button = (Button)item;
            button.Enabled = bEnable;
        }

        public override void Check(object item, bool bCheck)
        {
            Button button = (Button)item;
            button.Visible = bCheck;
        }


        // Execution event handler
        private void button_Click(object sender, System.EventArgs e)
        {
            Command cmd = GetCommandForInstance(sender);
            cmd.Execute();
        }

        private void RemoveClickEvent(Button b)
        {
            FieldInfo f1 = typeof(Control).GetField("EventClick",
                BindingFlags.Static | BindingFlags.NonPublic);
            object obj = f1.GetValue(b);
            PropertyInfo pi = b.GetType().GetProperty("Events",
                BindingFlags.NonPublic | BindingFlags.Instance);
            EventHandlerList list = (EventHandlerList)pi.GetValue(b, null);
            list.RemoveHandler(obj, list[obj]);
        }
    }

}