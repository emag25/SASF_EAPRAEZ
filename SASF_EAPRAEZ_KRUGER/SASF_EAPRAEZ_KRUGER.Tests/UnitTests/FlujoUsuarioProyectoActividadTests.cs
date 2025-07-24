using Moq;
using SASF_EAPRAEZ_KRUGER.DTOs.Actividad;
using SASF_EAPRAEZ_KRUGER.DTOs.Proyecto;
using SASF_EAPRAEZ_KRUGER.DTOs.Usuario;
using SASF_EAPRAEZ_KRUGER.Entities;
using SASF_EAPRAEZ_KRUGER.Repositories.Actividades;
using SASF_EAPRAEZ_KRUGER.Repositories.Proyectos;
using SASF_EAPRAEZ_KRUGER.Repositories.Usuarios;
using SASF_EAPRAEZ_KRUGER.Services.Actividades;
using SASF_EAPRAEZ_KRUGER.Services.Proyectos;
using SASF_EAPRAEZ_KRUGER.Services.Usuarios;
using SASF_EAPRAEZ_KRUGER.Utils;

namespace SASF_EAPRAEZ_KRUGER.Tests.UnitTests
{
    public class FlujoUsuarioProyectoActividadTests
    {

        private readonly Mock<IUsuarioRepository> _mockUsuarioRepo;
        private readonly Mock<IProyectoRepository> _mockProyectoRepo;
        private readonly Mock<IActividadRepository> _mockActividadRepo;

        private readonly UsuarioService _usuarioService;
        private readonly ProyectoService _proyectoService;
        private readonly ActividadService _actividadService;

        public FlujoUsuarioProyectoActividadTests()
        {
            _mockUsuarioRepo = new Mock<IUsuarioRepository>();
            _mockProyectoRepo = new Mock<IProyectoRepository>();
            _mockActividadRepo = new Mock<IActividadRepository>();

            _usuarioService = new UsuarioService(_mockUsuarioRepo.Object);
            _proyectoService = new ProyectoService(_mockProyectoRepo.Object, _mockUsuarioRepo.Object);
            _actividadService = new ActividadService(_mockActividadRepo.Object, _mockProyectoRepo.Object);
        }


        [Fact]
        public async Task FlujoCompleto_CrearUsuarioProyectoYActividad_Exitoso()
        {

            Guid usuarioId = Guid.NewGuid();
            Guid proyectoId = Guid.NewGuid();

            _mockUsuarioRepo.Setup(r => r.ExisteUsuarioPorCorreoAsync(It.IsAny<string>())).ReturnsAsync(false);
            _mockUsuarioRepo.Setup(r => r.ExisteUsuarioPorTelefonoAsync(It.IsAny<string>())).ReturnsAsync(false);
            _mockUsuarioRepo.Setup(r => r.InsertarAsync(It.IsAny<Usuario>())).Returns(Task.CompletedTask);

            _mockUsuarioRepo.Setup(r => r.ExisteUsuarioPorIDAsync(usuarioId)).ReturnsAsync(true);
            _mockProyectoRepo.Setup(r => r.InsertarAsync(It.IsAny<Proyecto>())).Returns(Task.CompletedTask);

            _mockProyectoRepo.Setup(r => r.ConsultarProyectoPorIdAsync(proyectoId)).ReturnsAsync(new Proyecto
            {
                ProyectoId = proyectoId,
                UsuarioId = usuarioId,
                Estado = Constantes.ESTADO_ACTIVO,
                FechaInicio = DateOnly.FromDateTime(DateTime.UtcNow),
                FechaFin = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(10)),
            });
            _mockActividadRepo.Setup(r => r.InsertarAsync(It.IsAny<Actividad>())).Returns(Task.CompletedTask);



            UsuarioCreacionDTO usuarioCreado = await _usuarioService.InsertarUsuarioAsync(new UsuarioCreacionDTO
            {
                NombreCompleto = "Emely Mishell Apráez González",
                Correo = "emely@krugercorp.com",
                Telefono = "0961097598",
                Rol = "ADMIN"
            });

            ProyectoCreacionDTO proyectoCreado = await _proyectoService.InsertarProyectoAsync(new ProyectoCreacionDTO
            {
                Nombre = "Migración Facturación Electrónica",
                Descripcion = "App Android para gestionar tareas diarias",
                FechaInicio = DateOnly.FromDateTime(DateTime.UtcNow),
                FechaFin = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(10)),
                UsuarioId = usuarioId,
                Estado = Constantes.ESTADO_ACTIVO
            });

            await _actividadService.InsertarActividadAsync(new ActividadCreacionDTO
            {
                Titulo = "Análisis de requerimientos",
                Descripcion = "Reunión inicial",
                Fecha = DateOnly.FromDateTime(DateTime.UtcNow),
                HorasEstimadas = 4,
                ProyectoId = proyectoId
            });


            _mockUsuarioRepo.Verify(r => r.InsertarAsync(It.IsAny<Usuario>()), Times.Once);
            _mockProyectoRepo.Verify(r => r.InsertarAsync(It.IsAny<Proyecto>()), Times.Once);
            _mockActividadRepo.Verify(r => r.InsertarAsync(It.IsAny<Actividad>()), Times.Once);

        }
    }
}
