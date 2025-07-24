using SASF_EAPRAEZ_KRUGER.DTOs.Actividad;
using SASF_EAPRAEZ_KRUGER.Entities;
using SASF_EAPRAEZ_KRUGER.Exceptions.BadRequest;
using SASF_EAPRAEZ_KRUGER.Exceptions.NotFound;
using SASF_EAPRAEZ_KRUGER.Mappers;
using SASF_EAPRAEZ_KRUGER.Repositories.Actividades;
using SASF_EAPRAEZ_KRUGER.Repositories.Proyectos;
using SASF_EAPRAEZ_KRUGER.Utils;

namespace SASF_EAPRAEZ_KRUGER.Services.Actividades
{
    public class ActividadService : IActividadService
    {

        private readonly IActividadRepository _actividadRepository;
        private readonly IProyectoRepository _proyectoRepository;


        public ActividadService(IActividadRepository actividadRepository, IProyectoRepository proyectoRepository)
        {
            _actividadRepository = actividadRepository;
            _proyectoRepository = proyectoRepository;
        }


        public async Task<List<ActividadDTO>> ConsultarActividadesAsync()
        {

            List<Actividad> actividadesList = await _actividadRepository.ConsultarActividadesAsync();

            List<ActividadDTO> actividadDTOList = new();

            foreach (Actividad entidad in actividadesList)
            {
                actividadDTOList.Add(ActividadMapper.ToDto(entidad));
            }

            return actividadDTOList;

        }


        public async Task<ActividadDTO> InsertarActividadAsync(ActividadCreacionDTO dto)
        {

            if (dto.Fecha < DateOnly.FromDateTime(DateTime.UtcNow))
            {
                throw new InvalidFieldException($"La fecha de inicio del actividad debe ser mayor o igual a la actual.");
            }

            Proyecto? proyecto = await _proyectoRepository.ConsultarProyectoPorIdAsync(dto.ProyectoId);

            if (proyecto is null)
            {
                throw new RegisterNotFoundException($"No existe proyecto con identificador {dto.ProyectoId}.");
            }

            if (proyecto.Estado.Equals(Constantes.ESTADO_INACTIVO))
            {
                throw new InvalidFieldException($"El proyecto debe estar activo.");
            }

            if (dto.Fecha < proyecto.FechaInicio)
            {
                throw new InvalidFieldException($"La fecha de la actividad se encuentra fuera del rango de fecha del proyecto.");
            }

            DateTime fechaFinActividad = dto.Fecha.ToDateTime(TimeOnly.MinValue).AddHours(dto.HorasEstimadas);

            if (DateOnly.FromDateTime(fechaFinActividad) > proyecto.FechaFin)
            {
                throw new InvalidFieldException($"La fecha de la actividad se encuentra fuera del rango de fecha del proyecto.");
            }

            Actividad actividad = ActividadMapper.ToEntity(dto);

            await _actividadRepository.InsertarAsync(actividad);

            return ActividadMapper.ToDto(actividad);

        }


        public async Task ActualizarActividadAsync(Guid id, ActividadDTO dto)
        {

            if (id != dto.ActividadId)
            {
                throw new InvalidIdException("El ID de la URL no coincide con el del cuerpo.");
            }

            Actividad? actividad = await _actividadRepository.ConsultarActividadPorIdAsync(dto.ActividadId);

            if (actividad is null)
            {
                throw new RegisterNotFoundException("El actividad no existe.");
            }


            bool fechaModificada = actividad.Fecha != dto.Fecha;
            bool fechaEsMenorFechaActual = dto.Fecha < DateOnly.FromDateTime(DateTime.UtcNow);

            if (fechaModificada && fechaEsMenorFechaActual)
            {
                throw new InvalidFieldException($"La fecha de inicio del actividad debe ser mayor o igual a la actual.");
            }

            Proyecto? proyecto = await _proyectoRepository.ConsultarProyectoPorIdAsync(dto.ProyectoId);

            if (proyecto is null)
            {
                throw new RegisterNotFoundException($"No existe proyecto con identificador {dto.ProyectoId}.");
            }

            if (proyecto.Estado.Equals(Constantes.ESTADO_INACTIVO))
            {
                throw new InvalidFieldException($"El proyecto debe estar activo.");
            }

            if (dto.Fecha < proyecto.FechaInicio)
            {
                throw new InvalidFieldException($"La fecha de la actividad se encuentra fuera del rango de fecha del proyecto.");
            }

            DateTime fechaFinActividad = dto.Fecha.ToDateTime(TimeOnly.MinValue).AddHours(dto.HorasEstimadas);

            if (DateOnly.FromDateTime(fechaFinActividad) > proyecto.FechaFin)
            {
                throw new InvalidFieldException($"La fecha de la actividad se encuentra fuera del rango de fecha del proyecto.");
            }

            actividad.Titulo = dto.Titulo;
            actividad.Descripcion = dto.Descripcion;
            actividad.Fecha = dto.Fecha;
            actividad.HorasEstimadas = dto.HorasEstimadas;
            actividad.HorasReales = dto.HorasReales;
            actividad.ProyectoId = dto.ProyectoId;
            actividad.FechaModificacion = DateTime.UtcNow;

            await _actividadRepository.ActualizarAsync(actividad);

        }


        public async Task EliminarActividadAsync(Guid id)
        {

            Actividad? actividad = await _actividadRepository.ConsultarActividadPorIdAsync(id);

            if (actividad is null)
            {
                throw new RegisterNotFoundException("El actividad no existe.");
            }

            actividad.Estado = Constantes.ESTADO_ELIMINADO;
            actividad.FechaModificacion = DateTime.UtcNow;

            await _actividadRepository.ActualizarAsync(actividad);

        }

    }
}