namespace Boovey.Services.Interfaces.IHandlers
{
    using System.Threading.Tasks;
    using Data.Entities.Interfaces.IEntities;

    public interface IValidator
    {
        Task<bool> ValidateEntityAsync<T>(T entity, string flag) where T : class, IEntity;
        Task<bool> ValidateUniqueEntityAsync<T>(T entity) where T : class, IEntity;
    }
}
