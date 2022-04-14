namespace Boovey.Services.AutoMapperProfiles.Genre
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Data.Entities;
    using Models.Requests.GenreModels;
    using Models.Responses.GenreModels;

    public class GenreMappingProfile : Profile
    {
        public GenreMappingProfile()
        {
            this.CreateMap<CreateGenreModel, Genre>()
                .ForMember(e => e.CreatedOn, m => m.MapFrom(d => DateTime.Now))
                .ForMember(e => e.LastModifiedOn, m => m.MapFrom(d => DateTime.Now));
            this.CreateMap<Genre, CreatedGenreModel>();
            this.CreateMap<Genre, EditedGenreModel>();
            this.CreateMap<Genre, DeletedGenreModel>();
            this.CreateMap<Genre, AddedFavoriteGenreModel>()
                .ForMember(m => m.GenreId, e => e.MapFrom(g => g.Id))
                .ForMember(m => m.UserId, e => e.Ignore());
            this.CreateMap<Genre, RemovedFavoriteGenreModel>()
                .ForMember(m => m.GenreId, e => e.MapFrom(g => g.Id))
                .ForMember(m => m.UserId, e => e.Ignore());
            this.CreateMap<Genre, GenreListingModel>();
        }
    }
}
