using EduNova.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Application.Services.Interfaces
{
    public interface IServiceEstudiante
    {
        Task<ICollection<EstudianteDTO>> FindByDescriptionAsync(string description);
        Task<ICollection<EstudianteDTO>> ListAsync();
        Task<EstudianteDTO> FindByIdAsync(int id);

        Task<int> AddAsync(EstudianteDTO dto);
        Task DeleteAsync(string id);
        Task UpdateAsync(int id, EstudianteDTO dto);
    }
}
