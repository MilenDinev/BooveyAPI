namespace Boovey.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Services.Interfaces;
    using Models.Responses.UserModels;
    using Boovey.Models.Requests;

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BooveyBaseController
    {
        public UsersController(IUserService userService) : base(userService)
        {
        }

        [HttpGet("List/")]
        public async Task<ActionResult<IEnumerable<UsersListingResponseModel>>> Get()
        {
            var allUsers = await this.userService.GetAllUsersAsync();
            return allUsers.ToList();
        }

        [HttpPost("Register/")]
        public async Task<ActionResult> Post(RegistrationModel userInput)
        {
            var registeredUser = await this.userService.CreateAsync(userInput);
            return CreatedAtAction(nameof(Get), "Users", new { username = registeredUser.Username }, registeredUser);
        }
    }
}
