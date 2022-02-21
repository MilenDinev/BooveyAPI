namespace Boovey.Services.AutoMapperProfiles.Genre
{
    using AutoMapper;
    using Data.Entities;
    using Models.Requests;

    public class GenreMappingProfile : Profile
    {
        public GenreMappingProfile()
        {
            this.CreateMap<AddGenreModel, Genre>();
        }
    }
}
