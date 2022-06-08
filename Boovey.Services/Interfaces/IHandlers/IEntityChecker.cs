namespace Boovey.Services.Interfaces.IHandlers
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Data.Entities.Interfaces.IEntities;

    public interface IEntityChecker
    {
        Task<bool> DuplicationChecker<T>(string searchFlag, ICollection<T> collection) where T : class, IEntity;
        Task NullableHandler<T>(T entity, string searchFlag) where T : class, IEntity;
        Task DeletedHandler<T>(T entity) where T : class, IEntity;
    }
}
