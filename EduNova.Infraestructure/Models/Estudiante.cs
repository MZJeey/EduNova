using System;
using System.Collections.Generic;

namespace EduNova.Infraestructure.Models;

public partial class Estudiante
{
    public int IdEstudiante { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public DateOnly? FechaNacimiento { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<Matricula> Matricula { get; set; } = new List<Matricula>();

    public virtual ICollection<Seguimiento> Seguimiento { get; set; } = new List<Seguimiento>();
}
