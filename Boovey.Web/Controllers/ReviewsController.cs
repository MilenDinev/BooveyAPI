namespace Boovey.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using AutoMapper;
    using Base;
    using Services.Handlers.Interfaces;
    using Services.MainServices.Interfaces;
    using Data.Entities;
    using Models.Requests.ReviewModels;
    using Models.Responses.ReviewModels;

    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : BooveyBaseController
    {
        private readonly IReviewService reviewService;
        private readonly IFinder finder;
        private readonly IValidator validator;
        private readonly IMapper mapper;
        public ReviewsController(IReviewService reviewService,
            IFinder finder,
            IValidator validator,
            IMapper mapper,
            IUserService userService) 
            : base(userService)
        {
            this.reviewService = reviewService;
            this.finder = finder;
            this.validator = validator;
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
            var review = await this.finder.FindByIdOrDefaultAsync<Review>(reviewId);
            await this.validator.ValidateEntityAsync(review, reviewId.ToString());
            await this.reviewService.EditAsync(review, reviewInput, CurrentUser.Id);
            return mapper.Map<EditedReviewModel>(review);
        }

        [HttpDelete("Delete/Review/{reviewId}")]
        public async Task<DeletedReviewModel> Delete(int reviewId)
        {
            await AssignCurrentUserAsync();
            var review = await this.finder.FindByIdOrDefaultAsync<Review>(reviewId);
            await this.validator.ValidateEntityAsync(review, reviewId.ToString());
            await this.reviewService.DeleteAsync(review, CurrentUser.Id);
            return mapper.Map<DeletedReviewModel>(review);
        }
    }
}
