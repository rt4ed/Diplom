using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskService.CommonTypes.Classes
{
    public class TaskWarning
    {
        public string Message { get; set; }

        /// <summary>
        /// If IsCritical then it is error
        /// </summary>
        public bool IsCritical { get; set; }

        public override string ToString()
        {
            return !IsCritical ? $"Warning: {Message}" : $"Error: {Message}";
        }
    }
}
