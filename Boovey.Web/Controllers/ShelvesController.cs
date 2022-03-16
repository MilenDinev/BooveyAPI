namespace Boovey.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using Models.Requests.ShelveModels;
    using Models.Responses.ShelveModels;

    [Route("api/[controller]")]
    [ApiController]
    public class ShelvesController : BooveyBaseController
    {
        private readonly IShelveService shelveService;

        public ShelvesController(IUserService userService, IShelveService shelveService) : base(userService)
        {
            this.shelveService = shelveService;
        }

        [HttpGet("List/")]
        public async Task<ActionResult<IEnumerable<ShelveListingModel>>> Get()
        {
            var allShelves = await this.shelveService.GetAllShelvesAsync();
            return allShelves.ToList();
        }

        [HttpPost("Add/")]
        public async Task<ActionResult> Add(AddShelveModel shelveInput)
        {
            await GetCurrentUserAsync();
            var addedShelve = await this.shelveService.AddAsync(shelveInput, CurrentUser.Id);
            return CreatedAtAction(nameof(Get), "Shelves", new { id = addedShelve.Id }, addedShelve);
        }

        [HttpPut("Edit/{shelveId}")]
        public async Task<ActionResult<EditedShelveModel>> Edit(EditShelveModel shelveInput, int shelveId)
        {
            await GetCurrentUserAsync();
            var editedShelve = await this.shelveService.EditAsync(shelveId, shelveInput, CurrentUser.Id);
            return editedShelve;
        }

        [HttpPut("Add-To-Favorites/{shelveId}")]
        public async Task<AddedFavoriteShelveModel> AddFavorite(int shelveId)
        {
            await GetCurrentUserAsync();
            var addedFavoriteShelve = await this.shelveService.AddFavoriteShelveAsync(shelveId, CurrentUser);
            addedFavoriteShelve.UserId = CurrentUser.Id;
            return addedFavoriteShelve;
        }

        [HttpPut("Remove-From-Favorites/{shelveId}")]
        public async Task<RemovedFavoriteShelveModel> RemoveFavorite(int shelveId)
        {
            await GetCurrentUserAsync();
            var removedFavoriteShelve = await this.shelveService.RemoveFavoriteShelveAsync(shelveId, CurrentUser);
            removedFavoriteShelve.UserId = CurrentUser.Id;
            return removedFavoriteShelve;
        }
    }
}
