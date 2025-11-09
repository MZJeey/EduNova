using EduNova.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryTickets
    {
        Task<int> AddAsync(Tickets entity);
        Task DeleteAsync(int id);
        Task<Tickets> FindByIdAsync(int id);
        Task<List<Tickets>> GetAllAsync();
        Task<ICollection<Tickets>> GetByUserRoleAsync(int usuarioId);
        Task UpdateAsync(Tickets entity);
        Task<List<Tickets>> GetTicketsByUserIdAsync(int userId);
        Task UpdateTicketStatusAsync(int ticketId, string nuevoEstado);
    }
}
