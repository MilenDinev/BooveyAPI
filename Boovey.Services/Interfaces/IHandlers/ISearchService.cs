namespace Boovey.Services.Interfaces.IHandlers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Entities.Interfaces;

    public interface ISearchService<TEntity> where TEntity : class, ISearchable
    {
        Task<TEntity> GetByIdAsync(int id, string type);
        Task<TEntity> GetByStringAsync(string stringValue, string type);
        Task<TEntity> GetActiveByIdAsync(int id, string type);
        Task<TEntity> GetActiveByStringAsync(string stringValue, string type);
        Task<ICollection<TEntity>> GetAllAsync();
        Task<ICollection<TEntity>> GetAllActiveAsync();
        Task<TEntity> FindByIdOrDefaultAsync(int id);
        Task<TEntity> FindByStringOrDefaultAsync(string stringValue);
        Task<bool> ContainsActiveByStringAsync(string stringValue);
        Task<bool> ContainsActiveByStringAsync(string stringValue, ICollection<TEntity> collection);
    }
}
