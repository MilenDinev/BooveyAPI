﻿namespace Boovey.Services
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Interfaces;
    using Exceptions;
    using Constants;
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

        public async Task<Publisher> GetByNameAsync(string name)
        {
            var publisher = await FindByNameOrDefaultAsync(name)
            ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Publisher), name));

            return publisher;
        }

        //public async Task<bool> ContainsActiveByNameAsync(string name)
        //{
        //    var publishers = await GetAllAsync();
        //    var contains = publishers.Any(p => p.Name == name && !p.Deleted);

        //    return await Task.FromResult(contains);
        //}

        private async Task<Publisher> FindByNameOrDefaultAsync(string name)
        {
            var publisher = await this.dbContext.Publishers.FirstOrDefaultAsync(p => p.Name == name);
            return publisher;
        }
    }
}
