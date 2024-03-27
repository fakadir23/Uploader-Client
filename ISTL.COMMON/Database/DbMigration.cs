using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using ISTL.COMMON.Entity;
using System.Data;

namespace ISTL.COMMON.Database
{
    public class DbMigration
    {
        public Logger logger = LogManager.GetCurrentClassLogger();
        private DbOperation dbOperation = null;
        private DbOperation dbOperationMig = null;

        public DbMigration(string dbName, string password, string dbNameMig, string passwordMig)
        {
            dbOperation = new DbOperation(dbName, password);
            dbOperationMig = new DbOperation(dbNameMig, passwordMig);
        }

        public bool MigrateDb()
        {
            try
            {
                dbOperation.OpenDbConnection();
                dbOperationMig.OpenDbConnection();

                if (GetMaxVersionFromRabCas() > GetMaxVersionFromMig())
                {
                    // Data DB is at a higher version than MigrationDB, so cannot continue
                    return false;
                }

                List<string> tableList = GetDistinctTables();
                foreach (string table in tableList)
                {
                    MigrateExistingTables(table);
                }
                return true;
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
                throw x;
            }
            finally
            {
                try
                {
                    dbOperation.CloseDbConnection();
                    dbOperationMig.CloseDbConnection();
                }
                catch (Exception e)
                {
                    logger.Debug("Error while closing DB connection. ERROR: " + e.Message);
                }
            }
        }

        private void MigrateExistingTables(string tableName)
        {
            TableVersion version = GetTableVersion(tableName);
            if (version.Version == 0)
            {
                InsertVersion(version.Name, version.Version);
            }

            List<Migration> migrationList = GetMigrationList(version.Name, version.Version);
            foreach (Migration migration in migrationList)
            {
                dbOperation.ExecuteQuery(migration.Query);
                UpdateVersion(migration.TargetTable, migration.Version);
            }
        }

        private TableVersion GetTableVersion(string tableName)
        {
            TableVersion version = new TableVersion(tableName, 0);
            string wherePart = String.Format("table_name='{0}'", tableName);
            string sql = String.Format("SELECT * FROM version WHERE {0}", wherePart);
            DataTable dataTable = dbOperation.GetDataTable(sql);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                version.Version = Convert.ToInt32(dataRow["table_version"]);
            }
            return version;
        }

        private List<Migration> GetMigrationList(string targetTable, int version)
        {
            List<Migration> migrationList = new List<Migration>();
            string wherePart = String.Format("target_table='{0}' and version > {1}", targetTable, version);
            string orderBy = "version asc";
            string sql = String.Format("SELECT * FROM scripts where {0} ORDER BY {1}", wherePart, orderBy);
            DataTable dataTable = dbOperationMig.GetDataTable(sql);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                int updatedVersion = Convert.ToInt32(dataRow["version"]);
                string query = (string)dataRow["query"];
                migrationList.Add(new Migration(targetTable, updatedVersion, query));
            }
            return migrationList;
        }


        private void InsertVersion(string tableName, int version)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("table_name", tableName);
            data.Add("table_version", version);
            dbOperation.InsertData("version", data);
        }

        private void UpdateVersion(string tableName, int version)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("table_version", version);

            //string wherePart = String.Format(" table_name='{0}'", tableName);
            //dbOperation.Update("version", data, wherePart);
            string wherePart = "table_name=?";
            dbOperation.Update("version", data, wherePart, new object[] { tableName });
        }

        private List<string> GetDistinctTables()
        {
            List<string> tableList = new List<string>();
            string sql = String.Format("SELECT DISTINCT target_table FROM scripts");
            DataTable dataTable = dbOperationMig.GetDataTable(sql);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                string distinctTable = (string)dataRow["target_table"];
                tableList.Add(distinctTable);
            }
            return tableList;
        }


        private int GetMaxVersionFromMig()
        {
            int maxVersion = 0;
            string sql = String.Format("SELECT max(version) AS max_version FROM scripts");
            DataTable dataTable = dbOperationMig.GetDataTable(sql);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                maxVersion = Convert.ToInt32(dataRow["max_version"]);
            }
            return maxVersion;
        }

        private int GetMaxVersionFromRabCas()
        {
            int maxVersion = 0;
            string sql = String.Format("SELECT max(table_version) AS max_version FROM version");
            DataTable dataTable = dbOperation.GetDataTable(sql);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                if (!dataRow.IsNull("max_version"))
                {
                    maxVersion = Convert.ToInt32(dataRow["max_version"]);
                }
            }
            return maxVersion;
        }
    }
}
