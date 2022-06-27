using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskService.Interface.Models
{
    public class TempTask
    {
        [Key]
        public int? TaskID { get; set; }

        public int? TaskType { get; set; }

        public bool? IsEnabled { get; set; }

        public int? Branch { get; set; }

        public string? TaskName { get; set; }

        public string? Dependency { get; set; } = string.Empty;

        public DateTime? LastWorkTime { get; set; }

        public DateTime? TaskStartTime { get; set; }

        public DateTime? TaskEndTime { get; set; }

        public string? FileMask { get; set; }

        public string? FilePath { get; set; } = string.Empty;

        public string? Url { get; set; } = string.Empty;

        public int? FieldsCount { get; set; }

        public string? FieldsSeparator { get; set; } = "#";

        public string? ModifiedBy { get; set; }

        public string? AuthoriziedBy { get; set; }

        public string? Params { get; set; } = string.Empty;

        public bool? ManualStart { get; set; }

        [NotMapped]
        public ServiceTask? ServiceTask { get; set; }

        public ServiceTask GetServiceTask()
        {
            var serviceTask =  new ServiceTask();
            serviceTask.TaskID = TaskID;
            serviceTask.TaskType = TaskType;
            serviceTask.IsEnabled = IsEnabled;
            serviceTask.Branch = Branch;
            serviceTask.TaskName = TaskName;
            serviceTask.Dependency = Dependency;
            serviceTask.LastWorkTime = LastWorkTime;
            serviceTask.TaskStartTime = TaskStartTime;
            serviceTask.TaskEndTime = TaskEndTime;
            serviceTask.FileMask = FileMask;
            serviceTask.FilePath = FilePath;
            serviceTask.Url = Url;
            serviceTask.FieldsCount = FieldsCount;
            serviceTask.FieldsSeparator = FieldsSeparator;
            serviceTask.ModifiedBy = ModifiedBy;
            serviceTask.AuthoriziedBy = AuthoriziedBy;
            serviceTask.ManualStart = ManualStart;

            return serviceTask;
        }
    }

    
}
