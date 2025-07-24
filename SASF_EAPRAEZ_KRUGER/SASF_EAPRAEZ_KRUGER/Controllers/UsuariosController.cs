using Microsoft.AspNetCore.Mvc;
using SASF_EAPRAEZ_KRUGER.DTOs.Usuario;
using SASF_EAPRAEZ_KRUGER.Exceptions.Models;
using SASF_EAPRAEZ_KRUGER.Services.Usuarios;

namespace SASF_EAPRAEZ_KRUGER.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {

        private readonly IUsuarioService _usuarioService;


        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<UsuarioDTO>), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        [ProducesResponseType(typeof(ErrorModel), 500)]
        public async Task<ActionResult<List<UsuarioDTO>>> ObtenerTodos()
        {
            List<UsuarioDTO> usuarios = await _usuarioService.ObtenerTodosAsync();

            return Ok(usuarios);
        }



        [HttpPost]
        [ProducesResponseType(typeof(List<UsuarioDTO>), 201)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        [ProducesResponseType(typeof(ErrorModel), 500)]
        public async Task<ActionResult<UsuarioDTO>> Insertar(
            [FromBody] UsuarioCreacionDTO dto)
        {

            UsuarioDTO nuevoUsuario = await _usuarioService.InsertarUsuarioAsync(dto);
            return CreatedAtAction(nameof(ObtenerTodos), nuevoUsuario);

        }



        [HttpPut("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        [ProducesResponseType(typeof(ErrorModel), 404)]
        [ProducesResponseType(typeof(ErrorModel), 500)]
        public async Task<IActionResult> Actualizar(
            Guid id, 
            [FromBody] UsuarioDTO usuarioDTO)
        {

            await _usuarioService.ActualizarUsuarioAsync(id, usuarioDTO);
            return NoContent();

        }



        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        [ProducesResponseType(typeof(ErrorModel), 404)]
        [ProducesResponseType(typeof(ErrorModel), 500)]
        public async Task<IActionResult> Eliminar(Guid id)
        {

            await _usuarioService.EliminarUsuarioAsync(id);
            return NoContent();

        }

    }
}
