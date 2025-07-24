using Moq;
using SASF_EAPRAEZ_KRUGER.DTOs.Actividad;
using SASF_EAPRAEZ_KRUGER.Entities;
using SASF_EAPRAEZ_KRUGER.Exceptions.BadRequest;
using SASF_EAPRAEZ_KRUGER.Repositories.Actividades;
using SASF_EAPRAEZ_KRUGER.Repositories.Proyectos;
using SASF_EAPRAEZ_KRUGER.Services.Actividades;
using SASF_EAPRAEZ_KRUGER.Utils;

namespace SASF_EAPRAEZ_KRUGER.Tests.UnitTests
{
    public class ActividadServiceTests
    {

        private readonly Mock<IActividadRepository> _mockActividadRepo;
        private readonly Mock<IProyectoRepository> _mockProyectoRepo;
        private readonly ActividadService _service;



        public ActividadServiceTests()
        {
            _mockActividadRepo = new Mock<IActividadRepository>();
            _mockProyectoRepo = new Mock<IProyectoRepository>();
            _service = new ActividadService(_mockActividadRepo.Object, _mockProyectoRepo.Object);
        }



        [Fact]
        public async Task InsertarActividadAsync_ProyectoInactivo_LanzaExcepcion()
        {

            ActividadCreacionDTO actividadCreacionDTO = new ActividadCreacionDTO
            {
                Titulo = "Análisis de requerimientos",
                Descripcion = "Reunión inicial",
                Fecha = DateOnly.FromDateTime(DateTime.UtcNow),
                HorasEstimadas = 4,
                ProyectoId = Guid.NewGuid()
            };

            Proyecto proyecto = new Proyecto
            {
                ProyectoId = actividadCreacionDTO.ProyectoId,
                Nombre = "Migración Facturación Electrónica",
                Descripcion = "App Android para gestionar tareas diarias",
                Estado = Constantes.ESTADO_INACTIVO,
                FechaInicio = DateOnly.FromDateTime(DateTime.UtcNow),
                FechaFin = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(5),
                UsuarioId = Guid.NewGuid()
            };

            _mockProyectoRepo.Setup(r => r.ConsultarProyectoPorIdAsync(actividadCreacionDTO.ProyectoId)).ReturnsAsync(proyecto);

            await Assert.ThrowsAsync<InvalidFieldException>(() => _service.InsertarActividadAsync(actividadCreacionDTO));

        }



        [Fact]
        public async Task InsertarActividadAsync_FechaFueraDeRango_LanzaExcepcion()
        {

            ActividadCreacionDTO actividadCreacionDTO = new ActividadCreacionDTO
            {
                Titulo = "Análisis de requerimientos",
                Descripcion = "Reunión inicial",
                Fecha = DateOnly.FromDateTime(DateTime.UtcNow).AddYears(10),
                HorasEstimadas = 4,
                ProyectoId = Guid.NewGuid()
            };

            Proyecto proyecto = new Proyecto
            {
                ProyectoId = actividadCreacionDTO.ProyectoId,
                Nombre = "Migración Facturación Electrónica",
                Descripcion = "App Android para gestionar tareas diarias",
                Estado = Constantes.ESTADO_ACTIVO,
                FechaInicio = DateOnly.FromDateTime(DateTime.UtcNow),
                FechaFin = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(5),
                UsuarioId = Guid.NewGuid()
            };

            _mockProyectoRepo.Setup(r => r.ConsultarProyectoPorIdAsync(actividadCreacionDTO.ProyectoId)).ReturnsAsync(proyecto);

            await Assert.ThrowsAsync<InvalidFieldException>(() => _service.InsertarActividadAsync(actividadCreacionDTO));

        }



        [Fact]
        public async Task ActualizarActividadAsync_ActividadValida_EjecutaCorrectamente()
        {

            Guid idActividad = Guid.NewGuid();

            ActividadDTO actividadDTO = new ActividadDTO
            {
                ActividadId = idActividad,
                Titulo = "Análisis de requerimientos",
                Descripcion = "Reunión inicial",
                Fecha = DateOnly.FromDateTime(DateTime.UtcNow),
                HorasEstimadas = 5,
                HorasReales = 4,
                ProyectoId = Guid.NewGuid()
            };

            Actividad actividad = new Actividad
            {
                ActividadId = idActividad,
                Titulo = "Análisis de requerimientos",
                Descripcion = "Reunión inicial",
                ProyectoId = actividadDTO.ProyectoId,
                Estado = Constantes.ESTADO_ACTIVO,
                Fecha = actividadDTO.Fecha,
                HorasEstimadas = actividadDTO.HorasEstimadas,
                HorasReales = actividadDTO.HorasReales,
            };

            _mockActividadRepo.Setup(r => r.ConsultarActividadPorIdAsync(idActividad)).ReturnsAsync(actividad);
            _mockProyectoRepo.Setup(r => r.ConsultarProyectoPorIdAsync(actividadDTO.ProyectoId)).ReturnsAsync(new Proyecto
            {
                ProyectoId = new Guid(),
                Nombre = "Aplicación Móvil de Tareas",
                Descripcion = "App Android para gestionar tareas diarias",
                FechaInicio = DateOnly.FromDateTime(DateTime.UtcNow),
                FechaFin = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(10)),
                UsuarioId = new Guid(),
                Estado = Constantes.ESTADO_ACTIVO
            });

            await _service.ActualizarActividadAsync(idActividad, actividadDTO);

            _mockActividadRepo.Verify(r => r.ActualizarAsync(It.IsAny<Actividad>()), Times.Once);

        }

    }
}
