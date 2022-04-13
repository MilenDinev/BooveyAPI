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
        Task EditAsync(Shelve shelve, EditShelveModel shelveModel, int modifierId);
        Task DeleteAsync(Shelve shelve, int modifierId);

        Task<AddedFavoriteShelveModel> AddFavoriteAsync(int Id, User currentUser);
        Task<RemovedFavoriteShelveModel> RemoveFavoriteAsync(int Id, User currentUser);

        Task<bool> ContainsActiveByTitleAsync(string title, ICollection<Shelve> shelves);
        Task<Shelve> GetActiveByIdAsync(int Id);
        Task<Shelve> GetByIdAsync(int Id);
        Task<Shelve> GetByTitleAsync(string title);
        Task<ICollection<Shelve>> GetAllActiveAsync();
    }
}
