using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace ISTL.COMMON.Database
{
    public class DbCreateTables
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private DbOperation dbOperation = null;

        public DbCreateTables(string dbName, string dbPass)
        {
            dbOperation = new DbOperation(dbName, dbPass);
        }

        public void CreateDbTables()
        {
            CreateVersionTable();
        }

        private bool CreateVersionTable()
        {
            string sql = "CREATE TABLE IF NOT EXISTS version(" +
                            "table_name VARCHAR2(255) PRIMARY KEY NOT NULL, " +
                            "table_version INTEGER)";
            try
            {
                dbOperation.OpenDbConnection();
                int isExecuted = dbOperation.ExecuteQuery(sql);
                dbOperation.CloseDbConnection();
                if (isExecuted > 0)
                {
                    return true;
                }
            }
            catch (Exception x)
            {
                logger.Error(x.Message);
            }
            return false;
        }
    }
}
