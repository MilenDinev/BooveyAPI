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
        /// ISearch service to be fixed 
        private readonly ISearchService<Entity> searchService;
        private readonly IMapper mapper;
        public AssignController(IBookService bookService, ISearchService<Entity> searchService,
        IMapper mapper, IUserService userService)
        : base(userService)
        {
            this.bookService = bookService;
            this.searchService = searchService;
            this.mapper = mapper;
        }

        [HttpPut("Book/{bookId}/Author/{authorId}")]
        public async Task<AssignedBookAuthorModel> AssignAuthor(int bookId, int authorId)
        {
            await AssignCurrentUserAsync();
            var book = await this.searchService.GetActiveBookByIdAsync(bookId);
            var author = await this.searchService.GetActiveAuthorByIdAsync(authorId);
            var updatedBook = await this.bookService.AssignAuthorAsync(book, author, CurrentUser.Id);
            return mapper.Map<AssignedBookAuthorModel>(updatedBook);
        }

        [HttpPut("Book/{bookId}/Genre/{genreId}")]
        public async Task<AssignedBookGenreModel> AssignGenre(int bookId, int genreId)
        {
            await AssignCurrentUserAsync();
            var book = await this.searchService.GetActiveBookByIdAsync(bookId);
            var genre = await this.searchService.GetActiveGenreByIdAsync(genreId);
            var updatedBook = await this.bookService.AssignGenreAsync(book, genre, CurrentUser.Id);
            return mapper.Map<AssignedBookGenreModel>(updatedBook);
        }

        [HttpPut("Book/{bookId}/Publisher/{publisherId}")]
        public async Task<AssignedBookPublisherModel> AssignPublisher(int bookId, int publisherId)
        {
            await AssignCurrentUserAsync();
            var book = await this.searchService.GetActiveBookByIdAsync(bookId);
            var publisher = await this.searchService.GetActivePublisherByIdAsync(publisherId);
            var updatedBook = await this.bookService.AssignPublisherAsync(book, publisher, CurrentUser.Id);
            return mapper.Map<AssignedBookPublisherModel>(updatedBook);
        }
    }
}
