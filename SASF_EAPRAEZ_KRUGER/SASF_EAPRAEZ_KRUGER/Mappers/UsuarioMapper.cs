using SASF_EAPRAEZ_KRUGER.DTOs.Usuario;
using SASF_EAPRAEZ_KRUGER.Entities;
using SASF_EAPRAEZ_KRUGER.Utils;

namespace SASF_EAPRAEZ_KRUGER.Mappers
{
    public class UsuarioMapper
    {

        public static UsuarioDTO ToDto(Usuario entity)
        {
            return new UsuarioDTO
            {
                UsuarioId = entity.UsuarioId,
                NombreCompleto = entity.NombreCompleto,
                Correo = entity.Correo,
                Telefono = entity.Telefono,
                Rol = entity.Rol
            };
        }


        public static Usuario ToEntity(UsuarioCreacionDTO dto)
        {
            return new Usuario
            {
                NombreCompleto = dto.NombreCompleto,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                Rol = dto.Rol,
                Estado = Constantes.ESTADO_ACTIVO
            };
        }


    }
}
