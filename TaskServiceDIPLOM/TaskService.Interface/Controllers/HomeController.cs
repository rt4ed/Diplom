using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TaskService.API.DataManagers;
using TaskService.Interface.Areas.Identity.Data;
using TaskService.Interface.Models;

namespace TaskService.Interface.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        IWebHostEnvironment _appEnvironment;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IWebHostEnvironment appEnvironment)
        {
            _db = context;
            _logger = logger;
            _appEnvironment = appEnvironment;
        }

        
        public IActionResult Index()
        {
            var taskDTO = new ServiceTaskViewModel();
            taskDTO.GetTaskDTOs = _db.ServiceTasks.ToList();
            return View(taskDTO);
        }

        public IActionResult Viewer()
        {
            return View();
        }

        public IActionResult ReportDesigner()
        {
            return View();
        }

        public IActionResult Modified()
        {
            var tempTaskDTO = new TempTaskViewModel();
            tempTaskDTO.GetTempTaskDTOs = _db.TempTasks.ToList();
            tempTaskDTO.GetTaskDTOs = _db.ServiceTasks.ToList();

            foreach(var task in tempTaskDTO.GetTempTaskDTOs)
            {
                task.ServiceTask = tempTaskDTO.GetTaskDTOs.FirstOrDefault(x => x.TaskID == task.TaskID);
            }

            return View(tempTaskDTO);
        }

        [HttpPost]
        public IActionResult Modified(bool pr, int id)
        {
            
            var temp = _db.TempTasks.FirstOrDefault(x => x.TaskID == id);
            var task = _db.ServiceTasks.FirstOrDefault(x => x.TaskID == id);

            if (pr && temp!=null)
            {
                _db.ServiceTasks.Remove(task);
                _db.ServiceTasks.Add(temp.GetServiceTask());
                _db.TempTasks.Remove(temp);
                _db.SaveChanges();
            }
            else
            {
                _db.TempTasks.Remove(temp);
                _db.SaveChanges();
            }


            return Modified();
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Index(int id, bool isEnabled, int branch, string dependency, DateTime startTime,
            DateTime endTime, string mask, string path, string url, bool manualStart, string modifiedBy, int fieldsCount, string fieldsSeparator)
        {
            var taskDTO = new ServiceTaskViewModel();
            taskDTO.GetTaskDTOs = _db.ServiceTasks.ToList();

            var originTask = _db.ServiceTasks.FirstOrDefault(x => x.TaskID == id);

            var tempTask =  new TempTask();
            tempTask.TaskID = id;
            tempTask.TaskType = originTask?.TaskType;
            tempTask.IsEnabled = isEnabled;
            tempTask.Branch = branch;
            tempTask.TaskName = originTask?.TaskName;
            tempTask.Dependency= dependency ?? string.Empty;
            tempTask.LastWorkTime = originTask?.LastWorkTime;
            tempTask.TaskStartTime = startTime;
            tempTask.TaskEndTime = endTime;
            tempTask.FileMask = mask;
            tempTask.FilePath = path;
            tempTask.Url = url;
            tempTask.FieldsCount = fieldsCount;
            tempTask.FieldsSeparator = fieldsSeparator;
            tempTask.ModifiedBy = modifiedBy;
            tempTask.ManualStart = manualStart;

            var check = _db.TempTasks.FirstOrDefault(x => x.TaskID == tempTask.TaskID);

            if (check != null)
            {
                _db.TempTasks.Remove(check);
            }
            _db.TempTasks.Add(tempTask);
            _db.SaveChanges();

            return View(taskDTO);
        }
    }
}
//public int? TaskID { get; set; }//13-(1)

//public int? TaskType { get; set; }//13-(1)

//public bool? IsEnabled { get; set; }//13+(1)

//public int? Branch { get; set; }//13+(1)

//public string? TaskName { get; set; }//13-(1)

//public string? Dependency { get; set; } = string.Empty;//13+(1)

//public DateTime? LastWorkTime { get; set; }//13-(1)

//public DateTime? TaskStartTime { get; set; }//13+(1)

//public DateTime? TaskEndTime { get; set; }//13+(1)

//public string? FileMask { get; set; }//13+(2)

//public string? FilePath { get; set; } = string.Empty;//13+(2)

//public string? Url { get; set; } = string.Empty;//1+(3)

//public int? FieldsCount { get; set; }//3+(2)

//public string? FieldsSeparator { get; set; } = "#";//3+(2)

//public string? ModifiedBy { get; set; }

//public string? AuthoriziedBy { get; set; }

//public string? Params { get; set; } = string.Empty;//похуй

//public bool? ManualStart { get; set; }//13+(3)