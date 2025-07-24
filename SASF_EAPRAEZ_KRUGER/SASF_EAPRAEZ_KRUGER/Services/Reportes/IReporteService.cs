using SASF_EAPRAEZ_KRUGER.DTOs.Reporte;

namespace SASF_EAPRAEZ_KRUGER.Services.Reportes
{
    public interface IReporteService
    {

        Task<ReporteActividadesDTO> ObtenerReporteActividadesPorUsuarioAsync(Guid usuarioId, DateOnly desde, DateOnly hasta);

    }
}
