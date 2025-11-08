using AutoMapper;
using EduNova.Application.DTOs;
using EduNova.Application.Services.Interfaces;
using EduNova.Infraestructure.Data;
using EduNova.Infraestructure.Models;
using EduNova.Infraestructure.Repository.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EduNova.Application.Services.Implementations
{
    public class ServiceImagen : IServiceImagen
    {
        private readonly IRepositoryImagen _repository;
        private readonly IMapper _mapper;


        private readonly eduNovaContext _context;

        public ServiceImagen(
            IRepositoryImagen repositoryImagen,
            IMapper mapper,
            eduNovaContext eduNovaContext
            )
        {
            _repository = repositoryImagen;
            _mapper = mapper;
            _context = eduNovaContext;


        }




        public async Task AddAsync(int idTicket, List<IFormFile> imagenes)
        {
            if (imagenes == null || !imagenes.Any())
                return;

            // Ruta directa a uploads (sin subcarpeta tickets)
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            // Crea la carpeta si no existe
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            foreach (var imagen in imagenes.Where(i => i.Length > 0))
            {
                // Generar nombre único
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imagen.FileName)}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                // Guardar archivo físico
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imagen.CopyToAsync(stream);
                }

                // Guardar en base de datos solo el nombre del archivo
                await _repository.AddAsync(new Imagenes
                {
                    Imagen = fileName,
                    IdTicket = idTicket
                });
            }

            // Guardar cambios en la base de datos para las imágenes
            await _context.SaveChangesAsync();
        }









        public async Task DeleteAsync(int id)
        {
           
        }

        public async Task<List<ImagenDTO>> FindByIdAsync(int id)
        {
            var imagenes = await _repository.FindByIdAsync(id);
            return imagenes.Select(i => new ImagenDTO
            {
                IdTicket = (int)i.IdTicket,
                Url = $"/uploads/{i.Imagen}",
                IdImagen = i.IdImagen
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