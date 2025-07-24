using Microsoft.EntityFrameworkCore;
using SASF_EAPRAEZ_KRUGER.Entities;
using SASF_EAPRAEZ_KRUGER.Repositories.Generic;
using SASF_EAPRAEZ_KRUGER.Utils;

namespace SASF_EAPRAEZ_KRUGER.Repositories.Usuarios
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {

        public UsuarioRepository(DbContextGestionProyectos context) : base(context) { }


        public async Task<List<Usuario>> ConsultarUsuariosAsync()
        {
            return await _dbSet.AsNoTracking().Where(u => !u.Estado.Equals(Constantes.ESTADO_ELIMINADO)).ToListAsync();
        }

        public async Task<Usuario?> ConsultarUsuarioPorIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(u => !u.Estado.Equals(Constantes.ESTADO_ELIMINADO) && 
                                                          u.UsuarioId == id);
        }


        public async Task<bool> ExisteUsuarioAsync(Guid id)
        {
            return await _dbSet.AnyAsync(u => !u.Estado.Equals(Constantes.ESTADO_ELIMINADO) && 
                                               u.UsuarioId == id);
        }

        public async Task<bool> ExisteUsuarioPorCorreoAsync(string correo)
        {
            return await _dbSet.AnyAsync(u => !u.Estado.Equals(Constantes.ESTADO_ELIMINADO) && 
                                               u.Correo == correo);
        }

        public async Task<bool> ExisteUsuarioPorTelefonoAsync(string telefono)
        {
            return await _dbSet.AnyAsync(u => !u.Estado.Equals(Constantes.ESTADO_ELIMINADO) && 
                                               u.Telefono == telefono);
        }

    }
}
