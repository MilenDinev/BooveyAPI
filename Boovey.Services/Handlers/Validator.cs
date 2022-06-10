namespace Boovey.Services.Handlers
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Constants;
    using Exceptions;
    using Interfaces.IHandlers;
    using Data.Entities;
    using Data.Entities.Interfaces.IEntities;
    using Data.Entities.Interfaces.IAssignables;
    using System.Linq;

    public class Validator : IValidator
    {
        private readonly IFinder finder;
        private readonly IEntityChecker entityChecker;

        public Validator(IFinder finder, IEntityChecker entityChecker)
        {
            this.finder = finder;
            this.entityChecker = entityChecker;
        }

        public async Task<bool> ValidateEntityAsync<T>(T entity, string flag) where T : class, IEntity
        {
            await this.entityChecker.NullableCheck<T>(entity, flag);
            await this.entityChecker.DeletedCheck<T>(entity);

            return true;
        }

        public async Task<bool> ValidateUniqueEntityAsync<T>(T entity) where T : class, IEntity
        {
            if (await Task.Run(() => entity != null))
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyContained, nameof(T)));

            return true;
        }

        public async Task ValidateAssigningBook<T>(T entity, Book book) where T : class, IBookAssignable
        {
            var isAlreadyAssigned = await this.entityChecker.BookAssignedCheck<T>(entity, book);
            if (isAlreadyAssigned)
            {
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyAssignedId,
                nameof(Book), book.Id, nameof(T), "test Id"));
            }
        }
        public async Task ValidateAssigningAuthor<T>(T entity, Author author) where T : class, IAuthorAssignable
        {
            var isAlreadyAssigned = await this.entityChecker.AuthorAssignedCheck<T>(entity, author);
            if (isAlreadyAssigned)
            {
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyAssignedId,
                nameof(Author), author.Id, nameof(T), "test Id"));
            }
        }
        public async Task ValidateAssigningGenre<T>(T entity, Genre genre) where T : class, IGenreAssignable
        {
            var isAlreadyAssigned = await this.entityChecker.GenreAssignedCheck<T>(entity, genre);
            if (isAlreadyAssigned)
            {
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyAssignedId,
                nameof(Genre), genre.Id, nameof(T), "test Id"));
            }
        }
        public async Task ValidateAssigningPublisher<T>(T entity, Publisher publisher) where T : class, IPublisherAssignable
        {
            var isAlreadyAssigned = await this.entityChecker.PublisherAssignedCheck<T>(entity, publisher);
            if (isAlreadyAssigned)
            {
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyAssignedId,
                nameof(Publisher), publisher.Id, nameof(T),"test Id"));
            }
        }
        public async Task ValidateAddingFavorite<T>(int entityId, ICollection<T> collection) where T : class, IEntity
        {
            var isAlreadyFavorite = await Task.Run(() => collection.Any(x => x.Id == entityId));
            if (isAlreadyFavorite)
            {
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.AlreadyFavoriteId,
                nameof(T), entityId));
            }
        }
        public async Task ValidateRemovingFavorite<T>(int entityId, ICollection<T> collection) where T : class, IEntity
        {
            var isAlreadyFavorite = await Task.Run(() => collection.Any(x => x.Id == entityId));
            if (!isAlreadyFavorite)
            {
                throw new ResourceNotFoundException(string.Format(ErrorMessages.NotFavoriteId,
                nameof(T), entityId));
            }
        }
    }
}
