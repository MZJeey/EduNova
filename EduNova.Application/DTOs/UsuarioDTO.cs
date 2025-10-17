using EduNova.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Application.DTOs
{
    public record UsuarioDTO
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public int idRol { get; set; }
        public bool Estado { get; set; }
        public string RolNombre => ((RolUsuario)idRol).ToString();
    }
    public enum RolUsuario
    {
        Administrador = 1,
        Docente = 2,
        Técnico = 3
    }
}
