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
        public async Task CreateAsync(CreateShelveModel model, int creatorId)
        {
            var shelve = this.mapper.Map<Shelve>(model);

            await SetCreatorAsync(shelve, creatorId) ;

            await this.dbContext.AddAsync(shelve);

            await SaveModificationAsync(shelve, creatorId);
        }
        public async Task EditAsync(Shelve shelve, EditShelveModel model, int modifierId)
        {
            await SetTitle(shelve, model.Title);
            await SaveModificationAsync(shelve, modifierId);
        }
        public async Task DeleteAsync(Shelve shelve, int modifierId)
        {
            shelve.Deleted = true;
            await SaveModificationAsync(shelve, modifierId);
        }

        public async Task<AddedFavoriteShelveModel> AddFavoriteAsync(int shelveId, User currentUser)
        {
            var shelve = await FindByIdOrDefault(shelveId);

            var isAlreadyFavoriteShelve = currentUser.FavoriteShelves.Any(s => s.Id == shelveId);

            if (isAlreadyFavoriteShelve)
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.AlreadyFavoriteId, nameof(Shelve), shelve.Id));

            currentUser.FavoriteShelves.Add(shelve);

            await dbContext.SaveChangesAsync();
            return mapper.Map<AddedFavoriteShelveModel>(shelve);
        }
        public async Task<RemovedFavoriteShelveModel> RemoveFavoriteAsync(int shelveId, User currentUser)
        {
            var shelve = await FindByIdOrDefault(shelveId);

            var isFavoriteShelve = currentUser.FavoriteShelves.FirstOrDefault(s => s.Id == shelveId);

            if (isFavoriteShelve == null)
                throw new ResourceNotFoundException(string.Format(ErrorMessages.NotFavoriteId, nameof(Shelve), shelve.Id));

            currentUser.FavoriteShelves.Remove(shelve);

            await dbContext.SaveChangesAsync();
            return mapper.Map<RemovedFavoriteShelveModel>(shelve);
        }

        public async Task<Shelve> GetById(int shelveId)
        {
            var shelve = await FindByIdOrDefault(shelveId)
            ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Shelve), shelveId));

            return shelve;
        }
        public async Task<Shelve> GetByTitle(string title)
        {
            var shelve = await FindByTitleOrDefault(title)
            ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Shelve), title));

            return shelve;
        }
        public async Task<ICollection<Shelve>> GetAllAsync()
        {
            var shelves = await this.dbContext.Shelves.ToListAsync();

            return shelves;
        }

        public async Task TitleDuplicationChecker(string title, User user)
        {
            var isDeleted = await IsDeletedByTitle(title);
            var exists = user.Shelves.Any(s => s.Title == title && !isDeleted);

            if (exists)
            throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyCreatedByUser, nameof(Shelve), user.Id));
        }

        private async Task SetTitle(Shelve shelve, string title)
        {
            await Task.Run(() => shelve.Title = title);
        }
        private async Task SetCreatorAsync(Shelve shelve, int creatorId)
        {
            await Task.Run(() => shelve.CreatorId = creatorId);
        }
        private async Task SaveModificationAsync(Shelve shelve, int modifierId)
        {
            shelve.LastModifierId = modifierId;
            shelve.LastModifiedOn = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();
        }
        private async Task<Shelve> FindByIdOrDefault(int id)
        {
            var shelve = await this.dbContext.Shelves.FirstOrDefaultAsync(s => s.Id == id);

            return shelve;
        }
        private async Task<Shelve> FindByTitleOrDefault(string title)
        {
            var shelve = await this.dbContext.Shelves.FirstOrDefaultAsync(s => s.Title == title);
            return shelve;
        }
        private async Task<bool> IsDeletedByTitle(string title)
        {
            var shelve = await FindByTitleOrDefault(title);

            return shelve == null || shelve.Deleted;
        }
    }
}
