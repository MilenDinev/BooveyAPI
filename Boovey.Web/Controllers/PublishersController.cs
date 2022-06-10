namespace Boovey.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using AutoMapper;
    using Base;
    using Services.Handlers.Interfaces;
    using Services.MainServices.Interfaces;
    using Data.Entities;
    using Models.Responses.SharedModels;
    using Models.Requests.PublisherModels;
    using Models.Responses.PublisherModels;

    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : BooveyBaseController
    {
        private readonly IPublisherService publisherService;
        private readonly IAssigner assigner;
        private readonly IFinder finder;
        private readonly IValidator validator;
        private readonly IMapper mapper;
        public PublishersController(IPublisherService publisherService,
            IAssigner assigner,
            IFinder finder,
            IValidator validator,
            IMapper mapper, 
            IUserService userService) 
            : base(userService)
        {
            this.publisherService = publisherService;
            this.assigner = assigner;
            this.finder = finder;
            this.validator = validator;
            this.mapper = mapper;
        }

        [HttpGet("List/")]
        public async Task<ActionResult<IEnumerable<PublisherListingModel>>> Get()
        {
            var allPublishers = await this.finder.GetAllActiveAsync<Publisher>();
            return mapper.Map<ICollection<PublisherListingModel>>(allPublishers).ToList();
        }

        [HttpPost("Create/")]
        public async Task<ActionResult> Create(CreatePublisherModel publisherInput)
        {
            await AssignCurrentUserAsync();
            var publisher = await this.finder.FindByStringOrDefaultAsync<Publisher>(publisherInput.Name);
            await this.validator.ValidateUniqueEntityAsync(publisher);

            var addedPublisher = await this.publisherService.CreateAsync(publisherInput, CurrentUser.Id);

            //var createdPublisher = mapper.Map<CreatedPublisherModel>(publisher);

            return CreatedAtAction(nameof(Get), "Publishers", new { id = addedPublisher.Id }, addedPublisher);
        }

        [HttpPut("Edit/Publisher/{publisherId}")]
        public async Task<ActionResult<EditedPublisherModel>> Edit(EditPublisherModel publisherInput, int publisherId)
        {
            await AssignCurrentUserAsync();

            var publisher = await this.finder.FindByIdOrDefaultAsync<Publisher>(publisherId);
            await this.validator.ValidateEntityAsync(publisher, publisherId.ToString());
            await this.publisherService.EditAsync(publisher, publisherInput, CurrentUser.Id);

            return mapper.Map<EditedPublisherModel>(publisher);
        }

        [HttpDelete("Delete/Publisher/{publisherId}")]
        public async Task<DeletedPublisherModel> Delete(int publisherId)
        {
            await AssignCurrentUserAsync();

            var publisher = await this.finder.FindByIdOrDefaultAsync<Publisher>(publisherId);
            await this.validator.ValidateEntityAsync(publisher, publisherId.ToString());
            await this.publisherService.DeleteAsync(publisher, CurrentUser.Id);

            return mapper.Map<DeletedPublisherModel>(publisher);
        }

        [HttpPut("Assign/Publisher/{publisherId}/Book/{bookId}")]
        public async Task<AssignedBookPublisherModel> AssignBook(int publisherId, int bookId)
        {
            await AssignCurrentUserAsync();

            var publisher = await this.finder.FindByIdOrDefaultAsync<Publisher>(publisherId);
            await this.validator.ValidateEntityAsync(publisher, publisherId.ToString());

            var book = await this.finder.FindByIdOrDefaultAsync<Book>(bookId);
            await this.validator.ValidateEntityAsync(book, bookId.ToString());

            await this.validator.ValidateAssigningBook(publisher, book);
            await this.assigner.AssignBookAsync(publisher, book);
            await this.publisherService.SaveModificationAsync(publisher, CurrentUser.Id);

            return mapper.Map<AssignedBookPublisherModel>(book);
        }
    }
}
