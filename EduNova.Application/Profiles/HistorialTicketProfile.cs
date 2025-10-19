using AutoMapper;
using EduNova.Application.DTOs;
using EduNova.Infraestructure.Models;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Application.Profiles
{
    public class HistorialTicketProfile: Profile
    {
        public HistorialTicketProfile()
        {
            // Mapeos de AutoMapper
            CreateMap<HistorialTicketDTO, TicketHistorial>().ReverseMap();
              CreateMap<HistorialTicketDTO, TicketHistorial>()
                .ForMember(dest => dest.IdHistorial, orig => orig.MapFrom(src => src.IdHistorial))
                .ForMember(dest => dest.IdTicket, orig => orig.MapFrom(src => src.IdTicket) )
                .ForMember(dest => dest.Observaciones, orig => orig.MapFrom(src => src.observaciones))
                .ForMember(dest => dest.EstadoTickets, orig => orig.MapFrom(src => src.EstadoNuevo))
                .ForMember(dest => dest.FechaCambio, orig => orig.MapFrom(src => src.FechaCambio))
                .ForMember(dest => dest.IdUsuarioCambio, orig => orig.MapFrom(src => src.IdUsuarioCambio));
                

        }
    }


    //CreateMap<LibroDTO, Libro>()
    //       .ForMember(dest => dest.IdLibro, orig => orig.MapFrom(o => o.IdLibro))
    //       .ForMember(dest => dest.Isbn, orig => orig.MapFrom(o => o.Isbn))
    //       .ForMember(dest => dest.IdAutor, orig => orig.MapFrom(o => o.IdAutor))
    //       .ForMember(dest => dest.Nombre, orig => orig.MapFrom(o => o.Nombre))
    //       .ForMember(dest => dest.Precio, orig => orig.MapFrom(o => o.Precio))
    //       .ForMember(dest => dest.Cantidad, orig => orig.MapFrom(o => o.Cantidad))
    //       .ForMember(dest => dest.Imagen, orig => orig.MapFrom(o => o.Imagen))
    //        .ForMember(dest => dest.IdAutorNavigation, orig => orig.MapFrom(o => o.IdAutorNavigation))
    //        .ForMember(dest => dest.OrdenDetalle, orig => orig.MapFrom(o => o.OrdenDetalle))
    //        .ForMember(dest => dest.IdCategoria, orig => orig.MapFrom(o => o.IdCategoria));
}
