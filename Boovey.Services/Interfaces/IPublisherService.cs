namespace Boovey.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Requests.PublisherModels;
    using Models.Responses.PublisherModels;

    public interface IPublisherService
    {
        Task<AddedPublisherModel> AddAsync(AddPublisherModel publisherModel, int currentUserId);
        Task<EditedPublisherModel> EditAsync(int publisherId, EditPublisherModel publisherModel, int currentUserId);
        Task<ICollection<PublisherListingModel>> GetAllPublishersAsync();
    }
}
