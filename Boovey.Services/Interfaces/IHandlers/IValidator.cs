namespace Boovey.Services.Interfaces.IHandlers
{
    using System.Threading.Tasks;
    using Data.Entities;
    using Data.Entities.Interfaces.IAssignables;
    using Data.Entities.Interfaces.IEntities;

    public interface IValidator
    {
        Task<bool> ValidateEntityAsync<T>(T entity, string flag) where T : class, IEntity;
        Task<bool> ValidateUniqueEntityAsync<T>(T entity) where T : class, IEntity;
        Task ValidateAssigningBook<T>(T entity, Book book) where T : class, IBookAssignable;
        Task ValidateAssigningAuthor<T>(T entity, Author author) where T : class, IAuthorAssignable;
        Task ValidateAssigningGenre<T>(T entity, Genre genre) where T : class, IGenreAssignable;
        Task ValidateAssigningPublisher<T>(T entity, Publisher publisher) where T : class, IPublisherAssignable;
    }
}
