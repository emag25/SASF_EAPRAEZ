using System.ComponentModel.DataAnnotations;

namespace SASF_EAPRAEZ_KRUGER.DTOs.Proyecto
{
    public class ProyectoDTO
    {

        [Required(ErrorMessage = "Identificador del proyecto es obligatorio.")]
        public Guid ProyectoId { get; set; }

    }
}
