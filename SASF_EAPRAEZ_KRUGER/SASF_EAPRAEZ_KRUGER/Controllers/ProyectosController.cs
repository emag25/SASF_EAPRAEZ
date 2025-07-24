using Microsoft.AspNetCore.Mvc;
using SASF_EAPRAEZ_KRUGER.DTOs.Proyecto;
using SASF_EAPRAEZ_KRUGER.Exceptions.Models;
using SASF_EAPRAEZ_KRUGER.Services.Proyectos;

namespace SASF_EAPRAEZ_KRUGER.Controllers
{

    [ApiController]
    [Route("api/proyectos")]
    public class ProyectosController : Controller
    {

        private readonly IProyectoService _proyectoService;


        public ProyectosController(IProyectoService proyectoService)
        {
            _proyectoService = proyectoService;
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<ProyectoDTO>), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        [ProducesResponseType(typeof(ErrorModel), 500)]
        public async Task<ActionResult<List<ProyectoDTO>>> ObtenerTodos()
        {
            List<ProyectoDTO> proyectos = await _proyectoService.ConsultarProyectosAsync();

            return Ok(proyectos);
        }



        [HttpPost]
        [ProducesResponseType(typeof(List<ProyectoDTO>), 201)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        [ProducesResponseType(typeof(ErrorModel), 500)]
        public async Task<ActionResult<ProyectoDTO>> Insertar(
            [FromBody] ProyectoCreacionDTO dto)
        {

            ProyectoDTO nuevoProyecto = await _proyectoService.InsertarProyectoAsync(dto);
            return CreatedAtAction(nameof(ObtenerTodos), nuevoProyecto);

        }



        [HttpPut("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        [ProducesResponseType(typeof(ErrorModel), 404)]
        [ProducesResponseType(typeof(ErrorModel), 500)]
        public async Task<IActionResult> Actualizar(
            Guid id,
            [FromBody] ProyectoDTO proyectoDTO)
        {

            await _proyectoService.ActualizarProyectoAsync(id, proyectoDTO);
            return NoContent();

        }



        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        [ProducesResponseType(typeof(ErrorModel), 404)]
        [ProducesResponseType(typeof(ErrorModel), 500)]
        public async Task<IActionResult> Eliminar(Guid id)
        {

            await _proyectoService.EliminarProyectoAsync(id);
            return NoContent();

        }

    }
}
