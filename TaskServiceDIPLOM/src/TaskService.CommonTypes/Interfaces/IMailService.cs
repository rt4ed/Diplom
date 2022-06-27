using TaskService.CommonTypes.Classes;

namespace TaskService.CommonTypes.Interfaces
{
    public interface IMailService
    {
        void SendMail(string taskName, int taskId, ICollection<TaskWarning> taskWarnings);

        void UpdateSettings();
    }
}
