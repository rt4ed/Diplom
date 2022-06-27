using TaskService.CommonTypes.Interfaces;

namespace TaskService.Interfaces
{
    public interface IPluginLoader
    {
        ICollection<ITask> LoadPlugins(string[] pluginPaths);
    }
}
