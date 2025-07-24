using Moq;
using SASF_EAPRAEZ_KRUGER.DTOs.Proyecto;
using SASF_EAPRAEZ_KRUGER.Entities;
using SASF_EAPRAEZ_KRUGER.Exceptions.BadRequest;
using SASF_EAPRAEZ_KRUGER.Exceptions.NotFound;
using SASF_EAPRAEZ_KRUGER.Repositories.Proyectos;
using SASF_EAPRAEZ_KRUGER.Repositories.Usuarios;
using SASF_EAPRAEZ_KRUGER.Services.Proyectos;
using SASF_EAPRAEZ_KRUGER.Services.Usuarios;
using SASF_EAPRAEZ_KRUGER.Utils;

namespace SASF_EAPRAEZ_KRUGER.Tests.UnitTests
{
    public class ProyectoServiceTests
    {

        private readonly Mock<IUsuarioRepository> _mockUsuarioRepo;
        private readonly Mock<IProyectoRepository> _mockProyectoRepo;
        private readonly ProyectoService _service;



        public ProyectoServiceTests()
        {
            _mockUsuarioRepo = new Mock<IUsuarioRepository>();
            _mockProyectoRepo = new Mock<IProyectoRepository>();
            _service = new ProyectoService(_mockProyectoRepo.Object, _mockUsuarioRepo.Object);
        }



        [Fact]
        public async Task InsertarProyectoAsync_UsuarioNoExiste_LanzaExcepcion()
        {

            ProyectoCreacionDTO proyectoCreacionDTO = new ProyectoCreacionDTO
            {
                Nombre = "Migración Facturación Electrónica",
                Descripcion = "App Android para gestionar tareas diarias",
                FechaInicio = DateOnly.FromDateTime(DateTime.UtcNow),
                FechaFin = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(10)),
                UsuarioId = Guid.NewGuid(),
                Estado = Constantes.ESTADO_ACTIVO
            };

            _mockUsuarioRepo.Setup(r => r.ExisteUsuarioPorIDAsync(proyectoCreacionDTO.UsuarioId)).ReturnsAsync(false);

            await Assert.ThrowsAsync<ForeignKeyViolationException>(() => _service.InsertarProyectoAsync(proyectoCreacionDTO));

        }



        [Theory]
        [InlineData("1900-12-31")]
        [InlineData("1850-01-01")]
        public async Task InsertarProyectoAsync_FechaFinMenorAFechaInicio_LanzaExcepcion(string fechaFinString)
        {

            DateOnly fechaFin = DateOnly.Parse(fechaFinString);

            ProyectoCreacionDTO proyectoCreacionDTO = new ProyectoCreacionDTO
            {
                Nombre = "Migración Facturación Electrónica",
                Descripcion = "App Android para gestionar tareas diarias",
                FechaInicio = DateOnly.FromDateTime(DateTime.UtcNow),
                FechaFin = fechaFin,
                UsuarioId = Guid.NewGuid(),
                Estado = Constantes.ESTADO_ACTIVO
            };

            await Assert.ThrowsAsync<InvalidFieldException>(() => _service.InsertarProyectoAsync(proyectoCreacionDTO));

        }



        [Fact]
        public async Task EliminarProyectoAsync_ProyectoExiste_EliminaCorrectamente()
        {

            Guid idProyecto = new Guid();

            Proyecto proyecto = new Proyecto
            {
                ProyectoId = idProyecto,
                Nombre = "Aplicación Móvil de Tareas",
                Descripcion = "App Android para gestionar tareas diarias",
                FechaInicio = DateOnly.FromDateTime(DateTime.UtcNow),
                FechaFin = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(10)),
                UsuarioId = new Guid("25785B27-9468-F011-B3A1-B42E99B2D502"),
                Estado = Constantes.ESTADO_ELIMINADO
            };

            _mockProyectoRepo.Setup(r => r.ConsultarProyectoPorIdAsync(idProyecto)).ReturnsAsync(proyecto);
            _mockProyectoRepo.Setup(r => r.ActualizarAsync(proyecto)).Returns(Task.CompletedTask);

            await _service.EliminarProyectoAsync(idProyecto);

            _mockProyectoRepo.Verify(r => r.ActualizarAsync(It.Is<Proyecto>(p => p.Estado == Constantes.ESTADO_ELIMINADO)), Times.Once);

        }

    }
}
