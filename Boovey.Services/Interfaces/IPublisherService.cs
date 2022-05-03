﻿namespace Boovey.Services.Interfaces
{
    using Data.Entities;
    using Models.Requests.PublisherModels;
    using System.Threading.Tasks;

    public interface IPublisherService
    {
        Task<Publisher> CreateAsync(CreatePublisherModel model, int creatorId);
        Task EditAsync(Publisher publisher, EditPublisherModel model, int modifierId);
        Task DeleteAsync(Publisher publisher, int modifierId);

        Task<Publisher> GetByNameAsync(string name);
        //Task<bool> ContainsActiveByNameAsync(string name);
    }
}
