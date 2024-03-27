using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISTL.COMMON
{
    public interface IView
    {
        void SetController(ViewController c);
        ViewController GetController();

        void AddControl(ViewUserControl v);
        void RemoveControl(string controllerName);
        
        void OnClosing();
        void OnClosed();
    }
}
