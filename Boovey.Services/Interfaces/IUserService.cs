namespace Boovey.Services.Interfaces
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Data.Entities;

    public interface IUserService
    {
        Task<User> GetCurrentUserAsync(ClaimsPrincipal principal);
    }
}
