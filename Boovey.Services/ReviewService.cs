namespace Boovey.Services
{
    using System;
    using System.Collections.Generic;
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

    public class ReviewService : IReviewService
    {
        private readonly BooveyDbContext dbContext;
        private readonly IMapper mapper;

        public ReviewService(BooveyDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<AddedReviewModel> AddAsync(AddReviewModel reviewModel, int currentUserId)
        {
            var review = await this.dbContext.Reviews.FirstOrDefaultAsync(r => r.BookId == reviewModel.BookId && r.CreatorId == currentUserId);
            if (review != null)
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyExists, nameof(Review), reviewModel.BookId));

            review = mapper.Map<Review>(reviewModel);

            review.CreatorId = currentUserId;
            review.LastModifierId = currentUserId;

            await this.dbContext.Reviews.AddAsync(review);
            await this.dbContext.SaveChangesAsync();

            return mapper.Map<AddedReviewModel>(review);
        }

        public async Task<EditedReviewModel> EditAsync(int reviewId, EditReviewModel reviewModel, int currentUserId)
        {
            var review = await GetReviewById(reviewId);

            review.Comment = reviewModel.Comment;
            review.Rating = reviewModel.Rating;
            review.BookId = reviewModel.BookId;
            review.LastModifierId = currentUserId;
            review.LastModifiedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();

            return mapper.Map<EditedReviewModel>(review);
        }

        private async Task<Review> GetReviewById(int reviewId)
        {
            var review = await this.dbContext.Reviews.FirstOrDefaultAsync(g => g.Id == reviewId)
                ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Review), reviewId));

            return review;
        }
    }
}
