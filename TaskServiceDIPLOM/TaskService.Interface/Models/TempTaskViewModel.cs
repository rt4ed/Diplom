namespace TaskService.Interface.Models
{
    public class TempTaskViewModel
    {
        public ICollection<TempTask>? GetTempTaskDTOs { get; set; }

        public ICollection<ServiceTask>? GetTaskDTOs { get; set; }
    }
}
