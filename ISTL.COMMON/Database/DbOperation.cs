using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.Collections;
using NLog;

namespace ISTL.COMMON.Database
{
    public class DbOperation
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private SQLiteConnection sqLiteConnection = null;

        public DbOperation(string dbName, string dbPass)
        {
            // If DB name is not a path (i.e. just a filename), then append the original application path
            if (!dbName.Contains(@"\"))
            {
                dbName = Utils.DefaultDataPath() + "\\" + dbName;
            }

            // **************
            // **************
            // TODO: Database should be password protected
            // **************
            // **************
            string source = String.Format("Data Source={0}", dbName);
            sqLiteConnection = new SQLiteConnection(source);
        }

        public SQLiteConnection GetSqliteConnection()
        {
            return sqLiteConnection;
        }

        public int ExecuteQuery(string sql)
        {
            try
            {
                SQLiteCommand sqLiteCommand = new SQLiteCommand(sqLiteConnection);
                sqLiteCommand.CommandText = sql;
                return sqLiteCommand.ExecuteNonQuery();
            }
            catch (Exception x)
            {
                logger.Error(x.Message);
                throw x;
            }
        }

        public bool ExecuteScalar(string sql)
        {
            object value = null;
            SQLiteCommand sQLiteCommand = null;
            try
            {
                sQLiteCommand = new SQLiteCommand(sqLiteConnection);
                sQLiteCommand.CommandText = sql;

                value = sQLiteCommand.ExecuteScalar();
                if (value != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception x)
            {
                logger.Error(x.Message);
                throw x;
            }
        }

        public int ExecuteScalarReturnInt(string sql)
        {
            object value = null;
            SQLiteCommand sQLiteCommand = null;
            try
            {
                sQLiteCommand = new SQLiteCommand(sqLiteConnection);
                sQLiteCommand.CommandText = sql;

                value = sQLiteCommand.ExecuteScalar();
                if (value != null)
                {
                    return Convert.ToInt32(value);
                }
                return -1;
            }
            catch (Exception x)
            {
                logger.Error(x.Message);
                throw x;
            }
        }

        public string ExecuteScalarReturnStr(string sql)
        {
            object value = null;
            SQLiteCommand sQLiteCommand = null;
            try
            {
                sQLiteCommand = new SQLiteCommand(sqLiteConnection);
                sQLiteCommand.CommandText = sql;

                value = sQLiteCommand.ExecuteScalar();
                if (value != null)
                {
                    return Convert.ToString(value);
                }
                return null;
            }
            catch (Exception x)
            {
                logger.Error(x.Message);
                throw x;
            }
        }

        public bool InsertData(string tableName, Dictionary<string, object> data)
        {
            try
            {
                SQLiteCommand commands = new SQLiteCommand(sqLiteConnection);
                string sql = String.Format("INSERT OR IGNORE INTO {0}({1}) VALUES({2})",
                    tableName,
                    string.Join(", ", data.Keys.ToArray()),
                    string.Join(", ", Enumerable.Repeat("?", data.Count).ToArray()));

                commands.CommandText = sql;
                foreach (object value in data.Values)
                {
                    SQLiteParameter sQLiteParameter = new SQLiteParameter();
                    sQLiteParameter.Value = value;
                    commands.Parameters.Add(sQLiteParameter);
                }

                if (commands.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception x)
            {
                logger.Error(x.Message);
                throw x;
            }
        }
       
        public bool Update(string tableName, Dictionary<string, object> data, string wherePart, object[] parameters)
        {
            try
            {
                SQLiteCommand commands = new SQLiteCommand(sqLiteConnection);

                string sql = String.Format("UPDATE {0} SET {1} WHERE {2}",
                    tableName,
                    string.Join("=?, ", data.Keys.ToArray()) + "=?",
                    wherePart);
                commands.CommandText = sql;

                foreach (object value in data.Values)
                {
                    SQLiteParameter sQLiteParameter = new SQLiteParameter();
                    sQLiteParameter.Value = value;
                    commands.Parameters.Add(sQLiteParameter);
                }

                foreach (object param in parameters)
                {
                    SQLiteParameter sQLiteParameter = new SQLiteParameter();
                    sQLiteParameter.Value = param;
                    commands.Parameters.Add(sQLiteParameter);
                }

                if (commands.ExecuteNonQuery() > 0)
                {
                    commands.Dispose();
                    return true;
                }
            }
            catch (Exception x)
            {
                logger.Error(x.StackTrace.ToString());
                throw x;
            }

            return false;
        }

        public bool Update(string tableName, Dictionary<string, object> data, string wherePart)
        {
            string fields = "";
            List<object> updatedValue = new List<object>();
            if (data != null && data.Count > 0)
            {
                int i = 1;
                foreach (KeyValuePair<string, object> value in data)
                {
                    fields += String.Format(" {0} = {1},", value.Key.ToString(), "@param" + i);
                    updatedValue.Add(value.Value);
                    i++;
                }
                fields = fields.Substring(0, fields.Length - 1);
            }
            try
            {
                SQLiteCommand commands = new SQLiteCommand(sqLiteConnection);
                string sql = String.Format("UPDATE {0} SET {1} WHERE {2};", tableName, fields, wherePart);
                commands.CommandText = sql;
                commands.CommandType = CommandType.Text;

                int j = 1;
                foreach (object obj in updatedValue)
                {
                    string param = "@param" + (j++);
                    commands.Parameters.Add(new SQLiteParameter(param, obj));
                }
                if (commands.ExecuteNonQuery() > 0)
                {
                    commands.Dispose();
                    return true;
                }
            }
            catch (Exception x)
            {
                logger.Error(x.StackTrace.ToString());
                throw x;
            }

            return false;
        }

        public DataTable GetDataTable(string sql)
        {
            DataTable dataTable = new DataTable();
            try
            {
                SQLiteCommand sQLiteCommand = new SQLiteCommand(sqLiteConnection);
                sQLiteCommand.CommandText = sql;
                SQLiteDataReader reader = sQLiteCommand.ExecuteReader();
                dataTable.Load(reader);
                reader.Close();
            }
            catch (Exception x)
            {
                logger.Error(x.Message);
                throw x;
            }
            return dataTable;
        }

        public long GetRowCount(string sql)
        {
            try
            {
                SQLiteCommand sQLiteCommand = new SQLiteCommand(sqLiteConnection);
                sQLiteCommand.CommandText = sql;
                return Convert.ToInt64(sQLiteCommand.ExecuteScalar());
            }
            catch (Exception x)
            {
                logger.Error(x.Message);
                throw x;
            }
        }

        public bool Delete(string tableName, string where)
        {
            Boolean isDeleted = true;
            try
            {
                ExecuteQuery(String.Format("delete from {0} where {1};", tableName, where));
            }
            catch (Exception x)
            {
                logger.Error(x.Message);
                isDeleted = false;
            }
            return isDeleted;
        }

        public bool DeleteAll(string tableName)
        {
            Boolean isDeleted = true;
            try
            {
                ExecuteQuery(String.Format("delete from {0};", tableName));
            }
            catch (Exception x)
            {
                logger.Error(x.Message);
                isDeleted = false;
            }
            return isDeleted;
        }

        private Object _locker = new object();

        public void OpenDbConnection()
        {
            try
            {
                lock (_locker)
                {
                    if (sqLiteConnection.State == ConnectionState.Closed)
                    {
                        sqLiteConnection.Open();
                    }
                    else
                    {
                        sqLiteConnection.Close();
                        sqLiteConnection.Open();
                    }
                }
            }
            catch (Exception x)
            {
                logger.Error(x.Message);
                throw x;
            }
        }

        public void CloseDbConnection()
        {
            try
            {
                lock (_locker)
                {
                    if (sqLiteConnection.State == ConnectionState.Open)
                    {
                        sqLiteConnection.Close();
                    }
                    else
                    {
                        sqLiteConnection.Open();
                        sqLiteConnection.Close();
                    }
                }
            }
            catch (Exception x)
            {
                logger.Error(x.Message);
                throw x;
            }
        }
    }
}
