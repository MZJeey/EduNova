using EduNova.Application.DTOs;
using EduNova.Application.Services.Interfaces;
using EduNova.Infraestructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduNova.web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IServiceUsuario _serviceUsuario;
        // private readonly ILogger<UsuarioController> _logger;
        private readonly eduNovaContext _context;
        public UsuarioController(IServiceUsuario serviceUsuario)
        {
            _serviceUsuario = serviceUsuario;
        }

        // GET: Usuarios
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var collection = await _serviceUsuario.ListAsync();
            return View(collection);
        }

        public async Task<IActionResult> Details(int id)
        {
            var @object = await _serviceUsuario.FindByIdAsync(id);
            ViewBag.NotificationMessage = Util.SweetAlertHelper.Mensaje("Exito", "Se han cargado la info del Usuario" + id + ".",
                Util.SweetAlertMessageType.info);
            return View(@object);
        }
        //se necesita realizar dos metodos para el update uno muestra la vista y otro recibe el post

        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0)
                return NotFound();

            var usuario = await _serviceUsuario.FindByIdAsync(id);
            if (usuario == null)
                return NotFound();

            return View("Update", usuario);
        }
        // metodo para crear
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UsuarioDTO usuarioDTO)
        {
            if (ModelState.IsValid)
            {
                await _serviceUsuario.AddAsync(usuarioDTO);
                ViewBag.NotificationMessage = Util.SweetAlertHelper.Mensaje(
                    "Exito",
                    "Se ha creado el Usuario " + usuarioDTO.IdUsuario + ".",
                    Util.SweetAlertMessageType.success
                );
                return RedirectToAction(nameof(Index));
            }
            return View(usuarioDTO);
        }

        //este metodo recibe el post
        [HttpPost]
        public async Task<IActionResult> Update(int id, UsuarioDTO usuarioDTO)
        {
            if (id != usuarioDTO.IdUsuario) return NotFound();
            try
            {
                await _serviceUsuario.UpdateAsync(id, usuarioDTO);
                ViewBag.NotificationMessage = Util.SweetAlertHelper.Mensaje("Exito", "Se ha actualizado el Usuario" + id + ".",
                Util.SweetAlertMessageType.success);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _serviceUsuario.FindByIdAsync(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
