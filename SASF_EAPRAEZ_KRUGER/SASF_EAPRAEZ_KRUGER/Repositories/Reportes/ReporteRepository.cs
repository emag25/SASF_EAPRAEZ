using Microsoft.EntityFrameworkCore;
using SASF_EAPRAEZ_KRUGER.Data;
using SASF_EAPRAEZ_KRUGER.Entities;
using SASF_EAPRAEZ_KRUGER.Utils;

namespace SASF_EAPRAEZ_KRUGER.Repositories.Reportes
{
    public class ReporteRepository : IReporteRepository
    {

        private readonly DbContextGestionProyectos _context;


        public ReporteRepository(DbContextGestionProyectos context)
        {
            _context = context;
        }


        public async Task<List<Proyecto>> ObtenerActividadesYProyectoPorUsuarioYFechasAsync(Guid usuarioId, DateOnly desde, DateOnly hasta)
        {
            return await _context.Proyecto.Include(p => p.Actividad)
                                            .Where(p => p.UsuarioId == usuarioId && 
                                                        p.Estado.Equals(Constantes.ESTADO_ACTIVO))
                                            .Select(p => new Proyecto
                                            {
                                                ProyectoId = p.ProyectoId,
                                                Nombre = p.Nombre,
                                                Actividad = p.Actividad
                                                    .Where(a => a.Fecha >= desde && a.Fecha <= hasta && 
                                                                a.Estado.Equals(Constantes.ESTADO_ACTIVO))
                                                    .ToList()
                                            })
                                            .Where(p => p.Actividad.Any())
                                            .ToListAsync();
        }

    }
}