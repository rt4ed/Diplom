using TaskService.CommonTypes.Interfaces;

namespace TaskService.Interfaces
{
    public interface IPluginService
    {
        ICollection<ITask> GetPlugins();

        void StartPluginListener(CancellationToken token);
    }
}
