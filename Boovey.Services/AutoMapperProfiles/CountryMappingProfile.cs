namespace Boovey.Services.AutoMapperProfiles.Book
{
    using AutoMapper;
    using Data.Entities;

    public class CountryMappingProfile : Profile
    {
        public CountryMappingProfile()
        {
            this.CreateMap<string, Country>();
        }
    }
}
