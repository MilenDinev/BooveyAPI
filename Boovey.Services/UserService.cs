namespace Boovey.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using System.Collections.Generic;
    using AutoMapper;
    using Interfaces;
    using Exceptions;
    using Constants;
    using Data;
    using Data.Entities;
    using Models.Responses.UserModels;
    using Models.Requests;

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

        public async Task<RegisteredUserModel> CreateAsync(RegistrationModel userInput)
        {
            if (await this.userManager.FindByNameAsync(userInput.UserName) != null)
                throw new ArgumentException(string.Format(ErrorMessages.EntityAlreadyExists, nameof(User), userInput.UserName));

            if (await this.userManager.FindByEmailAsync(userInput.Email) != null)
                throw new ArgumentException(string.Format(ErrorMessages.EntityAlreadyExists, nameof(userInput.Email), userInput.Email));

            var user = this.mapper.Map<User>(userInput);

            await this.userManager.CreateAsync(user, userInput.Password);
            await this.userManager.AddToRoleAsync(user, "regular");

            return this.mapper.Map<RegisteredUserModel>(user);
        }

        public async Task<User> GetCurrentUserAsync(ClaimsPrincipal principal)
        {
            return await this.userManager.GetUserAsync(principal);
        }

        public async Task<ICollection<UsersListingModel>> GetAllUsersAsync()
        {
            var users = await this.userManager.GetAllAsync();
            var usersResponceDto = this.mapper.Map<ICollection<UsersListingModel>>(users);
            return usersResponceDto.ToList();
        }

        public async Task<FollowerModel> Follow(User follower, int followedId)
        {
            if (follower.Id == followedId)
                throw new ArgumentException(ErrorMessages.FollowingItSelf);

            var followed = await GetUserByIdAsync(followedId);
            if (follower.Following.Contains(followed))
                throw new ResourceAlreadyExistsException(string.Format(ErrorMessages.AlreadyFollowing, nameof(User), followed.UserName));
           
            follower.Following.Add(followed);

            await dbContext.SaveChangesAsync();
            return this.mapper.Map<FollowerModel>(follower);
        }

        public async Task<UsersListingModel> ListUserByIdAsync(int userId)
        {
            return this.mapper.Map<UsersListingModel>(await GetUserByIdAsync(userId));
        }

        private async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await this.userManager.FindByIdAsync(userId.ToString())
                ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Country), userId));

            return user;
        }
    }
}
