namespace Boovey.Web.Controllers
{
    using Boovey.Models.Requests.QuoteModels;
    using Boovey.Models.Responses.QuoteModels;
    using Boovey.Services.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : BooveyBaseController
    {
        private readonly IQuoteService quoteService;
        public QuotesController(IUserService userService, IQuoteService quoteService) : base(userService)
        {
            this.quoteService = quoteService;
        }

        [HttpPost("Add/")]
        public async Task<ActionResult> Add(AddQuoteModel quoteInput)
        {
            await GetCurrentUserAsync();
            var addedQuote = await this.quoteService.AddAsync(quoteInput, CurrentUser.Id);
            return CreatedAtAction(nameof(Add), "Quotes", new { id = addedQuote.Id }, addedQuote);
        }

        [HttpPut("Edit/{quoteId}")]
        public async Task<ActionResult<EditedQuoteModel>> Edit(EditQuoteModel quoteInput, int quoteId)
        {
            await GetCurrentUserAsync();
            var editedQuote = await this.quoteService.EditAsync(quoteId, quoteInput, CurrentUser.Id);
            return editedQuote;
        }

        [HttpPut("Add-To-Favorites/{quoteId}")]
        public async Task<AddedFavoriteQuoteModel> AddFavorite(int quoteId)
        {
            await GetCurrentUserAsync();
            var addedFavoriteQuote = await this.quoteService.AddFavoriteQuote(quoteId, CurrentUser);
            return addedFavoriteQuote;
        }

        [HttpPut("Remove-From-Favorites/{quoteId}")]
        public async Task<RemovedFavoriteQuoteModel> RemoveFavorite(int quoteId)
        {
            await GetCurrentUserAsync();
            var removedFavoriteQuote = await this.quoteService.RemoveFavoriteQuote(quoteId, CurrentUser);
            return removedFavoriteQuote;
        }
    }
}
