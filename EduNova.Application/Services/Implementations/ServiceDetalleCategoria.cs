using AutoMapper;
using EduNova.Application.DTOs;
using EduNova.Application.Services.Interfaces;
using EduNova.Infraestructure.Data;
using EduNova.Infraestructure.Repository.Implementations;
using EduNova.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Application.Services.Implementations
{
    public class ServiceDetalleCategoria : IServiceDetalleCategoria
    {
        private readonly IMapper _mapper;
        private readonly eduNovaContext _context;
        private readonly IRepositoyCategoria _categoriaRepo;
        private readonly IRepositoryEtiqueta _etiquetaRepo;
        private readonly IRepositorySLA _slaRepo;
        public ServiceDetalleCategoria(IMapper mapper, eduNovaContext eduNovaContext)
        {
            _mapper = mapper;
            _context = eduNovaContext;
        }
        public ServiceDetalleCategoria(
          IRepositoyCategoria categoriaRepo,
          IRepositoryEtiqueta etiquetaRepo,
          IRepositorySLA slaRepo)
        {
            _categoriaRepo = categoriaRepo;
            _etiquetaRepo = etiquetaRepo;
            _slaRepo = slaRepo;
        }
        public async Task<ICollection<DetalleCategoriaDTO>> ListAsync()
        {
        //    var detalleCategorias = await _context.Categoria
        //.Where(c => c.Estado)
        //.SelectMany(c => c.IdEtiqueta, (c, e) => new DetalleCategoriaDTO
        //{
        //    IdDetalleCategoria = e.IdEtiqueta, // O usa el campo que consideres único
        //    IdCategoria = c.IdCategoria,
        //    IdEtiqueta = e.IdEtiqueta,
        //    Estado = c.Estado,
        //    idSla = c.IdSla,
        //    NombreSLA = c.IdSlaNavigation != null ? c.IdSlaNavigation.Nombre : string.Empty,
        //    TiempoRespuesta = c.IdSlaNavigation != null ? c.IdSlaNavigation.TiempoMaxRespuesta : 0,
        //    TiempoResolucion = c.IdSlaNavigation != null ? c.IdSlaNavigation.TiempoMaxRespuesta : 0
        //})
        //.ToListAsync();

        //    return detalleCategorias;

            return null;
        }

    }
}
