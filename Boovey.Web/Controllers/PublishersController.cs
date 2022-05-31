namespace Boovey.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using AutoMapper;
    using Base;
    using Services.Constants;
    using Services.Exceptions;
    using Services.Interfaces;
    using Services.Interfaces.IHandlers;
    using Data.Entities;
    using Models.Requests.PublisherModels;
    using Models.Responses.PublisherModels;

    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : BooveyBaseController
    {
        private readonly IPublisherService publisherService;
        private readonly ISearchService<Publisher> searchService;
        private readonly IMapper mapper;
        public PublishersController(IPublisherService publisherService, ISearchService<Publisher> searchService, IMapper mapper, IUserService userService) : base(userService)
        {
            this.publisherService = publisherService;
            this.searchService = searchService;
            this.mapper = mapper;
        }

        [HttpGet("List/")]
        public async Task<ActionResult<IEnumerable<PublisherListingModel>>> Get()
        {
            var allPublishers = await this.searchService.GetAllActiveAsync();
            return mapper.Map<ICollection<PublisherListingModel>>(allPublishers).ToList();
        }

        [HttpPost("Create/")]
        public async Task<ActionResult> Create(CreatePublisherModel publisherInput)
        {
            await AssignCurrentUserAsync();
            var alreadyExists = await this.searchService.ContainsActiveByStringAsync(publisherInput.Name);
            if (alreadyExists)
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyContained, nameof(Publisher)));

            var addedPublisher = await this.publisherService.CreateAsync(publisherInput, CurrentUser.Id);
            return CreatedAtAction(nameof(Get), "Publishers", new { id = addedPublisher.Id }, addedPublisher);
        }

        [HttpPut("Edit/Publisher/{publisherId}")]
        public async Task<ActionResult<EditedPublisherModel>> Edit(EditPublisherModel publisherInput, int publisherId)
        {
            await AssignCurrentUserAsync();
            var publisher = await this.searchService.GetActivePublisherByIdAsync(publisherId);
            await this.publisherService.EditAsync(publisher, publisherInput, CurrentUser.Id);

            return mapper.Map<EditedPublisherModel>(publisher);
        }

        [HttpDelete("Delete/Publisher/{publisherId}")]
        public async Task<DeletedPublisherModel> Delete(int publisherId)
        {
            await AssignCurrentUserAsync();
            var publisher = await this.searchService.GetActivePublisherByIdAsync(publisherId);
            await this.publisherService.DeleteAsync(publisher, CurrentUser.Id);
            return mapper.Map<DeletedPublisherModel>(publisher);
        }
    }
}
