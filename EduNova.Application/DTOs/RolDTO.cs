using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Application.DTOs
{
    public class RolDTO
    {
        public int IdRol { get; set; }

        public string Descripcion { get; set; } = null!;

        public virtual List<UsuarioDTO> Usuario { get; set; } = null!;
    }
}
