using EduNova.Infraestructure.Data;
using EduNova.Infraestructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infraestructure.Repository.Implementations
{
    public class RepositoryCategoria : IRepositoyCategoria
    {

        private readonly eduNovaContext _context;

        public RepositoryCategoria(eduNovaContext context)
        {
            _context = context;
        }
        public  async Task<string> AddAsync(Categoria entity)
        {
            await _context.Set<Categoria>().AddAsync(entity);
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

        public async Task<Categoria?> FindByIdAsync(int id)
        {
            var @object = await  _context.Set<Categoria>()
                                       .FirstOrDefaultAsync(e => e.IdCategoria == id);
            return @object;
        }

        public async Task<ICollection<Categoria>> ListAsync()
        {
            var collection = await _context.Categoria.Include(c => c.IdSlaNavigation).ToListAsync();
            return collection;
        }

        public async Task UpdateAsync()
        {
           await _context.SaveChangesAsync();

        }
    }
}
