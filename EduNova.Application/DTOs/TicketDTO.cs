using EduNova.Application.DTOs;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Application.DTOs
{
    public class TicketDTO
    {
        public int IdTicket { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaCierre { get; set; }
        public string Estado { get; set; }
        public string Prioridad { get; set; } = null!;
        public string? valoracion { get; set; }
        public int UsuarioSolicitante { get; set; }
        public int IdCategoria { get; set; }
        
        public string? NombreCategoria { get; set; }
        public string? NombreSla { get; set; }
        public string? NombreSolicitante { get; set; }
        public int? TiempoRespuesta { get; set; }
        public int? TiempoResolucion { get; set; }
        public List<ImagenDTO>? Imagenes { get; set; }

        [Display(Name = "Imágenes del tickets")]
        public List<IFormFile>? ImagenesArchivo { get; set; } // Nombre corregido

        public double HorasTranscurridas
        {
            get
            {
                if (FechaCierre.HasValue)
                {
                    // Ticket cerrado: tiempo total que tomó
                    return (FechaCierre.Value - FechaCreacion).TotalHours;
                }
                else
                {
                    // Ticket abierto: tiempo desde creación hasta ahora
                    return (DateTime.Now - FechaCreacion).TotalHours;
                }
            }
        }
        public double ProgresoTiempoRespuesta
        {
            get
            {
                if (TiempoRespuesta == null || TiempoRespuesta == 0)
                    return 0;

                double progreso = (HorasTranscurridas / TiempoRespuesta.Value) * 100;
                return Math.Min(Math.Round(progreso, 2), 100); // Máximo 100%
            }
        }

        public double ProgresoTiempoResolucion
        {
            get
            {
                if (TiempoResolucion == null || TiempoResolucion == 0)
                    return 0;

                double progreso = (HorasTranscurridas / TiempoResolucion.Value) * 100;
                return Math.Min(Math.Round(progreso, 2), 100); // Máximo 100%
            }
        }

        // 🔹 CORRECCIÓN: Lógica mejorada del estado temporal
        public string EstadoTemporalSLA
        {
            get
            {
                if (TiempoRespuesta == null || TiempoRespuesta == 0)
                    return "SLA no definido";

                if (HorasTranscurridas <= TiempoRespuesta)
                    return "En periodo de respuesta";
                else if (TiempoResolucion == null || TiempoResolucion == 0)
                    return "Periodo de respuesta vencido";
                else if (HorasTranscurridas <= TiempoResolucion)
                    return "En periodo de resolución";
                else
                    return "Fuera del SLA";
            }
        }

        // 🔹 NUEVA: Propiedad para saber si está fuera del SLA
        public bool EstaFueraSLA =>
            TiempoResolucion.HasValue && TiempoResolucion > 0 &&
            HorasTranscurridas > TiempoResolucion.Value;
    }
    }
