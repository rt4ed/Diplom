using Dapper;
using TaskService.API.Interfaces;
using TaskService.CommonTypes.Classes;
using TaskService.CommonTypes.Sql;

namespace TaskService.API.DataManagers
{
    public class TaskDataManager : ITaskDataManager
    {
        public ICollection<TaskDTO> GetTasks() => SqlDapper.ExecuteQuerySP<TaskDTO>("[dbo].[Service_GetAllTasks]");

        public void SetLastWorkTime(DateTime date, int taskId)
        {
            var param = new DynamicParameters();

            param.Add(
                name: "@TaskId",
                value: taskId,
                dbType: System.Data.DbType.Int32,
                direction: System.Data.ParameterDirection.Input);

            param.Add(
                name: "@LastWorkDate",
                value: date,
                dbType: System.Data.DbType.DateTime,
                direction: System.Data.ParameterDirection.Input);

            SqlDapper.ExecuteNonQuerySP("[dbo].[Service_SetAsWorked]", param); 
        }

        public void InsertStats(ServiceStats stats)
        {
            string sql = "INSERT INTO [dbo].[ServiceStats]" +
                "(TaskID, TaskResult, TaskStartTime, TaskEndTime, AffectedRows, ErrorCount, WarningCount, ProcessID)" +
                "VALUES (@TaskID, @TaskResult, @TaskStartTime, @TaskEndTime, @AffectedRows, @ErrorCount, @WarningCount, @ProcessID)";

            SqlDapper.ExecuteSqlNonQuery(sql, stats);
        }

        /// <summary>
        /// NULL if all table needed
        /// </summary>
        /// <param name="date"></param>
        public void GetStats(DateTime date)
        {
            var param = new DynamicParameters();

            param.Add(
                name: "@Date",
                value: date,
                dbType: System.Data.DbType.DateTime,
                direction: System.Data.ParameterDirection.Input);

            SqlDapper.ExecuteNonQuerySP("[dbo].[Service_GetStats]", param);
        }
    }
}
