namespace Boovey.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Entities;
    using Models.Requests.ShelveModels;
    using Models.Responses.ShelveModels;

    public interface IShelveService
    {
        Task<CreatedShelveModel> CreateAsync(AddShelveModel shelveModel, int currentUserId);
        Task<EditedShelveModel> EditAsync(int shelveId, EditShelveModel shelveModel, int currentUserId);
        Task DeleteAsync(Shelve shelve);
        Task<AddedFavoriteShelveModel> AddFavoriteAsync(int shelveId, User currentUser);
        Task<RemovedFavoriteShelveModel> RemoveFavoriteAsync(int shelveId, User currentUser);
        Task SaveChangesAsync(Shelve shelve, int modifierId);
        Task<Shelve> GetById(int shelveId);
        Task<ICollection<Shelve>> GetAllAsync();
    }
}
