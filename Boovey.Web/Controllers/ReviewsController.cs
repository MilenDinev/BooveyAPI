namespace Boovey.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using Models.Requests.ReviewModels;
    using Models.Responses.ReviewModels;

    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : BooveyBaseController
    {
        private readonly IReviewService reviewService;

        public ReviewsController(IUserService userService, IReviewService reviewService) : base(userService)
        {
            this.reviewService = reviewService;
        }

        [HttpPost("Add/")]
        public async Task<ActionResult> Add(AddReviewModel reviewInput)
        {
            await AssignCurrentUserAsync();
            var addedReview = await this.reviewService.AddAsync(reviewInput, CurrentUser.Id);
            return CreatedAtAction(nameof(Add), "Reviews", new { id = addedReview.Id }, addedReview);
        }

        [HttpPut("Edit/{reviewId}")]
        public async Task<ActionResult<EditedReviewModel>> Edit(EditReviewModel reviewInput, int reviewId)
        {
            await AssignCurrentUserAsync();
            var editedReview = await this.reviewService.EditAsync(reviewId, reviewInput, CurrentUser.Id);
            return editedReview;
        }
    }
}
