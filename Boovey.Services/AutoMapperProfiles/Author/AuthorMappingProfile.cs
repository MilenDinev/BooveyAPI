namespace Boovey.Services.AutoMapperProfiles.Author
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Data.Entities;
    using Models.Requests.AuthorModels;
    using Models.Responses.AuthorModels;

    public class AuthorMappingProfile : Profile
    {
        public AuthorMappingProfile()
        {
            this.CreateMap<AddAuthorModel, Author>()
                .ForMember(e => e.CreatedOn, m => m.MapFrom(d => DateTime.Now))
                .ForMember(e => e.LastModifiedOn, m => m.MapFrom(d => DateTime.Now));
            this.CreateMap<Author, AddedAuthorModel>();
            this.CreateMap<Author, EditedAuthorModel>()
                .ForMember(m => m.Nationality, e => e.MapFrom(a => a.Country.Name));
            this.CreateMap<Author, AddedFavoriteAuthorModel>()
                .ForMember(m => m.AuthorId, e => e.MapFrom(a => a.Id))
                .ForMember(m => m.UserId, e => e.MapFrom(a => a.FavoriteByUsers.Select(u => u.Id).LastOrDefault()));
            this.CreateMap<Author, RemovedFavoriteAuthorModel>()
                .ForMember(m => m.AuthorId, e => e.MapFrom(a => a.Id))
                .ForMember(m => m.UserId, e => e.Ignore());
            this.CreateMap<Author, AuthorListingModel>()
                .ForMember(m => m.Nationality, e => e.MapFrom(a => a.Country.Name));
        }
    }
}
