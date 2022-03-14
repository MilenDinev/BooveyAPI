namespace Boovey.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Entities;
    using Models.Requests.GenreModels;
    using Models.Responses.GenreModels;

    public interface IGenreService
    {
        Task<AddedGenreModel> AddAsync(AddGenreModel genreModel, int currentUserId);
        Task<EditedGenreModel> EditAsync(int genreId, EditGenreModel genreModel, int currentUserId);
        Task<AddedFavoriteGenreModel> AddFavoriteGenre(int genreId, User currentUser);
        Task<RemovedFavoriteGenreModel> RemoveFavoriteGenre(int genreId, User currentUser);
        Task<ICollection<GenreListingModel>> GetAllGenresAsync();
    }
}
