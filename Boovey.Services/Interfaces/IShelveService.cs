namespace Boovey.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Entities;
    using Models.Requests.ShelveModels;
    using Models.Responses.ShelveModels;

    public interface IShelveService
    {
        Task<Shelve> CreateAsync(CreateShelveModel model, int creatorId);
        Task EditAsync(Shelve shelve, EditShelveModel model, int modifierId);
        Task DeleteAsync(Shelve shelve, int modifierId);

        Task<AddedFavoriteShelveModel> AddFavoriteAsync(Shelve shelve, User currentUser);
        Task<RemovedFavoriteShelveModel> RemoveFavoriteAsync(Shelve shelve, User currentUser);

        Task<bool> ContainsActiveByTitleAsync(string title, ICollection<Shelve> shelves);
        Task<Shelve> GetByTitleAsync(string title);
    }
}
