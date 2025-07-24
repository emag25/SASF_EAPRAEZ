using SASF_EAPRAEZ_KRUGER.Utils;
using System.ComponentModel.DataAnnotations;

namespace SASF_EAPRAEZ_KRUGER.DTOs.Usuario
{
    public class UsuarioCreacionDTO
    {

        [Required(ErrorMessage = "El nombre del usuario es obligatorio.")]
        [StringLength(200, ErrorMessage = "El nombre del usuario no debe exceder los 200 caracteres.")]
        public string NombreCompleto { get; set; } = null!;


        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [StringLength(100, ErrorMessage = "El correo electrónico no debe exceder los 100 caracteres.")]
        [RegularExpression(Constantes.REGEX_CORREO, ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string Correo { get; set; } = null!;


        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [RegularExpression(Constantes.REGEX_NUMERO_LONGITUD_10, ErrorMessage = "El teléfono debe contener 10 dígitos numérticos.")]
        public string Telefono { get; set; } = null!;


        [Required(ErrorMessage = "El rol es obligatorio.")]
        [RegularExpression(@"^(ADMIN|EDITOR|VIEWER)$", ErrorMessage = "El rol debe ser ADMIN, EDITOR o VIEWER.")]
        public string Rol { get; set; } = null!;

    }

}
