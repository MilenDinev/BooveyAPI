namespace Boovey.Services.Interfaces.IEntities
{
    using System.Threading.Tasks;
    using Data.Entities;
    using Models.Requests.ReviewModels;

    public interface IReviewService
    {
        Task<Review> CreateAsync(CreateReviewModel model, int creatorId);
        Task EditAsync(Review review, EditReviewModel model, int modifierId);
        Task DeleteAsync(Review review, int modifierId);
        Task SaveModificationAsync(Review review, int modifierId);
    }
}
