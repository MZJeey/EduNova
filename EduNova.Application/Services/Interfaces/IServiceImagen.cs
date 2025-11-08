using EduNova.Application.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Application.Services.Interfaces
{
    public interface IServiceImagen
    {
        Task<ICollection<ImagenDTO>> ListAsync();
        Task< List<ImagenDTO>> FindByIdAsync(int id);
        Task AddAsync(int idTicket,List<IFormFile> imagenes);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, ImagenDTO dto);
    }
}
