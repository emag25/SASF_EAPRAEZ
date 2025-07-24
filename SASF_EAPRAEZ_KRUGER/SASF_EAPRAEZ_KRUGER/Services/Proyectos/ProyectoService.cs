using SASF_EAPRAEZ_KRUGER.DTOs.Proyecto;
using SASF_EAPRAEZ_KRUGER.Entities;
using SASF_EAPRAEZ_KRUGER.Exceptions.BadRequest;
using SASF_EAPRAEZ_KRUGER.Exceptions.NotFound;
using SASF_EAPRAEZ_KRUGER.Mappers;
using SASF_EAPRAEZ_KRUGER.Repositories.Proyectos;
using SASF_EAPRAEZ_KRUGER.Repositories.Usuarios;
using SASF_EAPRAEZ_KRUGER.Utils;

namespace SASF_EAPRAEZ_KRUGER.Services.Proyectos
{
    public class ProyectoService : IProyectoService
    {

        private readonly IProyectoRepository _proyectoRepository;
        private readonly IUsuarioRepository _usuarioRepository;


        public ProyectoService(IProyectoRepository proyectoRepository, IUsuarioRepository usuarioRepository)
        {
            _proyectoRepository = proyectoRepository;
            _usuarioRepository = usuarioRepository;
        }


        public async Task<List<ProyectoDTO>> ConsultarProyectosAsync()
        {

            List<Proyecto> proyectosList = await _proyectoRepository.ConsultarProyectosAsync();

            List<ProyectoDTO> proyectoDTOList = new();

            foreach (Proyecto entidad in proyectosList)
            {
                proyectoDTOList.Add(ProyectoMapper.ToDto(entidad));
            }

            return proyectoDTOList;

        }


        public async Task<ProyectoDTO> InsertarProyectoAsync(ProyectoCreacionDTO dto)
        {

            if (dto.FechaInicio < DateOnly.FromDateTime(DateTime.UtcNow))
            {
                throw new InvalidFieldException($"La fecha de inicio del proyecto debe ser mayor o igual a la actual.");
            }

            if (dto.FechaFin < dto.FechaInicio)
            {
                throw new InvalidFieldException($"La fecha de inicio del proyecto debe ser mayor a la fecha de fin.");
            }

            if (!await _usuarioRepository.ExisteUsuarioPorIDAsync(dto.UsuarioId))
            {
                throw new RegisterNotFoundException($"No existe usuario con identificador {dto.UsuarioId}.");
            }

            Proyecto proyecto = ProyectoMapper.ToEntity(dto);

            await _proyectoRepository.InsertarAsync(proyecto);

            return ProyectoMapper.ToDto(proyecto);

        }


        public async Task ActualizarProyectoAsync(Guid id, ProyectoDTO dto)
        {

            if (id != dto.ProyectoId)
            {
                throw new InvalidIdException("El ID de la URL no coincide con el del cuerpo.");
            }

            Proyecto? proyecto = await _proyectoRepository.ConsultarProyectoPorIdAsync(dto.ProyectoId);

            if (proyecto is null)
            {
                throw new RegisterNotFoundException("El proyecto no existe.");
            }


            bool fechaModificada = proyecto.FechaInicio != dto.FechaInicio;
            bool fechaEsMenorFechaActual = dto.FechaInicio < DateOnly.FromDateTime(DateTime.UtcNow);

            if (fechaModificada && fechaEsMenorFechaActual)
            {
                throw new InvalidFieldException($"La fecha de inicio del proyecto debe ser mayor o igual a la actual.");
            }

            if (dto.FechaFin < dto.FechaInicio)
            {
                throw new InvalidFieldException($"La fecha de inicio del proyecto debe ser mayor a la fecha de fin.");
            }

            if (!await _usuarioRepository.ExisteUsuarioPorIDAsync(dto.UsuarioId))
            {
                throw new RegisterNotFoundException($"No existe usuario con identificador {dto.UsuarioId}.");
            }

            proyecto.Nombre = dto.Nombre;
            proyecto.Descripcion = dto.Descripcion;
            proyecto.FechaInicio = dto.FechaInicio;
            proyecto.FechaFin = dto.FechaFin;
            proyecto.Estado = dto.Estado;
            proyecto.UsuarioId = dto.UsuarioId;
            proyecto.FechaModificacion = DateTime.UtcNow;

            await _proyectoRepository.ActualizarAsync(proyecto);

        }


        public async Task EliminarProyectoAsync(Guid id)
        {

            Proyecto? proyecto = await _proyectoRepository.ConsultarProyectoPorIdAsync(id);

            if (proyecto is null)
            {
                throw new RegisterNotFoundException("El proyecto no existe.");
            }

            proyecto.Estado = Constantes.ESTADO_ELIMINADO;
            proyecto.FechaModificacion = DateTime.UtcNow;

            await _proyectoRepository.ActualizarAsync(proyecto);

        }

    }
}