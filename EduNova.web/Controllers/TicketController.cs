using EduNova.Application.Services.Implementations;
using EduNova.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace EduNova.web.Controllers
{
    public class TicketController : Controller
    {
        private readonly IServiceTickets _serviceTickets;
        public TicketController(IServiceTickets serviceTickets)
        {
            _serviceTickets = serviceTickets;
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
            var tickets = await _serviceTickets.GetTicketsByUserIdAsync(1);
            return View(tickets);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateEstado(int ticketId)
        {
            var ticket = await _serviceTickets.FindByIdAsync(ticketId);
            if (ticket == null)
            {
                return NotFound();
            }
            return View("UpdateEstado",ticket);
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
