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
        Task<User> GetCurrentUserAsync(ClaimsPrincipal principal);
        Task<ICollection<UsersListingModel>> GetAllUsersAsync();
        Task<RegisteredUserModel> CreateAsync(RegistrationModel userRequestModel);
    }
}
