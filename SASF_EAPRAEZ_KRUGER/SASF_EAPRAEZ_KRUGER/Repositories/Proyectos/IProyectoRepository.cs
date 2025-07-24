using SASF_EAPRAEZ_KRUGER.Entities;
using SASF_EAPRAEZ_KRUGER.Repositories.Generic;

namespace SASF_EAPRAEZ_KRUGER.Repositories.Proyectos
{
    public interface IProyectoRepository : IGenericRepository<Proyecto>
    {

        Task<List<Proyecto>> ConsultarProyectosAsync();

        Task<Proyecto?> ConsultarProyectoPorIdAsync(Guid id);

        Task<bool> ExisteProyectoPorIDAsync(Guid id);

        Task<bool> ExisteProyectoPorFechaAsync(DateOnly fecha);

    }
}
