using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Application.DTOs
{
    public class EspecialidadesDTO
    {
        public int IdEspecialidad { get; set; }
        public string NombreEspecialidad { get; set; } = null!;
        public bool Estado { get; set; }
        public string? Descripcion { get; set; }
        
        public  int? IdCategoria { get; set; }
    }
}
