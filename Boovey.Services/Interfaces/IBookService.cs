namespace Boovey.Services.Interfaces
{
    using System.Threading.Tasks;
    using Models.Requests;
    using Models.Responses.BookModels;

    public interface IBookService
    {
        Task<AddedBookModel> Add(AddBookModel bookModel);
    }
}
