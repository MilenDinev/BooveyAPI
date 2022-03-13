namespace Boovey.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Entities;
    using Models.Requests;
    using Models.Responses.BookModels;
    using Models.Responses.SharedModels;

    public interface IBookService
    {
        Task<AddedBookModel> AddAsync(AddBookModel bookModel, int currentUserId);
        Task<EditedBookModel> EditAsync(int bookId, EditBookModel bookModel, int currentUserId);
        Task<AddedFavoriteBookModel> AddFavoriteBook(int bookId, User currentUser);
        Task<RemovedFavoriteBookModel> RemoveFavoriteBook(int bookId, User currentUser);
        Task<ICollection<BooksListingModel>> GetAllBooksAsync();

        Task<AssignedAuthorBookModel> AssignAuthorAsync(int bookId, int authorId, int modifierId);
        Task<AssignedBookGenreModel> AssignGenreAsync(int bookId, int genreId, int modifierId);
        Task<AssignedPublisherBookModel> AssignPublisherAsync(int bookId, int publisherId, int modifierId);
    }
}
