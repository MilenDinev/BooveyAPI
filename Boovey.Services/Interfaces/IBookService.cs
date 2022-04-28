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
        Task<Book> GetByIdAsync(int Id);
        Task<Book> GetActiveByIdAsync(int Id);
        Task<ICollection<Book>> GetAllActiveAsync();
        Task<bool> ContainsActiveByTitleAsync(string title, ICollection<Book> books);
        Task<Book> AssignAuthorAsync(Book book, Author author, int modifierId);
        Task<Book> AssignGenreAsync(Book book, Genre genre, int modifierId);
        Task<Book> AssignPublisherAsync(Book book, Publisher publisher, int modifierId);

        Task<Author> GetAuthorByIdAsync(int authorId);
        Task<Genre> GetGenreByIdAsync(int genreId);
        Task<Publisher> GetPublisherByIdAsync(int publisherId);
    }
}
