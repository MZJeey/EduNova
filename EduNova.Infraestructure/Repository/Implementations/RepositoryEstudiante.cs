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
    public class RepositoryEstudiante : IRepositoryEstudiante
    {
        private readonly eduNovaContext _context;
        public RepositoryEstudiante(eduNovaContext eduNovaContext)
        {
            _context = eduNovaContext;
        }
        public async Task<string> AddAsync(Estudiante entity)
        {
            await _context.Set<Estudiante>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Nombre;
        }

        public async Task DeleteAsync(int id)
        {
            await Task.Run(() =>
            {
                var estudiante = _context.Estudiante.FirstOrDefault(e => e.IdEstudiante == id);
                if (estudiante != null)
                {
                    _context.Estudiante.Remove(estudiante);
                    _context.SaveChanges();
                }
            });
        }

        public async Task<Estudiante?> FindByIdAsync(int id)
        {
            var @object = await _context.Set<Estudiante>()
                                      .FirstOrDefaultAsync(e => e.IdEstudiante == id);
            return @object;

        }

        public async Task<ICollection<Estudiante>> ListAsync()
        {
            var collection = await _context.Set<Estudiante>().ToListAsync();
            return collection;

        }

        public async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
