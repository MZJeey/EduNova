using AutoMapper;
using EduNova.Application.Config;
using EduNova.Application.DTOs;
using EduNova.Application.Services.Interfaces;
using EduNova.Infraestructure.Data;
using EduNova.Infraestructure.Models;
using EduNova.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Application.Services.Implementations
{
    public class ServiceUsuario : IServiceUsuario
    {
        private readonly IRepositoryUsuario _repository;
        private readonly IMapper _mapper;
      
        private readonly eduNovaContext _context;
        private readonly ILogger<ServiceUsuario> _logger;



        public ServiceUsuario(IRepositoryUsuario repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            
        }
        public async Task<string> AddAsync(UsuarioDTO dto)
        {
            // Validación básica
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var objectMapped = _mapper.Map<Usuario>(dto);

            // Agregar a la base de datos
             return await _repository.AddAsync(objectMapped);
        }


        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(int.Parse(id));
        }

        public Task<ICollection<UsuarioDTO>> FindByDescriptionAsync(string description)
        {
            throw new NotImplementedException();
        }

        public async Task<UsuarioDTO> FindByIdAsync(string id)
        {
            var @object = await _repository.FindByIdAsync(int.Parse(id));
            var objectMapped = _mapper.Map<UsuarioDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<UsuarioDTO>> ListAsync()
        {

            //Obtener datos del repositorio 
            var list = await _repository.ListAsync();
            // Map List<Autor> a ICollection<BodegaDTO> 
            var collection = _mapper.Map<ICollection<UsuarioDTO>>(list);
            // Return lista 
            return collection;
        }

        public Task<UsuarioDTO> LoginAsync(string id, string password)
        {
            throw new NotImplementedException();
        }

        public Task<string> RegisterAsync(UsuarioDTO dto)
        {

            throw new NotImplementedException();
        }

        public Task UpdateAsync(string id, UsuarioDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
