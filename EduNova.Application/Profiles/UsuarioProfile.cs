using AutoMapper;
using EduNova.Application.DTOs;
using EduNova.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Application.Profiles
{

    // se agraga la referencia a AutoMapper
    public class UsuarioProfile :Profile
    {
       public UsuarioProfile()
        {
            // Mapeos de AutoMapper
            CreateMap<UsuarioDTO, Usuario>().ReverseMap();
        }

       
    }
}
