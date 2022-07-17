namespace Boovey.Services.MainServices.Interfaces
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Data.Entities;
    using Models.Requests.UserModels;
    using Models.Responses.UserModels;

    public interface IUserService
    {
        Task<User> CreateAsync(CreateUserModel userRequestModel);
        Task EditAsync(User user, EditUserModel userModel, int modifierId);
        Task DeleteAsync(User user, int modifierId);

        Task<FollowerModel> Follow(User follower, int followedId);
        Task<User> GetCurrentUserAsync(ClaimsPrincipal principal);
        Task<UserListingModel> ListUserByIdAsync(int userId);
        Task<ICollection<UserListingModel>> GetAllUsersAsync();
    }
}
