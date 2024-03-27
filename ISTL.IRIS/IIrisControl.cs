using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.IRIS
{
    public interface IIrisControl
    {
        void OnGetLeftIris(Bitmap image);
        void OnGetRightIris(Bitmap image);
        void OnComplete(bool response);
        void SetMessage(String msg);
    }
}
