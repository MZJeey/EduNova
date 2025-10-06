using EduNova.Application.Services.Interfaces;
using EduNova.Infraestructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduNova.web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IServicesUsuario _serviceUsuario;
        private readonly ILogger<UsuarioController> _logger;
        private readonly eduNovaContext _context;
         public UsuarioController(IServicesUsuario serviceUsuario)
        {
            _serviceUsuario=serviceUsuario;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var collection = await _serviceUsuario.ListAsync();
            return View(collection);
        }

        public async Task<IActionResult> Details(string id)
        {
            var usuario = await _serviceUsuario.ListAsync();
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }
      

    }
}
