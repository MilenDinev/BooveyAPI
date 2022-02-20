namespace Boovey.Services.AutoMapperProfiles.User
{
    using AutoMapper;
    using Boovey.Data.Entities;
    using Boovey.Models.Responses.UserModels;

    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            this.CreateMap<User, UserResponseModel>()
                .ForMember(m => m.Email, e => e.MapFrom(u => u.Email ?? "none"));
        }
    }
}
