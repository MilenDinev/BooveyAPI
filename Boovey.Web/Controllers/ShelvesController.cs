namespace Boovey.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using AutoMapper;
    using Data.Entities;
    using Services.Constants;
    using Services.Interfaces;
    using Services.Exceptions;
    using Models.Requests.ShelveModels;
    using Models.Responses.ShelveModels;

    [Route("api/[controller]")]
    [ApiController]
    public class ShelvesController : BooveyBaseController
    {
        private readonly IShelveService shelveService;
        private readonly IMapper mapper;

        public ShelvesController(IUserService userService, IShelveService shelveService, IMapper mapper) : base(userService)
        {
            this.shelveService = shelveService;
            this.mapper = mapper;
        }

        [HttpGet("List/")]
        public async Task<ActionResult<IEnumerable<ShelveListingModel>>> Get()
        {
            var allShelves = await this.shelveService.GetAllActiveAsync();
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

            return CreatedAtAction(nameof(Get), "Shelves", new {id = createdShelve.Id}, createdShelve);
        }

        [HttpPut("Edit/{shelveId}")]
        public async Task<ActionResult<EditedShelveModel>> Edit(EditShelveModel shelveInput, int shelveId)
        {
            await AssignCurrentUserAsync();
            var shelve = await this.shelveService.GetActiveByIdAsync(shelveId);

            await this.shelveService.EditAsync(shelve, shelveInput, CurrentUser.Id);

            return mapper.Map<EditedShelveModel>(shelve);
        }

        [HttpPut("Add-To-Favorites/{shelveId}")]
        public async Task<AddedFavoriteShelveModel> AddFavorite(int shelveId)
        {
            await AssignCurrentUserAsync();
            var addedFavoriteShelve = await this.shelveService.AddFavoriteAsync(shelveId, CurrentUser);
            addedFavoriteShelve.UserId = CurrentUser.Id;
            return addedFavoriteShelve;
        }

        [HttpPut("Remove-From-Favorites/{shelveId}")]
        public async Task<RemovedFavoriteShelveModel> RemoveFavorite(int shelveId)
        {
            await AssignCurrentUserAsync();
            var removedFavoriteShelve = await this.shelveService.RemoveFavoriteAsync(shelveId, CurrentUser);
            removedFavoriteShelve.UserId = CurrentUser.Id;
            return removedFavoriteShelve;
        }

        [HttpDelete("Delete/{shelveId}")]
        public async Task<DeletedShelveModel> Delete(int shelveId)
        {
            await AssignCurrentUserAsync(); 
            var shelve = await this.shelveService.GetByIdAsync(shelveId);
            await this.shelveService.DeleteAsync(shelve, CurrentUser.Id);
            return mapper.Map<DeletedShelveModel>(shelve);
        }
    }
}
