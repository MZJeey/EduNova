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
        private readonly IMapper _mapper;
        private readonly eduNovaContext _context;
        public ServiceTickets(IRepositoryTickets repositoryTickets,IServiceImagen ServiceImagen, IMapper mapper, eduNovaContext eduNovaContext)
        {
            _repository = repositoryTickets;
           _ServiceImagen = ServiceImagen;
            _mapper = mapper;
            _context = eduNovaContext;
        }
        public Task<int> AddAsync(Tickets entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(TicketDTO entity)
        {
            throw new NotImplementedException();
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

        //public async Task<TicketDTO> FindByIdAsync(int id)
        //{
        //    var ticket = await _context.Tickets.Include
        //        (c => c.IdCategoriaNavigation)
        //        .Include(c => c.IdCategoriaNavigation.Nombre)
        //         .Include(c => c.UsuarioSolicitanteNavigation)
        //         .Include(c => c.IdSlaNavigation)
        //         .Include(c => c.IdSlaNavigation.Nombre)
        //          .Include(c => c.IdSlaNavigation.TiempoMaxRespuesta)
        //          .Include(c => c.IdSlaNavigation.TiempoMaxResolucion)



        //         .FirstOrDefaultAsync(c => c.IdTicket == id);
        //    return _mapper.Map<TicketDTO>(ticket);


        //}
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
                    NombreSla = t.IdSlaNavigation.Nombre,
                    TiempoRespuesta = t.IdSlaNavigation.TiempoMaxRespuesta,
                    TiempoResolucion = t.IdSlaNavigation.TiempoMaxResolucion,
                   

                })
                .FirstOrDefaultAsync();
            if (ticketDTO != null)

            {
                               ticketDTO.Imagenes = await _ServiceImagen.FindByIdAsync(ticketDTO.IdTicket);
            }
            return ticketDTO;
            //var ticket = await _context.Tickets
            //   .Include(t => t.IdCategoriaNavigation)
            //   .Include(t => t.UsuarioSolicitanteNavigation)
            //   .Include(t => t.IdSlaNavigation)

            //   .Include(t => t.Imagenes)
            //   .FirstOrDefaultAsync(t => t.IdTicket == id);
            //var ticketDTO = _mapper.Map<TicketDTO>(ticket);
            //ticketDTO.Imagenes = await _ServiceImagen.FindByIdAsync(ticket.IdTicket);



            //return ticketDTO;
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
                NombreSla = t.IdSlaNavigation?.Nombre ?? "Sin SLA",
                TiempoRespuesta = t.IdSlaNavigation?.TiempoMaxRespuesta,
                TiempoResolucion = t.IdSlaNavigation?.TiempoMaxResolucion,
                FechaCreacion = t.FechaCreacion,
                FechaCierre = t.FechaCierre,
    


            }).ToList();

            return ticketsDto;
        }



        public Task UpdateAsync(Tickets entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TicketDTO entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateTicketStatusAsync(int ticketId, string nuevoEstado)
        {
            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(t => t.IdTicket== ticketId);

        

            ticket.Estado = nuevoEstado;
            

            await _context.SaveChangesAsync();

            // Usar AutoMapper para convertir a DTO

            var ticketDto = new TicketDTO
            {
                IdTicket = ticket.IdTicket,
                Estado = ticket.Estado,
                // asigna otros campos que necesites
            };

           
        }
    }
}
