namespace Boovey.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class ShelvesController : BooveyBaseController
    {
        public ShelvesController(IUserService userService) : base(userService)
        {
        }
    }
}
