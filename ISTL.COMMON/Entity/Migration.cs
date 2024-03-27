using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISTL.COMMON.Entity
{
    public class Migration
    {
        private string targetTable;
        private int version;
        private string query;

        public Migration(string targetTable, int version, string query)
        {
            TargetTable = targetTable;
            Version = version;
            Query = query;
        }

        public string TargetTable
        {
            get { return targetTable; }
            set { targetTable = value; }
        }

        public int Version
        {
            get { return version; }
            set { version = value; }
        }

        public string Query
        {
            get { return query; }
            set { query = value; }
        }
    }
}
