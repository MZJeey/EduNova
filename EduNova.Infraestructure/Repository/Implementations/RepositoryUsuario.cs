using EduNova.Infraestructure.Data;
using EduNova.Infraestructure.Models;
using EduNova.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infraestructure.Repository.Implementations
{
    public class RepositoryUsuario: IRepositoryUsuario
    {
        private readonly eduNovaContext _context;
        public RepositoryUsuario(eduNovaContext context)
        {
            _context = context;
        }

       
       

        //listar usuarios

        //agregar usuario
        public async Task<string> AddAsync(Usuario entity)
        {
            await _context.Set<Usuario>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Correo;
        }

        //eliminar usuario
        public async Task DeleteAsync(int id)
        {

            var @object = await FindByIdAsync(id);
            _context.Remove(@object);
            _context.SaveChanges();
        }
      
        //encontrar usuario por id
        public async Task<Usuario?> FindByIdAsync(int id)
        {
            var @object = await _context.Set<Usuario>()
                                       .FirstOrDefaultAsync(e => e.IdUsuario == id);
            return @object;
        }

        public async Task<ICollection<Usuario>> ListAsync()
        {
            var collection = await _context.Set<Usuario>().ToListAsync();
            return collection;
        }

        //actualizar usuario
        public async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}
