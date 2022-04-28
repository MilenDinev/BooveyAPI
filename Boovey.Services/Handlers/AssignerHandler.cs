namespace Boovey.Services.Handlers
{
    using System.Threading.Tasks;
    using Data;
    using Data.Entities;
    using Data.Entities.Interfaces;
    using System.Linq;
    using Castle.DynamicProxy;

    public abstract class AssignerHandler<TEntity> : BaseService<TEntity> 
        where TEntity : class, IBook, IAssignable
    {


        protected AssignerHandler(BooveyDbContext dbContext) : base(dbContext)
        {

        }

        protected async Task AssignAsync(TEntity entity, IAssignable assignee)
        {
            var assigneeProxyType = (assignee as IProxyTargetAccessor).DynProxyGetTarget().GetType().Name;
            var assigneeType = await Task.Run(() => assigneeProxyType.Remove(assigneeProxyType.Length - 5));

            if (assigneeType == "Author")
            {
                entity.Authors.Add(assignee as Author);
            }
            else if (assigneeType == "Genre")
            {
                entity.Genres.Add(assignee as Genre);
            }
            else if (assigneeType == "Publisher")
            {
                entity.Publisher = assignee as Publisher;
            }
        }

        protected async Task<bool> IsAlreadyAssigned(TEntity entity, IAssignable assignee)
        {
            var isAlreadyAssigned = false;
            var assigneeProxyType = (assignee as IProxyTargetAccessor).DynProxyGetTarget().GetType().Name;
            var assigneeType = await Task.Run(() => assigneeProxyType.Remove(assigneeProxyType.Length - 5));

            if (assigneeType == "Author")
            {
                assignee = assignee as Author;
                isAlreadyAssigned = entity.Authors.Any(a =>a.Id == assignee.Id);
            }
            else if (assigneeType == "Genre")
            {
                assignee = assignee as Genre;
                isAlreadyAssigned = entity.Genres.Any(g => g.Id == assignee.Id);
            }
            else if (assigneeType == "Publisher")
            {
                assignee = assignee as Publisher;
                isAlreadyAssigned = entity.PublisherId == assignee.Id;
            }
            return await Task.Run(() => isAlreadyAssigned);
        }

    }
}
