using ISTL.COMMON.Database;
using ISTL.MODELS.DTO.Device;
using ISTL.PERSOGlobals;
using ISTL.RAB.Entity;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.DbManager
{
    public class DbDeviceManager
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private DbOperation dbOperation = null;

        public DbDeviceManager()
        {
            dbOperation = new DbOperation(Globals.Database.NAME, Globals.Database.PASSWORD);
        }

        public bool AddCamDevices(DeviceDto obj, string cat)
        {
            bool isAdded = false;
            try
            {
                DeleteDeviceByCategory(cat);
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("name", obj?.Name);
                data.Add("type", obj?.Type != null && obj?.Type.Length > 0 ? string.Join("$", obj?.Type) : null);
                data.Add("support_model", obj?.SupportedModel != null && obj?.SupportedModel.Length > 0 ? string.Join("$", obj?.Type) : null);
                data.Add("category", cat);

                dbOperation.OpenDbConnection();
                isAdded = dbOperation.InsertData("device", data);

                dbOperation.CloseDbConnection();
                return isAdded;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.Message);
                throw x;
            }
        }

        public DeviceDto GetDevice(string cat)
        {
            try
            {
                DeviceDto obj = null;

                dbOperation.OpenDbConnection();
                string sql = "SELECT * FROM device where category = '" + cat + "' LIMIT 1;";
                DataTable dataTable = dbOperation.GetDataTable(sql);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    obj = new DeviceDto();

                    obj.Name = dataRow["name"] != DBNull.Value
                               ? (string)dataRow["name"] : null;

                    obj.Type = dataRow["type"] != DBNull.Value
                               ? ((string)dataRow["type"]).Split('$') : null;

                    obj.SupportedModel = dataRow["support_model"] != DBNull.Value
                               ? ((string)dataRow["support_model"]).Split('$') : null;
                }
                dbOperation.CloseDbConnection();
                return obj;
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error("Got error when getting record for configuration.\n" + x.ToString());
                throw x;
            }
        }

        public void DeleteDeviceByCategory(string category)
        {
            try
            {
                dbOperation.OpenDbConnection();
                string sql = String.Format("category= '{0}'", category);
                dbOperation.Delete("device", sql);
                dbOperation.CloseDbConnection();
            }
            catch (Exception x)
            {
                dbOperation.CloseDbConnection();
                logger.Error(x.Message);
            }
        }
    }
}

