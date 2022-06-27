using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskService.Plugin.CBRTasks.Models
{
    public class CurrencyModel
    {
        public string Name { get; set; }

        public decimal Curs { get; set; }

        public int Code { get; set; }

        public DateTime Date { get; set; }
    }
}
