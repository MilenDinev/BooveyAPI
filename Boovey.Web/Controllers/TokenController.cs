﻿namespace Boovey.Web.Controllers
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Extensions.Authentication;
    using Base;
    using Services.MainServices.Interfaces;
    using Models.Requests;
    using Models.Responses;

    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : BooveyBaseController
    {
        public TokenController(IUserService userService) : base(userService)
        {
        }

        [HttpPost]
        public async Task<ActionResult<TokenModel>> Login(TokenUserInputModel user)
        {
            using var httpClientHandler = new HttpClientHandler();
            using var client = new HttpClient(httpClientHandler);
            var token = await this.userService.GetEligibilityTokenAsync(client, user.UsernameOrEmail, user.Password);
            return token;
        }
    }
}
