namespace Boovey.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using AutoMapper;
    using Base;
    using Services.Interfaces;
    using Services.Interfaces.IHandlers;
    using Models.Responses.SharedModels;
    using Data.Entities;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AssignController : BooveyBaseController
    {
        private readonly IBookService bookService;
        private readonly ISearchService<Book> bookSearchService;
        private readonly ISearchService<Author> authorSearchService;
        private readonly ISearchService<Genre> genreSearchService;
        private readonly ISearchService<Publisher> publisherSearchService;
        private readonly IMapper mapper;
        public AssignController(IBookService bookService, ISearchService<Book> bookSearchService, ISearchService<Author> authorSearchService,
        ISearchService<Genre> genreSearchService, ISearchService<Publisher> publisherSearchService,
        IMapper mapper, IUserService userService)
        : base(userService)
        {
            this.bookService = bookService;
            this.bookSearchService = bookSearchService;
            this.authorSearchService = authorSearchService;
            this.genreSearchService = genreSearchService;
            this.publisherSearchService = publisherSearchService;
            this.mapper = mapper;
        }

        [HttpPut("Book/{bookId}/Author/{authorId}")]
        public async Task<AssignedBookAuthorModel> AssignAuthor(int bookId, int authorId)
        {
            await AssignCurrentUserAsync();
            var book = await this.bookSearchService.GetActiveByIdAsync(bookId, nameof(Book));
            var author = await this.authorSearchService.GetActiveByIdAsync(authorId, nameof(Author));
            var updatedBook = await this.bookService.AssignAuthorAsync(book, author, nameof(Author), CurrentUser.Id);
            return mapper.Map<AssignedBookAuthorModel>(updatedBook);
        }

        [HttpPut("Book/{bookId}/Genre/{genreId}")]
        public async Task<AssignedBookGenreModel> AssignGenre(int bookId, int genreId)
        {
            await AssignCurrentUserAsync();
            var book = await this.bookSearchService.GetActiveByIdAsync(bookId, nameof(Book));
            var genre = await this.genreSearchService.GetActiveByIdAsync(genreId, nameof(Genre));
            var updatedBook = await this.bookService.AssignGenreAsync(book, genre, nameof(Genre), CurrentUser.Id);
            return mapper.Map<AssignedBookGenreModel>(updatedBook);
        }

        [HttpPut("Book/{bookId}/Publisher/{publisherId}")]
        public async Task<AssignedBookPublisherModel> AssignPublisher(int bookId, int publisherId)
        {
            await AssignCurrentUserAsync();
            var book = await this.bookSearchService.GetActiveByIdAsync(bookId, nameof(Book));
            var publisher = await this.publisherSearchService.GetActiveByIdAsync(publisherId, nameof(Publisher));
            var updatedBook = await this.bookService.AssignPublisherAsync(book, publisher, nameof(Publisher), CurrentUser.Id);
            return mapper.Map<AssignedBookPublisherModel>(updatedBook);
        }
    }
}
