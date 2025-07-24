using Microsoft.AspNetCore.Mvc;
using SASF_EAPRAEZ_KRUGER.DTOs.Actividad;
using SASF_EAPRAEZ_KRUGER.Exceptions.Models;
using SASF_EAPRAEZ_KRUGER.Services.Actividades;

namespace SASF_EAPRAEZ_KRUGER.Controllers
{
    [Route("api/actividades")]
    [ApiController]
    public class ActividadesController : Controller
    {

        private readonly IActividadService _actividadService;


        public ActividadesController(IActividadService actividadService)
        {
            _actividadService = actividadService;
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<ActividadDTO>), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        [ProducesResponseType(typeof(ErrorModel), 500)]
        public async Task<ActionResult<List<ActividadDTO>>> ObtenerTodos()
        {
            List<ActividadDTO> actividades = await _actividadService.ConsultarActividadesAsync();

            return Ok(actividades);
        }



        [HttpPost]
        [ProducesResponseType(typeof(List<ActividadDTO>), 201)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        [ProducesResponseType(typeof(ErrorModel), 500)]
        public async Task<ActionResult<ActividadDTO>> Insertar(
            [FromBody] ActividadCreacionDTO dto)
        {

            ActividadDTO nuevoActividad = await _actividadService.InsertarActividadAsync(dto);
            return CreatedAtAction(nameof(ObtenerTodos), nuevoActividad);

        }



        [HttpPut("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        [ProducesResponseType(typeof(ErrorModel), 404)]
        [ProducesResponseType(typeof(ErrorModel), 500)]
        public async Task<IActionResult> Actualizar(
            Guid id,
            [FromBody] ActividadDTO actividadDTO)
        {

            await _actividadService.ActualizarActividadAsync(id, actividadDTO);
            return NoContent();

        }



        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        [ProducesResponseType(typeof(ErrorModel), 404)]
        [ProducesResponseType(typeof(ErrorModel), 500)]
        public async Task<IActionResult> Eliminar(Guid id)
        {

            await _actividadService.EliminarActividadAsync(id);
            return NoContent();

        }

    }
}
