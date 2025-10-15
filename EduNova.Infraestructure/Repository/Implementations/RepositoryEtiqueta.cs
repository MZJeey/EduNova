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
    public class RepositoryEtiqueta : IRepositoryEtiqueta
    {
        private readonly eduNovaContext _context;
        public RepositoryEtiqueta(eduNovaContext context)
        {
            _context = context;
        }
        public async Task<string> AddAsync(Etiqueta entity)
        {
            await _context.Set<Etiqueta>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Nombre;
        }

        public async Task DeleteAsync(int id)
        {
            var @object = await FindByIdAsync(id);
            if (@object != null)
            {
                _context.Remove(@object);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Categoria with ID {id} not found.");
            }
        }

        public async Task<Etiqueta?> FindByIdAsync(int id)
        {
            var @object = await _context.Set<Etiqueta>()
                                      .FirstOrDefaultAsync(e => e.IdEtiqueta == id);
            return @object;
        }

        public async Task<ICollection<Etiqueta>> ListAsync()
        {
            var collection = await _context.Etiqueta.Include(c => c.IdCategoria).ToListAsync();
            return collection;
        }

        public async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
