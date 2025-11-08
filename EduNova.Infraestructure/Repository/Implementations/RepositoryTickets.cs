using EduNova.Infraestructure.Data;
using EduNova.Infraestructure.Models;
using EduNova.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infraestructure.Repository.Implementations
{
    public class RepositoryTickets : IRepositoryTickets
    {
        private readonly IRepositoryTickets _repository;

        private readonly eduNovaContext _context;
        public RepositoryTickets(eduNovaContext context)
        {
            _context = context;
        }
        // En tu repositorio genérico
        public async Task<int> AddAsync(Tickets entity)
        {
            await _context.Set<Tickets>().AddAsync(entity);
            var result = await _context.SaveChangesAsync();
            return result; // ✅ Retorna 1 si se guardó correctamente
        }



        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Tickets> FindByIdAsync(int id)
        {
            var @object = await _context.Set<Tickets>()
                                       .FirstOrDefaultAsync(e => e.IdTicket == id);
            return @object;
        }

        public async Task<List<Tickets>> GetAllAsync()
        {
            var collection = await _context.Set<Tickets>().ToListAsync();
            return collection;
        }

        public async Task<List<Tickets>> GetTicketsByUserIdAsync(int userId)
        {
            var tickets = await _context.Set<Tickets>()
                .Where(t => t.UsuarioSolicitante == userId)
                .Include(t => t.IdCategoriaNavigation)
                .Include(t => t.UsuarioSolicitanteNavigation)
                
                .ToListAsync();

            return tickets;
        }

        public Task UpdateAsync(Tickets entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateTicketStatusAsync(int ticketId, string nuevoEstado)
        {
            var ticket = await _context.Set<Tickets>()
        .FindAsync(ticketId);

           

            ticket.Estado = nuevoEstado;
            await _context.SaveChangesAsync();
           
        }
    }
}
