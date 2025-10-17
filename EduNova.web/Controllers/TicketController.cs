using EduNova.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
            var tickets =await _serviceTickets.GetAllAsync();
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

    }
}
