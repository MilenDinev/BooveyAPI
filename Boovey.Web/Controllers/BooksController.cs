namespace Boovey.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using AutoMapper;
    using Base;
    using Services.Interfaces.IHandlers;
    using Services.Interfaces.IEntities;
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
        private readonly IAssigner assigner;
        private readonly ISearchService searchService;
        private readonly IValidator validator;
        private readonly IMapper mapper;

        public BooksController(IBookService bookService,

            ISearchService searchService,
            IAssigner assigner,
            IValidator validator,
            IMapper mapper,
            IUserService userService)
            : base(userService)
        {
            this.bookService = bookService;
            this.assigner = assigner;
            this.searchService = searchService;
            this.validator = validator;
            this.mapper = mapper;
        }

        [HttpGet("List/")]
        public async Task<ActionResult<IEnumerable<BookListingModel>>> Get()
        {
            var allBooks = await this.searchService.GetAllActiveAsync<Book>();
            return mapper.Map<ICollection<BookListingModel>>(allBooks).ToList();
        }

        [HttpGet("Get/Book/{bookId}")]
        public async Task<ActionResult<BookListingModel>> GetById(int bookId)
        {
            var book = await this.searchService.FindByIdOrDefaultAsync<Book>(bookId);
            await this.validator.ValidateEntityAsync(book, bookId.ToString());

            return mapper.Map<BookListingModel>(book);
        }

        [HttpPost("Add/")]
        public async Task<ActionResult> Create(CreateBookModel bookInput)
        {
            await AssignCurrentUserAsync();

            var book = await this.searchService.FindByStringOrDefaultAsync<Book>(bookInput.Title);
            await this.validator.ValidateUniqueEntityAsync(book);

            var country = await this.searchService.FindByIdOrDefaultAsync<Country>(bookInput.CountryId);
            await this.validator.ValidateEntityAsync(country, bookInput.CountryId.ToString());

            book = await this.bookService.CreateAsync(bookInput, CurrentUser.Id);
            var bookResponse = mapper.Map<CreatedBookModel>(book);

            return CreatedAtAction(nameof(Get), "Books", new { id = bookResponse.Id }, bookResponse);
        }

        [HttpPut("Edit/Book/{bookId}")]
        public async Task<ActionResult<EditedBookModel>> Edit(EditBookModel bookInput, int bookId)
        {
            await AssignCurrentUserAsync();

            var book = await this.searchService.FindByIdOrDefaultAsync<Book>(bookId);
            await this.validator.ValidateEntityAsync(book, bookId.ToString());

            var country = await this.searchService.FindByIdOrDefaultAsync<Country>(bookInput.CountryId);
            await this.validator.ValidateEntityAsync(country, bookInput.CountryId.ToString());

            await this.bookService.EditAsync(book, bookInput, CurrentUser.Id);
            return mapper.Map<EditedBookModel>(book);
        }

        [HttpPut("Assign/Book/{bookId}/Author/{authorId}")]
        public async Task<AssignedBookAuthorModel> AssignAuthor(int bookId, int authorId)
        {
            await AssignCurrentUserAsync();

            var book = await this.searchService.FindByIdOrDefaultAsync<Book>(bookId);
            await this.validator.ValidateEntityAsync(book, bookId.ToString());

            var author = await this.searchService.FindByIdOrDefaultAsync<Author>(authorId);
            await this.validator.ValidateEntityAsync(author, authorId.ToString());

            // to be optimized
            await this.assigner.AssignAuthorAsync(book, author);
            await this.bookService.SaveModificationAsync(book, CurrentUser.Id);

            return mapper.Map<AssignedBookAuthorModel>(book);
        }

        [HttpPut("Assign/Book/{bookId}/Genre/{genreId}")]
        public async Task<AssignedBookGenreModel> AssignGenre(int bookId, int genreId)
        {
            await AssignCurrentUserAsync();

            var book = await this.searchService.FindByIdOrDefaultAsync<Book>(bookId);
            await this.validator.ValidateEntityAsync(book, bookId.ToString());

            var genre = await this.searchService.FindByIdOrDefaultAsync<Genre>(genreId);
            await this.validator.ValidateEntityAsync(genre, genreId.ToString());

            await this.assigner.AssignGenreAsync(book, genre);
            await this.bookService.SaveModificationAsync(book, CurrentUser.Id);

            return mapper.Map<AssignedBookGenreModel>(book);
        }

        [HttpPut("Assign/Book/{bookId}/Publisher/{publisherId}")]
        public async Task<AssignedBookPublisherModel> AssignPublisher(int bookId, int publisherId)
        {
            await AssignCurrentUserAsync();

            var book = await this.searchService.FindByIdOrDefaultAsync<Book>(bookId);
            await this.validator.ValidateEntityAsync(book, bookId.ToString());

            var publisher = await this.searchService.FindByIdOrDefaultAsync<Publisher>(publisherId);
            await this.validator.ValidateEntityAsync(publisher, publisherId.ToString());

            await this.assigner.AssignPublisherAsync(book, publisher);
            await this.bookService.SaveModificationAsync(book, CurrentUser.Id);

            return mapper.Map<AssignedBookPublisherModel>(book);
        }

        [HttpPut("Favorites/Add/Book/{bookId}")]
        public async Task<AddedFavoriteBookModel> AddFavorite(int bookId)
        {
            await AssignCurrentUserAsync();

            var book = await this.searchService.FindByIdOrDefaultAsync<Book>(bookId);
            await this.validator.ValidateEntityAsync(book, bookId.ToString());

            await this.bookService.AddFavoriteAsync(book, CurrentUser);
            return mapper.Map<AddedFavoriteBookModel>(book);
        }

        [HttpPut("Favorites/Remove/Book/{bookId}")]
        public async Task<RemovedFavoriteBookModel> RemoveFavorite(int bookId)
        {
            await AssignCurrentUserAsync();

            var book = await this.searchService.FindByIdOrDefaultAsync<Book>(bookId);
            await this.validator.ValidateEntityAsync(book, bookId.ToString());

            await this.bookService.RemoveFavoriteAsync(book, CurrentUser);
            var removedFavoriteBook = mapper.Map<RemovedFavoriteBookModel>(book);
            removedFavoriteBook.UserId = CurrentUser.Id;
            return removedFavoriteBook;
        }

        [HttpDelete("Delete/Book/{bookId}")]
        public async Task<DeletedBookModel> Delete(int bookId)
        {
            await AssignCurrentUserAsync();

            var book = await this.searchService.FindByIdOrDefaultAsync<Book>(bookId);

            await this.bookService.DeleteAsync(book, CurrentUser.Id);
            return mapper.Map<DeletedBookModel>(book);
        }
    }
}
