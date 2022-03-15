﻿namespace Boovey.Services.Interfaces
{
    using System.Threading.Tasks;
    using Data.Entities;
    using Models.Requests.QuoteModels;
    using Models.Responses.QuoteModels;

    public interface IQuoteService
    {
        Task<AddedQuoteModel> AddAsync(AddQuoteModel quoteModel, int currentUserId);
        Task<EditedQuoteModel> EditAsync(int quoteId, EditQuoteModel quoteModel, int currentUserId);
        Task<AddedFavoriteQuoteModel> AddFavoriteQuoteAsync(int quoteId, User currentUser);
        Task<RemovedFavoriteQuoteModel> RemoveFavoriteQuoteAsync(int quoteId, User currentUser);
    }
}
