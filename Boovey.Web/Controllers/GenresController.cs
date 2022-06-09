namespace Boovey.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using AutoMapper;
    using Base;
    using Services.Interfaces.IEntities;
    using Services.Interfaces.IHandlers;
    using Data.Entities;
    using Models.Requests.GenreModels;
    using Models.Responses.GenreModels;
    using Models.Responses.SharedModels;

    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : BooveyBaseController
    {
        private readonly IGenreService genreService;
        private readonly IAssigner assigner;
        private readonly IFinder finder;
        private readonly IValidator validator;
        private readonly IMapper mapper;

        public GenresController(IGenreService genreService,
            IAssigner assigner,
            IFinder finder,
            IValidator validator,
            IMapper mapper, 
            IUserService userService) 
            : base(userService)
        {
            this.genreService = genreService;
            this.assigner = assigner;
            this.finder = finder;
            this.validator = validator;
            this.mapper = mapper;
        }

        [HttpGet("List/")]
        public async Task<ActionResult<IEnumerable<GenreListingModel>>> Get()
        {
            var allGenres = await this.finder.GetAllActiveAsync<Genre>();
            return mapper.Map<ICollection<GenreListingModel>>(allGenres).ToList();
        }

        [HttpPost("Create/")]
        public async Task<ActionResult> Create(CreateGenreModel genreInput)
        {
            await AssignCurrentUserAsync();
            var genre = await this.finder.FindByStringOrDefaultAsync<Genre>(genreInput.Title);
            await this.validator.ValidateUniqueEntityAsync(genre);

            genre = await this.genreService.CreateAsync(genreInput, CurrentUser.Id);
            var createdGenre = mapper.Map<CreatedGenreModel>(genre);

            return CreatedAtAction(nameof(Get), "Genres", new { id = createdGenre.Id }, createdGenre);
        }

        [HttpPut("Edit/Genre/{genreId}")]
        public async Task<ActionResult<EditedGenreModel>> Edit(EditGenreModel genreInput, int genreId)
        {
            await AssignCurrentUserAsync();
            var genre = await this.finder.FindByIdOrDefaultAsync<Genre>(genreId);
            await this.validator.ValidateEntityAsync(genre, genreId.ToString());

            await this.genreService.EditAsync(genre, genreInput, CurrentUser.Id);

            return mapper.Map<EditedGenreModel>(genre);
        }

        [HttpPut("Assign/Genre/{genreId}/Book/{bookId}")]
        public async Task<AssignedBookAuthorModel> AssignBook(int genreId, int bookId)
        {
            await AssignCurrentUserAsync();

            var genre = await this.finder.FindByIdOrDefaultAsync<Genre>(genreId);
            await this.validator.ValidateEntityAsync(genre, genreId.ToString());

            var book = await this.finder.FindByIdOrDefaultAsync<Book>(bookId);
            await this.validator.ValidateEntityAsync(book, bookId.ToString());

            await this.validator.ValidateAssigningBook(genre, book);
            await this.assigner.AssignBookAsync(genre, book);
            await this.genreService.SaveModificationAsync(genre, CurrentUser.Id);

            return mapper.Map<AssignedBookAuthorModel>(book);
        }


        [HttpPut("Assign/Genre/{genreId}/Author/{authorId}")]
        public async Task<AssignedAuthorGenreModel> AssignAuthor(int genreId, int authorId)
        {
            await AssignCurrentUserAsync();

            var genre = await this.finder.FindByIdOrDefaultAsync<Genre>(genreId);
            await this.validator.ValidateEntityAsync(genre, genreId.ToString());

            var author = await this.finder.FindByIdOrDefaultAsync<Author>(authorId);
            await this.validator.ValidateEntityAsync(author, authorId.ToString());

            await this.assigner.AssignAuthorAsync(genre, author);
            await this.assigner.AssignAuthorAsync(genre, author);
            await this.genreService.SaveModificationAsync(genre, CurrentUser.Id);

            return mapper.Map<AssignedAuthorGenreModel>(author);
        }


        [HttpPut("Favorites/Add/Genre/{genreId}")]
        public async Task<AddedFavoriteGenreModel> AddFavorite(int genreId)
        {
            await AssignCurrentUserAsync();
            var genre = await this.finder.FindByIdOrDefaultAsync<Genre>(genreId);
            await this.validator.ValidateEntityAsync(genre, genreId.ToString());

            await this.genreService.AddFavoriteAsync(genre, CurrentUser);
            return mapper.Map<AddedFavoriteGenreModel>(genre);
        }

        [HttpPut("Favorites/Remove/Genre/{genreId}")]
        public async Task<RemovedFavoriteGenreModel> RemoveFavorite(int genreId)
        {
            await AssignCurrentUserAsync();
            var genre = await this.finder.FindByIdOrDefaultAsync<Genre>(genreId);
            await this.validator.ValidateEntityAsync(genre, genreId.ToString());
            await this.genreService.RemoveFavoriteAsync(genre, CurrentUser);
            var removedFavoriteGenre = mapper.Map<RemovedFavoriteGenreModel>(genre);
            removedFavoriteGenre.UserId = CurrentUser.Id;
            return removedFavoriteGenre;
        }

        [HttpDelete("Delete/Genre/{genreId}")]
        public async Task<DeletedGenreModel> Delete(int genreId)
        {
            await AssignCurrentUserAsync();
            var genre = await this.finder.FindByIdOrDefaultAsync<Genre>(genreId);
            await this.validator.ValidateEntityAsync(genre, genreId.ToString());
            await this.genreService.DeleteAsync(genre, CurrentUser.Id);
            return mapper.Map<DeletedGenreModel>(genre);
        }
    }
}
