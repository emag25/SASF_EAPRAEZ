using SASF_EAPRAEZ_KRUGER.DTOs.Proyecto;

namespace SASF_EAPRAEZ_KRUGER.Services.Proyectos
{
    public interface IProyectoService
    {

        Task<List<ProyectoDTO>> ConsultarProyectosAsync();

        Task<ProyectoDTO> InsertarProyectoAsync(ProyectoCreacionDTO ProyectoCreacionDTO);

        Task ActualizarProyectoAsync(Guid id, ProyectoDTO ProyectoDTO);

        Task EliminarProyectoAsync(Guid id);

    }
}
