namespace TaskService.CommonTypes.Classes
{
    /// <summary>
    /// DTO Object for tasks
    /// </summary>
    public class TaskDTO
    {
		public int TaskID { get; set; }

		public int TaskType { get; set; }

		public bool IsEnabled { get; set; }

		public int? Branch { get; set; }

		public string? TaskName { get; set; }

		public string Dependency { get; set; } = string.Empty;

		public DateTime? LastWorkTime { get; set; }

		public DateTime TaskStartTime { get; set; }

		public DateTime TaskEndTime { get; set; }

		public string FileMask { get; set; }

		public string FilePath { get; set; } = string.Empty;

		public string Url { get; set; } = string.Empty;

		public int? FieldsCount { get; set; }

		public string FieldsSeparator { get; set; } = "#";

		public string Params { get; set; } = string.Empty;

		public string SpName { get; set; }

		public bool ManualStart { get; set; }
	}
}
