namespace Boovey.Services.AutoMapperProfiles.User
{
    using AutoMapper;
    using Data.Entities;
    using Models.Requests;
    using Models.Responses.UserModels;

    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            this.CreateMap<RegistrationModel, User>();
            this.CreateMap<User, RegisteredUserModel>();
            this.CreateMap<User, UsersListingModel>()
                .ForMember(m => m.Email, e => e.MapFrom(u => u.Email ?? "none"));
        }
    }
}
