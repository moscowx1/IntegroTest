using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServerApp.Models;
using System.IO;

namespace ServerApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
