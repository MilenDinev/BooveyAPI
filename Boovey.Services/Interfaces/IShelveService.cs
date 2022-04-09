namespace Boovey.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Entities;
    using Models.Requests.ShelveModels;
    using Models.Responses.ShelveModels;

    public interface IShelveService
    {
        Task CreateAsync(CreateShelveModel model, int creatorId);
        Task EditAsync(Shelve shelve, EditShelveModel shelveModel, int modifierId);
        Task DeleteAsync(Shelve shelve, int modifierId);

        Task<AddedFavoriteShelveModel> AddFavoriteAsync(int Id, User currentUser);
        Task<RemovedFavoriteShelveModel> RemoveFavoriteAsync(int Id, User currentUser);

        Task TitleDuplicationChecker(string title, User user);
        Task<Shelve> GetById(int Id);
        Task<Shelve> GetByTitle(string title);
        Task<ICollection<Shelve>> GetAllAsync();
    }
}
