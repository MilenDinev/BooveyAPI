namespace Boovey.Services.AutoMapperProfiles.User
{
    using System.Linq;
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
            this.CreateMap<User, FollowerModel>()
                .ForMember(m => m.FollowerId, e => e.MapFrom(u => u.Id))
                .ForMember(m => m.FollowedId, e => e.MapFrom(u => u.Following.Select(u => u.Id).LastOrDefault()));
        }
    }
}
