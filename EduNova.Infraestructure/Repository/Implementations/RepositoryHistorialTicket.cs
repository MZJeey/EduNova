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
    public class RepositoryHistorialTicket : IRepositoryHistorialTicket
    {
        private readonly IRepositoryHistorialTicket _repositoryHistorialTicket;

        private readonly eduNovaContext _context;
        public RepositoryHistorialTicket(eduNovaContext context)
        {
            _context = context;
        }
        public async Task<TicketHistorial> FindByIdAsync(int id)
        {
           var @object = await _context.Set<TicketHistorial>()
                                      .FirstOrDefaultAsync(e => e.IdHistorial == id);
            return @object;
        }

        public   async Task<List<TicketHistorial>> GetAllAsync()
        {
            var collection = await _context.Set<TicketHistorial>().ToListAsync();
            return collection;
        }

        public Task UpdateAsync(TicketHistorial entity)
        {
            throw new NotImplementedException();
        }
    }
}
