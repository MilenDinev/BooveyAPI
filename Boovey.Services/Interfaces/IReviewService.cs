namespace Boovey.Services.Interfaces
{
    using System.Threading.Tasks;
    using Data.Entities;
    using Models.Requests.ReviewModels;

    public interface IReviewService
    {
        Task<Review> CreateAsync(CreateReviewModel model, int creatorId);
        Task EditAsync(Review review, EditReviewModel model, int modifierId);
        Task DeleteAsync(Review review, int modifierId);
        Task<Review> GetByIdAsync(int reviewId);
        Task<Review> GetActiveByIdAsync(int reviewId);
    }
}
