namespace Boovey.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Data.Entities;
    using Services.Interfaces;
    using Boovey.Web.Constants;

    public class BooveyBaseController : ControllerBase
    {
        protected readonly IUserService _userService;

        public BooveyBaseController(IUserService userService)
        {
            _userService = userService;
        }

        public User CurrentUser { get; set; }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult> GetCurrentUserAsync()
        {
            CurrentUser = await _userService.GetCurrentUserAsync(User) ?? throw new UnauthorizedAccessException(ErrorMessages.InvalidCredentials);
            return Ok();
        }
    }
}
