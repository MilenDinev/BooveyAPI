﻿namespace Boovey.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using AutoMapper;
    using Base;
    using Data.Entities;
    using Services.Handlers.Interfaces;
    using Services.Managers.Interfaces;
    using Services.MainServices.Interfaces;
    using Models.Requests.ShelveModels;
    using Models.Responses.ShelveModels;

    [Route("api/[controller]")]
    [ApiController]
    public class ShelvesController : BooveyBaseController
    {
        private readonly IShelveService shelveService;
        private readonly IFavoritesManager favoritesManager;
        private readonly IFinder finder;
        private readonly IValidator validator;
        private readonly IMapper mapper;

        public ShelvesController(IShelveService shelveService,
            IFavoritesManager favoritesManager,
            IFinder finder,
            IValidator validator,
            IMapper mapper, 
            IUserService userService) 
            : base(userService)
        {
            this.shelveService = shelveService;
            this.favoritesManager = favoritesManager;
            this.finder = finder;
            this.validator = validator;
            this.mapper = mapper;
        }

        [HttpGet("List/")]
        public async Task<ActionResult<IEnumerable<ShelveListingModel>>> Get()
        {
            var allShelves = await this.finder.GetAllActiveAsync<Shelve>();
            return mapper.Map<ICollection<ShelveListingModel>>(allShelves).ToList();
        }

        [HttpPost("Create/")]
        public async Task<ActionResult> Create(CreateShelveModel shelveInput)
        {
            await AssignCurrentUserAsync();
            var shelve = await this.finder.FindByStringOrDefaultAsync<Shelve>(shelveInput.Title);
            await this.validator.ValidateUniqueEntityAsync(shelve);

            shelve = await this.shelveService.CreateAsync(shelveInput, CurrentUser.Id);
            var createdShelve = mapper.Map<CreatedShelveModel>(shelve);

            return CreatedAtAction(nameof(Get), "Shelves", new { id = createdShelve.Id }, createdShelve);
        }

        [HttpPut("Edit/Shelve/{shelveId}")]
        public async Task<ActionResult<EditedShelveModel>> Edit(EditShelveModel shelveInput, int shelveId)
        {
            await AssignCurrentUserAsync();

            var shelve = await this.finder.FindByIdOrDefaultAsync<Shelve>(shelveId);
            await this.validator.ValidateEntityAsync(shelve, shelveId.ToString());

            await this.shelveService.EditAsync(shelve, shelveInput, CurrentUser.Id);

            return mapper.Map<EditedShelveModel>(shelve);
        }

        [HttpPut("Favorites/Add/Shelve/{shelveId}")]
        public async Task<AddedFavoriteShelveModel> AddFavorite(int shelveId)
        {
            await AssignCurrentUserAsync();

            var shelve = await this.finder.FindByIdOrDefaultAsync<Shelve>(shelveId);
            await this.validator.ValidateEntityAsync(shelve, shelveId.ToString());
            await this.validator.ValidateAddingFavorite(shelveId, this.CurrentUser.FavoriteShelves);

            await this.favoritesManager.AddFavoriteShelve(shelve, this.CurrentUser);

            await this.shelveService.SaveModificationAsync(shelve, CurrentUser.Id);

            return mapper.Map<AddedFavoriteShelveModel>(shelve);
        }

        [HttpPut("Favorites/Remove/Shelve/{shelveId}")]
        public async Task<RemovedFavoriteShelveModel> RemoveFavorite(int shelveId)
        {
            await AssignCurrentUserAsync();

            var shelve = await this.finder.FindByIdOrDefaultAsync<Shelve>(shelveId);
            await this.validator.ValidateEntityAsync(shelve, shelveId.ToString());
            await this.validator.ValidateRemovingFavorite(shelveId, this.CurrentUser.FavoriteShelves);

            await this.favoritesManager.RemoveFavoriteShelve(shelve, this.CurrentUser);
            await this.shelveService.SaveModificationAsync(shelve, CurrentUser.Id);

            var removedFavoriteShelve= mapper.Map<RemovedFavoriteShelveModel>(shelve);
            removedFavoriteShelve.UserId = CurrentUser.Id;
            return removedFavoriteShelve;
        }

        [HttpDelete("Delete/Shelve/{shelveId}")]
        public async Task<DeletedShelveModel> Delete(int shelveId)
        {
            await AssignCurrentUserAsync();

            var shelve = await this.finder.FindByIdOrDefaultAsync<Shelve>(shelveId);
            await this.validator.ValidateEntityAsync(shelve, shelveId.ToString());

            await this.shelveService.DeleteAsync(shelve, CurrentUser.Id);
            return mapper.Map<DeletedShelveModel>(shelve);
        }
    }
}
