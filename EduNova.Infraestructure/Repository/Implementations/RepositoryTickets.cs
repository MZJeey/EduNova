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
        public Task<int> AddAsync(Tickets entity)
        {
            throw new NotImplementedException();
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

        public Task UpdateAsync(Tickets entity)
        {
            throw new NotImplementedException();
        }
    }
}
