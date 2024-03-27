using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.FP
{
    public interface IFpControl
    {
        void OnGetImage(Bitmap image);
        void OnComplete(bool response);
        void SetMessage(String msg);
    }
}
