namespace Boovey.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using AutoMapper;
    using Base;
    using Services.Handlers.Interfaces;
    using Services.Managers.Interfaces;
    using Services.MainServices.Interfaces;
    using Data.Entities;
    using Models.Requests.AuthorModels;
    using Models.Responses.SharedModels;
    using Models.Responses.AuthorModels;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : BooveyBaseController
    {
        private readonly IAuthorService authorService;
        private readonly IAssigner assigner;
        private readonly IFavoritesManager favoritesManager;
        private readonly IFinder finder;
        private readonly IValidator validator;
        private readonly IMapper mapper;

        public AuthorsController(IAuthorService authorService, 
            IAssigner assigner,
            IFavoritesManager favoritesManager,
            IFinder finder,
            IValidator validator,
            IMapper mapper, 
            IUserService userService) 
            : base(userService)
        {
            this.authorService = authorService;
            this.assigner = assigner;
            this.favoritesManager = favoritesManager;
            this.finder = finder;
            this.validator = validator;
            this.mapper = mapper;
        }

        [HttpGet("List/")]
        public async Task<ActionResult<IEnumerable<AuthorListingModel>>> Get()
        {
            var allGenres = await this.finder.GetAllActiveAsync<Author>();
            return mapper.Map<ICollection<AuthorListingModel>>(allGenres).ToList();
        }

        [HttpGet("Get/Author/{authorId}")]
        public async Task<ActionResult<AuthorListingModel>> GetById(int authorId)
        {
            var author = await this.finder.FindByIdOrDefaultAsync<Author>(authorId);
            await this.validator.ValidateEntityAsync(author, authorId.ToString());

            return mapper.Map<AuthorListingModel>(author);
        }

        [HttpPost("Add/")]
        public async Task<ActionResult> Create(CreateAuthorModel authorInput)
        {
            await AssignCurrentUserAsync();
            var author = await this.finder.FindByStringOrDefaultAsync<Author>(authorInput.Fullname);
            await this.validator.ValidateUniqueEntityAsync(author);

            var country = await this.finder.FindByIdOrDefaultAsync<Country>(authorInput.CountryId);
            await this.validator.ValidateEntityAsync(country, authorInput.CountryId.ToString());

            author = await this.authorService.CreateAsync(authorInput, CurrentUser.Id);
            var createdAuthor = mapper.Map<CreatedAuthorModel>(author);

            return CreatedAtAction(nameof(Get), "Authors", new { id = createdAuthor.Id }, createdAuthor);
        }

        [HttpPut("Edit/Author/{authorId}")]
        public async Task<ActionResult<EditedAuthorModel>> Edit(EditAuthorModel authorInput, int authorId)
        {
            await AssignCurrentUserAsync();

            var author = await this.finder.FindByIdOrDefaultAsync<Author>(authorId);
            await this.validator.ValidateEntityAsync(author, authorId.ToString());

            var country = await this.finder.FindByIdOrDefaultAsync<Country>(authorInput.CountryId);
            await this.validator.ValidateEntityAsync(country, authorInput.CountryId.ToString());

            await this.authorService.EditAsync(author, authorInput, CurrentUser.Id);

            return mapper.Map<EditedAuthorModel>(author);
        }

        [HttpPut("Assign/Author/{authorId}/Book/{bookId}")]
        public async Task<AssignedBookAuthorModel> AssignBook(int authorId, int bookId)
        {
            await AssignCurrentUserAsync();

            var author = await this.finder.FindByIdOrDefaultAsync<Author>(authorId);
            await this.validator.ValidateEntityAsync(author, authorId.ToString());
            
            var book = await this.finder.FindByIdOrDefaultAsync<Book>(bookId);       
            await this.validator.ValidateEntityAsync(book, bookId.ToString());

            await this.validator.ValidateAssigningBook(author, book);
            await this.assigner.AssignBookAsync(author, book);
            await this.authorService.SaveModificationAsync(author, CurrentUser.Id);

            return mapper.Map<AssignedBookAuthorModel>(book);
        }

        [HttpPut("Assign/Author/{authorId}/Genre/{genreId}")]
        public async Task<AssignedAuthorGenreModel> AssignGenre(int authorId, int genreId)
        {
            await AssignCurrentUserAsync();

            var author = await this.finder.FindByIdOrDefaultAsync<Author>(authorId);
            await this.validator.ValidateEntityAsync(author, authorId.ToString());

            var genre = await this.finder.FindByIdOrDefaultAsync<Genre>(genreId);
            await this.validator.ValidateEntityAsync(genre, genreId.ToString());

            await this.validator.ValidateAssigningGenre(author, genre);
            await this.assigner.AssignGenreAsync(author, genre);
            await this.authorService.SaveModificationAsync(author, CurrentUser.Id);

            return mapper.Map<AssignedAuthorGenreModel>(author);
        }


        [HttpPut("Favorites/Add/Author/{authorId}")]
        public async Task<AddedFavoriteAuthorModel> AddFavorite(int authorId)
        {
            await AssignCurrentUserAsync();

            var author = await this.finder.FindByIdOrDefaultAsync<Author>(authorId);

            await this.validator.ValidateEntityAsync(author, authorId.ToString());
            await this.validator.ValidateAddingFavorite(authorId, this.CurrentUser.FavoriteAuthors);

            await this.favoritesManager.AddFavoriteAuthor(author, this.CurrentUser);

            await this.authorService.SaveModificationAsync(author, CurrentUser.Id);

            return mapper.Map<AddedFavoriteAuthorModel>(author);
        }

        [HttpPut("Favorites/Remove/Author/{authorId}")]
        public async Task<RemovedFavoriteAuthorModel> RemoveFavorite(int authorId)
        {
            await AssignCurrentUserAsync();

            var author = await this.finder.FindByIdOrDefaultAsync<Author>(authorId);
            await this.validator.ValidateEntityAsync(author, authorId.ToString());

            await this.validator.ValidateRemovingFavorite(authorId, this.CurrentUser.FavoriteAuthors);

            await this.favoritesManager.RemoveFavoriteAuthor(author, this.CurrentUser);
            await this.authorService.SaveModificationAsync(author, CurrentUser.Id);

            var removedFavoriteAuthor = mapper.Map<RemovedFavoriteAuthorModel>(author);
            removedFavoriteAuthor.UserId = CurrentUser.Id;
            return removedFavoriteAuthor;
        }

        [HttpDelete("Delete/Author/{authorId}")]
        public async Task<DeletedAuthorModel> Delete(int authorId)
        {
            await AssignCurrentUserAsync();

            var author = await this.finder.FindByIdOrDefaultAsync<Author>(authorId);
            await this.validator.ValidateEntityAsync(author, authorId.ToString());

            await this.authorService.DeleteAsync(author, CurrentUser.Id);
            return mapper.Map<DeletedAuthorModel>(author);
        }
    }
}
