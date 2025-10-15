using AutoMapper;
using EduNova.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Application.Profiles
{
    public class DetalleCategoriaProfile: Profile
    {
        public DetalleCategoriaProfile() {
            CreateMap<DetalleCategoriaDTO, DetalleCategoriaDTO>().ReverseMap();
        }
    }
}
