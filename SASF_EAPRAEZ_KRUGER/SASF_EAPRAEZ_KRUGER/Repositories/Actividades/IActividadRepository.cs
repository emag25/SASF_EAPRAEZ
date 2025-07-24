using SASF_EAPRAEZ_KRUGER.Entities;
using SASF_EAPRAEZ_KRUGER.Repositories.Generic;

namespace SASF_EAPRAEZ_KRUGER.Repositories.Actividades
{
    public interface IActividadRepository : IGenericRepository<Actividad>
    {

        Task<List<Actividad>> ConsultarActividadesAsync();

        Task<Actividad?> ConsultarActividadPorIdAsync(Guid id);

    }
}
