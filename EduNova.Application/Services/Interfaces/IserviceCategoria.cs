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
        Task<DetalleCategoriaDTO> FindByIdAsync(int id);
        Task<int> AddAsync(CrearCategoriaDTO dto);
        Task DeleteAsync(int id);
        Task<EditarCategoriaDTO> GetEditAsync(int id);
        Task UpdateAsync(int id, EditarCategoriaDTO dto);

        Task<(List<(int Id, string Nombre, int Tr, int Tres)> Slas,
                       List<(int Id, string Nombre)> Etiquetas,
                       List<(int Id, string Nombre)> Especialidades)> GetCreateListsAsync();


    }
}
