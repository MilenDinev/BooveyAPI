namespace Boovey.Services.Handlers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.IHandlers;
    using Data.Entities;
    using Data.Entities.Interfaces;

    public class AssignService<TEntity> : IAssignService<TEntity>
        where TEntity : class, IBook
    {
        public async Task AssignAsync(TEntity entity, IAssignee assignee, string assigneeType)
        {

            if (assigneeType == "Author")
            {
                await Task.Run(() => entity.Authors.Add(assignee as Author));
            }
            else if (assigneeType == "Genre")
            {
                await Task.Run(() => entity.Genres.Add(assignee as Genre));
            }
            else if (assigneeType == "Publisher")
            {
                await Task.Run(() => entity.Publisher = assignee as Publisher);
            }
        }

        public async Task<bool> IsAlreadyAssigned(TEntity entity, IAssignee assignee, string assigneeType)
        {
            var isAlreadyAssigned = false;

            if (assigneeType == "Author")
            {
                assignee = assignee as Author;
                isAlreadyAssigned = entity.Authors.Any(a => a.Id == assignee.Id);
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
