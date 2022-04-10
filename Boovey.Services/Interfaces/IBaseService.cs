namespace Boovey.Services.Interfaces
{
    using Data.Entities.Interfaces;
    using System.Threading.Tasks;

    public interface IBaseService<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity> FindByIdOrDefaultAsync(int id);
        Task SaveModificationAsync(TEntity entity, int modifierId);
    }
}
