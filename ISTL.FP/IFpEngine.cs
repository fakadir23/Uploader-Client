using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.FP
{
    public abstract class IFpEngine
    {
        public abstract bool OpenDevice();
        //public abstract void RegisterCallback();
        public abstract void CloseDevice();
    }
}
