namespace Boovey.Services.Interfaces.IHandlers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Entities;
    using Data.Entities.Interfaces.IEntities;

    public interface ISearchService<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity> GetByIdAsync(int id, string type);
        Task<TEntity> GetByStringAsync(string stringValue, string type);
        Task<Book> GetActiveBookByIdAsync(int id);
        Task<Author> GetActiveAuthorByIdAsync(int id);
        Task<Genre> GetActiveGenreByIdAsync(int id);
        Task<Publisher> GetActivePublisherByIdAsync(int id);
        Task<Review> GetActiveReviewByIdAsync(int id);
        Task<Quote> GetActiveQuoteByIdAsync(int id);
        Task<Shelve> GetActiveShelveByIdAsync(int id);
        Task<Country> GetActiveCountryByIdAsync(int id);
        Task<TEntity> GetActiveByStringAsync(string stringValue, string type);
        Task<ICollection<TEntity>> GetAllAsync();
        Task<ICollection<TEntity>> GetAllActiveAsync();
        Task<TEntity> FindByIdOrDefaultAsync(int id);
        Task<TEntity> FindByStringOrDefaultAsync(string stringValue);
        Task<bool> ContainsActiveByStringAsync(string stringValue);
        Task<bool> ContainsActiveByStringAsync(string stringValue, ICollection<TEntity> collection);
    }
}
