using EduNova.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EduNova.web.Controllers
{
    public class HistorialTicketController : Controller
    {
        private readonly IServiceHistorialTicket _serviceHistorialTicket;
        public HistorialTicketController(IServiceHistorialTicket serviceHistorialTicket)
        {
            _serviceHistorialTicket = serviceHistorialTicket;
        }
        public async Task<IActionResult> Index()
        {
           var historialTickets = await _serviceHistorialTicket.GetAllHistorialTickets();
            return View(historialTickets);
            
        }
    }
}
