using SASF_EAPRAEZ_KRUGER.DTOs.Actividad;
using SASF_EAPRAEZ_KRUGER.Entities;
using SASF_EAPRAEZ_KRUGER.Utils;

namespace SASF_EAPRAEZ_KRUGER.Mappers
{
    public class ActividadMapper
    {

        public static ActividadDTO ToDto(Actividad entidad)
        {
            return new ActividadDTO
            {
                ActividadId = entidad.ActividadId,
                Titulo = entidad.Titulo,
                Descripcion = entidad.Descripcion,
                Fecha = entidad.Fecha,
                HorasEstimadas = entidad.HorasEstimadas,
                HorasReales = entidad.HorasReales,
                ProyectoId = entidad.ProyectoId
            };
        }


        public static Actividad ToEntity(ActividadCreacionDTO dto)
        {
            return new Actividad
            {
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                Fecha = dto.Fecha,
                HorasEstimadas = dto.HorasEstimadas,
                ProyectoId = dto.ProyectoId,
                Estado = Constantes.ESTADO_ACTIVO
            };
        }

    }
}
