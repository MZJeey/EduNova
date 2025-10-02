using System;
using System.Collections.Generic;

namespace EduNova.Infraestructure.Models;

public partial class Sla
{
    public int IdSla { get; set; }

    public string Nombre { get; set; } = null!;

    public int TiempoMaxRespuesta { get; set; }

    public int TiempoMaxResolucion { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Categoria> Categoria { get; set; } = new List<Categoria>();

    public virtual ICollection<Tickets> Tickets { get; set; } = new List<Tickets>();
}
