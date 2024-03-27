using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.IRIS
{
    public abstract class IIrisEngine
    {
        public abstract bool OpenDevice(string pos);        
        public abstract void CloseDevice();
        public abstract void StartCapture(string pos);
    }
}
