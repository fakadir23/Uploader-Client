using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISTL.COMMON.Entity
{
    public class TableVersion
    {
        private string name;
        private int version;

        public TableVersion(string name, int version)
        {
            Name = name;
            Version = version;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Version
        {
            get { return version; }
            set { version = value; }
        }
    }
}
