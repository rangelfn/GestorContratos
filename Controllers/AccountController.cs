using Microsoft.AspNetCore.Mvc;

namespace GestorContratos.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

