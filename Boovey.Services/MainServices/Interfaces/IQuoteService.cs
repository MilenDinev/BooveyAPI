namespace Boovey.Services.MainServices.Interfaces
{
    using System.Threading.Tasks;
    using Data.Entities;
    using Models.Requests.QuoteModels;
    using Models.Responses.QuoteModels;

    public interface IQuoteService
    {
        Task<Quote> CreateAsync(CreateQuoteModel model, int creatorId);
        Task EditAsync(Quote quote, EditQuoteModel model, int modifierId);
        Task DeleteAsync(Quote quote, int modifierId);
        Task SaveModificationAsync(Quote quote, int modifierId);
    }
}
