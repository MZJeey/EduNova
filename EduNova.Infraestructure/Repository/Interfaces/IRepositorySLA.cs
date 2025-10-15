using EduNova.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infraestructure.Repository.Interfaces
{
    public interface IRepositorySLA
    {
        Task<ICollection<Sla>> ListAsync();
        Task<Sla> FindByIdAsync(int id);
    }
}
