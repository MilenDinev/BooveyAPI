namespace Boovey.Services
{
    using Boovey.Data.Entities;
    using Boovey.Services.Interfaces;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        public Task<User> GetCurrentUserAsync(ClaimsPrincipal principal)
        {
            throw new System.NotImplementedException();
        }
    }
}
