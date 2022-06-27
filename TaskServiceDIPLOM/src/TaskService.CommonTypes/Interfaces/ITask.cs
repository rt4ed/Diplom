using Microsoft.Extensions.Logging;
using TaskService.CommonTypes.Classes;

namespace TaskService.CommonTypes.Interfaces
{
    /// <summary>
    /// Interface for plugin task
    /// </summary>
    public interface ITask
    {
        string Name { get; }

        /// <summary>
        /// DTO for sheduler
        /// </summary>
        TaskDTO ServiceTask { get; set; }

        /// <summary>
        /// Main method for plugins
        /// </summary>
        /// <returns>
        /// Returns TaskResult with errors/warnings and results of task execution
        /// </returns>
        TaskResult Execute(CancellationToken token, ILogger logger);
    }
}
