using EduNova.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Application.Services.Interfaces
{
    public interface IServiceEtiqueta
    {
        Task<ICollection<EtiquetaDTO>> ListAsync();
        Task<string> AddAsync(EtiquetaDTO entity);
        Task DeleteAsync(int id);
        Task<EtiquetaDTO?> FindByIdAsync(int id);
        Task UpdateAsync(EtiquetaDTO entity);
    }
}
