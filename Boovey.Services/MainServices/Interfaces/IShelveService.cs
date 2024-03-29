﻿namespace Boovey.Services.MainServices.Interfaces
{
    using System.Threading.Tasks;
    using Data.Entities;
    using Models.Requests.ShelveModels;
    using Models.Responses.ShelveModels;

    public interface IShelveService
    {
        Task<Shelve> CreateAsync(CreateShelveModel model, int creatorId);
        Task EditAsync(Shelve shelve, EditShelveModel model, int modifierId);
        Task DeleteAsync(Shelve shelve, int modifierId);

        Task SaveModificationAsync(Shelve shelve, int modifierId);
    }
}
