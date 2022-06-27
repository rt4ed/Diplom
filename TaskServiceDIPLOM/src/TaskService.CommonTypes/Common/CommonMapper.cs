using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskService.CommonTypes.Classes;

namespace TaskService.CommonTypes.Common
{
    /// <summary>
    /// Common mapper with base operations
    /// </summary>
    public class CommonMapper
    {
        public string? CutStringWithWarning(string? s, int length, string fieldName, ref List<TaskWarning> taskWarnings)
        {
            return CutStringWithWarning(s, 0, length, fieldName, ref taskWarnings);
        }

        public string? CutStringWithWarning(string? s, int start, int length, string fieldName, ref List<TaskWarning> taskWarnings)
        {
            if (s is null)
            {
                return s;
            }

            if (s.Length > length)
            {
                taskWarnings.Add(new TaskWarning { IsCritical = false, Message = $"{fieldName} were truncated" });
                return s.Substring(start, length);
            }

            return s;
        }

        public int? ParseInt(string? s, string fieldName, ref List<TaskWarning> taskWarnings)
        {
            if (int.TryParse(s, out var res))
            {
                return res;
            }

            taskWarnings.Add(new TaskWarning { IsCritical = false, Message = $"{fieldName} INT value couldnt be parsed, changing to default" });
            return null;
        }

        public DateTime? ParseDateTime(string? s, string fieldName, ref List<TaskWarning> taskWarnings)
        {
            if (DateTime.TryParse(s, null, System.Globalization.DateTimeStyles.None, out var res))
            {
                return res;
            }

            taskWarnings.Add(new TaskWarning { IsCritical = false, Message = $"{fieldName} DateTime value couldnt be parsed, changing to default" });
            return null;
        }

        public bool? ParseBool(string? s, string fieldName, ref List<TaskWarning> taskWarnings)
        {
            if(bool.TryParse(s, out var res))
            {
                return res;
            }

            taskWarnings.Add(new TaskWarning { IsCritical = false, Message = $"{fieldName} bool value couldnt be parsed, changing to default" });
            return null;
        }
    }
}
