using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Application.DTOs
{
    public class SLADTO
    {
        public int IdSla { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int TiempoRespuestaHoras { get; set; }
        public int TiempoResolucionHoras { get; set; }
    }
}
