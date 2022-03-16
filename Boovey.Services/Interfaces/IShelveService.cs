namespace Boovey.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Entities;
    using Models.Requests.ShelveModels;
    using Models.Responses.ShelveModels;

    public interface IShelveService
    {
        Task<AddedShelveModel> AddAsync(AddShelveModel shelveModel, int currentUserId);
        Task<EditedShelveModel> EditAsync(int shelveId, EditShelveModel shelveModel, int currentUserId);
        Task<AddedFavoriteShelveModel> AddFavoriteShelveAsync(int shelveId, User currentUser);
        Task<RemovedFavoriteShelveModel> RemoveFavoriteShelveAsync(int shelveId, User currentUser);
        Task<ICollection<ShelveListingModel>> GetAllShelvesAsync();
    }
}
