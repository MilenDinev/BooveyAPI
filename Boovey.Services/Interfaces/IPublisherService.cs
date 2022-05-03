namespace Boovey.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Entities;
    using Models.Requests.PublisherModels;

    public interface IPublisherService
    {
        Task<Publisher> CreateAsync(CreatePublisherModel model, int creatorId);
        Task EditAsync(Publisher publisher, EditPublisherModel model, int modifierId);
        Task DeleteAsync(Publisher publisher, int modifierId);

        Task<Publisher> GetByNameAsync(string name);
        //Task<bool> ContainsActiveByNameAsync(string name);
    }
}
