namespace Boovey.Services.Interfaces.IHandlers
{
    using System.Threading.Tasks;
    using Data.Entities.Interfaces;

    public interface IAssigningService<TEntity>  where TEntity : class, IBook
    {
        Task AssignAsync(TEntity entity, IAssignable assignee);
        Task<bool> IsAlreadyAssigned(TEntity entity, IAssignable assignee);
    }
}
