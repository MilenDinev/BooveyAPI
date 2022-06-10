namespace Boovey.Services.MainServices
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Base;
    using Constants;
    using Exceptions;
    using Interfaces;
    using Data;
    using Data.Entities;
    using Models.Requests.QuoteModels;

    public class QuoteService : BaseService<Quote>, IQuoteService
    {

        private readonly IMapper mapper;

        public QuoteService(BooveyDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }

        public async Task<Quote> CreateAsync(CreateQuoteModel model, int creatorId)
        {
            var quote = await dbContext.Quotes.FirstOrDefaultAsync(q => q.Content == model.Content);
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
    }
}
