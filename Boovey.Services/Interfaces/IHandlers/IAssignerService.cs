namespace Boovey.Services.Interfaces.IHandlers
{
    using System.Threading.Tasks;
    using Data.Entities;
    using Data.Entities.Interfaces.IAssignables;

    public interface IAssignerService
    {
        Task AssignBookAsync(IBookAssignable entity, Book book);
        Task AssignAuthorAsync(IAuthorAssignable entity, Author author);
        Task AssignPublisherAsync(IPublisherAssignable entity, Publisher publisher);
        Task AssignGenreAsync(IGenreAssignable entity, Genre genre);

        Task<bool> IsAuthorAlreadyAssigned(IAuthorAssignable entity, Author author);
        Task<bool> IsGenreAlreadyAssigned(IGenreAssignable entity, Genre genre);
        Task<bool> IsPublisherAlreadyAssigned(IPublisherAssignable entity, Publisher publisher);
        Task<bool> IsBookAlreadyAssigned(IBookAssignable entity, Book book);
    }
}
