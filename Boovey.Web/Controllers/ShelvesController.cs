namespace Boovey.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using Models.Requests.ShelveModels;
    using Models.Responses.ShelveModels;
    using AutoMapper;

    [Route("api/[controller]")]
    [ApiController]
    public class ShelvesController : BooveyBaseController
    {
        private readonly IShelveService shelveService;
        private readonly IMapper mapper;

        public ShelvesController(IUserService userService, IShelveService shelveService) : base(userService)
        {
            this.shelveService = shelveService;
        }

        [HttpGet("List/")]
        public async Task<ActionResult<IEnumerable<ShelveListingModel>>> Get()
        {
            var allShelves = await this.shelveService.GetAllAsync();
            return allShelves.ToList();
        }

        [HttpPost("Add/")]
        public async Task<ActionResult> Add(AddShelveModel shelveInput)
        {
            await AssignCurrentUserAsync();
            var addedShelve = await this.shelveService.CreateAsync(shelveInput, CurrentUser.Id);
            return CreatedAtAction(nameof(Get), "Shelves", new { id = addedShelve.Id }, addedShelve);
        }

        [HttpPut("Edit/{shelveId}")]
        public async Task<ActionResult<EditedShelveModel>> Edit(EditShelveModel shelveInput, int shelveId)
        {
            await AssignCurrentUserAsync();
            var editedShelve = await this.shelveService.EditAsync(shelveId, shelveInput, CurrentUser.Id);
            return editedShelve;
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
            var shelve = await this.shelveService.GetById(shelveId);
            await this.shelveService.DeleteAsync(shelve);
            await this.shelveService.SaveChangesAsync(shelve, CurrentUser.Id);
            return mapper.Map<DeletedShelveModel>(shelve);
        }
    }
}
