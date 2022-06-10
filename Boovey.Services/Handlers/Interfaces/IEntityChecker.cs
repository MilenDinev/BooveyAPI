namespace Boovey.Services.Handlers.Interfaces
{
    using System.Threading.Tasks;
    using Data.Entities;
    using Data.Entities.Interfaces.IEntities;
    using Data.Entities.Interfaces.IAssignables;

    public interface IEntityChecker
    {

        Task<bool> NullableCheck<T>(T entity, string searchFlag) where T : class, IEntity;
        Task<bool> DeletedCheck<T>(T entity) where T : class, IEntity;

        Task<bool> AuthorAssignedCheck<T>(T entity, Author author) where T : class, IAuthorAssignable;
        Task<bool> GenreAssignedCheck<T>(T entity, Genre genre) where T : class, IGenreAssignable;
        Task<bool> PublisherAssignedCheck<T>(T entity, Publisher publisher) where T : class, IPublisherAssignable;
        Task<bool> BookAssignedCheck<T>(T entity, Book book) where T : class, IBookAssignable;
    }
}
