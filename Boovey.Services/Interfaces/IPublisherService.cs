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

        Task<Publisher> GetByIdAsync(int Id);
        Task<Publisher> GetByNameAsync(string name);
        Task<Publisher> GetActiveByIdAsync(int Id);
        Task<ICollection<Publisher>> GetAllActiveAsync();
        Task<bool> ContainsActiveByNameAsync(string name);
    }
}
