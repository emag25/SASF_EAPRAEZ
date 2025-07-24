using SASF_EAPRAEZ_KRUGER.DTOs.Proyecto;
using SASF_EAPRAEZ_KRUGER.DTOs.Reporte;
using SASF_EAPRAEZ_KRUGER.Entities;
using SASF_EAPRAEZ_KRUGER.Exceptions.NotFound;
using SASF_EAPRAEZ_KRUGER.Mappers;
using SASF_EAPRAEZ_KRUGER.Repositories.Reportes;
using SASF_EAPRAEZ_KRUGER.Repositories.Usuarios;

namespace SASF_EAPRAEZ_KRUGER.Services.Reportes
{
    public class ReporteService : IReporteService
    {
        private readonly IReporteRepository _reporteRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ReporteService(IReporteRepository reporteRepository, IUsuarioRepository usuarioRepository)
        {
            _reporteRepository = reporteRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<ReporteActividadesDTO> ObtenerReporteActividadesPorUsuarioAsync(
            Guid usuarioId, DateOnly desde, DateOnly hasta)
        {

            Usuario? usuario = await _usuarioRepository.ConsultarUsuarioPorIdAsync(usuarioId);

            if (usuario is null)
            {
                throw new RegisterNotFoundException("El usuario no existe.");
            }

            List<Proyecto> proyectos = await _reporteRepository.ObtenerActividadesYProyectoPorUsuarioYFechasAsync(usuarioId, desde, hasta);

            return ReporteMapper.MapearReporte(usuario.NombreCompleto, proyectos);

        }
    }
}