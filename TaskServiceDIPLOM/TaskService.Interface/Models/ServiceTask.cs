using System.ComponentModel.DataAnnotations;

namespace TaskService.Interface.Models
{
    public class ServiceTask
    {
        [Key]
        public int? TaskID { get; set; }//13-(1)

        public int? TaskType { get; set; }//13-(1)

        public bool? IsEnabled { get; set; }//13+(1)

        public int? Branch { get; set; }//13+(1)

        public string? TaskName { get; set; }//13-(1)

        public string? Dependency { get; set; } = string.Empty;//13+(1)

        public DateTime? LastWorkTime { get; set; }//13-(1)

        public DateTime? TaskStartTime { get; set; }//13+(1)

        public DateTime? TaskEndTime { get; set; }//13+(1)

        public string? FileMask { get; set; }//13+(2)

        public string? FilePath { get; set; } = string.Empty;//13+(2)

        public string? Url { get; set; } = string.Empty;//1+(3)

        public int? FieldsCount { get; set; }//3+(2)

        public string? FieldsSeparator { get; set; } = "#";//3+(2)

        public string? ModifiedBy { get; set; }

        public string? AuthoriziedBy { get; set; }

        public string? Params { get; set; } = string.Empty;//похуй

        public bool? ManualStart { get; set; }//13+(3)
    }
}
