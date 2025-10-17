using System;
using System.Collections.Generic;

namespace EduNova.Infraestructure.Models;

public partial class Imagenes
{
    public int IdImagen { get; set; }

    public string? Imagen { get; set; }

    public int? IdTicket { get; set; }

    public virtual Tickets? IdTicketNavigation { get; set; }
}
