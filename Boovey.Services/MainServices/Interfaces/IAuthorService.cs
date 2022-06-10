namespace Boovey.Services.MainServices.Interfaces
{
    using System.Threading.Tasks;
    using Data.Entities;
    using Models.Requests.AuthorModels;

    public interface IAuthorService
    {
        Task<Author> CreateAsync(CreateAuthorModel model, int creatorId);
        Task EditAsync(Author author, EditAuthorModel model, int modifierId);
        Task DeleteAsync(Author author, int modifierId);

        Task AddFavoriteAuthorAsync(Author author, User user);
        Task RemoveFavoriteAuthorAsync(Author author, User user);
        Task SaveModificationAsync(Author author, int modifierId);
    }
}
