using System;
using System.Collections.Generic;

namespace EduNova.Infraestructure.Models;

public partial class Especialidades
{
    public int Idespecialidad { get; set; }

    public int IdCategoria { get; set; }

    public int IdEtiqueta { get; set; }

    public string NombreEspecialidad { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool? Activo { get; set; }

    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;
}
