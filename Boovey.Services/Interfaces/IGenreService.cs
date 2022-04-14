namespace Boovey.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Entities;
    using Models.Requests.GenreModels;
    using Models.Responses.GenreModels;

    public interface IGenreService
    {
        Task<Genre> CreateAsync(CreateGenreModel model, int creatorId);
        Task EditAsync(Genre genre, EditGenreModel model, int modifierId);
        Task DeleteAsync(Genre genre, int modifierId);

        Task<AddedFavoriteGenreModel> AddFavoriteAsync(Genre genre, User currentUser);
        Task<RemovedFavoriteGenreModel> RemoveFavoriteAsync(Genre genre, User currentUser);

        Task<bool> ContainsActiveByTitleAsync(string title);
        Task<Genre> GetActiveByIdAsync(int Id);
        Task<Genre> GetByIdAsync(int Id);
        Task<Genre> GetByTitleAsync(string title);
        Task<ICollection<Genre>> GetAllActiveAsync();
    }
}
