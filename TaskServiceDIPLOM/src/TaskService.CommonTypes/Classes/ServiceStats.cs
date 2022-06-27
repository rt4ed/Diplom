namespace TaskService.CommonTypes.Classes
{
    public class ServiceStats
    {
        public int TaskID { get; set; }

        public string TaskResult
        {
            get {
                switch (this.ProcessID)
                {
                    case Enums.ProcessID.OK: return "Task successfully completed";
                    case Enums.ProcessID.Warning: return "Task completed with warnings";
                    case Enums.ProcessID.Error: return "Task completed with errors";
                    case Enums.ProcessID.Cancelled: return "Task was cancelled";
                    default: return "Nothing to do here";
                }
            }
        }

        public DateTime TaskStartTime { get; set; } = DateTime.Now;

        public DateTime TaskEndTime { get; set; }

        public int AffectedRows { get; set; }

        public int ErrorCount { get; set; } = 0;

        public int WarningCount { get; set; } = 0;

        public Enums.ProcessID ProcessID { get; set; } = Enums.ProcessID.OK;
    }
}
