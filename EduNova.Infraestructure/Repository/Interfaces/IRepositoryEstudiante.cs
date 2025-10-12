using EduNova.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryEstudiante
    {
        Task<ICollection<Estudiante>> ListAsync();

        Task<string> AddAsync(Estudiante entity);
        Task DeleteAsync(int id);
        Task<Estudiante?> FindByIdAsync(int id);
        Task UpdateAsync();
    }
}
