namespace Boovey.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Base;
    using Constants;
    using Exceptions;
    using Interfaces.IEntities;
    using Data;
    using Data.Entities;
    using Models.Requests.QuoteModels;
    using Models.Responses.QuoteModels;

    public class QuoteService : BaseService<Quote>, IQuoteService
    {

        private readonly IMapper mapper;

        public QuoteService(BooveyDbContext dbContext, IMapper mapper) : base (dbContext)
        {
            this.mapper = mapper;
        }

        public async Task<Quote> CreateAsync(CreateQuoteModel model, int creatorId)
        {
            var quote = await this.dbContext.Quotes.FirstOrDefaultAsync(q => q.Content == model.Content);
            if (quote != null)
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyExists, nameof(Quote), model.Content));

            quote = mapper.Map<Quote>(model);

            await CreateEntityAsync(quote, creatorId);

            return quote;
        }
     
        public async Task EditAsync(Quote quote, EditQuoteModel model, int currentUserId)
        {
            quote.Content = model.Content;
            await SaveModificationAsync(quote, currentUserId);
        }

        public async Task DeleteAsync(Quote quote, int modifierId)
        {
            await DeleteEntityAsync(quote, modifierId);
        }

        public async Task<AddedFavoriteQuoteModel> AddFavoriteAsync(Quote quote, User currentUser)
        {
            var isAlreadyFavoriteQuote = currentUser.FavoriteQuotes.FirstOrDefault(a => a.Id == quote.Id);

            if (isAlreadyFavoriteQuote != null)
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.AlreadyFavoriteId, nameof(Quote), quote.Id));

            currentUser.FavoriteQuotes.Add(quote);

            await SaveModificationAsync(quote, currentUser.Id);

            return mapper.Map<AddedFavoriteQuoteModel>(quote);
        }

        public async Task<RemovedFavoriteQuoteModel> RemoveFavoriteAsync(Quote quote, User currentUser)
        {

            var isFavoriteQuote = currentUser.FavoriteQuotes.FirstOrDefault(q => q.Id == quote.Id);

            if (isFavoriteQuote == null)
                throw new ResourceNotFoundException(string.Format(ErrorMessages.NotFavoriteId, nameof(Quote), quote.Id));

            currentUser.FavoriteQuotes.Remove(quote);

            await SaveModificationAsync(quote, currentUser.Id);
            return mapper.Map<RemovedFavoriteQuoteModel>(quote);
        }
    }
}
