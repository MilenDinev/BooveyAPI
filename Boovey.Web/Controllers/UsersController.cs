namespace Boovey.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Base;
    using AutoMapper;
    using Services.Exceptions;
    using Services.Constants;
    using Services.Handlers.Interfaces;
    using Services.Managers.Interfaces;
    using Services.MainServices.Interfaces;
    using Data.Entities;
    using Models.Responses.UserModels;
    using Models.Requests.UserModels;

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BooveyBaseController
    {
        private readonly IUserManager userManager;
        private readonly IFinder finder;
        private readonly IValidator validator;
        private readonly IMapper mapper;

        public UsersController(IUserManager userManager,
            IFinder finder,
            IValidator validator,
            IMapper mapper,
            IUserService userService)
            : base(userService)
        {
            this.userManager = userManager;
            this.finder = finder;
            this.validator = validator;
            this.mapper = mapper;
        }

        [HttpGet("List/")]
        public async Task<ActionResult<IEnumerable<UserListingModel>>> Get()
        {
            var allUsers = await this.finder.GetAllActiveAsync<User>();
            return mapper.Map<ICollection<UserListingModel>>(allUsers).ToList();
        }


        [HttpGet("Get/User/{userId}")]
        public async Task<ActionResult<UserListingModel>> GetById(int userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            await this.validator.ValidateEntityAsync(user, userId.ToString());
            return mapper.Map<UserListingModel>(user);
        }


        [HttpPost("Register/")]
        public async Task<ActionResult> Create(CreateUserModel userInput)
        {
            var user = await userManager.FindByNameAsync(userInput.UserName);
            await this.validator.ValidateUniqueEntityAsync(user);

            user = await userManager.FindByEmailAsync(userInput.Email);
            if (user != null)
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.EntityAlreadyExists, nameof(userInput.Email), userInput.Email));


            user = await this.userService.CreateAsync(userInput);

            var userResponse = mapper.Map<CreatedUserModel>(user);
            return CreatedAtAction(nameof(Get), "Users", new { id = userResponse.Id }, userResponse);

        }

        [HttpPut("Edit/User/{userId}")]
        public async Task<ActionResult<EditedUserModel>> Edit(EditUserModel userInput, int userId)
        {
            await AssignCurrentUserAsync();

            var user = await this.finder.FindByIdOrDefaultAsync<User>(userId);
            await this.validator.ValidateEntityAsync(user, userId.ToString());

            await this.userService.EditAsync(user, userInput, CurrentUser.Id);

            return mapper.Map<EditedUserModel>(user);
        }


        [HttpPut("Follow/User/{followedId}")]
        public async Task<ActionResult<FollowerModel>> Follow(int followedId)
        {
            await AssignCurrentUserAsync();
            var followerFollowed = await this.userService.Follow(CurrentUser, followedId);
            return followerFollowed;
        }

        [HttpDelete("Delete/User/{userId}")]
        public async Task<DeletedUserModel> Delete(int userId)
        {
            await AssignCurrentUserAsync();

            var user = await this.finder.FindByIdOrDefaultAsync<User>(userId);
            await this.validator.ValidateEntityAsync(user, userId.ToString());

            await this.userService.DeleteAsync(user, CurrentUser.Id);
            return mapper.Map<DeletedUserModel>(user);
        }

    }
}
