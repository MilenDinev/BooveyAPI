namespace Boovey.Services.Interfaces.IHandlers
{
    using System.Threading.Tasks;
    using Data.Entities.Interfaces;

    public interface IAssignService<TEntity>  where TEntity : class, IBook
    {
        Task AssignAsync(TEntity entity, IAssignee assignee, string assigneeType);
        Task<bool> IsAlreadyAssigned(TEntity entity, IAssignee assignee, string assigneeType);
    }
}
