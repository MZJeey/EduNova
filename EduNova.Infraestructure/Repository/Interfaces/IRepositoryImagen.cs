using EduNova.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryImagen
    {
        Task AddAsync (Imagenes imagenes);
        Task<List<Imagenes>> FindByIdAsync (int id);
        Task DeleteAsync (int id);
        Task <List<Imagenes>> ListAsync();
    }
}
