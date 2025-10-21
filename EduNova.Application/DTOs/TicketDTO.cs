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
        // Lista de todas las imágenes
        public List<ImagenDTO>? Imagenes { get; set; }


        public int horasRestantes()
        {
            if (TiempoResolucion == null)
                return 0; // No hay SLA definido

            // Si el ticket ya está cerrado, el tiempo restante es 0
            // PERO solo si el estado es "Cerrado" o similar
            if (Estado?.ToLower() == "cerrado" || Estado?.ToLower() == "completado")
                return 0;

            // Calculamos la fecha límite sumando las horas del SLA a la fecha de creación
            DateTime fechaLimite = FechaCreacion.AddHours(TiempoResolucion.Value);

            // Calculamos la diferencia con la fecha actual
            TimeSpan tiempoRestante = fechaLimite - DateTime.Now;

            // Devolvemos las horas restantes (puede ser negativo si está vencido)
            return (int)tiempoRestante.TotalHours;
        }



    }
}
