using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskService.CommonTypes.Classes
{
    public class ServiceSettings
    {
        public const string Settings = "Settings";

        public string ConnectionString { get; set; } = string.Empty;
    }
}
