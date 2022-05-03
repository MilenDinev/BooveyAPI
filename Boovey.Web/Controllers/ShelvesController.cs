namespace Boovey.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using AutoMapper;
    using Base;
    using Data.Entities;
    using Services.Constants;
    using Services.Interfaces;
    using Services.Exceptions;
    using Services.Interfaces.IHandlers;
    using Models.Requests.ShelveModels;
    using Models.Responses.ShelveModels;

    [Route("api/[controller]")]
    [ApiController]
    public class ShelvesController : BooveyBaseController
    {
        private readonly IShelveService shelveService;
        private readonly IContextAccessorService<Shelve> shelvesAccessorService;
        private readonly IMapper mapper;
        public ShelvesController(IShelveService shelveService, IContextAccessorService<Shelve> shelvesAccessorService, IMapper mapper, IUserService userService) : base(userService)
        {
            this.shelveService = shelveService;
            this.shelvesAccessorService = shelvesAccessorService;
            this.mapper = mapper;
        }

        [HttpGet("List/")]
        public async Task<ActionResult<IEnumerable<ShelveListingModel>>> Get()
        {
            var allShelves = await this.shelvesAccessorService.GetAllActiveAsync();
            return mapper.Map<ICollection<ShelveListingModel>>(allShelves).ToList();
        }

        [HttpPost("Create/")]
        public async Task<ActionResult> Create(CreateShelveModel shelveInput)
        {
            await AssignCurrentUserAsync();
            var alreadyExists = await this.shelveService.ContainsActiveByTitleAsync(shelveInput.Title, CurrentUser.Shelves);
            if (alreadyExists)
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyContained, nameof(Shelve)));

            var shelve = await this.shelveService.CreateAsync(shelveInput, CurrentUser.Id);
            var createdShelve = mapper.Map<CreatedShelveModel>(shelve);

            return CreatedAtAction(nameof(Get), "Shelves", new { id = createdShelve.Id }, createdShelve);
        }

        [HttpPut("Edit/Shelve/{shelveId}")]
        public async Task<ActionResult<EditedShelveModel>> Edit(EditShelveModel shelveInput, int shelveId)
        {
            await AssignCurrentUserAsync();
            var shelve = await this.shelvesAccessorService.GetActiveByIdAsync(shelveId, nameof(Shelve));
            await this.shelveService.EditAsync(shelve, shelveInput, CurrentUser.Id);

            return mapper.Map<EditedShelveModel>(shelve);
        }

        [HttpPut("Favorites/Add/Shelve/{shelveId}")]
        public async Task<AddedFavoriteShelveModel> AddFavorite(int shelveId)
        {
            await AssignCurrentUserAsync();

            var shelve = await this.shelvesAccessorService.GetActiveByIdAsync(shelveId, nameof(Shelve));
            var addedFavoriteShelve = await this.shelveService.AddFavoriteAsync(shelve, CurrentUser);

            addedFavoriteShelve.UserId = CurrentUser.Id;
            return addedFavoriteShelve;
        }

        [HttpPut("Favorites/Remove/Shelve/{shelveId}")]
        public async Task<RemovedFavoriteShelveModel> RemoveFavorite(int shelveId)
        {
            await AssignCurrentUserAsync();

            var shelve = await this.shelvesAccessorService.GetActiveByIdAsync(shelveId, nameof(Shelve));
            var removedFavoriteShelve = await this.shelveService.RemoveFavoriteAsync(shelve, CurrentUser);

            removedFavoriteShelve.UserId = CurrentUser.Id;
            return removedFavoriteShelve;
        }

        [HttpDelete("Delete/Shelve/{shelveId}")]
        public async Task<DeletedShelveModel> Delete(int shelveId)
        {
            await AssignCurrentUserAsync();
            var shelve = await this.shelvesAccessorService.GetActiveByIdAsync(shelveId, nameof(Shelve));
            await this.shelveService.DeleteAsync(shelve, CurrentUser.Id);
            return mapper.Map<DeletedShelveModel>(shelve);
        }
    }
}
