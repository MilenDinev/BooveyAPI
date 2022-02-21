namespace Boovey.Services.AutoMapperProfiles.User
{
    using AutoMapper;
    using Boovey.Data.Entities;
    using Boovey.Models.Requests;
    using Boovey.Models.Responses.UserModels;

    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            this.CreateMap<RegistrationRequestModel, User>();
            this.CreateMap<User, RegisteredUserResponseModel>();
            this.CreateMap<User, UsersListingResponseModel>()
                .ForMember(m => m.Email, e => e.MapFrom(u => u.Email ?? "none"));
        }
    }
}
