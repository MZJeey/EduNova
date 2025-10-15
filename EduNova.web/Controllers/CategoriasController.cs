using EduNova.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EduNova.web.Controllers
{
    public class CategoriasController : Controller
    {

        private readonly IserviceCategoria _serviceCategoria;
        public CategoriasController(IserviceCategoria serviceCategoria)
        {
            _serviceCategoria = serviceCategoria;
        }
        public async Task<IActionResult> Index()
        {
            var categorias = await _serviceCategoria.ListAsync();
            return View(categorias);
        }
        public async Task<IActionResult> Details(int id)
        {
            var categoria = await _serviceCategoria.FindByIdAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }
        public IActionResult Categorias()
        {
            return View();
        }
    }
}
