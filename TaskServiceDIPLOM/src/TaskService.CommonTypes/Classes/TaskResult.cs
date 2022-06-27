using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskService.CommonTypes.Classes
{
    /// <summary>
    /// All plugins return TaskResult
    /// </summary>
    public class TaskResult
    {
        public TaskResult() { }

        public TaskResult(bool cancelled) { IsCancelled = cancelled; }

        public ICollection<TaskWarning> TaskWarnings { get; set; } = new List<TaskWarning>();

        public void AddWarning(string message, bool IsCritical, int? recordId = null)
        {
            TaskWarnings.Add(new TaskWarning
            {
                IsCritical = IsCritical,
                Message = recordId.HasValue ? string.Join('.', recordId, message) : message,
            });
        }

        public bool IsCancelled { get; set; }

        public bool IsError => TaskWarnings.Any(x => x.IsCritical);

        public int AffectedRows { get; set; }
    }
}
