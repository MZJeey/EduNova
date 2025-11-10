using EduNova.Application.DTOs;
using EduNova.Application.Services.Implementations;
using EduNova.Application.Services.Interfaces;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var (slas, etiquetas, especialidades) = await _serviceCategoria.GetCreateListsAsync();

            ViewBag.Slas = slas;                
            ViewBag.Etiquetas = etiquetas;     
            ViewBag.Especialidades = especialidades;

           
            return View(new CrearCategoriaDTO());
        }



        ///Crear categoria


        [HttpPost]
        public async Task<IActionResult> Create(CrearCategoriaDTO crearCategoriaDTO)
        {
            // Validación adicional del enunciado cuando NO eligen SLA
            if (crearCategoriaDTO.IdSla is null)
            {
                if (!crearCategoriaDTO.TiempoRespuesta.HasValue || !crearCategoriaDTO.TiempoResolucion.HasValue)
                    ModelState.AddModelError("", "Debe indicar tiempos de respuesta y resolución cuando no selecciona un SLA.");

                if (crearCategoriaDTO.TiempoRespuesta <= 0)
                    ModelState.AddModelError(nameof(crearCategoriaDTO.TiempoRespuesta), "El tiempo de respuesta debe ser mayor que 0.");

                if (crearCategoriaDTO.TiempoResolucion <= crearCategoriaDTO.TiempoRespuesta)
                    ModelState.AddModelError(nameof(crearCategoriaDTO.TiempoResolucion), "El tiempo de resolución debe ser mayor que el tiempo de respuesta.");
            }

            if (!ModelState.IsValid)
            {
                // Si hay errores, recarga las listas para volver a mostrar la vista
                var (slas, etiquetas, especialidades) = await _serviceCategoria.GetCreateListsAsync();
                ViewBag.Slas = slas;
                ViewBag.Etiquetas = etiquetas;
                ViewBag.Especialidades = especialidades;
                return View(crearCategoriaDTO);
            }

            var id = await _serviceCategoria.AddAsync(crearCategoriaDTO);

            TempData["NotificationMessage"] = Util.SweetAlertHelper.Mensaje(
                "Éxito",
                $"Se ha creado la categoría #{id}.",
                Util.SweetAlertMessageType.success
            );

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _serviceCategoria.GetEditAsync(id);
            var (slas, etiquetas, especialidades) = await _serviceCategoria.GetCreateListsAsync();
            ViewBag.Slas = slas;
            ViewBag.Etiquetas = etiquetas;
            ViewBag.Especialidades = especialidades;

            return View(model);
        }


        // Actualiza la categoria recibiendo el post 


        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditarCategoriaDTO dto)
        {
            if (id != dto.IdCategoria) return NotFound();

            // Validación SLA cuando NO se elige uno
            if (dto.IdSla is null)
            {
                if (!dto.TiempoRespuesta.HasValue || !dto.TiempoResolucion.HasValue)
                    ModelState.AddModelError("", "Debe indicar tiempos cuando no selecciona un SLA.");
                if (dto.TiempoRespuesta <= 0)
                    ModelState.AddModelError(nameof(dto.TiempoRespuesta), "El tiempo de respuesta debe ser mayor que 0.");
                if (dto.TiempoResolucion <= dto.TiempoRespuesta)
                    ModelState.AddModelError(nameof(dto.TiempoResolucion), "El tiempo de resolución debe ser mayor que el tiempo de respuesta.");
            }

            if (!ModelState.IsValid)
                return await VolverConListasAsync(dto);

            try
            {
                await _serviceCategoria.UpdateAsync(id, dto);

                TempData["NotificationMessage"] = Util.SweetAlertHelper.Mensaje(
                    "Éxito", "La categoría se actualizó correctamente.", Util.SweetAlertMessageType.success);

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                var root = ex.GetBaseException()?.Message ?? ex.Message;
                ModelState.AddModelError("", $"No se pudo guardar los cambios: {root}");
                return await VolverConListasAsync(dto);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error inesperado: {ex.Message}");
                return await VolverConListasAsync(dto);
            }
        }

        private async Task<IActionResult> VolverConListasAsync(EditarCategoriaDTO dto)
        {
            var (slas, etiquetas, especialidades) = await _serviceCategoria.GetCreateListsAsync();
            ViewBag.Slas = slas;
            ViewBag.Etiquetas = etiquetas;
            ViewBag.Especialidades = especialidades;
            return View("Edit", dto);
        }


    }
}
