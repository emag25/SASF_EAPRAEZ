using SASF_EAPRAEZ_KRUGER.DTOs.Proyecto;
using SASF_EAPRAEZ_KRUGER.Entities;

namespace SASF_EAPRAEZ_KRUGER.Mappers
{
    public class ProyectoMapper
    {

        public static ProyectoDTO ToDto(Proyecto entidad)
        {
            return new ProyectoDTO
            {
                ProyectoId = entidad.ProyectoId,
                Nombre = entidad.Nombre,
                Descripcion = entidad.Descripcion,
                FechaInicio = entidad.FechaInicio,
                FechaFin = entidad.FechaFin,
                UsuarioId = entidad.UsuarioId,
                Estado = entidad.Estado
            };
        }


        public static Proyecto ToEntity(ProyectoCreacionDTO dto)
        {
            return new Proyecto
            {
                ProyectoId = Guid.NewGuid(),
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                UsuarioId = dto.UsuarioId,
                Estado = dto.Estado
            };
        }

    }
}
