namespace Boovey.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Entities;
    using Models.Requests.GenreModels;

    public interface IGenreService
    {
        Task<Genre> CreateAsync(CreateGenreModel model, int creatorId);
        Task EditAsync(Genre genre, EditGenreModel model, int modifierId);
        Task DeleteAsync(Genre genre, int modifierId);

        Task AddFavoriteAsync(Genre genre, User currentUser);
        Task RemoveFavoriteAsync(Genre genre, User currentUser);

        Task<Genre> GetByTitleAsync(string title);
        //Task<bool> ContainsActiveByTitleAsync(string title);
    }
}
