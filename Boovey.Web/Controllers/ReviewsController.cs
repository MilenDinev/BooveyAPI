namespace Boovey.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using AutoMapper;
    using Base;
    using Services.Interfaces;
    using Services.Interfaces.IHandlers;
    using Data.Entities;
    using Models.Requests.ReviewModels;
    using Models.Responses.ReviewModels;

    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : BooveyBaseController
    {
        private readonly IReviewService reviewService;
        private readonly IContextAccessorServices<Review> reviewsAccessorService;
        private readonly IMapper mapper;
        public ReviewsController(IReviewService reviewService, IContextAccessorServices<Review> reviewsAccessorService, IMapper mapper, IUserService userService) : base(userService)
        {
            this.reviewService = reviewService;
            this.reviewsAccessorService = reviewsAccessorService;
            this.mapper = mapper;
        }

        [HttpPost("Create/")]
        public async Task<ActionResult> Add(CreateReviewModel reviewInput)
        {
            await AssignCurrentUserAsync();
            var addedReview = await this.reviewService.CreateAsync(reviewInput, CurrentUser.Id);
            return CreatedAtAction(nameof(Add), "Reviews", new { id = addedReview.Id }, addedReview);
        }

        [HttpPut("Edit/Review/{reviewId}")]
        public async Task<ActionResult<EditedReviewModel>> Edit(EditReviewModel reviewInput, int reviewId)
        {
            await AssignCurrentUserAsync();
            var review = await this.reviewsAccessorService.GetActiveByIdAsync(reviewId);
            await this.reviewService.EditAsync(review, reviewInput, CurrentUser.Id);
            return mapper.Map<EditedReviewModel>(review);
        }

        [HttpDelete("Delete/Review/{reviewId}")]
        public async Task<DeletedReviewModel> Delete(int reviewId)
        {
            await AssignCurrentUserAsync();
            var review = await this.reviewsAccessorService.GetActiveByIdAsync(reviewId);
            await this.reviewService.DeleteAsync(review, CurrentUser.Id);
            return mapper.Map<DeletedReviewModel>(review);
        }
    }
}
