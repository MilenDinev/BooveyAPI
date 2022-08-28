namespace Boovey.Services.AutoMapperProfiles.User
{
    using System.Linq;
    using AutoMapper;
    using Data.Entities;
    using Models.Requests.UserModels;
    using Models.Responses.UserModels;

    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            this.CreateMap<CreateUserModel, User>()
            .ForMember(e => e.NormalizedName, m => m.MapFrom(m => m.UserName.ToUpper()));
            this.CreateMap<User, CreatedUserModel>();
            this.CreateMap<User, EditedUserModel>();
            this.CreateMap<User, DeletedUserModel>();
            this.CreateMap<User, FollowerModel>()
                .ForMember(m => m.FollowerId, e => e.MapFrom(u => u.Id))
                .ForMember(m => m.FollowedId, e => e.MapFrom(u => u.Following.Select(u => u.Id).LastOrDefault()));
            this.CreateMap<User, UserListingModel>()
                .ForMember(m => m.Email, e => e.MapFrom(u => u.Email ?? "none"))
                .ForMember(m => m.FavoriteAuthors, e => e.MapFrom(u => u.FavoriteAuthors.Select(u => u.Fullname).ToList()))
                .ForMember(m => m.FavoriteBooks, e => e.MapFrom(u => u.FavoriteBooks.Select(u => u.Title).ToList()))
                .ForMember(m => m.FavoriteGenres, e => e.MapFrom(u => u.FavoriteGenres.Select(u => u.Title).ToList()))
                .ForMember(m => m.Followers, e => e.MapFrom(u => u.Followers.Select(u => u.FirstName + " " + u.LastName).ToList()))
                .ForMember(m => m.Following, e => e.MapFrom(u => u.Following.Select(u => u.FirstName + " " + u.LastName).ToList()));
        }
    }
}
