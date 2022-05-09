namespace Boovey.Services.Interfaces.IHandlers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Entities.Interfaces;

    public interface IAccessorService<TEntity> where TEntity : class, ISearchable
    {
        Task<TEntity> GetByIdAsync(int id, string type);
        Task<TEntity> GetActiveByIdAsync(int id, string type);
        Task<ICollection<TEntity>> GetAllAsync();
        Task<ICollection<TEntity>> GetAllActiveAsync();
        Task<TEntity> FindByIdOrDefaultAsync(int id);
    }
}
