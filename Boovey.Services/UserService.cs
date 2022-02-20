namespace Boovey.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using System.Collections.Generic;
    using AutoMapper;
    using Interfaces;
    using Data;
    using Data.Entities;
    using Models.Responses.UserModels;

    public class UserService : IUserService
    {
        private readonly IUserManager userManager;
        private readonly BooveyDbContext dbContext;
        private readonly IMapper mapper;

        public UserService(IUserManager userManager, BooveyDbContext dbContext, IMapper mapper)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<User> GetCurrentUserAsync(ClaimsPrincipal principal)
        {
            return await this.userManager.GetUserAsync(principal);
        }

        public async Task<ICollection<UserResponseModel>> GetAllUsersAsync()
        {
            var users = await this.userManager.GetAllAsync();
            var usersResponceDto = this.mapper.Map<ICollection<UserResponseModel>>(users);
            return usersResponceDto.ToList();
        }
    }
}
