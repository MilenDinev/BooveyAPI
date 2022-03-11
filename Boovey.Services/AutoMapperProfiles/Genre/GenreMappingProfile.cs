namespace Boovey.Services.AutoMapperProfiles.Genre
{
    using System;
    using AutoMapper;
    using Data.Entities;
    using Models.Requests;

    public class GenreMappingProfile : Profile
    {
        public GenreMappingProfile()
        {
            this.CreateMap<AddGenreModel, Genre>()
                .ForMember(e => e.CreatedOn, m => m.MapFrom(d => DateTime.Now))
                .ForMember(e => e.LastModifiedOn, m => m.MapFrom(d => DateTime.Now));
        }
    }
}
