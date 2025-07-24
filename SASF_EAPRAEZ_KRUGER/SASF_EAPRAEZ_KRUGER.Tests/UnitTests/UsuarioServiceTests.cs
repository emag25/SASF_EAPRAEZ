using Moq;
using SASF_EAPRAEZ_KRUGER.DTOs.Usuario;
using SASF_EAPRAEZ_KRUGER.Entities;
using SASF_EAPRAEZ_KRUGER.Exceptions.BadRequest;
using SASF_EAPRAEZ_KRUGER.Exceptions.NotFound;
using SASF_EAPRAEZ_KRUGER.Repositories.Usuarios;
using SASF_EAPRAEZ_KRUGER.Services.Usuarios;

namespace SASF_EAPRAEZ_KRUGER.Tests.UnitTests
{
    public class UsuarioServiceTests
    {

        private readonly Mock<IUsuarioRepository> _mockRepo;
        private readonly UsuarioService _service;


        public UsuarioServiceTests()
        {
            _mockRepo = new Mock<IUsuarioRepository>();
            _service = new UsuarioService(_mockRepo.Object);
        }



        [Theory]
        [InlineData("carlos.leon@krugercorp.com")]
        [InlineData("diana.velez@krugercorp.com")]
        public async Task InsertarUsuarioAsync_CorreoYaExiste_LanzaExcepcion(string correo)
        {

            UsuarioCreacionDTO usuarioCreacionDTO = new UsuarioCreacionDTO
            {
                NombreCompleto = "Emely Mishell Apráez González",
                Correo = correo,
                Telefono = "0961097598",
                Rol = "ADMIN"
            };

            _mockRepo.Setup(r => r.ExisteUsuarioPorCorreoAsync(correo)).ReturnsAsync(true);

            await Assert.ThrowsAsync<UniqueFieldException>(() => _service.InsertarUsuarioAsync(usuarioCreacionDTO));

        }



        [Fact]
        public async Task ActualizarUsuarioAsync_IdInexistente_LanzaExcepcion()
        {

            Guid id = Guid.NewGuid();

            UsuarioDTO usuarioDTO = new UsuarioDTO
            {
                UsuarioId = id,
                NombreCompleto = "Emely Mishell Apráez González",
                Correo = "emely@krugercorp.com",
                Telefono = "0961097598",
                Rol = "ADMIN"
            };

            _mockRepo.Setup(r => r.ConsultarUsuarioPorIdAsync(id)).ReturnsAsync((Usuario?)null);

            await Assert.ThrowsAsync<RegisterNotFoundException>(() => _service.ActualizarUsuarioAsync(id, usuarioDTO));

        }



        [Fact]
        public async Task InsertarUsuarioAsync_UsuarioValido_RetornaDTO()
        {

            UsuarioCreacionDTO dto = new UsuarioCreacionDTO
            {
                NombreCompleto = "Emely Mishell Apráez González",
                Correo = "emely@krugercorp.com",
                Telefono = "0961097598",
                Rol = "ADMIN"
            };

            _mockRepo.Setup(r => r.ExisteUsuarioPorCorreoAsync(dto.Correo)).ReturnsAsync(false);
            _mockRepo.Setup(r => r.ExisteUsuarioPorTelefonoAsync(dto.Telefono)).ReturnsAsync(false);

            Usuario? usuarioInsertado = null;
            _mockRepo.Setup(r => r.InsertarAsync(It.IsAny<Usuario>()))
                     .Callback<Usuario>(u => usuarioInsertado = u)
                     .Returns(Task.CompletedTask);

            UsuarioDTO result = await _service.InsertarUsuarioAsync(dto);

            Assert.NotNull(result);
            Assert.Equal(dto.NombreCompleto, result.NombreCompleto);
            Assert.Equal(dto.Correo, result.Correo);
        }


    }
}
