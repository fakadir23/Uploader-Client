using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISTL.LOCALE
{
    public interface ILocaleManager
    {
        string Translate(string key);
    }
}
