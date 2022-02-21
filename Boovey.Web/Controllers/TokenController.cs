namespace Boovey.Web.Controllers
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models.Requests;
    using Models.Responses;
    using Services.Interfaces;
    using Extensions.Authentication;

    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : BooveyBaseController
    {
        public TokenController(IUserService userService) : base(userService)
        {
        }

        [HttpPost]
        public async Task<ActionResult<TokenResponseModel>> Login(GetTokenRequestModel user)
        {
            using var httpClientHandler = new HttpClientHandler();
            using var client = new HttpClient(httpClientHandler);
            var token = await this.userService.GetEligibilityTokenAsync(client, user.UsernameOrEmail, user.Password);
            return token;
        }
    }
}
