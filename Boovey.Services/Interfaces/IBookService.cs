namespace Boovey.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Requests;
    using Models.Responses.BookModels;

    public interface IBookService
    {
        Task<AddedBookModel> AddAsync(AddBookModel bookModel);
        Task<ICollection<BooksListingModel>> GetAllBooksAsync();
    }
}
