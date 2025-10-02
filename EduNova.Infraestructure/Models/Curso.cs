using System;
using System.Collections.Generic;

namespace EduNova.Infraestructure.Models;

public partial class Curso
{
    public int IdCurso { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int IdProfesor { get; set; }

    public bool Estado { get; set; }

    public virtual Usuario IdProfesorNavigation { get; set; } = null!;

    public virtual ICollection<Matricula> Matricula { get; set; } = new List<Matricula>();

    public virtual ICollection<Planeamiento> Planeamiento { get; set; } = new List<Planeamiento>();
}
