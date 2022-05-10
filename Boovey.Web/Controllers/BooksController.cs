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
    using Data.Entities;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : BooveyBaseController
    {
        private readonly IBookService bookService;
        private readonly ISearchService<Book> bookSearchService;
        private readonly ISearchService<Country> countrySearchService;
        private readonly IMapper mapper;

        public BooksController(IBookService bookService, 
            ISearchService<Book> bookSearchService,ISearchService<Country> countrySearchService,
           IMapper mapper, IUserService userService) 
            : base(userService)
        {
            this.bookService = bookService;
            this.bookSearchService = bookSearchService;
            this.countrySearchService = countrySearchService;
            this.mapper = mapper;
        }

        [HttpGet("List/")]
        public async Task<ActionResult<IEnumerable<BookListingModel>>> Get()
        {
            var allBooks = await this.bookSearchService.GetAllActiveAsync();
            return mapper.Map<ICollection<BookListingModel>>(allBooks).ToList();
        }

        [HttpGet("Get/Book/{bookId}")]
        public async Task<ActionResult<BookListingModel>> GetById(int bookId)
        {
            var book = await this.bookSearchService.GetActiveByIdAsync(bookId, nameof(Book));
            return mapper.Map<BookListingModel>(book);
        }

        [HttpPost("Add/")]
        public async Task<ActionResult> Create(CreateBookModel bookInput)
        {
            await AssignCurrentUserAsync();
            var alreadyExists = await this.bookSearchService.ContainsActiveByStringAsync(bookInput.Title);
            if (alreadyExists)
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyContained, nameof(Book)));

            await this.countrySearchService.GetActiveByIdAsync(bookInput.CountryId, nameof(Country));
            var book = await this.bookService.CreateAsync(bookInput, CurrentUser.Id);
            var createdBook = mapper.Map<CreatedBookModel>(book);

            return CreatedAtAction(nameof(Get), "Books", new { id = createdBook.Id }, createdBook);
        }

        [HttpPut("Edit/Book/{bookId}")]
        public async Task<ActionResult<EditedBookModel>> Edit(EditBookModel bookInput, int bookId)
        {
            await AssignCurrentUserAsync();
            await this.countrySearchService.GetActiveByIdAsync(bookInput.CountryId, nameof(Country));
            var book = await this.bookSearchService.GetActiveByIdAsync(bookId, nameof(Book));
            await this.bookService.EditAsync(book, bookInput, CurrentUser.Id);
            return mapper.Map<EditedBookModel>(book);
        }

        [HttpPut("Favorites/Add/Book/{bookId}")]
        public async Task<AddedFavoriteBookModel> AddFavorite(int bookId)
        {
            await AssignCurrentUserAsync();
            var book = await this.bookSearchService.GetActiveByIdAsync(bookId, nameof(Book));
            await this.bookService.AddFavoriteAsync(book, CurrentUser);
            return mapper.Map<AddedFavoriteBookModel>(book);
        }

        [HttpPut("Favorites/Remove/Book/{bookId}")]
        public async Task<RemovedFavoriteBookModel> RemoveFavorite(int bookId)
        {
            await AssignCurrentUserAsync();
            var book = await this.bookSearchService.GetActiveByIdAsync(bookId, nameof(Book));
            await this.bookService.RemoveFavoriteAsync(book, CurrentUser);
            var removedFavoriteBook = mapper.Map<RemovedFavoriteBookModel>(book);
            removedFavoriteBook.UserId = CurrentUser.Id;
            return removedFavoriteBook;
        }

        [HttpDelete("Delete/Book/{bookId}")]
        public async Task<DeletedBookModel> Delete(int bookId)
        {
            await AssignCurrentUserAsync();
            var book = await this.bookSearchService.GetActiveByIdAsync(bookId, nameof(Book));
            await this.bookService.DeleteAsync(book, CurrentUser.Id);
            return mapper.Map<DeletedBookModel>(book);
        }
    }
}
