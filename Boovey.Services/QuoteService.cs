namespace Boovey.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Interfaces;
    using Exceptions;
    using Constants;
    using Data;
    using Data.Entities;
    using Models.Requests.QuoteModels;
    using Models.Responses.QuoteModels;

    public class QuoteService : IQuoteService
    {

        private readonly BooveyDbContext dbContext;
        private readonly IMapper mapper;
        public QuoteService(BooveyDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<AddedQuoteModel> AddAsync(AddQuoteModel quoteModel, int currentUserId)
        {
            var quote = await this.dbContext.Quotes.FirstOrDefaultAsync(q => q.Content == quoteModel.Content);
            if (quote != null)
                throw new ArgumentException(string.Format(ErrorMessages.EntityAlreadyExists, nameof(Quote), quoteModel.Content));

            quote = mapper.Map<Quote>(quoteModel);

            quote.CreatorId = currentUserId;
            quote.LastModifierId = currentUserId;

            await this.dbContext.Quotes.AddAsync(quote);
            await this.dbContext.SaveChangesAsync();

            return mapper.Map<AddedQuoteModel>(quote);
        }
     
        public async Task<EditedQuoteModel> EditAsync(int quoteId, EditQuoteModel quoteModel, int currentUserId)
        {
            var quote = await this.dbContext.Quotes.FirstOrDefaultAsync(q => q.Id == quoteId)
            ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Quote), quoteId));

            quote.Content = quoteModel.Content;
            quote.LastModifierId = currentUserId;
            quote.LastModifiedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();

            return mapper.Map<EditedQuoteModel>(quote);
        }

        public async Task<AddedFavoriteQuoteModel> AddFavoriteQuoteAsync(int quoteId, User currentUser)
        {
            var quote = await this.dbContext.Quotes.FirstOrDefaultAsync(q => q.Id == quoteId)
                ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Quote), quoteId));

            var isAlreadyFavoriteQuote = currentUser.FavoriteQuotes.FirstOrDefault(a => a.Id == quoteId);

            if (isAlreadyFavoriteQuote != null)
                throw new ArgumentException(string.Format(ErrorMessages.AlreadyFavoriteId, nameof(Quote), quote.Id));

            currentUser.FavoriteQuotes.Add(quote);

            await dbContext.SaveChangesAsync();
            return mapper.Map<AddedFavoriteQuoteModel>(quote);
        }

        public async Task<RemovedFavoriteQuoteModel> RemoveFavoriteQuoteAsync(int quoteId, User currentUser)
        {
            var quote = await this.dbContext.Quotes.FirstOrDefaultAsync(q => q.Id == quoteId)
                ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Quote), quoteId));

            var isFavoriteQuote = currentUser.FavoriteQuotes.FirstOrDefault(q => q.Id == quoteId);

            if (isFavoriteQuote == null)
                throw new ResourceNotFoundException(string.Format(ErrorMessages.NotFavoriteId, nameof(Quote), quote.Id));

            currentUser.FavoriteQuotes.Remove(quote);

            await dbContext.SaveChangesAsync();
            return mapper.Map<RemovedFavoriteQuoteModel>(quote);
        }
    }
}
