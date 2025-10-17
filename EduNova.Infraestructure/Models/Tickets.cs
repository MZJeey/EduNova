using System;
using System.Collections.Generic;

namespace EduNova.Infraestructure.Models;

public partial class Tickets
{
    public int IdTicket { get; set; }

    public string Titulo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaCierre { get; set; }

    public string Prioridad { get; set; } = null!;

    public int UsuarioSolicitante { get; set; }

    public int IdCategoria { get; set; }

    public int IdSla { get; set; }

    public string? Valoracion { get; set; }

    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

    public virtual Sla IdSlaNavigation { get; set; } = null!;

    public virtual ICollection<Imagenes> Imagenes { get; set; } = new List<Imagenes>();

    public virtual ICollection<TicketHistorial> TicketHistorial { get; set; } = new List<TicketHistorial>();

    public virtual Usuario UsuarioSolicitanteNavigation { get; set; } = null!;
}
