namespace Boovey.Web.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Models.Requests.QuoteModels;
    using Models.Responses.QuoteModels;
    using Services.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : BooveyBaseController
    {
        private readonly IQuoteService quoteService;
        private readonly IMapper mapper;
        public QuotesController(IUserService userService, IQuoteService quoteService, IMapper mapper) : base(userService)
        {
            this.quoteService = quoteService;
            this.mapper = mapper;
        }

        [HttpPost("Add/")]
        public async Task<ActionResult> Add(CreateQuoteModel quoteInput)
        {
            await AssignCurrentUserAsync();
            var createdQuote = await this.quoteService.CreateAsync(quoteInput, CurrentUser.Id);
            return CreatedAtAction(nameof(Add), "Quotes", new { id = createdQuote.Id }, createdQuote);
        }

        [HttpPut("Edit/{quoteId}")]
        public async Task<ActionResult<EditedQuoteModel>> Edit(EditQuoteModel quoteInput, int quoteId)
        {
            await AssignCurrentUserAsync();

            var quote = await this.quoteService.GetActiveByIdAsync(quoteId);
            await this.quoteService.EditAsync(quote, quoteInput, CurrentUser.Id);

            return mapper.Map<EditedQuoteModel>(quote); ;
        }

        [HttpPut("Add-To-Favorites/{quoteId}")]
        public async Task<AddedFavoriteQuoteModel> AddFavorite(int quoteId)
        {
            await AssignCurrentUserAsync();

            var quote = await this.quoteService.GetActiveByIdAsync(quoteId);
            var addedFavoriteQuote = await this.quoteService.AddFavoriteAsync(quote, CurrentUser);

            return addedFavoriteQuote;
        }

        [HttpPut("Remove-From-Favorites/{quoteId}")]
        public async Task<RemovedFavoriteQuoteModel> RemoveFavorite(int quoteId)
        {
            await AssignCurrentUserAsync();

            var quote = await this.quoteService.GetActiveByIdAsync(quoteId);
            var removedFavoriteQuote = await this.quoteService.RemoveFavoriteAsync(quote, CurrentUser);

            return removedFavoriteQuote;
        }

        [HttpDelete("Delete/{quoteId}")]
        public async Task<DeletedQuoteModel> Delete(int quoteId)
        {
            await AssignCurrentUserAsync();
            var quote = await this.quoteService.GetActiveByIdAsync(quoteId);
            await this.quoteService.DeleteAsync(quote, CurrentUser.Id);
            return mapper.Map<DeletedQuoteModel>(quote);
        }
    }
}
