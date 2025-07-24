using SASF_EAPRAEZ_KRUGER.DTOs.Proyecto;
using SASF_EAPRAEZ_KRUGER.DTOs.Reporte;
using SASF_EAPRAEZ_KRUGER.Entities;
using SASF_EAPRAEZ_KRUGER.Exceptions.BadRequest;
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
            Guid usuarioId, DateOnly fechaDesde, DateOnly fechaHasta)
        {

            if(fechaHasta < fechaDesde)
            {
                throw new InvalidFieldException($"La fecha hasta debe ser mayor o igual a la fecha desde.");
            }

            Usuario? usuario = await _usuarioRepository.ConsultarUsuarioPorIdAsync(usuarioId);

            if (usuario is null)
            {
                throw new RegisterNotFoundException("El usuario no existe.");
            }

            List<Proyecto> proyectos = await _reporteRepository.ObtenerActividadesYProyectoPorUsuarioYFechasAsync(usuarioId, fechaDesde, fechaHasta);

            return ReporteMapper.MapearReporte(usuario.NombreCompleto, proyectos);

        }
    }
}