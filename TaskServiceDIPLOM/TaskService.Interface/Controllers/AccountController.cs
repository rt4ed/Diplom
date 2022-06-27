using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaskService.Interface.Models;

namespace TaskService.Interface.Controllers
{
    public class AccountController : Controller 
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
               
    }
}
