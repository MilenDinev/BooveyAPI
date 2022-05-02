namespace Boovey.Services.Interfaces.IHandlers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Entities.Interfaces;

    public interface IContextAccessorService<TEntity> where TEntity : class, IAccessible
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> GetActiveByIdAsync(int id);
        Task<ICollection<TEntity>> GetAllAsync();
        Task<ICollection<TEntity>> GetAllActiveAsync();
        Task<TEntity> FindByIdOrDefaultAsync(int id);
    }
}
