using TaskService.CommonTypes.Classes;

namespace TaskService.API.Interfaces
{
    public interface ITaskDataManager
    {
        ICollection<TaskDTO> GetTasks();

        void SetLastWorkTime(DateTime date, int taskId);

        void InsertStats(ServiceStats stats);

        void GetStats(DateTime date);
    }
}
