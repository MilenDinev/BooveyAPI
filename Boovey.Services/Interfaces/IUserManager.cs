namespace Boovey.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Data.Entities;

    public interface IUserManager
    {
        Task<IdentityResult> CreateAsync(User user, string password);
        Task<IdentityResult> UpdateAsync(User user);
        Task<IdentityResult> DeleteAsync(User user);
        Task<IdentityResult> AddToRoleAsync(User user, string role);
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
