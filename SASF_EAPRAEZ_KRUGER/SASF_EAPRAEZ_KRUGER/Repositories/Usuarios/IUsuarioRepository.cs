
using SASF_EAPRAEZ_KRUGER.Entities;
using SASF_EAPRAEZ_KRUGER.Repositories.Generic;

namespace SASF_EAPRAEZ_KRUGER.Repositories.Usuarios
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {

        Task<List<Usuario>> ConsultarUsuariosAsync();

        Task<Usuario?> ConsultarUsuarioPorIdAsync(Guid id);

        Task<bool> ExisteUsuarioPorTelefonoAsync(string correo);

        Task<bool> ExisteUsuarioPorCorreoAsync(string telefono);

    }
}
