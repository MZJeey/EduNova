using System;
using System.Collections.Generic;

namespace EduNova.Infraestructure.Models;

public partial class Etiqueta
{
    public int IdEtiqueta { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<Categoria> IdCategoria { get; set; } = new List<Categoria>();
}
