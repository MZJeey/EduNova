using EduNova.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryHistorialTicket
    {
        Task<TicketHistorial> FindByIdAsync(int id);
        Task<List<TicketHistorial>> GetAllAsync();
        Task UpdateAsync(TicketHistorial entity);
        Task<string> AddAsync(TicketHistorial entity);
    }
}
