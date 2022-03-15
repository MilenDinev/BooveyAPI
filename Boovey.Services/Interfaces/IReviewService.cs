namespace Boovey.Services.Interfaces
{
    using System.Threading.Tasks;
    using Models.Requests.ReviewModels;
    using Models.Responses.ReviewModels;

    public interface IReviewService
    {
        Task<AddedReviewModel> AddAsync(AddReviewModel reviewModel, int currentUserId);
        Task<EditedReviewModel> EditAsync(int reviewId, EditReviewModel reviewModel, int currentUserId);
    }
}
