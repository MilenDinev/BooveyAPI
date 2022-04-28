namespace Boovey.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using AutoMapper;
    using Base;
    using Services.Interfaces;
    using Services.Exceptions;
    using Services.Constants;
    using Data.Entities;
    using Models.Requests.PublisherModels;
    using Models.Responses.PublisherModels;

    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : BooveyBaseController
    {
        private readonly IPublisherService publisherService;
        private readonly IMapper mapper;

        public PublishersController(IUserService userService, IPublisherService publisherService, IMapper mapper) : base(userService)
        {
            this.publisherService = publisherService;
            this.mapper = mapper;
        }

        [HttpGet("List/")]
        public async Task<ActionResult<IEnumerable<PublisherListingModel>>> Get()
        {
            var allPublishers = await this.publisherService.GetAllActiveAsync();
            return mapper.Map<ICollection<PublisherListingModel>>(allPublishers).ToList();
        }

        [HttpPost("Create/")]
        public async Task<ActionResult> Create(CreatePublisherModel publisherInput)
        {
            await AssignCurrentUserAsync();
            var alreadyExists = await this.publisherService.ContainsActiveByNameAsync(publisherInput.Name);
            if (alreadyExists)
            throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyContained, nameof(Publisher)));

            var addedPublisher = await this.publisherService.CreateAsync(publisherInput, CurrentUser.Id);
            return CreatedAtAction(nameof(Get), "Publishers", new { id = addedPublisher.Id }, addedPublisher);
        }

        [HttpPut("Edit/{publisherId}")]
        public async Task<ActionResult<EditedPublisherModel>> Edit(EditPublisherModel publisherInput, int publisherId)
        {
            await AssignCurrentUserAsync();
            var publisher = await this.publisherService.GetActiveByIdAsync(publisherId);
            await this.publisherService.EditAsync(publisher, publisherInput, CurrentUser.Id);

            return mapper.Map<EditedPublisherModel>(publisher);
        }

        [HttpDelete("Delete/{publisherId}")]
        public async Task<DeletedPublisherModel> Delete(int publisherId)
        {
            await AssignCurrentUserAsync();
            var publisher = await this.publisherService.GetActiveByIdAsync(publisherId);
            await this.publisherService.DeleteAsync(publisher, CurrentUser.Id);
            return mapper.Map<DeletedPublisherModel>(publisher);
        }
    }
}
