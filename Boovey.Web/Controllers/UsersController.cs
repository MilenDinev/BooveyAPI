namespace Boovey.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Services.Interfaces;
    using Models.Requests;
    using Models.Responses.UserModels;

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BooveyBaseController
    {
        public UsersController(IUserService userService) : base(userService)
        {
        }

        [HttpGet("List/")]
        public async Task<ActionResult<IEnumerable<UsersListingModel>>> Get()
        {
            var allUsers = await this.userService.GetAllUsersAsync();
            return allUsers.ToList();
        }

        [HttpPost("Register/")]
        public async Task<ActionResult> Register(RegistrationModel userInput)
        {
            var registeredUser = await this.userService.CreateAsync(userInput);
            return CreatedAtAction(nameof(Get), "Users", new { username = registeredUser.Username }, registeredUser);
        }

        [HttpPut("Follow/{followedId}")]
        public async Task<ActionResult<FollowerModel>> Follow(int followedId)
        {
            await AssignCurrentUserAsync();
            var followerFollowed = await this.userService.Follow(CurrentUser, followedId);
            return followerFollowed;
        }

    }
}
