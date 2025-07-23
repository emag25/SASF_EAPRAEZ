using SASF_EAPRAEZ_KRUGER.Utils;
using System.ComponentModel.DataAnnotations;

namespace SASF_EAPRAEZ_KRUGER.DTOs.Actividad
{
    public class ActividadCreacionDTO
    {

        [Required(ErrorMessage = "El título de la actividad es obligatorio.")]
        [StringLength(100, ErrorMessage = "El título no debe exceder los 100 caracteres.")]
        public string Titulo { get; set; } = null!;


        [Required(ErrorMessage = "La descripción de la actividad es obligatoria.")]
        [StringLength(500, ErrorMessage = "La descripción no debe exceder los 500 caracteres.")]
        public string Descripcion { get; set; } = null!;


        [Required(ErrorMessage = "La fecha de la actividad es obligatoria.")]
        public DateOnly Fecha { get; set; }


        [Required(ErrorMessage = "Debe especificar las horas estimadas.")]
        [Range(1, Constantes.MAXIMO_HORAS_ESTIMADAS, ErrorMessage = "La cantidad de horas estimadas debe ser mayor a 0.")]
        public int HorasEstimadas { get; set; }


        [Required(ErrorMessage = "Debe especificar las horas reales.")]
        [Range(0, Constantes.MAXIMO_HORAS_REALES, ErrorMessage = "La cantidad de horas reales debe ser mayor o igual a 0.")]
        public int HorasReales { get; set; }


        [Required(ErrorMessage = "El identificador del proyecto es obligatorio.")]
        public Guid ProyectoId { get; set; }

    }
}
