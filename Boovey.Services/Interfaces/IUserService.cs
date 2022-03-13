namespace Boovey.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Data.Entities;
    using Models.Requests;
    using Models.Responses.UserModels;

    public interface IUserService
    {
        Task<RegisteredUserModel> CreateAsync(RegistrationModel userRequestModel);
        Task<FollowerModel> Follow(User follower, int followedId);
        Task<User> GetCurrentUserAsync(ClaimsPrincipal principal);
        Task<UsersListingModel> ListUserByIdAsync(int userId);
        Task<ICollection<UsersListingModel>> GetAllUsersAsync();
    }
}
