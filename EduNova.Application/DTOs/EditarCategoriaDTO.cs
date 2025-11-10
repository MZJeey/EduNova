using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Application.DTOs
{
    public class EditarCategoriaDTO: CrearCategoriaDTO

    {
        public int IdCategoria { get; set; }
        public bool Estado { get; set; } = true;

    }
}
