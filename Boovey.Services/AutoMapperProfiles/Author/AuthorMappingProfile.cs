namespace Boovey.Services.AutoMapperProfiles.Author
{
    using System.Linq;
    using AutoMapper;
    using Data.Entities;
    using Models.Requests.AuthorModels;
    using Models.Responses.AuthorModels;

    public class AuthorMappingProfile : Profile
    {
        public AuthorMappingProfile()
        {
            this.CreateMap<AddAuthorModel, Author>();
            this.CreateMap<Author, AddedAuthorModel>();
            this.CreateMap<Author, EditedAuthorModel>()
                .ForMember(m => m.Nationality, e => e.MapFrom(a => a.Country.Name));
            this.CreateMap<Author, AddedFavoriteAuthorModel>()
                .ForMember(m => m.Username, e => e.MapFrom(a => a.FavoriteByUsers.Select(u => u.UserName).LastOrDefault()));
            this.CreateMap<Author, RemovedFavoriteAuthorModel>();
            this.CreateMap<Author, AuthorsListingModel>()
                .ForMember(m => m.Nationality, e => e.MapFrom(a => a.Country.Name));
        }
    }
}
