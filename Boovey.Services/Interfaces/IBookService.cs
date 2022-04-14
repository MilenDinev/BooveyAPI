namespace Boovey.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Entities;
    using Models.Requests.BookModels;
    using Models.Responses.BookModels;
    using Models.Responses.SharedModels;

    public interface IBookService
    {
        Task<AddedBookModel> AddAsync(CreateBookModel model, int creatorId);
        Task<EditedBookModel> EditAsync(int bookId, EditBookModel model, int modifierId);
        Task DeleteAsync(Book book, int modifierId);
        Task<AddedFavoriteBookModel> AddFavoriteBook(int bookId, User currentUser);
        Task<RemovedFavoriteBookModel> RemoveFavoriteBook(int bookId, User currentUser);
        Task<Book> GetByIdAsync(int bookId);
        Task<ICollection<BookListingModel>> GetAllBooksAsync();

        Task<AssignedAuthorBookModel> AssignAuthorAsync(int bookId, int authorId, int modifierId);
        Task<AssignedBookGenreModel> AssignGenreAsync(int bookId, int genreId, int modifierId);
        Task<AssignedPublisherBookModel> AssignPublisherAsync(int bookId, int publisherId, int modifierId);
    }
}
