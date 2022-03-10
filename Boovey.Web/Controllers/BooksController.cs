﻿namespace Boovey.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Services.Interfaces;
    using Models.Requests;
    using Models.Responses.BookModels;
   
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
            return removedFavoriteBook;
        }
    }
}
