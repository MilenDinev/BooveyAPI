namespace Boovey.Services
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Interfaces;
    using Exceptions;
    using Constants;
    using Data;
    using Data.Entities;
    using Models.Requests.ReviewModels;
    using Models.Responses.ReviewModels;

    public class ReviewService : BaseService<Review>, IReviewService
    {
        private readonly IMapper mapper;

        public ReviewService(BooveyDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }

        public async Task<AddedReviewModel> AddAsync(AddReviewModel reviewModel, int currentUserId)
        {
            var review = await this.dbContext.Reviews.FirstOrDefaultAsync(r => r.BookId == reviewModel.BookId && r.CreatorId == currentUserId);
            if (review != null)
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyExists, nameof(Review), reviewModel.BookId));

            review = mapper.Map<Review>(reviewModel);

            await AddEntityAsync(review, currentUserId);

            return mapper.Map<AddedReviewModel>(review);
        }

        public async Task<EditedReviewModel> EditAsync(int reviewId, EditReviewModel reviewModel, int currentUserId)
        {
            var review = await GetByIdAsync(reviewId);

            review.Comment = reviewModel.Comment;
            review.Rating = reviewModel.Rating;
            review.BookId = reviewModel.BookId;
            
            await SaveModificationAsync(review, currentUserId);

            return mapper.Map<EditedReviewModel>(review);
        }

        public async Task<Review> GetByIdAsync(int reviewId)
        {
            var review = await FindByIdOrDefaultAsync(reviewId)
                ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Review), reviewId));

            return review;
        }
    }
}
