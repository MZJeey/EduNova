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

        public async Task<ICollection<Tickets>> GetByUserAsync(int usuarioId)
        {
            // Obtener el usuario con su rol
            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(u => u.IdUsuario == usuarioId);

            if (usuario == null)
                return new List<Tickets>();

            IQueryable<Tickets> query = _context.Tickets;

            // Filtrar según el rol del usuario
            query = usuario.IdRol switch
            {
                1 => query, // Administrador ve todos los tickets
                3 => query, // Soporte/Técnico ve todos los tickets
                2 => query.Where(t => t.UsuarioSolicitante == usuarioId), // Usuario normal ve solo los suyos
                _ => query.Where(t => t.UsuarioSolicitante == usuarioId) // Por defecto, igual que usuario normal
            };

            return await query.ToListAsync();
        }

        public Task<ICollection<Tickets>> GetByUserRoleAsync(int usuarioId)
        {
            throw new NotImplementedException();
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
