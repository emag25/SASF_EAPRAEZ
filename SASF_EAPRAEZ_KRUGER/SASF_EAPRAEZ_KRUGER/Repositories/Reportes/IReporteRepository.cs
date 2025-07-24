using SASF_EAPRAEZ_KRUGER.Entities;

namespace SASF_EAPRAEZ_KRUGER.Repositories.Reportes
{
    public interface IReporteRepository
    {

        Task<List<Proyecto>> ObtenerActividadesYProyectoPorUsuarioYFechasAsync(Guid usuarioId, DateOnly desde, DateOnly hasta);

    }
}
