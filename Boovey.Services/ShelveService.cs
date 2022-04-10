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
        private readonly IMapper mapper;

        public ShelveService(BooveyDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<Shelve> CreateAsync(CreateShelveModel model, int creatorId)
        {
            var shelve = this.mapper.Map<Shelve>(model);

            await SetCreatorAsync(shelve, creatorId) ;
            await this.dbContext.AddAsync(shelve);
            await SaveModificationAsync(shelve, creatorId);

            return shelve;
        }
        public async Task EditAsync(Shelve shelve, EditShelveModel model, int modifierId)
        {
            await SetTitleAsync(shelve, model.Title);
            await SaveModificationAsync(shelve, modifierId);
        }
        public async Task DeleteAsync(Shelve shelve, int modifierId)
        {
            shelve.Deleted = true;
            await SaveModificationAsync(shelve, modifierId);
        }

        public async Task<AddedFavoriteShelveModel> AddFavoriteAsync(int shelveId, User currentUser)
        {
            var shelve = await FindByIdOrDefaultAsync(shelveId);

            var isAlreadyFavoriteShelve = currentUser.FavoriteShelves.Any(s => s.Id == shelveId);

            if (isAlreadyFavoriteShelve)
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.AlreadyFavoriteId, nameof(Shelve), shelve.Id));

            currentUser.FavoriteShelves.Add(shelve);

            await dbContext.SaveChangesAsync();
            return mapper.Map<AddedFavoriteShelveModel>(shelve);
        }
        public async Task<RemovedFavoriteShelveModel> RemoveFavoriteAsync(int shelveId, User currentUser)
        {
            var shelve = await FindByIdOrDefaultAsync(shelveId);

            var isFavoriteShelve = currentUser.FavoriteShelves.FirstOrDefault(s => s.Id == shelveId);

            if (isFavoriteShelve == null)
                throw new ResourceNotFoundException(string.Format(ErrorMessages.NotFavoriteId, nameof(Shelve), shelve.Id));

            currentUser.FavoriteShelves.Remove(shelve);

            await dbContext.SaveChangesAsync();
            return mapper.Map<RemovedFavoriteShelveModel>(shelve);
        }

        public async Task<Shelve> GetActiveByIdAsync(int shelveId)
        {
            var shelve = await GetByIdAsync(shelveId);
            if (shelve.Deleted)
                throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityHasBeenDeleted, nameof(Shelve), shelveId));

            return shelve;
        }

        public async Task<Shelve> GetByIdAsync(int shelveId)
        {
            var shelve = await FindByIdOrDefaultAsync(shelveId)
            ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Shelve), shelveId));

            return shelve;
        }

        public async Task<Shelve> GetByTitleAsync(string title)
        {
            var shelve = await FindByTitleOrDefaultAsync(title)
            ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Shelve), title));

            return shelve;
        }
        public async Task<ICollection<Shelve>> GetAllAsync()
        {
            var shelves = await this.dbContext.Shelves.Where(s => !s.Deleted).ToListAsync();

            return shelves;
        }

        public async Task<bool> ContainsActiveByTitleAsync(string title, ICollection<Shelve> shelves)
        {
            var contains = shelves.Any(s => s.Title == title && !s.Deleted);

            return await Task.Run(() => contains);
        }

        private static async Task SetTitleAsync(Shelve shelve, string title)
        {
            if (title != shelve.Title)
            {
                await Task.Run(() => shelve.Title = title);
            }

        }
        private static async Task SetCreatorAsync(Shelve shelve, int creatorId)
        {
            await Task.Run(() => shelve.CreatorId = creatorId);
        }
        private async Task SaveModificationAsync(Shelve shelve, int modifierId)
        {
            shelve.LastModifierId = modifierId;
            shelve.LastModifiedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();
        }

        private async Task<Shelve> FindByIdOrDefaultAsync(int id)
        {
            var shelve = await this.dbContext.Shelves.FirstOrDefaultAsync(s => s.Id == id);

            return shelve;
        }
        private async Task<Shelve> FindByTitleOrDefaultAsync(string title)
        {
            var shelve = await this.dbContext.Shelves.FirstOrDefaultAsync(s => s.Title == title);
            return shelve;
        }
    }
}
