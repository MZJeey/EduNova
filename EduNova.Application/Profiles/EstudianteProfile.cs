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
    public class EstudianteProfile : Profile
    {
        public EstudianteProfile()
        {
            // Mapeos de AutoMapper
            CreateMap<EstudianteDTO, Estudiante>().ReverseMap();
        }
    }
}
