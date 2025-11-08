using EduNova.Application.DTOs;
using EduNova.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Application.Services.Interfaces
{
    public interface IServiceTickets
    {
        Task<int> AddAsync(TicketDTO entity);
        Task DeleteAsync(int id);
        Task<TicketDTO> FindByIdAsync(int id);
        Task<ICollection<TicketDTO>> GetAllAsync();
        Task UpdateAsync(TicketDTO entity);
        Task<ICollection<TicketDTO>> GetTicketsByUserIdAsync(int userId);
        Task UpdateTicketStatusAsync(int ticketId, string nuevoEstado);
        Task ImagenesTicket(int idTicket, TicketDTO dTO);
    }
}
