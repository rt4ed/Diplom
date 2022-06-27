namespace TaskService.Interface.Models
{
    public class TaskViewModel
    {
        public ICollection<ServiceTask>? GetTaskDTOs { get; set; }

        public ICollection<TempTask>? GetTempTaskDTOs { get; set; }
    }
}
