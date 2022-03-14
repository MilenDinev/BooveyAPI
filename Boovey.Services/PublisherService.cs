namespace Boovey.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Interfaces;
    using Constants;
    using Data;
    using Data.Entities;
    using Models.Requests.PublisherModels;
    using Models.Responses.PublisherModels;

    public class PublisherService : IPublisherService
    {

        private readonly BooveyDbContext dbContext;
        private readonly IMapper mapper;

        public PublisherService(BooveyDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<AddedPublisherModel> AddAsync(AddPublisherModel publisherModel, int currentUserId)
        {
            var publisher = await this.dbContext.Publishers.FirstOrDefaultAsync(p => p.Name == publisherModel.Name);
            if (publisher != null)
                throw new ArgumentException(string.Format(ErrorMessages.EntityAlreadyExists, nameof(Publisher), publisherModel.Name));

            publisher = mapper.Map<Publisher>(publisherModel);

            publisher.CreatorId = currentUserId;
            publisher.LastModifierId = currentUserId;

            await this.dbContext.Publishers.AddAsync(publisher);
            await this.dbContext.SaveChangesAsync();

            return mapper.Map<AddedPublisherModel>(publisher);
        }

        public async Task<EditedPublisherModel> EditAsync(int publisherId, EditPublisherModel publisherModel, int currentUserId)
        {
            var publisher = await GetPublisherById(publisherId);

            publisher.Name = publisherModel.Name;
            publisher.LastModifierId = currentUserId;
            publisher.LastModifiedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();

            return mapper.Map<EditedPublisherModel>(publisher);
        }

        public async Task<ICollection<PublisherListingModel>> GetAllPublishersAsync()
        {
            var publishers = await this.dbContext.Publishers.ToListAsync();

            return mapper.Map<ICollection<PublisherListingModel>>(publishers);
        }

        private async Task<Publisher> GetPublisherById(int publisherId)
        {
            var publisher = await this.dbContext.Publishers.FirstOrDefaultAsync(g => g.Id == publisherId)
                ?? throw new KeyNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Publisher), publisherId));

            return publisher;
        }
    }
}
