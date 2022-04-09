namespace Boovey.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Interfaces;
    using Exceptions;
    using Constants;
    using Data;
    using Data.Entities;
    using Models.Requests.ShelveModels;
    using Models.Responses.ShelveModels;

    public class ShelveService : IShelveService
    {

        private readonly BooveyDbContext dbContext;

        public ShelveService(BooveyDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<CreatedShelveModel> CreateAsync(AddShelveModel shelveModel, int currentUserId)
        {
            var shelve = await this.dbContext.Shelves.FirstOrDefaultAsync(s => s.Title == shelveModel.Title);
            if (shelve != null)
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyExists, nameof(Shelve), shelveModel.Title));

            shelve = mapper.Map<Shelve>(shelveModel);

            shelve.CreatorId = currentUserId;
            shelve.LastModifierId = currentUserId;

            await this.dbContext.Shelves.AddAsync(shelve);
            await this.dbContext.SaveChangesAsync();

            return mapper.Map<CreatedShelveModel>(shelve);
        }
        public async Task<EditedShelveModel> EditAsync(int shelveId, EditShelveModel shelveModel, int currentUserId)
        {
            var shelve = await FindById(shelveId);

            shelve.Title = shelveModel.Title;
            shelve.LastModifierId = currentUserId;
            shelve.LastModifiedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();

            return mapper.Map<EditedShelveModel>(shelve);
        }
        public async Task DeleteAsync(Shelve shelve)
        {
            await Task.Run(() => shelve.Deleted = true);
        }
        public async Task<AddedFavoriteShelveModel> AddFavoriteAsync(int shelveId, User currentUser)
        {
            var shelve = await FindById(shelveId);

            var isAlreadyFavoriteShelve = currentUser.FavoriteShelves.FirstOrDefault(s => s.Id == shelveId);

            if (isAlreadyFavoriteShelve != null)
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.AlreadyFavoriteId, nameof(Shelve), shelve.Id));

            currentUser.FavoriteShelves.Add(shelve);

            await dbContext.SaveChangesAsync();
            return mapper.Map<AddedFavoriteShelveModel>(shelve);
        }
        public async Task<RemovedFavoriteShelveModel> RemoveFavoriteAsync(int shelveId, User currentUser)
        {
            var shelve = await FindById(shelveId);

            var isFavoriteShelve = currentUser.FavoriteShelves.FirstOrDefault(s => s.Id == shelveId);

            if (isFavoriteShelve == null)
                throw new ResourceNotFoundException(string.Format(ErrorMessages.NotFavoriteId, nameof(Shelve), shelve.Id));

            currentUser.FavoriteShelves.Remove(shelve);

            await dbContext.SaveChangesAsync();
            return mapper.Map<RemovedFavoriteShelveModel>(shelve);
        }
        public async Task SaveChangesAsync(Shelve shelve, int modifierId)
        {
            shelve.LastModifiedOn = DateTime.UtcNow;
            shelve.LastModifierId = modifierId;

            await dbContext.SaveChangesAsync();
        }
        public async Task<ICollection<Shelve>> GetAllAsync()
        {
            var shelves = await this.dbContext.Shelves.ToListAsync();

            return shelves;
        }
        public async Task<Shelve> GetById(int shelveId)
        {
            return await FindById(shelveId);
        }
        private async Task<Shelve> FindById(int shelveId)
        {
            var shelve = await this.dbContext.Shelves.FirstOrDefaultAsync(s => s.Id == shelveId && !s.Deleted)
                ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Shelve), shelveId));

            return shelve;
        }
    }
}
