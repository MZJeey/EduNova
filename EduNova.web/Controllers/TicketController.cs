using EduNova.Application.DTOs;
using EduNova.Application.Services.Implementations;
using EduNova.Application.Services.Interfaces;
using EduNova.Infraestructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace EduNova.web.Controllers
{
    public class TicketController : Controller
    {
        private readonly IServiceTickets _serviceTickets;
        private readonly IserviceCategoria _ServiceCategoria;
        private readonly IServiceImagen _serviceImagen;
        private readonly IServiceEtiqueta _serviceEtiqueta;
        public TicketController(IServiceTickets serviceTickets, IserviceCategoria serviceCategoria, IServiceImagen serviceImagen, IServiceEtiqueta serviceEtiqueta)
        {
            _serviceTickets = serviceTickets;
            _ServiceCategoria = serviceCategoria;
            _serviceImagen = serviceImagen;
            _serviceEtiqueta = serviceEtiqueta;
        }
        public async Task<IActionResult> Index()
        {
            //var tickets = await _serviceTickets.GetAllAsync();
            //return View(tickets);


            var tickets = await _serviceTickets.GetByUserAsync(6); // Reemplaza 3 con el ID del usuario actual
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

        public async Task<IActionResult> update(int id)
        {
            var ticket = await _serviceTickets.FindByIdAsync(id);

            await CargarCategorias();
            await cargarEtiquetas();
            if (ticket == null)
            {

                return NotFound();
            }
           
            return View("update",ticket);
        }

        public async Task<IActionResult> Create()
        {
            await CargarCategorias();
            await cargarEtiquetas();
            return View();
        }

        private async Task CargarCategorias()
        {
            var categorias = await _ServiceCategoria.ListAsync();
            ViewBag.Categorias = new SelectList(categorias, "IdCategoria", "Nombre");
        }
        private async Task cargarEtiquetas()
        {
            var etiquetas = await _serviceEtiqueta.ListAsync();

            // Crear SelectList con data attributes adicionales
            var etiquetasList = etiquetas.Select(e => new {
                Value = e.IdEtiqueta.ToString(),
                Text = e.Nombre,
                IdCategoria = e.IdCategoria,
                NombreCategoria = e.NombreCategoria 
            }).ToList();

            ViewBag.Etiquetas = etiquetasList;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TicketDTO ticket)
        {
            await CargarCategorias();
            ticket.FechaCreacion = DateTime.Now;
            ticket.Estado = "Pendiente";
            ticket.UsuarioSolicitante = 3;
            ticket.IdRol = 3;

            if (ModelState.IsValid)
            {
                // Primero agregar el ticket para obtener el ID
                var ticketId = await _serviceTickets.AddAsync(ticket);

                if (ticket.ImagenesArchivo != null && ticket.ImagenesArchivo.Any())
                {
                   ModelState.AddModelError("", "Error al subir imágenes. Por favor, inténtelo de nuevo.");
                }

                TempData["NotificationMessage"] = Util.SweetAlertHelper.Mensaje(
              "Éxito",
              "Se ha creado el Ticket " + ticketId + ".",
              Util.SweetAlertMessageType.success
          );

                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        [HttpPost]
     
        public async Task<IActionResult> update(int id, TicketDTO ticketDTO)
        {
            if (id != ticketDTO.IdTicket)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await CargarCategorias();
                await cargarEtiquetas();
                return View(ticketDTO);
            }

            //try
            //{
                // Asegurar que todos los campos requeridos estén presentes
                var existingTicket = await _serviceTickets.FindByIdAsync(id);
                if (existingTicket == null)
                    return NotFound();

                // Mapear propiedades importantes que podrían faltar
                ticketDTO.FechaCreacion = existingTicket.FechaCreacion; 
            ticketDTO.UsuarioSolicitante = 3;

                ticketDTO.IdRol = 3;

                await _serviceTickets.UpdateAsync(id, ticketDTO);

            // Usar TempData en lugar de ViewBag para redirecciones
            TempData["NotificationMessage"] = Util.SweetAlertHelper.Mensaje(
                "Éxito",
                "Se ha actualizado el Ticket " + id + ".",
                Util.SweetAlertMessageType.success
            );

            return RedirectToAction(nameof(Index));
            //}
            //catch (DbUpdateConcurrencyException ex)
            //{
            //    if (await _serviceTickets.FindByIdAsync(id) == null)
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        ModelState.AddModelError("", "El ticket fue modificado por otro usuario. Por favor, recarga la página e intenta nuevamente.");
            //        await CargarCategorias();
            //        await cargarEtiquetas();
            //        return View(ticketDTO);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ModelState.AddModelError("", $"Error al actualizar el ticket: {ex.Message}");
            //    await CargarCategorias();
            //    await cargarEtiquetas();
            //    return View(ticketDTO);
            //}
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
