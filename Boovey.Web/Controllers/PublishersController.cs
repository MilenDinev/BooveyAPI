namespace Boovey.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Models.Requests.PublisherModels;
    using Models.Responses.PublisherModels;
    using Services.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : BooveyBaseController
    {
        IPublisherService publisherService;

        public PublishersController(IUserService userService, IPublisherService publisherService) : base(userService)
        {
            this.publisherService = publisherService;
        }

        [HttpGet("List/")]
        public async Task<ActionResult<IEnumerable<PublisherListingModel>>> Get()
        {
            var allPublishers = await this.publisherService.GetAllPublishersAsync();
            return allPublishers.ToList();
        }

        [HttpPost("Add/")]
        public async Task<ActionResult> Add(AddPublisherModel publisherInput)
        {
            await GetCurrentUserAsync();
            var addedPublisher = await this.publisherService.AddAsync(publisherInput, CurrentUser.Id);
            return CreatedAtAction(nameof(Get), "Publishers", new { id = addedPublisher.Id }, addedPublisher);
        }

        [HttpPut("Edit/{publisherId}")]
        public async Task<ActionResult<EditedPublisherModel>> Edit(EditPublisherModel publisherInput, int publisherId)
        {
            await GetCurrentUserAsync();
            var editedPublisher = await this.publisherService.EditAsync(publisherId, publisherInput, CurrentUser.Id);
            return editedPublisher;
        }
    }
}
