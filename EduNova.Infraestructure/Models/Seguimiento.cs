using System;
using System.Collections.Generic;

namespace EduNova.Infraestructure.Models;

public partial class Seguimiento
{
    public int IdSeguimiento { get; set; }

    public int IdEstudiante { get; set; }

    public string Descripcion { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public int IdProfesor { get; set; }

    public virtual Estudiante IdEstudianteNavigation { get; set; } = null!;

    public virtual Usuario IdProfesorNavigation { get; set; } = null!;
}
