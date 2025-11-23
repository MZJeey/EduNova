using System;
using System.Collections.Generic;

namespace EduNova.Infraestructure.Models;

public partial class Rol
{
    public int IdRol { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Tickets> Tickets { get; set; } = new List<Tickets>();

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
