namespace Boovey.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Services.Interfaces;
    using Models.Requests.BookModels;
    using Models.Responses.BookModels;
    using Models.Responses.SharedModels;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : BooveyBaseController
    {
        private readonly IBookService bookService;
        public BooksController(IUserService userService, IBookService bookService) : base(userService)
        {
            this.bookService = bookService;
        }

        [HttpGet("List/")]
        public async Task<ActionResult<IEnumerable<BooksListingModel>>> Get()
        {
            var allBooks = await this.bookService.GetAllBooksAsync();
            return allBooks.ToList();
        }

        [HttpPost("Add/")]
        public async Task<ActionResult> Add(AddBookModel bookInput)
        {
            await GetCurrentUserAsync();
            var addedBook = await this.bookService.AddAsync(bookInput, CurrentUser.Id);
            return CreatedAtAction(nameof(Get), "Books", new { title = addedBook.Title }, addedBook);
        }

        [HttpPut("Edit/{bookId}")]
        public async Task<ActionResult<EditedBookModel>> Edit(EditBookModel bookInput, int bookId)
        {
            await GetCurrentUserAsync();
            var editedBook = await this.bookService.EditAsync(bookId, bookInput, CurrentUser.Id);
            return editedBook;
        }

        [HttpPut("Assign/{bookId}/Author/{authorId}")]
        public async Task<AssignedAuthorBookModel> AssignAuthor(int bookId, int authorId)
        {
            await GetCurrentUserAsync();
            var assignedAuthorModel = await this.bookService.AssignAuthorAsync(bookId, authorId, CurrentUser.Id);
            return assignedAuthorModel;
        }

        [HttpPut("Assign/Book/{bookId}/Genre/{genreId}")]
        public async Task<AssignedBookGenreModel> AssignGenre(int bookId, int genreId)
        {
            await GetCurrentUserAsync();
            var assignedGenreModel = await this.bookService.AssignGenreAsync(bookId, genreId, CurrentUser.Id);
            return assignedGenreModel;
        }

        [HttpPut("Assign/Book/{bookId}/Publisher/{publisherId}")]
        public async Task<AssignedPublisherBookModel> AssignPublisher(int bookId, int publisherId)
        {
            await GetCurrentUserAsync();
            var assignedPublisherModel = await this.bookService.AssignPublisherAsync(bookId, publisherId, CurrentUser.Id);
            return assignedPublisherModel;
        }

        [HttpPut("Add-To-Favorites/{bookId}")]
        public async Task<AddedFavoriteBookModel> AddFavorite(int bookId)
        {
            await GetCurrentUserAsync();
            var addedFavoriteBook = await this.bookService.AddFavoriteBook(bookId, CurrentUser);
            return addedFavoriteBook;
        }

        [HttpPut("Remove-From-Favorites/{bookId}")]
        public async Task<RemovedFavoriteBookModel> RemoveFavorite(int bookId)
        {
            await GetCurrentUserAsync();
            var removedFavoriteBook = await this.bookService.RemoveFavoriteBook(bookId, CurrentUser);
            removedFavoriteBook.UserId = CurrentUser.Id;
            return removedFavoriteBook;
        }
    }
}
