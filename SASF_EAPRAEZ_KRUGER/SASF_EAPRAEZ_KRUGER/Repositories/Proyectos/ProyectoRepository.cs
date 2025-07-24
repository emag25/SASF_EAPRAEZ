using Microsoft.EntityFrameworkCore;
using SASF_EAPRAEZ_KRUGER.Data;
using SASF_EAPRAEZ_KRUGER.Entities;
using SASF_EAPRAEZ_KRUGER.Repositories.Generic;
using SASF_EAPRAEZ_KRUGER.Utils;

namespace SASF_EAPRAEZ_KRUGER.Repositories.Proyectos
{
    public class ProyectoRepository : GenericRepository<Proyecto>, IProyectoRepository
    {

        public ProyectoRepository(DbContextGestionProyectos context) : base(context) { }


        public async Task<List<Proyecto>> ConsultarProyectosAsync()
        {
            return await _dbSet.AsNoTracking().Where(u => !u.Estado.Equals(Constantes.ESTADO_ELIMINADO)).ToListAsync();
        }


        public async Task<Proyecto?> ConsultarProyectoPorIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(u => !u.Estado.Equals(Constantes.ESTADO_ELIMINADO) &&
                                                          u.ProyectoId == id);
        }

    }
}
