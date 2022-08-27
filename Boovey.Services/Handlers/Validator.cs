namespace Boovey.Services.Handlers
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Constants;
    using Interfaces;
    using Exceptions;
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

        public async Task ValidateEntityAsync<T>(T entity, string flag) where T : class, IEntity
        {
            var entityType = typeof(T).ToString().Substring(typeof(T).ToString().LastIndexOf('.') + 1);
            var isNullable = await this.entityChecker.NullableCheck<T>(entity, flag);
            if (isNullable)
                throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityDoesNotExist, entityType));
            var isDeleted =  await this.entityChecker.DeletedCheck<T>(entity);
            if (isDeleted)
                throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityHasBeenDeleted, entityType));
        }

        public async Task ValidateUniqueEntityAsync<T>(T entity) where T : class, IEntity
        {
            var entityType = typeof(T).ToString().Substring(typeof(T).ToString().LastIndexOf('.') + 1);
            if (await Task.Run(() => entity != null))
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyContained, entityType));
        }

        public async Task ValidateAssigningBook<T>(T entity, Book book) where T : class, IBookAssignable
        {
            var entityType = typeof(T).ToString().Substring(typeof(T).ToString().LastIndexOf('.') + 1);
            var bookType = typeof(Book).ToString().Substring(typeof(Book).ToString().LastIndexOf('.') + 1);
            var isAlreadyAssigned = await this.entityChecker.BookAssignedCheck<T>(entity, book);
            if (isAlreadyAssigned)
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyAssignedId, bookType, book.Id, entityType));
        }
        public async Task ValidateAssigningAuthor<T>(T entity, Author author) where T : class, IAuthorAssignable
        {
            var entityType = typeof(T).ToString().Substring(typeof(T).ToString().LastIndexOf('.') + 1);
            var authorType = typeof(Author).ToString().Substring(typeof(Author).ToString().LastIndexOf('.') + 1);
            var isAlreadyAssigned = await this.entityChecker.AuthorAssignedCheck<T>(entity, author);
            if (isAlreadyAssigned)
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyAssignedId, authorType, author.Id, entityType));
        }
        public async Task ValidateAssigningGenre<T>(T entity, Genre genre) where T : class, IGenreAssignable
        {
            var entityType = typeof(T).ToString().Substring(typeof(T).ToString().LastIndexOf('.') + 1);
            var genreType = typeof(Genre).ToString().Substring(typeof(Genre).ToString().LastIndexOf('.') + 1);
            var isAlreadyAssigned = await this.entityChecker.GenreAssignedCheck<T>(entity, genre);
            if (isAlreadyAssigned)
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyAssignedId, genreType, genre.Id, entityType));
        }
        public async Task ValidateAssigningPublisher<T>(T entity, Publisher publisher) where T : class, IPublisherAssignable
        {
            var entityType = typeof(T).ToString().Substring(typeof(T).ToString().LastIndexOf('.') + 1);
            var publisherType = typeof(Publisher).ToString().Substring(typeof(Publisher).ToString().LastIndexOf('.') + 1);
            var isAlreadyAssigned = await this.entityChecker.PublisherAssignedCheck<T>(entity, publisher);
            if (isAlreadyAssigned)
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyAssignedId, publisherType, publisher.Id, entityType));
        }
        public async Task ValidateAddingFavorite<T>(int entityId, ICollection<T> collection) where T : class, IEntity
        {
            var entityType = typeof(T).ToString().Substring(typeof(T).ToString().LastIndexOf('.') + 1);
            var isAlreadyFavorite = await Task.Run(() => collection.Any(x => x.Id == entityId));
            if (isAlreadyFavorite)
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.AlreadyFavoriteId, entityType, entityId));
        }
        public async Task ValidateRemovingFavorite<T>(int entityId, ICollection<T> collection) where T : class, IEntity
        {
            var entityType = typeof(T).ToString().Substring(typeof(T).ToString().LastIndexOf('.') + 1);
            var isAlreadyFavorite = await Task.Run(() => collection.Any(x => x.Id == entityId));
            if (!isAlreadyFavorite)
                throw new ResourceNotFoundException(string.Format(ErrorMessages.NotFavoriteId, entityType, entityId));
        }
    }
}
