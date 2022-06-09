namespace Boovey.Services.Managers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Interfaces;
    using Data.Entities;
    using Boovey.Services.Interfaces.IManagers;

    public class BooveyUserManager : UserManager<User>, IUserManager
    {
        public BooveyUserManager(IUserStore<User> store,
           IOptions<IdentityOptions> optionsAccessor,
           IPasswordHasher<User> passwordHasher,
           IEnumerable<IUserValidator<User>> userValidators,
           IEnumerable<IPasswordValidator<User>> passwordValidators,
           ILookupNormalizer keyNormalizer,
           IdentityErrorDescriber errors,
           IServiceProvider services,
           ILogger<UserManager<User>> logger) :
           base(store,
           optionsAccessor,
           passwordHasher,
           userValidators,
           passwordValidators,
           keyNormalizer,
           errors,
           services,
           logger)
        {

        }

        public async Task<ICollection<User>> GetAllAsync()
        {
            return await Users.ToListAsync();
        }

        public async Task<List<string>> GetUserRolesAsync(User user)
        {
            return (await GetRolesAsync(user)).ToList();
        }

        public async Task<bool> IsUserInRole(int userId, string roleName)
        {
            User user = await FindByIdAsync(userId.ToString());
            return await IsInRoleAsync(user, roleName);
        }

        public async Task<bool> ValidateUserCredentials(string userName, string password)
        {
            User user = await FindByNameAsync(userName);

            if (user != null)
            {
                bool result = await CheckPasswordAsync(user, password);
                return result;
            }
            else
            {
                user = await FindByEmailAsync(userName);
                if (user != null)
                {
                    bool result = await CheckPasswordAsync(user, password);
                    return result;
                }
            }
            return false;
        }
    }
}
