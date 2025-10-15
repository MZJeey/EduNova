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
   public class ServiceCategoria:IserviceCategoria
    {
        private readonly IRepositoyCategoria _repository;
        private readonly IMapper _mapper;
        private readonly eduNovaContext _context;
        public ServiceCategoria(IRepositoyCategoria repositoryCategoria, IMapper mapper , eduNovaContext eduNovaContext)
        {
            _repository = repositoryCategoria;
            _mapper = mapper;
            _context = eduNovaContext;
        }

        public Task<int> AddAsync(CategoriaDTO dto)
        {
            throw new NotImplementedException();
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
      .Where(c => c.IdCategoria == id)
      .Select(c => new DetalleCategoriaDTO
      {
          IdCategoria = c.IdCategoria,
          Nombre = c.Nombre,
          Descripcion = c.Descripcion,
          IdSla = c.IdSla,
          NombreSLA = c.IdSlaNavigation != null ? c.IdSlaNavigation.Nombre : "",
          TiempoRespuesta = c.IdSlaNavigation != null ? c.IdSlaNavigation.TiempoMaxRespuesta : 0,
          TiempoResolucion = c.IdSlaNavigation != null ? c.IdSlaNavigation.TiempoMaxResolucion : 0,
         Etiquetas= c.IdEtiqueta != null ? c.IdEtiqueta.Select(e => new EtiquetaDTO
          {
              IdEtiqueta = e.IdEtiqueta,
              Nombre = e.Nombre,
            
             
          }).ToList() : new List<EtiquetaDTO>()


      })
      .FirstOrDefaultAsync();


            return categoria!;
        }

        public async Task<ICollection<CategoriaDTO>> ListAsync()
        {
            var collection = await _repository.ListAsync();
            var listaMapeada = _mapper.Map<List<CategoriaDTO>>(collection);
            return listaMapeada; // List<T> implementa ICollection<T>
        }

        public Task UpdateAsync(int id, CategoriaDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
