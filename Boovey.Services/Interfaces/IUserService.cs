namespace Boovey.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Boovey.Models.Responses.UserModels;
    using Data.Entities;

    public interface IUserService
    {
        Task<User> GetCurrentUserAsync(ClaimsPrincipal principal);
        Task<ICollection<UserResponseModel>> GetAllUsersAsync();
    }
}
