using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskService.CommonTypes.Classes
{
    public static class TaskServiceConst
    {
        public const int DelayForPluginsCheck = 60000;

        public const int DelayForBlockingExc = 100;

        public static int DelayForTasks = 1000;

        public static int RetriesForFilesWork = 5;

        public static void InitDelay(string delay)
        {
            if (int.TryParse(delay, out var parsed))
                DelayForTasks = parsed;
        }
    }
}
