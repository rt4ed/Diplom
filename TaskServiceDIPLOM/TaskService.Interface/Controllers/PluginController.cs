using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using TaskService.Interface.Areas.Identity.Data;
using TaskService.Interface.Models;
using System.Linq;
using TaskService.CommonTypes.Sql;
using Microsoft.Build.Framework;
using System.Text.RegularExpressions;

namespace TaskService.Interface.Controllers
{
    public class PluginController : Controller
    {

        private readonly ApplicationDbContext _db;
        IWebHostEnvironment _appEnvironment;

        public PluginController(ApplicationDbContext context, IWebHostEnvironment appEnvironment)
        {
            _db = context;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {

            var pluginViewModel = new PluginViewModel();
            var path = Directory.GetParent(Environment.CurrentDirectory)?.FullName + @"\TaskService\bin\Debug\net6.0\Plugins";
            string[] dirs = Directory.GetFiles(path, "*.dll");

            try
            {
                foreach (var plugPath in dirs)
                {
                    var plugin = new PluginModel();
                    Assembly pluginAssembly = Assembly.LoadFrom(plugPath);
                    plugin.Name = pluginAssembly.GetName().Name;
                    plugin.Version = pluginAssembly.GetName()?.Version?.ToString();
                    
                    plugin.Author = pluginAssembly.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company;
                    pluginViewModel.GetPlugins.Add(plugin);
                }
            }
            catch (Exception ex)
            {

            }

            return View(pluginViewModel);
        }

        [HttpPost]
        public IActionResult AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                var path = Directory.GetParent(Environment.CurrentDirectory)?
                    .FullName + @"\TaskService\bin\Debug\net6.0\Plugins\";
                var dirs = Directory.GetFiles(path, uploadedFile.FileName).FirstOrDefault();
                if (dirs == null)
                {
                    var _patternForPlugins = new Regex("^TaskService[.]Plugin[.][a-zA-Z.\\d]+[.]dll$");
                    if (_patternForPlugins.IsMatch(uploadedFile.FileName))
                    {
                        using (var fileStream = new FileStream(path + uploadedFile.FileName, FileMode.Create))
                        {
                            uploadedFile.CopyTo(fileStream);
                        }
                    }
                    else
                        return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "This file are exist";
                }


                var curDir = Directory.GetFiles(path, uploadedFile.FileName).FirstOrDefault();
                try
                {
                    Assembly assembly = Assembly.LoadFrom(curDir);
                    
                    var desc = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;
                    SqlDapper.ExecuteSqlNonQuery(desc);
                }
                catch (Exception ex)
                {

                }
            }

            return RedirectToAction("Index");
        }
    }
}
