namespace Boovey.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using Models.Requests.AuthorModels;
    using Models.Responses.AuthorModels;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : BooveyBaseController
    {
        private readonly IAuthorService authorService;
        public AuthorsController(IUserService userService, IAuthorService authorService) : base(userService)
        {
            this.authorService = authorService;
        }

        [HttpGet("List/")]
        public async Task<ActionResult<IEnumerable<AuthorListingModel>>> Get()
        {
            var allAuthors = await this.authorService.GetAllAuthorsAsync();
            return allAuthors.ToList();
        }

        [HttpGet("Get/{authorId}")]
        public async Task<ActionResult<AuthorListingModel>> GetById(int authorId)
        {
            var allAuthor = await this.authorService.GetAuthorById(authorId);
            return allAuthor;
        }

        [HttpPost("Add/")]
        public async Task<ActionResult> Add(AddAuthorModel authorInput)
        {
            await AssignCurrentUserAsync();
            var addedAuthor = await this.authorService.AddAsync(authorInput, CurrentUser.Id);
            return CreatedAtAction(nameof(Get), "Authors", new { id = addedAuthor.Id }, addedAuthor);
        }

        [HttpPut("Edit/{authorId}")]
        public async Task<ActionResult<EditedAuthorModel>> Edit(EditAuthorModel authorInput, int authorId)
        {
            await AssignCurrentUserAsync();
            var editedAuthor = await this.authorService.EditAsync(authorId, authorInput, CurrentUser.Id);
            return editedAuthor;
        }

        [HttpPut("Add-To-Favorites/{authorId}")]
        public async Task<AddedFavoriteAuthorModel> AddFavorite(int authorId)
        {
            await AssignCurrentUserAsync();
            var addedFavoriteAuthor = await this.authorService.AddFavoriteAuthor(authorId, CurrentUser);
            return addedFavoriteAuthor;
        }

        [HttpPut("Remove-From-Favorites/{authorId}")]
        public async Task<RemovedFavoriteAuthorModel> RemoveFavorite(int authorId)
        {
            await AssignCurrentUserAsync();
            var removedFavoriteAuthor = await this.authorService.RemoveFavoriteAuthor(authorId, CurrentUser);
            removedFavoriteAuthor.UserId = CurrentUser.Id;
            return removedFavoriteAuthor;
        }
    }
}
