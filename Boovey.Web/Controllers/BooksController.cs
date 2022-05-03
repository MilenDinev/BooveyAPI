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
        private readonly IAccessorService<Book> bookAccessorService;
        private readonly IAccessorService<Country> countryAccessorService;
        private readonly IMapper mapper;

        public BooksController(IBookService bookService, 
            IAccessorService<Book> bookAccessorService,IAccessorService<Country> countryAccessorService,
           IMapper mapper, IUserService userService) 
            : base(userService)
        {
            this.bookService = bookService;
            this.bookAccessorService = bookAccessorService;
            this.countryAccessorService = countryAccessorService;
            this.mapper = mapper;
        }

        [HttpGet("List/")]
        public async Task<ActionResult<IEnumerable<BookListingModel>>> Get()
        {
            var allBooks = await this.bookAccessorService.GetAllActiveAsync();
            return mapper.Map<ICollection<BookListingModel>>(allBooks).ToList();
        }

        [HttpPost("Add/")]
        public async Task<ActionResult> Add(CreateBookModel bookInput)
        {
            await AssignCurrentUserAsync();
            await this.countryAccessorService.GetActiveByIdAsync(bookInput.CountryId, nameof(Country));
            var allBooks = await this.bookAccessorService.GetAllActiveAsync();
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
            await this.countryAccessorService.GetActiveByIdAsync(bookInput.CountryId, nameof(Country));
            var book = await this.bookAccessorService.GetActiveByIdAsync(bookId, nameof(Book));
            await this.bookService.EditAsync(book, bookInput, CurrentUser.Id);
            return mapper.Map<EditedBookModel>(book);
        }

        [HttpPut("Favorites/Add/Book/{bookId}")]
        public async Task<AddedFavoriteBookModel> AddFavorite(int bookId)
        {
            await AssignCurrentUserAsync();
            var book = await this.bookAccessorService.GetActiveByIdAsync(bookId, nameof(Book));
            await this.bookService.AddFavoriteAsync(book, CurrentUser);
            return mapper.Map<AddedFavoriteBookModel>(book);
        }

        [HttpPut("Favorites/Remove/Book/{bookId}")]
        public async Task<RemovedFavoriteBookModel> RemoveFavorite(int bookId)
        {
            await AssignCurrentUserAsync();
            var book = await this.bookAccessorService.GetActiveByIdAsync(bookId, nameof(Book));
            await this.bookService.RemoveFavoriteAsync(book, CurrentUser);
            var removedFavoriteBook = mapper.Map<RemovedFavoriteBookModel>(book);
            removedFavoriteBook.UserId = CurrentUser.Id;
            return removedFavoriteBook;
        }

        [HttpDelete("Delete/Book/{bookId}")]
        public async Task<DeletedBookModel> Delete(int bookId)
        {
            await AssignCurrentUserAsync();
            var book = await this.bookAccessorService.GetActiveByIdAsync(bookId, nameof(Book));
            await this.bookService.DeleteAsync(book, CurrentUser.Id);
            return mapper.Map<DeletedBookModel>(book);
        }
    }
}
