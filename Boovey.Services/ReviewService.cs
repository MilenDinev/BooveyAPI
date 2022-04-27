namespace Boovey.Services
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Interfaces;
    using Exceptions;
    using Constants;
    using Data;
    using Data.Entities;
    using Models.Requests.ReviewModels;

    public class ReviewService : BaseService<Review>, IReviewService
    {
        private readonly IMapper mapper;

        public ReviewService(BooveyDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }

        public async Task<Review> CreateAsync(CreateReviewModel reviewModel, int currentUserId)
        {
            var review = mapper.Map<Review>(reviewModel);
            await CreateEntityAsync(review, currentUserId);

            return review;
        }

        public async Task EditAsync(Review review, EditReviewModel reviewModel, int currentUserId)
        {
            review.Comment = reviewModel.Comment;
            review.Rating = reviewModel.Rating;
            review.BookId = reviewModel.BookId;
            
            await SaveModificationAsync(review, currentUserId);
        }

        public async Task DeleteAsync(Review review, int modifierId)
        {
            await DeleteEntityAsync(review, modifierId);
        }

        public async Task<Review> GetByIdAsync(int reviewId)
        {
            var review = await FindByIdOrDefaultAsync(reviewId)
                ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Review), reviewId));

            return review;
        }

        public async Task<Review> GetActiveByIdAsync(int reviewId)
        {
            var review = await GetByIdAsync(reviewId);
            if (review.Deleted)
                throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityHasBeenDeleted, nameof(Review)));

            return review;
        }
    }
}
