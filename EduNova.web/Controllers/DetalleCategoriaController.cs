using EduNova.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EduNova.web.Controllers
{
    public class DetalleCategoriaController : Controller
    {
        private readonly IServiceDetalleCategoria _serviceDetalleCategoria;
        public DetalleCategoriaController(IServiceDetalleCategoria serviceDetalleCategoria)
        {
            _serviceDetalleCategoria = serviceDetalleCategoria;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var detalleCategorias = await _serviceDetalleCategoria.ListAsync();
            return View(detalleCategorias);
        }
    }
}
