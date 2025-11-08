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
    public class RepositoryImagen : IRepositoryImagen
    {
        private readonly eduNovaContext _context;
        public RepositoryImagen(eduNovaContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Imagenes imagenes)
        {
           await _context.Imagenes.AddAsync(imagenes);
              await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Imagenes>> FindByIdAsync(int id)
        {
            return await _context.Imagenes
              .Where(i => i.IdTicket == id)
              .ToListAsync();
        }

        public Task<List<Imagenes>> ListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
