using EduNova.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Application.Services.Interfaces
{
    public interface IserviceCategoria
    {
        Task<ICollection<CategoriaDTO>> ListAsync();
        Task<CategoriaDTO> FindByIdAsync(int id);
        Task<int> AddAsync(CategoriaDTO dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, CategoriaDTO dto);
    }
}
