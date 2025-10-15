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
    public class ServiceEtiqueta : IServiceEtiqueta
    {
        private readonly IRepositoryEtiqueta _repository;
        private readonly IMapper _mapper;
        private readonly eduNovaContext _context;

        public ServiceEtiqueta(IRepositoryEtiqueta repositoryEtiqueta, IMapper mapper, eduNovaContext eduNovaContext)
        {
            _repository = repositoryEtiqueta;
            _mapper = mapper;
            _context = eduNovaContext;
        }

        public Task<string> AddAsync(EtiquetaDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<EtiquetaDTO?> FindByIdAsync(int id)
        {
            var @object = await _context.Etiqueta.Include(c => c.IdCategoria).FirstOrDefaultAsync(c => c.IdEtiqueta == id);
            return _mapper.Map<EtiquetaDTO>(@object);
        }

        public async Task<ICollection<EtiquetaDTO>> ListAsync()
        {

            var collection = await _repository.ListAsync();
            return _mapper.Map<ICollection<EtiquetaDTO>>(collection);
        }

        public Task UpdateAsync(EtiquetaDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
