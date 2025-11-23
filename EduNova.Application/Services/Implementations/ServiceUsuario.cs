using AutoMapper;
using AvilShop.Application.Utils;
using EduNova.Application.Config;
using EduNova.Application.DTOs;
using EduNova.Application.Services.Interfaces;
using EduNova.Infraestructure.Data;
using EduNova.Infraestructure.Models;
using EduNova.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Application.Services.Implementations
{
    public class ServiceUsuario : IServiceUsuario
    {
        private readonly IRepositoryUsuario _repository;
        private readonly IMapper _mapper;
        private readonly IOptions<AppConfig> _options;
        private readonly eduNovaContext _context;
        private readonly ILogger<ServiceUsuario> _logger;
        private readonly AppConfig _appConfig;


        public ServiceUsuario(IRepositoryUsuario repository, IMapper mapper,eduNovaContext context,
        ILogger<ServiceUsuario> logger, IOptions<AppConfig> options) 
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
            _logger = logger;
            _options = options;
            
        }
        public async Task<int> AddAsync(UsuarioDTO dto)
        {
            // Validación básica
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var objectMapped = _mapper.Map<Usuario>(dto);

            objectMapped.IdUsuario = 0; // Asegurar que el ID es 0 para nuevas inserciones
           // Agregar a la base de datos
             await _context.Usuario.AddAsync(objectMapped);
            return await _context.SaveChangesAsync();


        }


        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(int.Parse(id));
        }

        public Task<ICollection<UsuarioDTO>> FindByDescriptionAsync(string description)
        {
            throw new NotImplementedException();
        }

        public async Task<UsuarioDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<UsuarioDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<UsuarioDTO>> ListAsync()
        {

            //Obtener datos del repositorio 
            var list = await _repository.ListAsync();
            // Map List<Autor> a ICollection<BodegaDTO> 
            var collection = _mapper.Map<ICollection<UsuarioDTO>>(list);
            // Return lista 
            return collection;
        }

        //public async Task<UsuarioDTO> LoginAsync(string correo, string password)
        //{
        //    try
        //    {
        //        _logger.LogInformation($"Intentando login para: {correo}");

        //        // Buscar usuario por email
        //        var usuario = await _context.Set<Usuario>()
        //                                  .FirstOrDefaultAsync(u => u.Correo == correo);

        //        if (usuario == null)
        //        {
        //            _logger.LogWarning($"Usuario no encontrado: {correo}");
        //            return null;
        //        }

        //        // Encriptar la contraseña para comparar
        //        string secret = _appConfig.Crypto.Secret;
        //        string passwordEncrypted = Cryptography.Encrypt(password, secret);

        //        if (usuario.Clave != passwordEncrypted)
        //        {
        //            _logger.LogWarning($"Contraseña incorrecta para: {correo}");
        //            return null;
        //        }

        //        //// Actualizar último inicio de sesión
        //        //usuario.UltimoInicioSesion = DateTime.UtcNow;
        //        //await _context.SaveChangesAsync();

        //        _logger.LogInformation($"Login exitoso para: {correo}");

        //        return new UsuarioDTO
        //        {
        //            IdUsuario = usuario.IdUsuario,
        //            Correo = usuario.Correo,
        //            idRol = usuario.IdRol,
        //            Clave = usuario.Clave,
        //            Nombre = usuario.Nombre
                   
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Error durante login para: {correo}");
        //        return null;
        //    }
        //}
         public async Task<UsuarioDTO> LoginAsync(string id, string password)
    {
        UsuarioDTO usuarioDTO = null!;

        // Llave secreta
        string secret = _options.Value.Crypto.Secret;
        // Password encriptado
        string passwordEncrypted = Cryptography.Encrypt(password, secret);

        var @object = await _repository.LoginAsync(id, passwordEncrypted);

        if (@object != null)
        {
            usuarioDTO = _mapper.Map<UsuarioDTO>(@object);
        }

        return usuarioDTO;
    }


        public Task<string> RegisterAsync(UsuarioDTO dto)
        {

            throw new NotImplementedException();
        }

        //public async Task UpdateAsync(int id, UsuarioDTO dto)
        //{
        //    var @object = await _repository.FindByIdAsync(id);
        //    //       source, destination
        //    _mapper.Map(dto, @object!);
        //    await _repository.UpdateAsync();
        //}

public async Task UpdateAsync(int id, UsuarioDTO dto)
{
    var entity = await _repository.FindByIdAsync(id);
    if (entity == null)
        throw new Exception("Usuario no encontrado");

    // Si la vista NO envía una nueva clave, se mantiene la que ya tiene el usuario
    if (string.IsNullOrWhiteSpace(dto.Clave))
    {
        dto.Clave = entity.Clave; // ya está encriptada en BD
    }
    else
    {
        // Si sí viene algo en dto.Clave → lo encriptamos
        string secret = _options.Value.Crypto.Secret;
        dto.Clave = Cryptography.Encrypt(dto.Clave, secret);
    }

    // Mapear DTO → entidad existente
    _mapper.Map(dto, entity);

    await _repository.UpdateAsync();
}



    }
}
