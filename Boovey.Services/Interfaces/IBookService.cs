namespace Boovey.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Entities;
    using Models.Requests;
    using Models.Responses.BookModels;

    public interface IBookService
    {
        Task<AddedBookModel> AddAsync(AddBookModel bookModel, int currentUserId);
        Task<AddedFavoriteBookModel> AddFavoriteBook(int bookId, User currentUser);
        Task<ICollection<BooksListingModel>> GetAllBooksAsync();
    }
}
