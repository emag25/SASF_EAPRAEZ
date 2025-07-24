using Microsoft.EntityFrameworkCore;
using SASF_EAPRAEZ_KRUGER.Entities;
using SASF_EAPRAEZ_KRUGER.Repositories.Generic;
using SASF_EAPRAEZ_KRUGER.Utils;

namespace SASF_EAPRAEZ_KRUGER.Repositories.Actividades
{
    public class ActividadRepository : GenericRepository<Actividad>, IActividadRepository
    {

        public ActividadRepository(DbContextGestionProyectos context) : base(context) { }


        public async Task<List<Actividad>> ConsultarActividadesAsync()
        {
            return await _dbSet.AsNoTracking().Where(u => !u.Estado.Equals(Constantes.ESTADO_ELIMINADO)).ToListAsync();
        }


        public async Task<Actividad?> ConsultarActividadPorIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(u => !u.Estado.Equals(Constantes.ESTADO_ELIMINADO) &&
                                                          u.ActividadId == id);
        }
    
    }
}
