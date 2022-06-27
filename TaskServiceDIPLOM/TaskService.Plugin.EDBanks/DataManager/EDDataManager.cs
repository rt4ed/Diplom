using TaskService.CommonTypes.Sql;
using TaskService.Plugin.EDBanks.Models;

namespace TaskService.Plugin.CBRTasks.DataManager
{
    public class EDDataManager
    {
        public void InsertIntoTemp(EDBanksModel[] model)
        {
            string dest = "[dbo].[BIC_Dictionary_TMP]";

            var table = SqlDapper.CreateDataTable(model);
            var mapping = SqlDapper.PrepareColumnMapping(table);

            SqlDapper.BulkInsertIntoTable(table, dest, mapping);
        }

        public void ImportFromTemp() => SqlDapper.ExecuteNonQuerySP("[dbo].[Service_ED_Import]");

        public void CleartTempTable() => SqlDapper.ClearTable("[dbo].[BIC_Dictionary_TMP]");
    }
}
