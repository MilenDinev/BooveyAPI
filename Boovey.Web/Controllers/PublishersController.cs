namespace Boovey.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using AutoMapper;
    using Base;
    using Services.Interfaces.IEntities;
    using Services.Interfaces.IHandlers;
    using Data.Entities;
    using Models.Requests.PublisherModels;
    using Models.Responses.PublisherModels;

    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : BooveyBaseController
    {
        private readonly IPublisherService publisherService;
        private readonly ISearchService searchService;
        private readonly IValidator validator;
        private readonly IMapper mapper;
        public PublishersController(IPublisherService publisherService, 
            ISearchService searchService,
            IValidator validator,
            IMapper mapper, 
            IUserService userService) 
            : base(userService)
        {
            this.publisherService = publisherService;
            this.searchService = searchService;
            this.validator = validator;
            this.mapper = mapper;
        }

        [HttpGet("List/")]
        public async Task<ActionResult<IEnumerable<PublisherListingModel>>> Get()
        {
            var allPublishers = await this.searchService.GetAllActiveAsync<Publisher>();
            return mapper.Map<ICollection<PublisherListingModel>>(allPublishers).ToList();
        }

        [HttpPost("Create/")]
        public async Task<ActionResult> Create(CreatePublisherModel publisherInput)
        {
            await AssignCurrentUserAsync();
            var publisher = await this.searchService.FindByStringOrDefaultAsync<Publisher>(publisherInput.Name);
            await this.validator.ValidateUniqueEntityAsync(publisher);

            var addedPublisher = await this.publisherService.CreateAsync(publisherInput, CurrentUser.Id);

            //var createdPublisher = mapper.Map<CreatedPublisherModel>(publisher);

            return CreatedAtAction(nameof(Get), "Publishers", new { id = addedPublisher.Id }, addedPublisher);
        }

        [HttpPut("Edit/Publisher/{publisherId}")]
        public async Task<ActionResult<EditedPublisherModel>> Edit(EditPublisherModel publisherInput, int publisherId)
        {
            await AssignCurrentUserAsync();
            var publisher = await this.searchService.FindByIdOrDefaultAsync<Publisher>(publisherId);
            await this.validator.ValidateEntityAsync(publisher, publisherId.ToString());
            await this.publisherService.EditAsync(publisher, publisherInput, CurrentUser.Id);

            return mapper.Map<EditedPublisherModel>(publisher);
        }

        [HttpDelete("Delete/Publisher/{publisherId}")]
        public async Task<DeletedPublisherModel> Delete(int publisherId)
        {
            await AssignCurrentUserAsync();
            var publisher = await this.searchService.FindByIdOrDefaultAsync<Publisher>(publisherId);
            await this.validator.ValidateEntityAsync(publisher, publisherId.ToString());
            await this.publisherService.DeleteAsync(publisher, CurrentUser.Id);
            return mapper.Map<DeletedPublisherModel>(publisher);
        }
    }
}
