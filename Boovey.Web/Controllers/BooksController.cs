namespace Boovey.Web.Controllers
{
    using Boovey.Models.Requests;
    using Boovey.Models.Responses.BookModels;
    using Boovey.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult> Post(AddBookModel bookInput)
        {
            var addedBook = await this.bookService.AddAsync(bookInput);
            return CreatedAtAction(nameof(Get), "Users", new { title = addedBook.Title }, addedBook);
        }
    }
}
