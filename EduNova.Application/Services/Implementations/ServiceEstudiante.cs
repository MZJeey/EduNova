using EduNova.Application.DTOs;
using EduNova.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Application.Services.Implementations
{
    public class ServiceEstudiante : IServiceEstudiante
    {
        public Task<int> AddAsync(EstudianteDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<EstudianteDTO>> FindByDescriptionAsync(string description)
        {
            throw new NotImplementedException();
        }

        public Task<EstudianteDTO> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<EstudianteDTO>> ListAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, EstudianteDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
