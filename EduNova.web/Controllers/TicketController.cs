using EduNova.Application.DTOs;
using EduNova.Application.Services.Implementations;
using EduNova.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace EduNova.web.Controllers
{
    public class TicketController : Controller
    {
        private readonly IServiceTickets _serviceTickets;
        private readonly IserviceCategoria _ServiceCategoria;
        private readonly IServiceImagen _serviceImagen;
        public TicketController(IServiceTickets serviceTickets, IserviceCategoria serviceCategoria, IServiceImagen serviceImagen)
        {
            _serviceTickets = serviceTickets;
            _ServiceCategoria = serviceCategoria;
            _serviceImagen = serviceImagen;
        }
        public async Task<IActionResult> Index()
        {
            var tickets = await _serviceTickets.GetAllAsync();
            return View(tickets);
        }
        public async Task<IActionResult> Details(int id)
        {
            var ticket = await _serviceTickets.FindByIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }
        public async Task<IActionResult> Asignaciones(int id)
        {
            var tickets = await _serviceTickets.GetTicketsByUserIdAsync(3);
            return View(tickets);
        }

      
        public async Task<IActionResult> UpdateEstado(int ticketId)
        {
            var ticket = await _serviceTickets.FindByIdAsync(ticketId);
            if (ticket == null)
            {
                return NotFound();
            }
            return View("UpdateEstado",ticket);
        }
  
        public async Task<IActionResult> Create()
        {
            await CargarCategorias();
            return View();
        }

        private async Task CargarCategorias()
        {
            var categorias = await _ServiceCategoria.ListAsync();
            ViewBag.Categorias = new SelectList(categorias, "IdCategoria", "Nombre");
        }

        [HttpPost]
        public async Task<IActionResult> Create(TicketDTO ticket)
        {
            await CargarCategorias();
            ticket.FechaCreacion = DateTime.Now;
            ticket.Estado = "Abierto";
            ticket.UsuarioSolicitante = 3; // o el usuario autenticado

            if (ModelState.IsValid)
            {
                // Primero agregar el ticket para obtener el ID
                var ticketId = await _serviceTickets.AddAsync(ticket);

                if (ticket.ImagenesArchivo != null && ticket.ImagenesArchivo.Any())
                {
                   ModelState.AddModelError("", "Error al subir imágenes. Por favor, inténtelo de nuevo.");
                }

                ViewBag.NotificationMessage = Util.SweetAlertHelper.Mensaje(
                    "Éxito",
                    "Se ha creado el Ticket " + ticketId + ".",
                    Util.SweetAlertMessageType.success
                );
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateEstado(int IdTicket, string Estado)
        {
            try
            {
                if (string.IsNullOrEmpty(Estado))
                {
                    TempData["Error"] = "Debe seleccionar un estado";
                    return RedirectToAction("UpdateEstado", new { ticketId = IdTicket });
                }

                await _serviceTickets.UpdateTicketStatusAsync(IdTicket, Estado);

                TempData["Success"] = "Estado del ticket actualizado correctamente";
                return RedirectToAction("Asignaciones");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error al actualizar el estado: {ex.Message}";
                return RedirectToAction("UpdateEstado", new { ticketId = IdTicket });
            }
        }


    }
}
