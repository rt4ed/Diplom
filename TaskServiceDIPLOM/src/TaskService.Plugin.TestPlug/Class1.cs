using Microsoft.Extensions.Logging;
using TaskService.CommonTypes.Classes;
using TaskService.CommonTypes.Interfaces;

namespace TaskService.Plugin.TestPlug
{
    public class Class1 : ITask
    {
        public string Name => "TestWorkerPlugin";

        public TaskDTO ServiceTask { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        public TaskResult Execute(CancellationToken token, IMailService mailService, ILogger logger)
        {
            logger.LogInformation("I'm test worker, it is working");
            Thread.Sleep(1000);

            return new TaskResult();
        }
    }
}