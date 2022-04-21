namespace Boovey.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Entities;
    using Models.Requests.BookModels;
    using Models.Responses.BookModels;

    public interface IBookService
    {
        Task<Book> CreateAsync(CreateBookModel bookModel, int currentUserId);
        Task EditAsync(Book book, EditBookModel bookModel, int currentUserId);
        Task DeleteAsync(Book book, int modifierId);
        Task<AddedFavoriteBookModel> AddFavorite(int bookId, User currentUser);
        Task<RemovedFavoriteBookModel> RemoveFavorite(int bookId, User currentUser);
        Task<Book> GetByIdAsync(int bookId);
        Task<Book> GetActiveByIdAsync(int bookId);
        Task<ICollection<Book>> GetAllActiveAsync();
        Task<bool> ContainsActiveByTitleAsync(string title, ICollection<Book> books);
        Task<Book> AssignAuthorAsync(Book book, Author author, int modifierId);
        Task<Book> AssignGenreAsync(Book book, Genre genre, int modifierId);
        Task<Book> AssignPublisherAsync(Book book, Publisher publisher, int modifierId);
    }
}
