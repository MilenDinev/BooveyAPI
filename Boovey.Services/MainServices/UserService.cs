namespace Boovey.Services.MainServices
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using System.Collections.Generic;
    using Interfaces;
    using Base;
    using AutoMapper;
    using Constants;
    using Exceptions;
    using Managers.Interfaces;
    using Data;
    using Data.Entities;
    using Models.Requests;
    using Models.Responses.UserModels;
    using Models.Requests.UserModels;

    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserManager userManager;
        private readonly IMapper mapper;

        public UserService(IUserManager userManager, BooveyDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<User> GetCurrentUserAsync(ClaimsPrincipal principal)
        {
            return await userManager.GetUserAsync(principal);
        }

        public async Task<ICollection<UserListingModel>> GetAllUsersAsync()
        {
            var users = await userManager.GetAllAsync();
            var usersResponceDto = mapper.Map<ICollection<UserListingModel>>(users);
            return usersResponceDto.ToList();
        }

        public async Task<User> CreateAsync(CreateUserModel userInput)
        {

            var user = mapper.Map<User>(userInput);
            await userManager.CreateAsync(user, userInput.Password);
            await userManager.AddToRoleAsync(user, "regular");

            return user;
        }

        public async Task EditAsync(User user, EditUserModel userModel, int modifierId)
        {
            user.UserName = userModel.UserName;
            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.Email = userModel.Email;

            user.NormalizedUserName = user.UserName.ToUpper();
            await SaveModificationAsync(user, modifierId);
        }

        public async Task DeleteAsync(User user, int modifierId)
        {
            await DeleteEntityAsync(user, modifierId);
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
            return mapper.Map<FollowerModel>(follower);
        }

        public async Task<UserListingModel> ListUserByIdAsync(int userId)
        {
            return mapper.Map<UserListingModel>(await GetUserByIdAsync(userId));
        }

        private async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString())
                ?? throw new ResourceNotFoundException(string.Format(ErrorMessages.EntityIdDoesNotExist, nameof(Country), userId));

            return user;
        }
    }
}
