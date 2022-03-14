namespace Boovey.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using Models.Requests.GenreModels;
    using Models.Responses.GenreModels;

    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : BooveyBaseController
    {
       private readonly IGenreService genreService;

        public GenresController(IUserService userService, IGenreService genreService) : base(userService)
        {
            this.genreService = genreService;
        }

        [HttpGet("List/")]
        public async Task<ActionResult<IEnumerable<GenreListingModel>>> Get()
        {
            var allGenres = await this.genreService.GetAllGenresAsync();
            return allGenres.ToList();
        }

        [HttpPost("Add/")]
        public async Task<ActionResult> Add(AddGenreModel genreInput)
        {
            await GetCurrentUserAsync();
            var addedGenre = await this.genreService.AddAsync(genreInput, CurrentUser.Id);
            return CreatedAtAction(nameof(Get), "Genres", new { id = addedGenre.Id }, addedGenre);
        }

        [HttpPut("Edit/{genreId}")]
        public async Task<ActionResult<EditedGenreModel>> Edit(EditGenreModel genreInput, int genreId)
        {
            await GetCurrentUserAsync();
            var editedGenre = await this.genreService.EditAsync(genreId, genreInput, CurrentUser.Id);
            return editedGenre;
        }

        [HttpPut("Add-To-Favorites/{genreId}")]
        public async Task<AddedFavoriteGenreModel> AddFavorite(int genreId)
        {
            await GetCurrentUserAsync();
            var addedFavoriteGenre = await this.genreService.AddFavoriteGenre(genreId, CurrentUser);
            addedFavoriteGenre.UserId = CurrentUser.Id;
            return addedFavoriteGenre;
        }

        [HttpPut("Remove-From-Favorites/{genreId}")]
        public async Task<RemovedFavoriteGenreModel> RemoveFavorite(int genreId)
        {
            await GetCurrentUserAsync();
            var removedFavoriteGenre = await this.genreService.RemoveFavoriteGenre(genreId, CurrentUser);
            removedFavoriteGenre.UserId = CurrentUser.Id;
            return removedFavoriteGenre;
        }
    }
}
