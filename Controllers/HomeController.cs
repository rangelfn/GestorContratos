using GestorContratos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GestorContratos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Contratos()
        {
            return RedirectToAction("Index", "Contratos");

        }
        public IActionResult Editais()
        {
            return RedirectToAction("Index", "Editais");

        }
        public IActionResult Apostilamentos()
        {
            return RedirectToAction("Index", "Apostilamentos");

        }
        public IActionResult Aditivos()
        {
            return RedirectToAction("Index", "Aditivos");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}