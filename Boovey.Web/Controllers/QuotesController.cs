﻿namespace Boovey.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using AutoMapper;
    using Base;
    using Services.Handlers.Interfaces;
    using Services.Managers.Interfaces;
    using Services.MainServices.Interfaces;
    using Data.Entities;
    using Models.Requests.QuoteModels;
    using Models.Responses.QuoteModels;

    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : BooveyBaseController
    {
        private readonly IQuoteService quoteService;
        private readonly IFavoritesManager favoritesManager;
        private readonly IFinder finder;
        private readonly IValidator validator;
        private readonly IMapper mapper;
        public QuotesController(IQuoteService quoteService,
            IFavoritesManager favoritesManager,
            IFinder finder,
            IValidator validator,
            IMapper mapper, 
            IUserService userService) 
            : base(userService)
        {
            this.quoteService = quoteService;
            this.favoritesManager = favoritesManager;
            this.finder = finder;
            this.validator = validator;
            this.mapper = mapper;
        }

        [HttpPost("Create/")]
        public async Task<ActionResult> Add(CreateQuoteModel quoteInput)
        {
            await AssignCurrentUserAsync();
            var quote = await this.finder.FindByStringOrDefaultAsync<Quote>(quoteInput.Content);
            await this.validator.ValidateUniqueEntityAsync(quote);

            var createdQuote = await this.quoteService.CreateAsync(quoteInput, CurrentUser.Id);
            return CreatedAtAction(nameof(Add), "Quotes", new { id = createdQuote.Id }, createdQuote);
        }

        [HttpPut("Edit/Quote/{quoteId}")]
        public async Task<ActionResult<EditedQuoteModel>> Edit(EditQuoteModel quoteInput, int quoteId)
        {
            await AssignCurrentUserAsync();

            var quote = await this.finder.FindByIdOrDefaultAsync<Quote>(quoteId);
            await this.validator.ValidateEntityAsync(quote, quoteId.ToString());

            await this.quoteService.EditAsync(quote, quoteInput, CurrentUser.Id);

            return mapper.Map<EditedQuoteModel>(quote); ;
        }

        [HttpPut("Favorites/Add/Quote/{quoteId}")]
        public async Task<AddedFavoriteQuoteModel> AddFavorite(int quoteId)
        {
            await AssignCurrentUserAsync();

            var quote = await this.finder.FindByIdOrDefaultAsync<Quote>(quoteId);
            await this.validator.ValidateEntityAsync(quote, quoteId.ToString());
            await this.validator.ValidateAddingFavorite(quoteId, this.CurrentUser.FavoriteQuotes);

            await this.favoritesManager.AddFavoriteQuote(quote, this.CurrentUser);

            await this.quoteService.SaveModificationAsync(quote, CurrentUser.Id);

            return mapper.Map<AddedFavoriteQuoteModel>(quote);
        }

        [HttpPut("Favorites/Remove/Quote/{quoteId}")]
        public async Task<RemovedFavoriteQuoteModel> RemoveFavorite(int quoteId)
        {
            await AssignCurrentUserAsync();

            var quote = await this.finder.FindByIdOrDefaultAsync<Quote>(quoteId);
            await this.validator.ValidateEntityAsync(quote, quoteId.ToString());
            await this.validator.ValidateRemovingFavorite(quoteId, this.CurrentUser.FavoriteQuotes);

            await this.favoritesManager.RemoveFavoriteQuote(quote, this.CurrentUser);
            await this.quoteService.SaveModificationAsync(quote, CurrentUser.Id);

            var removedFavoriteQuote = mapper.Map<RemovedFavoriteQuoteModel>(quote);
            removedFavoriteQuote.UserId = CurrentUser.Id;
            return removedFavoriteQuote;
        }

        [HttpDelete("Delete/Quote/{quoteId}")]
        public async Task<DeletedQuoteModel> Delete(int quoteId)
        {
            await AssignCurrentUserAsync();

            var quote = await this.finder.FindByIdOrDefaultAsync<Quote>(quoteId);
            await this.validator.ValidateEntityAsync(quote, quoteId.ToString());

            await this.quoteService.DeleteAsync(quote, CurrentUser.Id);
            return mapper.Map<DeletedQuoteModel>(quote);
        }
    }
}
