namespace Boovey.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using AutoMapper;
    using Base;
    using Services.Constants;
    using Services.Interfaces;
    using Services.Exceptions;
    using Services.Interfaces.IHandlers;
    using Models.Requests.BookModels;
    using Models.Responses.BookModels;
    using Models.Responses.SharedModels;
    using Data.Entities;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : BooveyBaseController
    {
        private readonly IBookService bookService;
        private readonly IContextAccessorService<Book> booksAccessorService;
        private readonly IContextAccessorService<Author> authorsAccessorService;
        private readonly IContextAccessorService<Genre> genresAccessorService;
        private readonly IContextAccessorService<Publisher> publishersAccessorService;
        private readonly IMapper mapper;

        public BooksController(IBookService bookService, 
            IContextAccessorService<Book> booksAccessorService, IContextAccessorService<Author> authorsAccessorService, 
            IContextAccessorService<Genre> genresAccessorService, IContextAccessorService<Publisher> publishersAccessorService,
           IMapper mapper, IUserService userService) 
            : base(userService)
        {
            this.bookService = bookService;
            this.booksAccessorService = booksAccessorService;
            this.authorsAccessorService = authorsAccessorService;
            this.genresAccessorService = genresAccessorService;
            this.publishersAccessorService = publishersAccessorService;
            this.mapper = mapper;
        }

        [HttpGet("List/")]
        public async Task<ActionResult<IEnumerable<BookListingModel>>> Get()
        {
            var allBooks = await this.booksAccessorService.GetAllActiveAsync();
            return mapper.Map<ICollection<BookListingModel>>(allBooks).ToList();
        }

        [HttpPost("Add/")]
        public async Task<ActionResult> Add(CreateBookModel bookInput)
        {
            await AssignCurrentUserAsync();
            var allBooks = await this.booksAccessorService.GetAllActiveAsync();
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
            var book = await this.booksAccessorService.GetActiveByIdAsync(bookId);
            await this.bookService.EditAsync(book, bookInput, CurrentUser.Id);
            return mapper.Map<EditedBookModel>(book);
        }

        [HttpPut("Assign/Book/{bookId}/Author/{authorId}")]
        public async Task<AssignedBookAuthorModel> AssignAuthor(int bookId, int authorId)
        {
            await AssignCurrentUserAsync();
            var book = await this.booksAccessorService.GetActiveByIdAsync(bookId);
            var author = await this.authorsAccessorService.GetActiveByIdAsync(authorId);
            var updatedBook = await this.bookService.AssignAuthorAsync(book, author, CurrentUser.Id);
            return mapper.Map<AssignedBookAuthorModel>(updatedBook);
        }

        [HttpPut("Assign/Book/{bookId}/Genre/{genreId}")]
        public async Task<AssignedBookGenreModel> AssignGenre(int bookId, int genreId)
        {
            await AssignCurrentUserAsync();
            var book = await this.booksAccessorService.GetActiveByIdAsync(bookId);
            var genre = await this.genresAccessorService.GetActiveByIdAsync(genreId);
            var updatedBook = await this.bookService.AssignGenreAsync(book, genre, CurrentUser.Id);
            return mapper.Map<AssignedBookGenreModel>(updatedBook);
        }

        [HttpPut("Assign/Book/{bookId}/Publisher/{publisherId}")]
        public async Task<AssignedBookPublisherModel> AssignPublisher(int bookId, int publisherId)
        {
            await AssignCurrentUserAsync();
            var book = await this.booksAccessorService.GetActiveByIdAsync(bookId);
            var publisher = await this.publishersAccessorService.GetActiveByIdAsync(publisherId);
            var updatedBook = await this.bookService.AssignPublisherAsync(book, publisher, CurrentUser.Id);
            return mapper.Map<AssignedBookPublisherModel>(updatedBook);
        }

        [HttpPut("Favorites/Add/Book/{bookId}")]
        public async Task<AddedFavoriteBookModel> AddFavorite(int bookId)
        {
            await AssignCurrentUserAsync();
            var book = await this.booksAccessorService.GetActiveByIdAsync(bookId);
            await this.bookService.AddFavoriteAsync(book, CurrentUser);
            return mapper.Map<AddedFavoriteBookModel>(book);
        }

        [HttpPut("Favorites/Remove/Book/{bookId}")]
        public async Task<RemovedFavoriteBookModel> RemoveFavorite(int bookId)
        {
            await AssignCurrentUserAsync();
            var book = await this.booksAccessorService.GetActiveByIdAsync(bookId);
            await this.bookService.RemoveFavoriteAsync(book, CurrentUser);
            var removedFavoriteBook = mapper.Map<RemovedFavoriteBookModel>(book);
            removedFavoriteBook.UserId = CurrentUser.Id;
            return removedFavoriteBook;
        }

        [HttpDelete("Delete/Book/{bookId}")]
        public async Task<DeletedBookModel> Delete(int bookId)
        {
            await AssignCurrentUserAsync();
            var book = await this.booksAccessorService.GetActiveByIdAsync(bookId);
            await this.bookService.DeleteAsync(book, CurrentUser.Id);
            return mapper.Map<DeletedBookModel>(book);
        }
    }
}
