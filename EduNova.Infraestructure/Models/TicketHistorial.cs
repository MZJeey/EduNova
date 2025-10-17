using System;
using System.Collections.Generic;

namespace EduNova.Infraestructure.Models;

public partial class TicketHistorial
{
    public int IdHistorial { get; set; }

    public int? IdTicket { get; set; }

    public string? Observaciones { get; set; }

    public DateTime? FechaCambio { get; set; }

    public int? IdUsuarioCambio { get; set; }

    public string? EstadoTickets { get; set; }

    public virtual Tickets? IdTicketNavigation { get; set; }

    public virtual Usuario? IdUsuarioCambioNavigation { get; set; }
}
