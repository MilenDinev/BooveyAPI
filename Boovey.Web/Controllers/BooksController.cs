namespace Boovey.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using AutoMapper;
    using Helpers;
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
    public class BooksController : AssigningController
    {
        private readonly IMapper mapper;
        private readonly IBookService bookService;

        public BooksController(IMapper mapper, IUserService userService, IBookService bookService, IAuthorService authorService, IGenreService genreService, IPublisherService publisherService) 
            : base(userService, authorService, genreService, publisherService)
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
        public async Task<AssignedBookAuthorModel> AssignAuthor(int bookId, int authorId)
        {
            await AssignCurrentUserAsync();
            var book = await this.bookService.GetActiveByIdAsync(bookId);
            var author = await GetAuthorByIdAsync(authorId);

            var updatedBook = await this.bookService.AssignAuthorAsync(book, author, CurrentUser.Id);
            return mapper.Map<AssignedBookAuthorModel>(updatedBook);
        }

        [HttpPut("AssignGenre/Book/{bookId}/Genre/{genreId}")]
        public async Task<AssignedBookGenreModel> AssignGenre(int bookId, int genreId)
        {
            await AssignCurrentUserAsync();
            var book = await this.bookService.GetActiveByIdAsync(bookId);
            var genre = await GetGenreByIdAsync(genreId);

            var updatedBook = await this.bookService.AssignGenreAsync(book, genre, CurrentUser.Id);
            return mapper.Map<AssignedBookGenreModel>(updatedBook);
        }

        [HttpPut("AssignPublisher/Book/{bookId}/Publisher/{publisherId}")]
        public async Task<AssignedBookPublisherModel> AssignPublisher(int bookId, int publisherId)
        {
            await AssignCurrentUserAsync();
            var book = await this.bookService.GetActiveByIdAsync(bookId);
            var publisher = await GetPublisherByIdAsync(publisherId);

            var updatedBook = await this.bookService.AssignPublisherAsync(book, publisher, CurrentUser.Id);
            return mapper.Map<AssignedBookPublisherModel>(updatedBook);
        }

        [HttpPut("AddFavorite/Book/{bookId}")]
        public async Task<AddedFavoriteBookModel> AddFavorite(int bookId)
        {
            await AssignCurrentUserAsync();
            var book = await this.bookService.GetActiveByIdAsync(bookId);
            await this.bookService.AddFavoriteAsync(book, CurrentUser);
            return mapper.Map<AddedFavoriteBookModel>(book);
        }

        [HttpPut("RemoveFavorite/Book/{bookId}")]
        public async Task<RemovedFavoriteBookModel> RemoveFavorite(int bookId)
        {
            await AssignCurrentUserAsync();
            var book = await this.bookService.GetActiveByIdAsync(bookId);
            await this.bookService.RemoveFavoriteAsync(book, CurrentUser);
            var removedFavoriteBook = mapper.Map<RemovedFavoriteBookModel>(book);
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
