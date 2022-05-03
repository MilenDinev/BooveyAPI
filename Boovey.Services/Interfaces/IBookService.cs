namespace Boovey.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Entities;
    using Models.Requests.BookModels;

    public interface IBookService
    {
        Task<Book> CreateAsync(CreateBookModel bookModel, int creatorId);
        Task EditAsync(Book book, EditBookModel bookModel, int creatorId);
        Task DeleteAsync(Book book, int modifierId);
        Task AddFavoriteAsync(Book book, User user);
        Task RemoveFavoriteAsync(Book book, User user);
        Task<bool> ContainsActiveByTitleAsync(string title, ICollection<Book> books);
        Task<Book> AssignAuthorAsync(Book book, Author author, string assigneeType, int modifierId);
        Task<Book> AssignGenreAsync(Book book, Genre genre, string assigneeType, int modifierId);
        Task<Book> AssignPublisherAsync(Book book, Publisher publisher, string assigneeType, int modifierId);
    }
}
