using System;
using System.Collections.Generic;

namespace EduNova.Infraestructure.Models;

public partial class Nota
{
    public int IdNota { get; set; }

    public int IdMatricula { get; set; }

    public string TipoEvaluacion { get; set; } = null!;

    public decimal Valor { get; set; }

    public DateTime Fecha { get; set; }

    public virtual Matricula IdMatriculaNavigation { get; set; } = null!;
}
