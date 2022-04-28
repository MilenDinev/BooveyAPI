namespace Boovey.Services.Handlers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Castle.DynamicProxy;
    using Interfaces;
    using Data;
    using Data.Entities;
    using Data.Entities.Interfaces;

    public abstract class AssigningrHandlerService<TEntity> : BaseService<TEntity> 
        where TEntity : class, IBook, IAssignable
    {
        private readonly IAuthorService authorService;
        private readonly IGenreService genreService;
        private readonly IPublisherService publisherService;

        protected AssigningrHandlerService(IAuthorService authorService, IGenreService genreService, IPublisherService publisherService, BooveyDbContext dbContext) : base(dbContext)
        {
            this.authorService = authorService;
            this.genreService = genreService;
            this.publisherService = publisherService;
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

        public async Task<Author> GetAuthorByIdAsync(int authorId)
        {
            return await this.authorService.GetActiveByIdAsync(authorId);
        }

        public async Task<Genre> GetGenreByIdAsync(int genreId)
        {
            return await this.genreService.GetActiveByIdAsync(genreId);
        }

        public async Task<Publisher> GetPublisherByIdAsync(int publisherId)
        {
            return await this.publisherService.GetActiveByIdAsync(publisherId);
        }

    }
}
