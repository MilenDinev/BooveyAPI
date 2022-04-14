namespace Boovey.Services.Interfaces
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Data.Entities;
    using Models.Requests.AuthorModels;

    public interface IAuthorService
    {
        Task<Author> CreateAsync(CreateAuthorModel model, int creatorId);
        Task EditAsync(Author author, EditAuthorModel model, int modifierId);
        Task DeleteAsync(Author author, int modifierId);
        Task AddFavoriteAuthorAsync(Author author, User user);
        Task RemoveFavoriteAuthorAsync(Author author, User user);
        Task<bool> ContainsActiveByNameAsync(string name);
        Task<Author> GetByIdAsync(int Id);
        Task<Author> GetByNameAsync(string name);
        Task<Author> GetActiveByIdAsync(int Id);
        Task<ICollection<Author>> GetAllActiveAsync();
    }
}
