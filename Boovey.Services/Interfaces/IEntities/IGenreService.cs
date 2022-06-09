namespace Boovey.Services.Interfaces.IEntities
{
    using Data.Entities;
    using Models.Requests.GenreModels;
    using System.Threading.Tasks;

    public interface IGenreService
    {
        Task<Genre> CreateAsync(CreateGenreModel model, int creatorId);
        Task EditAsync(Genre genre, EditGenreModel model, int modifierId);
        Task DeleteAsync(Genre genre, int modifierId);

        Task AddFavoriteAsync(Genre genre, User currentUser);
        Task RemoveFavoriteAsync(Genre genre, User currentUser);
        Task SaveModificationAsync(Genre genre, int modifierId);
    }
}
