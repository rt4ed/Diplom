using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskService.CommonTypes.Sql;
using TaskService.Plugin.Clients.Models;

namespace TaskService.Plugin.Clients.DataManager
{
    public class ClientDataManager
    {
        public void InsertIntoTemp(ClientsDBModel[] model)
        {
            string dest = "[dbo].[Customer_TMP]";

            var table = SqlDapper.CreateDataTable(model);
            var mapping = SqlDapper.PrepareColumnMapping(table);

            SqlDapper.BulkInsertIntoTable(table, dest, mapping);
        }

        public void ImportFromTemp() => SqlDapper.ExecuteNonQuerySP("[dbo].[Service_Clients_Import]");

        public void CleartTempTable() => SqlDapper.ClearTable("[dbo].[Customer_TMP]");
    }
}
