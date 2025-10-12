using Microsoft.AspNetCore.Mvc;

namespace EduNova.web.Controllers
{
    public class CategoriasController : Controller
    {
        public IActionResult Categorias()
        {
            return View();
        }
    }
}
