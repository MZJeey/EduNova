using AutoMapper;
using EduNova.Application.DTOs;
using EduNova.Application.Services.Interfaces;
using EduNova.Infraestructure.Data;
using EduNova.Infraestructure.Models;
using EduNova.Infraestructure.Repository.Implementations;
using EduNova.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Application.Services.Implementations
{
    public class ServiceTickets : IServiceTickets
    {
        private readonly IRepositoryTickets _repository;
        private readonly IServiceImagen _ServiceImagen;
        private readonly IServiceHistorialTicket _HistorialTicket;
        private readonly IMapper _mapper;
        private readonly eduNovaContext _context;
        public ServiceTickets(IRepositoryTickets repositoryTickets,IServiceImagen ServiceImagen, IMapper mapper, eduNovaContext eduNovaContext, IServiceHistorialTicket historialTicket)
        {
            _repository = repositoryTickets;
            _ServiceImagen = ServiceImagen;
            _mapper = mapper;
            _context = eduNovaContext;
            _HistorialTicket = historialTicket;
        }



        public async Task<int> AddAsync(TicketDTO entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var objectMapped = _mapper.Map<Tickets>(entity);

            objectMapped.IdTicket = 0; // Asegurar que el ID es 0 para nuevas inserciones
                                        // Agregar a la base de datos
            await _context.Tickets.AddAsync(objectMapped);
             await _context.SaveChangesAsync();
            if(entity.ImagenesArchivo != null && entity.ImagenesArchivo.Count>0)
            {
                await _ServiceImagen.AddAsync(objectMapped.IdTicket, entity.ImagenesArchivo);
            }

            return objectMapped.IdTicket;


        }
        public async Task ImagenesTicket(int idTicket, TicketDTO dTO)
        {
            await _ServiceImagen.AddAsync(idTicket, dTO.ImagenesArchivo);
        }

        //  var categoria = await _context.Categoria
        //.Include(c => c.IdSlaNavigation)
        //.Include(c => c.IdEtiqueta)
        //.Include(c => c.Especialidades)
        //.Where(c => c.IdCategoria == id)
        //.Select(c => new DetalleCategoriaDTO
        //{
        //    IdCategoria = c.IdCategoria,
        //    Nombre = c.Nombre,
        //    Descripcion = c.Descripcion,
        //    Estado = c.Estado,
        //    IdSla = c.IdSla,
        //    NombreSLA = c.IdSlaNavigation != null ? c.IdSlaNavigation.Nombre : "",

        //    TiempoRespuesta = c.IdSlaNavigation != null ? c.IdSlaNavigation.TiempoMaxRespuesta : 0,
        //    TiempoResolucion = c.IdSlaNavigation != null ? c.IdSlaNavigation.TiempoMaxResolucion : 0,
        //    Etiquetas = c.IdEtiqueta != null ? c.IdEtiqueta.Select(e => new EtiquetaDTO
        //    {
        //        IdEtiqueta = e.IdEtiqueta,
        //        Nombre = e.Nombre,


        //    }).ToList() : new List<EtiquetaDTO>(),
        //    Especialidades = c.Especialidades != null ? c.Especialidades.Select(es => new EspecialidadesDTO
        //    {
        //        IdEspecialidad = es.Idespecialidad,
        //        NombreEspecialidad = es.NombreEspecialidad,
        //        IdCategoria = es.IdCategoria
        //    }).ToList() : new List<EspecialidadesDTO>()


        //})
        //.FirstOrDefaultAsync();


        //      return categoria!;

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

      
        public async Task<TicketDTO> FindByIdAsync(int id)
        {
            var ticketDTO = await _context.Tickets
                .Where(t => t.IdTicket == id)
                .Select(t => new TicketDTO
                {
                    // Mapea propiedades básicas del Ticket
                    IdTicket = t.IdTicket,
                    Titulo = t.Titulo,
                    Descripcion = t.Descripcion,
                    FechaCierre = t.FechaCierre,
                    FechaCreacion = t.FechaCreacion,
                    Prioridad = t.Prioridad,
                    Estado = t.Estado,
                    valoracion = t.Valoracion,



                    // Mapea propiedades de las navegaciones
                    NombreCategoria = t.IdCategoriaNavigation.Nombre,
                    NombreSolicitante = t.UsuarioSolicitanteNavigation.Nombre,
                    NombreSla = t.IdCategoriaNavigation.IdSlaNavigation.Nombre,
                    TiempoRespuesta = t.IdCategoriaNavigation.IdSlaNavigation.TiempoMaxRespuesta,
                    TiempoResolucion = t.IdCategoriaNavigation.IdSlaNavigation.TiempoMaxResolucion,
                   

                })
                .FirstOrDefaultAsync();
            //Esta parte es del historial 

            //var ticket = await _HistorialTicket.GetHistorialTicketById(id);


            if (ticketDTO != null)

            {
                               ticketDTO.Imagenes = await _ServiceImagen.FindByIdAsync(ticketDTO.IdTicket);
                ticketDTO.HistorialTickets = await _HistorialTicket.GetHistorialTicketById(ticketDTO.IdTicket);
               

            }
            return ticketDTO;
            
        }

        public async Task<ICollection<TicketDTO>> GetAllAsync()
        {
            var collection = await _repository.GetAllAsync();
            var listaMapeada = _mapper.Map<List<TicketDTO>>(collection);
          return listaMapeada;
        }

        public async Task<ICollection<TicketDTO>> GetTicketsByUserIdAsync(int userId)
        {
            // Obtener los tickets desde el repositorio
            var ticketsDb = await _repository.GetTicketsByUserIdAsync(userId);

            // Mapear manualmente al DTO, usando protección contra null
            var ticketsDto = ticketsDb.Select(t => new TicketDTO
            {
                IdTicket = t.IdTicket,
                Titulo = t.Titulo,
                Descripcion = t.Descripcion,
                Estado = t.Estado,
                Prioridad = t.Prioridad,
                valoracion = t.Valoracion,
                UsuarioSolicitante = t.UsuarioSolicitante,
                NombreCategoria = t.IdCategoriaNavigation?.Nombre ?? "Sin categoría",
                NombreSolicitante = t.UsuarioSolicitanteNavigation?.Nombre ?? "Desconocido",
                NombreSla = t.IdCategoriaNavigation.IdSlaNavigation?.Nombre ?? "Sin SLA",
                TiempoRespuesta = t.IdCategoriaNavigation.IdSlaNavigation?.TiempoMaxRespuesta,
                TiempoResolucion = t.IdCategoriaNavigation.IdSlaNavigation?.TiempoMaxResolucion,
                FechaCreacion = t.FechaCreacion,
                FechaCierre = t.FechaCierre,
    


            }).ToList();

            return ticketsDto;
        }



        public async Task UpdateAsync( int id,TicketDTO dTO)
        {
                
         var ticket=  _mapper.Map<Tickets>(dTO);
      
            await _repository.UpdateAsync(ticket);

            if (dTO.ImagenesArchivo != null && dTO.ImagenesArchivo.Count > 0)
            {
                await _ServiceImagen.AddAsync(id, dTO.ImagenesArchivo);
            }
        }




        public async Task UpdateTicketStatusAsync(int ticketId, string nuevoEstado)
        {
            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(t => t.IdTicket == ticketId);



            var estadoAnterior = ticket.Estado;
            ticket.Estado = nuevoEstado;

            
         
            // Usar AutoMapper para convertir a DTO

            var ticketDto = new TicketDTO
            {

                IdTicket = ticket.IdTicket,
                Estado = ticket.Estado,
               
            };
            await _context.SaveChangesAsync();
            var usuarioExiste = await _context.Usuario
                    .AnyAsync(u => u.IdUsuario == ticket.UsuarioSolicitante);
            //se crea en el historial
            var historialDto = new HistorialTicketDTO
            {
                IdTicket = ticket.IdTicket,
                FechaCambio = DateTime.Now,
               IdUsuarioCambio=ticket.UsuarioSolicitante,
                EstadoNuevo = nuevoEstado,
              
            };

            await _HistorialTicket.CreateHistorialTicket(historialDto);

        }

        public async Task<ICollection<TicketDTO>> GetByUserAsync(int usuarioId)
        {
            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(u => u.IdUsuario == usuarioId);

            if (usuario == null)
                return new List<TicketDTO>();

            IQueryable<Tickets> query = _context.Tickets;

            // Switch tradicional con valores de BD
            switch (usuario.IdRol)
            {
                case 1: // Administrador
                case 3: // Soporte/Técnico
                        // No aplicar filtro - ven todos los tickets
                    break;
                case 2: // Usuario normal
                default:
                    // Filtrar solo tickets del usuario
                    query = query.Where(t => t.UsuarioSolicitante == usuarioId);
                    break;
            }

            var tickets = await query.ToListAsync();
            return _mapper.Map<ICollection<TicketDTO>>(tickets);
        }
    }
    }


