using TaskService.CommonTypes.Classes;

namespace TaskService.CommonTypes.Interfaces
{
    public interface IMailServiceDataManager
    {
        ICollection<MailSettings> GetMailSettings();
    }
}
