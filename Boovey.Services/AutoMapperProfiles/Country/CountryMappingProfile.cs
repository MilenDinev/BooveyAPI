namespace Boovey.Services.AutoMapperProfiles.Country
{
    using AutoMapper;
    using Data.Entities;

    public class CountryMappingProfile : Profile
    {
        public CountryMappingProfile()
        {
            CreateMap<string, Country>();
        }
    }
}
