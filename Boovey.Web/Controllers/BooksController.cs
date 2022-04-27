namespace Boovey.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using AutoMapper;
    using Services.Interfaces;
    using Models.Requests.BookModels;
    using Models.Responses.BookModels;
    using Models.Responses.SharedModels;
    using Services.Exceptions;
    using Services.Constants;
    using Data.Entities;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : BooveyBaseController
    {
        private readonly IBookService bookService;
        private readonly IMapper mapper;

        public BooksController(IUserService userService, IBookService bookService, IMapper mapper) : base(userService)
        {
            this.bookService = bookService;
            this.mapper = mapper;
        }

        [HttpGet("List/")]
        public async Task<ActionResult<IEnumerable<BookListingModel>>> Get()
        {
            var allBooks = await this.bookService.GetAllActiveAsync();
            return mapper.Map<ICollection<BookListingModel>>(allBooks).ToList();
        }

        [HttpPost("Add/")]
        public async Task<ActionResult> Add(CreateBookModel bookInput)
        {
            await AssignCurrentUserAsync();
            var allBooks = await this.bookService.GetAllActiveAsync();
            var alreadyExists = await this.bookService.ContainsActiveByTitleAsync(bookInput.Title, allBooks);
            if (alreadyExists)
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyContained, nameof(Book)));

            var addedBook = await this.bookService.CreateAsync(bookInput, CurrentUser.Id);
            return CreatedAtAction(nameof(Get), "Books", new { id = addedBook.Id }, addedBook);
        }

        [HttpPut("Edit/Book/{bookId}")]
        public async Task<ActionResult<EditedBookModel>> Edit(EditBookModel bookInput, int bookId)
        {
            await AssignCurrentUserAsync();
            var book = await this.bookService.GetActiveByIdAsync(bookId);
            await this.bookService.EditAsync(book, bookInput, CurrentUser.Id);

            return mapper.Map<EditedBookModel>(book);
        }

        [HttpPut("AssignAuthor/Book/{bookId}/Author/{authorId}")]
        public async Task<AssignedAuthorBookModel> AssignAuthor(int bookId, int authorId)
        {
            await AssignCurrentUserAsync();
            var assignedAuthorModel = await this.bookService.AssignAuthorAsync(bookId, authorId, CurrentUser.Id);
            return assignedAuthorModel;
        }

        [HttpPut("AssignGenre/Book/{bookId}/Genre/{genreId}")]
        public async Task<AssignedBookGenreModel> AssignGenre(int bookId, int genreId)
        {
            await AssignCurrentUserAsync();
            var assignedGenreModel = await this.bookService.AssignGenreAsync(bookId, genreId, CurrentUser.Id);
            return assignedGenreModel;
        }

        [HttpPut("AssignPublisher/Book/{bookId}/Publisher/{publisherId}")]
        public async Task<AssignedPublisherBookModel> AssignPublisher(int bookId, int publisherId)
        {
            await AssignCurrentUserAsync();
            var assignedPublisherModel = await this.bookService.AssignPublisherAsync(bookId, publisherId, CurrentUser.Id);
            return assignedPublisherModel;
        }

        [HttpPut("AddFavorite/Book/{bookId}")]
        public async Task<AddedFavoriteBookModel> AddFavorite(int bookId)
        {
            await AssignCurrentUserAsync();
            var addedFavoriteBook = await this.bookService.AddFavorite(bookId, CurrentUser);
            addedFavoriteBook.UserId = CurrentUser.Id;
            return addedFavoriteBook;
        }

        [HttpPut("RemoveFavorite/Book/{bookId}")]
        public async Task<RemovedFavoriteBookModel> RemoveFavorite(int bookId)
        {
            await AssignCurrentUserAsync();
            var removedFavoriteBook = await this.bookService.RemoveFavorite(bookId, CurrentUser);
            removedFavoriteBook.UserId = CurrentUser.Id;
            return removedFavoriteBook;
        }

        [HttpDelete("Delete/Book/{bookId}")]
        public async Task<DeletedBookModel> Delete(int bookId)
        {
            await AssignCurrentUserAsync();
            var book = await this.bookService.GetActiveByIdAsync(bookId);
            await this.bookService.DeleteAsync(book, CurrentUser.Id);
            return mapper.Map<DeletedBookModel>(book);
        }
    }
}
