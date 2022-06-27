using System.Text.RegularExpressions;
using TaskService.CommonTypes.Classes;
using TaskService.CommonTypes.Interfaces;
using TaskService.Interfaces;

namespace TaskService.Classes
{
    /// <summary>
    /// Service for dynamic loading of plugins
    /// </summary>
    public class PluginService : IPluginService
    {
        private readonly ILogger<PluginService> _logger;
        private readonly IPluginLoader _pluginLoader;

        /// <summary>
        /// Pattern for plugins search
        /// </summary>
        private Regex _patternForPlugins = new Regex("^TaskService[.]Plugin[.][a-zA-Z.\\d]+[.]dll$");

        /// <summary>
        /// List of plugins
        /// </summary>
        private ICollection<ITask> _plugins;

        /// <summary>
        /// Plugin path + plugin datetime change
        /// </summary>
        private IDictionary<string, DateTime> _pluginsPaths;

        /// <summary>
        /// True if plugins are loading/activating
        /// False if there is no loading/activating
        /// </summary>
        private bool _blockGet = false;

        public PluginService(ILogger<PluginService> logger, IPluginLoader pluginLoader)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _pluginLoader = pluginLoader ?? throw new ArgumentNullException(nameof(pluginLoader));

            // Load plugins on Init ctor
            LoadPlugins();
        }

        public ICollection<ITask> GetPlugins()
        {
            if (!_blockGet)
                return _plugins;
            else
            {
                while (_blockGet)
                    Thread.Sleep(TaskServiceConst.DelayForBlockingExc);

                return _plugins;
            }
        }

        public void StartPluginListener(CancellationToken token)
        {
            // Second. Listen folder of plugins, and add new.
            _logger.LogInformation("Start plugin listener thread");

            while (!token.IsCancellationRequested)
            {
                try
                {
                    var tempDictionary = GetAssemblyPaths();
                    
                    if(tempDictionary.Count() != _pluginsPaths.Count() || _pluginsPaths.Except(tempDictionary).Any())
                    {
                        Lock();

                        _logger.LogInformation("Found new plugin, reloading");

                        _pluginsPaths = tempDictionary;
                        _plugins = _pluginLoader.LoadPlugins(_pluginsPaths.Keys.ToArray());

                        Unlock();

                        _logger.LogInformation("Reloading complete");
                    }

                    Thread.Sleep(TaskServiceConst.DelayForPluginsCheck);
                }
                catch { Thread.Sleep(TaskServiceConst.DelayForBlockingExc); }
            }
        }

        private void LoadPlugins()
        {
            // First. Load plugins on start
            Lock();

            _pluginsPaths = GetAssemblyPaths();
            _plugins = _pluginLoader.LoadPlugins(_pluginsPaths.Keys.ToArray());

            Unlock();
        }

        private IDictionary<string, DateTime> GetAssemblyPaths()
        {
            return Directory
                .GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.*", SearchOption.AllDirectories)
                .Where(x => _patternForPlugins.IsMatch(Path.GetFileName(x)))
                .ToDictionary(key => key, val => File.GetCreationTime(val));
        }

        /// <summary>
        /// Lock and Unlock for thread work. TODO: Change logic
        /// </summary>
        private void Lock() => _blockGet = true;
        private void Unlock() => _blockGet = false;
    }
}
