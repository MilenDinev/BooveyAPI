namespace Boovey.Services.Handlers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Interfaces;
    using Data.Entities;
    using Data.Entities.Interfaces.IEntities;
    using Data.Entities.Interfaces.IAssignables;

    public class EntityChecker : IEntityChecker
    {
        private readonly BooveyDbContext dbContext;

        public EntityChecker(BooveyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> NullableCheck<T>(T entity, string searchFlag) where T : class, IEntity
        {
            return await Task.Run(() => entity == null);           
        }
        public async Task<bool> DeletedCheck<T>(T entity) where T : class, IEntity
        {
            return await Task.Run(() => entity.Deleted);
        }

        public async Task<bool> AuthorAssignedCheck<T>(T entity, Author author) where T : class, IAuthorAssignable
        {
            return await Task.Run(() => entity.Authors.Any(a => a.Id == author.Id));
        }

        public async Task<bool> GenreAssignedCheck<T>(T entity, Genre genre) where T : class, IGenreAssignable
        {
            return await Task.Run(() => entity.Genres.Any(g => g.Id == genre.Id));
        }

        public async Task<bool> PublisherAssignedCheck<T>(T entity, Publisher publisher) where T : class, IPublisherAssignable
        {
            return await Task.Run(() => entity.PublisherId == publisher.Id);
        }

        public async Task<bool> BookAssignedCheck<T>(T entity, Book book) where T : class, IBookAssignable
        {
            return await Task.Run(() => entity.Books.Any(b => b.Id == book.Id));
        }
    }
}
