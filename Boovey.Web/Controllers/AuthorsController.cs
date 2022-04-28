namespace Boovey.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using AutoMapper;
    using Base;
    using Services.Constants;
    using Services.Exceptions;
    using Services.Interfaces;
    using Data.Entities;
    using Models.Requests.AuthorModels;
    using Models.Responses.AuthorModels;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : BooveyBaseController
    {
        private readonly IAuthorService authorService;
        private readonly IMapper mapper;
        public AuthorsController(IUserService userService, IAuthorService authorService, IMapper mapper) : base(userService)
        {
            this.authorService = authorService;
            this.mapper = mapper;
        }

        [HttpGet("List/")]
        public async Task<ActionResult<IEnumerable<AuthorListingModel>>> Get()
        {
            var allGenres = await this.authorService.GetAllActiveAsync();
            return mapper.Map<ICollection<AuthorListingModel>>(allGenres).ToList();
        }

        [HttpGet("Get/{authorId}")]
        public async Task<ActionResult<AuthorListingModel>> GetById(int authorId)
        {
            var author = await this.authorService.GetActiveByIdAsync(authorId);
            return mapper.Map<AuthorListingModel>(author);
        }

        [HttpPost("Add/")]
        public async Task<ActionResult> Create(CreateAuthorModel authorInput)
        {
            await AssignCurrentUserAsync();

            var alreadyExists = await this.authorService.ContainsActiveByNameAsync(authorInput.Fullname);
            if (alreadyExists)
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyContained, nameof(Author)));

            var author = await this.authorService.CreateAsync(authorInput, CurrentUser.Id);
            var createdAuthor = mapper.Map<CreatedAuthorModel>(author);

            return CreatedAtAction(nameof(Get), "Authors", new { id = createdAuthor.Id }, createdAuthor);
        }

        [HttpPut("Edit/{authorId}")]
        public async Task<ActionResult<EditedAuthorModel>> Edit(EditAuthorModel authorInput, int authorId)
        {
            await AssignCurrentUserAsync();

            var author = await this.authorService.GetActiveByIdAsync(authorId);
            await this.authorService.EditAsync(author, authorInput, CurrentUser.Id);

            return mapper.Map<EditedAuthorModel>(author);
        }

        [HttpPut("Add-To-Favorites/{authorId}")]
        public async Task<AddedFavoriteAuthorModel> AddFavorite(int authorId)
        {
            await AssignCurrentUserAsync();
            var author = await this.authorService.GetActiveByIdAsync(authorId);
            await this.authorService.AddFavoriteAuthorAsync(author, CurrentUser);
            return mapper.Map<AddedFavoriteAuthorModel>(author);
        }

        [HttpPut("Remove-From-Favorites/{authorId}")]
        public async Task<RemovedFavoriteAuthorModel> RemoveFavorite(int authorId)
        {
            await AssignCurrentUserAsync();
            var author = await this.authorService.GetActiveByIdAsync(authorId);
            await this.authorService.RemoveFavoriteAuthorAsync(author, CurrentUser);
            var removedFavoriteAuthor = mapper.Map<RemovedFavoriteAuthorModel>(author);
            removedFavoriteAuthor.UserId = CurrentUser.Id;
            return removedFavoriteAuthor;
        }

        [HttpDelete("Delete/{authorId}")]
        public async Task<DeletedAuthorModel> Delete(int authorId)
        {
            await AssignCurrentUserAsync();
            var author = await this.authorService.GetActiveByIdAsync(authorId);
            await this.authorService.DeleteAsync(author, CurrentUser.Id);
            return mapper.Map<DeletedAuthorModel>(author);
        }
    }
}
