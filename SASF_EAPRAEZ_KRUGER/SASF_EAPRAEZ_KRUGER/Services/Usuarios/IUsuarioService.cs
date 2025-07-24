using SASF_EAPRAEZ_KRUGER.DTOs.Usuario;

namespace SASF_EAPRAEZ_KRUGER.Services.Usuarios
{
    public interface IUsuarioService
    {

        Task<List<UsuarioDTO>> ConsultarUsuariosAsync();

        Task<UsuarioDTO> InsertarUsuarioAsync(UsuarioCreacionDTO usuarioCreacionDTO);

        Task ActualizarUsuarioAsync(Guid id, UsuarioDTO usuarioDTO);

        Task EliminarUsuarioAsync(Guid id);

    }
}
