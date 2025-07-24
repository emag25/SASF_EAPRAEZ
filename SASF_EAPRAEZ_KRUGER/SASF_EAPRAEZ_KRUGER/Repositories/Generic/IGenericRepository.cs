namespace SASF_EAPRAEZ_KRUGER.Repositories.Generic
{
    public interface IGenericRepository<T>
    {

        Task InsertarAsync(T entidad);

        Task ActualizarAsync(T entidad);

    }
}
