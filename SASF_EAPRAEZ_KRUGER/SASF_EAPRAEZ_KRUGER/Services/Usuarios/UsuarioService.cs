using SASF_EAPRAEZ_KRUGER.DTOs.Usuario;
using SASF_EAPRAEZ_KRUGER.Entities;
using SASF_EAPRAEZ_KRUGER.Exceptions.BadRequest;
using SASF_EAPRAEZ_KRUGER.Exceptions.NotFound;
using SASF_EAPRAEZ_KRUGER.Mappers;
using SASF_EAPRAEZ_KRUGER.Repositories.Usuarios;
using SASF_EAPRAEZ_KRUGER.Utils;

namespace SASF_EAPRAEZ_KRUGER.Services.Usuarios
{
    public class UsuarioService : IUsuarioService
    {

        private readonly IUsuarioRepository _usuarioRepository;


        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }


        public async Task<List<UsuarioDTO>> ObtenerTodosAsync()
        {

            List<Usuario> usuariosList = await _usuarioRepository.ConsultarUsuariosAsync();

            List<UsuarioDTO> usuarioDTOList = new();

            foreach (Usuario entidad in usuariosList)
            {
                usuarioDTOList.Add(UsuarioMapper.ToDto(entidad));
            }

            return usuarioDTOList;

        }


        public async Task<UsuarioDTO> InsertarUsuarioAsync(UsuarioCreacionDTO dto)
        {

            if (await _usuarioRepository.ExisteUsuarioPorCorreoAsync(dto.Correo))
            {
                throw new UniqueFieldException("correo", $"Ya existe un usuario con el correo {dto.Correo}.");
            }

            if (await _usuarioRepository.ExisteUsuarioPorTelefonoAsync(dto.Telefono))
            {
                throw new UniqueFieldException("teléfono", $"Ya existe un usuario con el teléfono {dto.Telefono}.");
            }

            Usuario usuario = UsuarioMapper.ToEntity(dto);

            await _usuarioRepository.InsertarAsync(usuario);

            return UsuarioMapper.ToDto(usuario);

        }


        public async Task ActualizarUsuarioAsync(Guid id, UsuarioDTO dto)
        {

            if (id != dto.UsuarioId)
            {
                throw new InvalidIdException("El ID de la URL no coincide con el del cuerpo.");
            }

            Usuario? usuario = await _usuarioRepository.ConsultarUsuarioPorIdAsync(dto.UsuarioId);

            if (usuario is null)
            {
                throw new RegisterNotFoundException("El usuario no existe.");
            }

            if (await _usuarioRepository.ExisteUsuarioPorCorreoAsync(dto.Correo))
            {
                throw new UniqueFieldException("correo", $"Ya existe un usuario con el correo {dto.Correo}.");
            }

            if (await _usuarioRepository.ExisteUsuarioPorTelefonoAsync(dto.Telefono))
            {
                throw new UniqueFieldException("teléfono", $"Ya existe un usuario con el teléfono {dto.Telefono}.");
            }

            usuario.NombreCompleto = dto.NombreCompleto;
            usuario.Correo = dto.Correo;
            usuario.Telefono = dto.Telefono;
            usuario.Rol = dto.Rol;
            usuario.FechaModificacion = DateTime.UtcNow;

            await _usuarioRepository.ActualizarAsync(usuario);

        }


        public async Task EliminarUsuarioAsync(Guid id)
        {

            Usuario? usuario = await _usuarioRepository.ConsultarUsuarioPorIdAsync(id);

            if (usuario is null)
            {
                throw new RegisterNotFoundException("El usuario no existe.");
            }

            usuario.Estado = Constantes.ESTADO_ELIMINADO;
            usuario.FechaModificacion = DateTime.UtcNow;

            await _usuarioRepository.ActualizarAsync(usuario);

        }

    }
}