using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Application.DTOs
{
    public class TicketDTO
    {
      public int IdTicket { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaCierre { get; set; }
        public string Estado { get; set; }
        public string Prioridad { get; set; } = null!;
        public string valoracion { get; set; } = null!; 
        public int UsuarioSolicitante { get; set; }
        public int IdCategoria { get; set; }
        
        public int IdSla { get; set; }

        public string? NombreCategoria { get; set; }
        public string? NombreSla { get; set; }
        public string? NombreSolicitante { get; set; }
        public int? TiempoRespuesta { get; set; }
        public int? TiempoResolucion { get; set; }
        


    }
}
