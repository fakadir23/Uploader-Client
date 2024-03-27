using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB
{
    public enum HttpResponseStatus
    {
        OK = 200,
        INACTIVE = 100,
        GENERAL_ERROR = 200,
        BEC_NID_IDENTIFICATION_ERROR = 500
    }
}
