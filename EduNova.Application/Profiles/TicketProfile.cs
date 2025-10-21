using AutoMapper;
using EduNova.Application.DTOs;
using EduNova.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Application.Profiles
{
    public class TicketProfile:Profile
    {
        public TicketProfile() {
            CreateMap<TicketDTO, Tickets>().ReverseMap();

            CreateMap<TicketDTO, Tickets>()
               .ForMember(dest => dest.IdTicket, opt => opt.MapFrom(src => src.IdTicket))
       .ForMember(dest => dest.IdCategoria, opt => opt.MapFrom(src => src.IdCategoria))
       .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo))
       .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
       .ForMember(dest => dest.IdSla, opt => opt.MapFrom(src => src.IdSla))
       .ForMember(dest => dest.UsuarioSolicitante, opt => opt.MapFrom(src => src.NombreSolicitante))
       .ForMember(dest => dest.Valoracion, opt => opt.MapFrom(src => src.valoracion))
       .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
       // Ignore navigation properties as they shouldn't be mapped from DTO
       .ForMember(dest => dest.IdCategoriaNavigation, opt => opt.Ignore())
       .ForMember(dest => dest.IdSlaNavigation, opt => opt.Ignore())
       .ForMember(dest => dest.UsuarioSolicitanteNavigation, opt => opt.Ignore())
       .ForMember(dest => dest.Imagenes, opt => opt.Ignore());
       //.ForMember(dest => dest.TicketHistorial, opt => opt.Ignore());
        }
    }
    //IdTicket = t.IdTicket,
    //                Titulo = t.Titulo,
    //                Descripcion = t.Descripcion,
    //                FechaCierre = t.FechaCierre,
    //                FechaCreacion = t.FechaCreacion,
    //                Prioridad = t.Prioridad,
    //                Estado = t.Estado,



    //                // Mapea propiedades de las navegaciones
    //                NombreCategoria = t.IdCategoriaNavigation.Nombre,
    //                NombreSolicitante = t.UsuarioSolicitanteNavigation.Nombre,
    //                NombreSla = t.IdSlaNavigation.Nombre,
    //                TiempoRespuesta = t.IdSlaNavigation.TiempoMaxRespuesta,
    //                TiempoResolucion = t.IdSlaNavigation.TiempoMaxResolucion

}
