using Microsoft.EntityFrameworkCore;
using SASF_EAPRAEZ_KRUGER.Entities;

namespace SASF_EAPRAEZ_KRUGER.Repositories.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        protected readonly DbContextGestionProyectos _context;
        protected readonly DbSet<T> _dbSet;


        public GenericRepository(DbContextGestionProyectos context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }


        public async Task InsertarAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }


        public async Task ActualizarAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

    }

}
