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
    public class RepositorySLA : IRepositorySLA
    {
        private readonly eduNovaContext _context;
        public RepositorySLA(eduNovaContext context)
        {
            _context = context;
        }
        public async Task<Sla> FindByIdAsync(int id)
        {
            var @object = await _context.Set<Sla>()
                                       .FindAsync(id);
            return @object;
        }

        public async Task<ICollection<Sla>> ListAsync()
        {
            var collection = await _context.Set<Sla>().ToListAsync();
            return collection;

        }
    }
}
