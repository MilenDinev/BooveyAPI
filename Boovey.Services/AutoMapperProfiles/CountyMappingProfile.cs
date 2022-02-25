namespace Boovey.Services.AutoMapperProfiles.Book
{
    using AutoMapper;
    using Data.Entities;

    public class CountyMappingProfile : Profile
    {
        public CountyMappingProfile()
        {
            this.CreateMap<string, Country>();
        }
    }
}
