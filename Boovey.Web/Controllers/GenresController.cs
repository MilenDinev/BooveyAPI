namespace Boovey.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using AutoMapper;
    using Base;
    using Services.Constants;
    using Services.Interfaces;
    using Services.Exceptions;
    using Services.Interfaces.IHandlers;
    using Data.Entities;
    using Models.Requests.GenreModels;
    using Models.Responses.GenreModels;

    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : BooveyBaseController
    {
        private readonly IGenreService genreService;
        private readonly ISearchService<Genre> genreSearchService;
        private readonly IMapper mapper;
        public GenresController(IGenreService genreService, ISearchService<Genre> genreSearchService, IMapper mapper, IUserService userService) : base(userService)
        {
            this.genreService = genreService;
            this.genreSearchService = genreSearchService;
            this.mapper = mapper;
        }

        [HttpGet("List/")]
        public async Task<ActionResult<IEnumerable<GenreListingModel>>> Get()
        {
            var allGenres = await this.genreSearchService.GetAllActiveAsync();
            return mapper.Map<ICollection<GenreListingModel>>(allGenres).ToList();
        }

        [HttpPost("Create/")]
        public async Task<ActionResult> Create(CreateGenreModel genreInput)
        {
            await AssignCurrentUserAsync();
            var alreadyExists = await this.genreSearchService.ContainsActiveByStringAsync(genreInput.Title);
            if (alreadyExists)
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyContained, nameof(Genre)));

            var genre = await this.genreService.CreateAsync(genreInput, CurrentUser.Id);
            var createdGenre = mapper.Map<CreatedGenreModel>(genre);

            return CreatedAtAction(nameof(Get), "Genres", new { id = createdGenre.Id }, createdGenre);
        }

        [HttpPut("Edit/Genre/{genreId}")]
        public async Task<ActionResult<EditedGenreModel>> Edit(EditGenreModel genreInput, int genreId)
        {
            await AssignCurrentUserAsync();
            var genre = await this.genreSearchService.GetActiveByIdAsync(genreId, nameof(Genre));
            await this.genreService.EditAsync(genre, genreInput, CurrentUser.Id);

            return mapper.Map<EditedGenreModel>(genre);
        }

        [HttpPut("Favorites/Add/Genre/{genreId}")]
        public async Task<AddedFavoriteGenreModel> AddFavorite(int genreId)
        {
            await AssignCurrentUserAsync();
            var genre = await this.genreSearchService.GetActiveByIdAsync(genreId, nameof(Genre));
            await this.genreService.AddFavoriteAsync(genre, CurrentUser);
            return mapper.Map<AddedFavoriteGenreModel>(genre);
        }

        [HttpPut("Favorites/Remove/Genre/{genreId}")]
        public async Task<RemovedFavoriteGenreModel> RemoveFavorite(int genreId)
        {
            await AssignCurrentUserAsync();
            var genre = await this.genreSearchService.GetActiveByIdAsync(genreId, nameof(Genre));
            await this.genreService.RemoveFavoriteAsync(genre, CurrentUser);
            var removedFavoriteGenre = mapper.Map<RemovedFavoriteGenreModel>(genre);
            removedFavoriteGenre.UserId = CurrentUser.Id;
            return removedFavoriteGenre;
        }

        [HttpDelete("Delete/Genre/{genreId}")]
        public async Task<DeletedGenreModel> Delete(int genreId)
        {
            await AssignCurrentUserAsync();
            var genre = await this.genreSearchService.GetActiveByIdAsync(genreId, nameof(Genre));
            await this.genreService.DeleteAsync(genre, CurrentUser.Id);
            return mapper.Map<DeletedGenreModel>(genre);
        }
    }
}
