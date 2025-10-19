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
    public class ImagenProfile:Profile
    {
        public ImagenProfile()
        {
            CreateMap<ImagenDTO, Imagenes>().ReverseMap();

        }
    }
}
