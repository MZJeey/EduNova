using EduNova.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infraestructure.Repository.Implementations
{
    public interface IRepositoyCategoria
    {
        Task<ICollection<Categoria>> ListAsync();

        Task<string> AddAsync(Categoria entity);
        Task DeleteAsync(int id);
        Task<Categoria?> FindByIdAsync(int id);
        Task UpdateAsync();
    }
}
