namespace Boovey.Services.Handlers.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Entities.Interfaces.IEntities;

    public interface IFinder
    {
        Task<T> FindByIdOrDefaultAsync<T>(int id) where T : class, IEntity;
        Task<T> FindByStringOrDefaultAsync<T>(string stringValue) where T : class, IEntity;
        Task<ICollection<T>> GetAllAsync<T>() where T : class, IEntity;
        Task<ICollection<T>> GetAllActiveAsync<T>() where T : class, IEntity;
    }
}
