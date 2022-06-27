using Microsoft.Extensions.Logging;
using System.Net.Mail;
using TaskService.CommonTypes.Interfaces;

namespace TaskService.CommonTypes.Classes
{
    public class MailService : IMailService
    {
        private readonly ILogger<MailService> _log;
        private readonly IMailServiceDataManager _dataManager;

        private ICollection<MailSettings> _settings;

        public MailService(ILogger<MailService> log, IMailServiceDataManager dataManager)
        {
            _log = log;
            _dataManager = dataManager;
        }

        public void UpdateSettings() => _settings = _dataManager.GetMailSettings();

        public void SendMail(string taskName, int taskId, ICollection<TaskWarning> taskWarnings)
        {
            if(_settings is null)
                _settings = _dataManager.GetMailSettings();

            var message = GetMessage(taskName, taskId, taskWarnings);

            foreach (var settings in _settings)
            {
                var smtp = new SmtpClient();
                message.From = new MailAddress(settings.From);
                message.To.Add(new MailAddress(settings.To));
                message.Subject = settings.Subject;
                smtp.Port = 25;
                smtp.Host = settings.SmtpHost;
                smtp.UseDefaultCredentials = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                try
                {
                    smtp.Send(message);
                }
                catch (Exception ex)
                {
                    _log.LogError(ex, $"Error while sending message to {settings.To}: {ex.Message}");
                }
            }
        }

        private MailMessage GetMessage(string taskName, int taskId, ICollection<TaskWarning> taskWarnings)
        {
            var message = new MailMessage();

            message.Body = $"List of warnings from {taskName} with Id {taskId}:"
                + Environment.NewLine
                + string.Join(Environment.NewLine, taskWarnings);

            return message;
        }
    }
}
