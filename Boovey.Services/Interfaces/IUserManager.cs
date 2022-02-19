namespace Boovey.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Data.Entities;

    public interface IUserManager
    {
        Task<User> GetUserAsync(ClaimsPrincipal claimsPrincipal);
        Task<User> FindByNameAsync(string name);
        Task<User> FindByEmailAsync(string name);
        Task<User> FindByIdAsync(string id);
        Task<ICollection<User>> GetAllAsync();
        Task<bool> IsUserInRole(int userId, string roleId);
        Task<List<string>> GetUserRolesAsync(User user);
        Task<bool> ValidateUserCredentials(string userName, string password);
    }
}
