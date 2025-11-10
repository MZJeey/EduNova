using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Application.DTOs
{
    public class CrearCategoriaDTO
    {
        public string Nombre { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Descripcion { get; set; }

      
        public int? IdSla { get; set; }

       
        [Range(1, int.MaxValue, ErrorMessage = "El tiempo de respuesta debe ser mayor que 0.")]
        public int? TiempoRespuesta { get; set; } 

        [Range(1, int.MaxValue, ErrorMessage = "El tiempo de resolución debe ser mayor que 0.")]
        public int? TiempoResolucion { get; set; } 

       
        public List<int> EtiquetaIds { get; set; } = new();
        public List<int> EspecialidadIds { get; set; } = new();



    }
}
