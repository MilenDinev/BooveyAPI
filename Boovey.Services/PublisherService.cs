namespace Boovey.Services
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Interfaces;
    using Data;
    using Data.Entities;
    using Models.Requests.PublisherModels;

    public class PublisherService : BaseService<Publisher>, IPublisherService
    {
        private readonly IMapper mapper;

        public PublisherService(BooveyDbContext dbContext, IMapper mapper) : base (dbContext)
        {
            this.mapper = mapper;
        }

        public async Task<Publisher> CreateAsync(CreatePublisherModel model, int creatorId)
        {
            var publisher = this.mapper.Map<Publisher>(model);
            await CreateEntityAsync(publisher, creatorId);
            return publisher;
        }
        public async Task EditAsync(Publisher publisher, EditPublisherModel publisherModel, int modifierId)
        {
            publisher.Name = publisherModel.Name;
            await SaveModificationAsync(publisher, modifierId);
        }
        public async Task DeleteAsync(Publisher publisher, int modifierId)
        {
            await DeleteEntityAsync(publisher, modifierId);
        }

    }
}
