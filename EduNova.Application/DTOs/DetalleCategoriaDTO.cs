using EduNova.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Application.DTOs
{
    public class DetalleCategoriaDTO
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Estado { get; set; }
        public string? Descripcion { get; set; }
        public int? IdSla { get; set; }
        public string? NombreSLA { get; set; }
        public int? TiempoRespuesta { get; set; }
        public int? TiempoResolucion { get; set; }

        public List<EtiquetaDTO> Etiquetas { get; set; } = new();


    }
}
