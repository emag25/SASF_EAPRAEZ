using SASF_EAPRAEZ_KRUGER.DTOs.Actividad;

namespace SASF_EAPRAEZ_KRUGER.Services.Actividades
{
    public interface IActividadService
    {

        Task<List<ActividadDTO>> ConsultarActividadesAsync();

        Task<ActividadDTO> InsertarActividadAsync(ActividadCreacionDTO actividadCreacionDTO);

        Task ActualizarActividadAsync(Guid id, ActividadDTO actividadDTO);

        Task EliminarActividadAsync(Guid id);

    }
}
