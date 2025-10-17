using System;
using System.Collections.Generic;

namespace EduNova.Infraestructure.Models;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public bool Estado { get; set; }

    public int IdSla { get; set; }

    public virtual ICollection<Especialidades> Especialidades { get; set; } = new List<Especialidades>();

    public virtual ICollection<Etiqueta> Etiqueta { get; set; } = new List<Etiqueta>();

    public virtual Sla IdSlaNavigation { get; set; } = null!;

    public virtual ICollection<Tickets> Tickets { get; set; } = new List<Tickets>();

    public virtual ICollection<Etiqueta> IdEtiqueta { get; set; } = new List<Etiqueta>();
}
