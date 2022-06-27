using System.Collections.ObjectModel;

namespace TaskService.Interface.Models
{
    public class PluginViewModel
    {
        public ICollection<PluginModel>? GetPlugins { get; set; } = new List<PluginModel>();
    }
}
