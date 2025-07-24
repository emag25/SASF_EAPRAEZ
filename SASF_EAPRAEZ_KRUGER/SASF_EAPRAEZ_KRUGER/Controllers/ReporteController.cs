using Microsoft.AspNetCore.Mvc;
using SASF_EAPRAEZ_KRUGER.DTOs.Reporte;
using SASF_EAPRAEZ_KRUGER.Middleware.Models;
using SASF_EAPRAEZ_KRUGER.Services.Reportes;
using System.ComponentModel.DataAnnotations;

namespace SASF_EAPRAEZ_KRUGER.Controllers
{
    [Route("api/reportes")]
    [ApiController]
    public class ReporteController : ControllerBase
    {

        private readonly IReporteService _reporteService;


        public ReporteController(IReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        [HttpGet("actividades")]
        [ProducesResponseType(typeof(ReporteActividadesDTO), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        public async Task<ActionResult<List<ReporteActividadesDTO>>> ObtenerReporteActividades(
            [FromQuery, Required] Guid usuarioId,
            [FromQuery, Required] DateOnly desde,
            [FromQuery, Required] DateOnly hasta)
        {
            ReporteActividadesDTO reportees = await _reporteService.ObtenerReporteActividadesPorUsuarioAsync(usuarioId, desde, hasta);

            return Ok(reportees);
        }
    }
}
