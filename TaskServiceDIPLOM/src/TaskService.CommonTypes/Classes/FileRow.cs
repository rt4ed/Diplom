using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskService.CommonTypes.Classes
{
    public class FileRow
    {
        public ICollection<string> RowValues { get; set; }

        public int RowNumber { get; set; }
    }
}
