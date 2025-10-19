using AutoMapper;
using EduNova.Application.DTOs;
using EduNova.Application.Services.Interfaces;
using EduNova.Infraestructure.Data;
using EduNova.Infraestructure.Models;
using EduNova.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Application.Services.Implementations
{
    public class ServiceImagen : IServiceImagen
    {
        private readonly IRepositoryImagen _repository;
        private readonly IMapper _mapper;
        private readonly eduNovaContext _context;
        //private readonly IWebHostEnvironment _hostingEnvironment;

        public ServiceImagen(IRepositoryImagen repositoryImagen, IMapper mapper, eduNovaContext eduNovaContext)
        {
            _repository = repositoryImagen;
            _mapper = mapper;
            _context = eduNovaContext;
        }
        public Task<int> AddAsync(ImagenDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ImagenDTO> FindByIdAsync(int id)
        {
            var imagenes = await _repository.FindByIdAsync(id);
            return imagenes.Select (i => new ImagenDTO
            {
                IdImagen = i.IdImagen,
                Url = $"/uploads/{i.Imagen}",
               
                Id = i.IdImagen
            }).ToList();
        }

        public Task<ICollection<ImagenDTO>> ListAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, ImagenDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
