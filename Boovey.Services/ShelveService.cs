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

        public async Task<AddedShelveModel> AddAsync(AddShelveModel shelveModel, int currentUserId)
        {
            var shelve = await this.dbContext.Shelves.FirstOrDefaultAsync(s => s.Title == shelveModel.Title)
            ?? throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyExists, nameof(Shelve), shelveModel.Title));

            shelve = mapper.Map<Shelve>(shelveModel);

            shelve.CreatorId = currentUserId;
            shelve.LastModifierId = currentUserId;

            await this.dbContext.Shelves.AddAsync(shelve);
            await this.dbContext.SaveChangesAsync();

            return mapper.Map<AddedShelveModel>(shelve);
        }

        public async Task<EditedShelveModel> EditAsync(int shelveId, EditShelveModel shelveModel, int currentUserId)
        {
            var shelve = await FindShelveById(shelveId);

            shelve.Title = shelveModel.Title;
            shelve.LastModifierId = currentUserId;
            shelve.LastModifiedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();

            return mapper.Map<EditedShelveModel>(shelve);
        }

        public async Task<AddedFavoriteShelveModel> AddFavoriteShelveAsync(int shelveId, User currentUser)
        {
            var shelve = await FindShelveById(shelveId);

            var isAlreadyFavoriteShelve = currentUser.FavoriteShelves.Any(s => s.Id == shelveId);

            if (isAlreadyFavoriteShelve)
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.AlreadyFavoriteId, nameof(Shelve), shelve.Id));

            currentUser.FavoriteShelves.Add(shelve);

            await dbContext.SaveChangesAsync();
            return mapper.Map<AddedFavoriteShelveModel>(shelve);
        }

        public async Task<RemovedFavoriteShelveModel> RemoveFavoriteShelveAsync(int shelveId, User currentUser)
        {
            var shelve = await FindShelveById(shelveId);

            var isFavoriteShelve = currentUser.FavoriteShelves.FirstOrDefault(s => s.Id == shelveId);

            if (isFavoriteShelve == null)
                throw new ResourceNotFoundException(string.Format(ErrorMessages.NotFavoriteId, nameof(Shelve), shelve.Id));

            currentUser.FavoriteShelves.Remove(shelve);

            await dbContext.SaveChangesAsync();
            return mapper.Map<RemovedFavoriteShelveModel>(shelve);
        }

        public async Task<ICollection<ShelveListingModel>> GetAllShelvesAsync()
        {
            var shelves = await this.dbContext.Shelves.ToListAsync();

            return mapper.Map<ICollection<ShelveListingModel>>(shelves);
        }

        private async Task<Shelve> FindShelveById(int shelveId)
        {
            var shelve = await this.dbContext.Shelves.FirstOrDefaultAsync(s => s.Id == shelveId)
                ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Shelve), shelveId));

            return shelve;
        }
    }
}
