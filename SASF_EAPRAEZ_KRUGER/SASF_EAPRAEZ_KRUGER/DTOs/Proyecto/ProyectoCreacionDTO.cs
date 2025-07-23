using SASF_EAPRAEZ_KRUGER.Utils;
using System.ComponentModel.DataAnnotations;

namespace SASF_EAPRAEZ_KRUGER.DTOs.Proyecto
{
    public class ProyectoCreacionDTO
    {

        [Required(ErrorMessage = "El nombre del proyecto es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres.")]
        public string Nombre { get; set; } = null!;


        [StringLength(500, ErrorMessage = "La descripción no debe exceder los 500 caracteres.")]
        public string? Descripcion { get; set; }


        [Required(ErrorMessage = "La fecha de inicio del proyecto es obligatoria.")]
        public DateOnly FechaInicio { get; set; }


        [Required(ErrorMessage = "La fecha de fin del proyecto es obligatoria.")]
        public DateOnly FechaFin { get; set; }


        [Required(ErrorMessage = "El estado es obligatorio.")]
        [StringLength(10, ErrorMessage = "El estado no debe exceder los 10 caracteres.")]
        [RegularExpression(Constantes.REGEX_ESTADO, ErrorMessage = $"El estado debe ser {Constantes.ESTADO_ACTIVO} o {Constantes.ESTADO_INACTIVO}.")]
        public string Estado { get; set; } = null!;


        [Required(ErrorMessage = "El identificador del usuario es obligatorio.")]
        public Guid UsuarioId { get; set; }

    }
}
