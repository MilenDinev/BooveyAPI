namespace Boovey.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Services.Interfaces;
    using Models.Responses.UserModels;

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BooveyBaseController
    {
        public UsersController(IUserService userService) : base(userService)
        {
        }

        [HttpGet("List/")]
        public async Task<ActionResult<IEnumerable<UserResponseModel>>> Get()
        {
            var allUsers = await _userService.GetAllUsersAsync();
            return allUsers.ToList();
        }
    }
}
