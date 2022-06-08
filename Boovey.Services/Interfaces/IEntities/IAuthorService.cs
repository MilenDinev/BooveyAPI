namespace Boovey.Services.Interfaces.IEntities
{
    using System.Threading.Tasks;
    using Data.Entities;
    using Models.Requests.AuthorModels;

    public interface IAuthorService
    {
        Task<Author> CreateAsync(CreateAuthorModel model, int creatorId);
        Task EditAsync(Author author, EditAuthorModel model, int modifierId);
        Task DeleteAsync(Author author, int modifierId);

        Task<Author> AssignGenreAsync(Author author, Genre genre, int modifierId);

        Task AddFavoriteAuthorAsync(Author author, User user);
        Task RemoveFavoriteAuthorAsync(Author author, User user);
    }
}
