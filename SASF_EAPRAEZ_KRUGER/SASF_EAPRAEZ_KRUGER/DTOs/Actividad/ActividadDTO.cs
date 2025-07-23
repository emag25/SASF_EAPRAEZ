using System.ComponentModel.DataAnnotations;

namespace SASF_EAPRAEZ_KRUGER.DTOs.Actividad
{
    public class ActividadDTO : ActividadCreacionDTO
    {

        [Required(ErrorMessage = "El identificador de la actividad es obligatorio.")]
        public Guid ActividadId { get; set; }

    }
}
