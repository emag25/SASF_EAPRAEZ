using Microsoft.AspNetCore.Mvc;
using SASF_EAPRAEZ_KRUGER.DTOs.Usuario;
using SASF_EAPRAEZ_KRUGER.Middleware.Models;
using SASF_EAPRAEZ_KRUGER.Services.Usuarios;

namespace SASF_EAPRAEZ_KRUGER.Controllers
{

    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : Controller
    {

        private readonly IUsuarioService _usuarioService;


        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<UsuarioDTO>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        public async Task<ActionResult<List<UsuarioDTO>>> ObtenerTodos()
        {
            List<UsuarioDTO> usuarios = await _usuarioService.ConsultarUsuariosAsync();

            return Ok(usuarios);
        }



        [HttpPost]
        [ProducesResponseType(typeof(List<UsuarioDTO>), 201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        public async Task<ActionResult<UsuarioDTO>> Insertar(
            [FromBody] UsuarioCreacionDTO dto)
        {

            UsuarioDTO nuevoUsuario = await _usuarioService.InsertarUsuarioAsync(dto);
            return CreatedAtAction(nameof(ObtenerTodos), nuevoUsuario);

        }



        [HttpPut("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        public async Task<IActionResult> Actualizar(
            Guid id, 
            [FromBody] UsuarioDTO usuarioDTO)
        {

            await _usuarioService.ActualizarUsuarioAsync(id, usuarioDTO);
            return NoContent();

        }



        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        public async Task<IActionResult> Eliminar(Guid id)
        {

            await _usuarioService.EliminarUsuarioAsync(id);
            return NoContent();

        }

    }
}
