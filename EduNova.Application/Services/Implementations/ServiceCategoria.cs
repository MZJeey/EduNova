using AutoMapper;
using EduNova.Application.DTOs;
using EduNova.Application.Services.Interfaces;
using EduNova.Infraestructure.Data;
using EduNova.Infraestructure.Models;
using EduNova.Infraestructure.Repository.Implementations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Application.Services.Implementations
{
    public class ServiceCategoria : IserviceCategoria
    {
        private readonly IRepositoyCategoria _repository;
        private readonly IMapper _mapper;
        private readonly eduNovaContext _context;
        public ServiceCategoria(IRepositoyCategoria repositoryCategoria, IMapper mapper, eduNovaContext eduNovaContext)
        {
            _repository = repositoryCategoria;
            _mapper = mapper;
            _context = eduNovaContext;
        }


        //para lllenar los combos de cada uno, sla, etiqueta

        public async Task<(List<(int Id, string Nombre, int Tr, int Tres)> Slas,
                         List<(int Id, string Nombre)> Etiquetas,
                         List<(int Id, string Nombre)> Especialidades)> GetCreateListsAsync()
        {
            var slas = await _context.Sla
                .Select(s => new { s.IdSla, s.Nombre, s.TiempoMaxRespuesta, s.TiempoMaxResolucion })
                .ToListAsync();

            var etiquetas = await _context.Etiqueta
                .Select(e => new { e.IdEtiqueta, e.Nombre })
                .ToListAsync();

            var especialidades = await _context.Especialidades
                .Select(es => new { es.Idespecialidad, es.NombreEspecialidad })
                .ToListAsync();

            return (
                slas.Select(s => (s.IdSla, s.Nombre, s.TiempoMaxRespuesta, s.TiempoMaxResolucion)).ToList(),
                etiquetas.Select(e => (e.IdEtiqueta, e.Nombre)).ToList(),
                especialidades.Select(es => (es.Idespecialidad, es.NombreEspecialidad)).ToList()
            );
        }



        //Metodo para crear la categoria
        public async Task<int> AddAsync(CrearCategoriaDTO dto)
        {
            // Validaciones de SLA (enunciado)
            if (dto.IdSla is null)
            {
                if (dto.TiempoRespuesta is null || dto.TiempoResolucion is null)
                    throw new ArgumentException("Debe indicar tiempos de respuesta y resolución cuando no selecciona un SLA.");

                if (dto.TiempoRespuesta <= 0)
                    throw new ArgumentException("El tiempo de respuesta debe ser mayor que 0.");

                if (dto.TiempoResolucion <= dto.TiempoRespuesta)
                    throw new ArgumentException("El tiempo de resolución debe ser mayor que el tiempo de respuesta.");
            }

            // Crear entidad base
            var entity = new Categoria
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Estado = true,
               
            };
            if (dto.IdSla.HasValue)
                entity.IdSla = dto.IdSla.Value;

            if (dto.IdSla is null)
            {
                var sla = new Sla
                {
                    Nombre = $"SLA-{dto.Nombre}-{DateTime.UtcNow:yyyyMMddHHmmss}",
                    TiempoMaxRespuesta = dto.TiempoRespuesta!.Value,
                    TiempoMaxResolucion = dto.TiempoResolucion!.Value,
                    Descripcion = $"SLA generado para la categoría '{dto.Nombre}'"
                };
                _context.Sla.Add(sla);
                await _context.SaveChangesAsync(); 
                entity.IdSla = sla.IdSla;
            }

         
            if (dto.EtiquetaIds?.Any() == true)
            {
                entity.IdEtiqueta = new List<Etiqueta>();
                foreach (var id in dto.EtiquetaIds.Distinct())
                {
                    var stub = new Etiqueta { IdEtiqueta = id };
                    _context.Attach(stub);           
                    entity.IdEtiqueta.Add(stub);
                }
            }


            if (dto.EspecialidadIds?.Any() == true)
            {
                entity.Especialidades = new List<Especialidades>();
                foreach (var id in dto.EspecialidadIds.Distinct())
                {
                    var stub = new Especialidades { Idespecialidad = id };
                    _context.Attach(stub);
                    entity.Especialidades.Add(stub);
                }
            }


            await _repository.AddAsync(entity);
            return entity.IdCategoria;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<DetalleCategoriaDTO> FindByIdAsync(int id)
        {
            //var @object = await _context.Categoria.Include(c => c.IdSlaNavigation).FirstOrDefaultAsync(c => c.IdCategoria == id);
            //return _mapper.Map<CategoriaDTO>(@object);
            var categoria = await _context.Categoria
      .Include(c => c.IdSlaNavigation)
      .Include(c => c.IdEtiqueta)
      .Include(c => c.Especialidades)
      .Where(c => c.IdCategoria == id)
      .Select(c => new DetalleCategoriaDTO
      {
          IdCategoria = c.IdCategoria,
          Nombre = c.Nombre,
          Descripcion = c.Descripcion,
          Estado=c.Estado,
          IdSla = c.IdSla,
          NombreSLA = c.IdSlaNavigation != null ? c.IdSlaNavigation.Nombre : "",
          
          TiempoRespuesta = c.IdSlaNavigation != null ? c.IdSlaNavigation.TiempoMaxRespuesta : 0,
          TiempoResolucion = c.IdSlaNavigation != null ? c.IdSlaNavigation.TiempoMaxResolucion : 0,
         Etiquetas= c.IdEtiqueta != null ? c.IdEtiqueta.Select(e => new EtiquetaDTO
          {
              IdEtiqueta = e.IdEtiqueta,
              Nombre = e.Nombre,
            
             
          }).ToList() : new List<EtiquetaDTO>(),
          Especialidades=c.Especialidades != null ? c.Especialidades.Select(es => new EspecialidadesDTO
          {
              IdEspecialidad = es.Idespecialidad,
              NombreEspecialidad = es.NombreEspecialidad,
              IdCategoria = es.IdCategoria
          }).ToList() : new List<EspecialidadesDTO>()


      })
      .FirstOrDefaultAsync();


            return categoria!;
        }

        //public async Task<ICollection<CategoriaDTO>> ListAsync()
        //{
        //    var collection = await _repository.ListAsync();
        //    var listaMapeada = _mapper.Map<List<CategoriaDTO>>(collection);
        //    return listaMapeada; // List<T> implementa ICollection<T>
        //}
        public async Task<ICollection<CategoriaDTO>> ListAsync()
        {
            var categorias = await _context.Categoria
                .Include(c => c.IdSlaNavigation)
                .Select(c => new CategoriaDTO
                {
                    IdCategoria = c.IdCategoria,
                    Nombre = c.Nombre,
                    Descripcion = c.Descripcion,
                    IdSla = c.IdSla,
                    NombreSLA = c.IdSlaNavigation != null
                        ? c.IdSlaNavigation.Nombre
                        : "Sin SLA"
                })
                .ToListAsync();

            return categorias; // List<T> implementa ICollection<T>
        }



        public async Task<EditarCategoriaDTO> GetEditAsync(int id)
        {
            var c = await _context.Categoria
                .Include(x => x.IdEtiqueta)
                .Include(x => x.Especialidades)
                .Include(x => x.IdSlaNavigation)
                .FirstOrDefaultAsync(x => x.IdCategoria == id);

            if (c == null) throw new KeyNotFoundException("Categoría no encontrada");

            return new EditarCategoriaDTO
            {
                IdCategoria = c.IdCategoria,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                Estado = c.Estado,
                IdSla = c.IdSla, 

                EtiquetaIds = c.IdEtiqueta?.Select(e => e.IdEtiqueta).ToList() ?? new(),
                EspecialidadIds = c.Especialidades?.Select(e => e.Idespecialidad).ToList() ?? new()
            };
        }





        public async Task UpdateAsync(int id, EditarCategoriaDTO dto)
        {
            if (id != dto.IdCategoria) throw new ArgumentException("IDs no coinciden");

            // Validación SLA si NO selecciona uno
            if (dto.IdSla is null)
            {
                if (dto.TiempoRespuesta is null || dto.TiempoResolucion is null)
                    throw new ArgumentException("Debe indicar tiempos cuando no selecciona un SLA.");
                if (dto.TiempoRespuesta <= 0)
                    throw new ArgumentException("El tiempo de respuesta debe ser mayor que 0.");
                if (dto.TiempoResolucion <= dto.TiempoRespuesta)
                    throw new ArgumentException("El tiempo de resolución debe ser mayor que el tiempo de respuesta.");
            }

            var entity = await _context.Categoria
                .Include(x => x.IdEtiqueta)
                .Include(x => x.Especialidades)
                .FirstOrDefaultAsync(x => x.IdCategoria == id);

            if (entity == null) throw new KeyNotFoundException("Categoría no encontrada");

            // Escalares
            entity.Nombre = dto.Nombre;
            entity.Descripcion = dto.Descripcion;
            entity.Estado = dto.Estado;

            // SLA: usar existente, o crear ad-hoc si no se eligió
            if (dto.IdSla.HasValue)
            {
                entity.IdSla = dto.IdSla.Value;
            }
            else
            {
                var sla = new Sla
                {
                    Nombre = $"SLA-{dto.Nombre}-{DateTime.UtcNow:yyyyMMddHHmmss}",
                    TiempoMaxRespuesta = dto.TiempoRespuesta!.Value,
                    TiempoMaxResolucion = dto.TiempoResolucion!.Value,
                        Descripcion = $"SLA generado para la categoría '{dto.Nombre}'"
                };
                _context.Sla.Add(sla);
                await _context.SaveChangesAsync();   // asegurar PK
                entity.IdSla = sla.IdSla;
            }

            // Etiquetas (many-to-many) – reemplazar selección SIN Attach
            entity.IdEtiqueta ??= new List<Etiqueta>();
            entity.IdEtiqueta.Clear();
            if (dto.EtiquetaIds?.Any() == true)
            {
                var etiquetas = await _context.Etiqueta
                    .Where(e => dto.EtiquetaIds.Contains(e.IdEtiqueta))
                    .ToListAsync();

                foreach (var e in etiquetas) entity.IdEtiqueta.Add(e);
            }

            // Especialidades (many-to-many) – reemplazar selección SIN Attach
            entity.Especialidades ??= new List<Especialidades>();
            entity.Especialidades.Clear();
            if (dto.EspecialidadIds?.Any() == true)
            {
                var especialidades = await _context.Especialidades
                    .Where(s => dto.EspecialidadIds.Contains(s.Idespecialidad))
                    .ToListAsync();

                foreach (var s in especialidades) entity.Especialidades.Add(s);
            }

            await _context.SaveChangesAsync();
        }
    }
}
