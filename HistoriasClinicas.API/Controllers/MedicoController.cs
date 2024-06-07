using Microsoft.AspNetCore.Mvc;

namespace HistoriasClinicas.API.Controllers
{
    public class MedicoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
