using TaskService.CommonTypes.Interfaces;
using TaskService.CommonTypes.Sql;

namespace TaskService.CommonTypes.Classes
{
    public class MailServiceDataManager : IMailServiceDataManager
    {
        public ICollection<MailSettings> GetMailSettings() => SqlDapper.ExecuteQuerySP<MailSettings>("[dbo].[Service_GetMailSettings]");
    }
}
