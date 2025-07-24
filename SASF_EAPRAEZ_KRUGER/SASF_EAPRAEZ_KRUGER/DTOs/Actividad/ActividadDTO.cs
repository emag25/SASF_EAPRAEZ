using SASF_EAPRAEZ_KRUGER.Utils;
using System.ComponentModel.DataAnnotations;

namespace SASF_EAPRAEZ_KRUGER.DTOs.Actividad
{
    public class ActividadDTO : ActividadCreacionDTO
    {

        [Required(ErrorMessage = "Debe especificar las horas reales.")]
        [Range(0, Constantes.MAXIMO_HORAS_REALES, ErrorMessage = "La cantidad de horas reales debe ser mayor o igual a 0.")]
        public int HorasReales { get; set; }


        [Required(ErrorMessage = "El identificador de la actividad es obligatorio.")]
        public Guid ActividadId { get; set; }

    }
}
