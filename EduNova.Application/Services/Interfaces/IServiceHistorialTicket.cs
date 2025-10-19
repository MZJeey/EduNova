using EduNova.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Application.Services.Interfaces
{
    public interface IServiceHistorialTicket
    {
        Task<ICollection<HistorialTicketDTO>> GetAllHistorialTickets();
        Task<HistorialTicketDTO> GetHistorialTicketById(int id);
        Task<HistorialTicketDTO> CreateHistorialTicket(HistorialTicketDTO historialTicketDto);
        Task<HistorialTicketDTO> UpdateHistorialTicket(int id, HistorialTicketDTO historialTicketDto);
        Task<bool> DeleteHistorialTicket(int id);
    }
}
