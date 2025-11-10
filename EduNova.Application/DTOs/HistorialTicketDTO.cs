using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Application.DTOs
{
    public class HistorialTicketDTO
    {
        public int IdHistorial { get; set; }
        public int IdTicket { get; set; }
        public string observaciones { get; set; } = null!;
        public string EstadoNuevo { get; set; } = null!;
        public string EstadoTickets { get; set; } = null!;
        public DateTime FechaCambio { get; set; }
        public int IdUsuarioCambio { get; set; }
       

    }
}
