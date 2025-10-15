using EduNova.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryEtiqueta
    {
        Task<ICollection<Etiqueta>> ListAsync();

        Task<string> AddAsync(Etiqueta entity);
        Task DeleteAsync(int id);
        Task<Etiqueta?> FindByIdAsync(int id);
        Task UpdateAsync();
    }
}
