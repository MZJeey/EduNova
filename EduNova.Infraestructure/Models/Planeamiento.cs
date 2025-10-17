using System;
using System.Collections.Generic;

namespace EduNova.Infraestructure.Models;

public partial class Planeamiento
{
    public int IdPlaneamiento { get; set; }

    public int IdCurso { get; set; }

    public int Semana { get; set; }

    public string Actividad { get; set; } = null!;

    public string Objetivo { get; set; } = null!;

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public virtual Curso IdCursoNavigation { get; set; } = null!;
}
