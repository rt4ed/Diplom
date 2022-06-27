using System.Reflection;
using TaskService.CommonTypes.Interfaces;
using TaskService.Interfaces;

namespace TaskService.Classes
{
    /// <summary>
    /// Plugins loader, dont catch errors inside, must be used with try/catch outsdide
    /// </summary>
    public class PluginLoader : IPluginLoader
    {
        ILogger<PluginLoader> _logger;

        public PluginLoader(ILogger<PluginLoader> logger)
        {
            _logger = logger;
        }

        public ICollection<ITask> LoadPlugins(string[] pluginPaths)
        {
            _logger.LogInformation($"Started loading plugins (avalible dlls count = {pluginPaths.Length})");

            var plugins = pluginPaths.SelectMany(pluginPath =>
            {
                Assembly pluginAssembly = LoadPlugin(pluginPath);
                return CreateCommands(pluginAssembly);
            }).ToList();

            _logger.LogInformation($"Plugins loaded (count = {pluginPaths.Length})");

            return plugins;
        }

        private Assembly LoadPlugin(string relativePath) => Assembly.LoadFrom(relativePath);

        private IEnumerable<ITask> CreateCommands(Assembly assembly)
        {
            int count = 0;

            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(ITask).IsAssignableFrom(type))
                {
                    ITask result = Activator.CreateInstance(type) as ITask;
                    if (result != null)
                    {
                        count++;
                        yield return result;
                    }
                }
            }

            if (count == 0)
            {
                string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
                throw new ApplicationException(
                    $"Can't find any type which implements ITask in {assembly} from {assembly.Location}.\n" +
                    $"Available types: {availableTypes}");
            }
        }
    }
}
