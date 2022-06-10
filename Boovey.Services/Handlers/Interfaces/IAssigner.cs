namespace Boovey.Services.Handlers.Interfaces
{
    using System.Threading.Tasks;
    using Data.Entities;
    using Data.Entities.Interfaces.IAssignables;

    public interface IAssigner
    {
        Task AssignBookAsync<T>(T entity, Book book) where T : class, IBookAssignable;
        Task AssignAuthorAsync<T>(T entity, Author author) where T : class, IAuthorAssignable;
        Task AssignPublisherAsync<T>(T entity, Publisher publisher) where T : class, IPublisherAssignable;
        Task AssignGenreAsync<T>(T entity, Genre genre) where T : class, IGenreAssignable;
    }
}
