using System;
using System.Collections.Generic;

namespace EduNova.Infraestructure.Models;

public partial class Matricula
{
    public int IdMatricula { get; set; }

    public int IdEstudiante { get; set; }

    public int IdCurso { get; set; }

    public DateTime FechaInscripcion { get; set; }

    public bool Estado { get; set; }

    public virtual Curso IdCursoNavigation { get; set; } = null!;

    public virtual Estudiante IdEstudianteNavigation { get; set; } = null!;

    public virtual ICollection<Nota> Nota { get; set; } = new List<Nota>();
}
