namespace Boovey.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Constants;
    using Data.Entities;
    using Services.Interfaces;

    public class BooveyBaseController : ControllerBase
    {
        protected readonly IUserService userService;

        public BooveyBaseController(IUserService userService)
        {
            this.userService = userService;
        }

        public User CurrentUser { get; set; }

        [ApiExplorerSettings(IgnoreApi = true)]
        protected async Task<ActionResult> AssignCurrentUserAsync()
        {
            CurrentUser = await this.userService.GetCurrentUserAsync(User) ?? 
                throw new UnauthorizedAccessException(ErrorMessages.InvalidCredentials);

            return Ok();
        }
    }
}
