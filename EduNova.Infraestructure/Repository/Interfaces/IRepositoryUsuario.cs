using EduNova.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryUsuario
    {
        Task<ICollection<Usuario>> ListAsync();

        Task<string> AddAsync(Usuario entity);
        Task DeleteAsync(int id);
        Task<Usuario?> FindByIdAsync(int id);
        Task UpdateAsync();

    }
}
