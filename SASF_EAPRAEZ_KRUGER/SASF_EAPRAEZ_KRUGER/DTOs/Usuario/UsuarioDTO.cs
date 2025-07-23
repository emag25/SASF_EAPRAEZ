using System.ComponentModel.DataAnnotations;

namespace SASF_EAPRAEZ_KRUGER.DTOs.Usuario
{
    public class UsuarioDTO : UsuarioCreacionDTO
    {

        [Required(ErrorMessage = "El identificador del usuario es obligatorio.")]
        public Guid UsuarioId { get; set; }

    }
}
