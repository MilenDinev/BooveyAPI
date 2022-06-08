namespace Boovey.Services.Handlers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Constants;
    using Exceptions;
    using Data;
    using Interfaces.IHandlers;
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

        public async Task<bool> DuplicationCheck<T>(string searchFlag, ICollection<T> collection) where T : class, IEntity 
        {
            var contains = collection.Any(e => e.NormalizedName == searchFlag.ToUpper());

            if (!contains)
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyContained, nameof(T), searchFlag));
            return await Task.FromResult(contains);
        }
        public async Task NullableCheck<T>(T entity, string searchFlag) where T : class, IEntity
        {
            if (await Task.Run(() => entity == null))
                throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(T), searchFlag));
        }
        public async Task DeletedCheck<T>(T entity) where T : class, IEntity
        {
            if (await Task.Run(() => entity.Deleted))
                throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityHasBeenDeleted, nameof(T)));

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
