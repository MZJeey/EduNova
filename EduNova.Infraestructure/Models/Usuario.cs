using System;
using System.Collections.Generic;

namespace EduNova.Infraestructure.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public int IdRol { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<Curso> Curso { get; set; } = new List<Curso>();

    public virtual Rol IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Seguimiento> Seguimiento { get; set; } = new List<Seguimiento>();

    public virtual ICollection<Tickets> Tickets { get; set; } = new List<Tickets>();
}
