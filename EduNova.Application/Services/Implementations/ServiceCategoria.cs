using AutoMapper;
using EduNova.Application.DTOs;
using EduNova.Application.Services.Interfaces;
using EduNova.Infraestructure.Data;
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

        public async Task<CategoriaDTO> FindByIdAsync(int id)
        { 
            var @object = await _context.Categoria.Include(c => c.IdSlaNavigation).FirstOrDefaultAsync(c => c.IdCategoria == id);
            return _mapper.Map<CategoriaDTO>(@object);


        }

        public async Task<ICollection<CategoriaDTO>> ListAsync()
        {
            var collection = await _repository.ListAsync();
            return _mapper.Map<ICollection<CategoriaDTO>>(collection);
        }

        public Task UpdateAsync(int id, CategoriaDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
