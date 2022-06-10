namespace Boovey.Services.Interfaces.IEntities
{
    using System.Threading.Tasks;
    using Data.Entities;
    using Models.Requests.BookModels;

    public interface IBookService 
    {
        Task<Book> CreateAsync(CreateBookModel bookModel, int creatorId);
        Task EditAsync(Book book, EditBookModel bookModel, int creatorId);
        Task DeleteAsync(Book book, int modifierId);
        Task SaveModificationAsync(Book book, int modifierId);
    }
}
